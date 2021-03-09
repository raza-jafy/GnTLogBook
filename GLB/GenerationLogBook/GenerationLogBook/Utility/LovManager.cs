using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenerationLogBook.Utility
{
    public class LovManager
    {

        public List<string> getPreDefinedRasons(string SiteId, string UnitId)
        {
            //implementing requirement 2 changes..
            List<string> reasons = new List<string>();
            GenerationLogBookEntities _context = new GenerationLogBookEntities();
            (from x in _context.GetGenLogBookPredefinedReasons("", "").ToList() select x).ToList().ForEach(rx => reasons.Add(rx == null ? "" : rx.ToString()));
            return reasons;
        }



        public List<string> getPreDefinedRasonsBySiteID(string SiteId)
        {
            //implementing requirement 2 changes..
            List<string> reasons = new List<string>();
            GenerationLogBookEntities _context = new GenerationLogBookEntities();
            (from x in _context.GetGenLogBookPredefinedReasonsBySite(SiteId).ToList() select x).ToList().ForEach(rx => reasons.Add(rx == null ? "" : rx.ToString()));
            return reasons;
        }

        //public List<ViewModels.RecipientsViewModel> getEmailRecipientsForReminderService(string SiteID)
        //where  res.SiteId.Equals(SiteID, StringComparison.CurrentCultureIgnoreCase)
        public List<ViewModels.RecipientsViewModel> getEmailRecipientsForReminderService(List<string> SiteID)
        {

            GenerationLogBookEntities _context = new GenerationLogBookEntities();
            List<ViewModels.RecipientsViewModel> rm = new List<ViewModels.RecipientsViewModel>();

            for (int i = 0; i < SiteID.Count(); i++)
            {

                (from x in 
                     _context.getGenLogBookRecipientsForNotification(SiteID[i].Trim()).ToList() select x).ToList()
                .ForEach(rx => rm.Add(new ViewModels.RecipientsViewModel()
                {
                    ID = rx.ID.ToString(),
                    SiteID = rx.SiteID.ToString(),
                    RecipientName = rx.ToRecipients,
                    RecipientType = rx.RecipientType.ToString(),
                    ReminderNum = rx.ReminderNo.ToString(),
                    Sites = (from res in _context.SitesMasters
                             where SiteID.Contains(res.SiteId)==true
                             select new KeyValue { ParentKey = res.SiteId, Key = res.SiteId, Value = res.SiteName }).ToList(),

                    ReminderNums = new List<KeyValue> { new KeyValue {ParentKey="1" ,Key = "1", Value = "First | 2nd Reminder Recipients" }, 
                                                        new KeyValue { ParentKey="2",Key = "2", Value = "Final Reminder Recipients" } },


                   Actives = new List<KeyValue> { new KeyValue {ParentKey="0" ,Key = "0", Value = "In Active" }, 
                                                        new KeyValue { ParentKey="1",Key = "1", Value = "Active" } },

                    Active = rx.ACTIVE.ToString()

                  


                }));


                //

            }

        
            return rm;
        }



    }
}