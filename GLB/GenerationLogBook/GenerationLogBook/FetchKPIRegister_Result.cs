//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenerationLogBook
{
    using System;
    
    public partial class FetchKPIRegister_Result
    {
        public string KPIId { get; set; }
        public string KPIText { get; set; }
        public string Prespective { get; set; }
        public string PrespectiveText { get; set; }
        public string UnitText { get; set; }
        public string SiteId { get; set; }
        public Nullable<System.DateTime> theMonth { get; set; }
        public decimal MaxScore { get; set; }
        public decimal Bad { get; set; }
        public decimal Average { get; set; }
        public decimal Good { get; set; }
        public Nullable<decimal> ActualResult { get; set; }
        public Nullable<decimal> AdjustedResult { get; set; }
        public Nullable<decimal> PointAchieved { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ChangeDate { get; set; }
        public string ChangeBy { get; set; }
        public short SortPrespective { get; set; }
        public short SortKPI { get; set; }
        public string RecSrc { get; set; }
    }
}
