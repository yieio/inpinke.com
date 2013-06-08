using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using Helper;
using Helper.UI;

namespace System.Web.Mvc
{
    public static class PagerExtension
    {
        private const int DEFAULT_PAGE_SIZE = 10;
        private const int DEFAULT_MARGIN = 3;

        #region 分页样式一
        /// <summary>
        /// 分页控件
        /// 使用时, 必须在Controller中传递以下参数:TempData["PageInfo"] = new { total = total, pageSize = PAGE_SIZE };
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper)
        {
            if (!helper.ViewContext.TempData.ContainsKey("PageInfo"))
                return string.Empty;

            return helper.Pager((PageInfo)helper.ViewContext.TempData["PageInfo"]);
        }

        public static string Pager(this HtmlHelper helper, PageInfo pageInfo)
        {
            if (pageInfo == null)
                return String.Empty;
            IDictionary<string, int> values = pageInfo.ToDictionary<int>();
            int total = pageInfo.Total;
            int pageSize = pageInfo.PageSize == 0 ? DEFAULT_PAGE_SIZE : pageInfo.PageSize;
            int margin = pageInfo.Margin == 0 ? DEFAULT_MARGIN : pageInfo.Margin;

            return helper.Pager(total, pageSize, margin, pageInfo.Skip / pageInfo.PageSize == 0 ? pageInfo.Skip / pageInfo.PageSize : pageInfo.Skip / pageInfo.PageSize + 1);
        }

        /// <summary>
        /// 显示页码链接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page">当前页码</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="margin">页宽度</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper, int total, int pageSize, int margin, int page)
        {

            if (total == 0)
                return String.Empty;

            Uri uri = helper.ViewContext.HttpContext.Request.Url;
            string url = uri.AbsolutePath;
            if (uri.Query.Length == 0)
                url += "?p={0}";
            else
            {
                url += uri.Query;
                Regex pr = new Regex(@"([\?&])p=[0-9]+");
                if (pr.IsMatch(uri.Query))
                {
                    url = pr.Replace(url, "$1p={0}");
                }
                else
                    url += "&p={0}";
            }
            url = System.Web.HttpUtility.UrlDecode(url);

            //int page = 1;
            //int.TryParse(helper.ViewContext.HttpContext.Request.QueryString["p"], out page);

            //页数量
            int pageCount = (total % pageSize) == 0 ? total / pageSize : total / pageSize + 1;
            if (page > pageCount)
                page = pageCount;
            else if (page < 1)
                page = 1;

            //起始页数
            int startPage = page - margin;
            if (startPage < 1)
                startPage = 1;

            //结束页数
            int endPage = page + margin;
            if (endPage > pageCount)
                endPage = pageCount;

            if (startPage == 1 && endPage == 1)
                return String.Empty;

            int prev = page > 1 ? page - 1 : 1;
            int next = page < pageCount ? page + 1 : pageCount;


            StringBuilder sb = new StringBuilder();

            sb.Append("<div class=\"pagerCss\">");
            sb.AppendFormat("<a href=\"{0}\" class=\"prev\">{1}</a>", string.Format(url, prev), "&nbsp;&nbsp;");


            if (page - margin > 0)
            {
                sb.AppendFormat("<a href=\"{0}\">{1}</a>", string.Format(url, 1), "1");
                sb.Append("<label>...</label>");
            }

            for (int i = page - margin; i <= page + margin && i <= pageCount; i++)
            {
                if (i < 1 || (page == 1 && page - margin > 0) || (page == pageCount && page + margin < pageCount))
                    continue;
                if (i == page)
                    sb.Append(string.Format("<label>{0}</label>", i.ToString()));
                else
                    sb.AppendFormat("<a href=\"{0}\">{1}</a>", string.Format(url, i), i.ToString());
            }

            if (page + margin < pageCount)
            {
                sb.Append("<label>...</label>");
                sb.AppendFormat("<a href=\"{0}\">{1}</a>", string.Format(url, pageCount), pageCount.ToString());

            }

            sb.AppendFormat("<a href=\"{0}\" class=\"next\">{1}</a>", string.Format(url, next), "&nbsp;&nbsp;");
            sb.Append("</div>");

            return sb.ToString();
        }
        #endregion

        #region 分页样式二
        /// <summary>
        /// 分页控件
        /// 使用时, 必须在Controller中传递以下参数:TempData["PageInfo"] = new { total = total, pageSize = PAGE_SIZE };
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string Pager2(this HtmlHelper helper)
        {
            PageInfo pi = null;
            if (!helper.ViewContext.TempData.ContainsKey("PageInfo"))
            {
                pi = new PageInfo
                {
                    PageSize = DEFAULT_PAGE_SIZE,
                    Margin = DEFAULT_MARGIN,
                    Total = 0,
                    Skip = 0
                };
            }
            else
            {
                pi = (PageInfo)helper.ViewContext.TempData["PageInfo"];
            }

            return helper.Pager2(pi);
        }

