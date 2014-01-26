using System;
using System.Linq;
using System.Security.Cryptography;
using Bespoke.Data;
using Bespoke.Infrastructure.Extensions;
using Bespoke.Models;
using Bespoke.Models.Auth;
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

        private User LoginWithEmail(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

            if (user == null || user.UserRegistrationMethod != UserRegistrationMethods.Email || !ValidatePassword(password, user.PasswordHash, user.PasswordSalt))
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
                _userRepository.Create(user);
            }

            return user;
        }

        private User LoginWithGoogle()
        {
            throw new Exception("Not implemented");
        }

        private void HashPassword(string password, out string salt, out string passwordHash)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, 20))
            {
                var saltBytes = deriveBytes.Salt;
                var hashBytes = deriveBytes.GetBytes(20);

                salt = saltBytes.GetString();
                passwordHash = hashBytes.GetString();
            }
        }

        private bool ValidatePassword(string password, string passwordHash, string saltHash)
        {
            var salt = saltHash.GetBytes();
            var key = passwordHash.GetBytes();
            bool isValid;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt))
            {
                var newKey = deriveBytes.GetBytes(20);

                isValid = newKey.SequenceEqual(key);
            }

            return isValid;
        }
    }
}