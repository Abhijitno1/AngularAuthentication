﻿using System.Web;
using System.Web.Optimization;

namespace AngularAuthentication
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/AngularApp").Include(
              "~/ngScripts/app.js",
              "~/ngScripts/controllers/about.js",
              "~/ngScripts/controllers/index.js",
              "~/ngScripts/controllers/login.js",
              "~/ngScripts/controllers/register.js",
              "~/ngScripts/controllers/landing-page.js",
              "~/ngScripts/services/login-data-service.js",
              "~/ngScripts/controllers/playlists-view.js",
              "~/ngScripts/controllers/playlist-videos.js",
              "~/ngScripts/services/utube-service.js"));
        }
    }
}