        public static string Pager2(this HtmlHelper helper, PageInfo pageInfo)
        {
            if (pageInfo == null)
                return String.Empty;
            //IDictionary<string, int> values = pageInfo.ToDictionary<int>();
            int total = pageInfo.Total;
            int pageSize = pageInfo.PageSize == 0 ? DEFAULT_PAGE_SIZE : pageInfo.PageSize;
            int margin = pageInfo.Margin == 0 ? DEFAULT_MARGIN : pageInfo.Margin;

            return helper.Pager2(total, pageSize, margin, pageInfo.Skip / pageInfo.PageSize == 0 ? pageInfo.Skip / pageInfo.PageSize : pageInfo.Skip / pageInfo.PageSize + 1);
        }

        /// <summary>
        /// 显示页码链接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page">当前页码</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="margin">页宽度</param>
        /// <returns></returns>
        public static string Pager2(this HtmlHelper helper, int total, int pageSize, int margin, int page)
        {

            //if (total == 0)
            //    return String.Empty;

            Uri uri = helper.ViewContext.HttpContext.Request.Url;
            string url = uri.AbsolutePath;
            if (uri.Query.Length == 0)
                url += "?p={0}";
            else
            {
                url += uri.Query;
                Regex pr = new Regex(@"([\?&])p=[0-9]+");
                if (pr.IsMatch(uri.Query))
                {
                    url = pr.Replace(url, "$1p={0}");
                }
                else
                    url += "&p={0}";
            }
            url = System.Web.HttpUtility.UrlDecode(url);

            //int page = 1;
            //int.TryParse(helper.ViewContext.HttpContext.Request.QueryString["p"], out page);

            //页数量
            int pageCount = (total % pageSize) == 0 ? total / pageSize : total / pageSize + 1;
            if (page > pageCount)
                page = pageCount;
            else if (page < 1)
                page = 1;

            //起始页数
            int startPage = page - margin;
            if (startPage < 1)
                startPage = 1;

            //结束页数
            int endPage = page + margin;
            if (endPage > pageCount)
                endPage = pageCount;

            //if (startPage == 1 && endPage == 1)
            //    return String.Empty;

            int prev = page > 1 ? page - 1 : 1;
            int next = page < pageCount ? page + 1 : pageCount;


            StringBuilder sb = new StringBuilder();

            if (page > 1)
            {
                sb.AppendFormat("<a href=\"{0}\" class=\"prev\"><input type=\"button\" class=\"pre_btn_0\" onclick=\"javascript:jump('{0}')\"/></a>", string.Format(url, prev));
            }
            else
            {
                sb.AppendFormat("<a href=\"javascript:void(0)\" loadTip=\"false\" class=\"prev\"><input type=\"button\" class=\"pre_btn_0_no\" /></a>");
            }
            if (page < pageCount)
            {
                sb.AppendFormat("<a href=\"{0}\" class=\"next\"><input type=\"button\" class=\"next_btn_0\" onclick=\"javascript:jump('{0}')\"/></a>", string.Format(url, next));
            }
            else
            {
                sb.AppendFormat("<a href=\"javascript:void(0)\" loadTip=\"false\" class=\"next\"><input type=\"button\" class=\"next_btn_0_no\" /></a>");
            }
            //sb.AppendFormat("<a href=\"/\" class=\"next\"><img src=\"/Content/Images/reback-yhd.jpg\" alt=\"返回优惠多首页\" /></a>");
            return sb.ToString();
        }
        #endregion

        #region 分页样式三
        /// <summary>
        /// 分页控件
        /// 使用时, 必须在Controller中传递以下参数:TempData["PageInfo"] = new { total = total, pageSize = PAGE_SIZE };
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string uiPager(this HtmlHelper helper)
        {
            if (!helper.ViewContext.TempData.ContainsKey("PageInfo"))
                return string.Empty;

            return helper.uiPager((PageInfo)helper.ViewContext.TempData["PageInfo"]);
        }

        public static string uiPager(this HtmlHelper helper, PageInfo pageInfo)
        {
            if (pageInfo == null)
                return String.Empty;
            IDictionary<string, int> values = pageInfo.ToDictionary<int>();
            int total = pageInfo.Total;
            int pageSize = pageInfo.PageSize == 0 ? DEFAULT_PAGE_SIZE : pageInfo.PageSize;
            int margin = pageInfo.Margin == 0 ? DEFAULT_MARGIN : pageInfo.Margin;

            return helper.uiPager(total, pageSize, margin, pageInfo.Skip / pageInfo.PageSize == 0 ? pageInfo.Skip / pageInfo.PageSize : pageInfo.Skip / pageInfo.PageSize + 1);
        }

