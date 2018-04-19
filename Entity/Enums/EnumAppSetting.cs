using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// EnumAppsetting
    /// </summary>
    public enum EnumAppsetting
    {
        #region Ladder
        /// <summary>
        /// [天梯赛]新手保护分数线
        /// </summary>
        LadderProctiveScore,
        /// <summary>
        /// [天梯赛]开始一轮天梯条件:>=时间(秒)And>=人数 | ...
        /// </summary>
        LadderMatchCondition,
        /// <summary>
        /// [天梯赛]开始关闭时间
        /// </summary>
        LadderCloseStart,
        /// <summary>
        /// [天梯赛]结束关闭时间
        /// </summary>
        LadderCloseEnd,
        /// <summary>
        /// [天梯赛]荣誉兑换，各类型卡刷新概率
        /// </summary>
        LadderExchangeRate,
        /// <summary>
        /// [天梯赛]荣誉兑换，刷新频率
        /// </summary>
        LadderExchangeShowDay,

        CrossLadderCondition,
        /// <summary>
        /// 天梯获胜获得的经验值
        /// </summary>
        LadderWinExp,
        /// <summary>
        /// 天梯打平获得的经验值
        /// </summary>
        LadderDrawExp,
        /// <summary>
        /// 天梯输掉了获得的经验值
        /// </summary>
        LadderLoseExp,
        /// <summary>
        /// 天梯获胜获得的金币
        /// </summary>
        LadderWinCoin,
        /// <summary>
        /// 天梯打平获得的金币
        /// </summary>
        LadderDrawCoin,
        /// <summary>
        /// 天梯输掉了获得的金币
        /// </summary>
        LadderLoseCoin,
        /// <summary>
        /// 天梯赛挂机cd
        /// </summary>
        LadderHookCD,
        /// <summary>
        /// 天梯赛VIP用户比赛CD
        /// </summary>
        LadderVipMatchCD,
        /// <summary>
        /// 天梯赛非VIP用户比赛CD
        /// </summary>
        LadderNotVipMatchCD,
        /// <summary>
        /// 天梯赛VIP用户清除CD价格
        /// </summary>
        LadderVipClearCDPrice,
        /// <summary>
        /// 天梯赛非VIP用户清除CD价格
        /// </summary>
        LadderNotVipClearCDPrice,

        LadderHookExpired,
        #endregion

        #region Register
        /// <summary>
        /// [创建经理]时的初始体力
        /// </summary>
        RegisterStamina,
        /// <summary>
        /// [创建经理]天梯赛初始积分
        /// </summary>
        RegisterLadderScore,
        /// <summary>
        /// [创建经理]时的初始背包格数
        /// </summary>
        RegisterPackageSize,
        /// <summary>
        /// [创建经理]初始替补席位
        /// </summary>
        RegisterSubstitute,
        /// <summary>
        /// [创建经理]初始技能数量
        /// </summary>
        RegisterSkillMax,
        /// <summary>
        /// [创建经理]初始阵型id
        /// </summary>
        RegisterFormationId,
        /// <summary>
        /// [创建经理]初始训练席位
        /// </summary>
        RegisterTrainSeat,
        /// <summary>
        /// [注册]赠送金币
        /// </summary>
        RegisterSendCoin,
        /// <summary>
        /// [注册]赠送点券
        /// </summary>
        RegisterSendPoint,

        #endregion

        #region Common
        /// <summary>
        /// [通用]恢复单位行动力所需时间(单位:分钟)
        /// </summary>
        ResumeStaminaTimeConfig,
        /// <summary>
        /// 恢复体力加速VIP等级
        /// </summary>
        ResumeStaminaAccelerateVip,
        /// <summary>
        /// [通用]每次恢复体力数量
        /// </summary>
        ResumeStaminaCount,
        /// <summary>
        /// [天梯赛]Bot名称
        /// </summary>
        BotName,
        /// <summary>
        /// [天梯赛]Bot胜率
        /// </summary>
        BotWinRate,
        /// <summary>
        /// [通用]经理等级上限
        /// </summary>
        ManagerMaxLevel,
        /// <summary>
        /// [装备]属性类型列表
        /// </summary>
        EquipmentPropertyTypeRange,
        /// <summary>
        /// [通用]球员强化等级可获得对应的属性加成，索引为强化等级
        /// </summary>
        PlayerCardStrengthPlus,
        /// <summary>
        /// [物品]新手礼包编码列表
        /// </summary>
        ItemNewPackCodes,
        /// <summary>
        /// [经理]升级增加体力数量
        /// </summary>
        ManagerLevelupAddStamina,
        /// <summary>
        /// 人人老玩家召回邮件
        /// </summary>
        RrRecallMailContent,

        LevelGiftConfig,

        VipPlusConfig,

        ScoutingBlackGoldConfig,

        StaminaGiftTimes,

        StaminaGiftCount,

        GuideStepGift1,

        GuideStepGift2,
        #endregion

        #region Tour

        /// <summary>
        /// 巡回赛每次扣除的行动力
        /// </summary>
        TourMinusStamina,

        /// <summary>
        /// 精英巡回赛每轮扣除的行动力
        /// </summary>
        TourEliteStamina,

        /// <summary>
        /// 巡回赛抽奖物品数量
        /// </summary>
        TourLotteryCount,

        /// <summary>
        /// 精英巡回每天默认可重置次数
        /// </summary>
        TourEliteCount,

        /// <summary>
        /// 挂机每轮间隔时间(秒)
        /// </summary>
        TourHookCD,

        /// <summary>
        /// 每次重置精英巡回需要的点券数量
        /// </summary>
        TourEliteRestPoint,
        /// <summary>
        /// [巡回赛]秒挂机cd商品编码
        /// </summary>
        TourClearCDCode,
        /// <summary>
        /// [巡回赛]新手抽卡卡库
        /// </summary>
        TourGuideCardLib,
        /// <summary>
        /// [巡回赛]新手引导时替换上场的球员
        /// </summary>
        TourGuidePlayers,
        /// <summary>
        /// [巡回赛]比赛cd时间
        /// </summary>
        TourMatchWaitTime,

        /// <summary>
        /// 精英巡回赛横扫巨星合同掉落概率
        /// </summary>
        EliteHookSupperPrizeRate,
        #endregion

        #region Teammember
        /// <summary>
        /// 球员成长大力丸物品ID
        /// </summary>
        GrowDaliWanId,
        /// <summary>
        /// 球员成长普通成长达到次数可免费快速成长
        /// </summary>
        GrowCountToFast,
        /// <summary>
        /// 球员成长每天免费快速成长次数上限
        /// </summary>
        GrowDayFastCountMax,
        /// <summary>
        /// 球员成长普通成长增加点数
        /// </summary>
        GrowPoint,
        /// <summary>
        /// 球员成长快速成长增加点数(3~6)
        /// </summary>
        GrowFastPointRange,
        /// <summary>
        /// 球员成长突破失败需要下降成长度(百分比)
        /// </summary>
        GrowFailPercent,
        /// <summary>
        /// [球员训练]球员体力上限
        /// </summary>
        TrainStamina,
        /// <summary>
        /// [球员训练]时间上限
        /// </summary>
        TrainSecondMax,
        /// <summary>
        /// [球员训练]每分钟获得的经验数量
        /// </summary>
        TrainExpPerSecond,
        /// <summary>
        /// [球员训练]每分钟消耗体力数量
        /// </summary>
        TrainCostStaminaSecond,
        /// <summary>
        /// [球员训练]每分钟恢复体力数量
        /// </summary>
        TrainRestorStaminaSecond,
        /// <summary>
        /// [球员]元老球员上限
        /// </summary>
        MaxVeteranCount,
        /// <summary>
        /// [球员]橙卡和元老数量额外buff
        /// </summary>
        SolutionLegendPlusConfig,
        #endregion

        #region Mall
        /// <summary>
        /// [商城]购买数量限制
        /// </summary>
        MallBuyCountLimit,
        /// <summary>
        /// [商城]扩展背包次数限制
        /// </summary>
        MallBuyPackageCountLimit,
        /// <summary>
        /// [商城]购买体力增加体力数量
        /// </summary>
        MallExtraAddStaminaCount,
        /// <summary>
        /// [商城]扩展背包增加格数
        /// </summary>
        MallExtraExpandPackageSize,
  
        #endregion

        #region Charge
        /// <summary>
        /// [充值]现金转换成Vip积分数量，单位元
        /// </summary>
        PayChargeCashToVipScore,
        /// <summary>
        /// 竞猜机器人账号
        /// </summary>
        GambleAccount,
        /// <summary>
        /// [活动]连续7天充值活动需要的点数
        /// </summary>
        PayContinuePoint,
        /// <summary>
        /// 充值翻倍配置
        /// </summary>
        ChargeDoubleConfig,
        /// <summary>
        /// 充值返利配置
        /// </summary>
        ChargeReturnConfig,
        #endregion

        #region Dailycup
        /// <summary>
        /// [每日杯赛]以秒为单位，计算当天的第几秒创建每日杯赛
        /// </summary>
        DailycupCreateLeagueTime,
        /// <summary>
        /// [每日杯赛]以秒为单位，计算当天的第几秒运行每日杯赛结果
        /// </summary>
        DailycupCaculateTime,
        /// <summary>
        /// [每日杯赛]报告间隔时间
        /// </summary>
        DailycupReportSendInterval,
        /// <summary>
        /// [每日杯赛]淘汰赛每轮开始比赛时间,单位秒
        /// </summary>
        DailycupFinalRoundStart,
        /// <summary>
        /// [每日杯赛]显示的淘汰赛轮数
        /// </summary>
        DailycupSemiFinalRoundCount,
        /// <summary>
        /// [每日杯赛]投注关闭时间，单位秒
        /// </summary>
        DailycupGambleCloseTime,
        /// <summary>
        /// [每日杯赛]淘汰赛每轮比赛时间
        /// </summary>
        DailycupFinalRoundMatchTime,
        /// <summary>
        /// [每日杯赛]每日杯赛保留日期
        /// </summary>
        DailycupLeagueDays,
        /// <summary>
        /// [每日杯赛]每天几点开始显示淘汰赛的第一轮
        /// </summary>
        DailycupMyFirstRoundTime,
        /// <summary>
        /// [每日杯赛]最多显示n轮
        /// </summary>
        DailycupMyRoundInterval,
        /// <summary>
        /// [每日杯赛]DcEventSendTime
        /// </summary>
        DailycupEventSendTime,
        /// <summary>
        /// [每日杯赛]最多显示n轮
        /// </summary>
        DailycupShowMaxMatchRound,
        /// <summary>
        /// [每日杯赛]每轮开奖时间
        /// </summary>
        DailycupGambleOpenTime,
        /// <summary>
        /// [杯赛]报名奖励物品
        /// </summary>
        DailycupAttendPrizeItem,
        /// <summary>
        /// [杯赛]每获胜一场奖励物品
        /// </summary>
        DailycupWinPrizeItem,
        /// <summary>
        /// [杯赛]每获胜一场奖励金币数量
        /// </summary>
        DailycupWinPrizeCoin,

        #endregion

        #region Pandora
        /// <summary>
        /// 球员卡强化分水岭，大于这个等级卡牌消失
        /// </summary>
        PandoraStrenthMid,
        /// <summary>
        /// 可强化的球员卡的最大等级
        /// </summary>
        PandoraStrengthMax,
        /// <summary>
        /// [潘多拉]中级百搭卡适用强化等级
        /// </summary>
        PandoraWildCardMidStrength,
        /// <summary>
        /// [潘多拉]初级百搭卡适用强化等级
        /// </summary>
        PandoraWildCardLowStrength,
        /// <summary>
        /// [潘多拉]分解暴击时提升百分比
        /// </summary>
        PandoraDecomposeCritPlus,
        /// <summary>
        /// [潘多拉]洗炼融合剂商品编码
        /// </summary>
        PandoraFusogenMallcode,
        /// <summary>
        /// [潘多拉]洗炼石列表，从1级到4级
        /// </summary>
        EquipmentWashStoneList,
        /// <summary>
        /// [潘多拉]装备合成时最少装备数量
        /// </summary>
        EquipmentSynthesisMinEquipment,
        /// <summary>
        /// 装备出售获得金币
        /// </summary>
        EquipmentSellCoin,
        /// <summary>
        /// [潘多拉]金币合成卡合成概率
        /// </summary>
        GoldFormulaRate,
        /// <summary>
        /// [潘多拉]金币合成卡卡库
        /// </summary>
        GoldFormulaLib,
        /// <summary>
        /// [潘多拉]图纸分解获得金币
        /// </summary>
        SuitdrawingDecomposeCoin,
        /// <summary>
        /// [潘多拉]图纸分解获得装备库id
        /// </summary>
        SuitdrawingDecomposeLibId,
        /// <summary>
        /// [潘多拉]图纸分解获得装备概率
        /// </summary>
        SuitdrawingDecomposeRate,

        /// <summary>
        /// 精铸金币
        /// </summary>
        PrecisionCastingCoin,

        /// <summary>
        /// 精铸点券
        /// </summary>
        PrecisionCastingPoint,

        /// <summary>
        /// 装备洗练锁定已激活球星封印
        /// </summary>
        EquipmentWashLockSkillPrice,
        /// <summary>
        /// 强化球员卡需要物品code
        /// </summary>
        StrengthPlayerItemCode,
        #endregion

        #region Task
        /// <summary>
        /// [任务]开放每日任务要求等级
        /// </summary>
        TaskDailyOpenLevel,
        /// <summary>
        /// [任务]每天可完成的每日任务数量
        /// </summary>
        TaskDailyCount,
        /// <summary>
        /// [任务]刷新每日任务对应的商品编码
        /// </summary>
        TaskDailyRefreshMallcode,

        GuidePrizeList,
        GuidePrizeSecond,
        GuideFinishTaskId,

        DailyTaskFinishPoint,
        #endregion

        #region Mail
        /// <summary>
        /// [邮件]带附件邮件过期天数
        /// </summary>
        MailAttachmentExpireDay,
        /// <summary>
        /// [邮件]邮件过期天数
        /// </summary>
        MailExpireDay,

        #endregion

        #region WorldChallenge
        /// <summary>
        /// [世界挑战赛]每天免费次数
        /// </summary>
        WChallengeDayFreeTimes,
        /// <summary>
        /// [世界挑战赛]报名商品编码
        /// </summary>
        WChallengeAttendMallcode,
        /// <summary>
        /// [世界挑战赛]鼓舞每次提升buff值
        /// </summary>
        WChallengeBoostBuffPer,
        /// <summary>
        /// [世界挑战赛]鼓舞提升最大buff值
        ///</summary>
        WChallengeBoostBuffMax,
        /// <summary>
        /// [世界挑战赛]奖励高级百搭卡编码
        /// </summary>
        WChallengePrizeWildCardCode,
        /// <summary>
        /// [世界挑战赛]不扣体能需要的净胜球数量
        /// </summary>
        WChallengePhysicalScoreRange,
        /// <summary>
        /// [世界挑战赛]每次扣体能数量
        /// </summary>
        WChallengePhysicalCostCount,
        /// <summary>
        /// [世界挑战赛]失败机会
        /// </summary>
        WChallengeFailTimes,
        /// <summary>
        /// [世界挑战赛]开始下一关需等待时间
        /// </summary>
        WChallengeWaitTime,
        /// <summary>
        /// [世界挑战赛]挂机cd，单位秒
        /// </summary>
        WChallengeHookCD,
        /// <summary>
        /// [世界挑战赛]秒挂机cd商品编码
        /// </summary>
        WChallengeClearCDCode,
        /// <summary>
        /// [世界挑战赛]鼓舞每次提升士气值
        /// </summary>
        WChallengeBoostPer,
        #endregion

        #region Friend
        /// <summary>
        /// [好友]最大数量
        /// </summary>
        FriendMaxCount,
        /// <summary>
        /// [好友]帮助训练减少的cd，单位秒
        /// </summary>
        FriendHelpTrainSeconds,
        /// <summary>
        /// [好友]每日增加亲密度上限
        /// </summary>
        FriendDayIntimacyMax,
        /// <summary>
        /// [好友]帮助训练增加亲密度
        /// </summary>
        FriendHelpIntimacyCount,
        /// <summary>
        /// [好友]友谊赛增加亲密度
        /// </summary>
        FriendMatchIntimacyCount,
        /// <summary>
        /// [好友]每日同一好友挑战上限
        /// </summary>
        FriendDayMatchMax,
        /// <summary>
        /// [好友]每亲密度增加帮组训练效果，单位秒
        /// </summary>
        FriendPerIntimacyAddSeconds,
        /// <summary>
        /// [好友]每亲密度增加金币数量
        /// </summary>
        FriendPerIntimacyAddCoin,
        /// <summary>
        /// [好友]友谊赛cd时间，单位秒
        /// </summary>
        FriendMatchWaitTime,
        /// <summary>
        /// 加速训练需要物品
        /// </summary>
        SpeedUpItemCode,
        /// <summary>
        /// 加速药水增加经验
        /// </summary>
        SpeedUpItemAddExp,
        /// <summary>
        /// 好友帮助增加经验
        /// </summary>
        SpeedUpFriendAddExp,
        /// <summary>
        /// [好友]每日同一好友帮助上限
        /// </summary>
        FriendDayHelpMax,

        #endregion

        #region 好友邀请
        FriendinviteLevel,
        FriendinvitePoint,
        #endregion

        #region 投资理财
        /// <summary>
        /// 充值月卡立刻返回的砖石数
        /// </summary>
        InvestDepositMonth,
        /// <summary>
        /// 充值月卡需要的钻石数
        /// </summary>
        DepositMonthly,
        #endregion
        #region 球员升星
        /// <summary>
        /// 潜力重置消耗点卷
        /// </summary>
        PotentialResetPoint,
        /// <summary>
        /// 潜力锁定1个消耗点卷
        /// </summary>
       PotentialLock1,
        /// <summary>
        /// 潜力锁定2个消耗点卷
        /// </summary>
       PotentialLock2,
        #endregion

        /// <summary>
        /// [球探]抽十次需要点券倍数，10倍打9折
        /// </summary>
        ScoutingTenPointMultiple,
        /// <summary>
        /// [球探]十连抽时橙卡上限
        /// </summary>
        ScoutingTenOrangeCount,
        /// <summary>
        /// 球探抽卡VIP多少免费抽奖的物品才不绑定
        /// </summary>
        ScoutingVipNotBinding,
        /// <summary>
        /// 球探抽卡免费次数
        /// </summary>
        ScoutingFellNumber,
        /// <summary>
        /// 球探按能力值抽卡概率
        /// </summary>
        ScoutingLotteryRate,
        /// <summary>
        /// 球探抽卡点券消耗
        /// </summary>
        ScoutingLotteryPoint,
        /// <summary>
        /// 金牌球探每天掉落可交易数量
        /// </summary>
         ScoutingDropOutNumber,
        /// <summary>
        /// 球探金币刷新
        /// </summary>
        ScoutingLotteryCoinRefresh,
        /// <summary>
        /// 球探点券刷新
        /// </summary>
        ScoutingLotteryPointRefresh,
        /// <summary>
        /// 新手引导固定86橙卡
        /// </summary>
        GuideLotteryCard,
        /// <summary>
        /// 联赛刷新间隔
        /// </summary>
        LeagueFreshInterval,
        /// <summary>
        /// 比赛竞猜奖池最小金额
        /// </summary>
        MinGamblePoolMoney,
        /// <summary>
        /// 开奖任务扫描时间间隔
        /// </summary>
        OpenGambleInterval,

        #region SkillCard
        /// <summary>
        /// 技能背包上限
        /// </summary>
        SKILLCardMaxBagSize,
        /// <summary>
        /// 技能背包上限
        /// </summary>
        SKILLCardMaxBagMapLen,
        /// <summary>
        /// 初始技能可装备数
        /// </summary>
        SKILLCardRAWSkillCellSize,
        /// <summary>
        /// 一键合并所需vip等级
        /// </summary>
        SKILLCardMixVipLevel,
        /// <summary>
        /// 一键学习所需vip等级
        /// </summary>
        SKILLCardAskVipLevel,
        /// <summary>
        /// 跳级学习所需vip等级
        /// </summary>
        SKILLCardAskSkipVipLevel,
        /// <summary>
        /// 一键拾取所需vip等级
        /// </summary>
        SKILLCardPickVipLevel,
        #endregion

        #region MSkill
        /// <summary>
        /// 重置天赋商品id
        /// </summary>
        MSKILLGoldItem4ResetTalent,
        /// <summary>
        /// 重置天赋价格
        /// </summary>
        MSKILLPriceE4ResetTalent,
        #endregion

        #region AdTopScorer
        /// <summary>
        /// 当前点球活动
        /// </summary>
        HOTAdType,
        /// <summary>
        /// 每日免费次数
        /// </summary>
        ADMaxDayCredit,
        /// <summary>
        /// 锁定球票价格
        /// </summary>
        ADPointPrice4LockGift,
        /// <summary>
        /// 射飞概率3%
        /// </summary>
        ADRateShootFly,
        /// <summary>
        /// 射中门框概率3%
        /// </summary>
        ADRateShootFrame,
        /// <summary>
        /// 扑救一致概率45%
        /// </summary>
        ADRateDiveSame,

        /// <summary>
        /// 刷新点球兑换钻石数
        /// </summary>
        RefreshExChangePoint,
        #endregion

        #region Guild
        GuildREQMLv4Create,
        GuildREQCoin4Create,
        GuildNUM4ViceLeader,
        GuildLIMITTimes4Draw,
        GuildLIMITActive4Tour,
        GuildUNITActive4Tour,
        GuildLIMITActive4Ladder,
        GuildUNITActive4Ladder,
        GuildExpireDays4AutoGrant,
        #endregion

        #region GuildWar
        /// <summary>
        /// 报名公会战力限制
        /// </summary>
        GWarLIMITKpi4Join,
        GWarTIMERDueMs4MasterProc,
        GWarTIMERDueMs4MasterStore,
        GWarTIMERDueMs4MasterRank,
        GWarTIMERDueMs4ThreadProc,
        GWarTIMERDueMs4ThreadStore,
        GWarTIMERDueSeconds4Kpi,
        GWarTIMERDueSeconds4Redo,
        GWarEXPIRESeconds4RaiseCD,
        GWarEXPIRESeconds4LoseCD,
        GWarEXPIRESeconds4GroupCD,
        GWarEXPIRESeconds4KO,
        GWarEXPIRESeconds4KOPlus,
        GWarREQVipLevel4Hook,
        GWarSIZEPrizeByeRound,
        GWarMAXWinScore,
        GWarMAXDebuff,
        GWarUNITWinDebuff,
        GWarUNITLoseDebuff,
        #endregion

        #region PlayerKill
        /// <summary>
        /// [PK赛]奖励列表，剩平负，金币经验
        /// </summary>
        PKMatchPrize,
        /// <summary>
        /// [PK赛]cd
        /// </summary>
        PkMatchWaitTime,
        /// <summary>
        /// [PK赛]经理等级限制
        /// </summary>
        PKMinLevel,
        /// <summary>
        /// PK赛每天挑战次数
        /// </summary>
        PKChallengeTimes,

        /// <summary>
        /// PK赛各Vip等级每日可购买次数
        /// </summary>
        PKVipCanBuyTimes,
        #endregion
       
        /// <summary>
        /// 疯狂精铸史诗装备橙色技能概率提升
        /// </summary>
        QualityRateString1,
        /// <summary>
        /// 疯狂精铸卓越装备橙色技能概率提升
        /// </summary>
        QualityRateString2,
        /// <summary>
        /// 不能通关合成得到的元老卡
        /// </summary>
        NotSynthesisPlayer,
        /// <summary>
        /// 月卡每天钻石数量
        /// </summary>
        MonthCardPointNunber,
        /// <summary>
        /// 限时礼包时间戳
        /// </summary>
        LimitedPackage,
        /// <summary>
        /// 节日限时礼包
        /// </summary>
        SevenPackage,
        #region 欧洲杯
        /// <summary>
        /// 欧洲杯竞猜点卷配置
        /// </summary>
        EuropeGamblePoint,
        /// <summary>
        /// 欧洲杯装备碎片
        /// </summary>
        EuropeEquipmentDebris,
        /// <summary>
        /// 欧洲杯抽卡获得碎片概率
        /// </summary>
        EuropeScoutingRate,
        #endregion

        #region  竞技场
        /// <summary>
        /// 竞技场上阵球员限制
        /// </summary>
        ArenaUpFormationPlayer,

        /// <summary>
        /// 竞技场最大体力
        /// </summary>
        ArenaMaxStamina,
        /// <summary>
        /// 竞技场购买体力配置
        /// </summary>
        ArenaBuyStaminaConfig,
        /// <summary>
        /// 竞技场体力恢复配置
        /// </summary>
        ArenaStaminaRestore,
        /// <summary>
        /// 竞技场刷新商店配置
        /// </summary>
        ArenaRefreshShop,
        #endregion
        #region 腾讯玩吧
        /// <summary>
        /// 腾讯玩吧下单url
        /// </summary>
        TxWb_ChargeUrl,
        /// <summary>
        /// 腾讯玩吧达人查询url
        /// </summary>
        TxWb_FindVipUrl,
        /// <summary>
        /// 腾讯玩吧appId（固定不变）
        /// </summary>
        TxWb_AppId,
        #endregion

        #region 球星启示录
        /// <summary>
        /// 球星启示录通关士气
        /// </summary>
        RevelationMorale,
        /// <summary>
        /// 球星启示录通关失败扣士气
        /// </summary>
        RevelationSubtractMorale,
        /// <summary>
        /// 球星启示录增加士气价格
        /// </summary>
        RevelationPlusMorale,
        /// <summary>
        /// 球星启示录增加翻牌次数价格
        /// </summary>
        RevelationDrawsPrice,
        /// <summary>
        /// 球星启示录最多扣多少士气
        /// </summary>
        RevelationMaxSubtractMorale,
        /// <summary>
        /// 勇气商城刷新价格配置
        /// </summary>
        RevelationRefrestShopPrice,
        #endregion
        /// <summary>
        /// 转盘活动时间
        /// </summary>
        TurntableActivityTime,
        /// <summary>
        /// 奥运活动时间
        /// </summary>
        OlympicActivityTime,
        /// <summary>
        /// A8平台独服列表
        /// </summary>
        H5_A8AloneZoneList,
        /// <summary>
        /// 玩吧平台编码
        /// </summary>
        H5_A8WanBa,
        /// <summary>
        /// 群黑和9G特殊处理
        /// </summary>
        H5_A8QunheiAnd9GList,
        /// <summary>
        /// 充值送幸运币
        /// </summary>
        TurntableLuckyCoinConfig,
        /// <summary>
        /// 充值送游戏币
        /// </summary>
        PenaltyKickGameCurrencyConfig,
        /// <summary>
        /// 应用调试者
        /// </summary>
        H5_A8Debug,
        /// <summary>
        /// 白鹭key
        /// </summary>
        EgretAppKey,
        /// <summary>
        /// 白鹭
        /// </summary>
        H5_Egret,
        /// <summary>
        /// 小熊
        /// </summary>
        H5_Bear,
        /// <summary>
        /// a8新增查询接口key
        /// </summary>
        H5_A8DataKey,
        /// <summary>
        /// a8 360礼包
        /// </summary>
        H5_360GoodsBag,
        /// <summary>
        /// 888kkk兑换码礼包
        /// </summary>
        H5_888kkk,
        /// <summary>
        /// 卡包一次开出5张卡的mallCode
        /// </summary>
        H5_CardCount5,
        /// <summary>
        /// 随机传奇球员卡
        /// </summary>
        RandomLegendCode,
        /// <summary>
        /// 随机传奇球员卡碎片
        /// </summary>
        RandomLegendDebrisCode,

        #region Crowd
        /// <summary>
        /// 群雄派送时间
        /// </summary>
        CrowdKillDoubleTime,
        /// <summary>
        /// [群雄逐鹿]开始时间,结束时间
        /// </summary>
        CrowdTime,
        /// <summary>
        /// [群雄逐鹿]起始士气
        /// </summary>
        CrowdMoraleInit,
        /// <summary>
        /// [群雄逐鹿]普通玩家cd
        /// </summary>
        CrowdCd,
        /// <summary>
        /// [群雄逐鹿]Vip玩家cd
        /// </summary>
        CrowdVipCd,
        /// <summary>
        /// [群雄逐鹿]配对条件
        /// </summary>
        CrowdCondition,
        /// <summary>
        /// [群雄逐鹿]复活cd
        /// </summary>
        CrowdResurrectionCd,
        /// <summary>
        /// [群雄逐鹿]单次活动最高奖励点券数量
        /// </summary>
        CrowdMaxPoint,
        /// <summary>
        /// [群雄逐鹿]单次活动最高奖励传奇精魄数量
        /// </summary>
        CrowdMaxLegendCount,
        #endregion
        #region 点球
        HOTAdResetExChange,
        HOTAdOnePrize,
        HOTAdTwoPrize,
        HOTAdTherePrize,
        /// <summary>
        /// 活动时间
        /// </summary>
        ActivityTime,
        #endregion

        #region 球场点击奖励
        APPCode,
        MATCHRewardMaxCoin,
        MATCHRewardMaxPoint,
        MATCHRewardMaxGetTimes,
        MATCHRewardMaxSetTimes,
        #endregion

        /// <summary>
        /// 跨服活动恢复间隔
        /// </summary>
        CrossActivityRecoverInterval,
        /// <summary>
        /// 跨服活动恢复多少
        /// </summary>
        CrossActivityRecoverValue,

        Tx_server_name,
    }

    public enum EnumAppCode
    {
        /// <summary>
        /// 通用
        /// </summary>
        COMMON,
        /// <summary>
        /// 热血英超
        /// </summary>
        RXYC,
    }
}
