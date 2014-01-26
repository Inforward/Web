using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bespoke.Services;
using Bespoke.Services.Contracts;
using Bespoke.Services.Messages.UserService;
using Bespoke.Web.Models.Account;

namespace Bespoke.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("~/login", Name = "Login")]
        public ActionResult Login()
        {
            var model = new LoginModel() {PersistLogin = true};

            return View(model);
        }

        [HttpPost]
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

            return Json(response);
        }

        public JsonResult FacebookLogin(string accessToken)
        {
            var request = new LoginRequest()
                {
                    FacebookAccessToken = accessToken,
                    LoginProvider = LoginProviders.Facebook
                };

            var response = _userService.Login(request);

            return Json(response);
        }
	}
}