using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inpinke.Model.CustomClass;
using Inpinke.BLL;
using Inpinke.BLL.Session;
using Inpinke.Model;

namespace admin.inpinke.com.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(string f)
        {
            ViewBag.ReturnUrl = f;
            return View("Login");
        }
        [HttpPost]
        public ActionResult Index(string username, string password, string f)
        {
            BaseResponse br = DBAdminBLL.ValidateUser(username, password);
            if (br.IsSuccess)
            {
                AdminSession.CurrentUser = br.ResponseObj as Inpinke_Admin;
            }
            else
            {
                ViewBag.Msg = "用户名/密码不正确";
                return View("Login");
            }
            if (string.IsNullOrEmpty(f))
            {
                return View("index", "home");
            }
            else
            {
                return Redirect(f);
            }
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
