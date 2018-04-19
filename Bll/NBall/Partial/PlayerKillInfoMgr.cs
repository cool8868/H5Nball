
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;
using Games.NBall.Entity.Response;

namespace Games.NBall.Bll
{
    
    public partial class PlayerkillInfoMgr
    {
        public static List<PlayerKillOpponentEntity> GetOpponents(int minKpi, int maxKpi, int configByTimes)
        {
            var provider = new PlayerkillInfoProvider();
            return provider.GetOpponents(minKpi, maxKpi, configByTimes);
        }

        public static PlayerKillOpponentEntity GetOpponentByName(string name, int configByTimes)
        {
            var provider = new PlayerkillInfoProvider();
            return provider.GetOpponentByName(name, configByTimes);
        }
	}
}

