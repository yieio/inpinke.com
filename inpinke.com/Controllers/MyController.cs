using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inpinke.BLL.Filters;
using Inpinke.BLL;
using Inpinke.Model;
using Helper.UI;
using Inpinke.BLL.Session;
using Helper.Web.Filters;
using System.IO;
using System.Text;
using Inpinke.Model.Enum;
using Inpinke.Model.CustomClass;
using inpinke.com.Models;
using Inpinke.BLL.ImageProcess;
using System.Drawing;

namespace inpinke.com.Controllers
{
    public class MyController : PagerController
    {
        //
        // GET: /My/
        [UserFilter]
        public ActionResult Index()
        {
            RegisterModel model = new RegisterModel()
            {
                Email = UserSession.CurrentUser.Email,
                NickName = UserSession.CurrentUser.NickName
            };
            return View(model);
        }
        [UserFilter]
        [HttpPost]
        public ActionResult Index(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Msg = "输入有误，麻烦检测下";

                return View(model);
            }
            Inpinke_User user = DBUserBLL.GetUserByID(UserSession.CurrentUser.ID);
            if (user != null)
            {
                user.Email = model.Email;
                user.Password = model.Password;
                user.NickName = model.NickName;
                BaseResponse br = DBUserBLL.UpdateUser(user);
                if (br.IsSuccess)
                {
                    ViewBag.Msg = "修改成功";
                }
                else
                {
                    ViewBag.Msg = br.Message;
                }
            }
            return View(model);
        }

        [UserFilter]
        [PageInfoFilter(5)]
        public ActionResult Book()
        {
            IList<Inpinke_Book> list = DBBookBLL.GetUserBooks(PageInfo, UserSession.CurrentUser.ID);
            return View(list);
        }
        [UserFilter]
        [PageInfoFilter(10)]
        public ActionResult Order()
        {
            IList<Inpinke_Order> list = DBOrderBLL.GetUserOrder(PageInfo, UserSession.CurrentUser.ID);
            if (list != null && list.Count() > 0)
            {
                ViewBag.OrderList = list;
                int[] orderID = list.Select(e => e.ID).ToArray();
                IList<Inpinke_Order_Product> pList = DBOrderBLL.GetOrderProduct(orderID);
                ViewBag.OrderProductList = pList;
            }
            return View();
        }
        [UserFilter]
        public ActionResult Photo()
        {
            return View();
        }
        [UserFilter]
        [PageInfoFilter(10)]
        public ActionResult Coupon()
        {
            IList<Inpinke_Coupon_Code> list = DBCouponBLL.GetUserCoupon(UserSession.CurrentUser.ID);
            if (list != null && list.Count() > 0)
            {
                ViewBag.CouponList = list;
            }
            return View();
        }
        [UserFilter]
        [PageInfoFilter(10)]
        public ActionResult Address()
        {
            IList<Inpinke_User_Address> list = DBAddressBLL.GetUserAddress(PageInfo, UserSession.CurrentUser.ID);
            if (list != null && list.Count() > 0)
            {
                ViewBag.Address = list;
            }
            return View();
        }
        [UserFilter]
        [PageInfoFilter(10)]
        public ActionResult Message()
        {
            IList<Inpinke_Message> list = DBMessageBLL.GetUserMessage(PageInfo, UserSession.CurrentUser.ID);
            if (list != null && list.Count() > 0)
            {
                ViewBag.MsgList = list;
            }
            return View();
        }
        /// <summary>
        /// 对消息进行咨询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [UserFilter]
        public ActionResult ReplyMsg(int id, FormCollection col)
        {
            string msg = col["Msg"];
            if (string.IsNullOrEmpty(msg))
            {
                msg = "内容不能为空，描述下您想咨询的事情吧！";
                return RedirectToAction("msgdetail", new { id = id, msg = msg });
            }
            int touser = string.IsNullOrEmpty(col["ToUser"]) ? 0 : int.Parse(col["ToUser"]);
            Inpinke_Message model = new Inpinke_Message()
            {
                ReplyID = id,
                Msg = col["Msg"],
                FromUser = UserSession.CurrentUser.ID,
                ToUser = touser,
                MsgType = (int)MsgType.UserReply
            };
            BaseResponse br = DBMessageBLL.SendMessage(model);
            return RedirectToAction("msgdetail", new { id = id, msg = br.Message });
        }

