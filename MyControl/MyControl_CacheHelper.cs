using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Xml;

namespace Games.MyControl
{
    public class MyControl_CacheHelper
{
    // Fields
    protected string basicKey = string.Empty;

    // Methods
    public MyControl_CacheHelper(string basicKey)
    {
        this.basicKey = basicKey;
    }

    public static void Delete(string key)
    {
        if (HttpRuntime.Cache[key] != null)
        {
            HttpRuntime.Cache.Remove(key);
        }
    }

    public static bool Exists(string key)
    {
        return (Get(key) != null);
    }

    public static void FlushAll()
    {
        IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
        while (enumerator.MoveNext())
        {
            HttpRuntime.Cache.Remove(enumerator.Key.ToString());
        }
    }

    public static object Get(string key)
    {
        return HttpRuntime.Cache[key];
    }

    public static void Set(string key, object value, int seconds)
    {
        if (value != null)
        {
            TimeSpan span = new TimeSpan(0, 0, seconds);
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.Add(span), TimeSpan.Zero, CacheItemPriority.Normal, null);
        }
    }

    public static void SetLast(string key, object value)
    {
        TimeSpan span = new TimeSpan(0xe42, 0, 0, 0, 0);
        if (value != null)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.Add(span), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }
    }


    
}
}
