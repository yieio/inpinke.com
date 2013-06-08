using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Helper.UI;
using Helper.Web.Filters;
using Inpinke.Model.CustomClass;
using Inpinke.Model;
using Inpinke.BLL;
using Inpinke.BLL.Filters;
using Inpinke.BLL.Session;

namespace admin.inpinke.com.Controllers
{
    public class UserController : PagerController
    {
        //
        // GET: /User/
        [AdminFilter]
        [PageInfoFilter(50)]       
        public ActionResult Index(UserQueryModels q)
        {
            ViewBag.QueryModel = q;
            IList<Inpinke_User> list = DBUserBLL.GetUserByQueryModel(PageInfo, q);
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            Inpinke_User model = DBUserBLL.GetUserByID(id);
            return View("Create", model);
        }

        public ActionResult ResetPassword(int userid, string password)
        {
            if (AdminSession.CurrentUser == null)
            {
                return Content("{\"success\":false,\"msg\":\"您没有权限进行此操作，请重新登录\"}");
            }

            Inpinke_User model = DBUserBLL.GetUserByID(userid);
            if (model == null)
            {
                return Content("{\"success\":false,\"msg\":\"未找到对应的用户\"}");
            }
            model.Password = password;
            BaseResponse br = DBUserBLL.UpdateUser(model);

            return Content("{\"success\":" + br.IsSuccess.ToString().ToLower() + ",\"msg\":\"" + br.Message + "\"}");
        }
    }
}
