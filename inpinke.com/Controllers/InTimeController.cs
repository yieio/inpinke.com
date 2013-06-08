using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inpinke.Model.CustomClass;
using Newtonsoft.Json;
using Inpinke.Model;
using Inpinke.BLL;
using Inpinke.Model.Enum;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using Inpinke.Helper;
using System.Text.RegularExpressions;
using Inpinke.BLL.ImageProcess;
using System.IO;
using Inpinke.BLL.PDFProcess;
using System.Drawing;
using Inpinke.BLL.Filters;
using Inpinke.BLL.Session;
using log4net;
using System.Collections;


namespace inpinke.com.Controllers
{
    public class InTimeController : Controller
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(InTimeController));
        //
        // GET: /InTime/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Intime编辑器
        /// </summary>
        /// <returns></returns>
        [UserFilter]
        public ActionResult Editor(int bookid)
        {
            Inpinke_Book model = DBBookBLL.GetBookByID(bookid);
            if (model != null)
            {
                if (model.BookStauts == (int)BookStatus.Making)
                {
                    ViewBag.Msg = "印品已下单印刷不能再做修改，您可以拷贝副本进行编辑/联系客服寻求帮助";
                    return View("error");
                }
                if (model.UserID != UserSession.CurrentUser.ID)
                {
                    ViewBag.Msg = "对不起，您不能编辑该印品，因为那件印品好像不属于您。";
                    return View("error");
                }
                ViewBag.EditBook = model;
            }
            else
            {
                ViewBag.Msg = "对不起，没有找到您要编辑的印品。";
                return View("error");
            }
            return View();
        }
        /// <summary>
        /// 添加一本新书
        /// </summary>
        /// <returns></returns>
        [UserFilter]
        public ActionResult CreateIntime(int prodid)
        {
            Inpinke_Product intime = DBProductBLL.GetProductByID(prodid);
            Inpinke_Book model = new Inpinke_Book()
            {
                UserID = UserSession.CurrentUser.ID,
                BookName = "我的时光",
                Author = UserSession.CurrentUser.NickName,
                BookDesc = "那些过往的时光，穿行在我的字里行间。无声的行走。无休。",
                PageCount = intime.BasePages,
                ProductID = prodid,
                ShowStatus = (int)ShowStatus.Public,
                BookCover = "/Content/pagestyle/images/intime_cover.png"
            };
            BaseResponse br = DBBookBLL.AddBook(model);
            if (br.IsSuccess)
            {
                return RedirectToAction("editor", new { bookid = model.ID });
            }
            else
            {
                ViewBag.Msg = "对不起，定制印品失败，您可以重新登录再尝试下。";
                return View("error");
            }
        }
       

        
        /// <summary>
        /// 生成Intime书本的PDF文件
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public ActionResult CreateIntimePDF(int bookid)
        {
            InTimePDFBLL.CreateBookPDF(bookid);
            return Content(true.ToString());
        }
    }
}
