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
    
    public partial class VRPTDailyUnitwiseReg1AFinal
    {
        public long Id { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public Nullable<System.DateTime> CurrRdgDateTime { get; set; }
        public Nullable<decimal> ServiceHrs { get; set; }
        public Nullable<decimal> unplannedOutageNumber { get; set; }
        public Nullable<decimal> unplannedOutageNumber_Grid { get; set; }
        public Nullable<decimal> unplannedOutageNumber_WithGrid { get; set; }
        public Nullable<decimal> PH { get; set; }
        public Nullable<decimal> PlannedOutageHrs { get; set; }
        public Nullable<decimal> UnplannedOutageHrs { get; set; }
        public Nullable<decimal> UnplannedOutageHrsGrid { get; set; }
        public Nullable<decimal> StandbyHrs { get; set; }
        public Nullable<decimal> LDCOutageHrs { get; set; }
        public Nullable<decimal> EUDH { get; set; }
        public Nullable<decimal> EPDH { get; set; }
        public Nullable<decimal> ELR_Excl_Grid { get; set; }
        public Nullable<decimal> ELR { get; set; }
        public Nullable<decimal> AvailabilityFactor { get; set; }
        public Nullable<decimal> EnergyAvailabilityFactor { get; set; }
        public Nullable<decimal> ReliabilityFactor { get; set; }
        public Nullable<decimal> MaxLoad { get; set; }
        public Nullable<decimal> MinLoad { get; set; }
        public Nullable<decimal> LDCDerationHrs { get; set; }
        public Nullable<decimal> PlannedDerationHrs { get; set; }
        public Nullable<decimal> GasDerationHrs { get; set; }
        public Nullable<decimal> UnplannedDerationHrs { get; set; }
        public Nullable<decimal> LDCDerationMwh { get; set; }
        public Nullable<decimal> PlannedDerationMwh { get; set; }
        public Nullable<decimal> ForcedDerationMwh { get; set; }
        public Nullable<decimal> GasDerationMwh { get; set; }
        public Nullable<decimal> PlannedOutageMwh { get; set; }
        public Nullable<decimal> ForcedOutageMwh { get; set; }
        public Nullable<decimal> LDCOutageMwh { get; set; }
        public Nullable<decimal> StandbyMwh { get; set; }
        public Nullable<decimal> GDC { get; set; }
    }
}
