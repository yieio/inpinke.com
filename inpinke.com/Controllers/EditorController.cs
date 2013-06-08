using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inpinke.Model;
using Inpinke.BLL;
using System.Xml.Linq;
using log4net;
using Inpinke.Model.CustomClass;
using Newtonsoft.Json;
using Inpinke.Model.Enum;
using System.Reflection;
using Inpinke.BLL.Session;
using System.IO;
using Inpinke.BLL.ImageProcess;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections;
using Inpinke.BLL.Config;

namespace inpinke.com.Controllers
{
    public class EditorController : Controller
    {
        public static readonly ILog Logger = LogManager.GetLogger(typeof(EditorController));
        //
        // GET: /Editor/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 保存页面内容
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="userid"></param>
        /// <param name="pagedata"></param>
        /// <returns></returns>
        public ActionResult SavePageData(int bookid, int userid, string pdata)
        {
            try
            {
                Inpinke_Book model = DBBookBLL.GetBookByID(bookid);
                PageDataObjs pageObjs = (PageDataObjs)JsonConvert.DeserializeObject(pdata, typeof(PageDataObjs));
                if (pageObjs == null)
                {
                    return Content("{\"success\":false,\"msg\":\"保存失败，内容为空\"}");
                }
                if (model.BookStauts == (int)BookStatus.Making)
                {
                    return Content("{\"success\":false,\"msg\":\"对不起当前印品已下单印制不能再编辑。\"}");
                }
                string strReturn = "{\"success\":true,\"pviews\":[";
                string noneused = "";
                string usedimgs = "";
                List<int> noneUseImage = new List<int>();
                List<int> usedimg = new List<int>();
                foreach (PageDataObj pageObj in pageObjs.pdatas)
                {
                    int pnum = pageObj.pagenum;
                    Inpinke_Book_Page bookPage = DBBookBLL.GetBookPageByPNum(pnum, bookid);
                    XElement root = new XElement("layout", new XAttribute("pnum", pnum), new XAttribute("styleid", pageObj.styleid),
                        new XAttribute("isskip", pageObj.isskip), new XAttribute("opnum", pageObj.opagenum), new XAttribute("bgcolor", pageObj.bgcolor));
                    bool isCreate = false;
                    if (bookPage == null)
                    {
                        bookPage = new Inpinke_Book_Page()
                        {
                            BookID = bookid,
                            PageNum = pnum
                        };
                        isCreate = true;
                    }
                    else
                    {
                        //原页面不是跨页，现在是跨页的，需要删除掉一个原页面
                        if (!bookPage.IsSkip && pageObj.isskip.ToLower() == "true" && pageObj.opagenum != "-1_0")
                        {
                            int opnum = int.Parse(pageObj.opagenum.Replace(pnum + "_", ""));
                            Inpinke_Book_Page oBookPage = DBBookBLL.GetBookPageByPNum(opnum, bookid);
                            if (oBookPage != null)
                            {
                                oBookPage.PageStatus = (int)PageStatus.Delete;
                                DBBookBLL.UpdateBookPage(oBookPage);
                                //修改页面图片使用次数
                                DBImageBLL.ChangeImageUsedNum(oBookPage.PageData, bookid, true, ref noneUseImage);
                            }
                        }
                        //修改页面图片使用次数
                        DBImageBLL.ChangeImageUsedNum(bookPage.PageData, bookid, true, ref noneUseImage);

                    }
                    bookPage.IsSkip = pageObj.isskip.ToLower() == "false" ? false : true;

                    //添加图片节点
                    foreach (PageImage image in pageObj.image)
                    {
                        XElement imageItem = new XElement("image");
                        PropertyInfo[] pis = image.GetType().GetProperties();
                        int urlIndex = image.src.ToLower().IndexOf("/userfile");
                        if (urlIndex > 0)
                        {
                            image.src = image.src.Substring(urlIndex);
                        }
                        foreach (PropertyInfo property in pis)
                        {
                            //设置节点属性
                            imageItem.SetAttributeValue(property.Name, property.GetValue(image, null));
                        }
                        root.Add(imageItem);
                    }
                    //添加文字节点
                    foreach (PageText text in pageObj.text)
                    {
                        XElement textItem = new XElement("text");
                        PropertyInfo[] pis = text.GetType().GetProperties();
                        foreach (PropertyInfo property in pis)
                        {
                            //设置节点属性
                            textItem.SetAttributeValue(property.Name, property.GetValue(text, null));
                        }
                        if (pageObj.pagenum == 0 && text.issingle == "true")
                        {
                            if (!string.IsNullOrEmpty(text.content) && text.conid == "txt_1")
                            {
                                model.BookName = text.content;
                            }
                            if (!string.IsNullOrEmpty(text.content) && text.conid == "txt_2")
                            {
                                model.SubBookName = text.content;
                            }
                        }

                        root.Add(textItem);
                    }
                    bookPage.PageData = root;
                    //修改页面图片使用次数

                    DBImageBLL.ChangeImageUsedNum(bookPage.PageData, bookid, false, ref usedimg);

                    bool istxtO = false; //文字是否过多
                    if (isCreate)
                    {
                        int pID = DBBookBLL.GetMaxBookPageID() + 1;
                        bookPage.PageImg = SavePageView(pageObj, userid, model, ref istxtO, pID);
                    }
                    else
                    {
                        bookPage.PageImg = SavePageView(pageObj, userid, model, ref istxtO, bookPage.ID);
                    }

                    strReturn += "{\"pnum\":" + pageObj.pagenum + ",\"src\":\"" + bookPage.PageImg + "\",\"isskip\":" + pageObj.isskip + ",\"istxtover\":" + istxtO.ToString().ToLower() + "},";
                    bookPage.PageStatus = (int)PageStatus.Normal;
                    if (pageObj.pagenum == 0)
                    {
                        model.BookCover = "/UserFile/" + UserSession.CurrentUser.ID + "/" + bookid + "/cover200.jpg";
                    }

                    if (isCreate)
                    {
                        DBBookBLL.AddBookPage(bookPage);
                        //更新已完成的页数
                        DBBookBLL.UpdateDonePage(bookid);
                    }
                    else
                    {
                        DBBookBLL.UpdateBookPage(bookPage);
                    }
                }
                noneused = string.Join(",", noneUseImage);
                usedimgs = string.Join(",", usedimg);

                strReturn = strReturn.TrimEnd(',') + "],\"noneusedimg\":[" + noneused + "],\"usedimg\":[" + usedimgs + "]}";
                return Content(strReturn);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("SavePageData BookID:{0},UserID:{1},Error:{2}", bookid, userid, ex.ToString()));
                return Content("{\"success\":false,\"msg\":\"服务器异常保存失败，请稍后再试\"}");
            }
        }
        /// <summary>
        /// 保存页面缩略图
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pobj"></param>
        public string SavePageView(PageDataObj pobj, int userid, Inpinke_Book book, ref bool istxtO, int pageid)
        {
            int bookid = book.ID;
            string prodName = book.Inpinke_Product.ShortName.ToString().ToLower();
            string dName = "/UserFile";
            string path = Server.MapPath(dName);
            string filePath = path + "/" + userid + "/" + bookid;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fileName = "pview_" + pageid + ".jpg";
            int orgWidth = ConfigMap.GetEditorAttr(prodName, "EditorWidth");
            orgWidth = pobj.isskip == "true" ? orgWidth * 2 : orgWidth;
            int orgHeight = ConfigMap.GetEditorAttr(prodName, "EditorHeight");
            int outWidth = ConfigMap.GetEditorAttr(prodName, "MiniViewWidth");
            outWidth = pobj.isskip == "true" ? outWidth * 2 : outWidth;
            int outHeight = ConfigMap.GetEditorAttr(prodName, "MiniViewHeight");

            ImageProcessBLL.CreatePageViewImage(pobj, filePath + "/" + fileName, orgWidth, orgHeight, outWidth, outHeight, ref istxtO);
            ImageProcessBLL.CreatePageViewImage(pobj, filePath + "/pthumb_" + pageid + ".jpg", orgWidth, orgHeight, orgWidth, orgHeight, ref istxtO);
            string returnName = dName + "/" + userid + "/" + bookid + "/" + fileName;
            if (pobj.pagenum == 0)
            {
                Image originalImage = Image.FromFile(filePath + "/pthumb_" + pageid + ".jpg");
                Bitmap bitmap = new Bitmap(originalImage);
                ImageProcessBLL.CreateScaleImage(bitmap, 200, 200, filePath + "/cover200.jpg", false);
                originalImage.Dispose();
            }

            return returnName;
        }
        /// <summary>
        /// 获取页面小图预览
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPageView(int bookid, string opum)
        {
            IList<Inpinke_Book_Page> list = DBBookBLL.GetBookPageByPViews(bookid);
            if (list == null)
            {
                return Content("{\"success\":true,\"pviews\":[]}");
            }
            string strReturn = "{\"success\":true,\"pviews\":[";
            foreach (Inpinke_Book_Page p in list)
            {
                strReturn += "{\"pnum\":" + p.PageNum + ",\"src\":\"" + p.PageImg + "\",\"isskip\":" + p.IsSkip.ToString().ToLower() + "},";
            }
            strReturn = strReturn.TrimEnd(',') + "]}";
            return Content(strReturn);
        }

