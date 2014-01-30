using System.Web;
using System.Web.Optimization;

namespace Bespoke.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            // Application specific js bundle
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Assets/Scripts/events.js",
                "~/Assets/Scripts/pubsub.js",
                "~/Assets/Scripts/script-loader.js",
                "~/Assets/Scripts/article-share.js",
                "~/Assets/Scripts/facebook-provider.js",
                "~/Assets/Scripts/login.js",
                "~/Assets/Scripts/header.js",
                "~/Assets/Scripts/jump-menu.js",
                "~/Assets/Scripts/app.js"));

            // Vendor js bundle
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Assets/vendor/foundation/foundation.js",
                "~/Assets/vendor/foundation/foundation.offcanvas.js",
                "~/Assets/vendor/foundation/foundation.reveal.js",
                "~/Assets/vendor/foundation/foundation.dropdown.js",
                "~/Assets/vendor/jquery.scroll-start-stop.js",
                "~/Assets/vendor/jquery.watermark.js",
                "~/Assets/vendor/jquery.sharrre.js",
                "~/Assets/vendor/jquery.iosslider.min.js",
                "~/Assets/vendor/jquery.validate.js",
                "~/Assets/vendor/jquery.validate.unobtrusive.js",
                "~/Assets/vendor/jquery.form.js"));

            // Header bundle
            bundles.Add(new ScriptBundle("~/bundles/vendor/modernizr").Include(
                "~/Assets/vendor/custom.modernizr.min.js",
                "~/Assets/vendor/respond.js"));

            // Home page bundle
            bundles.Add(new ScriptBundle("~/bundles/vendor/home").Include(
                "~/Assets/vendor/jquery.themepunch.plugins.min.js",
                "~/Assets/vendor/jquery.themepunch.showbizpro.min.js"));

            // Form bundle (for use on pages with forms)
            //bundles.Add(new ScriptBundle("~/bundles/scripts/form").Include(
            //    "~/Assets/vendor/jquery.iosslider.min.js",
            //    "~/Assets/vendor/jquery.validate.js",
            //    "~/Assets/vendor/jquery.validate.unobtrusive.js",
            //    "~/Assets/vendor/jquery.form.js"));

        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            // All css
            var bundle = new StyleBundle("~/bundles/css");
            bundle.Include("~/Assets/css/normalize.css", new CssRewriteUrlTransform());
            bundle.Include("~/Assets/css/foundation.css", new CssRewriteUrlTransform());
            bundle.Include("~/Assets/css/font-awesome.css", new CssRewriteUrlTransform());
            bundle.Include("~/Assets/css/screen.css", new CssRewriteUrlTransform());
            bundles.Add(bundle);

            // Home page bundle
            var homeBundle = new StyleBundle("~/bundles/css/home");
            homeBundle.Include("~/Assets/css/showbiz.css", new CssRewriteUrlTransform());
            bundles.Add(homeBundle);

        }
    }
}
