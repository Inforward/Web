using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bespoke.Web.Models.Account
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}