using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Inpinke.Model.Enum;
using Inpinke.Model.CustomClass;
using Inpinke.Model;
using Helper.UI;
using Inpinke.Model.DataAccess;
using Helper;

namespace Inpinke.BLL
{
    public class DBOrderBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBOrderBLL));
        /// <summary>
        /// 根据订单ID获取用户订单
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static Inpinke_Order GetOrderByID(int orderid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Orders.Get(e => e.ID == orderid);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetOrderByID OrderID:{0},Error:{1}", orderid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <param name="prodtype"></param>
        /// <returns></returns>
        public static string GetOrderCode(int prodtype)
        {
            string ordercode = "";
            DateTime now = DateTime.Now;
            Random rand = new Random();
            int r = rand.Next(now.Second, 99999);
            string dateStr = now.ToString("yyMMddss");
            string randStr = r.ToString().PadLeft(5, '0');
            switch (prodtype)
            {
                case (int)ProductType.LomoCard: ordercode = "L";
                    break;
                case (int)ProductType.Envelope: ordercode = "E";
                    break;
                case (int)ProductType.PhotoBook: ordercode = "B";
                    break;
                case (int)ProductType.PlusStuff: ordercode = "S";
                    break;
                default: ordercode = "O";
                    break;
            }
            ordercode += dateStr + "-" + randStr;
            return ordercode;
        }
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse AddOrder(Inpinke_Order model)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.Status = (int)RecordStatus.Nomral;
                model.InsertWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                if (AddOrderProduct(model.ID, model.UserID).IsSuccess)
                {
                    //清空购物车
                    DBCartBLL.ClearUserCart(model.UserID);
                    br.IsSuccess = true;
                    br.ResponseObj = model;
                }
                else
                {
                    br.IsSuccess = false;
                    br.Message = "添加订单礼品失败";
                    br.ResponseObj = model;
                }
                return br;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("AddOrder UserID:{0}, Error:{1}", model.UserID, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "添加订单失败，请稍后再试";
                return br;
            }
        }
        /// <summary>
        /// 添加订单产品
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="userid"></param>
        public static BaseResponse AddOrderProduct(int orderid, int userid)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                IList<Inpinke_Cart> cartList = DBCartBLL.GetUserCart(userid);
                if (cartList != null)
                {
                    foreach (Inpinke_Cart c in cartList)
                    {
                        Inpinke_Order_Product order = new Inpinke_Order_Product()
                        {
                            BookID = c.BookID,
                            CouponID = c.CouponID,
                            CreateTime = DateTime.Now,
                            Envelope = c.Envelope,
                            OrderID = orderid,
                            PlusID = c.PlusID,
                            Price = DBBookBLL.GetBookPrice(c.BookID, c.CouponID, c.Num),
                            ProductID = c.ProductID,
                            UserID = userid,
                            Num = c.Num,
                            UpdateTime = DateTime.Now
                        };
                        order.InsertWhenSubmit(InpinkeDataContext.Instance);
                    }
                    InpinkeDataContext.Instance.Submit();
                    br.IsSuccess = true;
                    br.ResponseObj = cartList;
                }
                else
                {
                    br.IsSuccess = false;
                    br.Message = "购物车里没有商品";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("AddOrderProduct OrderID:{0},Error:{1}", orderid, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "添加订单礼品失败，请稍后再试";
            }
            return br;
        }
        /// <summary>
        /// 获取订单商品信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Order_Product> GetOrderProduct(int orderid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Order_Products.Where(e => e.OrderID == orderid).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetOrderProduct OrderID:{0},Error:{1}", orderid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 获取订单礼品
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Order_Product> GetOrderProduct(int[] orderid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Order_Products.Where(e => orderid.Contains(e.OrderID)).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetOrderProduct OrderID:{0},Error:{1}", orderid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 获取用户订单
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Order> GetUserOrder(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Orders.Where(e => e.UserID == userid && e.Status == (int)RecordStatus.Nomral).OrderByDescending(e => e.CreateTime).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserOrder UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 分页获取用户订单
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Order> GetUserOrder(PageInfo pInfo, int userid)
        {
            try
            {
                var q = from t in InpinkeDataContext.Instance.Inpinke_Orders
                        where t.UserID == userid && t.Status == (int)RecordStatus.Nomral
                        orderby t.CreateTime descending
                        select t;
                pInfo.Total = InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from Inpinke_Order where userid={0}", userid)).FirstOrDefault();
                return q.Skip(pInfo.Skip).Take(pInfo.PageSize).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserOrder UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 获取用户订单数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetUserOrderCount(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from Inpinke_Order where userid={0} and status={1}", userid, (int)RecordStatus.Nomral)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserOrderCount UserID:{0},Error:{1}", userid, ex.ToString()));
                return 0;
            }
        }
        /// <summary>
        /// 根据订单编号获取订单
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public static Inpinke_Order GetOrderByCode(string ordercode)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Orders.Get(e => e.OrderCode == ordercode);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetOrderByCode OrderCode:{0},Error:{1}", ordercode, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 订单支付成功相关操作
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public static BaseResponse OrderPaySuccess(string ordercode, decimal totalfee, string buyerinfo)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                Inpinke_Order o_model = DBOrderBLL.GetOrderByCode(ordercode);
                if (o_model.OrderStatus == (int)OrderStatus.Create)
                {
                    o_model.OrderStatus = (int)OrderStatus.WaitSend;
                    o_model.PayTime = DateTime.Now;
                    o_model.PayMethod = (int)PayMethod.Alipay;
                    o_model.TotalFee = totalfee;
                    o_model.BuyerInfo = buyerinfo;
                    if (o_model.Inpinke_Order_Products != null)
                    {
                        foreach (Inpinke_Order_Product p in o_model.Inpinke_Order_Products)
                        {
                            //把印品设置为印刷中，防止编辑
                            if (p.BookID != 0 && p.Inpinke_Book != null)
                            {
                                p.Inpinke_Book.BookStauts = (int)BookStatus.Making;
                                p.Inpinke_Book.SaveWhenSubmit(InpinkeDataContext.Instance);
                            }
                        }
                    }
                    o_model.SaveWhenSubmit(InpinkeDataContext.Instance);
                    InpinkeDataContext.Instance.Submit();
                    br.IsSuccess = true;
                    br.Message = "订单处理成功";
                }
                else
                {
                    br.IsSuccess = false;
                    br.Message = "订单状态异常";
                    Logger.Info(string.Format("OrderPaySuccess：" + br.Message + "。订单号：" + o_model.OrderCode));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("OrderPaySuccess OrderCode:{0}, Error:{1}", ordercode, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "订单处理失败，请稍后再试";
            }
            return br;
        }
        /// <summary>
        /// 设置订单为印制中锁定对应印品防止修改
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public static BaseResponse SetOrderMaking(string ordercode, string adminname,bool isMaking)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                Inpinke_Order o_model = DBOrderBLL.GetOrderByCode(ordercode);
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
                o_model.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.Message = "订单处理成功";
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("SetOrderMaking OrderCode:{0}, Error:{1}", ordercode, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "订单处理失败，请稍后再试";
            }
            return br;
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="qModel"></param>
        /// <returns></returns>
        public static IList<Inpinke_Order> GetOrderByQueryModels(PageInfo pInfo, OrderQueryModels qModel)
        {
            try
            {
                IList<Inpinke_Order> list = new List<Inpinke_Order>();
                string field = " O.* ";
                string table = " Inpinke_Order O left join Inpinke_User U on O.UserID=U.ID ";
                string where = FormatQModel.FormatQueryModel<OrderQueryModels>(qModel);
                string orderby = " O.ID desc";

                string countQ = PagerHelper.GetCountSQL(table, where);
                string qSql = PagerHelper.GetPager(table, pInfo.Skip, pInfo.PageSize, field, orderby, where);
                pInfo.Total = InpinkeDataContext.Instance.ExecuteQuery<int>(countQ).FirstOrDefault();
                list = InpinkeDataContext.Instance.ExecuteQuery<Inpinke_Order>(qSql).ToList();

                return list;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetOrderByQueryModels Error:{0}", ex.ToString()));
                return null;
            }
        }
    }
}
