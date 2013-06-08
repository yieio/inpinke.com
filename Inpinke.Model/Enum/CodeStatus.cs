using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.Model.Enum
{
    public enum CodeStatus
    {
        /// <summary>
        /// 未分发
        /// </summary>
        Create = 1,
        /// <summary>
        /// 已分发未使用
        /// </summary>
        UserGet = 2,
        /// <summary>
        /// 已使用
        /// </summary>
        Used = 3,
        /// <summary>
        /// 已过期
        /// </summary>
        Expired=4,

    }
}
