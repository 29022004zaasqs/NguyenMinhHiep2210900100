using System.Web;
using System.Web.Optimization;

namespace NguyễnMinhHiệp_project2_2210900100
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Đăng ký bundle cho jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Đăng ký bundle cho jQuery Validate
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Đăng ký bundle cho Modernizr
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Đăng ký bundle cho Bootstrap JavaScript
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            // Đăng ký bundle cho Bootstrap CSS và CSS tùy chỉnh
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css")); 

            // Đăng ký bundle cho các tập tin JavaScript tùy chỉnh
            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Content/js/custom1.js",
                "~/Content/js/custom2.js"));

            // Đăng ký bundle cho các tập tin CSS tùy chỉnh
            bundles.Add(new StyleBundle("~/Content/customcss").Include(
                        "~/Content/css/customstyle.css",
                        "~/Content/css/anotherstyle.css"));

            
            BundleTable.EnableOptimizations = true; 
        }
    }
}
