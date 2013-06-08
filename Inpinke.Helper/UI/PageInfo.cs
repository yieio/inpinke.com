using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper.UI
{
    [Serializable]
    public class PageInfo
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页边距
        /// </summary>
        public int Margin { get; set; }

        public int Skip { get; set; }
    }
}
