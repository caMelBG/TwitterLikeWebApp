﻿namespace Twitter.Web.MVC.ViewModels.Manage
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using System.Collections.Generic;

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}