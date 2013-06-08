using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Inpinke.BLL.Session;
using Inpinke.BLL.Config;

namespace Inpinke.BLL.Filters
{
    /// <summary>
    /// 用户登录和权限过滤
    /// </summary>
    public class UserFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //#if !DEBUG
            string url = filterContext.HttpContext.Request.Url.PathAndQuery;
            string adminKey = filterContext.HttpContext.Request.QueryString["adminkey"];
            if (adminKey == ConfigMap.AdminKey)
            {
                UserSession.ClearUserSession();
                string userid = filterContext.HttpContext.Request.QueryString["userid"];
                UserSession.CurrentUser = DBUserBLL.GetUserByID(int.Parse(userid));
            }

            if (url.IndexOf("login") > -1)
            {
                if (filterContext.HttpContext.Request.UrlReferrer != null)
                {
                    url = filterContext.HttpContext.Request.UrlReferrer.PathAndQuery;
                }
            }

            if (UserSession.CurrentUser == null)
            {
                filterContext.Result = new RedirectResult(string.Format("{0}", "/login?f=" + url));
            }
            //#endif
            base.OnActionExecuting(filterContext);
        }

    }
}
