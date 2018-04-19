using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 商城道具效果类型
    /// </summary>
    public enum EnumMallEffectType
    {
        /// <summary>
        /// 保护膜
        /// </summary>
        ProteceItem=1001,
        /// <summary>
        /// 强化百搭卡
        /// </summary>
        PandoraJokerCard=1002,
        /// <summary>
        /// 合成保护卡
        /// </summary>
        ProtectSynthesis=1003,
        /// <summary>
        /// 装备合成卡
        /// </summary>
        SynthesisJokerCard=1004,
        /// <summary>
        /// 次级百搭卡
        /// </summary>
        PandoraJokerLowCard = 1005,

        /// <summary>
        /// 至尊百搭卡
        /// </summary>
        PandoraExtremeCard = 1006,

        /// <summary>
        /// 扩展背包
        /// </summary>
        ExpandPackage = 1,
        /// <summary>
        /// 重置精英巡回赛
        /// </summary>
        ResetElite = 2,
        /// <summary>
        /// 加速训练
        /// </summary>
        QuickenTrain = 3,
        /// <summary>
        /// 增加训练位
        /// </summary>
        AddTrainSeat = 4,
        /// <summary>
        /// 增加替补席
        /// </summary>
        AddSubstitute = 5,
        /// <summary>
        /// 成长大力丸
        /// </summary>
        GrowDaliwan = 6,
        /// <summary>
        /// 添加体力
        /// </summary>
        AddStamina = 7,
        /// <summary>
        /// 增加灵气
        /// </summary>
        AddReiki = 8,
        /// <summary>
        /// 增加金币
        /// </summary>
        AddCoin = 9,
        /// <summary>
        /// 球探
        /// </summary>
        Scouting = 10,
        /// <summary>
        /// 传奇卡强化保护
        /// </summary>
        StrengthProtectGold = 11,
        /// <summary>
        /// 橙卡强化保护
        /// </summary>
        StrengthProtectOrange = 12,
        /// <summary>
        /// 紫卡强化保护
        /// </summary>
        StrengthProtectPurple = 13,
        /// <summary>
        /// 蓝卡强化保护
        /// </summary>
        StrengthProtectBlue = 14,
        /// <summary>
        /// 绿卡强化保护
        /// </summary>
        StrengthProtectGreen = 15,
        /// <summary>
        /// 橙卡合成保护
        /// </summary>
        SynthesisProtectOrange = 16,
        /// <summary>
        /// 紫卡合成保护
        /// </summary>
        SynthesisProtectPurple = 17,
        /// <summary>
        /// 蓝卡合成保护
        /// </summary>
        SynthesisProtectBlue = 18,
        /// <summary>
        /// 绿卡合成保护
        /// </summary>
        SynthesisProtectGreen = 19,
        /// <summary>
        /// 强化百搭
        /// </summary>
        PandoraWildCard = 20,
        /// <summary>
        /// 精良装备合成保护
        /// </summary>
        EquipSynthesisProtect2 = 21,
        /// <summary>
        /// 优质装备合成保护
        /// </summary>
        EquipSynthesisProtect3 = 22,
        /// <summary>
        /// 普通装备合成保护
        /// </summary>
        EquipSynthesisProtect4 = 23,
        /// <summary>
        /// 劣质装备合成保护
        /// </summary>
        EquipSynthesisProtect5 = 24,
        /// <summary>
        /// 刷新任务
        /// </summary>
        RefreshTask = 25,
        /// <summary>
        /// 锁定洗炼属性
        /// </summary>
        EquipWashLockProperty = 26,
        /// <summary>
        /// 洗炼石
        /// </summary>
        EquipWashStone = 27,
        /// <summary>
        /// 洗炼融合剂
        /// </summary>
        EquipFusogen = 28,

        /// <summary>
        /// 报名世界挑战赛
        /// </summary>
        WorldChallengeAttend = 29,
        /// <summary>
        /// 锁定挑战机会
        /// </summary>
        WorldChallengeLock = 30,
        /// <summary>
        /// 恢复挑战赛体能
        /// </summary>
        WorldChallengeAddPhysical = 31,
        /// <summary>
        /// 卡库
        /// </summary>
        Cardlib = 35,
        /// <summary>
        /// 技能卡包
        /// </summary>
        SkillCard = 36,
        /// <summary>
        /// 新手礼包
        /// </summary>
        NewPlayerPack = 37,
        /// <summary>
        /// 恢复训练体力
        /// </summary>
        AddTrainStamina = 38,
        /// <summary>
        /// 添加buff
        /// </summary>
        AddBuff = 39,
        /// <summary>
        /// 合成百搭
        /// </summary>
        SynthesisWildCard = 40,
        /// <summary>
        /// 幸运符
        /// </summary>
        Luckycharm = 41,
        /// <summary>
        /// 挑战赛体能饮料
        /// </summary>
        WorldChallengePhysicalDrink = 42,
        /// <summary>
        /// 金卡配方
        /// </summary>
        GoldFormula = 43,

        /// <summary>
        /// 百搭保护卡
        /// </summary>
        WildProtectedCard = 46,
        /// <summary>
        /// 龙膜百搭卡
        /// </summary>
        WildDragonCard = 47,
        /// <summary>
        /// 技能经验卡
        /// </summary>
        SkillCardExp = 48,
        /// <summary>
        /// 修改logo
        /// </summary>
        UpdateLogo = 49,

        /// <summary>
        /// 节日礼包
        /// </summary>
        HolidayGifts = 50,
        /// <summary>
        /// 传奇精魄
        /// </summary>
        Legend = 51,
        /// <summary>
        /// 呜呜祖拉
        /// </summary>
        Vuvuzela = 52,

        MyStery = 54,
        /// <summary>
        /// 合同页
        /// </summary>
        TheContract = 56,
        /// <summary>
        /// 星辰元素
        /// </summary>
        TheStars = 58,
        /// <summary>
        /// 时空密钥
        /// </summary>
        TimeAndSpace = 59,
        /// <summary>
        /// 星座宇宙
        /// </summary>
        TheUniverse = 60,
        /// <summary>
        /// 圣光祝福
        /// </summary>
        Benediction = 61,
        /// <summary>
        /// 黄道挑战令
        /// </summary>
        GoldChallenge = 63,
        /// <summary>
        /// 精英横扫券
        /// </summary>
        EliteHookItem = 64,
        /// <summary>
        /// 增加荣誉值
        /// </summary>
        AddHonor = 65,
        /// <summary>
        /// 增加经验
        /// </summary>
        AddExp = 66,
        /// <summary>
        /// 月卡
        /// </summary>
        MonthCard = 67,
        /// <summary>
        /// 礼包
        /// </summary>
        GiftBag = 68,
        /// <summary>
        /// 装备碎片
        /// </summary>
        EquipmentDebris = 69,
        /// <summary>
        /// 首冲返利
        /// </summary>
        FirstCharge =70,
        /// <summary>
        /// 奥运金牌
        /// </summary>
        OlympicGoldMeda =71,
        /// <summary>
        /// 每周礼包
        /// </summary>
        WeeklyGiftBag = 72,
        /// <summary>
        /// 教练碎片
        /// </summary>
        CoachDebris=73,
        /// <summary>
        /// 教练碎片宝箱
        /// </summary>
        CoachDebrisGiftBag=74,
        /// <summary>
        /// 教练证书
        /// </summary>
        CoachCertificate = 75,
    }
}
