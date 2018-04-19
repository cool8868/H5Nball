using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.OAuth2.Data;
using Games.NBall.WebClient.Weibo.Data;

namespace Games.NBall.WebClient.Weibo
{
    public class WeiboApi
    {
        #region OAuth2
        public static string GetAuthUrl(Req_WeiboAuthCode reqArgs)
        {
            return string.Empty;
        }
        public static string GetAccessToken(Req_OAuth2AccessToken reqArgs)
        {
            if (null == reqArgs || !reqArgs.ValidateValue())
                return WebErrorCode.RequiredArgs;
            return string.Empty;
        }
        #endregion
    }
}
