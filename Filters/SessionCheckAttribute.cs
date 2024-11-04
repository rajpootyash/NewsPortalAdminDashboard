using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsApplication.Filters
{
    public class SessionCheckAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserName"] == null)
            {
                // Redirect to login page
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                    { "controller", "Login" }, // Controller name
                    { "action", "Index" }        // Action name
                    });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}