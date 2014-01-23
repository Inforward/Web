using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bespoke.Web.Controllers
{
    public class AccountController : BaseController
    {
        [Route("~/signin", Name = "Signin")]
        public ActionResult Signin()
        {
            return View();
        }
	}
}