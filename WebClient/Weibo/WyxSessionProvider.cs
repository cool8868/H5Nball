using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.Weibo.Data;
using Games.NBall.WebClient.Util;
using Games.NBall.WebClient.Plat;
using Games.NBall.Common;

namespace Games.NBall.WebClient.Weibo
{
    public class WyxSessionProvider : PlatSessionProvider
    {
        #region Singleton
        static readonly WyxSessionProvider s_instance = new WyxSessionProvider();
        public static new WyxSessionProvider Instance
        {
            get { return s_instance; }
        }
        #endregion


        public WyxSessionInfo LoadWyxSession(int sessionMode = -1)
        {
            return this.LoadSession(WyxSessionInfo.COLUid, sessionMode, WyxSessionInfo.ALLKeys) as WyxSessionInfo;
        }

        #region override
        public override IPlatSessionData InitSession(string colUid, params string[] colArgs)
        {
            var session = new WyxSessionInfo();
            PlatSessionUtil.InitSession(session, WyxSessionInfo.COLUid, WyxSessionInfo.ALLKeys);
            session.FromCollection(HttpContext.Current.Request.QueryString, false, WyxSessionInfo.ALLKeys);
            return session;
        }
        protected override bool ValidateSession(IPlatSessionData session)
        {
            var data = session as WyxSessionInfo;
            if (null == data)
                return false;
            if (!data.ValidateValue() || string.IsNullOrEmpty(data.Uid))
            {
                data.ErrorCode = WebErrorCode.RequiredArgs;
                return false;
            }
            string secret = WyxCache.AppCfg.AppSecret;
            if (string.IsNullOrEmpty(data.Signature) || !CheckSignature(data, secret))
            {
                data.ErrorCode = WebErrorCode.ErrorSignature;
                LogHelper.Insert(string.Format("InvalidSession:{0}", data), LogType.Error);
                return false;
            }
            data.ErrorCode = WebErrorCode.OK;
            return true;
        }
        bool CheckSignature(WyxSessionInfo data, string secret)
        {
            if (string.IsNullOrEmpty(data.Signature))
                return false;
            var signDic = data.ToCollection(true, WyxSessionInfo.SIGNKeys);
            string baseStr = WebUtil.BuildEscapeDataString(signDic);
            signDic.Clear();
            return string.Compare(data.Signature, CryptoUtil.GetSHA1(baseStr + secret, "x2"), true) == 0;
        }
        #endregion
    }
}