        /// <summary>
        /// 显示页码链接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page">当前页码</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="margin">页宽度</param>
        /// <returns></returns>
        public static string uiPager(this HtmlHelper helper, int total, int pageSize, int margin, int page)
        {
            StringBuilder sb = new StringBuilder();
            if (total == 0)
            {
                sb.Append("<div class=\"ui-corner-all ui-pager\">");
                sb.AppendFormat("<span id=\"pageCount\" >共{0}个记录</span>", total);
                sb.Append("</div>");
                return sb.ToString();

            }

            Uri uri = helper.ViewContext.HttpContext.Request.Url;
            string url = uri.AbsolutePath;
            if (uri.Query.Length == 0)
                url += "?p={0}";
            else
            {
                url += uri.Query;
                Regex pr = new Regex(@"([\?&])p=[0-9]+");
                if (pr.IsMatch(uri.Query))
                {
                    url = pr.Replace(url, "$1p={0}");
                }
                else
                    url += "&p={0}";
            }
            url = System.Web.HttpUtility.UrlDecode(url);

            //int page = 1;
            //int.TryParse(helper.ViewContext.HttpContext.Request.QueryString["p"], out page);

            //页数量
            int pageCount = (total % pageSize) == 0 ? total / pageSize : total / pageSize + 1;
            if (page > pageCount)
                page = pageCount;
            else if (page < 1)
                page = 1;

            //起始页数
            int startPage = page - margin;
            if (startPage < 1)
                startPage = 1;

            //结束页数
            int endPage = page + margin;
            if (endPage > pageCount)
                endPage = pageCount;

            if (startPage == 1 && endPage == 1)
            {
                sb.Append("<div class=\"ui-corner-all ui-pager\">");
                sb.AppendFormat("<span id=\"pageCount\" >共{0}个记录，第{1}页/共{2}页</span>", total, page, pageCount);
                sb.Append("</div>");
                return sb.ToString();
            }

            int prev = page > 1 ? page - 1 : 1;
            int next = page < pageCount ? page + 1 : pageCount;


            

            sb.Append("<div class=\"ui-corner-all ui-pager\">");
            sb.AppendFormat("<span id=\"pageCount\" >共{0}个记录，第{1}页/共{2}页</span>", total, page, pageCount);
            if (page > 1)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"firstPage\" class=\"link\">{1}</a>", string.Format(url, 1), "首页");

