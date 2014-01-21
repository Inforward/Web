using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bespoke.Infrastructure.Configuration;

namespace Bespoke.Web.Helpers
{
    public static class HtmlExtensions
    {
        public static string PageTitle(this HtmlHelper helper, string subtitle = null)
        {
            var title = SettingsHelper.Get<string>("Site.Title");

            if (!string.IsNullOrWhiteSpace(subtitle))
                title = string.Format("{0} | {1}", title, subtitle);

            return title;
        }

        public static string PageTitle(this HtmlHelper helper, params string[] titleParts)
        {
            var title = SettingsHelper.Get<string>("Site.Title");

            if (titleParts != null && titleParts.Length > 0)
            {
                title = titleParts.Aggregate(title, (current, part) => current + string.Concat(" | ", part));
            }

            return title;
        }

        public static string BodyCssClass(this HtmlHelper helper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            return string.Format("{0} {1}", routeValues["controller"], routeValues["action"]).ToLower();
        }
    }
}