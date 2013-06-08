using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Helper.UI;

namespace Helper.Web.Filters
{
    public class PageInfoFilterAttribute : ActionFilterAttribute
    {
        private int pageSize = 10;
        private string pageParam = "p";
        public string PageParam
        {
            get { return pageParam; }
            set { pageParam = value; }
        }
        public PageInfoFilterAttribute() { }
        public PageInfoFilterAttribute(int pageSize)
        {
            this.pageSize = pageSize;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int page = 1;
            int skip = 0;
            if (filterContext.HttpContext.Request.QueryString != null &&
                filterContext.HttpContext.Request.QueryString.AllKeys.Contains(pageParam) &&
                !string.IsNullOrEmpty(filterContext.HttpContext.Request.QueryString[pageParam]) &&
                int.TryParse(filterContext.HttpContext.Request.QueryString[pageParam], out page))
                skip = pageSize * (page - 1);


            filterContext.Controller.TempData["PageInfo"] = new PageInfo { PageSize = pageSize, Skip = skip };
            if (filterContext.Controller is PagerController)
                (filterContext.Controller as PagerController).PageInfo = (PageInfo)filterContext.Controller.TempData["PageInfo"];
            base.OnActionExecuting(filterContext);
        }
    }
}
