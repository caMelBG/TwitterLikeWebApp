namespace Twitter.Data.Infrastructure
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class DataImporter
    {
        private const string HTMLUrl = "http://www.gametracker.com/server_info/79.124.56.61:27029/top_players/?searchipp=50&searchpge={0}#search";
        private readonly HTMLReader reader;
        private readonly HTMLParser parser;
        private IdentityDbContext<User> db;
        private UserManager<User> userManager;

        public DataImporter(UserManager<User> userManager, IdentityDbContext<User> db)
        {
            this.reader = new HTMLReader();
            this.parser = new HTMLParser();
            this.db = db;
            this.userManager = userManager;
        }
    
        public void ImportUsers()
        {
            int pageIndex = 5;
            for (int index = 1; index <= pageIndex; index++)
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
                        Email = userEmail
                    };
                    this.userManager.Create(user, password);
                }
            }
        }

        public void MakeRelationBetweenUsers(TwitterDbContext db)
        {

        }

        public void ImportTweets()
        {

        }
    }
}
