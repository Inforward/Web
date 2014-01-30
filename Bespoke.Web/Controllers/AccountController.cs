using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.Models;
using Bespoke.Services;
using Bespoke.Services.Contracts;
using Bespoke.Services.Messages.UserService;
using Bespoke.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Bespoke.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Constructor

        public AccountController(IUserService userService) 
            : base(userService)
        {
            
        }

        #endregion

        #region Actions

        [HttpGet]
        [Route("~/login", Name = "Login")]
        [OutputCache(Duration = 600, VaryByParam = "*", VaryByContentEncoding = "gzip;deflate")]
        public PartialViewResult Login()
        {
            var model = new LoginRegisterModel() {LoginPersistLogin = true};

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login", Name = "EmailLogin")]
        public JsonResult Login(LoginRegisterModel model)
        {
            var request = new LoginRequest()
                {
                    Email = model.LoginEmail,
                    Password = model.LoginPassword,
                    LoginProvider = LoginProviders.Email
                };

            var response = _userService.Login(request);

            if (response.Success)
            {
                SignIn(response.User, model.LoginPersistLogin);
            }

            return Json(new
                {
                    response.Success,
                    response.Message,
                    User = response.User != null ? ToUserViewModel(response.User) : null
                });
        }

        [HttpPost]
        public JsonResult FacebookLogin(string accessToken)
        {
            var request = new LoginRequest()
                {
                    FacebookAccessToken = accessToken,
                    LoginProvider = LoginProviders.Facebook
                };

            var response = _userService.Login(request);

            if (response.Success)
            {
                SignIn(response.User, true);
            }

            return Json(new
                {
                    response.Success,
                    response.Message,
                    User = response.User != null ? ToUserViewModel(response.User) : null
                });
        }

        [HttpGet]
        [Route("~/logout", Name = "Logout")]
        public ActionResult Logout()
        {
            Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToRoute("Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Signup(LoginRegisterModel model)
        {
            var request = new CreateUserRequest()
            {
                Email = model.SignupEmail,
                Password = model.SignupPassword,
                FirstName = model.SignupFirstName,
                LastName = model.SignupLastName
            };

            var response = _userService.CreateUser(request);

            if (response.Success)
            {
                SignIn(response.User, true);
            }

            return Json(new
                {
                    response.Success,
                    response.Message,
                    User = response.User != null ? ToUserViewModel(response.User) : null
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ResetPassword(LoginRegisterModel model)
        {
            return Json(new {Success = true});
        }

        [HttpGet]
        [Route("profile", Name="AccountProfile")]
        public ActionResult AccountProfile()
        {
            return View("Profile");
        }

        #endregion

        #region Mapping Methods

        public static UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel()
            {
                ImageUrl = user.FacebookUserId.HasValue ? string.Format("http://graph.facebook.com/{0}/picture?type=small", user.FacebookUserId) : "/Assets/Images/profile-generic-small.gif",
                Name = user.Name
            };
        }

        #endregion

        #region Private Methods

        private void SignIn(User user, bool persistLogin)
        {
            var identity = new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.NameIdentifier, user.Email), new Claim(ClaimTypes.Name, user.Name)  }, 
                DefaultAuthenticationTypes.ApplicationCookie,
                ClaimTypes.Name, 
                ClaimTypes.Role);

            //identity.AddClaim(new Claim(ClaimTypes.Role, "guest"));

            Authentication.SignIn(new AuthenticationProperties() { IsPersistent = persistLogin }, identity);
        }

        #endregion
    }
}