using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Games.NBall.Core;

namespace Games.NBall.NB_Web.BaseCommon
{
    public class OnlineMgr
    {
        public static bool LoginSession(Guid managerId, string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))//PC端
            {
                sessionId = GetSessionId4PC();
            }
            return OnlineCore.LoginSession(managerId, sessionId);
        }

        /// <summary>
        /// 检查踢线状态，并根据参数[haltFlag]决定是否注销Cookie
        /// </summary>
        /// <param name="sessionId">SessinId</param>
        /// <param name="haltFlag">是否清除Cookie</param>
        /// <param name="bumpFlag">抢线标记,True为被用户抢线,False为系统踢线</param>
        /// <returns>踢线标记,True为被用户或系统踢线</returns>
        public static bool CheckAndKickSession(string sessionId, bool haltFlag, out bool bumpFlag)
        {
            bool kickState = CheckKickNBumpState(sessionId, out bumpFlag);
            if (kickState && haltFlag)
            {
                //注销Cookie
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                FormsAuthentication.SignOut();
            }
            return kickState;
        }

        /// <summary>
        /// 获取PC端的Session标识
        /// </summary>
        /// <returns></returns>
        public static string GetSessionId4PC()
        {
            var ip = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
            var port = HttpContext.Current.Request.Url.Port;
            var session = HttpContext.Current.Session.SessionID;
            return string.Format("{0}:{1}-{2}", ip, port, session);
        }

        public static bool CheckKickNBumpState(string sessionId, out bool bumpFlag)
        {
            return OnlineCore.CheckKickNBumpState(sessionId, out bumpFlag);
        }

        /// <summary>
        /// 保持Session
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="sessionId">Session标识</param>
        /// <returns></returns>
        public static bool ForceSession(Guid managerId, string sessionId)
        {
            return OnlineCore.ForceSession(managerId, sessionId);
        }
    }
}
