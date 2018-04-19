using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Weibo.Data
{
    public class WyxAppInfo
    {
        #region Names
        public const string COLAutoReg = "wyx_autoreg";
        public const string COLAppkey = "wyx_appkey";
        public const string COLAppSecret = "wyx_appsecret";
        public const string COLApiUrl = "wyx_apiurl";
        public const string COLOpenLogin = "OpenLogin";
        #endregion

        #region Values
        public bool AutoRegister
        {
            get;
            set;
        }
        public string AppKey
        {
            get;
            set;
        }
        public string AppSecret
        {
            get;
            set;
        }
        public string OpenApiUrl
        {
            get;
            set;
        }
        public string OpenLoginUrl
        {
            get;
            set;
        }
        public string ShareDomain
        {
            get;
            set;
        }
        public string SessionMode
        {
            get;
            set;
        }
        #endregion
    }
}
