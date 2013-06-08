using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inpinke.Model.CustomClass;
using Inpinke.Model;
using Inpinke.BLL;
using Helper.UI;
using Helper.Web.Filters;
using Inpinke.BLL.Filters;
using log4net;
using Inpinke.Model.Enum;
using Inpinke.BLL.Session;

namespace admin.inpinke.com.Controllers
{
    public class OrderController : PagerController
    {

        public static readonly ILog Logger = LogManager.GetLogger(typeof(OrderController));
        //
        // GET: /Order/
        [AdminFilter]
        [PageInfoFilter(20)]
        public ActionResult Index(OrderQueryModels query, string msg)
        {
            ViewBag.Msg = msg;
            IList<Inpinke_Order> list = DBOrderBLL.GetOrderByQueryModels(PageInfo, query);
            ViewBag.QueryModel = query;
            return View(list);
        }

        [AdminFilter]
        public ActionResult Edit(int id, string msg)
        {
            ViewBag.Msg = msg;
            Inpinke_Order model = DBOrderBLL.GetOrderByID(id);
            if (model == null)
            {
                return RedirectToAction("index", new { msg = "未找到要编辑的订单" });
            }
            //获取订单礼品
            ViewBag.OrderProducts = DBOrderBLL.GetOrderProduct(model.ID);
            return View(model);
        }

        [AdminFilter]
        [HttpPost]
        public ActionResult Edit(FormCollection col)
        {
            int orderid = int.Parse(col["ID"]);
            try
            {

                Inpinke_Order model = DBOrderBLL.GetOrderByID(orderid);
                ViewBag.Order = model;
                IList<Inpinke_Order_Product> prodList = DBOrderBLL.GetOrderProduct(model.ID);
                ViewBag.ProductList = prodList;

                decimal totalPrice = 0;
                foreach (Inpinke_Order_Product p in prodList)
                {
                    int bookid = p.BookID;
                    if (string.IsNullOrEmpty(col["num_" + bookid]))
                    {
                        totalPrice += p.Price;
                        continue;
                    }
                    p.Num = int.Parse(col["num_" + bookid]);
                    if (p.Num <= 0)
                    {
                        ViewBag.Msg = "印品购买数量不能少于1件";
                        return RedirectToAction("edit", new { id = orderid, msg = ViewBag.Msg + DateTime.Now.ToString("yyyy-MM-dd mm:ss") });
                    }
                    p.CouponID = int.Parse(col["coupon_select_" + bookid]);
                    p.Envelope = int.Parse(col["envelope_" + bookid]);
                    p.Price = DBBookBLL.GetBookPrice(bookid, p.CouponID, p.Num);
                    p.UpdateTime = DateTime.Now;
                    p.SaveWhenSubmit(InpinkeDataContext.Instance);
                    totalPrice += p.Price;
                }
                model.OrgPrice = totalPrice;
                model.TotalPrice = totalPrice;
                int orgOrderStatus = model.OrderStatus;
                model.OrderStatus = int.Parse(col["OrderStatus"]);
                model.UserRemark = col["Remark"];
                //收货人信息 
                model.RecUserName = col["UserName"];
                model.RecMobile = col["Mobile"];
                model.RecAddress = col["Address"];
                model.RecAreaID = int.Parse(col["AreaID"]);
                model.RecProvID = int.Parse(col["ProvID"]);
                model.RecCityID = int.Parse(col["CityID"]);
                model.RecProvName = DBProvinceBLL.GetProvName(model.RecProvID.Value);
                model.RecCityName = DBProvinceBLL.GetCityName(model.RecCityID.Value);
                model.RecAreaName = DBProvinceBLL.GetAreaName(model.RecAreaID.Value);

                model.UpdateTime = DateTime.Now;
                model.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                //锁定印品防止编辑
                if (model.OrderStatus == (int)OrderStatus.WaitSend && orgOrderStatus != (int)OrderStatus.WaitSend)
                {
                     SetOrderMaking(model, "", true);
                }
                if (model.OrderStatus != (int)OrderStatus.WaitSend && orgOrderStatus == (int)OrderStatus.WaitSend)
                {
                    SetOrderMaking(model, "", false);
                }

                return RedirectToAction("edit", new { id = orderid, msg = "编辑成功" + DateTime.Now.ToString("yyyy-MM-dd mm:ss") });
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "修改订单信息失败，请稍后再试";
                Logger.Error(string.Format("修改订单信息-Edit Error:{0}", ex.ToString()));
                return RedirectToAction("edit", new { id = orderid, msg = ViewBag.Msg + DateTime.Now.ToString("yyyy-MM-dd mm:ss") });
            }
        }

        /// <summary>
        /// 设置订单关联印品状态
        /// </summary>
        /// <param name="o_model"></param>
        /// <param name="adminname"></param>
        /// <param name="isMaking"></param>
        private void SetOrderMaking(Inpinke_Order o_model, string adminname, bool isMaking)
        {
            int bookStatus = isMaking ? (int)BookStatus.Making : (int)BookStatus.Create;
            if (o_model.Inpinke_Order_Products != null)
            {
                foreach (Inpinke_Order_Product p in o_model.Inpinke_Order_Products)
                {
                    //把印品设置为印刷中，防止编辑
                    if (p.BookID != 0 && p.Inpinke_Book != null)
                    {
                        p.Inpinke_Book.BookStauts = bookStatus;
                        p.Inpinke_Book.SaveWhenSubmit(InpinkeDataContext.Instance);
                    }
                }
            }
        }
    }
}

