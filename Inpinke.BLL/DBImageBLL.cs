using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inpinke.Model;
using Inpinke.Model.CustomClass;
using Inpinke.Model.Enum;
using log4net;
using Helper.UI;
using System.Xml.Linq;
using Inpinke.Helper;

namespace Inpinke.BLL
{
    public class DBImageBLL
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(DBImageBLL));
        /// <summary>
        /// 添加用户图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse AddUserImage(Inpinke_Image model)
        {
            BaseResponse br = new BaseResponse();
            try
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.Status = (int)RecordStatus.Nomral;
                model.InsertWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.ResponseObj = model;
                br.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("AddBookImage UserID:{0},Error:{1}", model.UserID, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "添加用户图片失败，请稍后再试";
            }
            return br;
        }
        /// <summary>
        /// 添加书本图片
        /// </summary>
        /// <param name="bImg"></param>
        /// <returns></returns>
        public static BaseResponse AddBookImage(Inpinke_Book_Image bImg)
        {
            BaseResponse br = new BaseResponse();
            try
            {
                bImg.InsertWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.ResponseObj = bImg;
                br.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("AddBookImage ImageID:{0},BookID:{1},Error:{2}", bImg.ImageID, bImg.BookID, ex.ToString()));
                br.IsSuccess = false;
                br.Message = "添加书本图片失败，请稍后再试";
            }
            return br;

        }
        /// <summary>
        /// 获取书本照片
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="count"></param>
        /// <param name="total"></param>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public static IList<Inpinke_Book_Image> GetBookImages(int skip, int count, ref int total, int bookid)
        {
            try
            {
                var q = from t in InpinkeDataContext.Instance.Inpinke_Book_Images
                        where t.BookID == bookid
                        orderby t.ID descending
                        select t;
                total = InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from Inpinke_Book_Image where bookid={0}", bookid)).FirstOrDefault();
                return q.Skip(skip).Take(count).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetBookImages Skip:{0},Count:{1},BookID:{2},Error:{3}", skip, count, bookid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 根据图片id获取图片
        /// </summary>
        /// <param name="imgid"></param>
        /// <returns></returns>
        public static Inpinke_Image GetImageByID(int imgid)
        {
            try
            {
                return InpinkeDataContext.Instance.Inpinke_Images.Get(e => e.ID == imgid && e.Status == (int)RecordStatus.Nomral);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetImageByID ImageID:{0},Error:{1}", imgid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 分页获取用户图片
        /// </summary>
        /// <param name="PageInfo"></param>
        /// <param name="userid"></param>
        public static IList<Inpinke_Image> GetUserImages(PageInfo PageInfo, int userid)
        {
            try
            {
                var q = from t in InpinkeDataContext.Instance.Inpinke_Images
                        where t.UserID == userid && t.Status == (int)RecordStatus.Nomral
                        orderby t.CreateTime descending
                        select t;
                PageInfo.Total = InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(Count(*),0) from Inpinke_Image where userid={0} and status={1}", userid, (int)RecordStatus.Nomral)).FirstOrDefault();
                return q.Skip(PageInfo.Skip).Take(PageInfo.PageSize).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("GetUserImages UserID:{0},Error:{1}", userid, ex.ToString()));
                return null;
            }
        }
        /// <summary>
        /// 删除用户图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BaseResponse DeleteUserImage(Inpinke_Image model)
        {
            BaseResponse br = new BaseResponse();
            try
            {
                model.Status = (int)RecordStatus.Delete;
                model.UpdateTime = DateTime.Now;
                model.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                br.IsSuccess = true;
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "删除用户图片失败，请稍后再试";

            }
            return br;
        }
        /// <summary>
        /// 删除书本图片
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="bookid"></param>
        /// <param name="imageid"></param>
        /// <returns></returns>
        public static BaseResponse DeleteBookImage(int userid, int bookid, int imageid)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                Inpinke_Book_Image model = InpinkeDataContext.Instance.Inpinke_Book_Images.Get(e => e.BookID == bookid && e.ImageID == imageid);
                if (model != null)
                {
                    if (model.Inpinke_Book.UserID == userid)
                    {
                        model.DeleteWhenSubmit(InpinkeDataContext.Instance);
                        InpinkeDataContext.Instance.Submit();
                        br.IsSuccess = true;
                        br.Message = "图片删除成功";
                    }
                    else
                    {
                        br.IsSuccess = false;
                        br.Message = "用户身份验证失败，图片删除失败";
                    }
                }
                else
                {
                    br.IsSuccess = false;
                    br.Message = "未找到要删除的书本图片";
                }
                return br;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("DeleteBookImage Userid:{0},BookID:{1},imageid:{2},Error:{3}", userid, bookid, imageid, ex.ToString()));
                br.Message = "书本照片删除失败，请稍后再试";
                return br;
            }
        }
        /// <summary>
        /// 修改书本图片使用次数
        /// </summary>
        /// <param name="pdata"></param>
        /// <param name="bookid"></param>
        /// <param name="isReduce"></param>
        /// <returns></returns>
        public static BaseResponse ChangeImageUsedNum(XElement pdata, int bookid, bool isReduce, ref List<int> imageids)
        {
            BaseResponse br = new BaseResponse();
            br.IsSuccess = false;
            try
            {
                int baseNum = isReduce ? -1 : 1;
                PageDataObj pageObj = (PageDataObj)SerializeXmlHelper.DeserializeFromXml(pdata, typeof(PageDataObj));
                if (pageObj.image != null)
                {
                    foreach (PageImage img in pageObj.image)
                    {
                        Inpinke_Book_Image model = InpinkeDataContext.Instance.Inpinke_Book_Images.Get(e => e.BookID == bookid && e.ImageID == img.imageid);
                        if (model != null)
                        {
                            model.UsedNum += baseNum;
                            if (model.UsedNum < 0)
                            {
                                model.UsedNum = 0;
                            }                            
                            if (isReduce && model.UsedNum == 0)
                            {
                                imageids.Add(model.ImageID);
                            }
                            else if (!isReduce && model.UsedNum == 1)
                            {
                                imageids.Add(model.ImageID);
                            }
                            model.SaveWhenSubmit(InpinkeDataContext.Instance);
                        }
                    }
                    InpinkeDataContext.Instance.Submit();
                }
                br.IsSuccess = true;
                return br;
            }
            catch (Exception ex)
            {
                br.IsSuccess = false;
                br.Message = "修改书本图片使用次数失败，请稍后再试";
                Logger.Error(string.Format("ChangeImageUsedNum BookID:{0},Error:{1}", bookid, ex.ToString()));
                return br;
            }
        }
        /// <summary>
        /// 获取用户图片数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetUserImageCount(int userid)
        {
            try
            {
                return InpinkeDataContext.Instance.ExecuteQuery<int>(string.Format("select isnull(count(*),0) from Inpinke_Image where userid={0} and status={1}", userid, (int)RecordStatus.Nomral)).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("ChangeImageUsedNum UserID:{0},Error:{1}", userid, ex.ToString()));
                return 0;
            }
        }
    }
}
