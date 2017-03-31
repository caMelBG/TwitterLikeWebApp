namespace Twitter.Web.MVC.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Twitter.Web.MVC.Infrastructures.Mapping;

    public class EditTweetViewModel : IMapFrom<Twitter.Models.Tweet>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tweet content is required!")]
        [MinLength(8, ErrorMessage = "Tweet content must be at least 8 characters!")]
        [MaxLength(256, ErrorMessage = "Tweet content must be with maximum of 256 charaters!")]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostedOn { get; set; }
    }
}
