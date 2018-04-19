
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class CrosscrowdInfoMgr
    {
        public static List<CrosscrowdSendRankPrizeEntity> GetForSendRankPrize(int crowdId)
        {
            var provider = new CrosscrowdInfoProvider();
            return provider.GetForSendRankPrize(crowdId);
        }

        public static CrosscrowdSendRankPrizeEntity GetMaxKiller(int crowdId)
        {
            var provider = new CrosscrowdInfoProvider();
            return provider.GetMaxKiller(crowdId);
        }

        public static List<CrosscrowdSendKillPrizeEntity> GetForSendKillPrize(int crowdId)
        {
            var provider = new CrosscrowdInfoProvider();
            return provider.GetForSendKillPrize(crowdId);
        }
	}
}
