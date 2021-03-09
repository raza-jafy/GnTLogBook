using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenerationLogBook.Utility;

namespace GenerationLogBook.ViewModels
{
    public class BQIElossFormViewModel
    {
        public string FormHeading { get; set; }
        public List<UnitReadingRegister1> Ledger { get; set; }
        public string UserId { get; set; }
        public string AuthId { get; set; }
        public string SelectedUnitId { get; set; }
        public List<KeyValue> LOVUnits { get; set; }
        public List<KeyValue> LOVForcedGrid { get; set; }
        public List<KeyValue> LOVIsGas { get; set; }
        public List<KeyValue> LOVIsLDC { get; set; }
        public List<KeyValue> LOVIsPlanned { get; set; }
        public List<KeyValue> LOVStartupRamp { get; set; }
        public List<KeyValue> LOVOutageDeration { get; set; }
        public RequestStatus Status { get; set; }
        public List<string> hiddenCols { get; set; }


        //IMPLEMENTING REQUIREMENT 2 CHANGES
        public List<string> LOVReason { get; set; }
    }
}