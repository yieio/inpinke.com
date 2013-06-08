using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using inpinke.com.Models;
using log4net;
using Inpinke.Model;
using Inpinke.BLL;
using Inpinke.Model.CustomClass;
using Inpinke.BLL.Session;
using Inpinke.BLL.Filters;
using Helper;
using Inpinke.Helper;

namespace inpinke.com.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index(string f)
        {
            return RedirectToAction("login", "account", new { f = f });
        }
    }


    public class AccountController : Controller
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(AccountController));

        public ActionResult Agreement()
        {
            return View();
        }
        [UserFilter]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn(string f)
        {
            if (string.IsNullOrEmpty(f) && Request.UrlReferrer != null)
            {
                f = Request.UrlReferrer.AbsolutePath;
            }
            ViewBag.ReturnUrl = f;
            return View();
        }

        public ActionResult Login(string f)
        {
            if (string.IsNullOrEmpty(f) && Request.UrlReferrer != null)
            {
                f = Request.UrlReferrer.PathAndQuery;
            }
            ViewBag.ReturnUrl = f;
            return View("logon");
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string f)
        {
            if (ModelState.IsValid)
            {
                BaseResponse br = DBUserBLL.ValidateUser(model.Email, model.Password);
                if (br.IsSuccess)
                {
                    //添加session
                    Inpinke_User loginUser = br.ResponseObj as Inpinke_User;
                    UserSession.CurrentUser = loginUser;
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    //设置友言单点登录cookie
                    //http://api.uyan.cc?mode=des&uid={0}&uname={1}&email={2}&uface={3}&ulink={4}&expire=3600&key=inpinke20130417
                    string loginStr = ConfigHelper.ReadConfig("UyanApi", "configuration/DESApiUrl");
                    loginStr = string.Format(loginStr, loginUser.ID, loginUser.NickName, loginUser.Email, "", "");
                    string CookieMi = UYanBLL.GetMi(loginStr);
                    string cookieName = ConfigHelper.ReadConfig("UyanApi", "configuration/CookieName");
                    HttpCookie cookie = new HttpCookie(cookieName);
                    cookie.Value = CookieMi;
                    //HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(f) && f.Length > 1 && f.StartsWith("/")
                        && !f.StartsWith("//") && !f.StartsWith("/\\"))
                    {
                        return Redirect(f);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "电子邮箱或密码不正确");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            //FormsAuthentication.SignOut();
            UserSession.ClearUserSession();
            return RedirectToAction("index", "home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (DBUserBLL.CheckEmailIsExist(model.Email, 0).IsSuccess)
                {
                    ModelState.AddModelError("Email", "该邮箱已经注册过了");
                    return View(model);
                }

                Inpinke_User user = new Inpinke_User()
                {
                    NickName = model.NickName.Trim(),
                    Password = model.Password.Trim(),
                    Email = model.Email.Trim()
                };
                BaseResponse br = DBUserBLL.CreateUser(user);
                if (!br.IsSuccess)
                {
                    ViewBag.Msg = br.Message;
                    return View(model);
                }
                if (br.IsSuccess)
                {
                    FormsAuthentication.SetAuthCookie(model.NickName, false);
                    return RedirectToAction("index", "home");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePassword(string v)
        {
            ViewBag.Validate = v;
            //判断重设验证码是否过期
            Inpinke_User user = DBUserBLL.GetUserByValidateCode(v);
            if (user == null)
            {
                ViewBag.Msg = "对不起重设密码链接已过期，请点击<a href=\"/account/resetpassword\">[重新获取]</a>";
                return View("error");
            }
            else
            {
                MD5Encrypt md5 = new MD5Encrypt();
                if (v != md5.GetMD5FromString(user.Email + DateTime.Now.ToString("yyyyMMdd")))
                {
                    ViewBag.Msg = "对不起重设密码链接已过期，请点击<a href=\"/account/resetpassword\">[重新获取]</a>";
                    return View("error");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model, string ValidateCode)
        {
            if (ModelState.IsValid)
            {
                Inpinke_User user = DBUserBLL.GetUserByValidateCode(ValidateCode);
                if (user != null)
                {
                    MD5Encrypt md5 = new MD5Encrypt();
                    if (ValidateCode != md5.GetMD5FromString(user.Email + DateTime.Now.ToString("yyyyMMdd")))
                    {
                        ViewBag.Msg = "对不起重设密码链接已过期，请点击<a href=\"/account/resetpassword\">[重新获取]</a>";
                        return View("error");
                    }
                    else
                    {
                        user.Password = model.ConfirmPassword;
                        DBUserBLL.UpdateUser(user);
                        //修改密码成功调整
                        ViewBag.Msg = "重设密码成功，请使用新密码重新登录";
                        return View("logon");
                    }

                }
                else
                {
                    ViewBag.Msg = "对不起重设密码链接已过期，请点击<a href=\"/account/resetpassword\">[重新获取]</a>";
                    return View("error");
                }
            }
            else
            {
                ModelState.AddModelError("Password", "密码最少6位");
            }
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public JsonResult AjaxCheckEmailIsUsed(string email)
        {
            BaseResponse br = DBUserBLL.CheckEmailIsExist(email, 0);
            return Json(!br.IsSuccess, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                BaseResponse br = DBUserBLL.CheckEmailIsExist(model.Email, 0);
                if (br.IsSuccess && br.ResponseObj != null)
                {
                    Inpinke_User user = br.ResponseObj as Inpinke_User;
                    //重置验证码生成规则，用户邮箱加上当前日期，所以每个码的有效期都是一天
                    MD5Encrypt md5 = new MD5Encrypt();
                    string validate = md5.GetMD5FromString(user.Email + DateTime.Now.ToString("yyyyMMdd"));
                    string mailTemplate = ConfigHelper.ReadConfig("EmailTemplate", "configuration/ResetPassword");
                    user.ValidateCode = validate;
                    DBUserBLL.UpdateUser(user);

                    ViewBag.Email = model.Email;
                    mailTemplate = EmailHelper.ReplaceTemplateVar<Inpinke_User>(mailTemplate, user);
                    EmailHelper.SendEmail(user.Email, mailTemplate, "印品客账户重设密码");
                    return View("ResetNotice");
                }
                else
                {
                    ModelState.AddModelError("Email", "不存在当前邮箱账户");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Email", "请填写正确的邮箱");
                return View(model);
            }
        }

    }
}
