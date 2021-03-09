using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerationLogBook.Utility
{
    public class CommDef
    {
        public const string AUTH_USERMGMT = "USRMGMT1";

        public const string AUTH_TARGET_EDIT = "TGT-EDIT";

        public const string SITE_KEY_KPC = "KPC";
        public const string SITE_KEY_SGT = "SGT";
        public const string SITE_KEY_KGT = "KGT";
        public const string SITE_KEY_BQ2 = "BQ2";
        public const string SITE_KEY_BQ1 = "BQ1";

        public const string AUTH_MASTER_DATA_MGMT_KPC = "MST-KPC";
        public const string AUTH_MASTER_DATA_MGMT_SGT = "MST-SGT";
        public const string AUTH_MASTER_DATA_MGMT_KGT = "MST-KGT";
        public const string AUTH_MASTER_DATA_MGMT_BQ2 = "MST-BQ2";
        public const string AUTH_MASTER_DATA_MGMT_BQ1 = "MST-BQ1";

        public const string AUTH_ENERGYLOSS_KPC_DATAENTRY = "ELOS-KPC-DEO";
        public const string AUTH_ENERGYLOSS_KPC_OPRAPPROVAL = "ELOS-KPC-L10";
        public const string AUTH_ENERGYLOSS_KPC_PERAPPROVAL = "ELOS-KPC-L20";
        public const string AUTH_ENERGYLOSS_KPC_PWRAPPROVAL = "ELOS-KPC-PWR";

        public const string AUTH_ENERGYLOSS_SGT_DATAENTRY = "ELOS-SGT-DEO";
        public const string AUTH_ENERGYLOSS_SGT_OPRAPPROVAL = "ELOS-SGT-L10";
        public const string AUTH_ENERGYLOSS_SGT_PERAPPROVAL = "ELOS-SGT-L20";
        public const string AUTH_ENERGYLOSS_SGT_PWRAPPROVAL = "ELOS-SGT-PWR";

        public const string AUTH_ENERGYLOSS_KGT_DATAENTRY = "ELOS-KGT-DEO";
        public const string AUTH_ENERGYLOSS_KGT_OPRAPPROVAL = "ELOS-KGT-L10";
        public const string AUTH_ENERGYLOSS_KGT_PERAPPROVAL = "ELOS-KGT-L20";
        public const string AUTH_ENERGYLOSS_KGT_PWRAPPROVAL = "ELOS-KGT-PWR";

        public const string AUTH_ENERGYLOSS_BQ2_DATAENTRY = "ELOS-BQ2-DEO";
        public const string AUTH_ENERGYLOSS_BQ2_OPRAPPROVAL = "ELOS-BQ2-L10";
        public const string AUTH_ENERGYLOSS_BQ2_PERAPPROVAL = "ELOS-BQ2-L20";
        public const string AUTH_ENERGYLOSS_BQ2_PWRAPPROVAL = "ELOS-BQ2-PWR";

        public const string AUTH_ENERGYLOSS_BQ1_DATAENTRY = "ELOS-BQ1-DEO";
        public const string AUTH_ENERGYLOSS_BQ1_OPRAPPROVAL = "ELOS-BQ1-L10";
        public const string AUTH_ENERGYLOSS_BQ1_PERAPPROVAL = "ELOS-BQ1-L20";
        public const string AUTH_ENERGYLOSS_BQ1_PWRAPPROVAL = "ELOS-BQ1-PWR";

        public const string AUTH_SAFETY_KPC_F1 = "SFT-KPC-F1";
        public const string AUTH_SAFETY_KGT_F1 = "SFT-KGT-F1";
        public const string AUTH_SAFETY_SGT_F1 = "SFT-SGT-F1";
        public const string AUTH_SAFETY_BQ1_F1 = "SFT-BQ1-F1";
        public const string AUTH_SAFETY_BQ2_F1 = "SFT-BQ2-F1";

        public const string AUTH_TEMP_KPC_F1 = "TEMP-KPC-F1";
        public const string AUTH_TEMP_KGT_F1 = "TEMP-KGT-F1";
        public const string AUTH_TEMP_SGT_F1 = "TEMP-SGT-F1";
        public const string AUTH_TEMP_BQ1_F1 = "TEMP-BQ1-F1";
        public const string AUTH_TEMP_BQ2_F1 = "TEMP-BQ2-F1";
    }
    public class KeyValue
    {
        public string ParentKey { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class LossInputSet
    {
        public string UnitId { get; set; }
        public string date { get; set; }
    }

    public class RequestStatus
    {
        public string CODE { get; set; }
        public string TEXT { get; set; }
    }

    public class ReportInputParams
    {
        public string Id { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
    public class ReportColumnsDef
    {
        public string data { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string @class { get; set; }
    }
}