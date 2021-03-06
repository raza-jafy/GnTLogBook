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
    
    public partial class VRPTDailyBQPS2_StationwiseFinal
    {
        public long Id { get; set; }
        public string SiteId { get; set; }
        public Nullable<System.DateTime> CurrRdgDateTime { get; set; }
        public Nullable<decimal> ServiceHrs { get; set; }
        public Nullable<decimal> unplannedOutageNumber { get; set; }
        public Nullable<decimal> unplannedOutageNumber_Grid { get; set; }
        public Nullable<decimal> unplannedOutageNumber_WithGrid { get; set; }
        public Nullable<decimal> unplannedOutageHrs { get; set; }
        public Nullable<decimal> unplannedOutageHrsGrid { get; set; }
        public Nullable<decimal> PlannedOutageHrs { get; set; }
        public Nullable<decimal> LDCOutageHrs { get; set; }
        public Nullable<decimal> StandbyHrs { get; set; }
        public Nullable<decimal> PH { get; set; }
        public Nullable<decimal> EPDH { get; set; }
        public Nullable<decimal> EUDH { get; set; }
        public Nullable<decimal> ELR_Excl_Grid { get; set; }
        public Nullable<decimal> ELR { get; set; }
        public Nullable<decimal> AvailabilityFactor { get; set; }
        public Nullable<decimal> EnergyAvailabilityFactor { get; set; }
        public Nullable<decimal> ReliabilityFactor { get; set; }
        public Nullable<decimal> LDCDerationHrs { get; set; }
        public Nullable<decimal> PlannedDerationHrs { get; set; }
        public Nullable<decimal> GasDerationHrs { get; set; }
        public Nullable<decimal> UnplannedDerationHrs { get; set; }
        public Nullable<decimal> GDC { get; set; }
    }
}
