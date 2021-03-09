using GenerationLogBook.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenerationLogBook;

namespace GenerationLogBook.ViewModels
{
    public class SafetyFormViewModel
    {

        private GenerationLogBookEntities _context;
        private string _userId, _authId, _formheading, _sitekey, _controller;
        private List<string> _hiddenCols;
        private List<SiteSafetyRegister> _safetyregister;
        private MonthlyWorkHoursRegister _workhoursregister;
        private RequestStatus _reqstatus;
        private DateTime _theDate;

        public SafetyFormViewModel(string controller, string sitekey)
        {
            _controller = controller;
            _sitekey = sitekey;
            _formheading = "Safety Form";
            _hiddenCols = new List<string>();
            _safetyregister = new List<SiteSafetyRegister>();
            _workhoursregister = new MonthlyWorkHoursRegister();
            _reqstatus = new RequestStatus { CODE = "OK", TEXT = "OK" };
        }
        public SafetyFormViewModel(GenerationLogBookEntities context, string userid, string authid, string sitekey, DateTime theDate, string controller)
        {
            _context = context;
            _userId = userid;
            _authId = authid;
            _sitekey = sitekey;
            _controller = controller;
            _formheading = "";
            _hiddenCols = new List<string>();
            //_safetyregister = new List<SiteSafetyRegister>();
            //_workhoursregister = new List<MonthlyWorkHoursRegister>();
            _reqstatus = new RequestStatus();
            _theDate = theDate;
        }


        #region Safety Ledger Handling
        
        public void FetchSafetyLedger()
        {
            getSafetyLedger();
        }

        private void getSafetyLedger()
        {
            _formheading = "Site Safety Recording Sheet [" + _sitekey + ", " + _theDate.ToString("MM-yyyy") + "]";
            _reqstatus = new RequestStatus { CODE = "OK", TEXT = "OK" };

            var res = _context.FetchSafetyRegister(_sitekey, _theDate).OrderBy(z => z.theDate);
            _safetyregister = new List<SiteSafetyRegister>();

            foreach (var x in res)
            {
                _safetyregister.Add(new SiteSafetyRegister
                {
                    SiteId = x.SiteID,
                    theDateStr = x.theDate.ToString(),
                    Fatality = x.Fatality,
                    LTI = x.LTI,
                    MTC = x.MTC,
                    RWC = x.RWC,
                    TotalIncidents = x.Fatality + x.LTI + x.MTC + x.RWC,
                    Remarks = x.Remarks,
                    CreateDateStr = x.CreateDate.ToString(),
                    ctrack = x.RecSrc == "DB" ? "N" : "Y"
                });
            }
        }
        
        public void PostSafetySheet(List<SiteSafetyRegister> changes)
        {
            string SiteId;
            DateTime date;

            if (changes == null || changes.Count == 0)
            {
                _reqstatus = new RequestStatus { CODE = "ERROR", TEXT = "There was Noting to Save to Database" };
            }
            else
            {
                date = DateTime.Parse(changes[0].theDateStr);
                SiteId = changes[0].SiteId;

                SaveSafetyChanges(changes);
                getSafetyLedger();
                getWorkHourLedger();
                _reqstatus = new RequestStatus { CODE = "OK", TEXT = "OK" };
            }
        }

        private void SaveSafetyChanges(List<SiteSafetyRegister> changes)
        {

            foreach (var c in changes)
            {

                if (c.CreateDateStr == null || DateTime.Parse(c.CreateDateStr) < DateTime.Parse("1/1/2018"))
                {
                    AddSafetyRecord(c);
                }
                else
                {
                    EditSafetyRecord(c);
                }
            }
        }
        private void AddSafetyRecord(SiteSafetyRegister rec)
        {
            rec.theDate = DateTime.Parse(rec.theDateStr);
            rec.CreateDate = DateTime.Now;
            rec.CreateBy = _userId;
            rec.ChangeDate = DateTime.Now;
            rec.ChangeBy = _userId;

            _context.SiteSafetyRegisters.Add(rec);
            _context.SaveChanges();
        }

