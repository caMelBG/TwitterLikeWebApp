namespace Twitter.Web.MVC.Controllers
{
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PagedList;

    using AutoMapper.QueryableExtensions;
    
    using Twitter.Data;
    using Twitter.Web.MVC.ViewModels;
    
    public class UserController : BaseController
    {

        public UserController(ITwitterData context) : base(context)
        {
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfilePage()
        {
            var user = this.Context.Users.All()
                .ProjectTo<DetailsUserViewModel>()
                .FirstOrDefault(u => u.UserName == User.Identity.Name);
            return View("Details", user);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetUsersBySearchQuery(string query = "", int page = 1, int pageSize = 25)
        {
            ViewData["Query"] = query;
            var users = this.Context.Users.All()
                .OrderBy(u => u.UserName)
                .ProjectTo<UserViewModel>()
                .Where(u => u.UserName.Contains(query) || u.FullName.Contains(query))
                .ToPagedList(page, pageSize);
            return PartialView("~/Views/User/Partial/_PagedUserListPartial.cshtml", users);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Image(int id)
        {
            var image = this.Context.Images.GetById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var user = this.Context.Users.All()
                .ProjectTo<DetailsUserViewModel>()
                .FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetFollowButton(string id)
        {
            var isInFollow = this.Context.Users.All()
                .Any(u => u.Id == id && u.Followers.Any(f => f.UserName == this.User.Identity.Name));
            if (isInFollow)
            {
                return PartialView("~/Views/User/Partial/Buttons/_UnFollowButtonPartial.cshtml");
            }
            else
            {
                return PartialView("~/Views/User/Partial/Buttons/_FollowButtonPartial.cshtml");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Follow(string id)
        {
            var user = this.Context.Users.All()
                .Include(u => u.Following)
                .FirstOrDefault(u => u.Id == id);
            var loggedUser = this.Context.Users.All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);
            if (user.Followers.Any(f => f.UserName == this.User.Identity.Name))
            {
                loggedUser.Following.Remove(user);
                user.Followers.Remove(loggedUser);
                this.Context.SaveChanges();
                return PartialView("~/Views/User/Partial/Buttons/_FollowButtonPartial.cshtml");
            }
            else
            {
                loggedUser.Following.Add(user);
                user.Followers.Add(loggedUser);
                this.Context.SaveChanges();
                return PartialView("~/Views/User/Partial/Buttons/_UnFollowButtonPartial.cshtml");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetFollowersByUserId(string id, int skip, int pageSize)
        {
            var followers = this.Context.Users.All()
                .Where(u => u.Following.Any(f => f.Id == id))
                .OrderBy(u => u.UserName)
                .Skip(skip * pageSize)
                .Take(pageSize)
                .ProjectTo<UserViewModel>()
                .AsEnumerable();
            return PartialView("~/Views/User/Partial/_ProfileUsersList.cshtml", followers);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetFollowingByUserId(string id, int skip, int pageSize)
        {
            var following = this.Context.Users.All()
                .Where(u => u.Followers.Any(f => f.Id == id))
                .OrderBy(u => u.UserName)
                .Skip(skip * pageSize)
                .Take(pageSize)
                .ProjectTo<UserViewModel>()
                .AsEnumerable();
            return PartialView("~/Views/User/Partial/_ProfileUsersList.cshtml", following);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetTweetsByUserId(string id, int skip, int pageSize)
        {
             var tweets = this.Context.Tweets.All()
                .Where(t => t.Author.Id == id)
                .OrderByDescending(t => t.PostedOn)
                .Skip(skip * pageSize)
                .Take(pageSize)
                .ProjectTo<TweetViewModel>()
                .ToList();
            return PartialView("~/Views/User/Partial/_ProfileTweetsList.cshtml", tweets);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetFavoritesByUserId(string id, int skip, int pageSize)
        {
            var user = this.Context.Users.All()
                .FirstOrDefault(u => u.Id == id);
            var tweets = user.FavouriteTweets
                .AsQueryable()
                .OrderByDescending(t => t.PostedOn)
                .Skip(skip * pageSize)
                .Take(pageSize)
                .ProjectTo<TweetViewModel>()
                .ToList();
            return PartialView("~/Views/User/Partial/_ProfileTweetsList.cshtml", tweets);
        }
    }
}
