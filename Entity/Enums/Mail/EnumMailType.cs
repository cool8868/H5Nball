using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 邮件类型
    /// </summary>
    public enum EnumMailType
    {
        /// <summary>
        /// 天梯排名奖励
        /// </summary>
        LadderPrize = 1,
        /// <summary>
        /// 联赛奖励
        /// </summary>
        LeaguePrize = 2,
        /// <summary>
        /// 拍卖成功
        /// </summary>
        AuctionSaleSuccess = 3,
        /// <summary>
        /// 竞拍成功
        /// </summary>
        AuctionBuySuccess = 4,
        /// <summary>
        /// 巡回赛记录
        /// </summary>
        TourFight = 5,
        /// <summary>
        /// 竞技场排名奖励
        /// </summary>
        ArenaPrize = 6,
        /// <summary>
        /// 世界挑战赛排名奖励
        /// </summary>
        WorldChalengePrize = 7,
        /// <summary>
        /// 拍卖失败
        /// </summary>
        AuctionSaleFail = 8,
        /// <summary>
        /// 球探抽卡
        /// </summary>
        ScoutingLottery = 9,
        /// <summary>
        /// 竞拍被超出
        /// </summary>
        AuctionOver = 10,
        /// <summary>
        /// 取消拍卖
        /// </summary>
        AuctionCancel = 11,
        /// <summary>
        /// 杯赛基础奖励，未进入128强
        /// </summary>
        DailycupPrizeBase = 12,
        /// <summary>
        /// 杯赛奖励
        /// </summary>
        DailycupPrize = 13,
        /// <summary>
        /// 杯赛竞猜成功
        /// </summary>
        DailycupGambleSuccess = 14,
        /// <summary>
        /// 杯赛竞猜失败
        /// </summary>
        DailycupGambleFail = 15,
        /// <summary>
        /// 世界挑战赛关卡奖励
        /// </summary>
        WorldChallengeStagePrize = 16,
        /// <summary>
        /// 巡回赛挂机奖励(背包满)
        /// </summary>
        TourHookPrize = 17,
        /// <summary>
        /// 精英巡回赛奖励(背包满)
        /// </summary>
        TourElitePrize = 18,
        /// <summary>
        /// 友谊赛奖励(背包满)
        /// </summary>
        TourPrize = 19,
        /// <summary>
        /// 世界杯点球活动(背包满)
        /// </summary>
        AdTopScorerKeep = 20,
        /// <summary>
        /// 竞猜真实比赛获胜，返回点券给玩家
        /// </summary>
        GambleReturnToGambler = 21,
        /// <summary>
        /// 精彩活动奖励
        /// </summary>
        ActivityExPrize = 22,
        /// <summary>
        /// 竞猜真实比赛获胜，返回点券庄家
        /// </summary>
        GambleReturnToHost = 23,
        /// <summary>
        /// 强9送母卡
        /// </summary>
        Strength9PrizeMaster = 24,
        /// <summary>
        /// 装备合一送一
        /// </summary>
        EquipmentSynthesisGift = 25,
        /// <summary>
        /// 竞技场排名奖励
        /// </summary>
        Arena = 26,
        /// <summary>
        /// 竞技场战败通知
        /// </summary>
        ArenaFailure = 27,

        /// <summary>
        /// 充值返球票
        /// </summary>
        ChargeReturnDouble = 30,
        /// <summary>
        /// 九宫格排名奖励
        /// </summary>
        SudokuAwary = 28,
        /// <summary>
        /// 球星启示录过关奖励
        /// </summary>
        RevelationAwary = 29,
        /// <summary>
        /// 豪门试炼通关奖励
        /// </summary>
        GiantsAwary = 31,
        /// <summary>
        /// 群雄逐鹿击杀奖励
        /// </summary>
        CrowdKill = 32,
        /// <summary>
        /// 群雄逐鹿排名奖励
        /// </summary>
        CrowdRank = 33,
        /// <summary>
        /// 公会战单轮胜负奖励
        /// </summary>
        GWarRoundPrize = 34,
        /// <summary>
        /// 公会战公会排名奖励
        /// </summary>
        GWarGuildRankPrize = 35,
        /// <summary>
        /// 公会战经理排名奖励
        /// </summary>
        GWarManagerRankPrize = 36,
        /// <summary>
        /// 跨服群雄逐鹿击杀奖励
        /// </summary>
        CrossCrowdKill = 37,
        /// <summary>
        /// 跨服群雄逐鹿排名奖励
        /// </summary>
        CrossCrowdRank = 38,
        /// <summary>
        /// 跨服天梯赛季奖励
        /// </summary>
        CrossLadderPrize = 39,
        /// <summary>
        /// 跨服天梯每日奖励
        /// </summary>
        CrossLadderDailyPrize = 40,
        /// <summary>
        /// 公会战会长分配奖励
        /// </summary>
        GWarGuildPresentPrize = 41,
        /// <summary>
        /// 充值100返50
        /// </summary>
        ChargeReturn50 = 42,
        /// <summary>
        /// 活跃度未领取奖励
        /// </summary>
        Active = 43,
        /// <summary>
        /// 点球季度奖励
        /// </summary>
        AdSpores = 44,
        /// <summary>
        /// 星座补偿
        /// </summary>
        Constellation = 45,
        /// <summary>
        /// 巅峰之战击杀奖励
        /// </summary>
        PeakKillPrize = 46,
        /// <summary>
        /// 巅峰之战排名奖励
        /// </summary>
        PeakRankPrize = 47,
        /// <summary>
        /// 巅峰之战绝杀奖励
        /// </summary>
        PeakBossPrize = 48,
        /// <summary>
        /// 巅峰之战世界奖励
        /// </summary>
        PeakWoldPrize = 49,
        /// <summary>
        /// 九宫格排名奖励
        /// </summary>
        SudokuAwaryNotItem = 50,
        /// <summary>
        /// 巅峰之战挑战NPC获胜奖励
        /// </summary>
        PeakChallengeNpc = 51,
        /// <summary>
        /// 租借球员
        /// </summary>
        HirePlayer = 52,
        /// <summary>
        /// 租借球员装备返回
        /// </summary>
        HirePlayerEquip = 53,
        /// <summary>
        /// 冠军杯比赛获胜奖励
        /// </summary>
        ChampionWinPrize = 54,
        /// <summary>
        /// 冠军杯排名奖励
        /// </summary>
        ChampionRankPrize = 55,
        /// <summary>
        /// 月卡奖励
        /// </summary>
        MonthCard = 56,
        /// <summary>
        /// 月卡奖励
        /// </summary>
        MonthCard1 = 57,
        /// <summary>
        /// 充值成功
        /// </summary>
        ChargeSuccess = 58,

        /// <summary>
        /// 分享成功
        /// </summary>
        Share = 59,

        /// <summary>
        /// 封测返点
        /// </summary>
        PackagingRebate = 60,
        /// <summary>
        /// 礼包购买成功
        /// </summary>
        GiftBagSuccess = 61,
        /// <summary>
        /// 欧洲杯
        /// </summary>
        Europe =62,
        /// <summary>
        /// 邀请
        /// </summary>
        Invite=63,
        /// <summary>
        /// 群雄击杀最多
        /// </summary>
        CrowdMaxKiller=64,
        /// <summary>
        /// 拍卖成功
        /// </summary>
        TransferSuccess=66,

        /// <summary>
        /// 购买成功
        /// </summary>
        TransferBuySuccess=67,
        /// <summary>
        /// 拍卖流标
        /// </summary>
        TransferRunOff=68,
        /// <summary>
        /// 下架成功
        /// </summary>
        TransferSoldOut=69,
        /// <summary>
        /// 球星启示录通关奖励
        /// </summary>
        RevelationPassAwary=70,
        /// <summary>
        /// 后台发送
        /// </summary>
        AdminSend = 100,


    }

    /// <summary>
    /// 弹出消息枚举
    /// </summary>
    public enum EnumPopType
    {
        /// <summary>
        /// 新邮件
        /// </summary>
        NewMail = 1,
        /// <summary>
        /// 添加好友
        /// </summary>
        AddFriend = 2,
        /// <summary>
        /// 拍卖成功
        /// </summary>
        AuctionSaleSuccess = 3,
        /// <summary>
        /// 竞拍被超过
        /// </summary>
        AuctionOver = 4,
        /// <summary>
        /// 任务完成
        /// </summary>
        TaskFinish = 5,
        /// <summary>
        /// 任务进度
        /// </summary>
        TaskProgress = 6,
        /// <summary>
        /// 更新经理信息
        /// </summary>
        UpdateManagerInfo = 7,
        /// <summary>
        /// 添加好友弹出消息
        /// </summary>
        InformationPopAddFriend = 8,
        /// <summary>
        /// 开放功能
        /// </summary>
        OpenFunction = 9,
        /// <summary>
        /// 激活任务
        /// </summary>
        OpenTask = 10,
        /// <summary>
        /// 更新kpi
        /// </summary>
        UpdateKpi = 11,
        /// <summary>
        /// 世界杯竞猜更新奖池
        /// </summary>
        GamblePrizePool = 13,
        /// <summary>
        /// 被pk通知
        /// </summary>
        ByPkPop = 14,
        /// <summary>
        /// 被复仇通知
        /// </summary>
        ByRevengePop = 15,

        /// <summary>
        /// 绑定点券
        /// </summary>
        BindPoint = 17,
        /// <summary>
        /// 群雄消息
        /// </summary>
        CrowdPop = 1000,
        /// <summary>
        /// 群雄-比赛胜
        /// </summary>
        CrowdMatchWin = 1001,
        /// <summary>
        /// 群雄-比赛平
        /// </summary>
        CrowdMatchDraw = 1002,
        /// <summary>
        /// 群雄-比赛负
        /// </summary>
        CrowdMatchLose = 1003,
        /// <summary>
        /// 群雄-击杀对方
        /// </summary>
        CrowdKill = 1004,
        /// <summary>
        /// 群雄-被击杀
        /// </summary>
        CrowdByKill = 1005,
        /// <summary>
        /// 群雄-奖励显示
        /// </summary>
        CrowdMatchPrize = 1006,
        /// <summary>
        /// 群雄-士气提升
        /// </summary>
        CrowdMoraleUp = 1007,
        /// <summary>
        /// 群雄-士气下降
        /// </summary>
        CrowdMoraleDown = 1008,
        /// <summary>
        /// 群雄-同归于尽
        /// </summary>
        CrowdKillTogether = 1009,
        /// <summary>
        /// 群雄-配对成功
        /// </summary>
        CrowdPair = 1010,

        /// <summary>
        /// 群雄消息
        /// </summary>
        CrossCrowdPop = 2000,
        /// <summary>
        /// 群雄-比赛胜
        /// </summary>
        CrossCrowdMatchWin = 2001,
        /// <summary>
        /// 群雄-比赛平
        /// </summary>
        CrossCrowdMatchDraw = 2002,
        /// <summary>
        /// 群雄-比赛负
        /// </summary>
        CrossCrowdMatchLose = 2003,
        /// <summary>
        /// 群雄-击杀对方
        /// </summary>
        CrossCrowdKill = 2004,
        /// <summary>
        /// 群雄-被击杀
        /// </summary>
        CrossCrowdByKill = 2005,
        /// <summary>
        /// 群雄-奖励显示
        /// </summary>
        CrossCrowdMatchPrize = 2006,
        /// <summary>
        /// 群雄-士气提升
        /// </summary>
        CrossCrowdMoraleUp = 2007,
        /// <summary>
        /// 群雄-士气下降
        /// </summary>
        CrossCrowdMoraleDown = 2008,
        /// <summary>
        /// 群雄-同归于尽
        /// </summary>
        CrossCrowdKillTogether = 2009,
        /// <summary>
        /// 群雄-配对成功
        /// </summary>
        CrossCrowdPair = 2010,


        /// <summary>
        /// 跨服巅峰之战消息
        /// </summary>
        CrossPeak = 2100,
        /// <summary>
        /// 巅峰之战挑战NPC
        /// </summary>
        CrossPeakPve = 2101,
        /// <summary>
        /// 巅峰之战被挑战打输了
        /// </summary>
        CrossPeakByChallengeLose = 2102,
        /// <summary>
        /// 巅峰之战被挑战打赢或打平
        /// </summary>
        CrossPeakByChallengeWin = 2103,
        /// <summary>
        /// 巅峰之战挑战他人打输了
        /// </summary>
        CrossPeakChallengeLose = 2104,
        /// <summary>
        /// 巅峰之战挑战他人打赢或打平
        /// </summary>
        CrossPeakChallengeWin = 2105,
        /// <summary>
        /// 巅峰之战击杀
        /// </summary>
        CrossPeakKill = 2106,
        /// <summary>
        /// 巅峰之战被击杀
        /// </summary>
        CrossPeakByKill = 2107,
        /// <summary>
        /// 巅峰之战玉石俱焚
        /// </summary>
        CrossPeakCrashAndBurn = 2108,
        /// <summary>
        /// 巅峰之战绝杀奖励
        /// </summary>
        CrossPeakFinalHit = 2109,

        /// <summary>
        /// 巅峰之战消息
        /// </summary>
        Peak = 1100,
        /// <summary>
        /// 巅峰之战挑战NPC
        /// </summary>
        PeakPve = 1101,
        /// <summary>
        /// 巅峰之战被挑战打输了
        /// </summary>
        PeakByChallengeLose = 1102,
        /// <summary>
        /// 巅峰之战被挑战打赢或打平
        /// </summary>
        PeakByChallengeWin = 1103,
        /// <summary>
        /// 巅峰之战挑战他人打输了
        /// </summary>
        PeakChallengeLose = 1104,
        /// <summary>
        /// 巅峰之战挑战他人打赢或打平
        /// </summary>
        PeakChallengeWin = 1105,
        /// <summary>
        /// 巅峰之战击杀
        /// </summary>
        PeakKill = 1106,
        /// <summary>
        /// 巅峰之战被击杀
        /// </summary>
        PeakByKill = 1107,
        /// <summary>
        /// 巅峰之战玉石俱焚
        /// </summary>
        PeakCrashAndBurn = 1108,
        /// <summary>
        /// 巅峰之战绝杀奖励
        /// </summary>
        PeakFinalHit = 1109,

        /// <summary>
        /// 邀请入会-接口返回
        /// </summary>
        GuildInvitePop = 92,
        /// <summary>
        /// 邀请入会-聊天弹出
        /// </summary>
        GuildInviteChatPop = 93,
        /// <summary>
        /// 开放幸福五选一
        /// </summary>
        OpenLevelGift = 101,
    }

    public enum EnumBannerType
    {
        /// <summary>
        /// 球探
        /// </summary>
        Scouting = 1,
        /// <summary>
        /// 合成
        /// </summary>
        Synthesis = 2,
        /// <summary>
        /// 学习技能
        /// </summary>
        SkillAsk = 3,
        /// <summary>
        /// 强化
        /// </summary>
        Strengthen = 4,

        ArenaWinning5 = 5,

        ArenaWinning10 = 6,

        ArenaWinning30 = 7,

        ArenaRanking = 8,

        RemindScouting = 101,

        RemindGuidePrize = 102,

        RemindSynthesis = 103,

        RemindWCHPrize = 104,

        RemindStrength = 105,

        RemindLevelGift = 106,

        RemindVipGift = 107,

        RemindAuctionSale = 108,

        RemindGrow = 109,

        RemindGoldmall = 110,

        RemindDaily = 111,

        RemindRevelation = 112,

        /// <summary>
        /// 群雄消息
        /// </summary>
        CrowdBanner = 1000,
        /// <summary>
        /// 群雄-开启
        /// </summary>
        CrowdOpen = 1001,
        /// <summary>
        /// 群雄-击杀
        /// </summary>
        CrowdKill = 1002,
        /// <summary>
        /// 群雄-5连胜
        /// </summary>
        Crowd5Win = 1003,
        /// <summary>
        /// 群雄-3连胜
        /// </summary>
        Crowd3Win = 1004,
        /// <summary>
        /// 群雄-完胜
        /// </summary>
        CrowdWinOver = 1005,
        /// <summary>
        /// 群雄-活动结束
        /// </summary>
        CrowdEnd = 1006,

        /// <summary>
        /// 群雄消息
        /// </summary>
        CrossCrowdBanner = 2000,
        /// <summary>
        /// 群雄-开启
        /// </summary>
        CrossCrowdOpen = 2001,
        /// <summary>
        /// 群雄-击杀
        /// </summary>
        CrossCrowdKill = 2002,
        /// <summary>
        /// 群雄-5连胜
        /// </summary>
        CrossCrowd5Win = 2003,
        /// <summary>
        /// 群雄-3连胜
        /// </summary>
        CrossCrowd3Win = 2004,
        /// <summary>
        /// 群雄-完胜
        /// </summary>
        CrossCrowdWinOver = 2005,
        /// <summary>
        /// 群雄-活动结束
        /// </summary>
        CrossCrowdEnd = 2006,

        /// <summary>
        /// 跨服巅峰之战消息
        /// </summary>
        CrossPeak = 2100,
        /// <summary>
        /// 巅峰之战开启
        /// </summary>
        CrossPeakStart = 2101,
        /// <summary>
        /// 巅峰之战连续杀人
        /// </summary>
        CrossPeakJoinKill = 2102,
        /// <summary>
        /// 巅峰之战世界奖励
        /// </summary>
        CrossPeakWoldPrize = 2103,
        /// <summary>
        /// 巅峰之战绝杀奖励
        /// </summary>
        CrossPeakFinalHit = 2104,
        /// <summary>
        /// 巅峰之战活动结束
        /// </summary>
        CrossPeakEnd = 2105,
        /// <summary>
        /// 跨服巅峰之战推送消息
        /// </summary>
        CrossPeakManagerInfo = 2106,

        /// <summary>
        /// 巅峰之战消息
        /// </summary>
        Peak = 1100,
        /// <summary>
        /// 巅峰之战开启
        /// </summary>
        PeakStart = 1101,
        /// <summary>
        /// 巅峰之战连续杀人
        /// </summary>
        PeakJoinKill = 1102,
        /// <summary>
        /// 巅峰之战世界奖励
        /// </summary>
        PeakWoldPrize = 1103,
        /// <summary>
        /// 巅峰之战绝杀奖励
        /// </summary>
        PeakFinalHit = 1104,
        /// <summary>
        /// 巅峰之战活动结束
        /// </summary>
        PeakEnd = 1105,
        /// <summary>
        /// 巅峰之战推送消息
        /// </summary>
        PeakManagerInfo = 1106,

        /// <summary>
        /// 球员租借市场开启
        /// </summary>
        HireAuctionOpen = 1107,
        /// <summary>
        /// 球员租借市场关闭
        /// </summary>
        HireAuctionClose = 1108,
    }


}
