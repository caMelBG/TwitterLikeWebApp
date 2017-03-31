namespace Twitter.Web.MVC.ViewModels
{
    using Infrastructures.Mapping;
    using System;
    using Twitter.Models;

    public class MessageViewModel : IMapFrom<Message>
    {
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public virtual Models.User Sender { get; set; }

        public virtual Models.User Receiver { get; set; }
    }
}