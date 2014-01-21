using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bespoke.Infrastructure.Configuration;
using Bespoke.Web.Models;

namespace Bespoke.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        #region Properties

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

            AddMetaNameTag("author", siteName);
            AddMetaNameTag("application-name", siteName);

            AddMetaPropertyTag("fb:app_id", SettingsHelper.Get<string>("Facebook.AppID"));
            AddMetaPropertyTag("og:site_name", siteName);
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

        #endregion
    }
}