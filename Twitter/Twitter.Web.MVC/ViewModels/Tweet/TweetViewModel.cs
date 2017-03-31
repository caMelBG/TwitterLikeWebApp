namespace Twitter.Web.MVC.ViewModels
{
    using System;
    
    using Infrastructures.Mapping;

    public class TweetViewModel : IMapFrom<Models.Tweet>
    {
        public int Id { get; set; }

        public string Content { get; set; }
        
        public DateTime PostedOn { get; set; }
        
        public UserViewModel Author { get; set; }
    }
}
