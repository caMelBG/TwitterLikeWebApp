namespace Twitter.Web.MVC.Controllers
{
    using System.Data;
    using System.Linq;
    using System.Web.Mvc;
    using PagedList;

    using AutoMapper.QueryableExtensions;

    using Twitter.Data;
    using ViewModels;

    [Authorize]
    public class NotificationController : BaseController
    {
        public NotificationController(ITwitterData context) : base(context)
        {
        }
        
        public ActionResult Index(int page = 1, int pageSize = 25)
        {
            var notifications = this.Context.Notifications.All()
                .Where(n => n.User.UserName == User.Identity.Name)
                .OrderBy(n => n.Date)
                .ProjectTo<NotificationViewModel>()
                .ToPagedList(page, pageSize);
            return View(notifications);
        }       
    }
}
