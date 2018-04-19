using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Weibo.Data
{
    public class DefineWeiboUri
    {
        public const string PREFIXOAuth2 = "https://api.weibo.com/oauth2";
        public const string PREFIXWeiboUriV1 = "https://api.weibo.com/1";
        public const string PREFIXWeiboUriV2 = "https://api.weibo.com/2";

        #region OAuth2
        public static readonly string OAuth2_Authorize = string.Format("{0}/authorize", PREFIXOAuth2);
        public static readonly string OAuth2_AccessToken = string.Format("{0}/access_token", PREFIXOAuth2);
        #endregion
    }
}
