using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.ServiceEngine;

namespace Games.NBall.Core
{
    public class MatchCdHandler
    {
        public static void SetCd(Guid managerId, EnumMatchType matchType, int cdTime)
        {
            SetCd(managerId, (int)matchType, cdTime);
        }

        public static void SetCd(Guid managerId, int matchType, int cdTime)
        {
            MemcachedFactory.MatchMutexClient.Set(BuildKey(managerId, matchType), DateTime.Now.AddSeconds(cdTime));
        }

        public static MessageCode CheckCd(Guid managerId, EnumMatchType matchType)
        {
            return CheckCd(managerId, (int)matchType);
        }

        public static MessageCode CheckCd(Guid managerId, int matchType)
        {
            double cdTime = -1;
            return CheckCd(managerId, matchType, ref cdTime);
        }

        public static MessageCode CheckCd(Guid managerId, EnumMatchType matchType, ref double cdTime)
        {
            return CheckCd(managerId, (int)matchType, ref cdTime);
        }

        public static MessageCode CheckCd(Guid managerId, int matchType, ref double cdTime)
        {
            var lastTime = MemcachedFactory.MatchMutexClient.Get<DateTime>(BuildKey(managerId, matchType));
            if (lastTime > DateTime.Now)
            {
                cdTime = lastTime.Subtract(DateTime.Now).TotalSeconds;
                return MessageCode.NbMatchCd;
            }
            else
            {
                cdTime = -1;
            }
            return MessageCode.Success;
        }

        public static int GetCdMilSecondsInt(Guid managerId, EnumMatchType matchType)
        {
            var lastTime = MemcachedFactory.MatchMutexClient.Get<DateTime>(BuildKey(managerId, (int)matchType));
            if (lastTime > DateTime.Now)
            {
                var d = lastTime.Subtract(DateTime.Now).TotalMilliseconds;
                return Convert.ToInt32(d);
            }
            else
            {
                return -1;
            }
        }

        public static int GetCdSecondsInt(Guid managerId, EnumMatchType matchType)
        {
            var cd = GetCdSeconds(managerId, (int)matchType);
            return Convert.ToInt32(cd);
        }

        public static double GetCdSeconds(Guid managerId, EnumMatchType matchType)
        {
            return GetCdSeconds(managerId, (int)matchType);
        }

        public static double GetCdSeconds(Guid managerId, int matchType)
        {
            var lastTime = MemcachedFactory.MatchMutexClient.Get<DateTime>(BuildKey(managerId, matchType));
            if (lastTime > DateTime.Now)
            {
                return lastTime.Subtract(DateTime.Now).TotalSeconds;
            }
            else
            {
                return -1;
            }
        }

        public static void Delete(Guid managerId, EnumMatchType matchType)
        {
            Delete(managerId, (int)matchType);
        }

        public static void Delete(Guid managerId, int matchType)
        {
            MemcachedFactory.MatchMutexClient.Delete(BuildKey(managerId, matchType));
        }

        static string BuildKey(Guid managerId, int matchType)
        {
            return string.Format("{0}.{1}", matchType, managerId);
        }
    }
}
