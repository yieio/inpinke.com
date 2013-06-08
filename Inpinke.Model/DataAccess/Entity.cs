using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Data.Linq;

namespace Inpinke.Model
{
    /// <summary>
    /// 表示实体
    /// </summary>
    public abstract class Entity<T> 
        where T : class
    {

        protected T Original { get; set; }

        ///// <summary>
        ///// 验证
        ///// </summary>
        ///// <param name="database">数据库</param>
        ///// <returns>验证结果</returns>
        //public ValidateResult Validate(DataContext database)
        //{
        //    if (database == null)
        //        throw new ArgumentNullException("database");

        //    var validater = new Validater();
        //    ValidateAction(validater, database);
        //    return validater.Validate();
        //}
        /// <summary>
        /// 验证动作
        /// </summary>
        /// <param name="validater">验证器</param>
        /// <param name="database">数据库</param>
        //protected virtual void ValidateAction(Validater validater, DataContext database) { }

        /// <summary>
        /// 保存 
        /// </summary>
        /// <param name="database">数据库</param>
        public void Save(DataContext database)
        {
            SaveWhenSubmit(database);

            database.SubmitChanges();
        }

        public void SaveWhenSubmit(DataContext database)
        {
            if (database == null)
                throw new ArgumentNullException("database");

            //var validateResult = this.Validate(database);
            //if (!validateResult.IsValidated)
            //    throw new ValidateFailException(validateResult);

            SaveAction(database);
        }
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="database">数据库</param>
        protected virtual void SaveAction(DataContext database)
        {
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="database"></param>
        public void Insert(DataContext database)
        {
            InsertWhenSubmit(database);
            database.SubmitChanges();
        }

        /// <summary>
        /// 当调用Submit时插入
        /// </summary>
        /// <param name="database"></param>
        public void InsertWhenSubmit(DataContext database)
        {
            if (database == null)
                throw new ArgumentNullException("database");

            //var validateResult = this.Validate(database);
            //if (!validateResult.IsValidated)
            //    throw new ValidateFailException(validateResult);

            InsertAction(database);
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="database"></param>
        protected virtual void InsertAction(DataContext database)
        {
            database.GetTable<T>().InsertOnSubmit(this as T);

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="database">数据库</param>
        public void Delete(DataContext database)
        {
            DeleteWhenSubmit(database);
            database.SubmitChanges();
        }

        /// <summary>
        /// 当执行Submit时删除
        /// </summary>
        /// <param name="database"></param>
        public void DeleteWhenSubmit(DataContext database)
        {
            if (database == null)
                throw new ArgumentNullException("database");
            database.GetTable<T>().DeleteOnSubmit(this as T);
            DeleteAction(database);
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="database">数据库</param>
        protected virtual void DeleteAction(DataContext database)
        {
            database.GetTable<T>().DeleteOnSubmit(this as T);
        }

    }

}
