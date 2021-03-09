using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenerationLogBook.ViewModels;
using GenerationLogBook.Utility;
using System.Data.Entity;

namespace GenerationLogBook.Controllers
{
    [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_USERMGMT)]
    [GenLogBookExceptionHandler]
    public class AuthorizationMgmtController : Controller
    {
        private GenerationLogBookEntities _context;

        public AuthorizationMgmtController()
        {
            _context = new GenerationLogBookEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        // GET: AuthorizationMgmt
        public ActionResult Index(int id)
        {
            int userSeqNo = id;
            AuthorizationsListViewModel authFormodel = new ViewModels.AuthorizationsListViewModel();
            string userid = _context.Users.Single(x => x.UserSequence == userSeqNo).UserId;
            
            authFormodel.authorizations = _context.UserAuthorizations.Where(x => x.UserId == userid).OrderBy( x => x.ModuleAuthMaster.SitesMaster.SiteName).ToList();
            authFormodel.UserSequenceNo = userSeqNo;

            return View(authFormodel);
        }

        public ActionResult ModifyAuthorizationForm(int id, string authid)
        {
            int userSeqNo = id;
            string userid = _context.Users.Single(x => x.UserSequence == userSeqNo).UserId;
            string siteid;

            AuthorizationFormViewModel authForm = new AuthorizationFormViewModel();

            authForm.authorization = new UserAuthorization();

            authForm.authorization.UserId = userid;
            authForm.UserSequenceNo = id;
            authForm.LOVAuthorizationStatus = _context.AuthStatuses.ToList();
            var asngauth = _context.UserAuthorizations.Where(x => x.UserId == userid).Select(x => x.AuthId).ToList();

            authForm.LOVAuthorizations = new List<KeyValue>();

            foreach (var x in from res in _context.ModuleAuthMasters
                              where (res.StatusId == "A" && !(asngauth.Contains(res.AuthId)))
                              select new { res.SiteId, res.AuthId, res.AuthText }
                     )
            {
                authForm.LOVAuthorizations.Add(new KeyValue { ParentKey = x.SiteId, Key = x.AuthId, Value = x.AuthText });
            }

            if (authid != null && authid != "") //i.e. request is for editing an existing authorization
            {
                var x = _context.UserAuthorizations.Single(y => y.UserId == userid && y.AuthId == authid);
                authForm.authorization.AuthStatusId = x.AuthStatusId;
                authForm.authorization.AuthId = x.AuthId;
                authForm.authorization.Comments = x.Comments;
                authForm.authorization.CreatedOn = x.CreatedOn;

                authForm.LOVModuleAuthMaster = _context.ModuleAuthMasters.Where(z => z.AuthId == authid).ToList();
                siteid = authForm.LOVModuleAuthMaster.Single().SiteId;
                authForm.LOVSitesMaster = _context.SitesMasters.Where(z => z.SiteId == siteid).ToList();
                authForm.SiteId = siteid;
            }
            else//i.e. request is for adding new authorization
            {
                authForm.LOVModuleAuthMaster = _context.ModuleAuthMasters.Where(i => i.AuthId == "XXXXXZZ<>X").ToList();
                authForm.LOVSitesMaster = _context.SitesMasters.ToList();
            }

            return View(authForm);
        }

        public ActionResult PostChanges(AuthorizationFormViewModel authForm)
        {
            //var userid = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ua.authorization.UserId));
            if (authForm.authorization.CreatedOn == null || authForm.authorization.CreatedOn < DateTime.Parse("1/1/2000"))
                Addmode(authForm.authorization);
            else
                Editmode(authForm.authorization);
            return RedirectToAction("Index", new { id = authForm.UserSequenceNo });
        }

        private void Addmode(UserAuthorization ua)
        {
            ua.CreatedBy = (string)Session["userid"];
            ua.ChangedBy = (string)Session["userid"];

            _context.UserAuthorizations.Add(ua);
            _context.SaveChanges();
        }

        private void Editmode(UserAuthorization ua)
        {
            var thisassign = _context.UserAuthorizations.Single(x => x.AuthId == ua.AuthId && x.UserId == ua.UserId);

            thisassign.ChangedBy = (string)Session["userid"];
            thisassign.ChangedOn = DateTime.Now;
            thisassign.Comments = ua.Comments;
            thisassign.AuthStatusId = ua.AuthStatusId;

            //_context.Entry(thisassign).State = EntityState.Modified;
            _context.SaveChanges();

        }
    }
}