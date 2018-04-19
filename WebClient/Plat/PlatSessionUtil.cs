using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Games.NBall.Bll.Cache;

namespace Games.NBall.WebClient.Plat
{
    public static class PlatSessionUtil
    {
        #region LoginSession
        public static void InitSession(IPlatSessionData data, string colUid, params string[] colArgs)
        {
            data.LastSiteId = SiteMapCache.SiteId;
            var nvc = HttpContext.Current.Request.QueryString;
            if (null == nvc)
                return;
            data.Uid = nvc[colUid] ?? string.Empty;
            if (null == colArgs || colArgs.Length == 0)
                data.AuthArgs = HttpContext.Current.Request.Url.Query.TrimStart('?');
            else
            {
                var sb = new StringBuilder();
                foreach (var col in colArgs)
                {
                    sb.AppendFormat("{0}{1}={2}", sb.Length == 0 ? "" : "&", col, nvc[col] ?? string.Empty);
                }
                data.AuthArgs = sb.ToString();
                sb.Clear();
            }
        }
        #endregion

        #region CookieEncode
        public static string ToCookieString(IPlatSessionData data)
        {
            return string.Format("{0}${1}${2}", data.Uid, data.LastSiteId, data.AuthArgs);
        }
        public static bool FromCookieString(IPlatSessionData data, string value)
        {
            if (string.IsNullOrEmpty(value) || null == data)
                return false;
            var splits = value.Split('$');
            if (splits.Length < 3)
                return false;
            data.Uid = splits[0];
            data.LastSiteId = splits[1];
            data.AuthArgs = splits[2];
            return true;
        }
        #endregion

    }
}
