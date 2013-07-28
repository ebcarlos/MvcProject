using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Website
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/styles").Include(
                "~/Content/Site.css",
                "~/Content/Extern/bootstrap.css",
                "~/Content/Extern/alertify.css"));

            bundles.Add(new ScriptBundle("~/scripts").Include(
                "~/Scripts/Site.js",
                "~/Scripts/Extern/jquery-1.7.1.js",
                "~/Scripts/Extern/jquery.unobtrusive-ajax.js",
                "~/Scripts/Extern/jquery.validate.js",
                "~/Scripts/Extern/jquery.validate.unobtrusive.js",
                "~/Scripts/Extern/bootstrap.js",
                "~/Scripts/Extern/alertify.js"));
        }
    }
}