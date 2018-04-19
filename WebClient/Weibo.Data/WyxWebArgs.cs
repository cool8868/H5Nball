using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.WebClient.Data;

namespace Games.NBall.WebClient.Weibo.Data
{
    public class WyxWebArgs : BaseWebArgs
    {
        #region Names
        public const string COLAppKey = "source";
        public const string COLTimestamp = "timestamp";
        public const string COLSessionKey = "session_key";
        public const string COLSignature = "signature";
        #endregion

        #region Values
        public string AppKey
        {
            get { return GetValue(COLAppKey); }
            set { SetValue(COLAppKey, value); }
        }
        public string Timestamp
        {
            get { return GetValue(COLTimestamp); }
            set { SetValue(COLTimestamp, value); }
        }
        public string SessionKey
        {
            get { return GetValue(COLSessionKey); }
            set { SetValue(COLSessionKey, value); }
        }
        public string Signature
        {
            get { return GetValue(COLSignature); }
            set { SetValue(COLSignature, value); }
        }
        #endregion
    }
}
