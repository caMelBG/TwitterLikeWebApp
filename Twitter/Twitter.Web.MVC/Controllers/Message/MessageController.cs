namespace Twitter.Web.MVC.Controllers.Message
{
    using System.Linq;
    using System.Web.Mvc;
    using PagedList;

    using AutoMapper.QueryableExtensions;

    using Twitter.Data;
    using Twitter.Web.MVC.ViewModels;

    [Authorize]
    public class MessageController : BaseController
    {
        public MessageController(ITwitterData context) : base(context)
        {
        }

        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var messages = this.Context.Messages.All()
                   .Where(m => m.Receiver.UserName == User.Identity.Name)
                   .OrderBy(m => m.PostedOn)
                   .ProjectTo<MessageViewModel>()
                   .ToPagedList(page, pageSize);
            return View(messages);
        }
    }
}
