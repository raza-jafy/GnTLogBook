using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenerationLogBook.ViewModels;
using GenerationLogBook.Utility;

namespace GenerationLogBook.Controllers
{
    [GenLogBookExceptionHandler]
    [GenLogBookAuthorization(ReqiredAuth = CommDef.AUTH_USERMGMT)]
    public class UserMgmtController : Controller
    {
        private GenerationLogBookEntities _context;

        public UserMgmtController()
        {
            _context = new GenerationLogBookEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        // GET: UserMgmt
        public ActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        public ActionResult ModifyUserForm(int? id)
        {
            var userFormView = new ModifyUserFormViewModel();
            //string email;

            if (id != null) //i.e. request is for editing existing user
            {
                //email = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(id));
                var usr = _context.Users.Where(x => x.UserSequence == id).FirstOrDefault();
                userFormView.UserId = usr.UserId;
                userFormView.UserStatusId = usr.StatusId;
                userFormView.Comments = usr.Comments;
                userFormView.CreatedOn = usr.CreatedOn.ToString();
            }

            userFormView.LOVUserStatus = _context.UserStatuses.ToList();
            return View(userFormView);
        }

        public ActionResult PostUserChanges(ModifyUserFormViewModel userdata)
        {
            if (ModelState.IsValid)
            {
                if (userdata.CreatedOn == null || userdata.CreatedOn == "")
                    addNewUser(userdata);
                else
                    editUser(userdata);

                return RedirectToAction("Index");
            }
            else
            {
                userdata.LOVUserStatus = _context.UserStatuses.ToList();
                return View("ModifyUserForm",userdata);
            }
            
        }
        private void addNewUser(ModifyUserFormViewModel userdata)
        {
            User usr = new User { UserId = userdata.UserId, Comments = userdata.Comments, StatusId = userdata.UserStatusId, CreatedBy=(string)Session["userid"], ChangedBy = (string)Session["userid"] };
            _context.Users.Add(usr);
            _context.SaveChanges();
        }

        private void editUser(ModifyUserFormViewModel userdata)
        {
            var thisuser = _context.Users.Single(x => x.UserId == userdata.UserId);

            thisuser.StatusId = userdata.UserStatusId;
            thisuser.Comments = userdata.Comments;
            thisuser.ChangedBy = (string)Session["userid"];

            _context.SaveChanges();
        }
    }
}