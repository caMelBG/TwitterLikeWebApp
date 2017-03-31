namespace Twitter.Web.MVC.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Twitter.Web.MVC.Infrastructures.Mapping;
    using Models;

    public class CreateTweetViewModel : IMapFrom<Twitter.Models.Tweet>
    {
        [Required(ErrorMessage = "Tweet content is required!")]
        [MinLength(8, ErrorMessage = "Tweet content must be at least 8 characters!")]
        [MaxLength(256, ErrorMessage = "Tweet content must be with maximum of 256 charaters!")]
        public string Content { get; set; }
    }
}
