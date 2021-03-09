using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenerationLogBook.Utility;
using GenerationLogBook.ViewModels;

namespace GenerationLogBook.Controllers
{
    [GenLogBookAuthorization]
    public class ELRReportsController : Controller
    {
        private GenerationLogBookEntities _context;

        public ELRReportsController()
        {
            _context = new GenerationLogBookEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult KPCForm()
        {
            ReportsFormViewModel vmodel = getReportFormViewModel(CommDef.SITE_KEY_KPC);
            return View("Index",vmodel);
        }
        public ActionResult SGTForm()
        {
            ReportsFormViewModel vmodel = getReportFormViewModel(CommDef.SITE_KEY_SGT);
            return View("Index", vmodel);
        }
        public ActionResult KGTForm()
        {
            ReportsFormViewModel vmodel = getReportFormViewModel(CommDef.SITE_KEY_KGT);
            return View("Index", vmodel);
        }


        //Added By Moiz 16-12-2019
        /// <summary>
        /// for BQPS Reports
        /// </summary>
        /// 
        /// <returns></returns>

        public ActionResult BQPS1Form()
        {
            ReportsFormViewModel vmodel = getReportFormViewModel(CommDef.SITE_KEY_BQ1);
            return View("Index", vmodel);
        }


        public ActionResult BQPS2Form()
        {
            ReportsFormViewModel vmodel = getReportFormViewModel(CommDef.SITE_KEY_BQ2);
            return View("Index", vmodel);
        }

        // End Of Moiz Addition
        public ActionResult ELRHOForm()
        {
            string userid = (string)System.Web.HttpContext.Current.Session["userid"];
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            vmodel.LOVSites = new List<KeyValue>();
            vmodel.LOVUnits = new List<KeyValue>();
   //         vmodel.LOVReports = new List<KeyValue>();


            var sites = from res in _context.SitesMasters
                        select new { res.SiteId, res.SiteName };

            var units = from res in _context.UnitsMasters
                        select new { res.SiteId, res.UnitId, res.UnitName };

            foreach (var x in sites)
                vmodel.LOVSites.Add(new KeyValue { Key = x.SiteId, Value = x.SiteName });

            foreach (var x in units)
                vmodel.LOVUnits.Add(new KeyValue { ParentKey = x.SiteId, Key = x.UnitId, Value = x.UnitName });

            vmodel.LOVReports = _context.ReportLists.Where(r => r.ReportCategory == "ELR" && r.StatusId == "A").OrderBy(r => r.ReportName).ToList();

            return View("Index",vmodel);
        }

        private ReportsFormViewModel getReportFormViewModel(string sitekey)
        {
            string userid = (string)System.Web.HttpContext.Current.Session["userid"];
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            vmodel.LOVSites = new List<KeyValue>();
            vmodel.LOVUnits = new List<KeyValue>();

            var sites = from res in _context.SitesMasters
                        where res.SiteId == sitekey
                        select new { res.SiteId, res.SiteName };

            var units = from res in _context.UnitsMasters
                        where res.SiteId == sitekey
                        select new { res.SiteId, res.UnitId, res.UnitName };

            foreach (var x in sites)
                vmodel.LOVSites.Add(new KeyValue { Key = x.SiteId, Value = x.SiteName });

            foreach (var x in units)
                vmodel.LOVUnits.Add(new KeyValue { ParentKey = x.SiteId, Key = x.UnitId, Value = x.UnitName });

            vmodel.LOVReports = _context.ReportLists.Where(r => r.ReportCategory == "ELR" && r.StatusId == "A").OrderBy(r => r.ReportName).ToList();

            return vmodel;
        }

    }
}