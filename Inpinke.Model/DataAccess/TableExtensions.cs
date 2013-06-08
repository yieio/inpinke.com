using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Linq.Expressions;


namespace Inpinke.Model
{
    class ListResult<TEntity>
    {
        public int Count { get; set; }
        public IList<TEntity> List { get; set; }
    }

    public static class TableExtensions
    {
        public static TEntity Get<TEntity>(this Table<TEntity> t, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return t.SingleOrDefault(predicate);
        }

        public static TEntity GetWith<TEntity>(this Table<TEntity> t, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] objects) where TEntity : class
        {
            InpinkeDataContext udc = new InpinkeDataContext();
            DataLoadOptions dlo = new DataLoadOptions();
            foreach (Expression<Func<TEntity, object>> obj in objects)
            {
                dlo.LoadWith(obj);
            }
            udc.LoadOptions = dlo;
            TEntity te = udc.GetTable<TEntity>().SingleOrDefault(predicate);

            return te;
        }

        public static IList<TEntity> SelectTop<TEntity, TKey>(this Table<TEntity> t, int count, Expression<Func<TEntity, bool>> predicate,Expression<Func<TEntity, TKey>> orderKey, bool isDesc) where TEntity : class
        {
            int total = -1;
            return t.Select(0, count, ref total, predicate, orderKey, isDesc);
        }

        public static IList<TEntity> Select<TEntity, TKey>(this Table<TEntity> t, Expression<Func<TEntity, TKey>> orderKey, bool isDesc) where TEntity : class
        {
            int total = -1;
            return t.Select(0, int.MaxValue, ref total, null, orderKey, isDesc);
        }


        public static IList<TEntity> Select<TEntity, TKey>(this Table<TEntity> t, int skip, int count, ref int total, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderKey, bool isDesc) where TEntity : class
        {
            ListResult<TEntity> result = new ListResult<TEntity>();
            IQueryable<TEntity> q = t.AsQueryable();
            if (predicate != null)
                q = q.Where(predicate);

            if (total >= 0)
                total = q.Count();

            if (orderKey != null && isDesc)
                q = q.OrderByDescending(orderKey);
            else if (orderKey != null)
                q = q.OrderBy(orderKey);

            return q.Skip(skip).Take(count).ToList();
        }

        public static IList<TEntity> Select<TEntity, TKey>(this Table<TEntity> t, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderKey, bool isDesc) where TEntity : class
        {
            int total = -1;
            return t.Select(0, int.MaxValue, ref total, predicate, orderKey, isDesc);
        }

        public static IList<TEntity> SelectWith<TEntity, TKey>(this Table<TEntity> t, int skip, int count, ref int total, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderKey, bool isDesc, params Expression<Func<TEntity, object>>[] objects) where TEntity : class
        {
            InpinkeDataContext udc = new InpinkeDataContext();
            DataLoadOptions dlo = new DataLoadOptions();
            foreach (Expression<Func<TEntity, object>> obj in objects)
            {
                dlo.LoadWith(obj);
            }
            udc.LoadOptions = dlo;
            IList<TEntity> list = udc.GetTable<TEntity>().Select(skip, count, ref total, predicate, orderKey, isDesc);

            return list;
        }

        public static int DeleteAll<TEntity>(this Table<TEntity> t, Expression<Func<TEntity, bool>> predicate) where TEntity:class
        {
            List<TEntity> list = t.Where(predicate).ToList();
            int count = list.Count;

            t.DeleteAllOnSubmit(list);

            return count;
        }

        public static TEntity Delete<TEntity>(this Table<TEntity> t, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            TEntity entity = t.Get(predicate);
            t.DeleteOnSubmit(entity);
            return entity;
        }
    }
}
