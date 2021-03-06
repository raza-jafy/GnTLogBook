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
    
    public partial class VRPTHourlyUnitwiseReg1D_4
    {
        public string UnitId { get; set; }
        public string SiteId { get; set; }
        public Nullable<System.DateTime> CurrRdgDateTime { get; set; }
        public Nullable<System.DateTime> PrevRdgDateTime { get; set; }
        public Nullable<decimal> GDC { get; set; }
        public Nullable<decimal> ReferenceCapacity { get; set; }
        public Nullable<decimal> ActualDispatchMW { get; set; }
        public Nullable<decimal> AmbTemp { get; set; }
        public Nullable<decimal> AmbPressure { get; set; }
        public Nullable<decimal> SeaWaterTemp { get; set; }
        public string CurrIsPlanned { get; set; }
        public string CurrIsForced { get; set; }
        public string CurrIsLDC { get; set; }
        public string CurrIsGas { get; set; }
        public string CurrIsOutageDeration { get; set; }
        public string CurrStartupRampId { get; set; }
        public string L1Approval { get; set; }
        public string L2Approval { get; set; }
        public string Reason { get; set; }
        public Nullable<decimal> PH { get; set; }
        public Nullable<decimal> AmbDeration { get; set; }
        public Nullable<decimal> ForcedDeration { get; set; }
        public Nullable<decimal> PlannedDeration { get; set; }
        public Nullable<decimal> PlannedOutageHrs { get; set; }
        public Nullable<decimal> UnplannedOutageHrs { get; set; }
        public Nullable<decimal> UnplannedOutageHrsGrid { get; set; }
        public Nullable<decimal> LDCOutageHrs { get; set; }
        public Nullable<decimal> StandbyHrs { get; set; }
        public Nullable<decimal> UnplannedDerationHrs { get; set; }
        public Nullable<decimal> PlannedDerationHrs { get; set; }
        public Nullable<decimal> GasDerationHrs { get; set; }
        public Nullable<decimal> LDCDerationHrs { get; set; }
        public Nullable<decimal> PlannedRampHrs { get; set; }
        public Nullable<decimal> ForcedRampHrs { get; set; }
        public Nullable<decimal> PlannedOutageMwh { get; set; }
        public Nullable<decimal> ForcedOutageMwh { get; set; }
        public Nullable<decimal> LDCOutageMwh { get; set; }
        public Nullable<decimal> StandbyMwh { get; set; }
        public Nullable<decimal> PlannedRampMwh { get; set; }
        public Nullable<decimal> ForcedRampMwh { get; set; }
        public Nullable<decimal> ForcedDerationMwh { get; set; }
        public Nullable<decimal> PlannedDerationMwh { get; set; }
        public Nullable<decimal> LDCDerationMwh { get; set; }
        public Nullable<decimal> GasDerationMwh { get; set; }
        public Nullable<decimal> AvailabilityPercent { get; set; }
        public Nullable<decimal> EUDH { get; set; }
        public Nullable<decimal> EPDH { get; set; }
    }
}
