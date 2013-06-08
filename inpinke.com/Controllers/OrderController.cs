using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inpinke.Model;
using Inpinke.BLL;
using Inpinke.Model.Enum;
using Inpinke.Model.CustomClass;
using Inpinke.BLL.Filters;
using Inpinke.BLL.Session;
using inpinke.com.Models;
using log4net;

namespace inpinke.com.Controllers
{
    public class OrderController : Controller
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(OrderController));

        //
        // GET: /Order/
        [UserFilter]
        public ActionResult Index(string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                ViewBag.Msg = msg;
            }
            IList<Inpinke_Cart> list = DBCartBLL.GetUserCart(UserSession.CurrentUser.ID);
            return View(list);
        }
        /// <summary>
        /// 提交确认的商品信息
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        [UserFilter]
        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            IList<Inpinke_Cart> list = DBCartBLL.GetUserCart(UserSession.CurrentUser.ID);
            try
            {
                string bookids = col["bookids"];
                if (string.IsNullOrEmpty(bookids))
                {
                    ViewBag.Msg = "商品信息异常，请重新确认";
                    return View(list);
                }
                //bookids = bookids.TrimEnd(',');
                string[] bookidInfo = bookids.Split(',');
                foreach (string bookid in bookidInfo)
                {
                    Inpinke_Book bModel = DBBookBLL.GetBookByID(int.Parse(bookid));
                    if (bModel != null)
                    {
                        Inpinke_Cart model = new Inpinke_Cart()
                        {
                            BookID = int.Parse(bookid),
                            Num = int.Parse(col["num_" + bookid]),
                            CouponID = int.Parse(col["coupon_select_" + bookid]),
                            Envelope = int.Parse(col["envelope_" + bookid]),
                            ProductID = bModel.ProductID,
                            UserID = UserSession.CurrentUser.ID
                        };
                        BaseResponse br = DBCartBLL.AddBook2Cart(model);
                        if (!br.IsSuccess)
                        {
                            ViewBag.Msg = "商品信息异常，请重新确认";
                            return View(list);
                        }
                    }
                }
                return RedirectToAction("OrderInfo");
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "商品信息确认失败，请稍后再试";
                Logger.Error(string.Format("购物车-Index Error:{0}", ex.ToString()));
                return View(list);
            }
        }
        /// <summary>
        /// 异步保存购物车商品信息
        /// </summary>
        /// <param name="cartInfo"></param>
        /// <returns></returns>
        public ActionResult AjaxUpdateCartInfo(CartModels cartInfo)
        {
            Inpinke_Cart cart = DBCartBLL.GetCartProductByID(cartInfo.ID);
            if (cart == null)
            {
                return Content("{\"success\":false,\"message\":\"未找到对应的商品\"}");
            }
            cart.Envelope = cartInfo.Envelope;
            cart.CouponID = cartInfo.CouponID;
            cart.Num = cartInfo.Num;
            BaseResponse br = DBCartBLL.UpdateUserCart(cart);
            if (!br.IsSuccess)
            {
                return Content("{\"success\":false,\"message\":\"" + br.Message + "\"}");
            }
            return Content("{\"success\":true}");
        }
        [UserFilter]
        public ActionResult OrderInfo()
        {
            IList<Inpinke_User_Address> address = DBAddressBLL.GetUserAddress(UserSession.CurrentUser.ID);
            if (address != null && address.Count() > 0)
            {
                ViewBag.UserAddress = address;
            }
            return View();
        }

        [HttpPost]
        [UserFilter]
        public ActionResult OrderInfo(OrderInfoModel orderInfo)
        {
            IList<Inpinke_User_Address> addressList = DBAddressBLL.GetUserAddress(UserSession.CurrentUser.ID);
            if (addressList != null && addressList.Count() > 0)
            {
                ViewBag.UserAddress = addressList;
            }
            Inpinke_User_Address address = new Inpinke_User_Address();
            if (orderInfo.AddressID > 0)
            {
                address = addressList.Where(e => e.ID == orderInfo.AddressID).FirstOrDefault();
                if (address == null)
                {
                    ViewBag.Msg = "不存在该收货人信息，请重新填写";
                    return View();
                }
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Msg = "输入信息有误，麻烦再检查一下";
                    return View();
                }
                address = new Inpinke_User_Address()
                {
                    Address = orderInfo.Address,
                    AreaID = orderInfo.AreaID,
                    ProvID = orderInfo.ProvID,
                    CityID = orderInfo.CityID,
                    Mobile = orderInfo.Mobile,
                    UserID = UserSession.CurrentUser.ID,
                    UserName = orderInfo.Consignee
                };
                BaseResponse br = DBAddressBLL.AddUserAddress(address);
                if (!br.IsSuccess)
                {
                    ViewBag.Msg = br.Message;
                    return View();
                }                
            }
            IList<Inpinke_Cart> cartList = DBCartBLL.GetUserCart(UserSession.CurrentUser.ID);
            decimal orderPrice = DBCartBLL.GetCartTotalPrices(UserSession.CurrentUser.ID);
            Inpinke_Order order = new Inpinke_Order()
            {
                OrderCode = DBOrderBLL.GetOrderCode(cartList[0].Inpinke_Product.ProductType),
                AddressID = address.ID,
                
                RecUserName = address.UserName,
                RecMobile = address.Mobile,
                RecProvID = address.ProvID,
                RecCityID = address.CityID,
                RecAreaID = address.AreaID,
                RecAddress = address.Address,
                RecAreaName = address.AreaName,
                RecCityName = address.CityName,
                RecProvName = address.ProvName,

                UserID = UserSession.CurrentUser.ID,
                OrgPrice = orderPrice,
                TotalPrice = orderPrice,
                UserRemark = orderInfo.Remark
            };

            BaseResponse br1 = DBOrderBLL.AddOrder(order);
            if (!br1.IsSuccess)
            {
                ViewBag.Msg = br1.Message;
                return View();
            }
            return RedirectToAction("Pay", new { orderid = order.ID });
        }
        [UserFilter]
        public ActionResult Pay(int orderid)
        {
            Inpinke_Order model = DBOrderBLL.GetOrderByID(orderid);
            if (model == null)
            {
                //跳转到出错页面
                return Redirect("/error.htm?msg=对不起没有找到您要支付的订单");
            }
            ViewBag.Order = model;
            IList<Inpinke_Order_Product> prodList = DBOrderBLL.GetOrderProduct(model.ID);
            if (prodList == null)
            {
                //跳转到出错页面
                return Redirect("/error.htm?msg=对不起没有找到您要支付的订单");
            }
            ViewBag.ProductList = prodList;
            IList<Inpinke_User_Address> address = DBAddressBLL.GetUserAddress(UserSession.CurrentUser.ID);
            if (address != null && address.Count() > 0)
            {
                ViewBag.UserAddress = address;
            }
            return View();
        }
        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [UserFilter]
        public ActionResult Buy(int orderid)
        {
            return View();
        }
        [UserFilter]
        public ActionResult Detail(int orderid)
        {
            Inpinke_Order model = DBOrderBLL.GetOrderByID(orderid);
            IList<Inpinke_Order_Product> prodList = DBOrderBLL.GetOrderProduct(model.ID);
            if (prodList == null)
            {
                //跳转到出错页面
                return Redirect("/error.htm?msg=对不起没有找到您要支付的订单");
            }
            ViewBag.ProductList = prodList;
            ViewBag.Order = model;
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }
        /// <summary>
        /// 添加书本到购物车
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public ActionResult AddBook2Cart(int bookid)
        {
            Inpinke_Book book = DBBookBLL.GetBookByID(bookid);
            if (book == null)
            {
                ViewBag.Msg = "Sorry！没有找到您要购买的印品";
                return RedirectToAction("Index", new { msg = ViewBag.Msg });
            }

            Inpinke_Cart myCart = new Inpinke_Cart()
            {
                UserID = book.UserID,
                BookID = book.ID,
                Num = 1,
                ProductID = book.ProductID
            };
            IList<Inpinke_Product> plusList = DBProductBLL.GetPlusProduct(book.ProductID, ProductType.Envelope);
            if (plusList != null && plusList.Count() > 0)
            {
                myCart.Envelope = plusList.FirstOrDefault().ID;
            }
            IList<Inpinke_Product> plusList1 = DBProductBLL.GetPlusProduct(book.ProductID, ProductType.PlusStuff);
            if (plusList1 != null && plusList1.Count() > 0)
            {
                myCart.Envelope = plusList1.FirstOrDefault().ID;
            }

            BaseResponse br = DBCartBLL.AddBook2Cart(myCart);
            if (!br.IsSuccess)
            {
                ViewBag.Msg = br.Message;
            }
            return RedirectToAction("Index", new { msg = ViewBag.Msg });
        }
        /// <summary>
        /// 获取地址
        /// </summary>
        /// <param name="addressid"></param>
        /// <returns></returns>
        public ActionResult AjaxGetAddress(int addressid)
        {
            Inpinke_User_Address address = DBAddressBLL.GetAddressByID(addressid);
            if (address == null)
            {
                return Content("{\"success\":false,\"message\":\"未找到对应的收货人信息\"}");
            }
            return Content("{\"success\":true,\"id\":" + address.ID + ",\"consignee\":\"" + address.UserName + "\",\"mobile\":\"" + address.Mobile + "\",\"provid\":" + address.ProvID + ",\"cityid\":" + address.CityID + ",\"areaid\":" + address.AreaID + ",\"address\":\"" + address.Address + "\"}");
        }

        [HttpPost]
        public ActionResult AjaxUpdateAddress(OrderInfoModel model)
        {
            if (UserSession.CurrentUser == null)
            {
                return Content("{\"success\":false,\"message\":\"请先登录\"}");
            }
            if (!ModelState.IsValid)
            {
                return Content("{\"success\":false,\"message\":\"收货人信息有误，麻烦检查下\"}");
            }
            Inpinke_User_Address address = new Inpinke_User_Address()
            {
                Address = model.Address,
                AreaID = model.AreaID,
                ProvID = model.ProvID,
                CityID = model.CityID,
                Mobile = model.Mobile,
                UserID = UserSession.CurrentUser.ID,
                UserName = model.Consignee
            };
            BaseResponse br = new BaseResponse();
            string result = "";
            if (model.AddressID != 0)
            {
                address = DBAddressBLL.GetAddressByID(model.AddressID);
                address.Address = model.Address;
                address.AreaID = model.AreaID;
                address.ProvID = model.ProvID;
                address.CityID = model.CityID;
                address.Mobile = model.Mobile;
                address.UserID = UserSession.CurrentUser.ID;
                address.UserName = model.Consignee;
                if (address == null)
                {
                    return Content("{\"success\":false,\"message\":\"更新收货人信息失败\"}");
                }

                br = DBAddressBLL.UpdateUserAddress(address);
            }
            else
            {
                br = DBAddressBLL.AddUserAddress(address);
            }
            if (br.IsSuccess)
            {
                result = "{\"success\":true,\"id\":" + address.ID + ",\"consignee\":\"" + address.UserName + "\",\"mobile\":\"" + address.Mobile + "\",\"provid\":" + address.ProvID + ",\"cityid\":" + address.CityID + ",\"areaid\":" + address.AreaID + ",\"address\":\"" + address.Address + "\",\"provname\":\"" + address.ProvName + "\",\"cityname\":\"" + address.CityName + "\",\"areaname\":\"" + address.AreaName + "\"}";
            }
            else
            {
                result = "{\"success\":false,\"message\":\"" + br.Message + "\"}";
            }
            return Content(result);
        }
        /// <summary>
        /// 删除购物车印品
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public ActionResult AjaxDeleteCartProduct(int bookid)
        {
            if (UserSession.CurrentUser == null)
            {
                return Content("{\"success\":false,\"message\":\"请先登录\"}");
            }
            BaseResponse br = DBCartBLL.DeleteUserCartProduct(UserSession.CurrentUser.ID, bookid);
            return Content("{\"success\":" + br.IsSuccess.ToString().ToLower() + ",\"message\":\"" + br.Message + "\",\"bookid\":" + bookid + "}");
        }
        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        [UserFilter]
        public ActionResult Modify(int orderid)
        {
            Inpinke_Order model = DBOrderBLL.GetOrderByID(orderid);
            if (model == null)
            {
                //跳转到出错页面
                return Redirect("/error.htm?msg=对不起没有找到您要修改的订单");
            }
            ViewBag.Order = model;
            IList<Inpinke_Order_Product> prodList = DBOrderBLL.GetOrderProduct(model.ID);
            if (prodList == null)
            {
                //跳转到出错页面
                return Redirect("/error.htm?msg=对不起没有找到您要修改的订单");
            }
            ViewBag.ProductList = prodList;
            IList<Inpinke_User_Address> address = DBAddressBLL.GetUserAddress(UserSession.CurrentUser.ID);
            if (address != null && address.Count() > 0)
            {
                ViewBag.UserAddress = address;
            }
            return View();
        }
        [HttpPost]
        [UserFilter]
        public ActionResult Modify(FormCollection col)
        {
            try
            {
                int orderid = int.Parse(col["ID"]);
                Inpinke_Order model = DBOrderBLL.GetOrderByID(orderid);
                ViewBag.Order = model;
                IList<Inpinke_Order_Product> prodList = DBOrderBLL.GetOrderProduct(model.ID);
                ViewBag.ProductList = prodList;
                IList<Inpinke_User_Address> address = DBAddressBLL.GetUserAddress(UserSession.CurrentUser.ID);
                if (address != null && address.Count() > 0)
                {
                    ViewBag.UserAddress = address;
                }
                
                model.AddressID = int.Parse(col["AddressID"]);
                Inpinke_User_Address selAddress = DBAddressBLL.GetAddressByID(model.AddressID);
                model.RecUserName = selAddress.UserName;
                model.RecMobile = selAddress.Mobile;
                model.RecAddress = selAddress.Address;
                model.RecAreaID = selAddress.AreaID;
                model.RecProvID =selAddress.ProvID;
                model.RecCityID = selAddress.CityID;
                model.RecProvName = selAddress.ProvName;
                model.RecCityName = selAddress.CityName;
                model.RecAreaName = selAddress.AreaName;
                
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
                        return View();
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
                model.UserRemark = col["Remark"];
                model.UpdateTime = DateTime.Now;
                model.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                return RedirectToAction("pay", new { orderid = orderid });
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "修改订单信息失败，请稍后再试";
                Logger.Error(string.Format("修改订单信息-Modify Error:{0}", ex.ToString()));
                return View("error");
            }

        }
    }
}
