using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Shadow
{
    /// <summary>
    /// 事务类型
    /// </summary>
    public enum EnumTransactionType
    {
        /// <summary>
        /// 巡回赛奖励
        /// </summary>
        TourPrize = 1,
        /// <summary>
        /// 精英巡回赛奖励
        /// </summary>
        ElitPrize = 2,
        /// <summary>
        /// 卡牌转化为球员
        /// </summary>
        TeammemberTrans = 3,
        /// <summary>
        /// 解雇球员
        /// </summary>
        TeammemberFire = 4,
        /// <summary>
        /// 卡牌强化
        /// </summary>
        CardStrength = 5,
        /// <summary>
        /// 卡牌合成
        /// </summary>
        CardSynthesize = 6,
        /// <summary>
        /// 卡牌分解
        /// </summary>
        CardDecompound = 7,
        /// <summary>
        /// 商城购买道具
        /// </summary>
        MallBuyItem = 8,
        /// <summary>
        /// 使用道具
        /// </summary>
        ItemUsed = 9,
        /// <summary>
        /// 删除道具
        /// </summary>
        ItemDelete = 10,
        /// <summary>
        /// 杯赛竞猜
        /// </summary>
        DailycupGamble = 11,
        /// <summary>
        /// 杯赛竞猜开注
        /// </summary>
        DailycupGambleOpen = 12,
        /// <summary>
        /// 联赛竞猜
        /// </summary>
        LeagueGamble = 13,
        /// <summary>
        /// 联赛竞猜开注
        /// </summary>
        LeagueGambleOpen = 14,
        /// <summary>
        /// 荣誉兑换元老卡牌
        /// </summary>
        CardExchangewithHonor = 15,
        /// <summary>
        /// 球探抽卡
        /// </summary>
        ScoutingLottery = 16,
        /// <summary>
        /// 设置球员装备
        /// </summary>
        TeammemberSetEquip = 17,
        /// <summary>
        /// 更换球员装备
        /// </summary>
        TeammemberReplaceEquip = 18,
        /// <summary>
        /// 卸掉球员装备
        /// </summary>
        TeammemberRemoveEquip = 19,
        /// <summary>
        /// 收集意志卡片
        /// </summary>
        WillCollectCard = 20,
        /// <summary>
        /// 装备合成
        /// </summary>
        EquipmentSynthesize = 21,
        /// <summary>
        /// 任务奖励
        /// </summary>
        TaskPrize = 22,
        /// <summary>
        /// 邮箱附件,天梯赛奖励
        /// </summary>
        MailLadderPrize = 23,
        /// <summary>
        /// 邮箱附件--联赛奖励
        /// </summary>
        MailLeaguePrize = 24,
        /// <summary>
        /// 邮箱附件--拍卖成功
        /// </summary>
        MailAuctionSaleSuccess = 25,
        /// <summary>
        /// 邮箱附件--竞拍成功
        /// </summary>
        MailAuctionBuySuccess = 26,
        /// <summary>
        /// 邮箱附件--巡回赛
        /// </summary>
        MailTourFight = 27,
        /// <summary>
        /// 邮箱附件--竞技场排名奖励
        /// </summary>
        MailArenaPrize = 28,
        /// <summary>
        /// 邮箱附件--世界挑战赛排名奖励
        /// </summary>
        MailWorldChalengePrize = 29,
        /// <summary>
        /// 邮箱附件--拍卖失败
        /// </summary>
        MailAuctionSaleFail = 30,
        /// <summary>
        /// 邮箱附件--球探抽卡
        /// </summary>
        MailScoutingLottery = 31,
        /// <summary>
        /// 拍卖行拍卖物品
        /// </summary>
        AuctionSellItem = 32,
        /// <summary>
        /// 拍卖行竞标物品
        /// </summary>
        AuctionBuyItem = 33,
        /// <summary>
        /// 镶嵌球魂
        /// </summary>
        MosaicBallsoul = 34,
        /// <summary>
        /// 装备洗炼
        /// </summary>
        EquipmentWash = 35,
        /// <summary>
        /// 拆分物品
        /// </summary>
        SplitItem = 36,
        /// <summary>
        /// 背包整理
        /// </summary>
        Arrange = 37,
        /// <summary>
        /// 移除镶嵌球魂
        /// </summary>
        MosaicRemove = 38,
        /// <summary>
        /// 世界挑战赛关卡奖励
        /// </summary>
        WorldChalengeStagePrize = 39,
        /// <summary>
        /// 巡回赛挂机奖励
        /// </summary>
        TourHookPrize = 40,
        /// <summary>
        /// 球员成长突破
        /// </summary>
        TeammemberGrowUp = 41,
        /// <summary>
        /// 天梯荣誉兑换
        /// </summary>
        LadderExchange = 42,
        /// <summary>
        /// 邮件-世界挑战赛关卡奖励
        /// </summary>
        MailWorldChalengeStagePrize = 43,
        /// <summary>
        /// 邮件-巡回赛挂机奖励
        /// </summary>
        MailTourHookPrize = 44,
        /// <summary>
        /// 邮件-拍卖取消
        /// </summary>
        MailAuctionCancel = 45,
        /// <summary>
        /// 邮件-精英巡回赛奖励(背包满)
        /// </summary>
        MailTourElitePrize = 46,
        /// <summary>
        /// 邮件-巡回赛奖励(背包满)
        /// </summary>
        MailTourPrize = 47,
        /// <summary>
        /// 活动奖励
        /// </summary>
        ActivityPrize = 48,
        /// <summary>
        /// 媒体礼包兑换
        /// </summary>
        ExchangeCodePrize = 49,
        /// <summary>
        /// 邮件-后台发送道具
        /// </summary>
        MailAdminAddItem = 50,
        /// <summary>
        /// 世界杯点球活动领取奖励
        /// </summary>
        AdTopScorerKeep = 51,
        /// <summary>
        /// 世界杯点球活动兑换门票
        /// </summary>
        AdTopScorerExch = 52,
        /// <summary>
        /// 邮件-点球活动领取奖励(背包满)
        /// </summary>
        MailTopScoreKeep = 53,
        /// <summary>
        /// 精彩活动奖励
        /// </summary>
        ActivityExPrize = 54,
        /// <summary>
        /// PK赛奖励
        /// </summary>
        PlayerKillPrize = 55,
        /// <summary>
        /// 邮件-精彩活动奖励
        /// </summary>
        MailActivityExPrize = 56,
        /// <summary>
        /// 完成新手引导奖励
        /// </summary>
        GuidePrize = 57,
        /// <summary>
        /// 巡回赛通关奖励
        /// </summary>
        TourPassPrize = 58,
        /// <summary>
        /// 图纸分解
        /// </summary>
        SuitdrawingDecompose = 59,
        /// <summary>
        /// 技能经验卡升级
        /// </summary>
        MixSkillExpCard = 60,
        /// <summary>
        /// 修改logo
        /// </summary>
        UpdateLogo = 61,
        /// <summary>
        /// 强化送母卡
        /// </summary>
        Strength9PrizeMaster = 62,
        /// <summary>
        /// 装备合一送一
        /// </summary>
        EquipmentSynthesisGift = 63,
        /// <summary>
        /// 洗炼石合成
        /// </summary>
        WashstoneSynthesis = 64,
        /// <summary>
        /// 传奇精魄合成
        /// </summary>
        LegendSynthesis = 65,
        /// <summary>
        /// 国庆翻牌
        /// </summary>
        NationDayClick = 66,
        /// <summary>
        /// 国庆翻牌Bonus
        /// </summary>
        NationDayBonus = 67,
        /// <summary>
        /// 金币商城购买
        /// </summary>
        GoldmallBuy=68,
        /// <summary>
        /// 邮件-群雄击杀奖励
        /// </summary>
        MailCrowdKillPrize=69,
        /// <summary>
        /// 邮件-群雄排名奖励
        /// </summary>
        MailCrowdRankPrize = 70,
        /// <summary>
        /// 老玩家回馈礼包
        /// </summary>
        RecallPrize=71,
        /// <summary>
        /// 邮件-跨服群雄击杀奖励
        /// </summary>
        MailCrossCrowdKillPrize = 72,
        /// <summary>
        /// 邮件-跨服群雄排名奖励
        /// </summary>
        MailCrossCrowdRankPrize = 73,
        /// <summary>
        /// 邮件-跨服天梯赛季奖励
        /// </summary>
        MailCrossLadderPrize = 74,
        /// <summary>
        /// 邮件-跨服天梯每日奖励
        /// </summary>
        MailCrossLadderDailyPrize = 75,
        /// <summary>
        /// 跨服天梯荣誉兑换
        /// </summary>
        CrossLadderExchange = 76,
        /// <summary>
        /// 公会福利
        /// </summary>
        GuildReward = 91,
        /// <summary>
        ///公会商城购买
        /// </summary>
        GuildShopBuy = 92,
        /// <summary>
        /// 公会战单轮胜负奖励
        /// </summary>
        GWarRoundPrize = 93,
        /// <summary>
        /// 公会战公会排名奖励
        /// </summary>
        GWarGuildRankPrize = 94,
        /// <summary>
        /// 公会战经理排名奖励
        /// </summary>
        GWarManagerRankPrize = 95,
        /// <summary>
        /// 公会战会长分配奖励
        /// </summary>
        GWarGuildPresentPrize=96,
        /// <summary>
        /// 后台添加道具
        /// </summary>
        AdminAddItem = 101,
        /// <summary>
        /// 后台删除道具
        /// </summary>
        AdminDeleteItem = 102,
        /// <summary>
        /// 竞技场连胜奖励道具
        /// </summary>
        ArenaAward = 103,
        /// <summary>
        /// 九宫格单局游戏奖励
        /// </summary>
        SudokuAward = 104,
        /// <summary>
        /// 教练技能升级
        /// </summary>
        CoachGrouoUpgrade = 105,
        /// <summary>
        /// 竞技场排名奖励
        /// </summary>
        ArenaRankingAward = 106,
        /// <summary>
        /// 九宫格排名奖励
        /// </summary>
        SudokuRankingAward = 107,
        /// <summary>
        /// 球星启示录过关奖励
        /// </summary>
        RevelationClearanceAward = 108,
        /// <summary>
        /// 豪门试炼通关奖励
        /// </summary>
        GiantsAwary = 109,

        /// <summary>
        /// 卡牌复制
        /// </summary>
        CopyCard = 110,
        /// <summary>
        /// 设置球员徽章
        /// </summary>
        TeammenberSetBadge =111,
            /// <summary>
            /// 更换球员徽章
            /// </summary>
        TeammemberReplaceBadge = 112,
        /// <summary>
        /// 卸掉球员徽章
        /// </summary>
        TeammemberRemoveBadge = 113,

        /// <summary>
        /// 装备进阶
        /// </summary>
        EquipmentUpgrade = 114,

        /// <summary>
        /// 装备精铸
        /// </summary>
        EquipmentPrecisionCasting = 115,

        /// <summary>
        /// 圣诞活动
        /// </summary>
        Christmas = 116,

        /// <summary>
        /// 球员觉醒
        /// </summary>
        Arousal = 117,
        /// <summary>
        /// 合同页合成
        /// </summary>
        TheContractSynthesis = 118,

        /// <summary>
        /// 精英巡回赛挂机
        /// </summary>
        TourEliteHook = 119,

        /// <summary>
        /// 黄道十二宫
        /// </summary>
        Constellation = 120,
        /// <summary>
        /// 活跃度
        /// </summary>
        Active= 121,
        /// <summary>
        /// 后台解锁道具
        /// </summary>
        AdminUnlockItem = 122,
        /// <summary>
        /// 星座合成
        /// </summary>
        ConstellationSynthesis = 123,

        /// <summary>
        /// 金券商城
        /// </summary>
        CouponBuy = 124,
        /// <summary>
        /// 好友邀请
        /// </summary>
        FriendInvite = 125,
        /// <summary>
        /// 点球季度排名奖励
        /// </summary>
        AdSports = 126,
        /// <summary>
        /// 世界杯
        /// </summary>
        WorldCup = 127,
        /// <summary>
        /// 经典球衣
        /// </summary>
        ClubClothes = 128,
        /// <summary>
        /// 巅峰之战击杀奖励
        /// </summary>
        PeakKillPrize = 129,
        /// <summary>
        /// 巅峰之战排名奖励
        /// </summary>
        PeakRankPrize = 130,
        /// <summary>
        /// 巅峰之战绝杀奖励
        /// </summary>
        PeakBossPrize = 131,
        /// <summary>
        /// 巅峰之战世界奖励
        /// </summary>
        PeakWoldPrize = 132,

        /// <summary>
        /// 球员兑换勋章
        /// </summary>
        ExchangeMedal = 133,

        /// <summary>
        /// 勋章兑换合同页
        /// </summary>
        MeDalToContract = 134,
        /// <summary>
        /// 
        /// </summary>
        MallBuyBindItem = 135,

        /// <summary>
        /// 经典球衣强化
        /// </summary>
        ClubClothesStrengthen = 136,
        /// <summary>
        /// 卸掉经典球衣
        /// </summary>
        TeammemberRemoveClubClothes = 137,
        /// <summary>
        /// 巅峰之战挑战NPC奖励
        /// </summary>
        PeakChallengeNpc=138,
        /// <summary>
        /// 租借球员
        /// </summary>
        HirePlayer = 139,

        /// <summary>
        /// 后台解锁道具
        /// </summary>
        AdminUnlockBindItem = 140,
        /// <summary>
        /// 邮件-租借球员
        /// </summary>
        MailHirePlayer = 52,
        /// <summary>
        /// 邮件-租借球员装备返回
        /// </summary>
        MailHirePlayerEquip = 53,
        /// <summary>
        /// 冠军杯押注
        /// </summary>
        ChampionQuiz= 141,
        /// <summary>
        /// 冠军杯比赛获胜奖励
        /// </summary>
        ChampionWinPrize=142,
        /// <summary>
        /// 冠军杯排名奖励
        /// </summary>
        ChampionRankPrize = 143,
        /// <summary>
        /// 解雇球员卡
        /// </summary>
        FireTeamMember=144,
        /// <summary>
        /// 装备出售
        /// </summary>
        EquipmentSell = 145,

        /// <summary>
        /// 联赛奖励
        /// </summary>
        LeaguePrize = 146,

        /// <summary>
        /// 球员管理初始化
        /// </summary>
        TeammemberInit=147,
        /// <summary>
        /// 球员训练
        /// </summary>
        PlayreTrain=148,

        /// <summary>
        /// 联赛积分兑换
        /// </summary>
        LeagueExchange = 149,

        /// <summary>
        /// 邮件-杯赛奖励
        /// </summary>
        MailDailycupPrize = 150,
        /// <summary>
        /// 更新成就数据
        /// </summary>
        AchievementUpdate = 151,
        /// <summary>
        /// 球员训练开宝箱
        /// </summary>
        OpenBoxPrize=152,
        /// <summary>
        /// 购买VIP礼包
        /// </summary>
        BuyVipPackage=153,
        /// <summary>
        /// 月卡奖励
        /// </summary>
        MonthCard = 154,
        /// <summary>
        /// 分享
        /// </summary>
        Share = 155,
        /// <summary>
        /// 封测返点
        /// </summary>
        PackagingRebate=156,
        /// <summary>
        /// 欧洲杯竞猜
        /// </summary>
        EuropeConfig = 157,
        /// <summary>
        /// 友谊赛抽卡
        /// </summary>
        FriendShip=158,
        /// <summary>
        /// 转盘
        /// </summary>
        Turntable=159,
        /// <summary>
        /// 球员升星
        /// </summary>
        UpgradeTheStar = 160,
        /// <summary>
        /// 潜力重置
        /// </summary>
        PotentialReset =161,
        /// <summary>
        /// 奥运金牌兑换
        /// </summary>
        OlympocExChangerize =162,
        /// <summary>
        /// 竞技场商城兑换
        /// </summary>
        ArenaShop=163,
        /// <summary>
        /// 竞技场球员下阵
        /// </summary>
        ArenaGoOffStage =164,
        /// <summary>
        /// 邮件-跨服群雄最大击杀奖励
        /// </summary>
        MailCrossCrowdMaxKillPrize = 165,
        /// <summary>
        /// 转会市场
        /// </summary>
        Transfer = 166,
        /// <summary>
        /// 跨服活动
        /// </summary>
        CrossActivity=167,

        /// <summary>
        /// 球星启示录兑换奖励
        /// </summary>
        RevelationExChange = 168,


        /// <summary>
        /// 教练激活升星
        /// </summary>
        CoachActivation = 169,
        /// <summary>
        /// 充值
        /// </summary>
        Charge = 1000,
       

        
    }
}
