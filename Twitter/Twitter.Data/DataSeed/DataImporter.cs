namespace Twitter.Data.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Microsoft.AspNet.Identity;

    using DataSeed;
    using Models;

    public class DataImporter
    {
        private const string FollowerNotificationContent = "{0} follow you!";
        private const string ReTweetNotificationContent = "{0} has just retweet your tweet!";
        private const string FavouriteTweetNotificationContent = "{0} has just favourite your tweet!";
        private const int TweetsPerUser = 32;
        private const int FavoriteTweetsPerUser = 32;
        private const int FollowersPerUser = 64;
        private const int UsersCount = 40;
        private const int ImagesCount = 16;
        private const string HTMLUrl = "http://www.gametracker.com/server_info/79.124.56.61:27029/top_players/?searchipp=50&searchpge={0}#search";
        private readonly HTMLReader reader;
        private readonly HTMLParser parser;
        private TwitterDbContext context;
        private UserManager<User> userManager;
        private RandomDataGenerator generator;
        private int usersCount;
        private int tweetsCount;
        private IEnumerable<User> users;

        public DataImporter(UserManager<User> userManager, TwitterDbContext context)
        {
            this.reader = new HTMLReader();
            this.parser = new HTMLParser();
            this.context = context;
            this.userManager = userManager;
            this.generator = new RandomDataGenerator();
        }

        public void ImportAdminRole()
        {
            var user = new User()
            {
                UserName = "admin",
                Email = "admin@abv.bg",
                Avatar = this.GetSampleImage()
            };
            this.userManager.Create(user, "admin");
            this.userManager.AddToRole(user.Id, "Admin");
            context.SaveChanges();
        }

        public void UploadImages()
        {
            //C:\Users\caMel.caMel-PC\Desktop\Twitter\Twitter.Data\bin\imgs
            for (int index = 0; index < ImagesCount; index++)
            {
                var path = AssemblyHelpers.GetDirectoryForAssembly(Assembly.GetExecutingAssembly());
                var finalPath = path + $"/imgs/download ({index}).jpg";
                var file = File.ReadAllBytes(finalPath);
                var image = new Image
                {
                    Content = file,
                    FileExtension = finalPath.Split(new[] { '.' }).Last()
                };
                context.Images.Add(image);
                context.SaveChanges();
            }
        }

        public void ImportUsers()
        {
            for (int index = 1; index <= UsersCount / 10; index++)
            {
                var currUrl = string.Format(HTMLUrl, index);
                var htmlText = this.reader.ReadFromWeb(currUrl);
                var usernames = this.parser.ExtractUserNames(htmlText);
                foreach (var username in usernames)
                {
                    var userEmail = username + "@abv.bg";
                    var password = "123456";
                    var user = new User()
                    {
                        UserName = username,
                        Email = userEmail,
                        FirstName = this.generator.GetRandomName(),
                        LastName = this.generator.GetRandomName(),
                        Avatar = this.GetSampleImage()
                    };
                    this.userManager.Create(user, password);
                    context.SaveChanges();
                }
            }
        }

        public void MakeRelationBetweenUsers()
        {
            usersCount = context.Users.Count();
            users = context.Users.ToList();
            foreach (var user in users)
            {
                for (int index = 0; index < FollowersPerUser; index++)
                {
                    var randNum = this.generator.GetRandomNumber(usersCount);
                    var randUser = context.Users.OrderBy(u => u.UserName).Skip(randNum).FirstOrDefault();
                    user.Followers.Add(randUser);
                    var notification = this.ImportNotification(user, randUser, NotificationTypes.Follower, FollowerNotificationContent);
                    user.Notifications.Add(notification);
                }

                context.SaveChanges();
            }
        }

        public void ImportTweets()
        {
            foreach (var user in users)
            {
                for (int index = 0; index < TweetsPerUser; index++)
                {
                    var tweet = new Tweet()
                    {
                        Content = this.generator.GetRandomTweetContent(),
                        PostedOn = this.generator.GetRandomDate(),
                        Author = user
                    };
                    user.Tweets.Add(tweet);
                }

                context.SaveChanges();
            }
            
            tweetsCount = context.Tweets.Count();
        }

        public void ImportFavouriteTweets()
        {
            foreach (var user in users)
            {
                for (int index = 0; index < FavoriteTweetsPerUser; index++)
                {
                    var randNum = this.generator.GetRandomNumber(usersCount);
                    var randUser = context.Users.OrderBy(u => u.UserName).Skip(randNum).FirstOrDefault();
                    if (user != randUser)
                    {
                        randNum = this.generator.GetRandomNumber(tweetsCount);
                        var randTweet = randUser.Tweets.Skip(randNum).FirstOrDefault();
                        user.FavouriteTweets.Add(randTweet);
                        var notification = this.ImportNotification(randUser, user, NotificationTypes.FavoutireTweet, FavouriteTweetNotificationContent);
                        randUser.Notifications.Add(notification);
                    }

                    context.SaveChanges();
                }
            }
        }

        private Notification ImportNotification(User user, User second, NotificationTypes type, string content)
        {
            var notification = new Notification()
            {
                Content = string.Format(content, second.UserName),
                Type = type,
                Date = this.generator.GetRandomDate(),
                User = user
            };
            return notification;
        }

        private Image GetSampleImage()
        {
            var randNum = this.generator.GetRandomNumber(ImagesCount);
            var image = context.Images.OrderBy(i => i.Id).Skip(randNum).FirstOrDefault();
            return image;
        }
    }
}
