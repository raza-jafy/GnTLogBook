using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GenerationLogBook.Utility
{
    public class GenLogBookAuthorization : AuthorizeAttribute
    {
        public string ReqiredAuth;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string userid = (string)httpContext.Session["userid"];
            List<string> authtokens = (List<string>)httpContext.Session["authtokens"];

            if (userid == null || userid == "")
                return false;

            if (ReqiredAuth != null)
            {
                if (authtokens.Contains(ReqiredAuth))
                    return true;
                else
                    return false;
            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                //Console.Write("aa");
                dynamic urlHelper = new UrlHelper(context.RequestContext);
                context.HttpContext.Response.StatusCode = 403;

                context.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "NotAuthorized",
                        LogOnUrl = urlHelper.Action("Logoff", "Login")
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //Console.Write("hh");
                context.Result = new RedirectResult("/Login/Logoff");
            }

            //ctx.Result = new EmptyResult();
            //context.HttpContext.Response.StatusCode = 403;
            
            //ctx.HttpContext.Response.Redirect("~/Login/Logoff");
            //ctx.HttpContext.Response.StatusCode = 403;
            //ctx.Result = new RedirectToRouteResult(new
            //System.Web.Routing.RouteValueDictionary(new { controller = "Login", action = "Logoff" }));
        }
    }
}