                sb.AppendFormat("<a href=\"{0}\" id=\"prevPage\" class=\"link\">{1}</a>", string.Format(url, prev), "上一页");
            }
            else
            {
                sb.AppendFormat("<a id=\"firstPage\" class=\"link\">{1}</a>", string.Format(url, 1), "首页");

                sb.AppendFormat("<a  id=\"prevPage\" class=\"link\">{1}</a>", string.Format(url, prev), "上一页");
            }

            if (page < pageCount)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"nextPage\" class=\"link\">{1}</a>", string.Format(url, next), "下一页");
                sb.AppendFormat("<a href=\"{0}\" id=\"lastPage\" class=\"link\">{1}</a>", string.Format(url, pageCount), "尾页");
            }
            else
            {
                sb.AppendFormat("<a  id=\"nextPage\" class=\"link\">{1}</a>", string.Format(url, next), "下一页");
                sb.AppendFormat("<a   id=\"lastPage\" class=\"link\">{1}</a>", string.Format(url, pageCount), "尾页");
            }
            sb.Append("跳转到：<input type=\"text\" id=\"pageNum\" value=\"" + page + "\" style=\"width: 64px;\" class=\"text ui-widget-content ui-corner-all\"   onkeydown=\" var regInput=/^\\d+$/; var inpJumpNum=this.value; if(!regInput.exec(inpJumpNum)) inpJumpNum=1; if(inpJumpNum>" + pageCount + "){ inpJumpNum=" + pageCount + "} if(inpJumpNum<=0 ){inpJumpNum=1}  if(event.keyCode==13) {window.location='" + string.Format(url, "") + "'+inpJumpNum; return false;}\" />");
            sb.Append("<a href=\"javascript:void(0)\" id=\"jumpPage\" onclick=\"var jumpNum=document.getElementById('pageNum').value; var reg=/^\\d+$/; if(!reg.exec(jumpNum)) jumpNum=1; if(jumpNum>" + pageCount + "){ jumpNum=" + pageCount + "} if(jumpNum<=0 ){jumpNum=1}  window.location='" + string.Format(url, "") + "'+jumpNum ;return false;\">跳转</a>");
            sb.Append("</div>");

            return sb.ToString();
        }
        #endregion

        #region 分页样式四
        /// <summary>
        /// 分页控件
        /// 使用时, 必须在Controller中传递以下参数:TempData["PageInfo"] = new { total = total, pageSize = PAGE_SIZE };
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string uiPager1(this HtmlHelper helper)
        {
            if (!helper.ViewContext.TempData.ContainsKey("PageInfo"))
                return string.Empty;

            return helper.uiPager1((PageInfo)helper.ViewContext.TempData["PageInfo"]);
        }

        public static string uiPager1(this HtmlHelper helper, PageInfo pageInfo)
        {
            if (pageInfo == null)
                return String.Empty;
            IDictionary<string, int> values = pageInfo.ToDictionary<int>();
            int total = pageInfo.Total;
            int pageSize = pageInfo.PageSize == 0 ? DEFAULT_PAGE_SIZE : pageInfo.PageSize;
            int margin = pageInfo.Margin == 0 ? DEFAULT_MARGIN : pageInfo.Margin;

            return helper.uiPager1(total, pageSize, margin, pageInfo.Skip / pageInfo.PageSize == 0 ? pageInfo.Skip / pageInfo.PageSize : pageInfo.Skip / pageInfo.PageSize + 1);
        }
        /// <summary>
        /// 讨论区显示页码链接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page">当前页码</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="margin">页宽度</param>
        /// <returns></returns>
        public static string uiPager1(this HtmlHelper helper, int total, int pageSize, int margin, int page)
        {
            Guid gid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            if (total == 0)
            {
                sb.Append("<div class=\"ui-corner-all uipager\">");
                sb.AppendFormat("<span id=\"pageCount\" >共{0}个记录</span>", total);
                sb.Append("</div>");
                return sb.ToString();

            }

            Uri uri = helper.ViewContext.HttpContext.Request.Url;
            string url = uri.AbsolutePath;
            if (uri.Query.Length == 0)
                url += "?p={0}";
            else
            {
                url += uri.Query;
                Regex pr = new Regex(@"([\?&])p=[0-9]+");
                if (pr.IsMatch(uri.Query))
                {
                    url = pr.Replace(url, "$1p={0}");
                }
                else
                    url += "&p={0}";
            }
            url = System.Web.HttpUtility.UrlDecode(url);

            //int page = 1;
            //int.TryParse(helper.ViewContext.HttpContext.Request.QueryString["p"], out page);

            //页数量
            int pageCount = (total % pageSize) == 0 ? total / pageSize : total / pageSize + 1;
            if (page > pageCount)
                page = pageCount;
            else if (page < 1)
                page = 1;

            //起始页数
            int startPage = page - margin;
            if (startPage < 1)
                startPage = 1;

            //结束页数
            int endPage = page + margin;
            if (endPage > pageCount)
                endPage = pageCount;

            if (startPage == 1 && endPage == 1)
            {
                sb.Append("<div class=\"ui-corner-all uipager\">");
                sb.AppendFormat("<span id=\"pageCount\" ><b>第{0}页/共{1}页</b></span>", page, pageCount);
                sb.Append("</div>");
                return sb.ToString();
            }

            int prev = page > 1 ? page - 1 : 1;
            int next = page < pageCount ? page + 1 : pageCount;




            sb.Append("<div class=\"ui-corner-all uipager\">");
            sb.AppendFormat("<span id=\"pageCount\" ><b>第{0}页/共{1}页</b></span>", page, pageCount);
            if (page > 1)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"prevPage\" class=\"prev\"><b>{1}</b></a>", string.Format(url, prev), " ");
            }
            else
            {
                sb.AppendFormat("<a  id=\"prevPage\" class=\"prev\"><b>{1}</b></a>", string.Format(url, prev), " ");
            }

            if (page < pageCount)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"nextPage\" class=\"next\"><b>{1}</b></a>", string.Format(url, next), " ");
            }
            else
            {
                sb.AppendFormat("<a  id=\"nextPage\" class=\"next\"><b>{1}</b></a>", string.Format(url, next), " ");
            }
            sb.Append("<input type=\"text\" id=\"pageNum" + gid + "\" value=\"" + page + "\" style=\"width: 64px;\" class=\"texttype\"   onkeydown=\" var regInput=/^\\d+$/; var inpJumpNum=this.value; if(!regInput.exec(inpJumpNum)) inpJumpNum=1; if(inpJumpNum>" + pageCount + "){ inpJumpNum=" + pageCount + "} if(inpJumpNum<=0 ){inpJumpNum=1}  if(event.keyCode==13) {window.location='" + string.Format(url, "") + "'+inpJumpNum; return false;}\" />");
            sb.Append("<a href=\"javascript:void(0)\" id=\"jumpPage\" class=\"goTo\" onclick=\"var jumpNum=document.getElementById('pageNum" + gid + "').value; var reg=/^\\d+$/; if(!reg.exec(jumpNum)) jumpNum=1; if(jumpNum>" + pageCount + "){ jumpNum=" + pageCount + "} if(jumpNum<=0 ){jumpNum=1} window.location='" + string.Format(url, "") + "'+jumpNum;return false;\"><b> </b></a>");
            sb.Append("</div>");

            return sb.ToString();
        }
        
        #endregion

        #region 分页样式五
        /// <summary>
        /// 分页控件没有go
        /// 使用时, 必须在Controller中传递以下参数:TempData["PageInfo"] = new { total = total, pageSize = PAGE_SIZE };
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string uiPager2(this HtmlHelper helper)
        {
            if (!helper.ViewContext.TempData.ContainsKey("PageInfo"))
                return string.Empty;

            return helper.uiPager2((PageInfo)helper.ViewContext.TempData["PageInfo"]);
        }

        public static string uiPager2(this HtmlHelper helper, PageInfo pageInfo)
        {
            if (pageInfo == null)
                return String.Empty;
            IDictionary<string, int> values = pageInfo.ToDictionary<int>();
            int total = pageInfo.Total;
            int pageSize = pageInfo.PageSize == 0 ? DEFAULT_PAGE_SIZE : pageInfo.PageSize;
            int margin = pageInfo.Margin == 0 ? DEFAULT_MARGIN : pageInfo.Margin;

            return helper.uiPager2(total, pageSize, margin, pageInfo.Skip / pageInfo.PageSize == 0 ? pageInfo.Skip / pageInfo.PageSize : pageInfo.Skip / pageInfo.PageSize + 1);
        }
        /// <summary>
        /// 讨论区显示页码链接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page">当前页码</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="margin">页宽度</param>
        /// <returns></returns>
        public static string uiPager2(this HtmlHelper helper, int total, int pageSize, int margin, int page)
        {
            Guid gid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            if (total == 0)
            {
                sb.Append("<div class=\"ui-corner-all uipager\">");
                sb.AppendFormat("<span id=\"pageCount\" >共{0}个记录</span>", total);
                sb.Append("</div>");
                return sb.ToString();

            }

            Uri uri = helper.ViewContext.HttpContext.Request.Url;
            string url = uri.AbsolutePath;
            if (uri.Query.Length == 0)
                url += "?p={0}";
            else
            {
                url += uri.Query;
                Regex pr = new Regex(@"([\?&])p=[0-9]+");
                if (pr.IsMatch(uri.Query))
                {
                    url = pr.Replace(url, "$1p={0}");
                }
                else
                    url += "&p={0}";
            }
            url = System.Web.HttpUtility.UrlDecode(url);

            //int page = 1;
            //int.TryParse(helper.ViewContext.HttpContext.Request.QueryString["p"], out page);

            //页数量
            int pageCount = (total % pageSize) == 0 ? total / pageSize : total / pageSize + 1;
            if (page > pageCount)
                page = pageCount;
            else if (page < 1)
                page = 1;

            //起始页数
            int startPage = page - margin;
            if (startPage < 1)
                startPage = 1;

            //结束页数
            int endPage = page + margin;
            if (endPage > pageCount)
                endPage = pageCount;

            if (startPage == 1 && endPage == 1)
            {
                sb.Append("<div class=\"ui-corner-all uipager\">");
                sb.AppendFormat("<span id=\"pageCount\" ><b>第{0}/{1}页</b></span>", page, pageCount);
                sb.Append("</div>");
                return sb.ToString();
            }

            int prev = page > 1 ? page - 1 : 1;
            int next = page < pageCount ? page + 1 : pageCount;




            sb.Append("<div class=\"ui-corner-all uipager\">");
            sb.AppendFormat("<span id=\"pageCount\" ><b>第{0}/{1}页</b></span>", page, pageCount);
            if (page > 1)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"prevPage\" class=\"prev\"><b>{1}</b></a>", string.Format(url, prev), " ");
            }
            else
            {
                sb.AppendFormat("<a  id=\"prevPage\" class=\"prev\"><b>{1}</b></a>", string.Format(url, prev), " ");
            }

            if (page < pageCount)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"nextPage\" class=\"next\"><b>{1}</b></a>", string.Format(url, next), " ");
            }
            else
            {
                sb.AppendFormat("<a  id=\"nextPage\" class=\"next\"><b>{1}</b></a>", string.Format(url, next), " ");
            }
            sb.Append("</div>");

            return sb.ToString();
        }

        #endregion

        #region 分页样式六
        /// <summary>
        /// 分页控件
        /// 使用时, 必须在Controller中传递以下参数:TempData["PageInfo"] = new { total = total, pageSize = PAGE_SIZE };
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string uiPager3(this HtmlHelper helper,string target)
        {
            if (!helper.ViewContext.TempData.ContainsKey("PageInfo"))
                return string.Empty;

            return helper.uiPager3((PageInfo)helper.ViewContext.TempData["PageInfo"],target);
        }

        public static string uiPager3(this HtmlHelper helper, PageInfo pageInfo, string target)
        {
            if (pageInfo == null)
                return String.Empty;
            IDictionary<string, int> values = pageInfo.ToDictionary<int>();
            int total = pageInfo.Total;
            int pageSize = pageInfo.PageSize == 0 ? DEFAULT_PAGE_SIZE : pageInfo.PageSize;
            int margin = pageInfo.Margin == 0 ? DEFAULT_MARGIN : pageInfo.Margin;

            return helper.uiPager3(total, pageSize, margin, pageInfo.Skip / pageInfo.PageSize == 0 ? pageInfo.Skip / pageInfo.PageSize : pageInfo.Skip / pageInfo.PageSize + 1,target);
        }

        /// <summary>
        /// 显示页码链接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page">当前页码</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="margin">页宽度</param>
        /// <returns></returns>
        public static string uiPager3(this HtmlHelper helper, int total, int pageSize, int margin, int page, string target)
        {
            Guid gid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            if (total == 0)
            {
                sb.Append("<div class=\"ui-corner-all ui-pager\">");
                sb.AppendFormat("<span id=\"pageCount\" >共{0}个记录</span>", total);
                sb.Append("</div>");
                return sb.ToString();

            }

            Uri uri = helper.ViewContext.HttpContext.Request.Url;
            string url = uri.AbsolutePath;
            if (target != "wishes" && target != "diary") { target = "0"; }
            if (uri.Query.Length == 0)
            {
                url += "?p={0}";
            }
            else
            {
                url += uri.Query;
                Regex pr = new Regex(@"([\?&])p=[0-9]+");
                if (pr.IsMatch(uri.Query))
                {
                    url = pr.Replace(url, "$1p={0}");
                }
                else
                    url += "&p={0}";
            }
            if (url.IndexOf('&') > 0)
            {
                url = url.Substring(0, url.IndexOf('&')) + "&plist=" + target;
            }
            else
            {
                url = url + "&plist=" + target;
            }
            url = System.Web.HttpUtility.UrlDecode(url) + "#" + target;

            //int page = 1;
            //int.TryParse(helper.ViewContext.HttpContext.Request.QueryString["p"], out page);

            //页数量
            int pageCount = (total % pageSize) == 0 ? total / pageSize : total / pageSize + 1;
            if (page > pageCount)
                page = pageCount;
            else if (page < 1)
                page = 1;

            //起始页数
            int startPage = page - margin;
            if (startPage < 1)
                startPage = 1;

            //结束页数
            int endPage = page + margin;
            if (endPage > pageCount)
                endPage = pageCount;

            if (startPage == 1 && endPage == 1)
            {
                sb.Append("<div class=\"ui-corner-all ui-pager\">");
                sb.AppendFormat("<span id=\"pageCount\" >共{0}个记录，第{1}页/共{2}页</span>", total, page, pageCount);
                sb.Append("</div>");
                return sb.ToString();
            }

            int prev = page > 1 ? page - 1 : 1;
            int next = page < pageCount ? page + 1 : pageCount;




            sb.Append("<div class=\"ui-corner-all ui-pager\">");
            sb.AppendFormat("<span id=\"pageCount\" >共{0}个记录，第{1}页/共{2}页</span>", total, page, pageCount);
            if (page > 1)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"firstPage" + gid + "\" class=\"link\">{1}</a>", string.Format(url, 1), "首页");

                sb.AppendFormat("<a href=\"{0}\" id=\"prevPage" + gid + "\" class=\"link\">{1}</a>", string.Format(url, prev), "上一页");
            }
            else
            {
                sb.AppendFormat("<a id=\"firstPage" + gid + "\" class=\"link\">{1}</a>", string.Format(url, 1), "首页");

                sb.AppendFormat("<a  id=\"prevPage" + gid + "\" class=\"link\">{1}</a>", string.Format(url, prev), "上一页");
            }

            if (page < pageCount)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"nextPage" + gid + "\" class=\"link\">{1}</a>", string.Format(url, next), "下一页");
                sb.AppendFormat("<a href=\"{0}\" id=\"lastPage" + gid + "\" class=\"link\">{1}</a>", string.Format(url, pageCount), "尾页");
            }
            else
            {
                sb.AppendFormat("<a  id=\"nextPage" + gid + "\" class=\"link\">{1}</a>", string.Format(url, next), "下一页");
                sb.AppendFormat("<a  id=\"lastPage" + gid + "\" class=\"link\">{1}</a>", string.Format(url, pageCount), "尾页");
            }
            sb.Append("跳转到：<input type=\"text\" id=\"pageNum" + gid + "\" value=\"" + page + "\" style=\"width: 64px;\" class=\"text ui-widget-content ui-corner-all\"   onkeydown=\" var regInput=/^\\d+$/; var inpJumpNum=this.value; if(!regInput.exec(inpJumpNum)) inpJumpNum=1; if(inpJumpNum>" + pageCount + "){ inpJumpNum=" + pageCount + "} if(inpJumpNum<=0 ){inpJumpNum=1}  if(event.keyCode==13) { var strHref ='" + string.Format(url, "") + "'; var newstr = strHref.replace('?p=','?p='+inpJumpNum); window.location=newstr; return false;}\" />");
            sb.Append("<a href=\"javascript:void(0)\" id=\"jumpPage" + gid + "\" onclick=\"var jumpNum=document.getElementById('pageNum" + gid + "').value; var reg=/^\\d+$/; if(!reg.exec(jumpNum)) jumpNum=1; if(jumpNum>" + pageCount + "){ jumpNum=" + pageCount + "} if(jumpNum<=0 ){jumpNum=1} var strHref ='" + string.Format(url, "") + "'; var newstr = strHref.replace('?p=','?p='+jumpNum); window.location=newstr; return false;\">跳转</a>");
            sb.Append("</div>");

            return sb.ToString();
        }
        #endregion

        #region 分页样式七
        /// <summary>
        /// 分页控件
        /// 使用时, 必须在Controller中传递以下参数:TempData["PageInfo"] = new { total = total, pageSize = PAGE_SIZE };
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string uiPager4(this HtmlHelper helper, int margin)
        {
            if (!helper.ViewContext.TempData.ContainsKey("PageInfo"))
                return string.Empty;

            return helper.uiPager4((PageInfo)helper.ViewContext.TempData["PageInfo"], margin);
    }

        public static string uiPager4(this HtmlHelper helper, PageInfo pageInfo, int margin)
        {
            if (pageInfo == null)
                return String.Empty;
            IDictionary<string, int> values = pageInfo.ToDictionary<int>();
            int total = pageInfo.Total;
            int pageSize = pageInfo.PageSize == 0 ? DEFAULT_PAGE_SIZE : pageInfo.PageSize;

            if (margin <= 0)
            {
                margin = pageInfo.Margin == 0 ? DEFAULT_MARGIN : pageInfo.Margin;
}

            return helper.uiPager4(total, pageSize, margin, pageInfo.Skip / pageInfo.PageSize == 0 ? pageInfo.Skip / pageInfo.PageSize : pageInfo.Skip / pageInfo.PageSize + 1);
        }

        /// <summary>
        /// 显示页码链接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page">当前页码</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="margin">页宽度</param>
        /// <returns></returns>
        public static string uiPager4(this HtmlHelper helper, int total, int pageSize, int margin, int page)
        {
            Guid gid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            //if (total == 0)
            //{
            //    sb.Append("<ul class=\"pagelist\">");
            //    sb.AppendFormat("<span id=\"pageCount\" >共{0}个记录</span>", total);
            //    sb.Append("</ul>");
            //    return sb.ToString();

            //}

            Uri uri = helper.ViewContext.HttpContext.Request.Url;
            string url = uri.AbsolutePath;

            if (uri.Query.Length == 0)
            {
                url += "?p={0}";
            }
            else
            {
                url += uri.Query;
                Regex pr = new Regex(@"([\?&])p=[0-9]+");
                if (pr.IsMatch(uri.Query))
                {
                    url = pr.Replace(url, "$1p={0}");
                }
                else
                    url += "&p={0}";
            }
            url = System.Web.HttpUtility.UrlDecode(url);

            //int page = 1;
            //int.TryParse(helper.ViewContext.HttpContext.Request.QueryString["p"], out page);

            //页数量
            int pageCount = (total % pageSize) == 0 ? total / pageSize : total / pageSize + 1;
            if (page > pageCount)
                page = pageCount;
            else if (page < 1)
                page = 1;

            //起始页数
            int startPage = page - margin;
            if (startPage < 1)
                startPage = 1;

            //结束页数
            int endPage = page + margin;

            if (endPage - startPage < margin * 2)
            {
                endPage = startPage + margin * 2;
            }

            if (endPage > pageCount)
            {
                endPage = pageCount;

                if (endPage - startPage < margin * 2)
                {
                    startPage = endPage - margin * 2;

                    if (startPage < 1)
                        startPage = 1;
                }
            }



            int prev = page > 1 ? page - 1 : 1;
            int next = page < pageCount ? page + 1 : pageCount;

            sb.Append("<ul class=\"pagelist clearfix\">");
            sb.AppendFormat("<li><a href=\"{0}\" id=\"firstPage" + gid + "\"  >{1}</a></li>"
                , string.Format(url, 1), "首页");


            if (startPage > 1)
            {
                int pp = (startPage - margin - 1);
                pp = pp <= 0 ? pp = 1 : pp;
                sb.AppendFormat("<li><a href=\"{0}\" id=\"selectPage" + pp + "_" + gid + "\"   >{1}</a></li>"
                    , string.Format(url, pp), "...");
            }


            for (int i = startPage; i <= endPage; i++)
            {
                string classHtml = "";
                if (page == i)
                {
                    classHtml = " class=\"thisclass\" ";
                }
                sb.AppendFormat("<li  " + classHtml + "><a href=\"{0}\" id=\"selectPage" + i + "_" + gid + "\" >{1}</a></li>"
                    , string.Format(url, i), i);
            }


            if (endPage < pageCount)
            {
                int pp = (endPage + margin + 1);
                pp = pp > pageCount ? pp = pageCount : pp;
                sb.AppendFormat("<li><a href=\"{0}\" id=\"selectPage" + pp + "_" + gid + "\"   >{1}</a></li>"
                    , string.Format(url, pp), "...");
            }

            sb.AppendFormat("<li><a href=\"{0}\" id=\"lastPage" + gid + "\"  >{1}</a></li>", string.Format(url, pageCount > 0 ? pageCount : 1), "尾页");
            sb.Append("</ul>");
            return sb.ToString();
        }
        #endregion

        #region 分页样式八
        /// <summary>
        /// 分页控件
        /// 使用时, 必须在Controller中传递以下参数:TempData["PageInfo"] = new { total = total, pageSize = PAGE_SIZE };
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string uiPager5(this HtmlHelper helper)
        {
            if (!helper.ViewContext.TempData.ContainsKey("PageInfo"))
                return string.Empty;

            return helper.uiPager5((PageInfo)helper.ViewContext.TempData["PageInfo"]);
        }

        public static string uiPager5(this HtmlHelper helper, PageInfo pageInfo)
        {
            if (pageInfo == null)
                return String.Empty;
            IDictionary<string, int> values = pageInfo.ToDictionary<int>();
            int total = pageInfo.Total;
            int pageSize = pageInfo.PageSize == 0 ? DEFAULT_PAGE_SIZE : pageInfo.PageSize;
            int margin = pageInfo.Margin == 0 ? DEFAULT_MARGIN : pageInfo.Margin;

            return helper.uiPager5(total, pageSize, margin, pageInfo.Skip / pageInfo.PageSize == 0 ? pageInfo.Skip / pageInfo.PageSize : pageInfo.Skip / pageInfo.PageSize + 1);
        }

        /// <summary>
        /// 显示页码链接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="page">当前页码</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageSize">每页记录数量</param>
        /// <param name="margin">页宽度</param>
        /// <returns></returns>
        public static string uiPager5(this HtmlHelper helper, int total, int pageSize, int margin, int page)
        {
            StringBuilder sb = new StringBuilder();
            if (total == 0)
            {
                sb.AppendFormat("共 <strong>{0}</strong> 个礼品", total);
                return sb.ToString();

            }

            Uri uri = helper.ViewContext.HttpContext.Request.Url;
            string url = uri.AbsolutePath;
            if (uri.Query.Length == 0)
                url += "?p={0}";
            else
            {
                url += uri.Query;
                Regex pr = new Regex(@"([\?&])p=[0-9]+");
                if (pr.IsMatch(uri.Query))
                {
                    url = pr.Replace(url, "$1p={0}");
                }
                else
                    url += "&p={0}";
            }
            url = System.Web.HttpUtility.UrlDecode(url);

            //int page = 1;
            //int.TryParse(helper.ViewContext.HttpContext.Request.QueryString["p"], out page);

            //页数量
            int pageCount = (total % pageSize) == 0 ? total / pageSize : total / pageSize + 1;
            if (page > pageCount)
                page = pageCount;
            else if (page < 1)
                page = 1;

            //起始页数
            int startPage = page - margin;
            if (startPage < 1)
                startPage = 1;

            //结束页数
            int endPage = page + margin;
            if (endPage > pageCount)
                endPage = pageCount;

            if (startPage == 1 && endPage == 1)
            {
                sb.AppendFormat("共 <strong>{0}</strong> 个礼品 第{1}页/共{2}页", total, page, pageCount);
                return sb.ToString();
            }

            int prev = page > 1 ? page - 1 : 1;
            int next = page < pageCount ? page + 1 : pageCount;

            sb.AppendFormat("共 <strong>{0}</strong> 个礼品 第{1}页/共{2}页", total, page, pageCount);
            if (page > 1)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"prevPage\" class=\"link\">{1}</a>", string.Format(url, prev), "上一页");
            }
            else
            {
                sb.AppendFormat("<a  id=\"prevPage\" class=\"link\">{1}</a>", string.Format(url, prev), "上一页");
            }

            if (page < pageCount)
            {
                sb.AppendFormat("<a href=\"{0}\" id=\"nextPage\" class=\"link\">{1}</a>", string.Format(url, next), "下一页");
            }
            else
            {
                sb.AppendFormat("<a  id=\"nextPage\" class=\"link\">{1}</a>", string.Format(url, next), "下一页");
            }

            return sb.ToString();
        }
        #endregion
    }
}
