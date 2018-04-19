using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity
{
    public enum EnumCoinConsumeSourceType
    {
        /// <summary>
        /// 拍卖行托管费
        /// </summary>
        AuctionFee = 1,
        /// <summary>
        /// 商城购买
        /// </summary>
        MallBuy,
        /// <summary>
        /// 球探
        /// </summary>
        Scouting,
        /// <summary>
        /// 强化
        /// </summary>
        Strength,
        /// <summary>
        /// 球员卡合成
        /// </summary>
        PlayerCardSynthesis,
        /// <summary>
        /// 装备合成
        /// </summary>
        EquipmentSynthesis,
        /// <summary>
        /// 球魂合成
        /// </summary>
        BallsoulSynthesis,
        /// <summary>
        /// 洗炼石合成
        /// </summary>
        WashstoneSynthesis,
        /// <summary>
        /// 技能学习
        /// </summary>
        SkillAsk,

        /// <summary>
        /// 技能学习
        /// </summary>
        SkillUpgrade,
        /// <summary>
        /// 公会创建
        /// </summary>
        GuildCreate,

        /// <summary>
        /// 教练系统
        /// </summary>
        CoachModel,

        /// <summary>
        /// 球员传承
        /// </summary>
        PlayerInherit,

        /// <summary>
        /// 装备进阶
        /// </summary>
        EquipmentUpgrade,

        /// <summary>
        /// 装备精铸金币消耗
        /// </summary>
        EquipmentPrecisionCasting,
        /// <summary>
        /// 巅峰之战鼓舞
        /// </summary>
        CrossPeak,
        /// <summary>
        /// 公会捐献
        /// </summary>
        GuildGive,
        /// <summary>
        /// 翻牌活动
        /// </summary>
        NationalDayActivity,
        /// <summary>
        /// 世界杯点球活动
        /// </summary>
        ADTopScorer,
        /// <summary>
        /// 球探金币刷新
        /// </summary>
        ScoutingReFresh,
        /// <summary>
        /// 绑定商城购买
        /// </summary>
        BindMallBuy,
        /// <summary>
        /// 经典球衣
        /// </summary>
        ClubClothes,
        /// <summary>
        /// 杯赛报名
        /// </summary>
        DailycupAttend,
        /// <summary>
        /// PK赛刷新
        /// </summary>
        PkMatchRefresh,
        /// <summary>
        /// 球员升星
        /// </summary>
        PlayerTheStar,
    }
}