        private void EditSafetyRecord(SiteSafetyRegister rec)
        {
            DateTime date;
            string SiteId;

            date = DateTime.Parse(rec.theDateStr);
            SiteId = rec.SiteId;

            var r = _context.SiteSafetyRegisters.Single(x => x.SiteId == SiteId && x.theDate == date);

            r.Fatality = rec.Fatality;
            r.LTI = rec.LTI;
            r.MTC = rec.MTC;
            r.RWC = rec.RWC;
            r.Remarks = rec.Remarks;

            r.ChangeBy = _userId;
            r.ChangeDate = DateTime.Now;

            _context.SaveChanges();
        }

        #endregion

        #region Work Hour Handling
        public void FetchWorkHourLedger()
        {
            getWorkHourLedger();
        }

        private void getWorkHourLedger()
        {
            //_formheading = "Site Safety Recording Sheet [" + _sitekey + ", " + _theDate.ToString("MM-yyyy") + "]";
            DateTime date = new DateTime(_theDate.Year, _theDate.Month, 1);
            _reqstatus = new RequestStatus { CODE = "OK", TEXT = "OK" };
            var res = _context.MonthlyWorkHoursRegisters.SingleOrDefault(x => x.theMonth == date && x.SiteId == _sitekey );
            if (res != null)
            {
                _workhoursregister = new MonthlyWorkHoursRegister();
                _workhoursregister.theMonthStr = res.theMonth.ToString();
                _workhoursregister.CreateDateStr = res.CreateDate.ToString();
                _workhoursregister.EmpWorkHours = res.EmpWorkHours;
                _workhoursregister.OSPWorkHours = res.OSPWorkHours;
                _workhoursregister.SiteId = res.SiteId;
            }
            else
            {
                _workhoursregister = null;
            }
        }

        public void PostWorkHourLedger(MonthlyWorkHoursRegister change)
        {
            string SiteId;
            DateTime date;

            if (change == null )
            {
                _reqstatus = new RequestStatus { CODE = "ERROR", TEXT = "There was Noting to Save to Database" };
            }
            else
            {
                date = DateTime.Parse(change.theMonthStr);
                date = new DateTime(date.Year, date.Month, 1);
                SiteId = change.SiteId;

                SaveWorkHourChanges(change);
                getWorkHourLedger();
                _reqstatus = new RequestStatus { CODE = "OK", TEXT = "OK" };
            }
        }

        private void SaveWorkHourChanges(MonthlyWorkHoursRegister change)
        {
            DateTime date = new DateTime(_theDate.Year, _theDate.Month, 1);

            if (change.CreateDateStr == null || DateTime.Parse(change.CreateDateStr) < DateTime.Parse("1/1/2018"))
            {
                change.theMonth = DateTime.Parse(change.theMonthStr);
                change.CreateDate = DateTime.Now;
                change.CreateBy = _userId;
                change.ChangeDate = DateTime.Now;
                change.ChangeBy = _userId;

                _context.MonthlyWorkHoursRegisters.Add(change);
                _context.SaveChanges();
            }
            else
            {
                var r = _context.MonthlyWorkHoursRegisters.Single(x => x.SiteId == _sitekey && x.theMonth == date);

                r.EmpWorkHours = change.EmpWorkHours;
                r.OSPWorkHours = change.OSPWorkHours;
                
                r.ChangeBy = _userId;
                r.ChangeDate = DateTime.Now;

                _context.SaveChanges();
            }
        }
        
        #endregion
        public List<SiteSafetyRegister> SafetyLedger
        {
            get {
                return _safetyregister;
            }
        }
        public MonthlyWorkHoursRegister WorkHoursLedger
        {
            get {
                return _workhoursregister;
            }
        }

        public List<string> hiddenCols
        {
            get {
                return _hiddenCols;
            }
            set {
                _hiddenCols = value;
            }
        }

        public string FormHeading
        {
            get
            {
                return _formheading;
            }
        }

        public string Controller
        {
            get
            {
                return _controller;
            }
        }
        public string SiteId
        {
            get
            {
                return _sitekey;
            }
        }
        public RequestStatus Status
        {
            get {
                return _reqstatus;
            }
        }
    }
}