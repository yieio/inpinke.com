using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.Model.Enum
{
    public enum MsgReferType
    {
        /// <summary>
        /// 无关联
        /// </summary>
        None=1,
        /// <summary>
        /// 关联订单
        /// </summary>
        Order = 2,
        /// <summary>
        /// 关联印品
        /// </summary>
        Book = 3

    }
}
