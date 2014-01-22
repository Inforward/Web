using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bespoke.Web.Controllers
{
    public class AccountController : Controller
    {
        [Route("~/signup", Name = "Signup")]
        public ActionResult Signup()
        {
            return View();
        }
	}
}