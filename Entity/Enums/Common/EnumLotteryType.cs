using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 抽卡类型枚举
    /// </summary>
    public enum EnumLotteryType
    {
        /// <summary>
        /// PK赛
        /// </summary>
        PlayerKill = 1,

        /// <summary>
        /// 球探抽卡
        /// </summary>
        Scouting = 2,

        /// <summary>
        /// 杯赛奖励
        /// </summary>
        DailyCup= 3,

        /// <summary>
        /// 帮助好友训练宝箱
        /// </summary>
        TrainHelpOpenBox = 4,

        /// <summary>
        /// 节日礼包
        /// </summary>
        HolidayGifts = 7,

        /// <summary>
        /// 球探抽卡（新）
        /// </summary>
        Lottery = 9,

        /// <summary>
        /// 精彩活动
        /// </summary>
        ActivityEx = 10,

        ///// <summary>
        ///// 巡回赛抽卡
        ///// </summary>
        //Tour = 1,
        ///// <summary>
        ///// 球探抽卡
        ///// </summary>
        //Scouting = 2,
        ///// <summary>
        ///// 天梯奖励
        ///// </summary>
        //Ladder = 3,
        ///// <summary>
        ///// 联赛抽卡
        ///// </summary>
        //League = 4,
        ///// <summary>
        ///// 世界挑战赛
        ///// </summary>
        //WorldChallenge = 5,
        ///// <summary>
        ///// PK赛
        ///// </summary>
        //PlayerKill = 6,
        ///// <summary>
        ///// 节日礼包
        ///// </summary>
        //HolidayGifts = 7,
        ///// <summary>
        ///// 传奇精魄
        ///// </summary>
        //Legend = 8,

        ///// <summary>
        ///// 球探抽卡（新）
        ///// </summary>
        //Lottery = 9,
    }
}
