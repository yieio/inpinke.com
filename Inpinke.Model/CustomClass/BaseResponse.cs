using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inpinke.Model.CustomClass
{
    /// <summary>
    /// 基础返回类
    /// </summary>
   public class BaseResponse
    {
       /// <summary>
       /// 是否成功
       /// </summary>
       public bool IsSuccess { get; set; }
       /// <summary>
       /// 提示信息
       /// </summary>
       public string Message { get; set; }
       /// <summary>
       /// 关联的对象
       /// </summary>
       public object ResponseObj { get; set; }
    }
}
