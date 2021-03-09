using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenerationLogBook.Utility;

namespace GenerationLogBook.ViewModels
{
    public class ReportsFormViewModel
    {
        public List<ReportList> LOVReports;
        public List<KeyValue> LOVSites;
        public List<KeyValue> LOVUnits;
        public object data;
        public List<ReportColumnsDef> cols;
        public string JSfootercallback;
        public RequestStatus Status;
    }
}