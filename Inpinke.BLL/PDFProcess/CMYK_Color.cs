using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.BLL.PDFProcess
{
    public class CMYK_Color
    {
        public int C { get; set; }

        public int M { get; set; }

        public int Y { get; set; }

        public int K { get; set; }

        public CMYK_Color() { }

        public CMYK_Color GetCMYK_Color(string strRGB)
        {
            Dictionary<string, CMYK_Color> dicCMYK = new Dictionary<string, CMYK_Color>();
            dicCMYK.Add("#000000", new CMYK_Color { C = 0, M = 0, Y = 0, K = 100 });
            dicCMYK.Add("#444444", new CMYK_Color { C = 0, M = 0, Y = 0, K = 85 });
            dicCMYK.Add("#E4DFD6", new CMYK_Color { C = 14, M = 12, Y = 16, K = 0 });
            dicCMYK.Add("#F9DCDA", new CMYK_Color { C = 2, M = 20, Y = 10, K = 0 });
            dicCMYK.Add("#D6EAF3", new CMYK_Color { C = 20, M = 2, Y = 3, K = 0 });
            dicCMYK.Add("#FFE99D", new CMYK_Color { C = 0, M = 10, Y = 50, K = 0 });
            dicCMYK.Add("#FCDCAB", new CMYK_Color { C = 2, M = 18, Y = 40, K = 0 });
            dicCMYK.Add("#FBE7D4", new CMYK_Color { C = 2, M = 13, Y = 18, K = 0 });
            dicCMYK.Add("#D2DC9F", new CMYK_Color { C = 25, M = 5, Y = 50, K = 0 });
            dicCMYK.Add("#B90000", new CMYK_Color { C = 35, M = 100, Y = 100, K = 0 });
            dicCMYK.Add("#175724", new CMYK_Color { C = 90, M = 55, Y = 95, K = 30 });
            dicCMYK.Add("#CABCAC", new CMYK_Color { C = 28, M = 28, Y = 34, K = 0 });
            dicCMYK.Add("#A7BBC1", new CMYK_Color { C = 43, M = 21, Y = 21, K = 0 });
            dicCMYK.Add("#FBBBAB", new CMYK_Color { C = 0, M = 38, Y = 30, K = 0 });
            dicCMYK.Add("#6789A4", new CMYK_Color { C = 70, M = 45, Y = 25, K = 0 });
            dicCMYK.Add("#F8F2E4", new CMYK_Color { C = 4, M = 5, Y = 12, K = 0 });
            dicCMYK.Add("#EF006B", new CMYK_Color { C = 0, M = 100, Y = 30, K = 0 });
            dicCMYK.Add("#009FE0", new CMYK_Color { C = 100, M = 0, Y = 0, K = 5 });
            dicCMYK.Add("#FFC400", new CMYK_Color { C = 0, M = 30, Y = 100, K = 0 });
            dicCMYK.Add("#F98C00", new CMYK_Color { C = 0, M = 60, Y = 100, K = 0 });
            dicCMYK.Add("#DF0000", new CMYK_Color { C = 0, M = 100, Y = 100, K = 10 });
            dicCMYK.Add("#93BC00", new CMYK_Color { C = 60, M = 0, Y = 100, K = 0 });
            dicCMYK.Add("#C2891B", new CMYK_Color { C = 33, M = 55, Y = 95, K = 0 });
            dicCMYK.Add("#77420D", new CMYK_Color { C = 70, M = 85, Y = 100, K = 0 });
            dicCMYK.Add("#FFFFFF", new CMYK_Color { C = 0, M = 0, Y = 0, K = 0 });
            dicCMYK.Add("#ffffff", new CMYK_Color { C = 0, M = 0, Y = 0, K = 0 });
            CMYK_Color newcmyk = new CMYK_Color { C = (int)((float)dicCMYK[strRGB].C * (float)2.55), M = (int)((float)dicCMYK[strRGB].M * (float)2.55), Y = (int)((float)dicCMYK[strRGB].Y * (float)2.55), K = (int)((float)dicCMYK[strRGB].K * (float)2.55) };
            return newcmyk;
        }
    }
}
