using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inpinke.Model;
using Inpinke.BLL;
using Inpinke.BLL.Session;
using Inpinke.Model.Enum;

namespace inpinke.com.Controllers
{
    public class ShowController : Controller
    {
        //
        // GET: /Show/

        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                Inpinke_Book model = DBBookBLL.GetBookByID(id.Value);
                if (model.ShowStatus == (int)ShowStatus.Pravice)
                {
                    if (UserSession.CurrentUser != null && UserSession.CurrentUser.ID == model.UserID)
                    {
                        ViewBag.ShowBook = model;
                        ViewBag.ShowBookPage = DBBookBLL.GetBookPage(id.Value);
                        return View();
                    }
                    else
                    {
                        ViewBag.Msg = "对不起，当前印品不对外展出，您无法查看。";
                        return View("error");
                    }
                }
                else
                {
                    ViewBag.ShowBook = model;
                    ViewBag.ShowBookPage = DBBookBLL.GetBookPage(id.Value);
                    return View();
                }
            }
            else
            {
                ViewBag.Msg = "对不起，访问出错！";
                return View("error");
            }
        }

        //public ActionResult Error(string msg)
        //{
        //    if (string.IsNullOrEmpty(msg))
        //    {
        //        ViewBag.Msg = msg;
        //    }
        //    return View();
        //}

        public ActionResult Random()
        {
            Inpinke_Book model = DBBookBLL.GetRandomShowBook();
            ViewBag.ShowBook = model;
            ViewBag.ShowBookPage = DBBookBLL.GetBookPage(model.ID);
            return View("index");
        }
 
    }
}
