using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Bespoke.Infrastructure.Configuration;
using Bespoke.Infrastructure.Extensions;
using Bespoke.Web.Models;
using Newtonsoft.Json;

namespace Bespoke.Web.Helpers
{
    public static class HtmlExtensions
    {
        public static string PageTitle(this HtmlHelper helper, string subtitle = null)
        {
            var title = SettingsHelper.Get<string>("Site.Name");

            if (!string.IsNullOrWhiteSpace(subtitle))
                title = string.Format("{0} | {1}", title, subtitle);

            return title;
        }

        public static string PageTitle(this HtmlHelper helper, params string[] titleParts)
        {
            var title = SettingsHelper.Get<string>("Site.Name");

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

        public static HtmlString RenderTags(this HtmlHelper helper, List<TagModel> tags)
        {
            if (tags.IsNullOrEmpty()) return MvcHtmlString.Empty;

            var sb = new StringBuilder();

            foreach (var tag in tags)
            {
                sb.AppendLine(tag.ToString());
            }

            return new HtmlString(sb.ToString());
        }

        public static HtmlString ToHtmlJson(this object obj)
        {
            return new HtmlString(JsonConvert.SerializeObject(obj));
        }

        public static HtmlString Base64Encode(this HtmlHelper helper, string stringToEncode)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(stringToEncode);

            return new HtmlString(Convert.ToBase64String(plainTextBytes));
        }
    }
}