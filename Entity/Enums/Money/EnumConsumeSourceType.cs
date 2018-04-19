using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 点卷扣除类型
    /// </summary>
    public enum EnumConsumeSourceType
    {
        /// <summary>
        /// 商城消费
        /// </summary>
        Mall = 1,
        /// <summary>
        /// 联赛竞猜
        /// </summary>
        LeagueGamble = 2,
        /// <summary>
        /// 拍卖行
        /// </summary>
        Auction = 3,
        /// <summary>
        /// 竞猜真实比赛
        /// </summary>
        GambleTrueMatch = 4,
        /// <summary>
        /// 竞技场
        /// </summary>
        ArenaGamble = 5,
        /// <summary>
        /// 教练传承
        /// </summary>
        CoachInheritGamble = 6,
        /// <summary>
        /// 教练技能升阶
        /// </summary>
        CoachSkillGamble = 7,
        /// <summary>
        /// 九宫格补射扣点卷
        /// </summary>
        SudokuFollowShotsGamble = 8,
        /// <summary>
        /// 国庆翻牌活动
        /// </summary>
        NationalDayActivityReset = 9,

        /// <summary>
        /// 腾讯直购道具
        /// </summary>
        TxBuy = 10,
        /// <summary>
        /// 球星启示录扣点卷
        /// </summary>
        RevelationAddChallenge = 11,
        /// <summary>
        /// 重置球员属性
        /// </summary>
        ResetTeammemberProperty = 12,
        /// <summary>
        /// 公会战消耗
        /// </summary>
        GuildWarCost = 13,
        /// <summary>
        /// 豪门试炼楚通关CD
        /// </summary>
        Giants = 14,
        /// <summary>
        /// 群雄逐鹿清除cd
        /// </summary>
        CrowdClearCd = 15,
        /// <summary>
        /// 群雄逐鹿复活
        /// </summary>
        CrowdResurrection = 16,
        /// <summary>
        /// 圣诞转盘扣点卷
        /// </summary>
        Rotarytable = 17,

        /// <summary>
        /// 黄道十二宫开箱扣点卷
        /// </summary>
        Constellation = 18,

        /// <summary>
        /// 装备精铸点券消耗
        /// </summary>
        EquipmentPrecisionCasting = 20,
        /// <summary>
        /// 购买跨服天梯场次
        /// </summary>
        CrossLadderStamina = 21,
        /// <summary>
        /// 刷新金币商城
        /// </summary>
        RefreshGoldmall = 22,
        /// <summary>
        /// 巅峰之战复活
        /// </summary>
        CrossPeakResurrection = 23,
        /// <summary>
        /// 巅峰之战清CD
        /// </summary>
        CrossPeakCd = 24,
        /// <summary>
        /// 巅峰之战鼓舞扣点卷
        /// </summary>
        CrossPeakInspire = 25,

        /// <summary>
        /// 金币商城点券购买
        /// </summary>
        GoldMallBuy = 26,
        /// <summary>
        /// 装备进阶保护
        /// </summary>
        EquipmentUpgradeProtected = 27,
        /// <summary>
        /// 四级教练技能学习
        /// </summary>
        SkillAsk = 59001,
        /// <summary>
        /// 刷新天梯可兑换元老
        /// </summary>
        RefreshLadderExchange = 28,
        /// <summary>
        /// 强化保护
        /// </summary>
        StrengthProtect = 29,
        /// <summary>
        /// 金券购买
        /// </summary>
        CouponBuy = 30,

        //腾讯金券
        TxCouponBuy = 31,
        /// <summary>
        /// 黄道十二宫重置关卡
        /// </summary>
        ConstellationReset = 32,
        /// <summary>
        /// 球探点券刷新
        /// </summary>
        ScoutingRefresh = 33,
        /// <summary>
        /// 球探抽卡
        /// </summary>
        ScoutingLottery = 34,

        /// <summary>
        /// 理财投资
        /// </summary>
        InvestDeposit = 35,
        /// <summary>
        /// 经典球衣
        /// </summary>
        ClubClothes = 36,
        /// <summary>
        /// 租借球员
        /// </summary>
        HirePlayer = 37,
        /// <summary>
        /// 联赛重置
        /// </summary>
        LeagueReset = 38,
        /// <summary>
        /// 联赛重赛
        /// </summary>
        LeagueRematch = 39,
        /// <summary>
        /// 刷新联赛积分商城
        /// </summary>
        RefreshLeagueExchange = 40,
        /// <summary>
        /// 重置天赋
        /// </summary>
        ResetTree= 41,
        /// <summary>
        /// 天梯赛清除CD
        /// </summary>
        LadderClearCD = 101,

        /// <summary>
        /// 购买vip礼包
        /// </summary>
        BuyVipPackage = 102,
        /// <summary>
        /// 转盘
        /// </summary>
        Turntable = 103,
        /// <summary>
        /// 重置潜力
        /// </summary>
        ResetPotential=104,
        /// <summary>
        /// 竞技场重置商城
        /// </summary>
        ArenaGambleResetShop=105,

        /// <summary>
        /// 点球决战重置兑换列表
        /// </summary>
        AdTopScoreResetExchange = 106,
        /// <summary>
        /// 球星启示录刷新商城
        /// </summary>
        RevelateionShopRefresh = 107,
    }
}