        /// <summary>
        /// 消息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UserFilter]
        public ActionResult MsgDetail(int id, string msg)
        {
            IList<Inpinke_Message> list = DBMessageBLL.GetMsgDetail(id, UserSession.CurrentUser.ID);
            if (list != null && list.Count() > 0)
            {
                ViewBag.MsgDetail = list;
            }
            ViewBag.Msg = msg;
            return View();
        }
        /// <summary>
        /// 获取用户图片
        /// </summary>
        /// <param name="num"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [PageInfoFilter(12)]
        public ActionResult GetUserImages(int num, int p)
        {
            if (UserSession.CurrentUser == null)
            {
                return Content("");
            }

            IList<Inpinke_Image> list = DBImageBLL.GetUserImages(PageInfo, UserSession.CurrentUser.ID);
            if (list != null)
            {
                List<pagedata> photoList = new List<pagedata>();
                foreach (Inpinke_Image i in list)
                {
                    pagedata d = new pagedata()
                    {
                        id = i.ID,
                        bigImg ="/userfile/" + UserSession.CurrentUser.ID + "/" + i.ImageName + "_edit.jpg",
                        img = i.Path,
                        title = string.IsNullOrEmpty(i.Remark) ? "" : i.Remark,
                    };
                    string imgPath = Server.MapPath(d.img);
                    d.img = "/userfile/" + UserSession.CurrentUser.ID + "/" + i.ImageName + "_view.jpg";
                    string vimgPath = Server.MapPath(d.img);
                    if (!System.IO.File.Exists(imgPath))
                    {
                        continue;
                    }
                    if (!System.IO.File.Exists(vimgPath))
                    {
                        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(imgPath);
                        if (originalImage != null)
                        {
                            Bitmap bitmap = new Bitmap(originalImage);
                            ImageProcessBLL.CreateStaticScaleImage(bitmap, 220, 1, 1000, vimgPath);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    photoList.Add(d);
                }
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(photoList.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, photoList);
                    return Content(Encoding.UTF8.GetString(ms.ToArray()));
                }
            }
            else
            {
                return Content("");
            }

        }
        /// <summary>
        /// 删除用户图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteUserImage(int id)
        {
            if (UserSession.CurrentUser == null)
            {
                return Content("{\"success\":false,\"msg\":\"请重新登录\"}");
            }
            Inpinke_Image model = DBImageBLL.GetImageByID(id);
            if (model.UserID != UserSession.CurrentUser.ID)
            {
                return Content("{\"success\":false,\"msg\":\"请重新登录\"}");
            }
            model.Status = (int)RecordStatus.Delete;
            BaseResponse br = DBImageBLL.DeleteUserImage(model);
            if (br.IsSuccess)
            {
                return Content("{\"success\":true,id:" + model.ID + ",\"msg\":\"删除用户图片成功\"}");
            }
            else
            {
                return Content("{\"success\":false,\"msg\":\"" + br.Message + "\"}");
            }
        }
        /// <summary>
        /// 设置默认收货人
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AjaxSetDefaultAddress(int id)
        {
            if (UserSession.CurrentUser == null)
            {
                return Content("{\"success\":false,\"msg\":\"请重新登录\"}");
            }
            BaseResponse br = DBAddressBLL.SetDefualtAddress(id, UserSession.CurrentUser.ID);
            return Content("{\"success\":" + br.IsSuccess.ToString().ToLower() + ",id:" + id + ",\"msg\":\"" + br.Message + "\"}");
        }
        /// <summary>
        /// 删除收货人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AjaxDeleteAddress(int id)
        {
            if (UserSession.CurrentUser == null)
            {
                return Content("{\"success\":false,\"msg\":\"请重新登录\"}");
            }
            BaseResponse br = DBAddressBLL.DeleteAddress(id);
            return Content("{\"success\":" + br.IsSuccess.ToString().ToLower() + ",id:" + id + ",\"msg\":\"" + br.Message + "\"}");
        }

        /// <summary>
        /// 设置印品是否分享
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public ActionResult AjaxSetBookShowStatus(int bookid)
        {
            if (UserSession.CurrentUser == null)
            {
                return Content("{\"success\":false,\"msg\":\"请重新登录\"}");
            }
            Inpinke_Book model = DBBookBLL.GetBookByID(bookid, UserSession.CurrentUser.ID);
            if (model == null)
            {
                return Content("{\"success\":false,\"msg\":\"印品分享设置失败\",\"bookid\":" + bookid + "}");
            }
            if (model.ShowStatus == (int)ShowStatus.Public)
            {
                model.ShowStatus = (int)ShowStatus.Pravice;
            }
            else
            {
                model.ShowStatus = (int)ShowStatus.Public;
            }
            BaseResponse br = DBBookBLL.UpdateBook(model);
            return Content("{\"success\":" + br.IsSuccess.ToString().ToLower() + ",\"msg\":\"" + br.Message + "\",\"bookid\":" + bookid + "}");

        }

        #region 印品相关操作
        /// <summary>
        /// 复制一件印品
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        [UserFilter]
        public ActionResult CopyBook(int bookid)
        {
            Inpinke_Book model = DBBookBLL.GetBookByID(bookid);
            if (model.UserID != UserSession.CurrentUser.ID)
            {
                ViewBag.Msg = "对不起，您不能拷贝该印品。";
                return View("error");
            }
            if (model.DonePages * 100 / model.PageCount < 90)
            {
                ViewBag.Msg = "对不起，当前印品完成度太低，不建议拷贝。您可以直接编辑该印品。";
                return View("error");
            }
            BaseResponse br = DBBookBLL.CopyBook(bookid, UserSession.CurrentUser.ID);
            if (!br.IsSuccess)
            {
                ViewBag.Msg = br.Message;
                return View("error");
            }
            else
            {
                return RedirectToAction("book", "my");
            }
        }

        /// <summary>
        /// 删除印品
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        [UserFilter]
        public ActionResult DelBook(int bookid)
        {
            Inpinke_Book model = DBBookBLL.GetBookByID(bookid, UserSession.CurrentUser.ID);
            if (model == null)
            {
                ViewBag.Msg = "对不起，您不能删除当前印品";
                return View("error");
            }
            model.Status = (int)RecordStatus.Delete;
            BaseResponse br = DBBookBLL.UpdateBook(model);
            if (br.IsSuccess)
            {
                return RedirectToAction("book", "my");
            }
            else
            {
                ViewBag.Msg = br.Message;
                return View("error");
            }
        }
        #endregion
    }
}
