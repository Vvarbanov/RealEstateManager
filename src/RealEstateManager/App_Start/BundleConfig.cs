using System.Web.Optimization;

namespace RealEstateManager
{
    public static class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryfancybox").Include(
                "~/Scripts/jquery.fancybox.min.js"));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                "~/Content/bootstrap/bootstrap.min.css",
                "~/Content/PagedList.css",
                "~/Content/jquery.fancybox.min.css",
                "~/Content/Site.min.css",
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/jquery-ui-timepicker-addon.min.css"));

            bundles.Add(new StyleBundle("~/Content/publiccss").Include(
                "~/Content/bootstrap/bootstrap.min.css",
                "~/Content/PagedList.css",
                "~/Content/jquery.fancybox.min.css",
                "~/Areas/Public/Content/Site.min.css",
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/jquery-ui-timepicker-addon.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/adminjqueryuijs").Include(
                "~/Scripts/jquery-ui-1.12.1.min.js",
                "~/Scripts/jquery-ui-timepicker-addon.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/publicjqueryuijs").Include(
                "~/Scripts/jquery-ui-1.12.1.min.js",
                "~/Scripts/jquery-ui-timepicker-addon.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/publicjs").Include(
                "~/Areas/Public/Scripts/site.js"));
        }
    }
}
