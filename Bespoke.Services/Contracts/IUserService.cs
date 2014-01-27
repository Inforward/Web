using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bespoke.Models.Auth;
using Bespoke.Services.Messages.UserService;

namespace Bespoke.Services.Contracts
{
    public interface IUserService
    {
        LoginResponse Login(LoginRequest request);
        CreateUserResponse CreateUser(CreateUserRequest request);
    }
}
