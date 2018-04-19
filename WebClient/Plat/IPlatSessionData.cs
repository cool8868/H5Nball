using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Plat
{
    public interface IPlatSessionData
    {
        string Uid
        {
            get;
            set;
        }
        string LastSiteId
        {
            get;
            set;
        }
        string AuthArgs
        {
            get;
            set;
        }
        string ErrorCode
        {
            get;
            set;
        }
        string ToPlatString(int sessionMode);
        bool FromPlatString(string platStr, int sessionMode);
    }
}
