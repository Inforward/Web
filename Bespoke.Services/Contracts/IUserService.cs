using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Services.Messages.UserService;

namespace Bespoke.Services.Contracts
{
    public interface IUserService
    {
        GenericUserResponse Login(LoginRequest request);
        GenericUserResponse CreateUser(CreateUserRequest request);
        GenericUserResponse GetUserByEmail(string email);
    }
}
