using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.Model.Enum
{
    public enum MsgType
    {
        /// <summary>
        /// 系统公告
        /// </summary>
        Notice=1,
        /// <summary>
        /// 发给单个用户的
        /// </summary>
        ToSingleUser = 2,
        /// <summary>
        /// 用户回复
        /// </summary>
        UserReply = 3,
        /// <summary>
        /// 系统回复
        /// </summary>
        SystemReply=4,
        /// <summary>
        /// 管理员回复
        /// </summary>
        AdminReply=5,

    }
}
