using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MainSite.Controllers
{
    public class CheckLoginStateJ:ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            HttpContext context = HttpContext.Current;
            LogingState(context);

        }
        private void LogingState(HttpContext context)
        {
            if (context.Session["userJ"] == null)

                context.Response.Redirect("~/LoginManager/Login");

        }
    }
}