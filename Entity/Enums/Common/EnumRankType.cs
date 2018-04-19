using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Common
{
    public enum EnumRankType
    {
        /// <summary>
        /// 等级排行
        /// </summary>
        LevelRank=1,
        /// <summary>
        /// 综合实力排行
        /// </summary>
        KpiRank=2,
        /// <summary>
        /// 冠军杯积分排行
        /// </summary>
        ScoreRank=3,
        /// <summary>
        /// 天梯排行
        /// </summary>
        LadderRank=4,
        /// <summary>
        /// 竞技场排行
        /// </summary>
        ArenaRank = 5,
        /// <summary>
        /// 群雄逐鹿
        /// </summary>
        CrowdRank=6,
        /// <summary>
        /// 跨服群雄逐鹿
        /// </summary>
        CrossCrowdRank = 7,
        /// <summary>
        /// 跨服天梯排行
        /// </summary>
        CrossLadderRank = 8,
        /// <summary>
        /// 跨服天梯每日排行
        /// </summary>
        CrossLadderDailyRank = 9,

        /// <summary>
        /// 璀璨巨星转盘每日排行
        /// </summary>
        CrossDialDailyRank=10,
        /// <summary>
        /// 璀璨巨星转盘每周排行
        /// </summary>
        CrossDialWeekRank=11,
    }
}
