﻿using System.Web;
using System.Web.Optimization;

namespace AirlineReservation
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/assets/css/bootstrap.min.css",
                      "~/Content/assets/css/animate.min.css",
                      "~/Content/assets/css/light-bootstrap-dashboard.css",
                      "~/Content/assets/css/pe-icon-7-stroke.css",
                       "~/Content/assets/css/style.css"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                     "~/Scripts/js/jquery-1.10.2.js",
                     "~/Scripts/js/bootstrap.min.js",
                     "~/Scripts/js/bootstrap-checkbox-radio-switch.js",
                     "~/Scripts/js/chartist.min.js",
                      "~/Scripts/js/bootstrap-notify.js",
                       "~/Scripts/js/light-bootstrap-dashboard.js"));
        }
    }
}
