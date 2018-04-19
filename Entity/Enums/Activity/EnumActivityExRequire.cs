using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Activity
{
    public enum EnumActivityExRequire
    {
        /// <summary>
        /// 充值
        /// </summary>
        Charge = 1,
        Lottery = 2,
        Strength = 3,
        Synthesis = 4,
        Wash = 5,

        /// <summary>
        /// 单笔充值
        /// </summary>
        SingleCharge = 6,
        /// <summary>
        /// 登录
        /// </summary>
        Login = 7,

        Ladder = 8,
        WorldChallenge = 9,
        Skillask = 10,
        Train = 11,
        BuyMallItem = 12,
        ManagerLevel = 13,
        TeammemberColect = 14,
        /// <summary>
        /// 等级大排行
        /// </summary>
        LevelRank = 15,

        TourLottery = 16,
        ScoutingOrange = 17,
        ScoutingCoin = 18,
        ScoutingPoint = 19,
        PlayerSysthesis = 25,
        EquipmentSysthesis = 26,
        LadderScore=27,
        WorldChallengeStage=28,
        ChargeReturnPoint = 29,
        PlayerSysthesisRank=30,
        ArenaWin=31,
        ManagerLevelRank=32,
        Consume = 33,
        WorldChallengeMaxMark = 34,
        WordChallengeMarkRank = 35,
        Kpi = 36,
        KpiRank = 37,
        /// <summary>
        /// 强化送合同页
        /// </summary>
        strengthening=38,


        ScoutingCoinRefresh = 39,
        ScoutingPointRefresh = 40,
        /// <summary>
        /// 强化+9送欧冠合同页
        /// </summary>
        strengthningEuro=41,

        /// <summary>
        /// 点球大战跨服排名
        /// </summary>
        TopScoreCrossRank = 42,
        /// <summary>
        /// 洗练锁定消耗
        /// </summary>
        EquipmentLockProperty = 43,

        /// <summary>
        ///跨服充值排名 
        /// </summary>
        ChargeCrossRank=44,
        /// <summary>
        ///跨服充值单日排名 
        /// </summary>
        ChargeDailyCrossRank = 45,



        #region  New
        /// <summary>
        /// 天梯每日获胜
        /// </summary>
        LadderDayPrzie = 101,
        /// <summary>
        /// 欧洲杯抽卡得球星碎片宝箱
        /// </summary>
        ScoutingEurope=102,

        /// <summary>
        /// 欧洲杯狂欢
        /// </summary>
        EuropeHilarity = 103,
        /// <summary>
        /// 欧洲杯抽卡得巨星碎片宝箱
        /// </summary>
        ScoutingHugeEurope = 104,
        /// <summary>
        /// 合成球员卡
        /// </summary>
        SynthesisPlayer=105,

        /// <summary>
        /// 抽A卡
        /// </summary>
        ScoutingACard=106,
        /// <summary>
        /// 抽A+卡
        /// </summary>
        ScoutingACardAdd=107,
        /// <summary>
        /// 充值计数活动
        /// </summary>
        ChargeCount = 108,
        /// <summary>
        /// 合成S卡
        /// </summary>
        SynthesisCardS = 109,
        /// <summary>
        /// 抽S卡碎片
        /// </summary>
        ScoutingCardS = 110,
        /// <summary>
        /// 强9返卡
        /// </summary>
        Strengthen9ReturnCard = 111,
        /// <summary>
        /// 抽S卡或碎片
        /// </summary>
        ScoutingCardOrDebris=112,
        #endregion
    }
}
