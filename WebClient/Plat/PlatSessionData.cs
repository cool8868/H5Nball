using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Plat
{
    public class PlatSessionData : IPlatSessionData
    {
        public string Uid
        {
            get;
            set;
        }
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
            return PlatSessionUtil.ToCookieString(this);
        }
        public bool FromPlatString(string platStr, int sessionMode)
        {
            return PlatSessionUtil.FromCookieString(this, platStr);
        }
    }
}
