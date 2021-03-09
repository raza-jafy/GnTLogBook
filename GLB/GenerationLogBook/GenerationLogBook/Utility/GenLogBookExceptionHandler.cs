using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GenerationLogBook.Utility
{
    public class GenLogBookExceptionHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var vr = new ViewResult();
            vr.ViewName = "Error";
            vr.TempData["objException"] = e;
            vr.TempData["ActionName"]= filterContext.RouteData.Values["action"].ToString();
            vr.TempData["ControllerName"] = filterContext.RouteData.Values["controller"].ToString(); 
            filterContext.Result = vr;
            
        }
    }
}