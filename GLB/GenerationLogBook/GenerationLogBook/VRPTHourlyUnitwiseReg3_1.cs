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
    using System.Collections.Generic;
    
    public partial class VRPTHourlyUnitwiseReg3_1
    {
        public string UnitId { get; set; }
        public string SiteId { get; set; }
        public Nullable<System.DateTime> PrevRdgDateTime { get; set; }
        public Nullable<System.DateTime> CurrRdgDateTime { get; set; }
        public Nullable<decimal> DurationHr { get; set; }
        public Nullable<decimal> GDC { get; set; }
        public Nullable<decimal> ReferenceCapacity { get; set; }
        public Nullable<decimal> PrevReferenceCapacity { get; set; }
        public decimal ActualDispatchMW { get; set; }
        public Nullable<decimal> PrevActualDispatchMW { get; set; }
        public Nullable<decimal> AvailableCapacity { get; set; }
        public Nullable<decimal> PrevAvailableCapacity { get; set; }
        public string CurrOutageNature { get; set; }
        public string PrevOutageNature { get; set; }
        public string CurrCalculateDeration { get; set; }
        public string PrevCalculateDeration { get; set; }
        public string CurrStartShut { get; set; }
        public string PrevStartShut { get; set; }
        public string L1Approval { get; set; }
        public string L2Approval { get; set; }
        public string Reason { get; set; }
    }
}
