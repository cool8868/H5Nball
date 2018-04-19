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
    public class WyxSessionInfo : BaseWebArgs, IPlatSessionData
    {
        #region Names
        public const string COLUid = "wyx_user_id";
        public const string COLSessionKey = "wyx_session_key";
        public const string COLCreate = "wyx_create";
        public const string COLExpire = "wyx_expire";
        public const string COLSignature = "wyx_signature";
        public const string COLAccessToken = "access_token";
        public static readonly string[] SIGNKeys = new string[] { COLCreate, COLExpire, COLSessionKey, COLUid };
        public static readonly string[] ALLKeys = new string[] { COLCreate, COLExpire, COLSessionKey, COLUid, COLSignature };
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
        public string Create
        {
            get { return GetValue(COLCreate); }
            set { SetValue(COLCreate, value); }
        }
        public string Expire
        {
            get { return GetValue(COLExpire); }
            set { SetValue(COLExpire, value); }
        }
        public string Signature
        {
            get { return GetValue(COLSignature); }
            set { SetValue(COLSignature, value); }
        }
        public string AccessToken
        {
            get { return GetValue(COLAccessToken); }
            set { SetValue(COLAccessToken, value); }
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
            return !string.IsNullOrEmpty(this.Uid)
              && !string.IsNullOrEmpty(this.SessionKey);
        }
        bool FromSessionKey(string sessionKey)
        {
            this.SessionKey = sessionKey;
            var splits = sessionKey.Split('_');
            if (splits.Length < 3)
                return false;
            this.Create = splits[1];
            this.Uid = splits[2];
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}.{2}.{3}.{4}.{5}",
                LastSiteId ?? string.Empty, Uid ?? string.Empty, SessionKey ?? string.Empty,
                Create ?? string.Empty, Expire ?? string.Empty, Signature ?? string.Empty);
        }


    }
}
