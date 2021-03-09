using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenerationLogBook.Utility;
using GenerationLogBook.ViewModels;
using System.Data.Entity;
using System.Globalization;

namespace GenerationLogBook.Controllers
{
    [GenLogBookAuthorization]
    public class ReportCoreController : Controller
    {
        private GenerationLogBookEntities _context;

        //For Unit Wise
        private string[] ELRH1 = { "KPC-GT1", "KPC-GT2", "KPC-GT3", "KPC-GT4", "KPC-ST1", "KPC-ST2" };
        private string[] ELRD1 = { "KPC-GT1", "KPC-GT2", "KPC-GT3", "KPC-GT4", "KPC-ST1", "KPC-ST2" };
        private string[] ELRH3 = { "SGT-SC1", "SGT-SC2", "SGT-SC3", "SGT-SC4", "KGT-SC1", "KGT-SC2", "KGT-SC3", "KGT-SC4" };
        private string[] ELRH5 = { "SGT-ST1", "KGT-ST1" };
        private string[] ELRH6 = { "BQ1-UT1", "BQ1-UT2", "BQ1-UT3", "BQ1-UT4", "BQ1-UT5", "BQ1-UT6" };
        private string[] ELRH4 = { "BQ2-GT1", "BQ2-GT2", "BQ2-GT3" };
        private string[] ELRH7 = { "BQ2-ST1" };

        //For Sitewise
        private string[] ELRH2 = { "KPC" };
        private string[] ELRD2 = { "KPC" };

        private string[] ELRD6 = { "BQ1" };
        private string[] ELRD7 = { "BQ2" };


        private string[] ELRD3 = { "BQ2-GT1", "BQ2-GT2", "BQ2-GT3" };
        private string[] ELRD4 = { "BQ2-ST1" };



        //for KGTPS
        private string[] ELRD8 = { "KGT-SC1", "KGT-SC2", "KGT-SC3", "KGT-SC4" };
        private string[] ELRD9 = { "KGT-ST1" };
        private string[] ELRD10 = { "KGT" };

        //For SGTPS
        private string[] ELRD11 = { "SGT-SC1", "SGT-SC2", "SGT-SC3", "SGT-SC4" };
        private string[] ELRD12 = { "SGT-ST1" };
        private string[] ELRD13 = { "SGT" };

