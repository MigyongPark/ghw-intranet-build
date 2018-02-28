using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace USNStarterKit.USNBusinessLogic
{
    /// <summary>
    /// Summary description for USNCacheHelper
    /// </summary>
    public class USNCacheHelper
    {
        public static void AddToCache(string key, object value, TimeSpan timespan)
        {
            HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, timespan, CacheItemPriority.Normal, null);
        }

        public static void AddToRequestCache(string key, object value)
        {
            HttpContext.Current.Items.Add(key, value);
        }

        public static object GetFromRequestCache(string key)
        {
            return HttpContext.Current.Items[key];
        }
    }
}