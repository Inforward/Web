using System.ComponentModel.DataAnnotations;

namespace Bespoke.Web.Models.Account
{
    public class LoginRegisterModel
    {
        [Required]
        [Display(Name = "Email Address")]
        public string LoginEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }

        [Display(Name = "Keep me logged in")]
        public bool LoginPersistLogin { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string SignupEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string SignupPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string SignupFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string SignupLastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string ResetPasswordEmail { get; set; }
    }
}