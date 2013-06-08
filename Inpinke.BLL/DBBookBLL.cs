using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpinke.Model;
using log4net;
using Inpinke.Model.Enum;
using Inpinke.Model.CustomClass;
using Helper.UI;

namespace Inpinke.BLL
{
    public class DBBookBLL
    {

        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBBookBLL));
        /// <summary>
        /// 创建照片书
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse AddBook(Inpinke_Book model)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = true;
            try
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.Status = (int)RecordStatus.Nomral;
                model.BookStauts = (int)BookStatus.Create;
                model.InsertWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.Message = "创建照片书成功";
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "创建照片书失败，请稍后再试";
                Logger.Error(string.Format("AddBook Error:{0}", ex.ToString()));
            }
            return br;
        }
        /// <summary>
        /// 拷贝一件印品
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static BaseResponse CopyBook(int bookid, int userid)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                //1.拷贝印品信息
                string cSql = string.Format("insert Inpinke_Book(BookName,SubBookName,Author,BookFont,BookDesc,BookCover,BookThumb,BookView,"
                                       + " UserID,BookStauts,PageCount,DonePages,ProductID,CreateTime,UpdateTime,ShowStatus,Status) "
                                       + " select BookName,SubBookName,Author,BookFont,BookDesc,BookCover,BookThumb,BookView, "
                                       + " UserID,BookStauts,PageCount,DonePages,ProductID,GETDATE(),GETDATE(),ShowStatus,Status "
                                       + " from Inpinke_Book where ID={0} ", bookid);
                int r = InpinkeDataContext.Instance.ExecuteCommand(cSql);
                int maxID = GetUserMaxBookID(userid);
                if (r <= 0 || maxID <= 0)
                {
                    br.IsSuccess = false;
                    br.Message = "拷贝书本失败";
                    return br;
                }
                cSql = string.Format("insert Inpinke_Book_Page(BookID,PageNum,IsSkip,PageData,PageImg,PageTxt,PageStatus,CreateTime,UpdateTime,Status)"
                                  + " select {1},PageNum,IsSkip,PageData,PageImg,PageTxt,PageStatus,GETDATE(),GETDATE(),Status "
                                  + " from Inpinke_Book_Page where BookID={0}", bookid, maxID);
                r = InpinkeDataContext.Instance.ExecuteCommand(cSql);
                if (r <= 0)
                {
                    br.IsSuccess = false;
                    br.Message = "拷贝书本页面失败";
                    return br;
                }
                cSql = string.Format("insert Inpinke_Book_Image(BookID,ImageID,UsedNum) "
                                  + " select {1},ImageID,UsedNum  from Inpinke_Book_Image where BookID={0}", bookid, maxID);
                r = InpinkeDataContext.Instance.ExecuteCommand(cSql);
                if (r <= 0)
                {
                    br.IsSuccess = false;
                    br.Message = "拷贝书本图片失败";
                    return br;
                }
                br.IsSuccess = true;
                return br;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("CopyBook BookID:{0},UserID:{1},Error:{2}", bookid, userid, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "复制印品失败，请稍后再试";
                return br;
            }

        }
        /// <summary>
        /// 获取用户最大的bookid
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetUserMaxBookID(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Books.Where(e => e.UserID == userid).Select(e => e.ID).Max();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserMaxBookID UserID:{0} Error:{1}", userid, ex.ToString()));
                return 0;
            }
        }
        /// <summary>
        /// 根据书本ID获取一本书
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static Inpinke_Book GetBookByID(int bookid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Books.Get(e => e.ID == bookid);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetBookByID BookID:{0},Error:{1}", bookid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据书本id和用户id获取印品
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Inpinke_Book GetBookByID(int bookid, int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Books.Get(e => e.ID == bookid && e.UserID == userid);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetBookByID BookID:{0}，UserID:{1},Error:{2}", bookid, userid, ex.ToString()));
                return null;
            }
        }

        /// <summary>
        /// 获取书本页面内容
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Book_Page> GetBookPage(int bookid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Book_Pages.Where(e => e.BookID == bookid && e.Status == (int)RecordStatus.Nomral && e.PageStatus != (int)PageStatus.Delete).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetBookPage BookID:{0},Error:{1}", bookid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据ID获取书页内容
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public static Inpinke_Book_Page GetBookPageByID(int pageid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Book_Pages.Get(e => e.ID == pageid);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetBookPageByID PageID:{0},Error:{1}", pageid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据页码获取书本页面
        /// </summary>
        /// <param name="pnum"></param>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static Inpinke_Book_Page GetBookPageByPNum(int pnum, int bookid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Book_Pages.Get(e => e.PageNum == pnum && e.BookID == bookid && e.Status == (int)RecordStatus.Nomral);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetBookPageByID BookID:{0},PageNum:{1},Error:{2}", bookid, pnum, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据页码数组批量获取页面
        /// </summary>
        /// <param name="pnums"></param>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Book_Page> GetBookPageByPNums(string[] pnums, int bookid)
        {
            try
            {
                IList<Inpinke_Book_Page> list = InpinkeDataContext.Instance.Inpinke_Book_Pages.Where(e => pnums.Contains(e.PageNum.ToString()) && e.BookID == bookid && e.Status == (int)RecordStatus.Nomral && e.PageStatus != (int)PageStatus.Delete).ToList();
                return list;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetBookPageByPNums BookID:{0},Error:{1}", bookid, ex.ToString()));
                return null;
            }
        }

        /// <summary>
        /// 根据页码获取页面
        /// </summary>
        /// <param name="pnums"></param>
        /// <param name="bookid"></param>
        /// <param name="hasDel">是否包含删除页面</param>
        /// <returns></returns>
        public static IList<Inpinke_Book_Page> GetBookPageByPNums(string[] pnums, int bookid, bool hasDel)
        {
            try
            {
                IList<Inpinke_Book_Page> list = InpinkeDataContext.Instance.Inpinke_Book_Pages.Where(e => pnums.Contains(e.PageNum.ToString()) && e.BookID == bookid && e.Status == (int)RecordStatus.Nomral && (hasDel ? true : e.PageStatus != (int)PageStatus.Delete)).ToList();

                return list;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetBookPageByPNums BookID:{0},Error:{1}", bookid, ex.ToString()));
                return null;
            }
        }

        /// <summary>
        /// 获取有图片预览的页面
        /// </summary>
        /// <returns></returns>
        public static IList<Inpinke_Book_Page> GetBookPageByPViews(int bookid)
        {
            try
            {
                IList<Inpinke_Book_Page> list = InpinkeDataContext.Instance.Inpinke_Book_Pages.Where(e => e.BookID == bookid && e.PageImg.Length > 0 && e.Status == (int)RecordStatus.Nomral && e.PageStatus == (int)PageStatus.Normal).ToList();
                return list;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetBookPageByPViews BookID:{0},Error:{1}", bookid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 添加新页面
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static BaseResponse AddBookPage(Inpinke_Book_Page page)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                page.CreateTime = DateTime.Now;
                page.UpdateTime = DateTime.Now;
                page.Status = (int)RecordStatus.Nomral;
                page.InsertWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.ResponseObj = page;
                br.Message = "添加页面成功";
                return br;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("AddBookPage BookID:{0},PageNum:{1},Error:{2}", page.BookID, page.PageNum, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "添加页面失败请稍后再试";
                return br;
            }
        }
        /// <summary>
        /// 更新页面内容
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static BaseResponse UpdateBookPage(Inpinke_Book_Page page)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                page.UpdateTime = DateTime.Now;
                page.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.ResponseObj = page;
                br.Message = "更新页面成功";
                return br;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("UpdateBookPage BookID:{0},PageNum:{1},Error:{2}", page.BookID, page.PageNum, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "更新页面失败请稍后再试";
                return br;
            }
        }
        /// <summary>
        /// 获取印品价格
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="couponid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal GetBookPrice(int bookid, int couponid, int num)
        {
            Inpinke_Book c = DBBookBLL.GetBookByID(bookid);
            if (c == null)
            {
                throw new Exception();
            }
            int plusPages = c.PageCount - c.Inpinke_Product.BasePages;
            plusPages = plusPages < 0 ? 0 : plusPages;
            decimal price = (c.Inpinke_Product.Price + (plusPages) * (c.Inpinke_Product.PlusPrice / c.Inpinke_Product.PlusPages)) * num;
            decimal couponPrices = 0;
            if (couponid != 0)
            {
                Inpinke_Coupon coupon = DBCouponBLL.GetCouponByID(couponid);
                if (coupon == null)
                {
                    couponPrices = 0;
                }
                if (coupon.StartTime <= DateTime.Now && coupon.EndTime >= DateTime.Now)
                {
                    couponPrices = coupon.DiscountPostage + coupon.DiscountPrice;
                }
            }
            price -= couponPrices;
            price = price < 0 ? 0 : price;
            return price;

        }
        /// <summary>
        /// 获取用户印品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IList<Inpinke_Book> GetUserBooks(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Books.Where(e => e.UserID == userid).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserBooks UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 分页获取用户照片书
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Book> GetUserBooks(PageInfo pageInfo, int userid)
        {
            try
            {
                var q = from b in InpinkeDataContext.Instance.Inpinke_Books
                        where b.Status == (int)RecordStatus.Nomral && b.UserID == userid
                        orderby b.CreateTime descending
                        select b;
                pageInfo.Total = GetUserBooksCount(userid);
                return q.Skip(pageInfo.Skip).Take(pageInfo.PageSize).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserBooks UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 获取用户照片数总数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetUserBooksCount(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from Inpinke_Book where userid={0} and status={1}", userid, (int)RecordStatus.Nomral)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserBooksCount UserID:{0},Error:{1}", userid, ex.ToString()));
                return 0;
            }
        }
        /// <summary>
        /// 更新书本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse UpdateBook(Inpinke_Book model)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                model.UpdateTime = DateTime.Now;
                model.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.Message = "书本信息更新成功";
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "书本信息更新失败，请稍后再试";
                Logger.Error(string.Format("UpdateBook Error:{0},BookID:{1}", ex.ToString(), model.ID));
            }
            return br;
        }
        /// <summary>
        /// 删除书本页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse DeleteBookPage(Inpinke_Book_Page model)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                model.PageStatus = (int)PageStatus.Delete;
                model.UpdateTime = DateTime.Now;
                model.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
                br.Message = "书本信息更新成功";
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "书本信息更新失败，请稍后再试";
                Logger.Error(string.Format("DeleteBookPage Error:{0},BookID:{1}", ex.ToString(), model.ID));
            }
            return br;
        }
        /// <summary>
        /// 删除书本指定的页面
        /// </summary>
        /// <param name="pnums">多页用逗号隔开</param>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static BaseResponse DeleteBookPage(string pnums, int bookid)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                string delQ = string.Format("update Inpinke_Book_Page set pagestatus={0} where bookid={1} and pagenum in({2})", (int)PageStatus.Delete, bookid, pnums);
                InpinkeDataContext.Instance.ExecuteCommand(delQ);
                br.IsSuccess = true;
                br.Message = "书本信息更新成功";
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "书本信息更新失败，请稍后再试";
                Logger.Error(string.Format("DeleteBookPage Error:{0},BookID:{1},PageNums:{2}", ex.ToString(), bookid, pnums));
            }
            return br;
        }
        /// <summary>
        /// 获取页面库最大编号
        /// </summary>
        /// <returns></returns>
        public static int GetMaxBookPageID()
        {
            int num = 0;
            num = InpinkeDataContext.Instance.Inpinke_Book_Pages.Select(e => e.ID).Max();
            return num;
        }
        /// <summary>
        /// 更新完成的页数
        /// </summary>
        /// <param name="bookid"></param>
        public static void UpdateDonePage(int bookid)
        {
            try
            {
                Inpinke_Book model = GetBookByID(bookid);
                IList<Inpinke_Book_Page> list = GetBookPage(bookid);
                int donePage = 0;
                if (list != null)
                {
                    foreach (Inpinke_Book_Page p in list)
                    {
                        if (p.PageNum == 0)
                        {
                            continue;
                        }
                        if (p.IsSkip)
                        {
                            donePage += 2;
                        }
                        else
                        {
                            donePage++;
                        }
                    }
                    model.DonePages = donePage;
                }
                model.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("UpdateDonePage BookID:{0}, Error:{1}", bookid, ex.ToString()));
            }
        }
        /// <summary>
        /// 获取推荐的印品
        /// </summary>
        /// <param name="p">分页信息</param>
        /// <param name="gname">组名</param>
        /// <returns></returns>
        public static IList<Inpinke_Book> GetRecommendBook(PageInfo p, string gname)
        {
            //IList<Inpinke_Recommend_Book> list = InpinkeDataContext.Instance.Inpinke_Recommend_Books.Where(e => e.GroupName == gname).ToList();
            //string qSql = string.Format("select * from Inpinke_Book where id in(select bookid from Inpinke_Recommend_Book where groupname={0})", gname);
            return null;
        }
        /// <summary>
        /// 获取推荐的印品
        /// </summary>
        /// <param name="gname"></param>
        /// <returns></returns>
        public static IList<Inpinke_Book> GetRecommendBook(string gname)
        {
            try
            {
                string qSql = string.Format("select * from Inpinke_Book where id in(select bookid from Inpinke_Recommend_Book where groupname='{0}') and status={1} and showstatus={2}", gname, (int)RecordStatus.Nomral, (int)ShowStatus.Public);
                return InpinkeDataContext.Instance.ExecuteQuery<Inpinke_Book>(qSql).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetRecommendBook GroupName:{0},Error:{1}", gname, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 随机获取一本印品展示
        /// </summary>
        /// <returns></returns>
        public static Inpinke_Book GetRandomShowBook()
        {
            try
            {
                string qSql = string.Format("select top 1 * from Inpinke_Book where DonePages*100/PageCount>90 and ShowStatus={0} and Status={1} order by NEWID()", (int)RecordStatus.Nomral, (int)ShowStatus.Public);
                return InpinkeDataContext.Instance.ExecuteQuery<Inpinke_Book>(qSql).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetRandomShowBook Error:{0}", ex.ToString()));
                return null;
            }
        }
    }
}
