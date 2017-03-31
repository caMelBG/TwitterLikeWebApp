namespace Twitter.Web.MVC.Controllers
{
    using System.Web.Mvc;

    using Data;

    public abstract class BaseController : Controller
    {
        public BaseController(ITwitterData context)
        {
            this.Context = context;
        }

        protected ITwitterData Context { get; private set; }
    }
}