        public ReportCoreController()
        {
            _context = new GenerationLogBookEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        // GET: ReportCore
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getELRHourlyForUnit(ReportInputParams input)
        {
            ReportsFormViewModel vmodel;

            if (ELRH1.Contains(input.Id))
                vmodel = getELRH1Format(input);
            else if (ELRH3.Contains(input.Id))
                vmodel = getELRH3Format(input);
            else if (ELRH5.Contains(input.Id))
                vmodel = getELRH5Format(input);
            else if (ELRH4.Contains(input.Id))
                vmodel = getELRH4Format(input);
            else if (ELRH6.Contains(input.Id))
                vmodel = getELRH6Format(input);
            else if (ELRH7.Contains(input.Id))
                vmodel = getELRH7Format(input);
            else
                vmodel = null;

            return PartialView("_ReportTemplate", vmodel);
        }

        [HttpGet]
        public ActionResult getELRHourlyForSite(ReportInputParams input)
        {
            ReportsFormViewModel vmodel;

            if (ELRH2.Contains(input.Id))
                vmodel = getELRH2Format(input);
            //else if (SGTUnits.Contains(input.Id))
            //    vmodel = getELRHourlyForSGTUnits(input);
            else
                vmodel = null;
            return PartialView("_ReportTemplate", vmodel);
        }

        [HttpGet]
        public ActionResult getELRDailyForUnit(ReportInputParams input)
        {
            ReportsFormViewModel vmodel;

            if (ELRD1.Contains(input.Id))
                vmodel = getELRDailyKPC_UnitWise(input);
            //else if (SGTUnits.Contains(input.Id))
            //    vmodel = getELRHourlyForSGTUnits(input);
            else if (ELRH6.Contains(input.Id))
                vmodel = getELRDailyBQPS1(input);
            else if (ELRD3.Contains(input.Id))
                vmodel = getELRDailyBQPS2_GT(input);

            else if (ELRD4.Contains(input.Id))
                vmodel = getELRDailyBQPS2_ST(input);

            else if (ELRD8.Contains(input.Id))
                vmodel = getELRDailyKGTPS_UnitWise(input);
            else if (ELRD9.Contains(input.Id))
                vmodel = getELRDailyKGTPS_UnitWiseStation1(input);

            else if (ELRD11.Contains(input.Id))
                vmodel = getELRDailySGTPS_UnitWise(input);
            else if (ELRD12.Contains(input.Id))
                vmodel = getELRDailySGTPS_UnitWiseStation1(input);

            else
                vmodel = null;
            return PartialView("_ReportTemplate", vmodel);
        }



        [HttpGet]
        public ActionResult getELRDailyForSite(ReportInputParams input)
        {
            ReportsFormViewModel vmodel;

            if (ELRD2.Contains(input.Id))
                vmodel = getELRDailyKPC_Station(input);
            //else if (SGTUnits.Contains(input.Id))
            //    vmodel = getELRHourlyForSGTUnits(input);

            else if (ELRD6.Contains(input.Id))
            {
                vmodel = getELRDailyBQPS1SiteWise(input);
            }

            else if (ELRD7.Contains(input.Id))
            {
                vmodel = getELRDailyBQPS2_StationWise(input);
            }
            else if (ELRD10.Contains(input.Id))
            {
                vmodel = getELRDailyKGTPS_Station(input);
            }
            else if (ELRD13.Contains(input.Id))
            {
                vmodel = getELRDailySGTPS_Station(input);
            }


            else
                vmodel = null;

            return PartialView("_ReportTemplate", vmodel);
        }




        [HttpGet]
        public ActionResult getELRMonthlyForUnit(ReportInputParams input)
        {
            ReportsFormViewModel vmodel;

            if (ELRD2.Contains(input.Id))
                vmodel = getELRMonthlyKPC_UnitWise(input);
            //else if (SGTUnits.Contains(input.Id))
            //    vmodel = getELRHourlyForSGTUnits(input);
            else if (ELRD6.Contains(input.Id))
                vmodel = getELRMonthlyBQPS1(input);
            else if (ELRD7.Contains(input.Id))
                vmodel = getELRMonthlyBQPS2_UnitWise(input);
            else if (ELRD10.Contains(input.Id))
                vmodel = getELRMontlyKGTPS_UnitWise(input);
            else if (ELRD13.Contains(input.Id))
                vmodel = getELRMontlySGTPS_UnitWise(input);

            else
                vmodel = null;
            return PartialView("_ReportTemplate", vmodel);
        }


        [HttpGet]
        public ActionResult getELRMonthlyForSite(ReportInputParams input)
        {
            ReportsFormViewModel vmodel;

            if (ELRD2.Contains(input.Id))
                vmodel = getELRMonthlyKPC_Station(input);
            //else if (SGTUnits.Contains(input.Id))
            //    vmodel = getELRHourlyForSGTUnits(input);
            else if (ELRD6.Contains(input.Id))
                vmodel = getELRMonthlyBQPS1Site(input);
            else if (ELRD7.Contains(input.Id))
                vmodel = getELRMonthlyBQPS2_StationWise(input);
            else if (ELRD10.Contains(input.Id))
                vmodel = getELRMonthlyKGTPS_Station(input);
            else if (ELRD13.Contains(input.Id))
                vmodel = getELRMonthlySGTPS_Station(input);
            else
                vmodel = null;
            return PartialView("_ReportTemplate", vmodel);
        }

        private ReportsFormViewModel getELRH1Format(ReportInputParams input) //Mainly used for KPC Hourly Report
        {
            string UnitId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            vmodel.data = _context.VRPTHourlyUnitwiseReg1AFinal.Where(x => x.UnitId == UnitId && DbFunctions.TruncateTime(x.CurrRdgDateTime) >= dtFrom && DbFunctions.TruncateTime(x.CurrRdgDateTime) <= dtTo).ToList();
            vmodel.cols = new List<ReportColumnsDef> {
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Reading<br/>DateTime", type="DateMinutes" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
                new ReportColumnsDef{ data= "ReferenceCapacity" , title= "Ref<br/>Cap." },
                new ReportColumnsDef{ data= "ActualDispatchMW" , title= "Actual<br/>Dispatch<br/>(MW)" },
                new ReportColumnsDef{ data= "AmbTemp" , title= "Ambient<br/>Temp<br/>(Deg. C)" },
                new ReportColumnsDef{ data= "SeaWaterTemp" , title= "SeaWater<br/>Temp<br/>(Deg. C)" },
                new ReportColumnsDef{ data= "AmbPressure" , title= "Ambient<br/>Pressure<br/>(mBar)" },
                new ReportColumnsDef{ data= "CurrIsOutageDeration" , title= "Outage<br/>Deration" },
                new ReportColumnsDef{ data= "CurrIsForced" , title= "Forced<br/>ForcedGrid" },
                new ReportColumnsDef{ data= "CurrIsPlanned" , title= "Planned" },
                new ReportColumnsDef{ data= "CurrIsLDC" , title= "LDC"},
                new ReportColumnsDef{ data= "CurrIsGas" , title= "Gas" },
                new ReportColumnsDef{ data= "CurrStartupRampId" , title= "Startup<br/>Ramp" },
                new ReportColumnsDef{ data= "Reason" , title= "Reason" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "AmbDeration" , title= "Ambient<br/>Deration" },
                new ReportColumnsDef{ data= "ForcedDeration" , title= "Forced<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedDeration" , title= "Planned<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned<br/>OutageHrs<br/>" },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned<br/>Outage<br/>Hrs" },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned<br/>OutageHrs<br/>Grid" },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "OutageHrs<br/>LDC" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby<br/>Hrs" },
                new ReportColumnsDef{ data= "Total1" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedRampHrs" , title= "Planned<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "ForcedRampHrs" , title= "Forced<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "Total2" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "LDCOutageMwh" , title= "LDC<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "StandbyMwh" , title= "Standby<br/>Mwh" },
                new ReportColumnsDef{ data= "Total3" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedRampMwh" , title= "Planned<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedRampMwh" , title= "Forced<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "LDCDerationMwh" , title= "LDC<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "Total4" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "AvailabilityPercent" , title= "Availability<br/>Percent" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "ELRExclGrid" , title= "ELR<br/>ExclGrid<br/>Percent" },
                new ReportColumnsDef{ data= "ELR",  title= "ELR<br/>Percent" }
            };

            string js =
                "function(row, data, start, end, display) {\n" +
                "if(result.length ==0){\n" +
                "  return;\n" +
                "}\n" +
                "var api = this.api(), data;\n";
            string jsCol = "", jsFoot = "";

            jsCol = jsCol + "var " + "GDC" + " = api\n" +
                       "       .column(" + "1" + ")\n" +
                       "        .data()\n" +
                       "        .reduce ((a, b, index, array) => {\n" +
                       "a += b;\n" +
                       "if (index === array.length - 1)\n" +
                       "{\n" +
                       "return a / array.length;\n" +
                       "}\n" +
                       "else\n" +
                       "{\n" +
                       "return a;\n" +
                       "}\n" +
                       "});\n";
            jsCol = jsCol + "GDC = GDC.toFixed(2);";
            jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            jp = getJSColumnTotal("PH", 14);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageHrs", 18);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnPlannedOutageHrs", 19);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnPlannedOutageHrsGrid", 20);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;
            /////////////
            jp = getJSColumnTotal("OutageHrsLDC", 21);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("StandbyHrs", 22);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("Total", 23);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnplannedDertionHrs", 24);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationHrs", 25);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GasDerationHrs", 26);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCDerationHrs", 27);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("AmbDerationHrs", 28);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampHrs", 29);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampHrs", 30);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalT", 31);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageMW", 32);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedOutageMW", 33);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCOutageMW", 34);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("StandbyMW", 35);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalH", 36);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampMW", 37);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampMW", 38);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedDerationMW", 39);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationMW", 40);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCDerationMW", 41);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GasDerationMW", 42);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("AmbDerationMW", 43);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalF", 44);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jsCol = jsCol + "var AvailabilityPerc = (((PH - PlannedOutageHrs - UnPlannedOutageHrs) / PH) * 100);\n";
            jsFoot = jsFoot + "$(api.column(" + "45" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            jsCol = jsCol + "var EUDH = (ForcedDerationMW/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "46" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            jsCol = jsCol + "var EPDH = (PlannedDerationMW/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "47" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            jsCol = jsCol + "var ELRExclGrid = (((EUDH+UnPlannedOutageHrs)/(UnPlannedOutageHrs+(PH-PlannedOutageHrs-UnPlannedOutageHrs-StandbyHrs-OutageHrsLDC-UnPlannedOutageHrsGrid)))) * 100;";
            jsFoot = jsFoot + "$(api.column(" + "48" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var ELR= ((EUDH+UnPlannedOutageHrs+UnPlannedOutageHrsGrid)/(UnPlannedOutageHrs+UnPlannedOutageHrsGrid+(PH-PlannedOutageHrs-UnPlannedOutageHrs-UnPlannedOutageHrsGrid-StandbyHrs-OutageHrsLDC))) * 100;";
            jsFoot = jsFoot + "$(api.column(" + "49" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            js = js + jsCol;
            js = js + jsFoot + "}";

            vmodel.JSfootercallback = js;

            return vmodel;
        }

        private ReportsFormViewModel getELRH3Format(ReportInputParams input) //Mainly used for SGTPS & KGTPS Hourly Report
        {
            string UnitId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            vmodel.data = _context.VRPTHourlyUnitwiseReg2Final.Where(x => x.UnitId == UnitId && DbFunctions.TruncateTime(x.RdgDateTime) >= dtFrom && DbFunctions.TruncateTime(x.RdgDateTime) <= dtTo).ToList();
            vmodel.cols = new List<ReportColumnsDef> {
                //new ReportColumnsDef{ data= "UnitId", title = "UnitId"},
                //new ReportColumnsDef{ data= "SiteId", title = "SiteId"},
                new ReportColumnsDef{ data= "RdgDateTime", title = "DateTime", type="DateMinutes"},
                new ReportColumnsDef{ data= "E1ActualDispatchMW", title = "E1Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{ data= "E1Outage", title = "E1Outage"},
                new ReportColumnsDef{ data= "E2ActualDispatchMW", title = "E2Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{ data= "E2Outage", title = "E2Outage"},
                new ReportColumnsDef{ data= "E3ActualDispatchMW", title = "E3Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{ data= "E3Outage", title = "E3Outage"},
                new ReportColumnsDef{ data= "E4ActualDispatchMW", title = "E4Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{ data= "E4Outage", title = "E4Outage"},
                new ReportColumnsDef{ data= "E5ActualDispatchMW", title = "E5Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{ data= "E5Outage", title = "E5Outage"},
                new ReportColumnsDef{ data= "E6ActualDispatchMW", title = "E6Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{ data= "E6Outage", title = "E6Outage"},
                new ReportColumnsDef{ data= "E7ActualDispatchMW", title = "E7Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{ data= "E7Outage", title = "E7Outage"},
                new ReportColumnsDef{ data= "E8ActualDispatchMW", title = "E8Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{ data= "E8Outage", title = "E8Outage"},
                new ReportColumnsDef{ data= "Reason", title = "Reason"},
                new ReportColumnsDef{ data= "PH", title = "PH"},
                new ReportColumnsDef{ data= "E1ForcedHrs", title = "E1<br>Forced<br>Hrs"},
                new ReportColumnsDef{ data= "E2ForcedHrs", title = "E2<br>Forced<br>Hrs"},
                new ReportColumnsDef{ data= "E3ForcedHrs", title = "E3<br>Forced<br>Hrs"},
                new ReportColumnsDef{ data= "E4ForcedHrs", title = "E4<br>Forced<br>Hrs"},
                new ReportColumnsDef{ data= "E5ForcedHrs", title = "E5<br>Forced<br>Hrs"},
                new ReportColumnsDef{ data= "E6ForcedHrs", title = "E6<br>Forced<br>Hrs"},
                new ReportColumnsDef{ data= "E7ForcedHrs", title = "E7<br>Forced<br>Hrs"},
                new ReportColumnsDef{ data= "E8ForcedHrs", title = "E8<br>Forced<br>Hrs"},
                new ReportColumnsDef{ data= "TotalForcedHrs", title = "Total<br>Forced<br>Hrs"},
                new ReportColumnsDef{ data= "E1PlannedHrs", title = "E1<br>Planned<br>Hrs"},
                new ReportColumnsDef{ data= "E2PlannedHrs", title = "E2<br>Planned<br>Hrs"},
                new ReportColumnsDef{ data= "E3PlannedHrs", title = "E3<br>Planned<br>Hrs"},
                new ReportColumnsDef{ data= "E4PlannedHrs", title = "E4<br>Planned<br>Hrs"},
                new ReportColumnsDef{ data= "E5PlannedHrs", title = "E5<br>Planned<br>Hrs"},
                new ReportColumnsDef{ data= "E6PlannedHrs", title = "E6<br>Planned<br>Hrs"},
                new ReportColumnsDef{ data= "E7PlannedHrs", title = "E7<br>Planned<br>Hrs"},
                new ReportColumnsDef{ data= "E8PlannedHrs", title = "E8<br>Planned<br>Hrs"},
                new ReportColumnsDef{ data= "TotalPlannedHrs", title = "Total<br>Planned<br>Hrs"},
                new ReportColumnsDef{ data= "E1StandbyHrs", title = "E1<br>Standby<br>Hrs"},
                new ReportColumnsDef{ data= "E2StandbyHrs", title = "E2<br>Standby<br>Hrs"},
                new ReportColumnsDef{ data= "E3StandbyHrs", title = "E3<br>Standby<br>Hrs"},
                new ReportColumnsDef{ data= "E4StandbyHrs", title = "E4<br>Standby<br>Hrs"},
                new ReportColumnsDef{ data= "E5StandbyHrs", title = "E5<br>Standby<br>Hrs"},
                new ReportColumnsDef{ data= "E6StandbyHrs", title = "E6<br>Standby<br>Hrs"},
                new ReportColumnsDef{ data= "E7StandbyHrs", title = "E7<br>Standby<br>Hrs"},
                new ReportColumnsDef{ data= "E8StandbyHrs", title = "E8<br>Standby<br>Hrs"},
                new ReportColumnsDef{ data= "TotalStandbyHrs", title = "Total<br>Standby<br>Hrs"},
                new ReportColumnsDef{ data= "E1ForcedHrsGrid", title = "E1<br>ForcedHrs<br>Grid"},
                new ReportColumnsDef{ data= "E2ForcedHrsGrid", title = "E2<br>ForcedHrs<br>Grid"},
                new ReportColumnsDef{ data= "E3ForcedHrsGrid", title = "E3<br>ForcedHrs<br>Grid"},
                new ReportColumnsDef{ data= "E4ForcedHrsGrid", title = "E4<br>ForcedHrs<br>Grid"},
                new ReportColumnsDef{ data= "E5ForcedHrsGrid", title = "E5<br>ForcedHrs<br>Grid"},
                new ReportColumnsDef{ data= "E6ForcedHrsGrid", title = "E6<br>ForcedHrs<br>Grid"},
                new ReportColumnsDef{ data= "E7ForcedHrsGrid", title = "E7<br>ForcedHrs<br>Grid"},
                new ReportColumnsDef{ data= "E8ForcedHrsGrid", title = "E8<br>ForcedHrs<br>Grid"},
                new ReportColumnsDef{ data= "TotalForcedHrsGrid", title = "Total<br>ForcedHrs<br>Grid"},
                new ReportColumnsDef{ data= "E1AvailabilityExclGrid", title = "E1<br>Availability<br>ExclGrid"},
                new ReportColumnsDef{ data= "E2AvailabilityExclGrid", title = "E2<br>Availability<br>ExclGrid"},
                new ReportColumnsDef{ data= "E3AvailabilityExclGrid", title = "E3<br>Availability<br>ExclGrid"},
                new ReportColumnsDef{ data= "E4AvailabilityExclGrid", title = "E4<br>Availability<br>ExclGrid"},
                new ReportColumnsDef{ data= "E5AvailabilityExclGrid", title = "E5<br>Availability<br>ExclGrid"},
                new ReportColumnsDef{ data= "E6AvailabilityExclGrid", title = "E6<br>Availability<br>ExclGrid"},
                new ReportColumnsDef{ data= "E7AvailabilityExclGrid", title = "E7<br>Availability<br>ExclGrid"},
                new ReportColumnsDef{ data= "E8AvailabilityExclGrid", title = "E8<br>Availability<br>ExclGrid"},
                new ReportColumnsDef{ data= "TotalAvailabilityExclGrid", title = "Total<br>Availability<br>ExclGrid"},
                new ReportColumnsDef{ data= "E1ELRExclGrid", title = "E1<br>ELR<br>ExclGrid"},
                new ReportColumnsDef{ data= "E2ELRExclGrid", title = "E2<br>ELR<br>ExclGrid"},
                new ReportColumnsDef{ data= "E3ELRExclGrid", title = "E3<br>ELR<br>ExclGrid"},
                new ReportColumnsDef{ data= "E4ELRExclGrid", title = "E4<br>ELR<br>ExclGrid"},
                new ReportColumnsDef{ data= "E5ELRExclGrid", title = "E5<br>ELR<br>ExclGrid"},
                new ReportColumnsDef{ data= "E6ELRExclGrid", title = "E6<br>ELR<br>ExclGrid"},
                new ReportColumnsDef{ data= "E7ELRExclGrid", title = "E7<br>ELR<br>ExclGrid"},
                new ReportColumnsDef{ data= "E8ELRExclGrid", title = "E8<br>ELR<br>ExclGrid"},
                new ReportColumnsDef{ data= "TotalELRExclGrid", title = "Total<br>ELR<br>ExclGrid"},
                new ReportColumnsDef{ data= "E1ELRIncGrid", title = "E1<br>ELR<br>IncGrid"},
                new ReportColumnsDef{ data= "E2ELRIncGrid", title = "E2<br>ELR<br>IncGrid"},
                new ReportColumnsDef{ data= "E3ELRIncGrid", title = "E3<br>ELR<br>IncGrid"},
                new ReportColumnsDef{ data= "E4ELRIncGrid", title = "E4<br>ELR<br>IncGrid"},
                new ReportColumnsDef{ data= "E5ELRIncGrid", title = "E5<br>ELR<br>IncGrid"},
                new ReportColumnsDef{ data= "E6ELRIncGrid", title = "E6<br>ELR<br>IncGrid"},
                new ReportColumnsDef{ data= "E7ELRIncGrid", title = "E7<br>ELR<br>IncGrid"},
                new ReportColumnsDef{ data= "E8ELRIncGrid", title = "E8<br>ELR<br>IncGrid"},
                new ReportColumnsDef{ data= "TotalELRIncGrid", title = "Total<br>ELR<br>IncGrid"}
            };

            string js =
                "function(row, data, start, end, display) {\n" +
                "if(result.length ==0){\n" +
                "  return;\n" +
                "}\n" +
                "var api = this.api(), data;\n";
            string jsCol = "", jsFoot = "";

            jp = getJSColumnTotal("PH", 18);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E1ForcedHrs", 19);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E2ForcedHrs", 20);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;
            /////////////
            jp = getJSColumnTotal("E3ForcedHrs", 21);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E4ForcedHrs", 22);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E5ForcedHrs", 23);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E6ForcedHrs", 24);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E7ForcedHrs", 25);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E8ForcedHrs", 26);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalForcedHrs", 27);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E1PlannedHrs", 28);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E2PlannedHrs", 29);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E3PlannedHrs", 30);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E4PlannedHrs", 31);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E5PlannedHrs", 32);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E6PlannedHrs", 33);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E7PlannedHrs", 34);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E8PlannedHrs", 35);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalPlannedHrs", 36);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E1StandbyHrs", 37);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E2StandbyHrs", 38);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E3StandbyHrs", 39);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E4StandbyHrs", 40);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E5StandbyHrs", 41);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E6StandbyHrs", 42);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E7StandbyHrs", 43);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E8StandbyHrs", 44);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalStandbyHrs", 45);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E1ForcedHrsGrid", 46);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E2ForcedHrsGrid", 47);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E3ForcedHrsGrid", 48);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E4ForcedHrsGrid", 49);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E5ForcedHrsGrid", 50);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E6ForcedHrsGrid", 51);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E7ForcedHrsGrid", 52);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("E8ForcedHrsGrid", 53);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalForcedHrsGrid", 54);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            /* jp = getJSColumnTotal("E1AvailabilityExclGrid", 55);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E2AvailabilityExclGrid", 56);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E3AvailabilityExclGrid", 57);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E4AvailabilityExclGrid", 58);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E5AvailabilityExclGrid", 59);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E6AvailabilityExclGrid", 60);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E7AvailabilityExclGrid", 61);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E8AvailabilityExclGrid", 62);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("TotalAvailabilityExclGrid", 63);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E1ELRExclGrid", 64);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E2ELRExclGrid", 65);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E3ELRExclGrid", 66);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E4ELRExclGrid", 67);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E5ELRExclGrid", 68);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E6ELRExclGrid", 69);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E7ELRExclGrid", 70);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E8ELRExclGrid", 71);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("TotalELRExclGrid", 72);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E1ELRIncGrid", 73);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E2ELRIncGrid", 74);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E3ELRIncGrid", 75);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E4ELRIncGrid", 76);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E5ELRIncGrid", 77);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E6ELRIncGrid", 78);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E7ELRIncGrid", 79);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("E8ELRIncGrid", 80);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;

             jp = getJSColumnTotal("TotalELRIncGrid", 81);
             jsCol = jsCol + jp.JSCol;
             jsFoot = jsFoot + jp.JSFooter;
             */


            jsCol = jsCol + "var E1AvailabilityExclGrid = (((PH-E1ForcedHrs-E1PlannedHrs)/PH)*100);\n";
            jsFoot = jsFoot + "$(api.column(" + "55" + ").footer()).html(" + "fnNumFormatter(E1AvailabilityExclGrid)" + ");\n";

            jsCol = jsCol + "var E2AvailabilityExclGrid = (((PH-E2ForcedHrs-E2PlannedHrs)/PH)*100);\n";
            jsFoot = jsFoot + "$(api.column(" + "56" + ").footer()).html(" + "fnNumFormatter(E2AvailabilityExclGrid)" + ");\n";

            jsCol = jsCol + "var E3AvailabilityExclGrid = (((PH-E3ForcedHrs-E3PlannedHrs)/PH)*100);\n";
            jsFoot = jsFoot + "$(api.column(" + "57" + ").footer()).html(" + "fnNumFormatter(E3AvailabilityExclGrid)" + ");\n";

            jsCol = jsCol + "var E4AvailabilityExclGrid = (((PH-E4ForcedHrs-E4PlannedHrs)/PH)*100);\n";
            jsFoot = jsFoot + "$(api.column(" + "58" + ").footer()).html(" + "fnNumFormatter(E4AvailabilityExclGrid)" + ");\n";

            jsCol = jsCol + "var E5AvailabilityExclGrid = (((PH-E5ForcedHrs-E5PlannedHrs)/PH)*100);\n";
            jsFoot = jsFoot + "$(api.column(" + "59" + ").footer()).html(" + "fnNumFormatter(E5AvailabilityExclGrid)" + ");\n";

            jsCol = jsCol + "var E6AvailabilityExclGrid = (((PH-E6ForcedHrs-E6PlannedHrs)/PH)*100);\n";
            jsFoot = jsFoot + "$(api.column(" + "60" + ").footer()).html(" + "fnNumFormatter(E6AvailabilityExclGrid)" + ");\n";

            jsCol = jsCol + "var E7AvailabilityExclGrid = (((PH-E7ForcedHrs-E7PlannedHrs)/PH)*100);\n";
            jsFoot = jsFoot + "$(api.column(" + "61" + ").footer()).html(" + "fnNumFormatter(E7AvailabilityExclGrid)" + ");\n";

            jsCol = jsCol + "var E8AvailabilityExclGrid = (((PH-E8ForcedHrs-E8PlannedHrs)/PH)*100);\n";
            jsFoot = jsFoot + "$(api.column(" + "62" + ").footer()).html(" + "fnNumFormatter(E8AvailabilityExclGrid)" + ");\n";

            jsCol = jsCol + "var TotalAvailabilityExclGrid = (((PH*8)-TotalForcedHrs-TotalPlannedHrs)/(PH*8))*100;\n";
            jsFoot = jsFoot + "$(api.column(" + "63" + ").footer()).html(" + "fnNumFormatter(TotalAvailabilityExclGrid)" + ");\n";




            jsCol = jsCol + "var E1ELRExclGrid = (E1ForcedHrs/(E1ForcedHrs+(PH-E1ForcedHrs-E1PlannedHrs-E1StandbyHrs-E1ForcedHrsGrid)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "64" + ").footer()).html(" + "fnNumFormatter(E1ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var E2ELRExclGrid = (E2ForcedHrs/(E2ForcedHrs+(PH-E2ForcedHrs-E2PlannedHrs-E2StandbyHrs-E2ForcedHrsGrid)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "65" + ").footer()).html(" + "fnNumFormatter(E2ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var E3ELRExclGrid = (E3ForcedHrs/(E3ForcedHrs+(PH-E3ForcedHrs-E3PlannedHrs-E3StandbyHrs-E3ForcedHrsGrid)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "66" + ").footer()).html(" + "fnNumFormatter(E3ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var E4ELRExclGrid = (E4ForcedHrs/(E4ForcedHrs+(PH-E4ForcedHrs-E4PlannedHrs-E4StandbyHrs-E4ForcedHrsGrid)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "67" + ").footer()).html(" + "fnNumFormatter(E4ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var E5ELRExclGrid = (E5ForcedHrs/(E5ForcedHrs+(PH-E5ForcedHrs-E5PlannedHrs-E5StandbyHrs-E5ForcedHrsGrid)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "68" + ").footer()).html(" + "fnNumFormatter(E5ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var E6ELRExclGrid = (E6ForcedHrs/(E6ForcedHrs+(PH-E6ForcedHrs-E6PlannedHrs-E6StandbyHrs-E6ForcedHrsGrid)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "69" + ").footer()).html(" + "fnNumFormatter(E6ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var E7ELRExclGrid = (E7ForcedHrs/(E7ForcedHrs+(PH-E7ForcedHrs-E7PlannedHrs-E7StandbyHrs-E7ForcedHrsGrid)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "70" + ").footer()).html(" + "fnNumFormatter(E7ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var E8ELRExclGrid = (E8ForcedHrs/(E8ForcedHrs+(PH-E8ForcedHrs-E8PlannedHrs-E8StandbyHrs-E8ForcedHrsGrid)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "71" + ").footer()).html(" + "fnNumFormatter(E8ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var TotalELRExclGrid =(TotalForcedHrs/(TotalForcedHrs+((PH*8)-TotalForcedHrs-TotalPlannedHrs-TotalStandbyHrs-TotalForcedHrsGrid)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "72" + ").footer()).html(" + "fnNumFormatter(TotalELRExclGrid)" + ");\n";


            jsCol = jsCol + "var E1ELRIncGrid = ((E1ForcedHrsGrid+E1ForcedHrs)/(E1ForcedHrsGrid+E1ForcedHrs+(PH-E1PlannedHrs-E1StandbyHrs-E1ForcedHrsGrid-E1ForcedHrs)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "73" + ").footer()).html(" + "fnNumFormatter(E1ELRIncGrid)" + ");\n";

            jsCol = jsCol + "var E2ELRIncGrid = ((E2ForcedHrsGrid+E2ForcedHrs)/(E2ForcedHrsGrid+E2ForcedHrs+(PH-E2PlannedHrs-E2StandbyHrs-E2ForcedHrsGrid-E2ForcedHrs)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "74" + ").footer()).html(" + "fnNumFormatter(E2ELRIncGrid)" + ");\n";

            jsCol = jsCol + "var E3ELRIncGrid = ((E3ForcedHrsGrid+E3ForcedHrs)/(E3ForcedHrsGrid+E3ForcedHrs+(PH-E3PlannedHrs-E3StandbyHrs-E3ForcedHrsGrid-E3ForcedHrs)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "75" + ").footer()).html(" + "fnNumFormatter(E3ELRIncGrid)" + ");\n";

            jsCol = jsCol + "var E4ELRIncGrid = ((E4ForcedHrsGrid+E4ForcedHrs)/(E4ForcedHrsGrid+E4ForcedHrs+(PH-E4PlannedHrs-E4StandbyHrs-E4ForcedHrsGrid-E4ForcedHrs)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "76" + ").footer()).html(" + "fnNumFormatter(E4ELRIncGrid)" + ");\n";

            jsCol = jsCol + "var E5ELRIncGrid = ((E5ForcedHrsGrid+E5ForcedHrs)/(E5ForcedHrsGrid+E5ForcedHrs+(PH-E5PlannedHrs-E5StandbyHrs-E5ForcedHrsGrid-E5ForcedHrs)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "77" + ").footer()).html(" + "fnNumFormatter(E5ELRIncGrid)" + ");\n";

            jsCol = jsCol + "var E6ELRIncGrid = ((E6ForcedHrsGrid+E6ForcedHrs)/(E6ForcedHrsGrid+E6ForcedHrs+(PH-E6PlannedHrs-E6StandbyHrs-E6ForcedHrsGrid-E6ForcedHrs)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "78" + ").footer()).html(" + "fnNumFormatter(E6ELRIncGrid)" + ");\n";

            jsCol = jsCol + "var E7ELRIncGrid = ((E7ForcedHrsGrid+E7ForcedHrs)/(E7ForcedHrsGrid+E7ForcedHrs+(PH-E7PlannedHrs-E7StandbyHrs-E7ForcedHrsGrid-E7ForcedHrs)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "79" + ").footer()).html(" + "fnNumFormatter(E7ELRIncGrid)" + ");\n";

            jsCol = jsCol + "var E8ELRIncGrid = ((E8ForcedHrsGrid+E8ForcedHrs)/(E8ForcedHrsGrid+E8ForcedHrs+(PH-E8PlannedHrs-E8StandbyHrs-E8ForcedHrsGrid-E8ForcedHrs)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "80" + ").footer()).html(" + "fnNumFormatter(E8ELRIncGrid)" + ");\n";

            jsCol = jsCol + "var TotalELRIncGrid = ((TotalForcedHrsGrid+TotalForcedHrs)/(TotalForcedHrsGrid+TotalForcedHrs+((PH*8)-TotalPlannedHrs-TotalStandbyHrs-TotalForcedHrsGrid-TotalForcedHrs)))*100;";
            jsFoot = jsFoot + "$(api.column(" + "81" + ").footer()).html(" + "fnNumFormatter(TotalELRIncGrid)" + ");\n";

            js = js + jsCol;
            js = js + jsFoot + "}";

            vmodel.JSfootercallback = js;

            return vmodel;
        }

        private ReportsFormViewModel getELRH4Format(ReportInputParams input) //Mainly used for BQPS-II(GT 1, 2 & 3) Hourly Report
        {
            string UnitId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            vmodel.data = _context.VRPTHourlyUnitwiseReg3Final.Where(x => x.UnitId == UnitId && DbFunctions.TruncateTime(x.RdgDateTime) >= dtFrom && DbFunctions.TruncateTime(x.RdgDateTime) <= dtTo).ToList();
            vmodel.cols = new List<ReportColumnsDef> {
                new ReportColumnsDef{ data= "RdgDateTime" , title= "Reading<br/>DateTime", type="DateMinutes" },
                new ReportColumnsDef{ data= "DurationHr" , title= "Duration<br/>Hrs" },
                new ReportColumnsDef{ data= "OutageNature" , title= "Outage<br/>Nature" },
                new ReportColumnsDef{ data= "CalculateDeration" , title= "Calculate<br/>Deration" },
                new ReportColumnsDef{ data= "StartupShutdown" , title= "Startup<br/>Shutdown" },
                new ReportColumnsDef{ data= "Reason" , title= "Reason" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
                new ReportColumnsDef{ data= "ReferenceCapacity" , title= "Ref<br/>Cap." },
                new ReportColumnsDef{ data= "AvailableCapacity" , title= "Available<br/>Capacity<br/>(MW)" }, //this
                new ReportColumnsDef{ data= "ActualDispatchMW" , title= "Actual<br/>Dispatch<br/>(MW)" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned<br/>Outage<br/>Hrs"},
                new ReportColumnsDef{ data= "FOHHrs" , title= "FOH<br/>Hrs" },
                new ReportColumnsDef{ data= "FOHHrsGrid" , title= "FOH<br/>Hrs<br/>Grid" },
                new ReportColumnsDef{ data= "GasHrs" , title= "Gas<br/>Hrs" },  //this
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDCOutage<br/>Hrs" },
                new ReportColumnsDef{ data= "ServiceHrs" , title= "Service<br/>Hrs" },
                new ReportColumnsDef{ data= "Total" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "DerationHrs" , title= "Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "EPDHPlannedDerationHrs" , title= "EPDH<br/>Planned Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "GasDeration<br/>Hrs" },
                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDCDeration<br/>Hrs" },
                new ReportColumnsDef{ data= "FOHDerationHrs" , title= "FOHDeration<br/>Hrs" },
                new ReportColumnsDef{ data= "FOHGridDerationHrs" , title= "FOH<br/>GridDeration<br/>Hrs" },
                new ReportColumnsDef{ data= "AvailabilityPerc" , title= "Availability<br/>%" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "ELRExclOMC" , title= "ELR<br/>Excl<br/>OMC" },
                new ReportColumnsDef{ data= "ELR" , title= "ELR" },
                new ReportColumnsDef{ data= "FOH" , title= "FOH" }
            };

            string js =
                "function(row, data, start, end, display) {\n" +
                "if(result.length ==0){\n" +
                "  return;\n" +
                "}\n" +
                "var api = this.api(), data;\n";
            string jsCol = "", jsFoot = "";

            jsCol = jsCol + "var " + "GDC" + " = api\n" +
                       "       .column(" + "6" + ")\n" +
                       "        .data()\n" +
                       "        .reduce ((a, b, index, array) => {\n" +
                       "a += b;\n" +
                       "if (index === array.length - 1)\n" +
                       "{\n" +
                       "return a / array.length;\n" +
                       "}\n" +
                       "else\n" +
                       "{\n" +
                       "return a;\n" +
                       "}\n" +
                       "});\n";
            jsCol = jsCol + "GDC = GDC.toFixed(2);";
            jsFoot = jsFoot + "$(api.column(" + "6" + ").footer()).html(" + "GDC" + ");\n";

            jp = getJSColumnTotal("ActualDispatchMW", 9);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PH", 10);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageHrs", 11);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("FOHHrs", 12);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("FOHHrsGrid", 13);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;
            /////////////
            jp = getJSColumnTotal("GasHrs", 14);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCOutageHrs", 15);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ServiceHrs", 16);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("Total", 17);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            /*     
               Availability % 
               EUDH    
               EPDH 
               ELRExclOMC  
               ELR 
               FOH
               */
            jp = getJSColumnTotal("DerationHrs", 18);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("EPDHPlannedDerationHrs", 19);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GasDerationHrs", 20);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCDerationHrs", 21);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("FOHDerationHrs", 22);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("FOHGridDerationHrs", 23);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("EUDH", 25);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("EPDH", 26);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jsCol = jsCol + "var AvailabilityPerc = ((PH-(PlannedOutageHrs+ FOHHrs))/PH)*100;\n";
            jsFoot = jsFoot + "$(api.column(" + "24" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            jsCol = jsCol + "var ELRExclOMC = ((FOHDerationHrs + FOHHrs) / (FOHHrs + ServiceHrs));";
            jsFoot = jsFoot + "$(api.column(" + "27" + ").footer()).html(" + "fnNumFormatter(ELRExclOMC)" + ");\n";

            jsCol = jsCol + "var ELR = ((FOHDerationHrs+FOHGridDerationHrs+FOHHrs+FOHHrsGrid)/(FOHHrs+FOHHrsGrid+ServiceHrs));";
            jsFoot = jsFoot + "$(api.column(" + "28" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";

            jsCol = jsCol + "var FOH = FOHHrs + FOHHrsGrid;";
            jsFoot = jsFoot + "$(api.column(" + "29" + ").footer()).html(" + "fnNumFormatter(FOH)" + ");\n";

            js = js + jsCol;
            js = js + jsFoot + "}";

            vmodel.JSfootercallback = js;

            return vmodel;
        }

        private ReportsFormViewModel getELRH5Format(ReportInputParams input) //Mainly used for SGPT & KGTPS ST Hourly Report
        {
            string UnitId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            vmodel.data = _context.VRPTHourlyUnitwiseReg1BFinal.Where(x => x.UnitId == UnitId && DbFunctions.TruncateTime(x.CurrRdgDateTime) >= dtFrom && DbFunctions.TruncateTime(x.CurrRdgDateTime) <= dtTo).ToList();
            vmodel.cols = new List<ReportColumnsDef> {
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Reading<br/>DateTime", type="DateMinutes" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
                new ReportColumnsDef{ data= "ReferenceCapacity" , title= "Ref<br/>Cap." },
                new ReportColumnsDef{ data= "ActualDispatchMW" , title= "Actual<br/>Dispatch<br/>(MW)" },
                new ReportColumnsDef{ data= "AmbTemp" , title= "Ambient<br/>Temp<br/>(Deg. F)" },
                //new ReportColumnsDef{ data= "SeaWaterTemp" , title= "SeaWater<br/>Temp<br/>(Deg. F)" },
                //new ReportColumnsDef{ data= "AmbPressure" , title= "Ambient<br/>Pressure<br/>(mBar)" },
                new ReportColumnsDef{ data= "CurrIsOutageDeration" , title= "Outage<br/>Deration" },
                new ReportColumnsDef{ data= "CurrIsForced" , title= "Forced<br/>ForcedGrid" },
                new ReportColumnsDef{ data= "CurrIsPlanned" , title= "Planned" },
                //new ReportColumnsDef{ data= "CurrIsLDC" , title= "LDC"},
                new ReportColumnsDef{ data= "CurrIsGas" , title= "Gas" },
                new ReportColumnsDef{ data= "CurrStartupRampId" , title= "Startup<br/>Ramp" },
                new ReportColumnsDef{ data= "Reason" , title= "Reason" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "AmbDeration" , title= "Ambient<br/>Deration" },
                new ReportColumnsDef{ data= "ForcedDeration" , title= "Forced<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedDeration" , title= "Planned<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned<br/>OutageHrs<br/>" },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned<br/>Outage<br/>Hrs" },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned<br/>OutageHrs<br/>Grid" },
                //new ReportColumnsDef{ data= "LDCOutageHrs" , title= "OutageHrs<br/>LDC" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby<br/>Hrs" },
                new ReportColumnsDef{ data= "Total1" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas<br/>Deration<br/>Hrs" },
                //new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedRampHrs" , title= "Planned<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "ForcedRampHrs" , title= "Forced<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "Total2" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced<br/>Outage<br/>Mwh" },
                //new ReportColumnsDef{ data= "LDCOutageMwh" , title= "LDC<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "StandbyMwh" , title= "Standby<br/>Mwh" },
                new ReportColumnsDef{ data= "Total3" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedRampMwh" , title= "Planned<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedRampMwh" , title= "Forced<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned<br/>Deration<br/>Mwh" },
                //new ReportColumnsDef{ data= "LDCDerationMwh" , title= "LDC<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "Total4" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "AvailabilityPercent" , title= "Availability<br/>Percent" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "ELRExclGrid" , title= "ELR<br/>ExclGrid" },
                new ReportColumnsDef{ data= "ELR",  title= "ELR" }
            };

            string js =
                "function(row, data, start, end, display) {\n" +
                "if(result.length ==0){\n" +
                "  return;\n" +
                "}\n" +
                "var api = this.api(), data;\n";
            string jsCol = "", jsFoot = "";

            jsCol = jsCol + "var " + "GDC" + " = api\n" +
                       "       .column(" + "1" + ")\n" +
                       "        .data()\n" +
                       "        .reduce ((a, b, index, array) => {\n" +
                       "a += b;\n" +
                       "if (index === array.length - 1)\n" +
                       "{\n" +
                       "return a / array.length;\n" +
                       "}\n" +
                       "else\n" +
                       "{\n" +
                       "return a;\n" +
                       "}\n" +
                       "});\n";
            jsCol = jsCol + "GDC = GDC.toFixed(2);";
            jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            jp = getJSColumnTotal("PH", 11);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageHrs", 15);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnPlannedOutageHrs", 16);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnPlannedOutageHrsGrid", 17);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;
            /////////////
            //jp = getJSColumnTotal("OutageHrsLDC", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("StandbyHrs", 18);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("Total", 19);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnplannedDertionHrs", 20);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationHrs", 21);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GasDerationHrs", 22);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("AmbDerationHrs", 23);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampHrs", 24);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampHrs", 25);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalT", 26);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageMW", 27);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedOutageMW", 28);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMW", 34);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("StandbyMW", 29);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalH", 30);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampMW", 31);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampMW", 32);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedDerationMW", 33);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationMW", 34);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMW", 41);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GasDerationMW", 35);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("AmbDerationMW", 36);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalF", 37);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jsCol = jsCol + "var AvailabilityPerc = (((PH - PlannedOutageHrs - UnPlannedOutageHrs) / PH) * 100);\n";
            jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            jsCol = jsCol + "var EUDH = (ForcedDerationMW/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "39" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            jsCol = jsCol + "var EPDH = (PlannedDerationMW/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "40" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            jsCol = jsCol + "var ELRExclGrid = ((EUDH+UnPlannedOutageHrs)/(UnPlannedOutageHrs+(PH-PlannedOutageHrs-UnPlannedOutageHrs-StandbyHrs-UnPlannedOutageHrsGrid)));";
            jsFoot = jsFoot + "$(api.column(" + "41" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var ELR= (EUDH+UnPlannedOutageHrs+UnPlannedOutageHrsGrid)/(UnPlannedOutageHrs+UnPlannedOutageHrsGrid+(PH-PlannedOutageHrs-UnPlannedOutageHrs-UnPlannedOutageHrsGrid-StandbyHrs));";
            jsFoot = jsFoot + "$(api.column(" + "42" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            js = js + jsCol;
            js = js + jsFoot + "}";

            vmodel.JSfootercallback = js;

            return vmodel;
        }

        private ReportsFormViewModel getELRH6Format(ReportInputParams input) //Mainly used for BQPS-I ELR Report
        {
            string UnitId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            vmodel.data = _context.VRPTHourlyUnitwiseReg1CFinal.Where(x => x.UnitId == UnitId && DbFunctions.TruncateTime(x.CurrRdgDateTime) >= dtFrom && DbFunctions.TruncateTime(x.CurrRdgDateTime) <= dtTo).ToList();
            vmodel.cols = new List<ReportColumnsDef> {
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Reading<br/>DateTime", type="DateMinutes" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
                new ReportColumnsDef{ data= "ReferenceCapacity" , title= "Ref<br/>Cap." },
                new ReportColumnsDef{ data= "ActualDispatchMW" , title= "Actual<br/>Dispatch<br/>(MW)" },
                //new ReportColumnsDef{ data= "AmbTemp" , title= "Ambient<br/>Temp<br/>(Deg. F)" },
                //new ReportColumnsDef{ data= "SeaWaterTemp" , title= "SeaWater<br/>Temp<br/>(Deg. F)" },
                //new ReportColumnsDef{ data= "AmbPressure" , title= "Ambient<br/>Pressure<br/>(mBar)" },
                new ReportColumnsDef{ data= "CurrIsOutageDeration" , title= "Outage<br/>Deration" },
                new ReportColumnsDef{ data= "CurrIsForced" , title= "Forced<br/>ForcedGrid" },
                new ReportColumnsDef{ data= "CurrIsPlanned" , title= "Planned" },
                new ReportColumnsDef{ data= "CurrIsLDC" , title= "LDC"},
                //new ReportColumnsDef{ data= "CurrIsGas" , title= "Gas" },
                new ReportColumnsDef{ data= "CurrStartupRampId" , title= "Startup<br/>Ramp" },
                new ReportColumnsDef{ data= "Reason" , title= "Reason" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                //new ReportColumnsDef{ data= "AmbDeration" , title= "Ambient<br/>Deration" },
                new ReportColumnsDef{ data= "ForcedDeration" , title= "Forced<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedDeration" , title= "Planned<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned<br/>OutageHrs<br/>" },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned<br/>Outage<br/>Hrs" },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned<br/>OutageHrs<br/>Grid" },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "OutageHrs<br/>LDC" },
                //new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby<br/>Hrs" },
                new ReportColumnsDef{ data= "Total1" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br/>Deration<br/>Hrs" },
                //new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC<br/>Deration<br/>Hrs" },
                //new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedRampHrs" , title= "Planned<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "ForcedRampHrs" , title= "Forced<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "Total2" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "LDCOutageMwh" , title= "LDC<br/>Outage<br/>Mwh" },
                //new ReportColumnsDef{ data= "StandbyMwh" , title= "Standby<br/>Mwh" },
                new ReportColumnsDef{ data= "Total3" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedRampMwh" , title= "Planned<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedRampMwh" , title= "Forced<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "LDCDerationMwh" , title= "LDC<br/>Deration<br/>Mwh" },
                //new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas<br/>Deration<br/>Mwh" },
                //new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "Total4" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "AvailabilityPercent" , title= "Availability<br/>Percent" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "ELRExclGrid" , title= "ELR<br/>ExclGrid" },
                new ReportColumnsDef{ data= "ELR",  title= "ELR" }
            };

            string js =
                "function(row, data, start, end, display) {\n" +
                "if(result.length ==0){\n" +
                "  return;\n" +
                "}\n" +
                "var api = this.api(), data;\n";
            string jsCol = "", jsFoot = "";

            jsCol = jsCol + "var " + "GDC" + " = api\n" +
                       "       .column(" + "1" + ")\n" +
                       "        .data()\n" +
                       "        .reduce ((a, b, index, array) => {\n" +
                       "a += b;\n" +
                       "if (index === array.length - 1)\n" +
                       "{\n" +
                       "return a / array.length;\n" +
                       "}\n" +
                       "else\n" +
                       "{\n" +
                       "return a;\n" +
                       "}\n" +
                       "});\n";
            jsCol = jsCol + "GDC = GDC.toFixed(2);";
            jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            jp = getJSColumnTotal("PH", 10);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedDeration", 11);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDeration", 12);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageHrs", 13);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnPlannedOutageHrs", 14);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnPlannedOutageHrsGrid", 15);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;
            /////////////
            jp = getJSColumnTotal("OutageHrsLDC", 16);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("Total", 17);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnplannedDertionHrs", 18);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationHrs", 19);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCDerationHrs", 20);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbDerationHrs", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampHrs", 21);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampHrs", 22);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalT", 23);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageMW", 24);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedOutageMW", 25);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCOutageMW", 26);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMW", 35);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalH", 27);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampMW", 28);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampMW", 29);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedDerationMW", 30);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationMW", 31);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCDerationMW", 32);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMW", 42);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbDerationMW", 43);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalF", 33);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jsCol = jsCol + "var AvailabilityPerc = (((PH - PlannedOutageHrs - UnPlannedOutageHrs) / PH) * 100);\n";
            jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            jsCol = jsCol + "var EUDH = (ForcedDerationMW/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            jsCol = jsCol + "var EPDH = (PlannedDerationMW/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            jsCol = jsCol + "var ELRExclGrid = ((EUDH+UnPlannedOutageHrs)/(UnPlannedOutageHrs+(PH-PlannedOutageHrs-UnPlannedOutageHrs-OutageHrsLDC-UnPlannedOutageHrsGrid)));";
            jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var ELR= (EUDH+UnPlannedOutageHrs+UnPlannedOutageHrsGrid)/(UnPlannedOutageHrs+UnPlannedOutageHrsGrid+(PH-PlannedOutageHrs-UnPlannedOutageHrs-UnPlannedOutageHrsGrid-OutageHrsLDC));";
            jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            js = js + jsCol;
            js = js + jsFoot + "}";

            vmodel.JSfootercallback = js;

            return vmodel;
        }

        private ReportsFormViewModel getELRH7Format(ReportInputParams input) //Mainly used for BQPS-II ST ELR Report
        {
            string UnitId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            vmodel.data = _context.VRPTHourlyUnitwiseReg1AFinal.Where(x => x.UnitId == UnitId && DbFunctions.TruncateTime(x.CurrRdgDateTime) >= dtFrom && DbFunctions.TruncateTime(x.CurrRdgDateTime) <= dtTo).ToList();
            vmodel.cols = new List<ReportColumnsDef> {
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Reading<br/>DateTime", type="DateMinutes" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
                new ReportColumnsDef{ data= "ReferenceCapacity" , title= "Ref<br/>Cap." },
                new ReportColumnsDef{ data= "ActualDispatchMW" , title= "Actual<br/>Dispatch<br/>(MW)" },
                //new ReportColumnsDef{ data= "AmbTemp" , title= "Ambient<br/>Temp<br/>(Deg. F)" },
                //new ReportColumnsDef{ data= "SeaWaterTemp" , title= "SeaWater<br/>Temp<br/>(Deg. F)" },
                //new ReportColumnsDef{ data= "AmbPressure" , title= "Ambient<br/>Pressure<br/>(mBar)" },
                new ReportColumnsDef{ data= "CurrIsOutageDeration" , title= "Outage<br/>Deration" },
                new ReportColumnsDef{ data= "CurrIsForced" , title= "Forced<br/>ForcedGrid" },
                new ReportColumnsDef{ data= "CurrIsPlanned" , title= "Planned" },
                new ReportColumnsDef{ data= "CurrIsLDC" , title= "LDC"},
                new ReportColumnsDef{ data= "CurrIsGas" , title= "Gas" },
                new ReportColumnsDef{ data= "CurrStartupRampId" , title= "Startup<br/>Ramp" },
                new ReportColumnsDef{ data= "Reason" , title= "Reason" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                //new ReportColumnsDef{ data= "AmbDeration" , title= "Ambient<br/>Deration" },
                new ReportColumnsDef{ data= "ForcedDeration" , title= "Forced<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedDeration" , title= "Planned<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned<br/>OutageHrs<br/>" },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned<br/>Outage<br/>Hrs" },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned<br/>OutageHrs<br/>Grid" },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "OutageHrs<br/>LDC" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby<br/>Hrs" },
                new ReportColumnsDef{ data= "Total1" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC<br/>Deration<br/>Hrs" },
                //new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedRampHrs" , title= "Planned<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "ForcedRampHrs" , title= "Forced<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "Total2" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "LDCOutageMwh" , title= "LDC<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "StandbyMwh" , title= "Standby<br/>Mwh" },
                new ReportColumnsDef{ data= "Total3" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedRampMwh" , title= "Planned<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedRampMwh" , title= "Forced<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "LDCDerationMwh" , title= "LDC<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas<br/>Deration<br/>Mwh" },
                //new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "Total4" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "AvailabilityPercent" , title= "Availability<br/>Percent" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "ELRExclGrid" , title= "ELR<br/>ExclGrid" },
                new ReportColumnsDef{ data= "ELR",  title= "ELR" }
            };

            string js =
                "function(row, data, start, end, display) {\n" +
                "if(result.length ==0){\n" +
                "  return;\n" +
                "}\n" +
                "var api = this.api(), data;\n";
            string jsCol = "", jsFoot = "";

            jsCol = jsCol + "var " + "GDC" + " = api\n" +
                       "       .column(" + "1" + ")\n" +
                       "        .data()\n" +
                       "        .reduce ((a, b, index, array) => {\n" +
                       "a += b;\n" +
                       "if (index === array.length - 1)\n" +
                       "{\n" +
                       "return a / array.length;\n" +
                       "}\n" +
                       "else\n" +
                       "{\n" +
                       "return a;\n" +
                       "}\n" +
                       "});\n";
            jsCol = jsCol + "GDC = GDC.toFixed(2);";
            jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            jp = getJSColumnTotal("PH", 11);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageHrs", 14);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnPlannedOutageHrs", 15);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnPlannedOutageHrsGrid", 16);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;
            /////////////
            jp = getJSColumnTotal("OutageHrsLDC", 17);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("StandbyHrs", 18);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("Total", 19);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnplannedDertionHrs", 20);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationHrs", 21);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GasDerationHrs", 22);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCDerationHrs", 23);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbDerationHrs", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampHrs", 24);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampHrs", 25);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalT", 26);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageMW", 27);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedOutageMW", 28);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCOutageMW", 29);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("StandbyMW", 30);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalH", 31);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampMW", 32);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampMW", 33);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedDerationMW", 34);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationMW", 35);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCDerationMW", 36);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GasDerationMW", 37);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbDerationMW", 43);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalF", 38);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jsCol = jsCol + "var AvailabilityPerc = (((PH - PlannedOutageHrs - UnPlannedOutageHrs) / PH) * 100);\n";
            jsFoot = jsFoot + "$(api.column(" + "39" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            jsCol = jsCol + "var EUDH = (ForcedDerationMW/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "40" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            jsCol = jsCol + "var EPDH = (PlannedDerationMW/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "41" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            jsCol = jsCol + "var ELRExclGrid = ((EUDH+UnPlannedOutageHrs)/(UnPlannedOutageHrs+(PH-PlannedOutageHrs-UnPlannedOutageHrs-StandbyHrs-OutageHrsLDC)));";
            jsFoot = jsFoot + "$(api.column(" + "42" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var ELR= (EUDH+UnPlannedOutageHrs+UnPlannedOutageHrsGrid)/(UnPlannedOutageHrs+UnPlannedOutageHrsGrid+(PH-PlannedOutageHrs-UnPlannedOutageHrs-UnPlannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            jsFoot = jsFoot + "$(api.column(" + "43" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            js = js + jsCol;
            js = js + jsFoot + "}";

            vmodel.JSfootercallback = js;

            return vmodel;
        }
        private ReportsFormViewModel getELRD1Format(ReportInputParams input) //Mainly used for KPC Daily Report
        {
            string UnitId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDailyUnitwiseFinals.Where(x => x.Unit == UnitId && x.theDay >= dtFrom && x.theDay <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "theDay" , title= "Date", type="DateDay" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
                new ReportColumnsDef{ data= "ReferenceCapacity" , title= "Ref<br/>Cap." },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "AmbDeration" , title= "Ambient<br/>Deration" },
                new ReportColumnsDef{ data= "ForcedDeration" , title= "Forced<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedDeration" , title= "Planned<br/>Deration" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned<br/>OutageHrs<br/>" },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned<br/>Outage<br/>Hrs" },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned<br/>OutageHrs<br/>Grid" },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "OutageHrs<br/>LDC" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby<br/>Hrs" },
                new ReportColumnsDef{ data= "Total1" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient<br/>Deration<br/>Hrs" },
                new ReportColumnsDef{ data= "PlannedRampHrs" , title= "Planned<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "ForcedRampHrs" , title= "Forced<br/>Ramp<br/>Hrs" },
                new ReportColumnsDef{ data= "Total2" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "LDCOutageMwh" , title= "LDC<br/>Outage<br/>Mwh" },
                new ReportColumnsDef{ data= "StandbyMwh" , title= "Standby<br/>Mwh" },
                new ReportColumnsDef{ data= "Total3" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "PlannedRampMwh" , title= "Planned<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedRampMwh" , title= "Forced<br/>Ramp<br/>Mwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "LDCDerationMwh" , title= "LDC<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient<br/>Deration<br/>Mwh" },
                new ReportColumnsDef{ data= "Total4" , title= "Total", @class="total" },
                new ReportColumnsDef{ data= "AvailabilityPercent" , title= "Availability<br/>Percent" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "ELRExclGrid" , title= "ELR<br/>ExclGrid" },
                new ReportColumnsDef{ data= "ELR",  title= "ELR" }
#endregion
            };

            string js =
                "function(row, data, start, end, display) {\n" +
                "var api = this.api(), data;\n";
            string jsCol = "", jsFoot = "";

            jsCol = jsCol + "var " + "GDC" + " = api\n" +
                       "       .column(" + "1" + ")\n" +
                       "        .data()\n" +
                       "        .reduce ((a, b, index, array) => {\n" +
                       "a += b;\n" +
                       "if (index === array.length - 1)\n" +
                       "{\n" +
                       "return a / array.length;\n" +
                       "}\n" +
                       "else\n" +
                       "{\n" +
                       "return a;\n" +
                       "}\n" +
                       "});\n";
            jsCol = jsCol + "GDC = GDC.toFixed(2);";
            jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            jp = getJSColumnTotal("PH", 3);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("AmbientDeration", 4);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedDeration", 5);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDeration", 6);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;
            /////////////
            jp = getJSColumnTotal("PlannedOutageHrs", 7);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("OutageHrsLDC", 10);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("StandbyHrs", 11);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("Total1", 12);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationHrs", 14);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GasDerationHrs", 15);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCDerationHrs", 16);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("AmbientDerationHrs", 17);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampHrs", 18);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampHrs", 19);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("Total2", 20);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageMwh", 21);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedOutageMwh", 22);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCOutageMwh", 23);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("StandbyMwh", 24);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("Total3", 25);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedRampMwh", 26);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedRampMwh", 27);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ForcedDerationMwh", 28);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedDerationMwh", 29);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCDerationMwh", 30);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GasDerationMwh", 31);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("AmbientDerationMwh", 32);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;


            jp = getJSColumnTotal("Total4", 33);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            js = js + jsCol;
            js = js + jsFoot + "}";

            vmodel.JSfootercallback = js;

            return vmodel;
        }


        #region ELRDailyKGTPS UnitWise
        private ReportsFormViewModel getELRDailyKGTPS_UnitWise(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDaily_KGTPS_Final.Where(x => x.UnitId == UnitId && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },
                //new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours" },
                
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                //new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                //new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },
                //new ReportColumnsDef{ data= "LDCOutageHrs" , title= "OutageHrs<br/>LDC" },
                
                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },
                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion

        #region ELRDailyKGTPS UnitWise_Station1
        private ReportsFormViewModel getELRDailyKGTPS_UnitWiseStation1(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDaily_KGTPS_Final.Where(x => x.UnitId == UnitId && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },
                
                
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },
               
                
                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },
                new ReportColumnsDef{ data= "MaxLoad" , title= "Maximum</br>Load." },

                new ReportColumnsDef{ data= "MinLoad" , title= "Minimum</br>Load." },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned</br>DerationHrs." },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas</br>DerationHrs." },
                new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient</br>DerationHrs." },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>DerationHrs." },

                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas</br>DerationMwh." },
                new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient</br>DerationMwh." },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh." },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh." },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned</br>OutageMwh." },

                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced</br>OutageMwh." },
                new ReportColumnsDef{ data= "StandbyMwh" , title= "Standby</br>Mwh." },
                new ReportColumnsDef{ data= "GDC" , title= "GDC." }
                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion

        #region ELRDailyKGTPS Station
        private ReportsFormViewModel getELRDailyKGTPS_Station(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDaily_KGTPS_Station_Final.Where(x => x.SiteId == UnitId && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                new ReportColumnsDef{ data= "SiteName" , title= "Site</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },
                //new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours" },
                
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },
                //new ReportColumnsDef{ data= "LDCOutageHrs" , title= "OutageHrs<br/>LDC" },
                
                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },


                   new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned</br>DerationHrs." },
                      new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas</br>DerationHrs." },
                         new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient</br>DerationHrs." },
                            new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>DerationHrs." },
                               new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas</br>DerationMwh." },

                                  new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient</br>DerationMwh." },
                                     new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh." },
                                        new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh." }

                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion



        #region ELRMonthlyKGTPS UnitWise
        private ReportsFormViewModel getELRMontlyKGTPS_UnitWise(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTMonthly_KGTPS_UnitWiseFinal.Where(x => x.SiteId == UnitId && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date"},
                new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },
                
               
                
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },
            
                
                
                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },
                new ReportColumnsDef{ data= "MaxLoad" , title= "Maximum</br>Load." },

                new ReportColumnsDef{ data= "MinLoad" , title= "Minimum</br>Load." },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned</br>DerationHrs." },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas</br>DerationHrs." },
                new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient</br>DerationHrs." },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>DerationHrs." },
                
                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas</br>DerationMwh." },
                new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient</br>DerationMwh." },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh." },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh." },
                new ReportColumnsDef{ data= "GDC" , title= "GDC." },
                
                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion


        #region ELRMontlyKGTPS Station
        private ReportsFormViewModel getELRMonthlyKGTPS_Station(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTMonthly_KGTPS_StationFinal.Where(x => x.SiteId == UnitId && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                  #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date" },
                new ReportColumnsDef{ data= "SiteName" , title= "Site</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },



                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },



                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },
                
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned</br>DerationHrs." },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas</br>DerationHrs." },
                new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient</br>DerationHrs." },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>DerationHrs." },

                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas</br>DerationMwh." },
                new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient</br>DerationMwh." },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh." },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh." }
               
                
                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion



        #region ELRDailySGTPS UnitWise
        private ReportsFormViewModel getELRDailySGTPS_UnitWise(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            if(UnitId == "SGT-SC1")
            {
                UnitId = "SGT-ST1";
            }
            else if (UnitId == "SGT-SC2")
            {
                UnitId = "SGT-ST2";
            }
            else if (UnitId == "SGT-SC3")
            {
                UnitId = "SGT-ST3";
            }
            else if (UnitId == "SGT-SC4")
            {
                UnitId = "SGT-ST4";
            }

            vmodel.data = _context.VRPTDaily_SGTPS_Final.Where(x => x.UnitId == UnitId && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },
                //new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours" },
                
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                //new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                //new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },
                //new ReportColumnsDef{ data= "LDCOutageHrs" , title= "OutageHrs<br/>LDC" },
                
                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },
                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion
        #region ELRDailySGTPS UnitWise_Station1
        private ReportsFormViewModel getELRDailySGTPS_UnitWiseStation1(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDaily_SGTPS_Final.Where(x => x.UnitId == UnitId && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },


                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },


                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },
                new ReportColumnsDef{ data= "MaxLoad" , title= "Maximum</br>Load." },

                new ReportColumnsDef{ data= "MinLoad" , title= "Minimum</br>Load." },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned</br>DerationHrs." },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas</br>DerationHrs." },
                new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient</br>DerationHrs." },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>DerationHrs." },

                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas</br>DerationMwh." },
                new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient</br>DerationMwh." },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh." },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh." },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned</br>OutageMwh." },

                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced</br>OutageMwh." },
                new ReportColumnsDef{ data= "StandbyMwh" , title= "Standby</br>Mwh." },
                new ReportColumnsDef{ data= "GDC" , title= "GDC." }
                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion


        #region ELRDailySGTPS Station
        private ReportsFormViewModel getELRDailySGTPS_Station(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDaily_SGTPS_Station_Final.Where(x => x.SiteId == UnitId && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                new ReportColumnsDef{ data= "SiteName" , title= "Site</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },
                //new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours" },
                
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },
                //new ReportColumnsDef{ data= "LDCOutageHrs" , title= "OutageHrs<br/>LDC" },
                
                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },


                   new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned</br>DerationHrs." },
                      new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas</br>DerationHrs." },
                         new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient</br>DerationHrs." },
                            new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>DerationHrs." },
                               new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas</br>DerationMwh." },

                                  new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient</br>DerationMwh." },
                                     new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh." },
                                        new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh." }

                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion

        #region ELRMonthlySGTPS UnitWise
        private ReportsFormViewModel getELRMontlySGTPS_UnitWise(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTMonthly_SGTPS_UnitWiseFinal.Where(x => x.SiteId == UnitId && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date" },
                new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },



                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },



                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },
                new ReportColumnsDef{ data= "MaxLoad" , title= "Maximum</br>Load." },

                new ReportColumnsDef{ data= "MinLoad" , title= "Minimum</br>Load." },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned</br>DerationHrs." },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas</br>DerationHrs." },
                new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient</br>DerationHrs." },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>DerationHrs." },

                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas</br>DerationMwh." },
                new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient</br>DerationMwh." },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh." },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh." },
                new ReportColumnsDef{ data= "GDC" , title= "GDC." }
                
                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion


        #region ELRMontlySGTPS Station
        private ReportsFormViewModel getELRMonthlySGTPS_Station(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTMonthly_SGTPS_StationFinal.Where(x => x.SiteId == UnitId && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                  #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date" },
                new ReportColumnsDef{ data= "SiteName" , title= "Site</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },

                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby</br>Hours" },



                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Exl_Grid" , title= "ELR</br>Excl<br>Grid" },



                new ReportColumnsDef{ data= "ELR" , title= "Energy</br>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabiliyFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabiliyFactor" , title= "Energy</br>Availability</br>Factor." },
                new ReportColumnsDef{ data= "ReliabiliyFactor" , title= "Reliability</br>Factor" },

                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned</br>DerationHrs." },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas</br>DerationHrs." },
                new ReportColumnsDef{ data= "AmbDerationHrs" , title= "Ambient</br>DerationHrs." },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>DerationHrs." },

                new ReportColumnsDef{ data= "GasDerationMwh" , title= "Gas</br>DerationMwh." },
                new ReportColumnsDef{ data= "AmbDerationMwh" , title= "Ambient</br>DerationMwh." },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh." },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh." }
               
                
                
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion


        #region ELRDailyBQPS1 UnitWise
        /// <summary>
        /// Report created by Moiz 17-12-2019
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 

        private ReportsFormViewModel getELRDailyBQPS1(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDailyUnitwiseReg1CFinal.Where(x => x.UnitId == UnitId && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELRExclGrid" , title= "ELR</br>Excl<br>Grid" },
           
                new ReportColumnsDef{ data= "EnergyLossRate" , title= "Energy<br/>Loss</br>Rate." },
                new ReportColumnsDef{ data= "AvailabilityFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabilityFactor_IncludingGrid" , title= "Energy</br>Availability</br>Factor</br>Including</br>Grid" },
                new ReportColumnsDef{ data= "ReliabilityFactor_IncludingGrid" , title= "Reliability</br>Factor<br>Including</br>Grid" },
                new ReportColumnsDef{ data= "MaxLoad" , title= "MaxLoad" },
                new ReportColumnsDef{ data= "MinLoad" , title= "MinLoad" },
                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC</br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "LDCDerationMwh" , title= "LDC</br>DerationMwh" },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned</br>OutageMwh" },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced</br>OutageMwh" },
                new ReportColumnsDef{ data= "LDCOutageMwh" , title= "LDC</br>OutageMwh" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
               
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }

        #endregion



        #region ELRDailyBQPS2 UnitWise_GT
        /// <summary>
        /// Report created by Moiz 17-12-2019
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 

        private ReportsFormViewModel getELRDailyBQPS2_GT(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDailyUnitwiseReg3Final.Where(x => x.UnitId == UnitId && x.RdgDateTime >= dtFrom && x.RdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "RdgDateTime" , title= "Date", type="DateDay" },
                //new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHrs" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "unplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours" },
                 new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby </br>Hours" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDU" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Excl_Grid" , title= "ELR</br>Excl<br>Grid" },
                new ReportColumnsDef{ data= "ELR" , title= "ELR" },
                new ReportColumnsDef{ data= "AvailabilityFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabilityFactor" , title= "Energy</br>Availability</br>Factor" },
                new ReportColumnsDef{ data= "ReliabilityFactor" , title= "Reliability</br>Factor" },
                new ReportColumnsDef{ data= "MaxLoad" , title= "MaxLoad" },
                new ReportColumnsDef{ data= "MinLoad" , title= "MinLoad" },
                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC</br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title= "Gas</br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title=  "UnPlanned<br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
               
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }

        #endregion


        #region ELRDailyBQPS2 UnitWise_ST
        /// <summary>
        /// Report created by Moiz 17-12-2019
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 

        private ReportsFormViewModel getELRDailyBQPS2_ST(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDailyUnitwiseReg1AFinal.Where(x => x.UnitId == UnitId && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                //new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHrs" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },

                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid." },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby </br>Hours" },

                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Excl_Grid" , title= "ELR</br>Excl<br>Grid" },
                new ReportColumnsDef{ data= "ELR" , title= "ELR" },

                new ReportColumnsDef{ data= "AvailabilityFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabilityFactor" , title= "Energy</br>Availability</br>Factor" },
                new ReportColumnsDef{ data= "ReliabilityFactor" , title= "Reliability</br>Factor" },
                new ReportColumnsDef{ data= "MaxLoad" , title= "MaxLoad" },
                new ReportColumnsDef{ data= "MinLoad" , title= "MinLoad" },

                new ReportColumnsDef{ data= "LDCDerationHrs" , title=  "LDC</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title=  "Planned</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title=  "Gas</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title=  "UnPlanned</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "LDCDerationMwh" , title=  "LDC</br>Deration</br>Mwh" },

                new ReportColumnsDef{ data= "PlannedDerationMwh" , title=  "Planned</br>Deration</br>Mwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title=  "Forced</br>Deration</br>Mwh" },
                new ReportColumnsDef{ data= "GasDerationMwh" , title=  "Gas</br>Deration</br>Mwh" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title=  "Planned<br>Outage</br>Mwh." },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title=  "Forced<br>Outage</br>Mwh." },

                new ReportColumnsDef{ data= "LDCOutageMwh" , title=  "LDC<br>Outage</br>Mwh." },
                new ReportColumnsDef{ data= "StandbyMwh" , title=  "Standby</br>Mwh" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
               
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }

        #endregion



        #region ELRDailyBQPS2 Statiowise
        /// <summary>
        /// Report created by Moiz 17-12-2019
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 

        private ReportsFormViewModel getELRDailyBQPS2_StationWise(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDailyBQPS2_StationwiseFinal.Where(x => x.SiteId == UnitId && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                //new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHrs" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },

                new ReportColumnsDef{ data= "unplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid." },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby </br>Hours" },

                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Excl_Grid" , title= "ELR</br>Excl<br>Grid" },
                new ReportColumnsDef{ data= "ELR" , title= "ELR" },

                new ReportColumnsDef{ data= "AvailabilityFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabilityFactor" , title= "Energy</br>Availability</br>Factor" },
                new ReportColumnsDef{ data= "ReliabilityFactor" , title= "Reliability</br>Factor" },
                //new ReportColumnsDef{ data= "MaxLoad" , title= "MaxLoad" },
                //new ReportColumnsDef{ data= "MinLoad" , title= "MinLoad" },

                new ReportColumnsDef{ data= "LDCDerationHrs" , title=  "LDC</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title=  "Planned</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title=  "Gas</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title=  "UnPlanned</br>Deration</br>Hrs" },
                //new ReportColumnsDef{ data= "LDCDerationMwh" , title=  "LDC</br>Deration</br>Mwh" },

                //new ReportColumnsDef{ data= "PlannedDerationMwh" , title=  "Planned</br>Deration</br>Mwh" },
                //new ReportColumnsDef{ data= "ForcedDerationMwh" , title=  "Forced</br>Deration</br>Mwh" },
                //new ReportColumnsDef{ data= "GasDerationMwh" , title=  "Gas</br>Deration</br>Mwh" },
                //new ReportColumnsDef{ data= "PlannedOutageMwh" , title=  "Planned<br>Outage</br>Mwh." },
                //new ReportColumnsDef{ data= "ForcedOutageMwh" , title=  "Forced<br>Outage</br>Mwh." },

                //new ReportColumnsDef{ data= "LDCOutageMwh" , title=  "LDC<br>Outage</br>Mwh." },
                //new ReportColumnsDef{ data= "StandbyMwh" , title=  "Standby</br>Mwh" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
               
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }

        #endregion




        #region ELRMonthlyBQPS2 UnitWise
        /// <summary>
        /// Report created by Moiz 17-12-2019
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 

        private ReportsFormViewModel getELRMonthlyBQPS2_UnitWise(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTMonthlyUnitwiseBQPS2_Final.Where(x => x.SiteId == UnitId && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date" },
                new ReportColumnsDef{ data= "UnitId" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHrs" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },

                new ReportColumnsDef{ data= "unplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid." },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby </br>Hours" },

                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Excl_Grid" , title= "ELR</br>Excl<br>Grid" },
                new ReportColumnsDef{ data= "ELR" , title= "ELR" },

                new ReportColumnsDef{ data= "AvailabilityFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabilityFactor" , title= "Energy</br>Availability</br>Factor" },
                new ReportColumnsDef{ data= "ReliabilityFactor" , title= "Reliability</br>Factor" },
                new ReportColumnsDef{ data= "MaxLoad" , title= "MaxLoad" },
                new ReportColumnsDef{ data= "MinLoad" , title= "MinLoad" },

                new ReportColumnsDef{ data= "LDCDerationHrs" , title=  "LDC</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title=  "Planned</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title=  "Gas</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title=  "UnPlanned</br>Deration</br>Hrs" },
                //new ReportColumnsDef{ data= "LDCDerationMwh" , title=  "LDC</br>Deration</br>Mwh" },

                //new ReportColumnsDef{ data= "PlannedDerationMwh" , title=  "Planned</br>Deration</br>Mwh" },
                //new ReportColumnsDef{ data= "ForcedDerationMwh" , title=  "Forced</br>Deration</br>Mwh" },
                //new ReportColumnsDef{ data= "GasDerationMwh" , title=  "Gas</br>Deration</br>Mwh" },
                //new ReportColumnsDef{ data= "PlannedOutageMwh" , title=  "Planned<br>Outage</br>Mwh." },
                //new ReportColumnsDef{ data= "ForcedOutageMwh" , title=  "Forced<br>Outage</br>Mwh." },

                //new ReportColumnsDef{ data= "LDCOutageMwh" , title=  "LDC<br>Outage</br>Mwh." },
                //new ReportColumnsDef{ data= "StandbyMwh" , title=  "Standby</br>Mwh" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
               
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }

        #endregion


        #region ELRMonthlyBQPS2 StationWise
        /// <summary>
        /// Report created by Moiz 17-12-2019
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 

        private ReportsFormViewModel getELRMonthlyBQPS2_StationWise(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTMonthlySitewiseBQPS2_Final.Where(x => x.SiteId == UnitId && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date"},
                //new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHrs" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },

                new ReportColumnsDef{ data= "unplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid." },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC </br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "StandbyHrs" , title= "Standby </br>Hours" },

                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELR_Excl_Grid" , title= "ELR</br>Excl<br>Grid" },
                new ReportColumnsDef{ data= "ELR" , title= "ELR" },

                new ReportColumnsDef{ data= "AvailabilityFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabilityFactor" , title= "Energy</br>Availability</br>Factor" },
                new ReportColumnsDef{ data= "ReliabilityFactor" , title= "Reliability</br>Factor" },
                //new ReportColumnsDef{ data= "MaxLoad" , title= "MaxLoad" },
                //new ReportColumnsDef{ data= "MinLoad" , title= "MinLoad" },

                new ReportColumnsDef{ data= "LDCDerationHrs" , title=  "LDC</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title=  "Planned</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "GasDerationHrs" , title=  "Gas</br>Deration</br>Hrs" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title=  "UnPlanned</br>Deration</br>Hrs" },
                //new ReportColumnsDef{ data= "LDCDerationMwh" , title=  "LDC</br>Deration</br>Mwh" },

                //new ReportColumnsDef{ data= "PlannedDerationMwh" , title=  "Planned</br>Deration</br>Mwh" },
                //new ReportColumnsDef{ data= "ForcedDerationMwh" , title=  "Forced</br>Deration</br>Mwh" },
                //new ReportColumnsDef{ data= "GasDerationMwh" , title=  "Gas</br>Deration</br>Mwh" },
                //new ReportColumnsDef{ data= "PlannedOutageMwh" , title=  "Planned<br>Outage</br>Mwh." },
                //new ReportColumnsDef{ data= "ForcedOutageMwh" , title=  "Forced<br>Outage</br>Mwh." },

                //new ReportColumnsDef{ data= "LDCOutageMwh" , title=  "LDC<br>Outage</br>Mwh." },
                //new ReportColumnsDef{ data= "StandbyMwh" , title=  "Standby</br>Mwh" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
               
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }

        #endregion



        #region ELRDailyBQPS1 SiteWise

        private ReportsFormViewModel getELRDailyBQPS1SiteWise(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTDailySitewiseReg1CFinal.Where(x => x.SiteId == CommDef.SITE_KEY_BQ1 && x.CurrRdgDateTime >= dtFrom && x.CurrRdgDateTime <= dtTo).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Date", type="DateDay" },
                new ReportColumnsDef{ data= "SiteName" , title= "Site</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },
                
                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                 new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELRExclGrid" , title= "ELR</br>Excl<br>Grid" },
                new ReportColumnsDef{ data= "EnergyLossRate" , title= "Energy<br/>Loss<br/>Rate." },
                new ReportColumnsDef{ data= "AvailabilityFactor" , title= "Availability</br>Factor" },
              
                new ReportColumnsDef{ data= "EnergyAvailabilityFactor_IncludingGrid" , title= "Energy</br>Availability</br>Factor</br>Including</br>Grid" },
                new ReportColumnsDef{ data= "ReliabilityFactor_IncludingGrid" , title= "Reliability</br>Factor<br>Including</br>Grid" },
                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC</br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>Deration</br>Hours" },
                
                new ReportColumnsDef{ data= "LDCDerationMwh" , title= "LDC</br>DerationMwh" },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned</br>OutageMwh" },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced</br>OutageMwh" },
                
                new ReportColumnsDef{ data= "LDCOutageMwh" , title= "LDC</br>OutageMwh" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
               
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion



        #region ELRMonthlyBQPS1 UnitWise
        private ReportsFormViewModel getELRMonthlyBQPS1(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTMonthlyUnitwiseReg1CFinal.Where(x => x.SiteId == input.Id && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Month" },
                new ReportColumnsDef{ data= "UnitName" , title= "Unit</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELRExclGrid" , title= "ELR</br>Excl<br>Grid" },
                
                new ReportColumnsDef{ data= "EnergyLossRate" , title= "Energy<br/>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabilityFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabilityFactor_IncludingGrid" , title= "Energy</br>Availability</br>Factor</br>Including</br>Grid" },
                new ReportColumnsDef{ data= "ReliabilityFactor_IncludingGrid" , title= "Reliability</br>Factor<br>Including</br>Grid" },
                new ReportColumnsDef{ data= "MaxLoad" , title= "MaxLoad" },
                new ReportColumnsDef{ data= "MinLoad" , title= "MinLoad" },
                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC</br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "LDCDerationMwh" , title= "LDC</br>DerationMwh" },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned</br>OutageMwh" },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced</br>OutageMwh" },
                new ReportColumnsDef{ data= "LDCOutageMwh" , title= "LDC</br>OutageMwh" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
               
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion


        #region  ELRMonthlyBQPS1 SiteWise
        private ReportsFormViewModel getELRMonthlyBQPS1Site(ReportInputParams input)
        {

            string UnitId;
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;

            UnitId = input.Id;

            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            //_context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            vmodel.data = _context.VRPTMonthlySitewiseReg1CFinal.Where(x => x.SiteId == CommDef.SITE_KEY_BQ1 && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();

            vmodel.cols = new List<ReportColumnsDef> {
                #region Col Def
                new ReportColumnsDef{ data= "CurrRdgDateTime" , title= "Month" },
                new ReportColumnsDef{ data= "SiteName" , title= "Site</br>Name." },
                new ReportColumnsDef{ data= "ServiceHours" , title= "Service</br>Hours." },
                new ReportColumnsDef{ data= "unplannedOutageNumber" , title= "UnPlanned</br>Outage Number." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_Grid" , title= "UnPlanned</br>Outage Number </br> Grid." },
                new ReportColumnsDef{ data= "unplannedOutageNumber_WithGrid" , title= "UnPlanned</br>Outage Number </br> With Grid." },
                new ReportColumnsDef{ data= "UnplannedOutageHrs" , title= "Unplanned</br>Outage</br>Hours." },
                new ReportColumnsDef{ data= "UnplannedOutageHrsGrid" , title= "Unplanned</br>Outage</br>Hours</br>Grid" },
                new ReportColumnsDef{ data= "PlannedOutageHrs" , title= "Planned</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "LDCOutageHrs" , title= "LDC</br>Outage</br>Hours" },
                new ReportColumnsDef{ data= "PH" , title= "PH" },
                new ReportColumnsDef{ data= "EPDH" , title= "EPDH" },
                new ReportColumnsDef{ data= "EUDH" , title= "EUDH" },
                new ReportColumnsDef{ data= "ELRExclGrid" , title= "ELR</br>Excl<br>Grid" },
               
                new ReportColumnsDef{ data= "EnergyLossRate" , title= "Energy<br/>Loss</br>Rate" },
                new ReportColumnsDef{ data= "AvailabilityFactor" , title= "Availability</br>Factor" },
                new ReportColumnsDef{ data= "EnergyAvailabilityFactor_IncludingGrid" , title= "Energy</br>Availability</br>Factor</br>Including</br>Grid" },
                new ReportColumnsDef{ data= "ReliabilityFactor_IncludingGrid" , title= "Reliability</br>Factor<br>Including</br>Grid" },

                new ReportColumnsDef{ data= "LDCDerationHrs" , title= "LDC</br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "PlannedDerationHrs" , title= "Planned<br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "UnplannedDerationHrs" , title= "Unplanned</br>Deration</br>Hours" },
                new ReportColumnsDef{ data= "LDCDerationMwh" , title= "LDC</br>DerationMwh" },
                new ReportColumnsDef{ data= "PlannedDerationMwh" , title= "Planned</br>DerationMwh" },
                new ReportColumnsDef{ data= "ForcedDerationMwh" , title= "Forced</br>DerationMwh" },
                new ReportColumnsDef{ data= "PlannedOutageMwh" , title= "Planned</br>OutageMwh" },
                new ReportColumnsDef{ data= "ForcedOutageMwh" , title= "Forced</br>OutageMwh" },
                new ReportColumnsDef{ data= "LDCOutageMwh" , title= "LDC</br>OutageMwh" },
                new ReportColumnsDef{ data= "GDC" , title= "GDC" },
               
#endregion
            };

            #region commectedJS_Footer

            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            //jsCol = jsCol + "var " + "GDC" + " = api\n" +
            //           "       .column(" + "1" + ")\n" +
            //           "        .data()\n" +
            //           "        .reduce ((a, b, index, array) => {\n" +
            //           "a += b;\n" +
            //           "if (index === array.length - 1)\n" +
            //           "{\n" +
            //           "return a / array.length;\n" +
            //           "}\n" +
            //           "else\n" +
            //           "{\n" +
            //           "return a;\n" +
            //           "}\n" +
            //           "});\n";
            //jsCol = jsCol + "GDC = GDC.toFixed(2);";
            //jsFoot = jsFoot + "$(api.column(" + "1" + ").footer()).html(" + "GDC" + ");\n";

            //jp = getJSColumnTotal("PH", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDeration", 4);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDeration", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDeration", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;
            ///////////////
            //jp = getJSColumnTotal("PlannedOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrsGrid", 9);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("OutageHrsLDC", 10);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 11);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total1", 12);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedDerationHrs", 13);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationHrs", 14);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationHrs", 15);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationHrs", 16);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationHrs", 17);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampHrs", 18);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampHrs", 19);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total2", 20);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageMwh", 21);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedOutageMwh", 22);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageMwh", 23);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyMwh", 24);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("Total3", 25);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedRampMwh", 26);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedRampMwh", 27);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("ForcedDerationMwh", 28);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedDerationMwh", 29);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCDerationMwh", 30);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("GasDerationMwh", 31);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("AmbientDerationMwh", 32);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;


            //jp = getJSColumnTotal("Total4", 33);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = ((PH-PlannedOutageHrs-UnplannedOutageHrs)/PH)*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "34" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var EUDH = (ForcedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "35" + ").footer()).html(" + "fnNumFormatter(EUDH)" + ");\n";

            //jsCol = jsCol + "var EPDH = (PlannedDerationMwh/GDC);";
            //jsFoot = jsFoot + "$(api.column(" + "36" + ").footer()).html(" + "fnNumFormatter(EPDH)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (EUDH+UnplannedOutageHrs)/(UnplannedOutageHrs+(PH-PlannedOutageHrs-UnplannedOutageHrs-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "37" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";

            //jsCol = jsCol + "var ELR= (EUDH+UnplannedOutageHrs+UnplannedOutageHrsGrid)/(UnplannedOutageHrs+UnplannedOutageHrsGrid+(PH-PlannedOutageHrs-UnplannedOutageHrs-UnplannedOutageHrsGrid-StandbyHrs-OutageHrsLDC));";
            //jsFoot = jsFoot + "$(api.column(" + "38" + ").footer()).html(" + "fnNumFormatter(ELR)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;

            #endregion

            return vmodel;


        }
        #endregion

        private ReportsFormViewModel getELRH2Format(ReportInputParams input)
        {
            string SiteId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            JSPair jp;
            string js;

            SiteId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);

            switch (SiteId)
            {
                case CommDef.SITE_KEY_KPC:
                    vmodel.data = _context.VRPTHourlySitewiseKPCFinals.Where(x => x.SiteId == CommDef.SITE_KEY_KPC && DbFunctions.TruncateTime(x.RdgDateTime) >= dtFrom && DbFunctions.TruncateTime(x.RdgDateTime) <= dtTo).ToList();
                    break;
                default:
                    break;
            }

            vmodel.cols = new List<Utility.ReportColumnsDef>
            {
                new ReportColumnsDef{data="RdgDateTime", title="DateTime", type="DateMinutes"},
                new ReportColumnsDef{data="GT1ActualDispatchMW", title="GT1<br>Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{data="GT2ActualDispatchMW", title="GT2<br>Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{data="GT3ActualDispatchMW", title="GT3<br>Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{data="GT4ActualDispatchMW", title="GT4<br>Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{data="ST1ActualDispatchMW", title="ST1<br>Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{data="ST2ActualDispatchMW", title="ST2<br>Actual<br>Dispatch<br>MW"},
                new ReportColumnsDef{data="StationDispatch", title="Station<br>Dispatch<br>MW"},
                new ReportColumnsDef{data="PH", title="PH"},
                new ReportColumnsDef{data="PlannedOutageHrs", title="Planned<br>Outage<br>Hrs"},
                new ReportColumnsDef{data="UnplannedOutageHrs", title="Unplanned<br>Outage<br>Hrs"},
                new ReportColumnsDef{data="AvailabilityPercent", title="Availability<br>Percent"},
                new ReportColumnsDef{data="EUDH", title="EUDH"},
                new ReportColumnsDef{data="EPDH", title="EPDH"},
                new ReportColumnsDef{data="LDCOutageHrs", title="LDCOutage<br>Hrs"},
                new ReportColumnsDef{data="StandbyHrs", title="Standby<br>Hrs"},
                new ReportColumnsDef{data="ELRExclGrid", title="ELR<br>ExclGrid"}
            };

            js = "function(row, data, start, end, display) {\n" +
                "if(result.length ==0){\n" +
                "  return;\n" +
                "}\n" +
                "var api = this.api(), data;\n";
            string jsCol = "", jsFoot = "";

            jp = getJSColumnTotal("GT1Disp", 1);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GT2Disp", 2);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GT3Disp", 3);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("GT4Disp", 4);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ST1Disp", 5);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("ST2Disp", 6);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("TotalDisp", 7);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PH", 8);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("PlannedOutageHrs", 9);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("UnplannedOutageHrs", 10);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("EUDH", 12);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("EPDH", 13);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("LDCOutageHrs", 14);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jp = getJSColumnTotal("StandbyHrs", 15);
            jsCol = jsCol + jp.JSCol;
            jsFoot = jsFoot + jp.JSFooter;

            jsCol = jsCol + "var AvailabilityPerc = (((PH*6)-PlannedOutageHrs-UnplannedOutageHrs)/(PH*6))*100;\n";
            jsFoot = jsFoot + "$(api.column(" + "11" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            jsCol = jsCol + "var ELRExclGrid=(UnplannedOutageHrs+EUDH)/(UnplannedOutageHrs+((PH*6)-PlannedOutageHrs-UnplannedOutageHrs-LDCOutageHrs-StandbyHrs));\n";
            jsFoot = jsFoot + "$(api.column(" + "16" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";


            js = js + jsCol;
            js = js + jsFoot + "}";
            vmodel.JSfootercallback = js;
            return vmodel;
        }

        #region ELRDailyKPC Station
        private ReportsFormViewModel getELRDailyKPC_Station(ReportInputParams input)
        {
            string SiteId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            //JSPair jp;
            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            SiteId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);



            vmodel.data = _context.VRPTDailyKPC_StationFinal.Where(x => x.SiteId == CommDef.SITE_KEY_KPC && DbFunctions.TruncateTime(x.CurrRdgDateTime) >= dtFrom && DbFunctions.TruncateTime(x.CurrRdgDateTime) <= dtTo).ToList();
            vmodel.cols = new List<Utility.ReportColumnsDef>
                {
                    new ReportColumnsDef{data="CurrRdgDateTime", title="Date", type="DateDay" },
                    new ReportColumnsDef{data="ServiceHours", title="Service</br>Hours."},
                    new ReportColumnsDef{data="UnplannedOutageNumber", title="unplannedOutageNumber."},
                    new ReportColumnsDef{data="unplannedOutageNumber_Grid", title="unplannedOutageNumber</br>Grid."},
                    new ReportColumnsDef{data="unplannedOutageNumber_WithGrid", title="unplannedOutageNumber</br>WithGrid."},

                    new ReportColumnsDef{data="UnplannedOutageHrs", title="unplanned</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="UnplannedOutageHrsGrid", title="Unplanned</br>Outage</br>Hours Grid."},
                    new ReportColumnsDef{data="PlannedOutageHrs", title="Planned</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="LDCOutageHrs", title="LDC</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="StandbyHrs", title="Standby</br>Hours."},

                    new ReportColumnsDef{data="PH", title="PH."},
                    new ReportColumnsDef{data="EPDH", title="EPDH."},
                    new ReportColumnsDef{data="EUDH", title="EUDH."},
                    new ReportColumnsDef{data="ELR", title="Energy<br>Loss Rate."},
                    new ReportColumnsDef{data="ELRExclGrid", title="ELR</br>ExcelGrid."},

                    new ReportColumnsDef{data="AvailabilityFactor", title="Availability</br>Factor."},
                    new ReportColumnsDef{data="EnergyAvailabilityFactor", title="Energy</br>Availability</br>Factor."},
                    new ReportColumnsDef{data="ReliabilityFactor", title="Reliability</br>Factor."},
                    //new ReportColumnsDef{data="ServiceHours", title="Service</br>Hours."},
                    //new ReportColumnsDef{data="ServiceHours", title="Service</br>Hours."},

                    new ReportColumnsDef{data="LDCDerationHrs", title="LDC</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="PlannedDerationHrs", title="Planned</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="GasDerationHrs", title="Gas</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="AmbDerationHrs", title="Ambient</br>Deration</br>Hourss."},
                    new ReportColumnsDef{data="UnplannedDerationHrs", title="Unplanned</br>Deration</br>Hours."},

                    new ReportColumnsDef{data="LDCDerationMwh", title="LDC</br>Deration</br>Mwhs."},
                    new ReportColumnsDef{data="GasDerationMwh", title="Gas</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="AmbDerationMwh", title="Ambient</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="PlannedDerationMwh", title="Planned</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="ForcedDerationMwh", title="Forced</br>Deration</br>Mwh."},

                    new ReportColumnsDef{data="PlannedOutageMwh", title="Planned</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="ForcedOutageMwh", title="Forced</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="LDCOutageMwh", title="LDCOutage</br>Mwh."},
                    new ReportColumnsDef{data="StandbyMwh", title="Standby</br>Mwh."},
                    new ReportColumnsDef{data="GDC", title="GDC."},



                };

            #region commented footer code

            //jp = getJSColumnTotal("PH", 1);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageHrs", 2);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("EUDH", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("EPDH", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = (((PH*6)-PlannedOutageHrs-UnplannedOutageHrs)/(PH*6))*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "4" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (UnplannedOutageHrs+EUDH)/(UnplannedOutageHrs+((PH*6)-PlannedOutageHrs-UnplannedOutageHrs-LDCOutageHrs-StandbyHrs));";
            //jsFoot = jsFoot + "$(api.column(" + "9" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;
            #endregion
            return vmodel;
        }

        #endregion


        #region ELRDailyKPC UnitWise
        private ReportsFormViewModel getELRDailyKPC_UnitWise(ReportInputParams input)
        {
            string SiteId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            //JSPair jp;
            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            SiteId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);



            vmodel.data = _context.VRPTDailyKPC_UnitWiseFinal.Where(x => x.UnitId == SiteId && DbFunctions.TruncateTime(x.CurrRdgDateTime) >= dtFrom && DbFunctions.TruncateTime(x.CurrRdgDateTime) <= dtTo).ToList();
            vmodel.cols = new List<Utility.ReportColumnsDef>
                {
                    new ReportColumnsDef{data="CurrRdgDateTime", title="Date", type="DateDay" },
                     new ReportColumnsDef{data="UnitId", title="Unit Name" },
                    new ReportColumnsDef{data="ServiceHours", title="Service</br>Hours."},
                    new ReportColumnsDef{data="UnplannedOutageNumber", title="unplannedOutageNumber."},
                    new ReportColumnsDef{data="unplannedOutageNumber_Grid", title="unplannedOutageNumber</br>Grid."},
                    new ReportColumnsDef{data="unplannedOutageNumber_WithGrid", title="unplannedOutageNumber</br>WithGrid."},

                    new ReportColumnsDef{data="UnplannedOutageHrs", title="unplanned</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="UnplannedOutageHrsGrid", title="Unplanned</br>Outage</br>Hours Grid."},
                    new ReportColumnsDef{data="PlannedOutageHrs", title="Planned</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="LDCOutageHrs", title="LDC</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="StandbyHrs", title="Standby</br>Hours."},

                    new ReportColumnsDef{data="PH", title="PH."},
                    new ReportColumnsDef{data="EPDH", title="EPDH."},
                    new ReportColumnsDef{data="EUDH", title="EUDH."},
                    new ReportColumnsDef{data="ELR", title="Energy<br>Loss Rate."},
                    new ReportColumnsDef{data="ELRExclGrid", title="ELR</br>ExcelGrid."},

                    new ReportColumnsDef{data="AvailabilityFactor", title="Availability</br>Factor."},
                    new ReportColumnsDef{data="EnergyAvailabilityFactor", title="Energy</br>Availability</br>Factor."},
                    new ReportColumnsDef{data="ReliabilityFactor", title="Reliability</br>Factor."},
                    new ReportColumnsDef{data="Maxload", title="Maximum</br>Load."},
                    new ReportColumnsDef{data="Minload", title="Minimum</br>load."},

                    new ReportColumnsDef{data="LDCDerationHrs", title="LDC</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="PlannedDerationHrs", title="Planned</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="GasDerationHrs", title="Gas</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="AmbDerationHrs", title="Ambient</br>Deration</br>Hourss."},
                    new ReportColumnsDef{data="UnplannedDerationHrs", title="Unplanned</br>Deration</br>Hours."},

                    new ReportColumnsDef{data="LDCDerationMwh", title="LDC</br>Deration</br>Mwhs."},
                    new ReportColumnsDef{data="GasDerationMwh", title="Gas</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="AmbDerationMwh", title="Ambient</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="PlannedDerationMwh", title="Planned</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="ForcedDerationMwh", title="Forced</br>Deration</br>Mwh."},

                    new ReportColumnsDef{data="PlannedOutageMwh", title="Planned</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="ForcedOutageMwh", title="Forced</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="LDCOutageMwh", title="LDC</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="StandbyMwh", title="Standby</br>Mwh."},
                    new ReportColumnsDef{data="GDC", title="GDC."},



                };


            #region commented footer code
            //jp = getJSColumnTotal("PH", 1);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageHrs", 2);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("EUDH", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("EPDH", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = (((PH*6)-PlannedOutageHrs-UnplannedOutageHrs)/(PH*6))*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "4" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (UnplannedOutageHrs+EUDH)/(UnplannedOutageHrs+((PH*6)-PlannedOutageHrs-UnplannedOutageHrs-LDCOutageHrs-StandbyHrs));";
            //jsFoot = jsFoot + "$(api.column(" + "9" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;
            #endregion
            return vmodel;
        }
        #endregion


        #region ELRMonthlyKPC UnitWise
        private ReportsFormViewModel getELRMonthlyKPC_UnitWise(ReportInputParams input)
        {
            string SiteId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            //JSPair jp;
            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            SiteId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);



            vmodel.data = _context.VRPTMonthlyKPC_UnitWiseFinal.Where(x => x.SiteId == CommDef.SITE_KEY_KPC && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();
            vmodel.cols = new List<Utility.ReportColumnsDef>
                {
                    new ReportColumnsDef{data="CurrRdgDateTime", title="Month" },
                    new ReportColumnsDef{data="UnitId", title="Unit Name" },
                    new ReportColumnsDef{data="ServiceHours", title="Service</br>Hours."},
                    new ReportColumnsDef{data="UnplannedOutageNumber", title="unplannedOutageNumber."},
                    new ReportColumnsDef{data="unplannedOutageNumber_Grid", title="unplannedOutageNumber</br>Grid."},
                    new ReportColumnsDef{data="unplannedOutageNumber_WithGrid", title="unplannedOutageNumber</br>WithGrid."},

                    new ReportColumnsDef{data="UnplannedOutageHrs", title="unplanned</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="UnplannedOutageHrsGrid", title="Unplanned</br>Outage</br>Hours Grid."},
                    new ReportColumnsDef{data="PlannedOutageHrs", title="Planned</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="LDCOutageHrs", title="LDC</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="StandbyHrs", title="Standby</br>Hours."},

                    new ReportColumnsDef{data="PH", title="PH."},
                    new ReportColumnsDef{data="EPDH", title="EPDH."},
                    new ReportColumnsDef{data="EUDH", title="EUDH."},
                    new ReportColumnsDef{data="ELR", title="Energy<br>Loss Rate."},
                    new ReportColumnsDef{data="ELRExclGrid", title="ELR</br>ExcelGrid."},

                    new ReportColumnsDef{data="AvailabilityFactor", title="Availability</br>Factor."},
                    new ReportColumnsDef{data="EnergyAvailabilityFactor", title="Energy</br>Availability</br>Factor."},
                    new ReportColumnsDef{data="ReliabilityFactor", title="Reliability</br>Factor."},
                    new ReportColumnsDef{data="Maxload", title="Maximum</br>Load."},
                    new ReportColumnsDef{data="Minload", title="Minimum</br>load."},

                    new ReportColumnsDef{data="LDCDerationHrs", title="LDC</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="PlannedDerationHrs", title="Planned</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="GasDerationHrs", title="Gas</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="AmbDerationHrs", title="Ambient</br>Deration</br>Hourss."},
                    new ReportColumnsDef{data="UnplannedDerationHrs", title="Unplanned</br>Deration</br>Hours."},

                    new ReportColumnsDef{data="LDCDerationMwh", title="LDC</br>Deration</br>Mwhs."},
                    new ReportColumnsDef{data="GasDerationMwh", title="Gas</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="AmbDerationMwh", title="Ambient</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="PlannedDerationMwh", title="Planned</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="ForcedDerationMwh", title="Forced</br>Deration</br>Mwh."},

                    new ReportColumnsDef{data="PlannedOutageMwh", title="Planned</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="ForcedOutageMwh", title="Forced</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="LDCOutageMwh", title="LDC</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="StandbyMwh", title="Standby</br>Mwh."},
                    new ReportColumnsDef{data="GDC", title="GDC."},



                };


            #region commented footer code
            //jp = getJSColumnTotal("PH", 1);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageHrs", 2);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("EUDH", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("EPDH", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = (((PH*6)-PlannedOutageHrs-UnplannedOutageHrs)/(PH*6))*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "4" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (UnplannedOutageHrs+EUDH)/(UnplannedOutageHrs+((PH*6)-PlannedOutageHrs-UnplannedOutageHrs-LDCOutageHrs-StandbyHrs));";
            //jsFoot = jsFoot + "$(api.column(" + "9" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;
            #endregion
            return vmodel;
        }
        #endregion

        #region ELRMonthlyKPC Station
        private ReportsFormViewModel getELRMonthlyKPC_Station(ReportInputParams input)
        {
            string SiteId;
            DateTime dtFrom, dtTo;
            ReportsFormViewModel vmodel = new ReportsFormViewModel();
            //JSPair jp;
            //string js =
            //    "function(row, data, start, end, display) {\n" +
            //    "var api = this.api(), data;\n";
            //string jsCol = "", jsFoot = "";

            SiteId = input.Id;
            dtFrom = DateTime.Parse(input.DateFrom);
            dtTo = DateTime.Parse(input.DateTo);



            vmodel.data = _context.VRPTMonthlyKPC_StationFinal.Where(x => x.SiteId == CommDef.SITE_KEY_KPC && x.CurrRdgDateTime >= dtFrom.Month && x.CurrRdgDateTime <= dtTo.Month).ToList();
            vmodel.cols = new List<Utility.ReportColumnsDef>
                {
                    new ReportColumnsDef{data="CurrRdgDateTime", title="Date"},
                    new ReportColumnsDef{data="ServiceHours", title="Service</br>Hours."},
                    new ReportColumnsDef{data="UnplannedOutageNumber", title="unplannedOutageNumber."},
                    new ReportColumnsDef{data="unplannedOutageNumber_Grid", title="unplannedOutageNumber</br>Grid."},
                    new ReportColumnsDef{data="unplannedOutageNumber_WithGrid", title="unplannedOutageNumber</br>WithGrid."},

                    new ReportColumnsDef{data="UnplannedOutageHrs", title="unplanned</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="UnplannedOutageHrsGrid", title="Unplanned</br>Outage</br>Hours Grid."},
                    new ReportColumnsDef{data="PlannedOutageHrs", title="Planned</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="LDCOutageHrs", title="LDC</br>Outage</br>Hours."},
                    new ReportColumnsDef{data="StandbyHrs", title="Standby</br>Hours."},

                    new ReportColumnsDef{data="PH", title="PH."},
                    new ReportColumnsDef{data="EPDH", title="EPDH."},
                    new ReportColumnsDef{data="EUDH", title="EUDH."},
                    new ReportColumnsDef{data="ELR", title="Energy<br>Loss Rate."},
                    new ReportColumnsDef{data="ELRExclGrid", title="ELR</br>ExcelGrid."},

                    new ReportColumnsDef{data="AvailabilityFactor", title="Availability</br>Factor."},
                    new ReportColumnsDef{data="EnergyAvailabilityFactor", title="Energy</br>Availability</br>Factor."},
                    new ReportColumnsDef{data="ReliabilityFactor", title="Reliability</br>Factor."},
                    //new ReportColumnsDef{data="ServiceHours", title="Service</br>Hours."},
                    //new ReportColumnsDef{data="ServiceHours", title="Service</br>Hours."},

                    new ReportColumnsDef{data="LDCDerationHrs", title="LDC</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="PlannedDerationHrs", title="Planned</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="GasDerationHrs", title="Gas</br>Deration</br>Hours."},
                    new ReportColumnsDef{data="AmbDerationHrs", title="Ambient</br>Deration</br>Hourss."},
                    new ReportColumnsDef{data="UnplannedDerationHrs", title="Unplanned</br>Deration</br>Hours."},

                    new ReportColumnsDef{data="LDCDerationMwh", title="LDC</br>Deration</br>Mwhs."},
                    new ReportColumnsDef{data="GasDerationMwh", title="Gas</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="AmbDerationMwh", title="Ambient</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="PlannedDerationMwh", title="Planned</br>Deration</br>Mwh."},
                    new ReportColumnsDef{data="ForcedDerationMwh", title="Forced</br>Deration</br>Mwh."},

                    new ReportColumnsDef{data="PlannedOutageMwh", title="Planned</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="ForcedOutageMwh", title="Forced</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="LDCOutageMwh", title="LDC</br>Outage</br>Mwh."},
                    new ReportColumnsDef{data="StandbyMwh", title="Standby</br>Mwh."},
                    new ReportColumnsDef{data="GDC", title="GDC."},



                };

            #region commented footer code

            //jp = getJSColumnTotal("PH", 1);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("PlannedOutageHrs", 2);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("UnplannedOutageHrs", 3);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("EUDH", 5);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("EPDH", 6);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("LDCOutageHrs", 7);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jp = getJSColumnTotal("StandbyHrs", 8);
            //jsCol = jsCol + jp.JSCol;
            //jsFoot = jsFoot + jp.JSFooter;

            //jsCol = jsCol + "var AvailabilityPerc = (((PH*6)-PlannedOutageHrs-UnplannedOutageHrs)/(PH*6))*100;\n";
            //jsFoot = jsFoot + "$(api.column(" + "4" + ").footer()).html(" + "fnNumFormatter(AvailabilityPerc)" + ");\n";

            //jsCol = jsCol + "var ELRExclGrid = (UnplannedOutageHrs+EUDH)/(UnplannedOutageHrs+((PH*6)-PlannedOutageHrs-UnplannedOutageHrs-LDCOutageHrs-StandbyHrs));";
            //jsFoot = jsFoot + "$(api.column(" + "9" + ").footer()).html(" + "fnNumFormatter(ELRExclGrid)" + ");\n";


            //js = js + jsCol;
            //js = js + jsFoot + "}";

            //vmodel.JSfootercallback = js;
            #endregion
            return vmodel;
        }

        #endregion

        private JSPair getJSColumnTotal(string varName, int varIndex)
        {
            JSPair ret = new JSPair();

            ret.JSCol = "var " + varName + " = api\n" +
                        "       .column(" + varIndex.ToString() + ")\n" +
                        "        .data()\n" +
                        "        .reduce(function(a, b) {\n" +
                        "    return (a * 1) + (b * 1);\n" +
                        "}, 0 );\n";

            ret.JSFooter = "$(api.column(" + varIndex.ToString() + ").footer()).html(fnNumFormatter(" + varName + "));\n";

            return ret;
        }
        struct JSPair
        {
            public string JSCol;
            public string JSFooter;
        }
    }
}