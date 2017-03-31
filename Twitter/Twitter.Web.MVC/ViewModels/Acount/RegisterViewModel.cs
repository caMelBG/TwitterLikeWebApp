namespace Twitter.Web.MVC.ViewModels.Acount
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(20, ErrorMessage = "The {0} must be with maximum of {1} characters long.")]
        [System.Web.Mvc.Remote("IsUsernameUsed", "Account", ErrorMessage = "{0} already exists!")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [System.Web.Mvc.Remote("IsEmailUsed", "Account", ErrorMessage = "{0} already exists!")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Avatar")]
        public HttpPostedFileBase UploadedImage { get; set; }
    }
}