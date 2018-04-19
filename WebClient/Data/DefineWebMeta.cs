using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Data
{
    public enum EnumWebRequestMethod
    {
        GET,
        POST,
        PUT,
        DELETE,
    }

    public struct EnumWebContentType
    {
        public const string PostContentType = "application/x-www-form-urlencoded";
    }

    public struct EnumWebUserAgent
    {
        public const string Mozilla40 = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0)";
    }
}
