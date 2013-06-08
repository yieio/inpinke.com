using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using Inpinke.BLL.ImageProcess;
using Helper;
using System.IO;
using Inpinke.BLL;
using Inpinke.Model;
using log4net;
using Inpinke.Model.CustomClass;
using System.Text;

namespace inpinke.com.Controllers
{
    public class FileUploadController : Controller
    {
        //
        // GET: /FileUpload/
        public static readonly ILog Logger = LogManager.GetLogger(typeof(FileUploadController));
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            HttpFileCollectionBase files = Request.Files;

            if (files.Count == 0)
            {
                Response.Write("请勿直接访问本文件");
                Response.End();
                return Content("请勿直接访问本文件");
            }
            string dName = "/UserFile";
            string path = Server.MapPath(dName);
            string userid = Request.Form["userid"];
            string bookid = Request.Form["bookid"];
            try
            {
                // 只取第 1 个文件
                HttpPostedFileBase file = files[0];
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Request.Form["fileName"];
                    string orgFileName = fileName ?? string.Empty;

                    string[] fileNameInfo = fileName.Split('.');
                    string extendName = ".jpg";
                    if (fileNameInfo.Length > 1)
                    {
                        extendName = "." + fileNameInfo[fileNameInfo.Length - 1];
                    }
                    int intRandom = (new Random()).Next(10000, 99999);
                    string strTimeNow = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() +
                                        DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                    fileName = strTimeNow + intRandom.ToString();

                    string theFileName = fileName;
                    string vPath = dName + "/" + userid + "/OriginalImage/" + fileName + ".jpg";
                    if (!Directory.Exists(path + "/OriginalImage"))
                    {
                        Directory.CreateDirectory(path + "/" + userid + "/OriginalImage");
                    }
                    string strOriginalUrl = path + "/" + userid + "/OriginalImage/" + fileName + extendName;
                    file.SaveAs(strOriginalUrl);

                    //string mad5String = MD5HashFile.GetMD5HashFromFile(strOriginalUrl);
                    fileName = path + "/" + userid + "/" + fileName;
                    System.Drawing.Image originalImage = System.Drawing.Image.FromFile(strOriginalUrl);
                    Bitmap bitmap = new Bitmap(originalImage);
                    IList<Inpinke_ImageScale> list = DBImageScaleBLL.GetAllImageScaleSize();
                    if (list != null)
                    {
                        foreach (Inpinke_ImageScale s in list)
                        { 
                            if (s.GroupID == 1)
                            {
                                ImageProcessBLL.CreateStaticScaleImage(bitmap, s.Width, s.Height, 1000, fileName + s.PlusName + ".jpg");
                            }
                            else
                            {
                                ImageProcessBLL.CreateScaleImage(bitmap, s.Width, s.Height, fileName + s.PlusName + ".jpg", s.IsScale);
                            }

                        }
                    }
                    else
                    {
                        ImageProcessBLL.CreateScaleImage(bitmap, 80, 80, fileName + "_thumb.jpg", false);
                        ImageProcessBLL.CreateScaleImage(bitmap, 2400, 2400, fileName + "_print.jpg", true);
                        ImageProcessBLL.CreateScaleImage(bitmap, 600, 600, fileName + "_edit.jpg", true);
                    }
                    originalImage.Dispose();
                    bitmap.Dispose();
                    Inpinke_Image model = new Inpinke_Image()
                    {
                        UserID = int.Parse(userid),
                        Path = vPath,
                        OriginalName = orgFileName.Length > 150 ? orgFileName.Substring(0, 150) : orgFileName,
                        ImageName = theFileName,
                        HashCode = MD5HashFile.GetMD5HashFromFile(strOriginalUrl),
                        UsedNum = 0
                    };
                    BaseResponse br = DBImageBLL.AddUserImage(model);
                    string result = string.Empty;
                    if (br.IsSuccess)
                    {
                        if (!string.IsNullOrEmpty(bookid))
                        {
                            Inpinke_Book_Image bImg = new Inpinke_Book_Image()
                            {
                                UsedNum = 0,
                                ImageID = model.ID,
                                BookID = int.Parse(bookid)
                            };
                            DBImageBLL.AddBookImage(bImg);
                        }
                        result = "{success:true,image:{id:" + model.ID + ",name:\"" + model.ImageName + "\",path:\"" + model.Path + "\"}}";
                    }
                    else
                    {
                        result = "{success:false,msg:\"" + br.Message + "\"}";
                    }
                    return Content(result);
                }
                else
                {
                    return Content("{success:false,msg:\"上传内容为空\"}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("UploadFile UserID:{0}, Error{1}", userid, ex.ToString()));
                return Content("{success:false,msg:\"上传出错，请稍后再试\"}");
            }
        }


        public ActionResult AjaxGetUserImageByID(int? imageid)
        {
            if (imageid.HasValue)
            {
                Inpinke_Image model = DBImageBLL.GetImageByID(imageid.Value);
                return Content("{success:true,image:{id:" + model.ID + ",name:\"" + model.ImageName + "\",path:\"" + model.Path + "\"}}");
            }
            else
            {
                return Content("{success:false,msg:\"参数不正确\"}");
            }
        }

        public ActionResult AjaxGetUserImageByUserID(int? userid, int? count)
        {
            return View();
        }

        public ActionResult AjaxGetBookImage(int userid, int bookid, int count, int skip)
        {
            int total = 0;
            IList<Inpinke_Book_Image> list = DBImageBLL.GetBookImages(skip, count, ref total, bookid);
            if (list != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"success\":true,\"images\":[");
                foreach (Inpinke_Book_Image i in list)
                {
                    sb.Append("{" + string.Format("\"id\":{0},\"name\":\"{1}\",\"usednum\":{2}", i.ImageID, i.Inpinke_Image.ImageName, i.UsedNum) + "},");
                }

                return Content(sb.ToString().TrimEnd(',') + "]}");

            }
            else
            {
                return Content("{\"success\":false,\"msg\":\"暂无照片\"}");
            }
        }

        public ActionResult AjaxDeleteBookImage(int userid, int bookid, int imageid)
        {
            BaseResponse br = DBImageBLL.DeleteBookImage(userid, bookid, imageid);
            return Content("{\"success\":" + br.IsSuccess.ToString().ToLower() + ",\"imageid\":" + imageid + ",\"msg\":\"" + br.Message + "\"}");
        }
    }
}
