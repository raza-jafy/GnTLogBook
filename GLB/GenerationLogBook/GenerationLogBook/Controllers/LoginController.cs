using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GenerationLogBook.ViewModels;
using GenerationLogBook.Utility;

namespace GenerationLogBook.Controllers
{
    [AllowAnonymous]
    [GenLogBookExceptionHandler]
    public class LoginController : Controller
    {
        private GenerationLogBookEntities _context;

        public LoginController()
        {
            _context = new GenerationLogBookEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Login
        public ActionResult Index()
        {
            //int x = 0;
            //int y = 2 / x;
            return View();
        }

        public ActionResult Logoff()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

        public ActionResult doLogin(LoginFormViewModel login)
        {
            if(ModelState.IsValid )
            {
                ADAuthenticator.AuthenticationResult authresult = ADAuthenticator.Authenticate(login.UserId.ToLower(), login.Password);
                UserMenu umenu = new UserMenu(login.UserId);
                // || authresult.Status != "OK"
                if (authresult.Status == "OK")
                {
                    var user = _context.Users.Where(u => u.UserId.ToLower() == login.UserId.ToLower()).ToList();
                    if (user.Count == 0)
                    {
                        ModelState.AddModelError(string.Empty, "This is a privileged application. You are not authorized to access it");
                        return View("Index");
                    }

                    if (user[0].UserStatus.StatusId != "A")
                    {
                        ModelState.AddModelError(string.Empty, "Your Account has been disabled by the administrator of the application. For details, please contact to application administrator");
                        return View("Index");
                    }

                    System.Web.HttpContext.Current.Session["userid"] = login.UserId.ToLower();
                    System.Web.HttpContext.Current.Session["authtokens"] = umenu.getAuthTokens();
                    System.Web.HttpContext.Current.Session["authmenus"] = umenu.getobjMenu();

                    //var urlBase = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    Session["menuhtml"] = umenu.getMenuhtml();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, authresult.Text);
                    return View("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "You must be login to access this resource");
            return View("Index");
            //return Content("Invalid Access!");
        }
    }
}