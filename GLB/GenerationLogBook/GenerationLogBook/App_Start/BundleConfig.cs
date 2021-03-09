using System.Web;
using System.Web.Optimization;

namespace GenerationLogBook
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
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-datepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqwidgets").Include(
                            "~/Scripts/jqwidgets/jqxcore.js",
                            "~/Scripts/jqwidgets/jqxdata.js",
                            "~/Scripts/jqwidgets/jqxbuttons.js",
                            "~/Scripts/jqwidgets/jqxscrollbar.js",
                            "~/Scripts/jqwidgets/jqxmenu.js",
                            "~/Scripts/jqwidgets/jqxgrid.js",
                            "~/Scripts/jqwidgets/jqxgrid.selection.js",
                            "~/Scripts/jqwidgets/jqxgrid.edit.js",
                            "~/Scripts/jqwidgets/jqxgrid.sort.js",
                            "~/Scripts/jqwidgets/jqxgrid.columnsresize.js",
                            "~/Scripts/jqwidgets/jqxlistbox.js",
                            "~/Scripts/jqwidgets/jqxdropdownlist.js",
                            //"~/Scripts/jqwidgets/jqxdatetimeinput.js",
                            //"~/Scripts/jqwidgets/jqxcalendar.js",
                            "~/Scripts/jqwidgets/jqxgrid.filter.js",
                            "~/Scripts/jqwidgets/jqxlistbox.js",
                            //"~/Scripts/jqwidgets/jqxcombobox.js",
                            //"~/Scripts/jqwidgets/jqxgrid.pager.js",
                            //"~/Scripts/jqwidgets/jqxdata.export.js",
                            //"~/Scripts/jqwidgets/jqxgrid.export.js",
                            //"~/Scripts/jqwidgets/jqxwindow.js",
                            "~/Scripts/jqwidgets/jqxinput.js",
                            "~/Scripts/jqwidgets/jqxvalidator.js",
                            //"~/Scripts/jqwidgets/jqxloader.js",
                            "~/Scripts/jqwidgets/globalization/globalize.js",
                            "~/Scripts/jqwidgets/jqxgrid.aggregates.js",
                            "~/Scripts/jqwidgets/jqxcheckbox.js",
                            "~/Scripts/jqwidgets/jqxnumberinput.js",
                            "~/Scripts/jqwidgets/jqxgrid.grouping.js",

                            "~/Scripts/jqwidgets/jqxgrid.pager.js"

                ));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                            "~/Scripts/DataTables/datatables.js",
                            "~/Scripts/DataTables/dataTables.buttons.min.js",
                            "~/Scripts/DataTables/dataTables.fixedColumns.min.js",
                            "~/Scripts/DataTables/buttons.flash.min.js",
                            "~/Scripts/DataTables/buttons.print.min.js",
                            "~/Scripts/DataTables/jszip.min.js",
                            //"~/Scripts/DataTables/pdfmake.min.js",
                            "~/Scripts/DataTables/vfs_fonts.js",
                            "~/Scripts/DataTables/buttons.html5.min.js"));//

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-datepicker3.min.css",
                      "~/Content/datatables.css",
                      "~/Content/fixedColumns.dataTables.min.css",
                      "~/Content/buttons.dataTables.min.css"));

            bundles.Add(new StyleBundle("~/Content/jqwidgetscss").Include(
                   "~/Content/jqwidgets/jqx.base.css",
                   "~/Content/jqwidgets/jqx.energyblue.css",
                   "~/Content/jqwidgets/jqx.metro.css",
                   "~/Content/jqwidgets/jqx.office.css",
                   "~/Content/jqwidgets/jqx.bootstrap.css"
               ));
        }
    }
}
