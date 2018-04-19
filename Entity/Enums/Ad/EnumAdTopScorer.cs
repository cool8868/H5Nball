using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Ad
{
    public enum EnumAdType
    {
        Ad0 = 90,
        /// <summary>
        /// 世界杯活动
        /// </summary>
        WorldCup = 91,
        Ad2 = 92,
        Ad3 = 93,
        Ad4 = 94,
        Ad5 = 95,
        Ad6 = 96,
        Ad7 = 97,
        Ad8 = 98,
        Ad9 = 99,

    }
    public enum EnumTopScorerPrizeRank
    {
        /// <summary>
        /// 进球数
        /// </summary>
        CountScore = 1,
        /// <summary>
        /// 连击数
        /// </summary>
        CombScore = 2,
        /// <summary>
        /// 最终得分
        /// </summary>
        FinalScore = 3,
    }

    [Flags]
    public enum EnumTopScorerLockState
    {
        None = 0,
        /// <summary>
        /// 锁定3连击奖励
        /// </summary>
        Lock3 = 1,
        /// <summary>
        /// 锁定4连击奖励
        /// </summary>
        Lock4 = 2,
        /// <summary>
        /// 锁定3连击和4连击奖励
        /// </summary>
        Lock3N4 = 3,

    }
}
