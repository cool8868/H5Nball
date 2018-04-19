using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Games.NBall.Bll.Cache;
using Games.NBall.WebClient.Util;
using Games.NBall.WebClient.Data;

namespace Games.NBall.WebClient.Plat
{
    public class PlatSessionProvider : IPlatSessionProvider
    {
        #region Cache
        public static readonly string PlatCookieName = "nb_plat";
        public static readonly int PlatExpireInMinutes = 7 * 24 * 60;
        protected int _sessionMode;
        protected string _shareDomain;
        #endregion

        #region Singleton
        static readonly PlatSessionProvider s_instance = new PlatSessionProvider();
        public static PlatSessionProvider Instance
        {
            get { return s_instance; }
        }
        #endregion

        #region .ctor
        protected PlatSessionProvider()
        {
            var plat = SiteMapCache.Instance().GetPlatConfig();
            if (null != plat)
            {
                _sessionMode = plat.SessionMode;
                _shareDomain = plat.ShareDomain;
            }
        }
        #endregion

     
        #region IPlatSession
        public IPlatSessionData LoginSession(string colUid, int sessionMode = -1, params string[] colArgs)
        {
            var session = InitSession(colUid, colArgs);
            if (!this.ValidateSession(session))
                return session;
            this.SaveSession(session, sessionMode);
            return session;
        }
        public IPlatSessionData LoadSession(string colUid, int sessionMode = -1, params string[] colArgs)
        {
            var session = this.InitSession(colUid, colArgs);
            if (sessionMode < 0 && this.ValidateSession(session))
                return session;
            session.LastSiteId = string.Empty;
            this.LoadSession(session, sessionMode);
            return session;
        }
        public virtual IPlatSessionData InitSession(string colUid, params string[] colArgs)
        {
            var session = new PlatSessionData();
            PlatSessionUtil.InitSession(session, colUid, colArgs);
            return session;
        }
        public bool LoadSession(IPlatSessionData session, int sessionMode = -1)
        {
            if (sessionMode < 0)
                sessionMode = this._sessionMode;
            if (CanSessionFlag(sessionMode) && LoadFromSession(session)
                || LoadFromCookie(session))
                return true;
            return CanDbFlag(sessionMode) && LoadFromDb(session);
        }
        public bool SaveSession(IPlatSessionData session, int sessionMode = -1)
        {
            if (sessionMode < 0)
                sessionMode = this._sessionMode;
            bool val = SaveToCookie(session);
            if (CanSessionFlag(sessionMode))
                val |= SaveToSession(session);
            if (CanDbFlag(sessionMode))
                val |= SaveToDb(session);
            return val;
        }
        protected bool CanSessionFlag(int sessionMode)
        {
            return (sessionMode & (int)EnumPlatSessionMode.Session) > 0;
        }
        protected bool CanDbFlag(int sessionMode)
        {
            return (sessionMode & (int)EnumPlatSessionMode.Db) > 0;
        }
        #endregion

        #region virtual
        protected virtual bool ValidateSession(IPlatSessionData session)
        {
            return !string.IsNullOrEmpty(session.Uid);
        }
        protected virtual bool LoadFromSession(IPlatSessionData session)
        {
            var obj = HttpContext.Current.Session[PlatCookieName];
            if (null == obj)
                return false;
            string value = obj.ToString();
            return session.FromPlatString(value, (int)EnumPlatSessionMode.Session);
        }
        protected virtual bool SaveToSession(IPlatSessionData session)
        {
            string value = session.ToPlatString((int)EnumPlatSessionMode.Session);
            if (string.IsNullOrEmpty(value))
                return false;
            HttpContext.Current.Session[PlatCookieName] = value;
            return true;
        }
        protected virtual bool LoadFromCookie(IPlatSessionData session)
        {
            HttpCookie obj = null;
            string[] keys = HttpContext.Current.Response.Cookies.AllKeys;
            if (null != keys && keys.Contains(PlatCookieName))
                obj = HttpContext.Current.Response.Cookies[PlatCookieName];
            if (null == obj || string.IsNullOrEmpty(obj.Value))
                obj = HttpContext.Current.Request.Cookies[PlatCookieName];
            if (null == obj || string.IsNullOrEmpty(obj.Value))
                return false;
            string value = CryptoUtil.FromBase64(obj.Value);
            return session.FromPlatString(value, (int)EnumPlatSessionMode.Cookie);
        }
        protected virtual bool SaveToCookie(IPlatSessionData session)
        {
            string value = session.ToPlatString((int)EnumPlatSessionMode.Cookie);
            if (string.IsNullOrEmpty(value))
                return false;
            var cookie = new HttpCookie(PlatCookieName);
            cookie.Value = CryptoUtil.ToBase64(value);
            cookie.Expires = DateTime.Now.AddMinutes(PlatExpireInMinutes);
            if (!string.IsNullOrEmpty(_shareDomain))
                cookie.Domain = _shareDomain;
            HttpContext.Current.Response.Cookies.Set(cookie);
            return true;
        }
        protected virtual bool LoadFromDb(IPlatSessionData session)
        {
            return false;
        }
        protected virtual bool SaveToDb(IPlatSessionData session)
        {
            return false;
        }
        #endregion



    }
}
