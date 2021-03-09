using GenerationLogBook.Utility;
using GenerationLogBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerationLogBook
{
    public class TemperatureHandler
    {
        private GenerationLogBookEntities _context;
        private string _userId, _authId;

        public TemperatureHandler(GenerationLogBookEntities context, string userid, string authid)
        {
            _context = context;
            _userId = userid;
            _authId = authid;
        }
   
        public TemperatureFormViewModel FetchTemperatureLedger(DateTime date, string SITEKEY)
        {
            TemperatureFormViewModel vmodel = getTemperatureLedger(date, SITEKEY);
            return vmodel;
        }

        private TemperatureFormViewModel getTemperatureLedger(DateTime date, string SITEKEY)
        {
            TemperatureFormViewModel temperaturevm = new TemperatureFormViewModel();
            temperaturevm.Ledger = new List<TemperatureRegister>();

            temperaturevm.FormHeading = "Temperature Recording Sheet [" + SITEKEY + ", " + date.ToString("dd-MM-yyyy") + "]";
            temperaturevm.Status = new RequestStatus { CODE = "OK", TEXT = "OK" };

            var res = _context.FetchTemperatureRegister(SITEKEY, date).OrderBy(z => z.RdgDateTime);

            foreach (var x in res)
            {
                temperaturevm.Ledger.Add(new TemperatureRegister
                {
                    RdgDateTimeStr = x.RdgDateTime.ToString(),
                    SiteId = SITEKEY,
                    AmbTemp = x.AmbTemp,
                    SeaWaterTemp = x.SeaWaterTemp,
                    CreateDateStr = x.CreateDate.ToString()
                });
            }

            return temperaturevm;
        }

        public TemperatureFormViewModel PostTemperatureSheet(List<TemperatureRegister> changes)
        {
            TemperatureFormViewModel vmodel = new TemperatureFormViewModel();
            string SiteId;
            DateTime date;

            if (changes == null || changes.Count == 0)
            {
                vmodel.Status = new RequestStatus { CODE = "ERROR", TEXT = "There was Noting to Save to Database" };
            }
            else
            {
                date = DateTime.Parse(changes[0].RdgDateTimeStr);
                SiteId = changes[0].SiteId;

                SaveTemperatureChanges(changes);
                vmodel = getTemperatureLedger(date, SiteId);
                vmodel.Status = new RequestStatus { CODE = "OK", TEXT = "OK" };
            }

            return vmodel;
        }

        private void SaveTemperatureChanges(List<TemperatureRegister> changes)
        {

            foreach (var c in changes)
            {

                if (c.CreateDateStr == null || DateTime.Parse(c.CreateDateStr) < DateTime.Parse("1/1/2018"))
                {
                    AddTemperatureRecord(c);
                }
                else
                {
                    EditTemperatureRecord(c);
                }
            }
        }
        private void AddTemperatureRecord(TemperatureRegister rec)
        {
            rec.RdgDateTime = DateTime.Parse(rec.RdgDateTimeStr);
            rec.CreateDate = DateTime.Now;
            rec.CreateBy = _userId;
            rec.ChangeDate = DateTime.Now;
            rec.ChangeBy = _userId;

            _context.TemperatureRegisters.Add(rec);
            _context.SaveChanges();
        }

        private void EditTemperatureRecord(TemperatureRegister rec)
        {
            DateTime date;
            string SiteId;

            date = DateTime.Parse(rec.RdgDateTimeStr);
            SiteId = rec.SiteId;

            var r = _context.TemperatureRegisters.Single(x => x.SiteId == SiteId && x.RdgDateTime == date);

            r.AmbTemp = rec.AmbTemp;
            r.SeaWaterTemp = rec.SeaWaterTemp;
            r.ChangeBy = _userId;
            r.ChangeDate = DateTime.Now;

            _context.SaveChanges();
        }
    }
}