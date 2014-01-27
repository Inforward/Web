using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bespoke.Web.Models.Account
{
    public class LoginModel
    {
        [Required]
        [Display(Name="Email Address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Keep me logged in")]
        public bool PersistLogin { get; set; }
    }
}