        /// <summary>
        /// 获取页面内容
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="userid"></param>
        /// <param name="opnum"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPageData(int bookid, int userid, string opnum)
        {
            if (string.IsNullOrEmpty(opnum))
            {
                return Content("{\"success\":false,\"msg\":\"获取内容失败,没有要查找的页码\"}");
            }
            string[] pnums = opnum.Split('_');
            IList<Inpinke_Book_Page> pages = DBBookBLL.GetBookPageByPNums(pnums, bookid);
            if (pages == null)
            {
                return Content("{\"success\":false,\"msg\":\"获取内容失败,没找到对应的页面\"}");
            }
            string jsonString = "[";
            foreach (Inpinke_Book_Page p in pages)
            {
                jsonString += JsonConvert.SerializeXNode(p.PageData, Newtonsoft.Json.Formatting.None) + ",";
                if (p.IsSkip)
                {
                    break;
                }
            }
            jsonString = jsonString.TrimEnd(',') + "]";
            return Content(FormatJsonObjectString(jsonString));
        }

        public string FormatJsonObjectString(string jsonString)
        {
            jsonString = Regex.Replace(jsonString, "(?<!:)(\"@)(?!.\":\\s )", "\"", RegexOptions.IgnoreCase);
            return jsonString;
        }

        /// <summary>
        /// 保存书本预设
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="userid"></param>
        /// <param name="bookname"></param>
        /// <param name="bookauthor"></param>
        /// <param name="bookbrief"></param>
        /// <param name="bookfont"></param>
        /// <returns></returns>
        public ActionResult SaveBookSet(int bookid, int userid, string bookname, string bookauthor, string bookbrief, int bookfont)
        {
            Inpinke_Book book = DBBookBLL.GetBookByID(bookid);
            if (book != null)
            {
                book.BookName = bookname;
                book.BookDesc = bookbrief;
                book.Author = bookauthor;
                book.BookFont = bookfont;
                BaseResponse br = DBBookBLL.UpdateBook(book);
                return Content("{\"success\":" + br.IsSuccess.ToString().ToLower() + ",\"msg\":\"" + br.Message + "\"}");
            }
            else
            {
                return Content("{\"success\":false,\"msg\":\"更新书本信息失败\"}");
            }
        }

