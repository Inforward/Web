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
            bundles.Add(new ScriptBundle("~/bundles/scripts").IncludeDirectory("~/Assets/Scripts", "*.js"));

            // Vendor js bundle
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Assets/vendor/foundation/foundation.js",
                "~/Assets/vendor/foundation/foundation.offcanvas.js",
                "~/Assets/vendor/jquery.scroll-start-stop.js",
                "~/Assets/vendor/jquery-ui-1.10.4.custom.js",
                "~/Assets/vendor/jquery.sharrre.js",
                "~/Assets/vendor/bamboo.0.1.js"));

            // Miscellaneous (single-page) js bundles
            bundles.Add(new ScriptBundle("~/bundles/vendor/modernizr").Include("~/Assets/vendor/custom.modernizr.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/vendor/iosslider").Include("~/Assets/vendor/jquery.iosslider.min.js"));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            // All css
            var bundle = new StyleBundle("~/bundles/css");

            bundle.Include("~/Assets/css/bamboo.css", new CssRewriteUrlTransform());
            bundle.Include("~/Assets/css/normalize.css", new CssRewriteUrlTransform());
            bundle.Include("~/Assets/css/foundation.css", new CssRewriteUrlTransform());
            bundle.Include("~/Assets/css/font-awesome.css", new CssRewriteUrlTransform());
            bundle.Include("~/Assets/css/screen.css", new CssRewriteUrlTransform());

            bundles.Add(bundle);
        }
    }
}
