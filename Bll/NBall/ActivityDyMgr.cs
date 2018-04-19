using Games.NBall.Entity.NBall.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.NBall;

namespace Games.NBall.Bll.NBall
{
     public class ActivityDyMgr
    {
        public static List<ActivityDyUserEntity> GetManagerIdDyStrength(string zoneId = "")
        {
            var provider = new ActivityDyProvider(zoneId);
            return provider.GetManagerIdDyStrength(zoneId);
        }

        public static List<ActivityDyUserEntity> GetDyLadderRank(string zoneId = "")
        {
            var provider = new ActivityDyProvider(zoneId);
            return provider.GetDyLadderRank(zoneId);
        }

        public static List<ActivityDyUserEntity> GetDyPowerRank(string zoneId = "")
        {
            var provider = new ActivityDyProvider(zoneId);
            return provider.GetDyPowerRank(zoneId);
        }
    }
}
