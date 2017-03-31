namespace Twitter.Web.MVC.ViewModels
{
    using System;
    using Infrastructures.Mapping;
    using Models;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public string Content { get; set; }

        public NotificationTypes Type { get; set; }

        public DateTime Date { get; set; }
        
        public virtual Models.User User { get; set; }
    }
}