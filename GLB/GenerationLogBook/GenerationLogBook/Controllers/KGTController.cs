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
    public class KGTController : Controller
    {
        private GenerationLogBookEntities _context;
        private string _userId, _authId;
        public KGTController()
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

        #region Temperature Sheet Handling
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_TEMP_KGT_F1)]
        public ActionResult OpenTemperatureForm()
        {
            TemperatureFormViewModel temperaturevm = new TemperatureFormViewModel();
            temperaturevm.Controller = "KGT";
            temperaturevm.hiddenCols = new List<string>();
            temperaturevm.Ledger = new List<TemperatureRegister>();

            return View("_TemperatureForm", temperaturevm);
        }

        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_TEMP_KGT_F1)]
        public JsonResult FetchTemperatureLedger(List<LossInputSet> input)
        {
            DateTime date = DateTime.Parse(input[0].date);
            TemperatureHandler temphandler = new TemperatureHandler(_context, _userId, _authId);
            TemperatureFormViewModel vmodel = temphandler.FetchTemperatureLedger(date, CommDef.SITE_KEY_KGT);
            vmodel.Controller = "KGT";
            vmodel.hiddenCols = new List<string>();

            return Json(vmodel);
        }

        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_TEMP_KGT_F1)]
        public JsonResult PostTemperatureSheet(List<TemperatureRegister> changes)
        {
            TemperatureHandler temphandler = new TemperatureHandler(_context, _userId, _authId);
            TemperatureFormViewModel vmodel = temphandler.PostTemperatureSheet(changes);
            vmodel.Controller = "KGT";
            vmodel.hiddenCols = new List<string>();

            return Json(vmodel);
        }

        #endregion Temperature Sheet Handling

        #region Safety Sheet Handling
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_SAFETY_KGT_F1)]
        public ActionResult OpenSafetyForm()
        {
            SafetyFormViewModel vmodel = new SafetyFormViewModel("KGT", CommDef.SITE_KEY_KGT);
            return View("_SafetyForm", vmodel);
        }

        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_SAFETY_KGT_F1)]
        public JsonResult FetchSafetyLedger(List<LossInputSet> input)
        {
            DateTime date = DateTime.Parse(input[0].date);

            SafetyFormViewModel vmodel = new SafetyFormViewModel(_context, _userId, _authId, CommDef.SITE_KEY_KGT, date, "KGT");
            vmodel.FetchSafetyLedger();

            if (vmodel.Status.CODE == "OK")
                vmodel.FetchWorkHourLedger();

            vmodel.hiddenCols = new List<string>();
            return Json(vmodel);
        }
        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_SAFETY_KGT_F1)]
        public JsonResult PostSafetySheet(List<SiteSafetyRegister> changes)
        {
            DateTime date = DateTime.Parse(changes[0].theDateStr);

            SafetyFormViewModel vmodel = new SafetyFormViewModel(_context, _userId, _authId, CommDef.SITE_KEY_KGT, date, "KGT");
            vmodel.PostSafetySheet(changes);
            vmodel.hiddenCols = new List<string>();
            return Json(vmodel);
        }

        [HttpPost]
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_SAFETY_KGT_F1)]
        public JsonResult PostWorkHourData(MonthlyWorkHoursRegister change)
        {
            DateTime date = DateTime.Parse(change.theMonthStr);

            SafetyFormViewModel vmodel = new SafetyFormViewModel(_context, _userId, _authId, CommDef.SITE_KEY_KGT, date, "KGT");
            vmodel.PostWorkHourLedger(change);
            vmodel.hiddenCols = new List<string>();
            return Json(vmodel);
        }
        #endregion

        #region Master Data Management
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_MASTER_DATA_MGMT_KGT)]
        public ActionResult MasterDataListForm()
        {
            IEnumerable<UnitsMaster> model = _context.UnitsMasters.Where(u => u.SiteId == CommDef.SITE_KEY_KGT && u.StatusId == "A");
            return View("_MasterDataList", model);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_MASTER_DATA_MGMT_KGT)]
        public ActionResult ModifyMasterDataForm(string id)
        {
            UnitsMaster model = _context.UnitsMasters.Single(u => u.UnitId == id);
            return View("_ModifyMasterDataForm", model);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_MASTER_DATA_MGMT_KGT)]
        public ActionResult PostMasterDataChanges(UnitsMaster model)
        {
            var dbentry = _context.UnitsMasters.Single(u => u.UnitId == model.UnitId);
            dbentry.GDC = model.GDC;
            dbentry.ReferenceCapacity = model.ReferenceCapacity;
            dbentry.ChangeBy = _userId;
            dbentry.ChangeDate = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("MasterDataListForm");
        }
        #endregion

        #region Energy Loss Handling
        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_ENERGYLOSS_KGT_PWRAPPROVAL)]
        public ActionResult ElossL3ApprovalForm()
        {
            string AuthId = CommDef.AUTH_ENERGYLOSS_KGT_PWRAPPROVAL;
            KGTELossFormViewModel vmodel;

            System.Web.HttpContext.Current.Session["ActiveAuthId"] = AuthId;
            _authId = AuthId;

            vmodel = buildElossFormViewModel(AuthId);

            vmodel.hiddenCols = new List<string> { };

            return View("ElossDataEntryForm", vmodel);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_ENERGYLOSS_KGT_PERAPPROVAL)]
        public ActionResult ElossL2ApprovalForm()
        {
            string AuthId = CommDef.AUTH_ENERGYLOSS_KGT_PERAPPROVAL;
            KGTELossFormViewModel vmodel;

            System.Web.HttpContext.Current.Session["ActiveAuthId"] = AuthId;
            _authId = AuthId;

            vmodel = buildElossFormViewModel(AuthId);

            vmodel.hiddenCols = new List<string> { };

            return View("ElossDataEntryForm", vmodel);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_ENERGYLOSS_KGT_OPRAPPROVAL)]
        public ActionResult ElossL1ApprovalForm()
        {
            string AuthId = CommDef.AUTH_ENERGYLOSS_KGT_OPRAPPROVAL;
            KGTELossFormViewModel vmodel;

            System.Web.HttpContext.Current.Session["ActiveAuthId"] = AuthId;
            _authId = AuthId;

            vmodel = buildElossFormViewModel(AuthId);

            vmodel.hiddenCols = new List<string> { "L2Approvalbool", "AmbDeration"/*, "ForcedDeration", "PlannedDeration"*/ };

            return View("ElossDataEntryForm", vmodel);
        }

        [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_ENERGYLOSS_KGT_DATAENTRY)]
        public ActionResult ElossDataEntryForm()
        {
            string AuthId = CommDef.AUTH_ENERGYLOSS_KGT_DATAENTRY;
            KGTELossFormViewModel vmodel;

            System.Web.HttpContext.Current.Session["ActiveAuthId"] = AuthId;
            _authId = AuthId;

            vmodel = buildElossFormViewModel(AuthId);

            vmodel.hiddenCols = new List<string> { "L1Approvalbool", "L2Approvalbool", "AmbDeration"/*, "ForcedDeration", "PlannedDeration"*/ };

            return View(vmodel);
        }

        private KGTELossFormViewModel buildElossFormViewModel(string AuthId)
        {
            KGTELossFormViewModel vmodel = new KGTELossFormViewModel();

            vmodel.UserId = _userId;
            vmodel.AuthId = AuthId;

            vmodel.LOVUnits = (from res in _context.UnitsMasters
                               where res.SiteId == CommDef.SITE_KEY_KGT
                               select new KeyValue { ParentKey = res.SiteId, Key = res.UnitId, Value = res.UnitName }).ToList();

            vmodel.LOVOutage = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "P", Value = "Planned" }, new KeyValue { Key = "F", Value = "Forced" }, new KeyValue { Key = "D", Value = "Forced Grid" }, new KeyValue { Key = "G", Value = "Gas" } };

            vmodel.LOVForcedGrid = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "F", Value = "Forced" }, new KeyValue { Key = "G", Value = "Forced Grid" } };
            vmodel.LOVIsGas = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "Y", Value = "Gas" } };
            vmodel.LOVIsPlanned = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "Y", Value = "Planned" } };
            vmodel.LOVStartupRamp = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "F", Value = "Forced" }, new KeyValue { Key = "P", Value = "Planned" }, new KeyValue { Key = "L", Value = "LDC" }, new KeyValue { Key = "G", Value = "Gas" } };
            vmodel.LOVOutageDeration = new List<KeyValue> { new KeyValue { Key = "N", Value = " " }, new KeyValue { Key = "O", Value = "Outage" }, new KeyValue { Key = "D", Value = "Deration" } };


            //vmodel.LOVReason = new Utility.LovManager().getPreDefinedRasons("", "");
           vmodel.LOVReason = new Utility.LovManager().getPreDefinedRasonsBySiteID(CommDef.SITE_KEY_KGT);


            vmodel.Ledger2 = new List<UnitReadingRegister2>();
            return vmodel;
        }

        [HttpPost]
        public JsonResult FetchLossLedger1(List<LossInputSet> input)
        {
            KGTELossFormViewModel vmodel = new KGTELossFormViewModel();
            string UnitId = input[0].UnitId;
            string UnitName;
            DateTime date = DateTime.Parse(input[0].date);

            vmodel.Ledger1 = getLedger1(UnitId, date);
            UnitName = _context.UnitsMasters.Single(x => x.UnitId == UnitId).UnitName;
            vmodel.FormHeading = "KGTPS-Energy Loss Recording Register [" + UnitName + ", " + date.ToString("dd-MM-yyyy") + "]";
            vmodel.SelectedUnitId = UnitId;
            vmodel.Status = new RequestStatus { CODE = "OK", TEXT = "OK" };

            return Json(vmodel);
        }

        private List<UnitReadingRegister1> getLedger1(string pUnitId, DateTime pdate)
        {
            List<UnitReadingRegister1> ledger = new List<UnitReadingRegister1>();
            IEnumerable<FetchRegister1_Result> res;


            switch (_authId)
            {
                case CommDef.AUTH_ENERGYLOSS_KGT_DATAENTRY:
                case CommDef.AUTH_ENERGYLOSS_KGT_PWRAPPROVAL:
                    res = _context.FetchRegister1(pUnitId, pdate).OrderBy(z => z.RdgDateTime);
                    break;
                case CommDef.AUTH_ENERGYLOSS_KGT_OPRAPPROVAL:
                    res = _context.FetchRegister1(pUnitId, pdate).Where(z => z.RecSrc == "DB").OrderBy(z => z.RdgDateTime);
                    break;
                case CommDef.AUTH_ENERGYLOSS_KGT_PERAPPROVAL:
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
                    SiteId = x.SiteID,
                    UnitId = x.UnitId,
                    GDC = x.GDC,
                    ReferenceCapacity = x.ReferenceCapacity,
                    ActualDispatchMW = x.RecSrc == "DB" ? x.ActualDispatchMW : (decimal)8.5,
                    ctrack = x.RecSrc == "DB" ? "N" : "Y",
                    AmbDeration = x.AmbDeration,
                    ForcedDeration = x.ForcedDeration,
                    PlannedDeration = x.PlannedDeration,
                    AmbTemp = x.AmbTemp,
                    IsOutageDeration = x.IsOutageDeration,
                    IsForced = x.IsForced,
                    IsPlanned = x.IsPlanned,
                    IsGas = x.IsGas,
                    StartupRampId = x.StartupRampId,
                    //IsLDC = x.IsLDC,
                    //SeaWaterTemp = x.SeaWaterTemp,
                    //AmbPressure = x.AmbPressure,

                    L1Approvalbool = x.L1Approval == "Y" ? true : false,
                    L1ApprovalCopy = x.L1Approval == "Y" ? true : false,
                    L1ApproveBy = x.L1ApproveBy,
                    L1ApproveDate = x.L1ApproveDate,

                    L2Approvalbool = x.L2Approval == "Y" ? true : false,
                    L2ApprovalCopy = x.L2Approval == "Y" ? true : false,
                    L2ApproveBy = x.L2ApproveBy,
                    L2ApproveDate = x.L2ApproveDate,
                    RdgDateTime = x.RdgDateTime ?? new DateTime(1900, 1, 1),
                    RdgDateTimeStr = x.RdgDateTime.ToString(),
                    Reason = x.Reason,

                    ChangeBy = x.ChangeBy,
                    ChangeDate = x.ChangeDate,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    CreateDateStr = x.CreateDate.ToString()
                });
            }
            return ledger;
        }

        [HttpPost]
        public JsonResult PostChanges1(List<UnitReadingRegister1> changes)
        {
            KGTELossFormViewModel vmodel = new KGTELossFormViewModel();
            string UnitId;
            DateTime date;

            if (changes == null || changes.Count == 0)
            {
                vmodel.Status = new RequestStatus { CODE = "ERROR", TEXT = "There was Noting to Save to Database" };
            }
            else
            {
                UnitId = changes[0].UnitId;
                date = DateTime.Parse(changes[0].RdgDateTimeStr);
                SaveChanges1(changes);
                vmodel.Ledger1 = getLedger1(UnitId, date);
                vmodel.SelectedUnitId = UnitId;
                vmodel.Status = new RequestStatus { CODE = "OK", TEXT = "OK" };
            }

            return Json(vmodel);
        }
        private void SaveChanges1(List<UnitReadingRegister1> changes)
        {

            foreach (var c in changes)
            {

                if (c.CreateDateStr == null || DateTime.Parse(c.CreateDateStr) < DateTime.Parse("1/1/2018"))
                {
                    AddRecord1(c);
                }
                else
                {
                    EditRecord1(c);
                }
            }
        }

        private void AddRecord1(UnitReadingRegister1 rec)
        {
            rec.RdgDateTime = DateTime.Parse(rec.RdgDateTimeStr);
            rec.CreateDate = DateTime.Now;
            rec.CreateBy = _userId;
            rec.ChangeDate = DateTime.Now;
            rec.ChangeBy = _userId;
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

            rec.IsForced = rec.IsForced == null ? "N" : rec.IsForced;
            rec.IsGas = rec.IsGas == null ? "N" : rec.IsGas;
            rec.IsOutageDeration = rec.IsOutageDeration == null ? "N" : rec.IsOutageDeration;
            rec.IsPlanned = rec.IsPlanned == null ? "N" : rec.IsPlanned;
            rec.StartupRampId = rec.StartupRampId == null ? "N" : rec.StartupRampId;
            rec.IsLDC = "XX";

            _context.UnitReadingRegister1.Add(rec);
            _context.SaveChanges();
        }

        private void EditRecord1(UnitReadingRegister1 rec)
        {
            DateTime date;
            string UnitId;

            date = DateTime.Parse(rec.RdgDateTimeStr);
            UnitId = rec.UnitId;

            var r = _context.UnitReadingRegister1.Single(x => x.UnitId == UnitId && x.RdgDateTime == date);


            if (r.L1Approval == "Y" && rec.L1Approvalbool == false)
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
            //r.AmbPressure = rec.AmbPressure;
            r.AmbTemp = rec.AmbTemp;
            r.ChangeBy = _userId;
            r.ForcedDeration = rec.ForcedDeration;
            r.IsForced = rec.IsForced == null ? "N" : rec.IsForced;
            r.IsGas = rec.IsGas == null ? "N" : rec.IsGas;
            //r.IsLDC = (rec.IsLDC == null) ? "N" : rec.IsLDC;
            r.IsOutageDeration = rec.IsOutageDeration == null ? "N" : rec.IsOutageDeration;
            r.IsPlanned = rec.IsPlanned == null ? "N" : rec.IsPlanned;
            r.StartupRampId = rec.StartupRampId == null ? "N" : rec.StartupRampId;
            r.L1Approval = rec.L1Approvalbool == true ? "Y" : "N";
            r.L2Approval = rec.L2Approvalbool == true ? "Y" : "N";
            r.PlannedDeration = rec.PlannedDeration;
            r.Reason = rec.Reason;
            //r.SeaWaterTemp = rec.SeaWaterTemp;

            r.ChangeDate = DateTime.Now;

            _context.SaveChanges();
        }

        [HttpPost]
        public JsonResult FetchLossLedger2(List<LossInputSet> input)
        {
            KGTELossFormViewModel vmodel = new KGTELossFormViewModel();
            string UnitId = input[0].UnitId;
            string UnitName;
            DateTime date = DateTime.Parse(input[0].date);

            vmodel.Ledger2 = getLedger2(UnitId, date);
            UnitName = _context.UnitsMasters.Single(x => x.UnitId == UnitId).UnitName;
            vmodel.FormHeading = "KGTPS-Energy Loss Recording Register [" + UnitName + ", " + date.ToString("dd-MM-yyyy") + "]";
            vmodel.SelectedUnitId = UnitId;
            vmodel.Status = new RequestStatus { CODE = "OK", TEXT = "OK" };

            return Json(vmodel);
        }
        private List<UnitReadingRegister2> getLedger2(string pUnitId, DateTime pdate)
        {
            List<UnitReadingRegister2> ledger = new List<UnitReadingRegister2>();
            IEnumerable<FetchRegister2_Result> res;


            switch (_authId)
            {
                case CommDef.AUTH_ENERGYLOSS_KGT_DATAENTRY:
                case CommDef.AUTH_ENERGYLOSS_KGT_PWRAPPROVAL:
                    res = _context.FetchRegister2(pUnitId, pdate).OrderBy(z => z.RdgDateTime);
                    break;
                case CommDef.AUTH_ENERGYLOSS_KGT_OPRAPPROVAL:
                    res = _context.FetchRegister2(pUnitId, pdate).Where(z => z.RecSrc == "DB").OrderBy(z => z.RdgDateTime);
                    break;
                case CommDef.AUTH_ENERGYLOSS_KGT_PERAPPROVAL:
                    res = _context.FetchRegister2(pUnitId, pdate).Where(z => z.RecSrc == "DB" /*&& z.L1Approval == "Y"*/).OrderBy(z => z.RdgDateTime);
                    break;
                default:
                    res = _context.FetchRegister2(pUnitId, pdate).OrderBy(z => z.RdgDateTime);
                    break;
            }

            foreach (var x in res)
            {
                ledger.Add(new UnitReadingRegister2
                {
                    E1ActualDispatchMW = x.E1ActualDispatchMW,
                    E2ActualDispatchMW = x.E2ActualDispatchMW,
                    E3ActualDispatchMW = x.E3ActualDispatchMW,
                    E4ActualDispatchMW = x.E4ActualDispatchMW,
                    E5ActualDispatchMW = x.E5ActualDispatchMW,
                    E6ActualDispatchMW = x.E6ActualDispatchMW,
                    E7ActualDispatchMW = x.E7ActualDispatchMW,
                    E8ActualDispatchMW = x.E8ActualDispatchMW,
                    ctrack = x.RecSrc == "DB" ? "N" : "Y",
                    E1Outage = x.E1Outage,
                    E2Outage = x.E2Outage,
                    E3Outage = x.E3Outage,
                    E4Outage = x.E4Outage,
                    E5Outage = x.E5Outage,
                    E6Outage = x.E6Outage,
                    E7Outage = x.E7Outage,
                    E8Outage = x.E8Outage,
                    ChangeBy = x.ChangeBy,
                    ChangeDate = x.ChangeDate,
                    CreateBy = x.CreateBy,
                    CreateDate = x.CreateDate,
                    CreateDateStr = x.CreateDate.ToString(),
                    L1Approvalbool = x.L1Approval == "Y" ? true : false,
                    L1ApprovalCopy = x.L1Approval == "Y" ? true : false,
                    L1ApproveBy = x.L1ApproveBy,
                    L1ApproveDate = x.L1ApproveDate,
                    L2Approvalbool = x.L2Approval == "Y" ? true : false,
                    L2ApprovalCopy = x.L2Approval == "Y" ? true : false,
                    L2ApproveBy = x.L2ApproveBy,
                    L2ApproveDate = x.L2ApproveDate,
                    RdgDateTime = x.RdgDateTime ?? new DateTime(1900, 1, 1),
                    RdgDateTimeStr = x.RdgDateTime.ToString(),
                    Reason = x.Reason,
                    SiteId = x.SiteID,
                    UnitId = x.UnitId
                });
            }
            return ledger;
        }

        [HttpPost]
        public JsonResult PostChanges2(List<UnitReadingRegister2> changes)
        {
            KGTELossFormViewModel vmodel = new KGTELossFormViewModel();
            string UnitId;
            DateTime date;

            if (changes == null || changes.Count == 0)
            {
                vmodel.Status = new RequestStatus { CODE = "ERROR", TEXT = "There was Noting to Save to Database" };
            }
            else
            {
                UnitId = changes[0].UnitId;
                date = DateTime.Parse(changes[0].RdgDateTimeStr);
                SaveChanges2(changes);
                vmodel.Ledger2 = getLedger2(UnitId, date);
                vmodel.SelectedUnitId = UnitId;
                vmodel.Status = new RequestStatus { CODE = "OK", TEXT = "OK" };
            }

            return Json(vmodel);
        }

        private void SaveChanges2(List<UnitReadingRegister2> changes)
        {

            foreach (var c in changes)
            {

                if (c.CreateDateStr == null || DateTime.Parse(c.CreateDateStr) < DateTime.Parse("1/1/2018"))
                {
                    AddRecord2(c);
                }
                else
                {
                    EditRecord2(c);
                }
            }
        }

        private void AddRecord2(UnitReadingRegister2 rec)
        {
            rec.RdgDateTime = DateTime.Parse(rec.RdgDateTimeStr);
            rec.CreateDate = DateTime.Now;
            rec.CreateBy = _userId;
            rec.ChangeDate = DateTime.Now;
            rec.ChangeBy = _userId;
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

            rec.E1Outage = rec.E1Outage == null ? "N" : rec.E1Outage;
            rec.E2Outage = rec.E2Outage == null ? "N" : rec.E2Outage;
            rec.E3Outage = rec.E3Outage == null ? "N" : rec.E3Outage;
            rec.E4Outage = rec.E4Outage == null ? "N" : rec.E4Outage;
            rec.E5Outage = rec.E5Outage == null ? "N" : rec.E5Outage;
            rec.E6Outage = rec.E6Outage == null ? "N" : rec.E6Outage;
            rec.E7Outage = rec.E7Outage == null ? "N" : rec.E7Outage;
            rec.E8Outage = rec.E8Outage == null ? "N" : rec.E8Outage;

            _context.UnitReadingRegister2.Add(rec);
            _context.SaveChanges();
        }

        private void EditRecord2(UnitReadingRegister2 rec)
        {
            DateTime date;
            string UnitId;

            date = DateTime.Parse(rec.RdgDateTimeStr);
            UnitId = rec.UnitId;

            var r = _context.UnitReadingRegister2.Single(x => x.UnitId == UnitId && x.RdgDateTime == date);


            if (r.L1Approval == "Y" && rec.L1Approvalbool == false)
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

            r.E1ActualDispatchMW = rec.E1ActualDispatchMW;
            r.E2ActualDispatchMW = rec.E2ActualDispatchMW;
            r.E3ActualDispatchMW = rec.E3ActualDispatchMW;
            r.E4ActualDispatchMW = rec.E4ActualDispatchMW;
            r.E5ActualDispatchMW = rec.E5ActualDispatchMW;
            r.E6ActualDispatchMW = rec.E6ActualDispatchMW;
            r.E7ActualDispatchMW = rec.E7ActualDispatchMW;
            r.E8ActualDispatchMW = rec.E8ActualDispatchMW;

            r.E1Outage = rec.E1Outage == null ? "N" : rec.E1Outage;
            r.E2Outage = rec.E2Outage == null ? "N" : rec.E2Outage;
            r.E3Outage = rec.E3Outage == null ? "N" : rec.E3Outage;
            r.E4Outage = rec.E4Outage == null ? "N" : rec.E4Outage;
            r.E5Outage = rec.E5Outage == null ? "N" : rec.E5Outage;
            r.E6Outage = rec.E6Outage == null ? "N" : rec.E6Outage;
            r.E7Outage = rec.E7Outage == null ? "N" : rec.E7Outage;
            r.E8Outage = rec.E8Outage == null ? "N" : rec.E8Outage;

            r.ChangeBy = _userId;
            r.L1Approval = rec.L1Approvalbool == true ? "Y" : "N";
            r.L2Approval = rec.L2Approvalbool == true ? "Y" : "N";
            r.Reason = rec.Reason;
            r.ChangeDate = DateTime.Now;

            _context.SaveChanges();
        }
        #endregion
    }
}