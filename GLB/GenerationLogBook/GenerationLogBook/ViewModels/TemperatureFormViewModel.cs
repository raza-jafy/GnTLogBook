using GenerationLogBook.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerationLogBook.ViewModels
{
    public class TemperatureFormViewModel
    {
        public string FormHeading;

        public List<TemperatureRegister> Ledger;
        public List<string> hiddenCols { get; set; }
        public string Controller { get; set; }
        public RequestStatus Status { get; set; }

    }
}