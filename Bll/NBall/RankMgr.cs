using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.NBall;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.Rank;

namespace Games.NBall.Bll.NBall
{
    public class RankMgr
    {
        public static List<BaseRankEntity> GetRankList(EnumRankType rankType, int domainId)
        {
            var provider = new RankProvider();
            return provider.GetRankList(rankType, domainId);
        }
    }
}
