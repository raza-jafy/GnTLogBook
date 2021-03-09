using GenerationLogBook.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GenerationLogBook.Controllers
{
    public class EmailConfigurtionController : Controller
    {
        private GenerationLogBookEntities _context;
        private string _userId, _authId;

        public EmailConfigurtionController()
        {
            _context = new GenerationLogBookEntities();
            _userId = (string)System.Web.HttpContext.Current.Session["userid"];
            _authId = (string)System.Web.HttpContext.Current.Session["ActiveAuthId"];

        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }


        //
        // GET: /EmailConfigurtion/
        public ActionResult Index()
        {
            //get over all sites configuration
            List<string> AVS = this.getAvaiableSites();
            return View(new Utility.LovManager().getEmailRecipientsForReminderService(AVS));
        }


        //public ActionResult ElossEmlConfFormBQ1()
        //{
        //    List<string> AVS = new List<string>();
        //    AVS.Add("BQ1");
        //    return View(new Utility.LovManager().getEmailRecipientsForReminderService(AVS));
        //}


          [HttpGet]
        public JsonResult GenerateRequestView()
        {
            //List<string> AVS = this.getAvaiableSites();
            List<ViewModels.RecipientsViewModel> rm = 
                new Utility.LovManager().getEmailRecipientsForReminderService(this.getAvaiableSites());
            return Json(rm, JsonRequestBehavior.AllowGet);
        }


          [HttpPost]
          public JsonResult PostChangesEditForNotification(int rowID,string SiteID, string RecipientName,int ReminderNumber,int Active)
          {
           
              //var r = _context.xEmailReminderRecipientsForNotification.Single(x => x.ID == rowID && x.SiteID == SiteID);
              var r = _context.xEmailReminderRecipientsForNotifications.Single(x => x.ID == rowID);

              r.ToRecipients = RecipientName.ToString();
              r.SiteID = SiteID;
              r.ReminderNo = ReminderNumber;
              r.Active = Active;
              _context.SaveChanges();

              return Json(new RequestStatus { CODE = "OK", TEXT = "OK" });
          }


          [HttpPost]
          public JsonResult PostNewRecipientForNotification(string inputSiteID, string RecipientName,int inputReminderNumber,string RecipientType)
          {
              bool b = false;
              string msg = string.Empty;

              if ((from x in _context.xEmailReminderRecipientsForNotifications.ToList() 
                       where x.ToRecipients.Equals(RecipientName,StringComparison.CurrentCultureIgnoreCase) select x).Count()==0)
              {

                  switch (RecipientType)
                  {
                      case "TO":
                          _context.xEmailReminderRecipientsForNotifications.Add(
                                new xEmailReminderRecipientsForNotification() { SiteID = inputSiteID, 
                                    ToRecipients = RecipientName, ReminderNo = inputReminderNumber, RType = RecipientType, Active = 1 });

                          b = true;
                          _context.SaveChanges();

                          break;

                      case "CC":
                          _context.xEmailReminderRecipientsForNotifications.Add(
                                new xEmailReminderRecipientsForNotification() { SiteID = inputSiteID, 
                                    ToRecipients = RecipientName, ReminderNo = inputReminderNumber, RType = RecipientType, Active = 1 });
                          b = true;
                          _context.SaveChanges();
                          break;


                  }

                  
              }
              else
              {
                  msg = "user already exists";
              }

              
              return Json(new RequestStatus { CODE = (b == true ? "OK" : "Error"), TEXT = (b == true ? "OK" : msg) });
          }


          private List<string> getAvaiableSites()
          {
              List<VMenuMaster> _objMenu  = (System.Web.HttpContext.Current.Session["authmenus"] as List<VMenuMaster>);
              var stationMenu = _objMenu.Where(u => new[] { "ELOS", "SFT", "TEMP" }.Contains(u.ModuleId)).ToList();
              List<VMenuMaster> distinctStations = stationMenu.GroupBy(p => new { p.SiteId, p.SiteName }).Select(g => g.First()).ToList();
              return (from x in distinctStations select x.SiteId).ToList();

          }



	}
}