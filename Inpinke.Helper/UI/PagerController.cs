using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Helper.UI
{
    public class PagerController : Controller
    {
        protected int Total;

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (PageInfo != null && PageInfo.Total == 0)
                PageInfo.Total = Total;
            base.OnActionExecuted(filterContext);

        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public PageInfo PageInfo
        {
            set;
            get;
        }

    }
}
