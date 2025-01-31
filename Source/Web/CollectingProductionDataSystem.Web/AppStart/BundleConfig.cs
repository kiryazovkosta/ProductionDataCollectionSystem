﻿using System.Web.Optimization;

namespace CollectingProductionDataSystem.Web.AppStart
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            ConfigureStyleBundles(bundles);
            ConfigureScriptBundles(bundles);
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }

        private static void ConfigureStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-theme.min.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo/kendo-stiles").Include(
                    "~/Content/kendo/kendo.common-bootstrap.min.css",
                    "~/Content/kendo/kendo.bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/custom/slidebar").Include(
                "~/Content/custom/slidebar.css"));

            bundles.Add(new StyleBundle("~/Content/custom/manager-dashboard").Include(
                "~/Content/custom/manDashSideBar.css"));
        }

        private static void ConfigureScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/custom/clearSeparators.js",
                      "~/Scripts/custom/bootstrapAdditional.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
            "~/Scripts/kendo/kendo.all.min.js",
            // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
            "~/Scripts/kendo/kendo.aspnetmvc.min.js",
            "~/Scripts/kendo/cultures/kendo.culture." + System.Threading.Thread.CurrentThread.CurrentCulture.Name + ".min.js",
            "~/Scripts/kendo/jszip.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom/sidebar").Include(
                "~/Scripts/custom/sidebar.js",
                "~/Scripts/custom/saveSiteBarStatus.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom/unitGrids").Include(
                "~/Scripts/custom/unitGridsData.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/custom/techReportToPdf").Include(
                            "~/Scripts/custom/techReportToPdfPageSetup.js"
                            ));
            bundles.Add(new ScriptBundle("~/bundles/custom/kendoadditional").Include(
                "~/Scripts/custom/kendoAdditional.js",
                "~/Scripts/custom/sendAntiForgery.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom/signalrR").Include(
                "~/Scripts/custom/signalR.js",
                "~/Scripts/custom/logoff.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom/renderMaterialGraphic")
                .Include("~/Scripts/custom/renderDailyMaterialGraphic.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom/renderEnergyGraphic")
                .Include("~/Scripts/custom/renderDailyEnergyGraphic.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom/dashboard")
                .Include("~/Scripts/custom/dashboard.js"));
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}
