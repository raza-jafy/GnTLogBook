using GenerationLogBook.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerationLogBook.ViewModels
{
    public class KPIFormViewModel
    {
         public List<KPIRegister> Ledger { get; set; }

        public int Year { get; set; }
        public string Site { get; set; }
        public List<KeyValue> LOVYear { get; set; }
        public IEnumerable<SitesMaster> LOVSitesMaster { get; set; }
        public RequestStatus Status { get; set; }
        public KPIFormViewModel(GenerationLogBookEntities context)
        {
            LOVYear = new List<KeyValue>();
            int year = DateTime.Today.Year;

            for (int x = year; x >= 2009; x--)
            {
                LOVYear.Add(new KeyValue { Key = x.ToString(), Value = x.ToString() });
            }

            Status = new RequestStatus {CODE="OK", TEXT="OK" };
            LOVSitesMaster = context.SitesMasters.Where(x => x.SiteId != "HO").ToList();
        }

        public KPIFormViewModel()
        {
            Status = new RequestStatus { CODE = "OK", TEXT = "OK" };   
        }
    }
}