using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Games.NBall.Entity;
using Games.NBall.Bll.Cache;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.Weibo.Data;
using Games.NBall.WebClient.Util;


namespace Games.NBall.WebClient.Weibo
{
    public class WyxCache
    {
        static readonly WyxAppInfo s_appCfg = new WyxAppInfo();
        static readonly object s_lockObj = new object();
        static volatile bool s_initFlag = false;
        public static WyxAppInfo AppCfg
        {
            get
            {
                if (s_initFlag)
                    return s_appCfg;
                lock (s_lockObj)
                {
                    if (s_initFlag)
                        return s_appCfg;
                    s_appCfg.AutoRegister = !string.IsNullOrEmpty(ConfigurationManager.AppSettings[WyxAppInfo.COLAutoReg]);
                    var obj = SiteMapCache.Instance();
                    if (!obj.InitFlag)
                        return s_appCfg;
                    var platCfg = obj.GetPlatConfig();
                    if (null != platCfg)
                    {
                        s_appCfg.AppKey = platCfg.AppKey;
                        s_appCfg.AppSecret = platCfg.AppSecret;
                        s_appCfg.OpenLoginUrl = platCfg.PlatMainUrl;
                        string apiUrl = platCfg.PlatApiUrl;
                        if (string.IsNullOrEmpty(apiUrl))
                            apiUrl = DefineWyxUri.PREFIXWyxUriV1;
                        s_appCfg.OpenApiUrl = apiUrl;
                    }
                    s_initFlag = true;
                }
                return s_appCfg;
                
            }
        }
       
    }
}
