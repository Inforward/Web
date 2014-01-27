using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bespoke.Infrastructure.Configuration;
using Bespoke.Web.Models;
using Microsoft.Owin.Security;

namespace Bespoke.Web.Controllers
{
    public abstract class BaseController : Controller
    {
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

        #endregion

        #region Overrides

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

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