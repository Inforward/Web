using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bespoke.Infrastructure.Configuration;
using Bespoke.Models;
using Bespoke.Services.Contracts;
using Bespoke.Web.Models;
using Bespoke.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Bespoke.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        #region Private Members

        private UserViewModel _currentUser;
        protected readonly IUserService _userService;

        #endregion

        #region Properties

        protected IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        protected List<TagModel> Tags
        {
            get
            {
                if (ViewBag.Tags == null)
                    ViewBag.Tags = new List<TagModel>();

                return (List<TagModel>)ViewBag.Tags;
            }
        }

        protected UserViewModel CurrentUser
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                    return null;

                if (_currentUser == null)
                    _currentUser = Session["CurrentUser"] as UserViewModel;

                if (_currentUser == null)
                {
                    var email = User.Identity.GetUserId();
                    var user = _userService.GetUserByEmail(email).User;

                    _currentUser = AccountController.ToUserViewModel(user);

                    Session["CurrentUser"] = _currentUser;
                }

                return _currentUser;
            }
        }

        #endregion

        #region Constructor

        protected BaseController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Overrides

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            ViewBag.CurrentUser = CurrentUser;

            var siteName = SettingsHelper.Get<string>("Site.Name");
            var facebookAppId = SettingsHelper.Get<string>("Facebook.AppID");

            AddMetaNameTag("author", siteName);
            AddMetaNameTag("application-name", siteName);

            AddMetaPropertyTag("fb:app_id", facebookAppId);
            AddMetaPropertyTag("og:site_name", siteName);

            ViewBag.SiteName = siteName;
            ViewBag.FacebookAppId = facebookAppId;
        }

        #endregion

        #region Protected Methods

        protected void AddMetaPropertyTag(string key, string value)
        {
            Tags.Add(new TagModel() { Key = key, Value = value, TagType = TagType.MetaProperty});
        }

        protected void AddMetaNameTag(string key, string value)
        {
            Tags.Add(new TagModel() { Key = key, Value = value, TagType = TagType.MetaName });
        }

        protected void AddLinkTag(string key, string value)
        {
            Tags.Add(new TagModel() { Key = key, Value = value, TagType = TagType.Link });
        }

        protected JsonResponse JsonResponse(Action action)
        {
            var response = new JsonResponse();

            try
            {
                action();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                // TODO: Log it!
            }

            return response;
        }

        protected JsonResponse JsonResponse<T>(Func<T> method)
        {
            var response = new JsonResponse();

            try
            {
                response.Data = (T)method();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                // TODO: Log it!
            }

            return response;
        }

        #endregion
    }
}