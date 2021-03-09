using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerationLogBook
{
    public class PClassDef
    {
    }
    public partial class UnitReadingRegister1
    {
        public bool L1ApprovalCopy { get; set; }
        public bool L2ApprovalCopy { get; set; }
        public bool L1Approvalbool { get; set; }
        public bool L2Approvalbool { get; set; }
        public string RdgDateTimeStr { get; set; }
        public string CreateDateStr { get; set; }
        public string ctrack { get; set; }
    }

    public partial class UnitReadingRegister2
    {
        public bool L1ApprovalCopy { get; set; }
        public bool L2ApprovalCopy { get; set; }
        public bool L1Approvalbool { get; set; }
        public bool L2Approvalbool { get; set; }
        public string RdgDateTimeStr { get; set; }
        public string CreateDateStr { get; set; }
        public string ctrack { get; set; }
    }

    public partial class UnitReadingRegister3
    {
        public bool L1ApprovalCopy { get; set; }
        public bool L2ApprovalCopy { get; set; }
        public bool L1Approvalbool { get; set; }
        public bool L2Approvalbool { get; set; }
        public string RdgDateTimeStr { get; set; }
        public string CreateDateStr { get; set; }
        public string ctrack { get; set; }
    }

    public partial class TemperatureRegister
    {
        public string RdgDateTimeStr { get; set; }
        public string CreateDateStr { get; set; }
    }

    public partial class SiteSafetyRegister
    {
        public decimal TotalIncidents { get; set; }
        public string theDateStr { get; set; }
        public string CreateDateStr { get; set; }
        public string ctrack { get; set; }
    }

    public partial class MonthlyWorkHoursRegister
    {
        public string theMonthStr { get; set; }
        public string CreateDateStr { get; set; }
    }

    public partial class KPIRegister
    {
        public string CreateDateStr { get; set; }
        public string KPIText { get; set; }
        public string PrespectiveText { get; set; }
        public string UnitText { get; set; }
        public string theMonthStr { get; set; }
        public string ctrack { get; set; }
    }
}