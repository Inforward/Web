using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bespoke.Infrastructure.Configuration;
using Bespoke.Services.Contracts;
using Bespoke.Web.Helpers;
using Bespoke.Web.Models.Discussions;

namespace Bespoke.Web.Controllers
{
    public class DiscussionsController : BaseController
    {
        #region Constructor

        public DiscussionsController(IUserService userService) 
            : base(userService)
        {
            
        }

        #endregion

        public ActionResult Index()
        {
            var model = new MootConfigModel()
            {
                ApiKey = SettingsHelper.Get<string>("Moot.ApiKey"),
                SecretKey = SettingsHelper.Get<string>("Moot.SecretKey"),
                Timestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds
            };

            model.Message = Extensions.Base64Encode("{\"user\":{}}");
            model.Signature = Extensions.SHA1HashStringForUTF8String(string.Format("{0} {1} {2}", model.SecretKey, model.Message, model.Timestamp));

            return View(model);
        }
	}
}