using System;
using System.Linq;
using System.Security.Cryptography;
using Bespoke.Data;
using Bespoke.Infrastructure.Extensions;
using Bespoke.Infrastructure.Helpers;
using Bespoke.Models;
using Bespoke.Services.Helpers;
using Bespoke.Services.Contracts;
using Bespoke.Services.Messages.UserService;
using Facebook;

namespace Bespoke.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var response = new LoginResponse();
            var user = default(User);

            try
            {
                Required.NotEmpty(request.Email, "Email");
                Required.NotEmpty(request.Password, "Password");

                switch (request.LoginProvider)
                {
                    case LoginProviders.Email:
                    {
                        user = LoginWithEmail(request.Email, request.Password);
                    }
                        break;
                    case LoginProviders.Facebook:
                    {
                        user = LoginWithFacebook(request.FacebookAccessToken);
                    }
                        break;
                    case LoginProviders.Google:
                    {
                        user = LoginWithGoogle();
                    }
                        break;
                }

                if (user == null)
                    throw new Exception("Could not retrieve user details");

                response.User = user;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var response = new CreateUserResponse();

            try
            {
                Required.NotEmpty(request.Email, "Email");
                Required.NotEmpty(request.Password, "Password");
                Required.NotEmpty(request.FirstName, "FirstName");
                Required.NotEmpty(request.LastName, "LastName");

                var exists = _userRepository.GetByEmail(request.Email) != null;

                if (exists)
                    throw new ApplicationException("An account with this e-mail already exists");

                var password = new SaltedHash(request.Password);

                var user = new User()
                    {
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PasswordHash = password.Hash,
                        PasswordSalt = password.Salt,
                        UserRegistrationMethod = UserRegistrationMethods.Email
                    };

                _userRepository.Insert(user);

                response.User = user;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private User LoginWithEmail(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

            if (user == null || user.UserRegistrationMethod != UserRegistrationMethods.Email || !SaltedHash.Verify(user.PasswordSalt, user.PasswordHash, password))
                throw new Exception("Invalid email address or password");

            return user;
        }

        private User LoginWithFacebook(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException("accessToken", "Invalid access token");

            var client = new FacebookClient(accessToken);

            dynamic facebookUser = client.Get("me");

            var user = new User()
            {
                Email = facebookUser.email,
                FirstName = facebookUser.first_name,
                LastName = facebookUser.last_name,
                FacebookUserId = Convert.ToInt64(facebookUser.id),
                UserRegistrationMethod = UserRegistrationMethods.Facebook
            };

            var existingUser = _userRepository.GetByEmail(user.Email);

            if(existingUser != null && existingUser.UserRegistrationMethod != UserRegistrationMethods.Facebook)
                throw new Exception("Email account already exists.");


            if (existingUser == null)
            {
                _userRepository.Insert(user);
            }

            return user;
        }

        private User LoginWithGoogle()
        {
            throw new Exception("Not implemented");
        }
    }
}