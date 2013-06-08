using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

namespace Helper
{
    public class PagerHelper
    {
        public static string GetPager(string table, int skip, int pageSize, string Fields, string orderBy, string strWhere)
        {
            int pageIndex = skip / pageSize == 0 ? skip / pageSize : skip / pageSize;

            if (pageIndex == 0)
            {
                return "select top " + pageSize.ToString() + "  " + Fields + "  from  " + table + " where " + strWhere + "  order by " + orderBy;
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                int START_ID = (pageSize * pageIndex) + 1;
                int END_ID = pageSize * (pageIndex + 1);
                strSql.Append("SELECT * ");
                strSql.Append("FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS rownum, ");
                strSql.Append("" + Fields + " ");
                strSql.Append("FROM " + table + " where " + strWhere + ") AS D ");
                strSql.Append("WHERE rownum BETWEEN " + START_ID + " AND " + END_ID + " ORDER BY  D.rownum asc ");
                return strSql.ToString();
            }
        }
        public static string GetCountSQL(string table, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append("from " + table + " ");
            strSql.Append("where " + strWhere + "");

            return strSql.ToString();
        }

        public static string GetPager2(string table, int skip, int pageSize, string Fields, string orderBy, string strWhere)
        {
            int pageIndex = skip / pageSize == 0 ? skip / pageSize : skip / pageSize;

            if (pageIndex == 0)
            {
                return "select top " + pageSize.ToString() + "  " + Fields + "  from  " + table + " where " + strWhere + "  order by " + orderBy;
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                int START_ID = (pageSize * pageIndex) + 1;
                int END_ID = pageSize * (pageIndex + 1);
                string showFields = Regex.Replace(Fields, @"[a-zA-Z]+\.", "");
                showFields = Regex.Replace(showFields, @"([a-zA-Z]+)?\(.+\)\s*as", "");
                strSql.Append("SELECT " + showFields + " ");
                strSql.Append("FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + orderBy + ") AS rownum, ");
                strSql.Append("" + Fields + " ");
                strSql.Append("FROM " + table + " where " + strWhere + ") AS D ");
                strSql.Append("WHERE rownum BETWEEN " + START_ID + " AND " + END_ID + " ORDER BY  D.rownum asc ");
                return strSql.ToString();
            }
           
        }
    }


}
