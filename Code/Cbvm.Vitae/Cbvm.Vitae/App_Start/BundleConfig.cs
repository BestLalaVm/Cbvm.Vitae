using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Cbvm.Vitae
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Manage/BaseJs").Include(
                "~/Scripts/jquery-1.9.1.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/validate/jquery.validate.js",
                "~/Scripts/validate/jquery.custom.validate.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxcore.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxdata.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxinput.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxcheckbox.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxscrollbar.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxlistbox.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxdropdownlist.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxcombobox.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxbuttons.js",
                "~/Scripts/jqwidgets/jqwidgets/jqxpanel.js"));


            bundles.Add(new ScriptBundle("~/Manage/Js").Include(
               "~/Scripts/BaseCv.js",
               "~/Scripts/XMCATool.js",
               "~/Scripts/Prompt.js"));

            bundles.Add(new ScriptBundle("~/Manage/FrontJs").Include(
                       "~/Scripts/BaseCv.js",
                       "~/Scripts/XMCATool.js",
                       "~/Scripts/Prompt.js",
                       "~/Scripts/knockout-2.2.1.js"));

            bundles.Add(new StyleBundle("~/Manage/BaseCss").Include(
                  "~/Content/bootstrap.css",
                 "~/Content/bootstrap-theme.css"
                ));

            bundles.Add(new StyleBundle("~/Manage/Skin").Include(
                   "~/Content/style/common.css",
                   "~/Content/style/xsht.css"
             ));

            bundles.Add(new StyleBundle("~/Template/Css").Include(
                "~/Content/style/common.css",
                "~/Content/style/qylb.css"
            ));


            BundleTable.EnableOptimizations = false;
        }
    }
}