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
        #region Private Members

        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Actions

        [HttpGet]
        [Route("~/login", Name = "Login")]
        public ActionResult Login()
        {
            var model = new LoginRegisterModel() {LoginPersistLogin = true};

            return View(model);
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

            return Json(response);
        }

        [HttpGet]
        [Route("~/logout", Name = "Logout")]
        public ActionResult Logout()
        {
            Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("login");
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

        #endregion

        #region Mapping Methods

        public static UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel()
            {
                ImageUrl = string.Empty,
                Name = user.Name,
                UserId = user.UserId
            };
        }

        #endregion

        #region Private Methods

        private void SignIn(User user, bool persistLogin)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Email) }, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);

            identity.AddClaim(new Claim(ClaimTypes.Role, "guest"));

            Authentication.SignIn(new AuthenticationProperties() { IsPersistent = persistLogin }, identity);
        }

        #endregion
    }
}