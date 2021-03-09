using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenerationLogBook.Utility;
using GenerationLogBook.ViewModels;
using System.Data.Entity;

namespace GenerationLogBook.Controllers
{
    [GenLogBookAuthorization]
    [GenLogBookExceptionHandler]
    
    public class KPCController : Controller
    {
        private GenerationLogBookEntities _context;
        private string _userId, _authId;
        public KPCController()
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
 
        #region Master Data Management
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_MASTER_DATA_MGMT_KPC)]
        public ActionResult MasterDataListForm()
        {
            IEnumerable<UnitsMaster> model = _context.UnitsMasters.Where( u => u.SiteId == CommDef.SITE_KEY_KPC && u.StatusId == "A") ;
            return View("_MasterDataList", model);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_MASTER_DATA_MGMT_KPC)]
        public ActionResult ModifyMasterDataForm(string id)
        {
            UnitsMaster model = _context.UnitsMasters.Single(u => u.UnitId == id);
            return View("_ModifyMasterDataForm", model);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_MASTER_DATA_MGMT_KPC)]
        public ActionResult PostMasterDataChanges(UnitsMaster model)
        {
            var dbentry = _context.UnitsMasters.Single(u => u.UnitId == model.UnitId);
            dbentry.GDC = model.GDC;
            dbentry.ReferenceCapacity = model.ReferenceCapacity;
            dbentry.ChangeBy= _userId;
            dbentry.ChangeDate = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("MasterDataListForm");
        }

        #endregion Master Data Management

        #region Temperature Sheet Handling
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_TEMP_KPC_F1)]
        public ActionResult OpenTemperatureForm()
        {
            TemperatureFormViewModel temperaturevm = new TemperatureFormViewModel();
            temperaturevm.Controller = "KPC";
            temperaturevm.hiddenCols = new List<string>();
            temperaturevm.Ledger = new List<TemperatureRegister>();

            return View("_TemperatureForm", temperaturevm);   
        }

        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_TEMP_KPC_F1)]
        public JsonResult FetchTemperatureLedger(List<LossInputSet> input)
        {
            DateTime date = DateTime.Parse(input[0].date);
            TemperatureHandler temphandler = new TemperatureHandler(_context, _userId, _authId);
            TemperatureFormViewModel vmodel = temphandler.FetchTemperatureLedger(date, CommDef.SITE_KEY_KPC);
            vmodel.Controller = "KPC";
            vmodel.hiddenCols = new List<string>();

            return Json(vmodel);
        }

        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_TEMP_KPC_F1)]
        public JsonResult PostTemperatureSheet(List<TemperatureRegister> changes)
        {
            TemperatureHandler temphandler = new TemperatureHandler(_context, _userId, _authId);
            TemperatureFormViewModel vmodel = temphandler.PostTemperatureSheet(changes);
            vmodel.Controller = "KPC";
            vmodel.hiddenCols = new List<string>();

            return Json(vmodel);
        }

        #endregion Temperature Sheet Handling

        #region Safety Sheet Handling
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_SAFETY_KPC_F1)]
        public ActionResult OpenSafetyForm()
        {
            SafetyFormViewModel vmodel = new SafetyFormViewModel("KPC", CommDef.SITE_KEY_KPC );
            return View("_SafetyForm", vmodel);
        }

        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_SAFETY_KPC_F1)]
        public JsonResult FetchSafetyLedger(List<LossInputSet> input)
        {
            DateTime date = DateTime.Parse(input[0].date);

            SafetyFormViewModel vmodel = new SafetyFormViewModel(_context, _userId, _authId, CommDef.SITE_KEY_KPC, date, "KPC");
            vmodel.FetchSafetyLedger();

            if (vmodel.Status.CODE == "OK")
                vmodel.FetchWorkHourLedger();

            vmodel.hiddenCols = new List<string>();
            return Json(vmodel);
        }
        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_SAFETY_KPC_F1)]
        public JsonResult PostSafetySheet(List<SiteSafetyRegister> changes)
        {
            DateTime date = DateTime.Parse(changes[0].theDateStr);

            SafetyFormViewModel vmodel = new SafetyFormViewModel(_context, _userId, _authId, CommDef.SITE_KEY_KPC, date, "KPC");
            vmodel.PostSafetySheet(changes);
            vmodel.hiddenCols = new List<string>();
            return Json(vmodel);
        }
        
        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_SAFETY_KPC_F1)]
        public JsonResult PostWorkHourData(MonthlyWorkHoursRegister change)
        {
            DateTime date = DateTime.Parse(change.theMonthStr);

            SafetyFormViewModel vmodel = new SafetyFormViewModel(_context, _userId, _authId, CommDef.SITE_KEY_KPC, date, "KPC");
            vmodel.PostWorkHourLedger(change);
            vmodel.hiddenCols = new List<string>();
            return Json(vmodel);
        }
        #endregion
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_ENERGYLOSS_KPC_PWRAPPROVAL)]
        public ActionResult ElossL3ApprovalForm()
        {
            string AuthId = CommDef.AUTH_ENERGYLOSS_KPC_PWRAPPROVAL;// "ELOSKPCL2";
            KPCELossFormViewModel vmodel;

            System.Web.HttpContext.Current.Session["ActiveAuthId"] = AuthId;
            _authId = AuthId;

            vmodel = buildElossFormViewModel(AuthId);

            vmodel.hiddenCols = new List<string> { };

            return View("ElossDataEntryForm", vmodel);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_ENERGYLOSS_KPC_PERAPPROVAL)]
        public ActionResult ElossL2ApprovalForm()
        {
            string AuthId = CommDef.AUTH_ENERGYLOSS_KPC_PERAPPROVAL;// "ELOSKPCL2";
            KPCELossFormViewModel vmodel;

            System.Web.HttpContext.Current.Session["ActiveAuthId"] = AuthId;
            _authId = AuthId;

            vmodel = buildElossFormViewModel(AuthId);

            vmodel.hiddenCols = new List<string> {  };

            return View("ElossDataEntryForm", vmodel);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_ENERGYLOSS_KPC_OPRAPPROVAL)]
        public ActionResult ElossL1ApprovalForm()
        {
            string AuthId = CommDef.AUTH_ENERGYLOSS_KPC_OPRAPPROVAL;// "ELOSKPCL1";
            KPCELossFormViewModel vmodel;

            System.Web.HttpContext.Current.Session["ActiveAuthId"] = AuthId;
            _authId = AuthId;

            vmodel = buildElossFormViewModel(AuthId);

            vmodel.hiddenCols = new List<string> { "L2Approvalbool", "AmbDeration", "ForcedDeration", "PlannedDeration" };

            return View("ElossDataEntryForm", vmodel);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_ENERGYLOSS_KPC_DATAENTRY)]
        public ActionResult ElossDataEntryForm()
        {
            string AuthId = CommDef.AUTH_ENERGYLOSS_KPC_DATAENTRY;// "ELOSKPCDE";
            KPCELossFormViewModel vmodel;

            System.Web.HttpContext.Current.Session["ActiveAuthId"] = AuthId;
            _authId = AuthId;

            vmodel = buildElossFormViewModel(AuthId);

            vmodel.hiddenCols = new List<string> { "L1Approvalbool", "L2Approvalbool", "AmbDeration", "ForcedDeration", "PlannedDeration" };

            return View(vmodel);
        }

        private KPCELossFormViewModel buildElossFormViewModel(string AuthId)
        {
            KPCELossFormViewModel vmodel = new KPCELossFormViewModel();

            vmodel.UserId = _userId;
            vmodel.AuthId = AuthId;

            vmodel.LOVUnits = (from res in _context.UnitsMasters
                               where res.SiteId == CommDef.SITE_KEY_KPC
                               select new KeyValue { ParentKey = res.SiteId, Key = res.UnitId, Value = res.UnitName }).ToList();

            vmodel.LOVForcedGrid = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "F", Value = "Forced" }, new KeyValue { Key = "G", Value = "Forced Grid" } };
            vmodel.LOVIsGas = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "Y", Value = "Gas" } };
            vmodel.LOVIsLDC = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "Y", Value = "LDC" } };
            vmodel.LOVIsPlanned = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "Y", Value = "Planned" } };
            vmodel.LOVStartupRamp = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "F", Value = "Forced" }, new KeyValue { Key = "P", Value = "Planned" }, new KeyValue { Key = "L", Value = "LDC" }, new KeyValue { Key = "G", Value = "Gas" } };
            vmodel.LOVOutageDeration = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "O", Value = "Outage" }, new KeyValue { Key = "D", Value = "Deration" } };


            //vmodel.LOVReason = this.getPreDefinedRasons("", "");
            //vmodel.LOVReason = new Utility.LovManager().getPreDefinedRasons("", "");
            vmodel.LOVReason = new Utility.LovManager().getPreDefinedRasonsBySiteID(CommDef.SITE_KEY_KPC);


            vmodel.Ledger = new List<UnitReadingRegister1>();

            return vmodel;
        }

        [HttpPost]
        public JsonResult FetchLossLedger(List<LossInputSet> input)
        {
            KPCELossFormViewModel vmodel = new KPCELossFormViewModel();
            string UnitId = input[0].UnitId;
            string UnitName;
            DateTime date = DateTime.Parse(input[0].date);
            
            vmodel.Ledger = getLedger(UnitId,date);
            UnitName = _context.UnitsMasters.Single(x => x.UnitId == UnitId).UnitName;
            vmodel.FormHeading = "KPC-Energy Loss Recording Register [" + UnitName +", " + date.ToString("dd-MM-yyyy")+ "]";
            vmodel.Status = new RequestStatus { CODE = "OK", TEXT = "OK" };

            return Json(vmodel);
        }

        [HttpPost]
        public JsonResult PostChanges(List<UnitReadingRegister1> changes)
        {
            KPCELossFormViewModel vmodel = new KPCELossFormViewModel();
            string UnitId;
            DateTime date;

            if (changes == null ||changes.Count == 0)
            {
                vmodel.Status = new RequestStatus{CODE="ERROR", TEXT="There was Noting to Save to Database" };
            }
            else
            {
                UnitId = changes[0].UnitId;
                date = DateTime.Parse(changes[0].RdgDateTimeStr);
                SaveChanges(changes);
                vmodel.Ledger = getLedger(UnitId, date);
                vmodel.Status = new RequestStatus { CODE = "OK", TEXT = "OK" };
            }

            return Json(vmodel);
        }

        private List<UnitReadingRegister1> getLedger(string pUnitId, DateTime pdate)
        {
            List<UnitReadingRegister1> ledger = new List<UnitReadingRegister1>();
            IEnumerable<FetchRegister1_Result> res;


            switch (_authId)
            {
                case CommDef.AUTH_ENERGYLOSS_KPC_DATAENTRY:
                case CommDef.AUTH_ENERGYLOSS_KPC_PWRAPPROVAL:
                    res = _context.FetchRegister1(pUnitId, pdate).OrderBy(z => z.RdgDateTime);
                    break;
                case CommDef.AUTH_ENERGYLOSS_KPC_OPRAPPROVAL:
                    res = _context.FetchRegister1(pUnitId, pdate).Where(z => z.RecSrc == "DB").OrderBy(z => z.RdgDateTime);
                    break;
                case CommDef.AUTH_ENERGYLOSS_KPC_PERAPPROVAL:
                    res = _context.FetchRegister1(pUnitId, pdate).Where(z => z.RecSrc == "DB" /*&& z.L1Approval == "Y"*/).OrderBy(z => z.RdgDateTime);
                    break;
                default:
                    res = _context.FetchRegister1(pUnitId, pdate).OrderBy(z => z.RdgDateTime);
                    break;
            }

            foreach (var x in res)
            {
                ledger.Add(new UnitReadingRegister1
                {
                    ActualDispatchMW = x.ActualDispatchMW,
                    //AmbDeration = x.AmbDeration,
                    //AmbDeration = x.RecSrc =="DB"? (x.L2Approval == "Y" ? x.AmbDeration : (Math.Abs(((((decimal)9 * (decimal)x.AmbTemp) / (decimal)5) + (decimal)32) - (decimal)86) * (decimal)0.2) + ((Math.Abs((decimal)x.SeaWaterTemp - (decimal)25) * (decimal)0.1) * 0)) : 0,
                    AmbDeration = x.RecSrc == "DB" ? (x.L2Approval == "Y" ? x.AmbDeration : (Math.Max(0, ((((((decimal)9 * (decimal)x.AmbTemp) / (decimal)5) + (decimal)32) - (decimal)86) * (decimal)0.2)))) : x.AmbDeration,
                    AmbPressure = x.AmbPressure,
                    AmbTemp = x.AmbTemp,
                    ChangeBy = x.ChangeBy,
                    ChangeDate = x.ChangeDate,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    CreateDateStr = x.CreateDate.ToString(),
                    ForcedDeration = x.ForcedDeration,
                    GDC = x.GDC,
                    IsForced = x.IsForced,
                    IsGas = x.IsGas,
                    IsLDC = x.IsLDC,
                    IsOutageDeration = x.IsOutageDeration,
                    IsPlanned = x.IsPlanned,
                    //L1Approval = x.L1Approval,
                    L1Approvalbool = x.L1Approval == "Y" ? true : false,
                    L1ApprovalCopy = x.L1Approval == "Y" ? true : false,
                    L1ApproveBy = x.L1ApproveBy,
                    L1ApproveDate = x.L1ApproveDate,
                    //L2Approval = x.L2Approval,
                    L2Approvalbool = x.L2Approval == "Y" ? true : false,
                    L2ApprovalCopy = x.L2Approval == "Y" ? true : false,
                    L2ApproveBy = x.L2ApproveBy,
                    L2ApproveDate = x.L2ApproveDate,
                    PlannedDeration = x.PlannedDeration,
                    RdgDateTime = x.RdgDateTime ?? new DateTime(1900, 1, 1),
                    RdgDateTimeStr = x.RdgDateTime.ToString(),
                    Reason = x.Reason,
                    ReferenceCapacity = x.ReferenceCapacity,
                    SeaWaterTemp = x.SeaWaterTemp,
                    SiteId = x.SiteID,
                    StartupRampId = x.StartupRampId,
                    UnitId = x.UnitId,
                    ctrack = x.RecSrc == "DB" ? "N" : "Y"
                });
            }
            return ledger;
        }
        private void SaveChanges(List<UnitReadingRegister1> changes)
        {

            foreach(var c in changes)
            {
                
                if (c.CreateDateStr == null || DateTime.Parse(c.CreateDateStr) < DateTime.Parse("1/1/2018"))
                {
                    AddRecord(c);
                }
                else
                {
                    EditRecord(c);
                }
            }
        }
        private void AddRecord(UnitReadingRegister1 rec)
        {
            rec.RdgDateTime = DateTime.Parse(rec.RdgDateTimeStr);
            rec.CreateDate = DateTime.Now;
            rec.CreateBy = _userId;
            rec.ChangeDate = DateTime.Now;
            rec.ChangeBy= _userId;
            rec.CreateByAuthId = _authId;

            if (rec.L1Approvalbool)
            {
                rec.L1Approval = "Y";
                rec.L1ApproveBy = _userId;
                rec.L1ApproveAuthId = _authId;
                rec.L1ApproveDate = DateTime.Now;
            }
            else
            {
                rec.L1Approval = "N";
                rec.L1ApproveBy = "";
                rec.L1ApproveAuthId = "";
                rec.L1ApproveDate = null;
            }
                
            if (rec.L2Approvalbool)
            {
                rec.L2Approval = "Y";
                rec.L2ApproveBy = _userId;
                rec.L2ApproveAuthId = _authId;
                rec.L2ApproveDate = DateTime.Now;
            }
            else
            {
                rec.L2Approval = "N";
                rec.L2ApproveBy = "";
                rec.L2ApproveAuthId = "";
                rec.L2ApproveDate = null;
            }

            rec.IsForced = (rec.IsForced == null) ? "N" : rec.IsForced;
            rec.IsGas = (rec.IsGas == null) ? "N" : rec.IsGas;
            rec.IsLDC = (rec.IsLDC == null) ? "N" : rec.IsLDC;
            rec.IsOutageDeration = (rec.IsOutageDeration == null) ? "N" : rec.IsOutageDeration;
            rec.IsPlanned = (rec.IsPlanned == null) ? "N" : rec.IsPlanned;
            rec.StartupRampId = (rec.StartupRampId == null) ? "N" : rec.StartupRampId;

            _context.UnitReadingRegister1.Add(rec);
            _context.SaveChanges();
        }
        private void EditRecord(UnitReadingRegister1 rec)
        {
            DateTime date;
            string UnitId;

            date = DateTime.Parse(rec.RdgDateTimeStr);
            UnitId = rec.UnitId;

            var r = _context.UnitReadingRegister1.Single(x => x.UnitId == UnitId && x.RdgDateTime == date);


            if (r.L1Approval == "Y" && rec.L1Approvalbool==false)
            {
                r.L1ApproveBy = "";
                r.L1ApproveAuthId = "";
                r.L1ApproveDate = null;
            }

            if (r.L1Approval == "N" && rec.L1Approvalbool == true)
            {
                r.L1ApproveBy = _userId;
                r.L1ApproveAuthId = _authId;
                r.L1ApproveDate = DateTime.Now;
            }

            if (r.L2Approval == "Y" && rec.L2Approvalbool == false)
            {
                r.L2ApproveBy = "";
                r.L2ApproveAuthId = "";
                r.L2ApproveDate = null;
            }

            if (r.L2Approval == "N" && rec.L2Approvalbool == true)
            {
                r.L2ApproveBy = _userId;
                r.L2ApproveAuthId = _authId;
                r.L2ApproveDate = DateTime.Now;
            }

            r.ActualDispatchMW = rec.ActualDispatchMW;
            r.AmbDeration = rec.AmbDeration;
            r.AmbPressure = rec.AmbPressure;
            r.AmbTemp = rec.AmbTemp;
            r.ChangeBy = _userId;
            r.ForcedDeration = rec.ForcedDeration;
            r.IsForced = (rec.IsForced == null) ? "N" : rec.IsForced;
            r.IsGas = (rec.IsGas == null) ? "N" : rec.IsGas;
            r.IsLDC = (rec.IsLDC == null) ? "N" : rec.IsLDC;
            r.IsOutageDeration = (rec.IsOutageDeration == null) ? "N" : rec.IsOutageDeration;
            r.IsPlanned = (rec.IsPlanned == null) ? "N" : rec.IsPlanned;
            r.StartupRampId = (rec.StartupRampId == null) ? "N" : rec.StartupRampId;
            r.L1Approval = rec.L1Approvalbool == true?"Y":"N";
            r.L2Approval = rec.L2Approvalbool == true?"Y":"N";
            r.PlannedDeration = rec.PlannedDeration;
            r.Reason = rec.Reason;
            r.SeaWaterTemp = rec.SeaWaterTemp;
             
            r.ChangeDate = DateTime.Now;
        
            _context.SaveChanges();
        }






    }

}