using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Web;

namespace Inpinke.Model
{
    public partial class InpinkeDataContext
    {
        public void Submit()
        {
            this.SubmitChanges();
        }
        public static InpinkeDataContext Instance
        {
            get
            {
                if (HttpContext.Current != null)
                    return DataContextFactory.GetWebRequestScopedDataContext<InpinkeDataContext>();
                else
                    return DataContextFactory.GetThreadScopedDataContext<InpinkeDataContext>();
            }
        }

        public static void DisposeDataContext()
        {
            DataContextFactory.DisposeDataContext(null);
        }

        #region DataContext 成员


        public void Execute(string sql)
        {
            this.ExecuteCommand(sql);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            this.SubmitChanges();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 随机序列
        /// </summary>
        /// <returns></returns>
        [Function(Name = "NEWID", IsComposable = true)]
        public Guid NEWID()
        {
            return ((Guid)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod()))).ReturnValue));
        }

        public string GetSQL(System.Linq.IQueryable q)
        {
            return this.GetCommand(q).CommandText;
        }

          
    }
}
