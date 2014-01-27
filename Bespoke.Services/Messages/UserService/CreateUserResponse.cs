using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models;

namespace Bespoke.Services.Messages.UserService
{
    public class CreateUserResponse : BaseResponse
    {
        public User User { get; set; }
    }
}
