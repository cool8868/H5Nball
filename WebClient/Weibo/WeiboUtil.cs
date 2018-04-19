using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.WebClient.Weibo
{
    public static class WeiboUtil
    {
        static readonly DateTime DTUnixOrig = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        static readonly Random s_rand = new Random();
       
       
        public static long GetTimeStamp()
        {
            return Convert.ToInt64(DateTime.UtcNow.Subtract(DTUnixOrig).TotalSeconds);
        }
        public static int GetNonce()
        {
            lock (s_rand)
            {
                return s_rand.Next(123400, 9999999);
            }
        }
       
    }
}
