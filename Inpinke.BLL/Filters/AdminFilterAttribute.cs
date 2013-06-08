using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Inpinke.BLL.Session;

namespace Inpinke.BLL.Filters
{
    /// <summary>
    /// 用户登录和权限过滤
    /// </summary>
    public class AdminFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #if !DEBUG
            string url = filterContext.HttpContext.Request.Url.PathAndQuery;

            if (url.IndexOf("login") > -1)
            {
                if (filterContext.HttpContext.Request.UrlReferrer != null)
                {
                    url = filterContext.HttpContext.Request.UrlReferrer.PathAndQuery;
                }
            }

            if (AdminSession.CurrentUser == null)
            {
                filterContext.Result = new RedirectResult(string.Format("{0}", "/login?f=" + url));
            }
#endif
            base.OnActionExecuting(filterContext);
        }

    }
}
