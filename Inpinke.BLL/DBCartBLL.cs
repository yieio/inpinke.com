using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Inpinke.Model;
using Inpinke.Model.CustomClass;
using Inpinke.Model.Enum;

namespace Inpinke.BLL
{
    public class DBCartBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBCartBLL));
        /// <summary>
        /// 根据ID获取购物车上的具体一件商品
        /// </summary>
        /// <param name="cartid"></param>
        /// <returns></returns>
        public static Inpinke_Cart GetCartProductByID(int cartid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Carts.Get(e => e.ID == cartid);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetCartByID CartID:{0},Error:{1}", cartid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据用户id和印品id获取购物车商品
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static Inpinke_Cart GetCartProduct(int userid, int bookid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Carts.Get(e => e.UserID == userid && e.BookID == bookid);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetCartProduct UserID:{0},BookID:{1},Error:{2}", userid, bookid, ex.ToString()));
                return null;
            }
        }

        /// <summary>
        /// 添加书到购物车
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse AddBook2Cart(Inpinke_Cart model)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                Inpinke_Cart oldModel = GetCartProduct(model.UserID, model.BookID);
                if (oldModel != null)
                {
                    oldModel.Envelope = model.Envelope;
                    oldModel.Num = model.Num <= 0 ? 1 : model.Num;
                    oldModel.CouponID = model.CouponID;
                    oldModel.UpdateTime = DateTime.Now;
                    oldModel.SaveWhenSubmit(InpinkeDataContext.Instance);
                }
                else
                {
                    model.Num = model.Num <= 0 ? 1 : model.Num;
                    model.CreateTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    model.InsertWhenSubmit(InpinkeDataContext.Instance);
                }
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.ResponseObj = model;
                return br;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("AddBook2Cart Error:{0}", ex.ToString()));
                br.IsSuccess = false;
                br.Message = "添加印品到购物车出错，请稍后再试";
                return br;
            }
        }
        /// <summary>
        /// 根据用户ID获取用户购物车信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Cart> GetUserCart(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Carts.Where(e => e.UserID == userid).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserCart UserID:{0} Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 获取购物车中商品总价
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static decimal GetCartTotalPrices(int userid)
        {
            IList<Inpinke_Cart> cartList = GetUserCart(userid);
            decimal totalPrices = 0;
            decimal plusPrices = 0;
            decimal couponPrices = 0;
            foreach (Inpinke_Cart c in cartList)
            {
                //书本算加页价格
                if (c.Inpinke_Product.ProductType == (int)ProductType.PhotoBook)
                {
                    plusPrices = c.Inpinke_Product.PlusPrice * ((c.Inpinke_Book.PageCount - c.Inpinke_Product.BasePages) / c.Inpinke_Product.PlusPages);
                }
                totalPrices += (c.Inpinke_Product.Price + plusPrices) * c.Num;
                //优惠折扣
                if (c.CouponID != 0)
                {
                    Inpinke_Coupon coupon = DBCouponBLL.GetCouponByID(c.CouponID);
                    if (coupon.StartTime <= DateTime.Now && coupon.EndTime >= DateTime.Now)
                    {
                        couponPrices = coupon.DiscountPostage + coupon.DiscountPrice;
                    }
                }
                totalPrices -= couponPrices;
            }
            totalPrices = totalPrices < 0 ? 0 : totalPrices;
            return totalPrices;
        }
        /// <summary>
        /// 清空用户购物车
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static BaseResponse ClearUserCart(int userid)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                string delSql = string.Format("delete Inpinke_Cart where userid={0}", userid);
                InpinkeDataContext.Instance.ExecuteCommand(delSql);
                br.IsSuccess = true;
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "清空用户购物车失败，请稍后再试";
                Logger.Error(string.Format("ClearUserCart UserID:{0},Error:{1}", userid, ex.ToString()));
            }
            return br;
        }
        /// <summary>
        /// 更新购物车商品信息
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public static BaseResponse UpdateUserCart(Inpinke_Cart cart)
        {
            BaseResponse br = new BaseResponse();
            try
            {
                cart.UpdateTime = DateTime.Now;
                cart.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = false;
                br.ResponseObj = cart;
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "购物车商品信息更新失败，请稍后再试";
                Logger.Error(string.Format("UpdateUserCart UserID:{0},Error:{1}", cart.UserID, ex.ToString()));
            }
            return br;
        }
        /// <summary>
        /// 删除用户购物车中的某件印品
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static BaseResponse DeleteUserCartProduct(int userid, int bookid)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                string delSql = string.Format("delete Inpinke_Cart where userid={0} and bookid={1}", userid,bookid);
                InpinkeDataContext.Instance.ExecuteCommand(delSql);
                br.IsSuccess = true;
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "清空用户购物车印品失败，请稍后再试";
                Logger.Error(string.Format("DeleteUserCartProduct UserID:{0}，BookID:{2},Error:{1}", userid, ex.ToString(),bookid));
            }
            return br;
        }
        /// <summary>
        /// 获取用户购物车商品数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetUserCartProductCount(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from Inpinke_Cart where userid={0}", userid, (int)RecordStatus.Nomral)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserCartProductCount UserID:{0},Error:{1}", userid, ex.ToString()));
                return 0;
            }
        }
    }
}
