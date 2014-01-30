using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bespoke.Data;
using Bespoke.Infrastructure.Extensions;
using Bespoke.Infrastructure.Helpers;
using Bespoke.Models;
using Bespoke.Services.Helpers;
using Bespoke.Services.Contracts;
using Bespoke.Services.Messages.UserService;

namespace Bespoke.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public GenericUserResponse Login(LoginRequest request)
        {
            var response = new GenericUserResponse();
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

        public GenericUserResponse CreateUser(CreateUserRequest request)
        {
            var response = new GenericUserResponse();

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

        public GenericUserResponse GetUserByEmail(string email)
        {
            var response = new GenericUserResponse();

            try
            {
                response.User = _userRepository.GetByEmail(email);
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
            Required.NotEmpty(email, "Email");
            Required.NotEmpty(password, "Password");

            var user = _userRepository.GetByEmail(email);

            if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash) || !SaltedHash.Verify(user.PasswordSalt, user.PasswordHash, password))
                throw new Exception("Invalid email address or password");

            return user;
        }

        private User LoginWithFacebook(string accessToken)
        {
            Required.NotEmpty(accessToken, "Access Token");

            var helper = new FacebookHelper(accessToken);

            var t1 = Task<User>.Factory.StartNew(helper.GetFacebookUser);
            var t2 = Task<List<Connection>>.Factory.StartNew(helper.GetFacebookFriends);

            Task.WaitAll(t1, t2);

            var user = t1.Result;

            if (user != null)
            {
                user.FacebookFriends = t2.Result;

                var existingUser = _userRepository.GetByEmail(user.Email);

                if (existingUser != null)
                {
                    user.Id = existingUser.Id;

                    if (user.FirstName.IsNullOrEmpty())
                        user.FirstName = existingUser.FirstName;

                    if (user.LastName.IsNullOrEmpty())
                        user.LastName = existingUser.LastName;

                    if (!existingUser.FacebookFriends.IsNullOrEmpty())
                    {
                        foreach (var friend in existingUser.FacebookFriends.Where(friend => !user.FacebookFriends.Exists(c => c.Id == friend.Id)))
                        {
                            user.FacebookFriends.Add(friend);
                        }
                    }
                }


                if (existingUser == null)
                {
                    _userRepository.Insert(user);
                }
                else
                {
                    _userRepository.Update(user);
                }
            }

            return user;
        }

        private User LoginWithGoogle()
        {
            throw new Exception("Not implemented");
        }
    }
}