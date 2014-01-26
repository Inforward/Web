using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bespoke.Models.Auth
{
    public class FacebookUser
    {
        public FacebookUser()
        {
            Friends = new List<FacebookUser>();
        }

        public long FacebookUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<FacebookUser> Friends { get; set; }
    }
}
