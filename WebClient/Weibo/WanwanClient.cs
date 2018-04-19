using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using System.Configuration;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.Weibo.Data;
using Games.NBall.WebClient.Util;
using Games.NBall.Common;


namespace Games.NBall.WebClient.Weibo
{
    public class WanwanClient : WebClientBase
    {
        #region Cache
        readonly WanwanSessionInfo _session = new WanwanSessionInfo();
        #endregion

        #region .ctor
        public WanwanClient(string uri, WyxWebArgs getArgs = null, WyxWebArgs postArgs = null)
            : base(uri, getArgs, postArgs)
        {
            this._baseUri = GetApiJsonUrl(uri);
            this._session = WanwanSessionProvider.Instance.LoadWanwanSession();
            this.NoSesssionKey = false;
            this.Timeout = 3000;
        }
        #endregion

        #region Data
        public bool NoSesssionKey
        {
            get;
            set;
        }
        #endregion

        #region Custom
        protected override IWebArgs CreateCommonArgs()
        {
            return new WyxWebArgs();
        }
        protected override void PrepareArgs(IWebArgs args)
        {
            var wyxArgs = args as WyxWebArgs;
            if (null == wyxArgs)
                return;
            wyxArgs.AppKey = WyxCache.AppCfg.AppKey;
            wyxArgs.Timestamp = WeiboUtil.GetTimeStamp().ToString();
            if (!this.NoSesssionKey && !string.IsNullOrEmpty(_session.SessionKey))
                wyxArgs.SessionKey = _session.SessionKey;
            var signKeys = wyxArgs.GetCollection().AllKeys;
            Array.Sort(signKeys);
            var signDic = wyxArgs.ToCollection(true, signKeys);
            string baseStr = WebUtil.BuildEscapeDataString(signDic);
            signDic.Clear();
            wyxArgs.Signature = CryptoUtil.GetSHA1(baseStr + WyxCache.AppCfg.AppSecret, "x2");
        }
        protected override void PrepareRequest(HttpWebRequest req)
        {
            if (string.IsNullOrEmpty(this._session.SessionKey))
                throw new Exception(WebErrorCode.MissWyxSession);
        }
        protected override string PostResponseText(string respText)
        {
            if (null == respText)
                return string.Empty;
            if (respText.StartsWith(@"{""request"":"))
            {
                throw new Exception(string.Format("return:{0} session:{1}", respText, _session));
            }
            return respText;
        }
        #endregion

        #region Navive
        protected string GetApiJsonUrl(string uri)
        {
            if (uri.StartsWith("http:", true, System.Globalization.CultureInfo.InvariantCulture))
                return uri;
            string baseUri = WyxCache.AppCfg.OpenApiUrl;
            return string.Format("{0}{1}{2}.json", baseUri, baseUri.EndsWith(@"/") ? "" : @"/", uri);
        }
        #endregion
    }
}