        /// <summary>
        /// 调整书本顺序
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="userid"></param>
        /// <param name="numorder"></param>
        /// <returns></returns>
        public ActionResult SetPageNumOrder(int bookid, int userid, string numorder, string delpnum)
        {
            try
            {
                string[] delpnums = delpnum.Split(',');
                string[] nums = numorder.Split(',');
                // list = DBBookBLL.GetBookPageByPNums(delpnums, bookid);
                Inpinke_Book book = DBBookBLL.GetBookByID(bookid);
                if (delpnums != null && delpnums.Count() > 0)
                {
                    book.PageCount = book.PageCount - delpnums.Length;
                    DBBookBLL.UpdateBook(book);
                    DBBookBLL.DeleteBookPage(delpnum, bookid);
                    DBBookBLL.UpdateDonePage(bookid);
                }

                IList<Inpinke_Book_Page> list = DBBookBLL.GetBookPageByPNums(nums, bookid);
                if (list != null)
                {
                    Hashtable ht = new Hashtable();
                    foreach (Inpinke_Book_Page p in list)
                    {
                        ht[p.PageNum] = p.ID;
                    }

                    int[] intNums = nums.Where(e => int.Parse(e) >= 0).Select(e => int.Parse(e)).ToArray();
                    for (int i = 0; i < intNums.Length; i++)
                    {
                        if (ht[intNums[i]] == null)
                        {
                            continue;
                        }
                        Inpinke_Book_Page model = list.Where(e => e.ID == (int)ht[intNums[i]]).FirstOrDefault();
                        if (model != null)
                        {
                            model.PageNum = i;

                            model.PageData.SetAttributeValue("pnum", i);
                            string opnum = "-1_0";
                            if (i == 0)
                            {
                                opnum = "-1_0";
                            }
                            else if (i == 1)
                            {
                                opnum = "-2_1";
                            }
                            else if (i == intNums.Length - 1)
                            {
                                opnum = i.ToString() + "_-3";
                            }
                            else if (i % 2 == 0)
                            {
                                opnum = i + "_" + (i + 1);
                            }
                            else
                            {
                                opnum = (i - 1) + "_" + i;
                            }
                            model.PageData.SetAttributeValue("opnum", opnum);
                            model.PageData = XElement.Parse(model.PageData.ToString());
                            model.SaveWhenSubmit(InpinkeDataContext.Instance);
                        }
                    }
                }
                InpinkeDataContext.Instance.Submit();
                return Content("{\"success\":true,\"msg\":\"更新书本信息成功\"}");
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("SetPageNumOrder BookID:{0} Error:{1}", bookid, ex.ToString()));
                return Content("{\"success\":false,\"msg\":\"更新书本信息失败,刷新页面再试试吧！\"}");
            }
        }
        /// <summary>
        /// 增加页面
        /// </summary>
        /// <param name="bookid"></param>
        /// <param name="userid"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public ActionResult AddBookPage(int bookid, int userid, int pagecount)
        {
            try
            {
                Inpinke_Book book = DBBookBLL.GetBookByID(bookid);
                if (book == null)
                {
                    return Content("{\"success\":false,\"msg\":\"添加书本页面失败,刷新页面再试试吧！\"}");
                }
                if (book.PageCount >= pagecount)
                {
                    return Content("{\"success\":false,\"msg\":\"添加书本页面失败,刷新页面再试试吧！\"}");
                }
                Inpinke_Book_Page bookpage = DBBookBLL.GetBookPageByPNum(book.PageCount, bookid);
                if (bookpage != null)
                {
                    bookpage.PageData.SetAttributeValue("opnum", book.PageCount + "_" + (book.PageCount + 1));
                    bookpage.PageData = XElement.Parse(bookpage.PageData.ToString());
                    bookpage.SaveWhenSubmit(InpinkeDataContext.Instance);
                }
                book.PageCount = pagecount;
                book.SaveWhenSubmit(InpinkeDataContext.Instance);
                InpinkeDataContext.Instance.Submit();
                return Content("{\"success\":true,\"msg\":\"添加书本页面成功！\"}");
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("AddBookPage Error:{0},BookID:{1},UserID:{2},PageCount:{3}", ex.ToString(), bookid, userid, pagecount));
                return Content("{\"success\":false,\"msg\":\"添加书本页面失败,刷新页面再试试吧！\"}");
            }
        }


        public ActionResult Checkbookdone(int userid, int bookid)
        {
            Inpinke_Book model = DBBookBLL.GetBookByID(bookid);
            if (model == null)
            {
                //ViewBag.Msg = "对不起没有找到您要下单印刷的印品，您可以在【<a href=\"/my/book\">我的照片书</a>】中直接选择要印刷的印品进行下单印刷。";
                //return View("error");
                return Content("{\"success\":false,\"msg\":\"对不起没找到您要下单印刷印品，请尝试重新登录。\"}");
            }
            if (model.UserID != userid)
            {
                return Content("{\"success\":false,\"msg\":\"对不起您不能下单印刷当前印品，请尝试重新登录。\"}");
            }
            if (model.PageCount > model.DonePages)
            {
                return Content("{\"success\":false,\"msg\":\"书本还有" + (model.PageCount - model.DonePages) + "页没有编辑，将印成空白页。确认要下单印刷吗？\"}");
            }
            else
            {
                return Content("{\"success\":true,\"msg\":\"书本已完成编辑\"}");
            }

        }

    }
}
