namespace Twitter.Web.MVC.Controllers
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using PagedList;

    using AutoMapper.QueryableExtensions;

    using Twitter.Data;
    using Twitter.Models;
    using ViewModels;

    public class TweetController : BaseController
    {
        public TweetController(ITwitterData context) : base(context)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ListTweets(int page = 1, int pageSize = 10)
        {
            var tweets = this.Context.Tweets.All()
                .Include(t => t.Author)
                .OrderByDescending(t => t.PostedOn)
                .ProjectTo<TweetViewModel>()
                .ToPagedList(page, pageSize);
            return PartialView("~/Views/Tweet/Partial/_PagedTweetListPartial.cshtml", tweets);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create([Bind(Include = "Content")]CreateTweetViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var author = this.Context.Users.All()
                    .FirstOrDefault(u => u.UserName == User.Identity.Name);
                var tweet = new Tweet()
                {
                    Content = model.Content,
                    PostedOn = DateTime.Now,
                    Author = author
                };
                author.Tweets.Add(tweet);
                this.Context.Users.Update(author);
                this.Context.SaveChanges();
                return RedirectToAction("Index", "Tweet");
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EditTweetViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var tweet = this.Context.Tweets.GetById(model.Id);
                tweet.Content = model.Content;
                this.Context.Tweets.Update(tweet);
                this.Context.SaveChanges();
                return RedirectToAction("Index", "Tweet");
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var tweet = this.Context.Tweets.All()
                .ProjectTo<EditTweetViewModel>()
                .FirstOrDefault(t => t.Id == id);
            return View(tweet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult IsFavorite(int id)
        {
            var isFavorite = this.Context.Users.All()
                .Any(u =>
                    u.UserName == this.User.Identity.Name &&
                    u.FavouriteTweets.Any(t => t.Id == id));
            if (isFavorite)
            {
                return PartialView("~/Views/Tweet/Partial/Buttons/_FavoritedButtonPartial.cshtml");
            }
            else
            {
                return PartialView("~/Views/Tweet/Partial/Buttons/_FavoriteButtonPartial.cshtml");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult IsReTweeted(int id)
        {
            var isFavorite = this.Context.Users.All()
                .Any(u =>
                    u.UserName == this.User.Identity.Name &&
                    u.ReTweets.Any(t => t.Id == id));
            if (isFavorite)
            {
                return PartialView("~/Views/Tweet/Partial/Buttons/_ReTweetedButtonPartial.cshtml");
            }
            else
            {
                return PartialView("~/Views/Tweet/Partial/Buttons/_ReTweetButtonPartial.cshtml");
            }
        }

        [Authorize]
        [HttpPost]
        public void Favorite(int id)
        {
            var tweet = this.Context.Tweets.GetById(id);
            var loggedUser = this.Context.Users.All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);
            loggedUser.FavouriteTweets.Add(tweet);
            this.Context.SaveChanges();
        }

        [Authorize]
        [HttpPost]
        public void ReTweet(int id)
        {
            var tweet = this.Context.Tweets.GetById(id);
            var loggedUser = this.Context.Users.All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);
            loggedUser.ReTweets.Add(tweet);
            this.Context.SaveChanges();
        }
    }
}
