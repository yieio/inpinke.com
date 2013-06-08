using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace inpinke.com.Controllers
{    
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error(string msg)
        {
            ViewBag.Msg = msg;
            return View();
        }
    }
}
