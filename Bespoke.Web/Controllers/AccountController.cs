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
            var model = new LoginModel() {PersistLogin = true};

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login", Name = "EmailLogin")]
        public JsonResult Login(LoginModel model)
        {
            var request = new LoginRequest()
                {
                    Email = model.Email,
                    Password = model.Password,
                    LoginProvider = LoginProviders.Email
                };

            var response = _userService.Login(request);

            if (response.Success)
            {
                SignIn(response.User, model.PersistLogin);
            }

            return Json(response);
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
        public JsonResult Signup(LoginModel model)
        {
            //if (ModelState.IsValid)
            {
                var request = new CreateUserRequest()
                {
                    Email = model.Email,
                    Password = model.Password
                };

                var response = _userService.CreateUser(request);

                if (response.Success)
                {
                    SignIn(response.User, model.PersistLogin);
                }

                return Json(response);
            }

            return Json(new { Success = false, Message = "Model Errors"});
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