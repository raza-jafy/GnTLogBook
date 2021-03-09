using GenerationLogBook.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerationLogBook.ViewModels
{
    public class RecipientsViewModel
    {
        public string ID { get; set; }

        public string SiteID { get; set; }
        public string RecipientName { get; set; }
        public string RecipientType { get; set; }

        public string ReminderNum { get; set; }

        public string Active { get; set; }


        public List<KeyValue> Sites { get; set; }

        public List<KeyValue> ReminderNums { get; set; }

        public List<KeyValue> Actives { get; set; }

    }
}