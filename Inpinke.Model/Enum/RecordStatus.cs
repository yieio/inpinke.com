using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.Model.Enum
{
    public enum RecordStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        Create = 1,
        /// <summary>
        /// 正常
        /// </summary>
        Nomral = 2,
        /// <summary>
        /// 冻结
        /// </summary>
        Frozen = 3,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 4
    }
}
