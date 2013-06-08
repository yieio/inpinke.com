using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace inpinke.com.Controllers
{
    public class InPinController : Controller
    {
        //
        // GET: /InPin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Compare()
        {
            return View();
        }

        public ActionResult PriceList()
        {
            return View();
        }

        public ActionResult DealRecorder()
        {
            return View();
        }

        /// <summary>
        /// 制作印品
        /// </summary>
        /// <param name="prodid"></param>
        /// <returns></returns>
        public ActionResult Create(int prodid)
        {

            return View();
        }

    }
}
