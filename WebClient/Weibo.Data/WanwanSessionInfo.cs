using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.Plat;

namespace Games.NBall.WebClient.Weibo.Data
{
    public class WanwanSessionInfo : BaseWebArgs, IPlatSessionData
    {
        #region Names
        public const string COLUid = "uid";
        public const string COLSessionKey = "session_key";
        public const string COLTicket = "ticket";
        public const string COLSignature = "signature";
        public static readonly string[] ALLKeys = new string[] { COLSessionKey, COLTicket, COLSignature };
        #endregion

        #region Values
        public string Uid
        {
            get { return GetValue(COLUid); }
            set { SetValue(COLUid, value); }
        }
        public string SessionKey
        {
            get { return GetValue(COLSessionKey); }
            set { SetValue(COLSessionKey, value); }
        }
        public string Ticket
        {
            get { return GetValue(COLTicket); }
            set { SetValue(COLTicket, value); }
 
        }
        #endregion

        #region IPlatSessionData
        public string LastSiteId
        {
            get;
            set;
        }
        public string AuthArgs
        {
            get;
            set;
        }
        public string ErrorCode
        {
            get;
            set;
        }
        public string ToPlatString(int sessionMode)
        {
            if (sessionMode == (int)EnumPlatSessionMode.Session)
                return this.SessionKey;
            this.AuthArgs = "uid=" + this.Uid + "&" + this.AuthArgs;
            return PlatSessionUtil.ToCookieString(this);
        }
        public bool FromPlatString(string platStr, int sessionMode)
        {
            if (string.IsNullOrEmpty(platStr))
                return false;
            if (sessionMode == (int)EnumPlatSessionMode.Session)
                return FromSessionKey(platStr);
            PlatSessionUtil.FromCookieString(this, platStr);
            this.FromCollection(HttpUtility.ParseQueryString(this.AuthArgs), false);
            return true;
        }
        #endregion

        public override bool ValidateValue()
        {
            return !string.IsNullOrEmpty(this.SessionKey);
        }
        bool FromSessionKey(string sessionKey)
        {
            this.SessionKey = sessionKey;
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}.{2}.{3}",
                LastSiteId ?? string.Empty, Uid ?? string.Empty, SessionKey ?? string.Empty,
                Ticket ?? string.Empty);
        }

    }
}
