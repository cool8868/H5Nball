using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Activity
{
    public enum EnumActivityExEffectType
    {
        MatchExp = 1,

        MatchCoin = 2,

        MatchItemCount = 3,

        SkillAskCoin = 4,

        QuickTrainRate = 5,

        MallDiscounts = 6,

        ScoutingDiscounts = 7,

        StrengthRate = 8,

        StrengthProtect = 9,

        SynthesisProtectDiscount = 12,

        AddDailycupGambleCount = 14,

        StrengthWildCardProtect = 15,

        TrainExp = 16,

        ScoutingCoinGift = 17,

        ScoutingPointGift = 18,

        TourGift = 19,

        Strength9PrizeMaster = 20,

        SynthesisRateLow50 = 21,

        EquipmentSynthesisGift = 22,

        ChargeReturnDouble = 23,

        SudoVigorDouble = 24,

        DecomposePlus = 25,
        TrainExpDouble = 26,

        RevelationDouble = 27,

        WorldChallenge = 28,

        ChristmasActivity = 29,

        EquipmentPrecisionCasting = 30,

        ScoutingCoinGift2 = 31,

        ScoutingPointGift2 = 32,

        /// <summary>
        /// 充值100返50
        /// </summary>
        ChargeReturn50 = 33,

        ScoutingBlackGold = 34,

        /// <summary>
        /// 球探抽1送1
        /// </summary>
        ScoutingCardFeed = 35,

        /// <summary>
        /// 强化送合同页
        /// </summary>
        Strengthening = 36,

        /// <summary>
        /// 球探点卷抽合同页
        /// </summary>
        ScoutingPointTheContract1 = 37,
        ScoutingPointTheContract2 = 38,
        GoldMallDiscounts = 39,

        /// <summary>
        /// 抽卡概率提升
        /// </summary>
        ScoutingRate = 40,

        /// <summary>
        /// 合同页合金卡合一送一
        /// </summary>
        GoldenSynthesisGift = 41,

        /// <summary>
        /// 黄道十二宫双倍星辰元素
        /// </summary>
        Constellation = 42,

        ScoutingDiscounts1 = 43,

        /// <summary>
        /// 抽卡概率提升
        /// </summary>
        ScoutingRate1 = 44,

        /// <summary>
        /// 新版球探抽合同页抽1送1
        /// </summary>
        ScoutingTheContractFeed = 45,

        /// <summary>
        /// 装备精铸橙属性概率翻倍
        /// </summary>
        EquipmentPrecisionCasting1 = 46,

        /// <summary>
        /// 装备点卷精铸5折
        /// </summary>
        EquipmentPrecisionCasting2 = 47,

        /// <summary>
        /// 新版球探抽卡送勋章
        /// </summary>
        ScoutingMedal = 48,

        /// <summary>
        /// 新版球探金币抽卡
        /// </summary>
        ScoutingNewCoin = 49,

        /// <summary>
        /// 新版球探点卷抽卡
        /// </summary>
        ScutingNewPoint = 50,

        /// <summary>
        /// 新版球探抽黑金卡
        /// </summary>
        ScoutingNewBlackGold = 51,

        /// <summary>
        /// 球探抽合同页抽1送2
        /// </summary>
        ScoutingTheContractThreeTimes = 52,

        /// <summary>
        /// 装备强化不掉级
        /// </summary>
        EquipmentStreng = 53,

        /// <summary>
        /// 封印洗一送一
        /// </summary>
        SealDiscount = 54,

        /// <summary>
        /// 收集欧冠合同
        /// </summary>
        ScountingEuroDiscount = 55,

        /// <summary>
        /// 强化送欧冠合同页
        /// </summary>
        StrengtheningEuro = 56,

        /// <summary>
        /// 星座宝箱3倍
        /// </summary>
        ConstellationThreeTimes = 57,

        /// <summary>
        /// 迎新送贺卡（赠送球星贺卡）
        /// </summary>
        ScountingPlayerGiftCard = 58,

        /// <summary>
        /// 觉醒连级跳
        /// </summary>
        PlayerArousalUpTwo = 59,

        /// <summary>
        /// 点球大战送左脚的口袋
        /// </summary>
        TopScorePrize = 60,

        /// <summary>
        /// 球探抽到传奇或欧冠合同页可获得双份奖励
        /// </summary>
        ScountingTheContractLegendOrEuro = 61,

        /// <summary>
        /// 球员卡合一送一（传奇或者欧冠）
        /// </summary>
        SynthesisGiftCard = 62,

        // ----------------------------------------------------------------------------------------------------------------------------------------------
        // -----------------------------------------------------分割线  上面的为老数据----------------------------------------------------------
        // ----------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// 欧洲之星
        /// </summary>
        EuropeTheStars = 63,

        /// <summary>
        /// 欧洲杯狂欢
        /// </summary>
        EuropeCarnival = 64,

        /// <summary>
        /// 巨星集会。 抽卡S碎片送相同的碎片
        /// </summary>
        ScoutingDebris = 65,

        /// <summary>
        /// 暑期礼包抽卡
        /// </summary>
        ScoutingGiftBag = 66,

        /// <summary>
        /// 强化返百搭
        /// </summary>
        StrengthenReturnCard = 67,

        /// <summary>
        /// 合卡达人（送元老碎片宝箱）
        /// </summary>
        SynthesisTarento = 68,

        /// <summary>
        /// 升星双倍经验
        /// </summary>
        UpgradeTheStarDlouble = 69,

        /// <summary>
        /// 潜力免费锁定
        /// </summary>
        PotentialLock = 70,

        /// <summary>
        /// 巨星集会。 抽卡S碎片送相同的碎片 掉落S元老碎片
        /// </summary>
        ScoutingDebris1 = 71,

        /// <summary>
        /// 奥运金牌活动
        /// </summary>
        Olympic = 72,

        /// <summary>
        /// 充值
        /// </summary>
        Charge = 73,

        /// <summary>
        /// 潜力消耗钻石半价
        /// </summary>
        PotentialHalf = 74,

        /// <summary>
        /// 装备合一送一
        /// </summary>
        EquipmentSynthesis = 75,

        /// <summary>
        /// 商城买一送一
        /// </summary>
        MallBuyOneGetOne = 76,
        /// <summary>
        /// 友谊赛抽卡
        /// </summary>
        FriendMatchScouting = 77,
        /// <summary>
        /// 钻石抽卡
        /// </summary>
        PointScouting = 78,
        /// <summary>
        /// 金币抽卡
        /// </summary>
        CoinScouting = 79,
        /// <summary>
        /// 友情的抽卡
        /// </summary>
        FriendPointScouting = 80,
        /// <summary>
        /// 强9返卡
        /// </summary>
        Strengthen9ReturnCard=81,

        /// <summary>
        /// 鏖战欧罗巴
        /// </summary>
        EquipmentDebris = 82,
        /// <summary>
        /// 球探半价
        /// </summary>
        ScoutingHalfPrice=83,

        /// <summary>
        /// 金条抽卡
        /// </summary>
        GoldBarScouting = 84,
    }
}
