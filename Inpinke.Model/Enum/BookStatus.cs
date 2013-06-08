using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.Model.Enum
{
    public enum BookStatus
    {
        /// <summary>
        /// 创建/编辑中
        /// </summary>
        Create = 1,
        /// <summary>
        /// 已完成
        /// </summary>
        Finished = 2,
        /// <summary>
        /// 印刷中
        /// </summary>
        Making  =3,
        /// <summary>
        /// 已删除
        /// </summary>
        Delete = 4        
    }
}
