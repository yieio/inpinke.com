using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inpinke.BLL;

namespace inpinke.com.Controllers
{
    public class ShelfController : Controller
    {
        //
        // GET: /Shelf/

        public ActionResult Index()
        {            
            ViewBag.RecomBooks = DBBookBLL.GetRecommendBook(DateTime.Now.ToString("yyyy-MM"));
            return View();
        }

    }
}
