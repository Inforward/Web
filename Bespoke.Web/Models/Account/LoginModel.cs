using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bespoke.Web.Models.Account
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool PersistLogin { get; set; }
    }
}