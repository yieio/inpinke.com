using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.Model.CustomClass
{
    public class PageDataObjs
    {
        public List<PageDataObj> pdatas { get; set; }
    }

    public class PageDataObj
    {
        public string isfinish { get; set; }
        public string isskip { get; set; } //是否跨页
        public int pagenum { get; set; }
        public string opagenum { get; set; }
        public int pnum { get; set; }
        public string opnum { get; set; }
        public int styleid { get; set; }
        public string side { get; set; }
        public string bgcolor { get; set; }    
        public List<PageImage> image { get; set; }
        public List<PageText> text { get; set; }
    }

    public class layout
    {
        public string isfinish { get; set; }
        public string isskip { get; set; } //是否跨页
        public int pagenum { get; set; }
        public string opagenum { get; set; }
        public int styleid { get; set; }
        public string side { get; set; }
        public string bgcolor { get; set; }
        public List<PageImage> image { get; set; }
        public List<PageText> text { get; set; }
    }

    public class PageImage
    {
        public int imageid { get; set; }
        public string conid { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public int conx { get; set; }//容器相对页面位置
        public int cony { get; set; }
        public int conwidth { get; set; }
        public int conheight { get; set; }
        public int x { get; set; }//图片相对容器的位置
        public int y { get; set; }
        public string src { get; set; }
        public int orgwidth { get; set; }//图片原大小
        public int orgheight { get; set; }
    }

    public class PageText
    {
        public string conid { get; set; }
        public int conx { get; set; }
        public int cony { get; set; }
        public int conwidth { get; set; }
        public int conheight { get; set; }
        public string content { get; set; }
        public int fontsize { get; set; }
        public string color { get; set; }
        public string textalign { get; set; }
        public string issingle { get; set; } //是否单行文本
    }
     
}
