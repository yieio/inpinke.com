using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using Inpinke.BLL.Filters;
using Inpinke.Model;
using Inpinke.BLL;
using Inpinke.BLL.Session;

namespace inpinke.com.Controllers
{
    public class BookController : Controller
    {
        //
        // GET: /Book/

        public ActionResult Index()
        {
            return View();
        }
        [UserFilter]
        public ActionResult GetBookList(int num,int page)
        {
            List<pagedata> list = new List<pagedata>();
            Random rand = new Random();
            IList<Inpinke_Book> bookList = DBBookBLL.GetUserBooks(UserSession.CurrentUser.ID);
            if (bookList != null)
            {
                foreach (Inpinke_Book b in bookList)
                {
                    pagedata p = new pagedata()
                    {
                        img = b.BookCover,
                        title = b.BookName
                    };
                    list.Add(p);
                }
                
            }

            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(list.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, list);
                return Content(Encoding.UTF8.GetString(ms.ToArray()));
            }
 
        }

    }
    public class pagedata
    {
        public int id { get; set; }
        public string img { get; set; }
        public string bigImg { get; set; }
        public int height { get; set; }
        public string title { get; set; }
    }
}
