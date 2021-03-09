using GenerationLogBook.Utility;
using GenerationLogBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GenerationLogBook.Controllers
{
    [GenLogBookAuthorization]
    [GenLogBookExceptionHandler]
    
    public class KPIController : Controller
    {
        private GenerationLogBookEntities _context;
        private string _userId, _authId;

        public KPIController()
        {
            _context = new GenerationLogBookEntities();
            _userId = (string)System.Web.HttpContext.Current.Session["userid"];
            _authId = (string)System.Web.HttpContext.Current.Session["ActiveAuthId"];
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: KPI

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_TARGET_EDIT)]
        public ActionResult OpenKPILedger()
        {
            KPIFormViewModel vmodel = new KPIFormViewModel(_context);
            return View("KPIForm", vmodel);
        }

        [HttpPost]
        public JsonResult FetchLedger(LossInputSet input)
        {
            KPIFormViewModel vmodel = new KPIFormViewModel();
            vmodel.Ledger = new List<KPIRegister>();

            string site = input.UnitId;
            DateTime date = DateTime.Parse("01/01/"+input.date);

            vmodel.Site = site;
            var res = _context.FetchKPIRegister(site, date).OrderBy( x => x.SortPrespective).ThenBy(x => x.SortKPI).ToList();

            foreach (var x in res)
            {
                vmodel.Ledger.Add(new KPIRegister
                {
                    theMonthStr = x.theMonth.ToString(),
                    KPIId = x.KPIId,
                    ActualResult = x.ActualResult,
                    AdjustedResult = x.AdjustedResult,
                    Average = x.Average,
                    Bad = x.Bad,
                    ChangeBy = x.ChangeBy,
                    ChangeDate = x.ChangeDate,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    CreateDateStr = x.CreateDate.ToString(),
                    Good =x.Good,
                    MaxScore = x.MaxScore,
                    PointAchieved = x.PointAchieved,
                    SiteId = x.SiteId,
                    KPIText = x.KPIText,
                    PrespectiveText = x.PrespectiveText,
                    UnitText = x.UnitText,
                    ctrack = x.RecSrc == "DB"?"N":"Y"
                });
            }
                
            return Json(vmodel);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_TARGET_EDIT)]
        public JsonResult PostKPIChanges(List<KPIRegister> changes)
        {
            KPIFormViewModel vmodel = new KPIFormViewModel();
            string SiteId;
            DateTime date;

            if (changes == null || changes.Count == 0)
            {
                vmodel.Status = new RequestStatus { CODE = "ERROR", TEXT = "There was Noting to Save to Database" };
                return Json(vmodel);
            }

            SiteId = changes[0].SiteId;
            date = DateTime.Parse(changes[0].theMonthStr);
            //SaveChanges

            foreach (var change in changes)
            {
                if (change.CreateDateStr == null || DateTime.Parse(change.CreateDateStr) < DateTime.Parse("1/1/2018"))
                {
                    change.theMonth = DateTime.Parse(change.theMonthStr);
                    change.CreateDate = DateTime.Now;
                    change.CreateBy = _userId;
                    change.ChangeDate = DateTime.Now;
                    change.ChangeBy = _userId;

                    _context.KPIRegisters.Add(change);
                    _context.SaveChanges();
                }
                else
                {
                    var r = _context.KPIRegisters.Single(x => x.SiteId == change.SiteId && x.theMonth == date && x.KPIId == change.KPIId );

                    r.ActualResult = change.ActualResult;
                    r.AdjustedResult = change.AdjustedResult;
                    r.Average = change.Average;
                    r.Bad = change.Bad;
                    r.Good = change.Good;
                    r.MaxScore = change.MaxScore;
                    r.PointAchieved = change.PointAchieved;

                    r.ChangeBy = _userId;
                    r.ChangeDate = DateTime.Now;

                    _context.SaveChanges();
                }
            }
            return FetchLedger(new LossInputSet {date = date.Year.ToString(), UnitId = SiteId });
        }
    }
}