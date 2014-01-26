using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bespoke.Services.Messages.UserService
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FacebookAccessToken { get; set; }
        public LoginProviders LoginProvider { get; set; }
    }
}
