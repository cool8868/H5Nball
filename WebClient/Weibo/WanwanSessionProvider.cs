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
    public class WanwanSessionProvider : PlatSessionProvider
    {
        #region Singleton
        static readonly WanwanSessionProvider s_instance = new WanwanSessionProvider();
        public static new WanwanSessionProvider Instance
        {
            get { return s_instance; }
        }
        #endregion


        public WanwanSessionInfo LoadWanwanSession(int sessionMode = -1)
        {
            return this.LoadSession(WanwanSessionInfo.COLUid, sessionMode, WanwanSessionInfo.ALLKeys) as WanwanSessionInfo;
        }

        #region override
        public override IPlatSessionData InitSession(string colUid, params string[] colArgs)
        {
            var session = new WanwanSessionInfo();
            PlatSessionUtil.InitSession(session, WanwanSessionInfo.COLUid, WanwanSessionInfo.ALLKeys);
            session.FromCollection(HttpContext.Current.Request.QueryString, false, WanwanSessionInfo.ALLKeys);
            if (string.IsNullOrEmpty(session.Uid))
            {
                if (null != HttpContext.Current.Session)
                {
                    var obj = HttpContext.Current.Session[WanwanSessionInfo.COLUid];
                    if (null != obj)
                    {
                        session.Uid = obj.ToString();
                        HttpContext.Current.Session[WanwanSessionInfo.COLUid] = null;
                    }
                }
            }
            return session;
        }
        protected override bool ValidateSession(IPlatSessionData session)
        {
            var data = session as WanwanSessionInfo;
            if (null == data || string.IsNullOrEmpty(data.SessionKey))
                return false;
            data.ErrorCode = WebErrorCode.OK;
            return true;
        }
        #endregion
    }
}
