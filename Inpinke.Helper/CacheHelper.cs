using System;
using System.Web;
using System.Web.Caching;

namespace Helper
{
    public class CacheHelper
    {
        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache; 
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="cacheDPFilePath">���������������ļ�����·��</param>
        public static void SetCache(string CacheKey, object objObject, string cacheDPFilePath)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, new CacheDependency(cacheDPFilePath));
        }
    }
}
