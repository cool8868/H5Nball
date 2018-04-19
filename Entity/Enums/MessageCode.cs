using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NBall.Common;

namespace Games.NBall.Entity.Enums
{

    /// <summary>
    ///  消息code枚举
    /// </summary>
    public enum MessageCode
    {
        AdFailUpdatePack = -2000,
        /// <summary>
        /// Bad Request.
        /// </summary>
        BadRequest = -999,
        /// <summary>
        /// 系统繁忙,请稍后再试.
        /// </summary>
        WebServerException = -100,
        /// <summary>
        /// 系统繁忙,请稍后再试.
        /// </summary>
        ExecuteFail = -2,

        /// <summary>
        /// 系统繁忙,请稍后再试.
        /// </summary>
        Exception = -1,
        /// <summary>
        /// 操作超时,请重试！
        /// </summary>
        SystemBusy = -3,
        /// <summary>
        /// 您的等级未达到开放该功能要求,请升级后再来
        /// </summary>
        NbFunctionNotOpen = -4,
        /// <summary>
        /// 操作成功.
        /// </summary>
        Success = 0,

        #region Normal (1 到 20) Nb开头
        /// <summary>
        /// 参数不正确.
        /// </summary>
        NbParameterError = 1,

        /// <summary>
        /// 您的游戏币不足.
        /// </summary>
        NbCoinShortage = 2,

        /// <summary>
        /// 钻石不足
        /// </summary>
        NbPointShortage = 3,

        /// <summary>
        /// 没有定义的消息编号.
        /// </summary>
        NbNoDefineMessageCode = 4,

        /// <summary>
        /// 更新失败,请稍后再试.
        /// </summary>
        NbUpdateFail = 6,

        /// <summary>
        /// 已抽过奖.
        /// </summary>
        NbLotteryNot = 7,

        /// <summary>
        /// 正在挂机.
        /// </summary>
        NbNowHook = 8,

        /// <summary>
        /// 删除失败.
        /// </summary>
        NbDeleteFail = 9,

        /// <summary>
        /// 体力不足.
        /// </summary>
        NbStaminaShortage = 10,

        /// <summary>
        /// 背包不存在.
        /// </summary>
        NbNoPackage = 11,
        /// <summary>
        /// 经理等级以达上限
        /// </summary>
        NbManagerLevelOver = 12,
        /// <summary>
        /// 暂无排名
        /// </summary>
        NbRankAfter = 13,

        /// <summary>
        /// 更新失败,请稍后再试(p).
        /// </summary>
        NbUpdateFailPackage = 14,
        /// <summary>
        /// 更新失败,请稍后再试(m).
        /// </summary>
        NbUpdateFailManager = 15,
        /// <summary>
        /// 不能重复领奖
        /// </summary>
        NbPrizeRepeat = 16,

        /// <summary>
        /// CD中，请耐心等待
        /// </summary>
        NbMatchCd = 17,
        /// <summary>
        /// 没有CD，无须处理
        /// </summary>
        NbMatchNoCd = 18,
        /// <summary>
        /// 您所在区未开放跨服服务
        /// </summary>
        NbDomainInvalid = 19,
        /// <summary>
        /// 绑定钻石不足，快去投资理财吧
        /// </summary>
        NbBindPointShortage = 20,
        #endregion

        #region 登录相关(21 到 40) Login开头
        /// <summary>
        /// 没有注册.
        /// </summary>
        LoginNoRegister = 21,

        /// <summary>
        /// 您还未登录.
        /// </summary>
        LoginNoLogin = 22,


        /// <summary>
        /// 该帐号已被封停.
        /// </summary>
        LoginOnlineLock = 23,

        /// <summary>
        /// 您的帐号已在别处登录.
        /// </summary>
        LoginOnlineBump = 24,

        /// <summary>
        /// 您已被系统踢线.
        /// </summary>
        LoginOnlineKick = 25,

        /// <summary>
        /// 存在多个经理,需要选择.
        /// </summary>
        LoginMultiManager = 26,

        /// <summary>
        /// 账号不存在,请重新登录.
        /// </summary>
        LoginNoUser = 27,

        /// <summary>
        /// 没有获取到原区服信息
        /// </summary>
        NoMergeZoneInfo = 28,
        /// <summary>
        /// 该绑定码已被使用
        /// </summary>
        BindCodeNotExist = 29,
        /// <summary>
        /// 已经设置过名字
        /// </summary>
        HaveSetName = 30,
        /// <summary>
        /// 经理不存在
        /// </summary>
        LoginNoManager = 31,
        /// <summary>
        /// 登录失败
        /// </summary>
        LoginError = 32,
        #endregion

        #region 注册相关 (41 到 60) Register开头
        /// <summary>
        /// 该经理名已存在,请换一个名称。
        /// </summary>
        RegisterNameRepeat = 41,

        /// <summary>
        /// 经理名里包含了屏蔽字,请更换一个经理名！
        /// </summary>
        RegisterNameContainBadWord = 42,

        /// <summary>
        /// 经理名不能为空
        /// </summary>
        RegisterNameIsEmpty = 43,

        /// <summary>
        /// 经理名包含无效字符,请重新输入
        /// </summary>
        RegisterNameHasValidWord = 44,

        /// <summary>
        /// 经理名的长度只能在4-12个字符之间
        /// </summary>
        RegisterNameLengthRange = 45,

        /// <summary>
        /// 您的账号下已有经理,不用再创建了.
        /// </summary>
        RegisterExistsManager = 46,

        /// <summary>
        /// 创建角色失败.
        /// </summary>
        RegisterFail = 47,
        /// <summary>
        /// 创建角色失败,配置不存在
        /// </summary>
        RegisterNoTemplate = 48,
        /// <summary>
        /// 真实姓名格式不正确
        /// </summary>
        RegisterRealNameValid = 49,
        /// <summary>
        /// 身份证号码格式不正确，请重新输入
        /// </summary>
        RegisterCertIdValid = 50,
        #endregion

        #region Match相关 (100-149) Match 开头
        /// <summary>
        /// 创建比赛失败.
        /// </summary>
        MatchCreateFail = 100,
        /// <summary>
        /// 正在创建比赛,请等待.
        /// </summary>
        MatchWait = 101,
        /// <summary>
        /// 比赛不存在.
        /// </summary>
        MatchMiss = 102,
        /// <summary>
        /// 创建比赛失败,参数不正确
        /// </summary>
        MatchStateObjisNull = 103,
        #endregion

        #region 物品相关 (109-199)  Item 开头

        /// <summary>
        /// 您没有更名卡
        /// </summary>
        UpdateNameCardNot=109,
        /// <summary>
        /// 主力球员不能作为副卡
        /// </summary>
        MainPlayerNoStrength = 110,
        /// <summary>
        /// 训练中的球员不能作为副卡
        /// </summary>
        TrainPlayerNoStrength = 111,
        /// <summary>
        /// 这些物品不能出售
        /// </summary>
        ItemsNotSell = 112,

        /// <summary>
        /// 请先移除球员穿戴的装备
        /// </summary>
        PlayerHaveEquipment = 113,
        /// <summary>
        /// 保护膜不存在
        /// </summary>
        ProtectItemNot = 148,
        /// <summary>
        /// 该物品不能出售
        /// </summary>
        ItemNotSell = 149,
        /// <summary>
        /// 背包已满.
        /// </summary>
        ItemPackageFull = 150,
        /// <summary>
        /// 物品不存在.
        /// </summary>
        ItemNotExists = 151,
        /// <summary>
        /// 操作失败,物品处于锁定状态.
        /// </summary>
        ItemIsLocked = 152,
        /// <summary>
        /// 操作失败,物品处于绑定状态.
        /// </summary>
        ItemIsBinding = 153,
        /// <summary>
        /// 装备不存在.
        /// </summary>
        ItemEquipmentNotExists = 154,
        /// <summary>
        /// 物品id重复.
        /// </summary>
        ItemIdRepeat = 155,
        /// <summary>
        /// 操作失败,物品属性为null.
        /// </summary>
        ItemPropertyIsNull = 156,
        /// <summary>
        /// 这些物品不能强化
        /// </summary>
        ItemStrengthInvalid = 157,
        /// <summary>
        /// 这些物品不能强化
        /// </summary>
        ItemStrengthNoConfig = 158,
        /// <summary>
        /// 百搭卡级别不足,无法强化,请使用更强力的百搭卡
        /// </summary>
        ItemStrengthProtectShortage = 159,
        /// <summary>
        /// 这些物品不能合成
        /// </summary>
        ItemSynthesisNoConfig = 160,
        /// <summary>
        /// 必须使用5张相同颜色的球员卡才可进行合成
        /// </summary>
        ItemSynthesisDiffCardlevel = 161,
        /// <summary>
        /// 操作失败,物品数量不足
        /// </summary>
        ItemCountInvalid = 162,
        /// <summary>
        /// 该物品无法分解
        /// </summary>
        ItemDecomposeInvalid = 163,
        /// <summary>
        /// 该物品无法分解
        /// </summary>
        ItemDecomposeNoConfig = 164,
        /// <summary>
        /// 这些物品不能合成
        /// </summary>
        ItemEquipmentSynthesisNoConfig = 165,
        /// <summary>
        /// 史诗装备不能合成
        /// </summary>
        ItemEquipmentSynthesisQuality1 = 166,
        /// <summary>
        /// 装备合成卡最多只能使用3张
        /// </summary>
        ItemEquipmentSynthesisCardOver = 167,
        /// <summary>
        /// 合成材料与图纸不匹配
        /// </summary>
        ItemEquipmentSynthesisFormulaInvalid = 168,
        /// <summary>
        /// 不存在的合成配方
        /// </summary>
        ItemEquipmentSynthesisNoSuitdrawing = 169,
        /// <summary>
        /// 背包已满,物品已发送至邮箱
        /// </summary>
        ItemPackageFullToMail = 170,
        /// <summary>
        /// 操作失败,Code：171
        /// </summary>
        ItemNoShadow = 171,
        /// <summary>
        /// 操作失败,物品堆叠数量超出
        /// </summary>
        ItemLapOver = 172,
        /// <summary>
        /// 插槽颜色和球魂颜色不匹配
        /// </summary>
        ItemMosaicColorNotMatch = 173,
        /// <summary>
        /// 该洗炼石不能用于洗炼当前品质的装备
        /// </summary>
        ItemWashStoneNotMatch = 174,
        /// <summary>
        /// 该球魂已经是最高等级了,无法合成
        /// </summary>
        ItemBallsoulLevelOver = 175,
        /// <summary>
        /// 该洗炼石已经是最高等级了,无法合成
        /// </summary>
        ItemWashStoneLevelOver = 176,
        /// <summary>
        /// 拆分数量过大,无法完成拆分
        /// </summary>
        ItemSplitCountOver = 177,
        /// <summary>
        /// 幸运符参数不正确
        /// </summary>
        ItemLuckyParameterError = 178,
        /// <summary>
        /// 一次合成最多只限同时使用3个合成卡
        /// </summary>
        ItemSynthesisWildcardCoutOver = 179,
        /// <summary>
        /// 合成卡数量不足,请检查
        /// </summary>
        ItemSynthesisMallCountShortage = 180,
        /// <summary>
        /// 合成卡与装备品质必须匹配
        /// </summary>
        ItemSynthesisWildcardQualityError = 181,
        /// <summary>
        /// 新手礼包不能删除
        /// </summary>
        ItemNewplayerPackCantDelete = 182,

        /// <summary>
        /// 复制卡类型不匹配
        /// </summary>
        ItemCopyCardTypeError = 183,

        /// <summary>
        /// 球员卡已被复制过
        /// </summary>
        ItemIsCopyedCard = 184,

        /// <summary>
        /// 进阶石数量不足
        /// </summary>
        ItemUpgradeItemCountShortage = 185,

        /// <summary>
        /// 装备等级已满
        /// </summary>
        ItemEquipmentLeverError = 186,

        /// <summary>
        /// 没有可精铸的属性
        /// </summary>
        NonePrecisionCastingProperty = 187,

        /// <summary>
        /// 锁定数量不可大于当前属性个数，且最多可锁定2个属性
        /// </summary>
        PrecisionCastingMaxChoice = 188,

        /// <summary>
        /// 请选择金币或钻石精铸
        /// </summary>
        PrecisionCastingMethod = 189,

        /// <summary>
        /// 横扫券数量不足
        /// </summary>
        HookItemCountShortage = 190,

        /// <summary>
        /// 巨星卡无需选中，每关随机掉落
        /// </summary>
        NoneEliteHookSuperPrize = 191,
        /// <summary>
        /// 物品等级超过上限
        /// </summary>
        ItemLevelOver = 192,
        /// <summary>
        /// 星辰元素数量不足
        /// </summary>
        ItemCountNot = 193,
        /// <summary>
        /// 星座宇宙数量不足
        /// </summary>
        ItemCountNot1 = 194,

        /// <summary>
        /// 该物品无法兑换成勋章
        /// </summary>
        ItemExchangeMedalNoconfig = 195,
        /// <summary>
        /// 当前没有可兑换的球员
        /// </summary>
        NoItemExchangeMedal = 196,
        /// <summary>
        /// 勋章兑换的合同页不存在
        /// </summary>
        MedalExchangeItemNoconfig = 197,
        /// <summary>
        /// 勋章不足
        /// </summary>
        MedalShortage = 198,
        /// <summary>
        /// 阵容上没有装备该球员卡
        /// </summary>
        TeammemberNotPlary = 199,

        #endregion

        #region Ladder相关 (200-249) Ladder开头
        /// <summary>
        /// 您正参加上一轮天梯赛,请耐心等待比赛结束.
        /// </summary>
        LadderBusy = 200,

        /// <summary>
        /// 正在进行比赛分组,请耐心等待.
        /// </summary>
        LadderGrouping = 201,

        /// <summary>
        /// 您报名的天梯赛已进入比赛序列.
        /// </summary>
        LadderCountdown = 202,

        /// <summary>
        /// 天梯赛在01:00--08:00间关闭,请好好休息!
        /// </summary>
        LadderClose = 203,

        /// <summary>
        /// 比赛不存在.
        /// </summary>
        LadderMatchNotExists = 204,

        /// <summary>
        /// 您的积分未达到兑换要求
        /// </summary>
        LadderExchangeScoreShortage = 205,
        /// <summary>
        /// 您的荣誉值不足,无法兑换
        /// </summary>
        LadderExchangeHonorShortage = 206,
        /// <summary>
        /// 当天的赛季信息不存在
        /// </summary>
        LadderNoSeason = 207,
        /// <summary>
        /// 赛季刚开始，无需发奖
        /// </summary>
        LadderSeasonDonotNeedSend = 208,
        /// <summary>
        /// 今日场次已用完，请购买更多场次
        /// </summary>
        LadderStaminaShortage = 209,

        /// <summary>
        /// 该物品兑换次数已用完
        /// </summary>
        LadderExchangeTimesOver = 210,

        /// <summary>
        /// 对不起，您还在CD时间中...
        /// </summary>
        LadderMatchCding = 211,

        /// <summary>
        /// 您好，您的CD时间已到，可以直接比赛了
        /// </summary>
        LadderCdEnd = 212,
        /// <summary>
        /// 您的天梯币不足,无法兑换
        /// </summary>
        LadderExchangeLadderCoinShortage = 213,
        #endregion

        #region 巡回赛相关 (300-349) Tour开头

        /// <summary>
        /// 您今天已挑战过该关卡,请尝试挑战其他关卡.
        /// </summary>
        TourEliteCountLimit = 300,

        /// <summary>
        /// 您今天的重置次数已用完,请明日再来.
        /// </summary>
        TourEliteResetCountLimit = 301,

        /// <summary>
        /// 挂机已结束.
        /// </summary>
        TourHookOver = 302,

        /// <summary>
        /// 关卡未开通
        /// </summary>
        TourStageNotEnable = 303,

        /// <summary>
        /// 不能重复抽奖
        /// </summary>
        TourLotteryRepeat = 304,

        /// <summary>
        /// 关卡不存在
        /// </summary>
        TourStageNotExists = 305,
        /// <summary>
        /// 只有已经通关的关卡才能扫荡
        /// </summary>
        TourStageNotPass = 306,

        /// <summary>
        /// 操作失败,您的球队已经进入扫荡队列,请先终止扫荡
        /// </summary>
        TourHookBusy = 307,
        /// <summary>
        /// 你没有足够体力进行比赛,请减少扫荡次数.
        /// </summary>
        TourHookFightTimesOver = 308,

        /// <summary>
        /// 本次挂机已经结束
        /// </summary>
        TourHookHasFinish = 309,

        /// <summary>
        /// 比赛记录不存在,请刷新.
        /// </summary>
        TourHookMatchNotExists = 310,
        /// <summary>
        /// 您有未抽卡的比赛,请先抽卡
        /// </summary>
        TourHasLottery = 311,
        /// <summary>
        /// 您的球员正在参加前一场比赛,请稍后再来
        /// </summary>
        TourMatchWait = 312,
        /// <summary>
        /// 比赛中,请等待
        /// </summary>
        TourMatchLoop = 313,
        /// <summary>
        /// 没有可领取的奖励
        /// </summary>
        TourNoPassPrize = 314,
        /// <summary>
        /// 该奖励已领取过,请勿重复领取.
        /// </summary>
        TourPassPrizeHasReceive = 315,
        #endregion

        #region 球员相关,包括球员成长 (350-399) Teammember开头

        /// <summary>
        /// 成长已达到上限.
        /// </summary>
        TeammemberGrowMax = 350,

        /// <summary>
        /// 成长值已达到当前阶段突破要求,可以进行突破了.
        /// </summary>
        TeammemberGrowUp = 351,

        /// <summary>
        /// 成长值不足.
        /// </summary>
        TeammemberGrowShortage = 352,

        /// <summary>
        /// 灵气不足.
        /// </summary>
        TeammemberReikiShortage = 353,

        /// <summary>
        /// 阅历不足.
        /// </summary>
        TeammemberSophisticateShortage = 354,

        /// <summary>
        /// 该球员已存在,不能转化重复的球员.
        /// </summary>
        TeammemberRepeat = 355,

        /// <summary>
        /// 球员数量已达到上限.
        /// </summary>
        TeammemberOver = 356,

        /// <summary>
        /// 阵容中球员数量不匹配.
        /// </summary>
        TeammemberInvalidCount = 357,

        /// <summary>
        /// 阵容中有重复球员.
        /// </summary>
        TeammemberSolutionPlayerRepeat = 358,

        /// <summary>
        /// 阵容中有球员不存在.
        /// </summary>
        TeammemberInvalidPlayer = 359,

        /// <summary>
        /// 不能解雇阵容中的球员,请将该球员移除阵容,再解雇.
        /// </summary>
        TeammemberIsMain = 360,

        /// <summary>
        /// 该球员身上有装备,无法解雇(请先卸下).
        /// </summary>
        TeammemberHasEquip = 361,

        /// <summary>
        /// 球员卡和该球员id不匹配.
        /// </summary>
        TeammemberCardNotMatch = 362,

        /// <summary>
        /// 阵型等级已达最高
        /// </summary>
        TeammemberFormationLevelMax = 363,

        /// <summary>
        /// 阵型等级不能超过经理等级
        /// </summary>
        TeammemberFormationLevelOver = 364,
        /// <summary>
        /// 球员等级不能超过经理等级,请升级经理等级后再来训练
        /// </summary>
        TeammemberTrainLevelOver = 365,
        /// <summary>
        /// 训练位已满
        /// </summary>
        TeammemberTrainSeatOver = 366,
        /// <summary>
        /// 球员已经在训练
        /// </summary>
        TeammemberTraining = 367,
        /// <summary>
        /// 该球员不在训练队列
        /// </summary>
        TeammemberTrainNotin = 368,
        /// <summary>
        /// 体力不足,无法训练
        /// </summary>
        TeammemberTrainNoStamina = 369,
        /// <summary>
        /// 该球员训练已结束
        /// </summary>
        TeammemberTrainFinish = 370,
        /// <summary>
        /// 该球员没有可加的属性点
        /// </summary>
        TeammemberPropertyShortage = 371,
        /// <summary>
        /// 该属性以达上限,球员成长可提升上限
        /// </summary>
        TeammemberPropertyOver = 372,
        /// <summary>
        /// 主力球员中,元老数量超上限
        /// </summary>
        TeammemberVeteranCountOver = 373,
        /// <summary>
        /// 选定的球员不存在
        /// </summary>
        TeammemberNotExists = 374,
        /// <summary>
        /// 选定的球员颜色不符
        /// </summary>
        TeammemberCardLevelOver = 375,
        /// <summary>
        /// 选定的球员能力值超出
        /// </summary>
        TeammemberPowerOver = 376,
        /// <summary>
        /// 该球员没有加过属性点,不需要重置
        /// </summary>
        TeammemberUsedPropertyShortage = 377,

        /// <summary>
        /// 训练中的球员不能进行传承
        /// </summary>
        TeammemberTrainingNoInherit = 378,

        /// <summary>
        ///球员未达到可觉醒等级 
        /// </summary>
        TeammemberArousalShortage = 379,

        /// <summary>
        /// 已达到觉醒最高级
        /// </summary>
        TeammemberArousalMax = 380,
        /// <summary>
        /// 经验药水数量不足
        /// </summary>
        SpeedUpItemCodeNumber = 381,
        /// <summary>
        /// 球员不是主力
        /// </summary>
        TeammemberNotMain = 382,
        /// <summary>
        /// 主力球员不可分解
        /// </summary>
        TeammemberCantDecompose = 383,

        /// <summary>
        /// 该球员已经经验已经不能增加了
        /// </summary>
        TeammemberTrainLevelNoAddExp = 384,
        /// <summary>
        /// 主力球员不可出售
        /// </summary>
        TeammemberCantSell = 385,

        /// <summary>
        /// 训练中的球员不可出售
        /// </summary>
        TeammemberTrainSell = 386,
        /// <summary>
        /// 训练中的球员不可设置意志
        /// </summary>
        TeammemberTrainSkill = 387,

        #endregion

        #region Mall相关 400-449
        /// <summary>
        /// 购买数量超过上限
        /// </summary>
        MallBuyCountLimit = 400,
        /// <summary>
        /// 今日购买体力次数已达上限,提升Vip等级可购买更多体力
        /// </summary>
        MallAddStaminaLimit = 401,
        /// <summary>
        /// 训练位数量已达上限,提升Vip等级将开放更多训练位
        /// </summary>
        MallAddTrainSeatLimit = 402,
        /// <summary>
        /// 背包扩展次数已达上限
        /// </summary>
        MallBuyPackageCountLimit = 403,
        /// <summary>
        /// 今日重置精英巡回赛次数已达上限,提升Vip等级可获得更多重置次数
        /// </summary>
        MallResetEliteLimit = 404,
        /// <summary>
        /// 不存在这样的商品编码
        /// </summary>
        MallItemNotExists = 405,
        /// <summary>
        /// 体力还是满的,不需要添加
        /// </summary>
        MallStaminaOver = 406,
        /// <summary>
        /// 确定要花费{0}钻石,购买{1}点体力吗?
        /// </summary>
        MallBuyStaminaCheck = 407,
        /// <summary>
        /// 确定要花费{0}钻石,购买{1}格背包吗?
        /// </summary>
        MallBuyPackageCheck = 408,
        /// <summary>
        /// 确定要花费{0}钻石,购买{1}个训练位吗?
        /// </summary>
        MallBuyTrainSeatCheck = 409,
        /// <summary>
        /// 确定要花费{0}钻石,重置精英巡回赛吗?
        /// </summary>
        MallResetEliteCheck = 410,
        /// <summary>
        /// 确定要花费{0}钻石,完成该球员的训练吗?
        /// </summary>
        MallQuickenTrainCheck = 411,
        /// <summary>
        /// 该物品未上架,不能购买
        /// </summary>
        MallItemInvalidBuy = 412,
        /// <summary>
        /// 确定要花费{0}钻石,购买{1}个替补席吗?
        /// </summary>
        MallBuySubstitute = 413,
        /// <summary>
        /// 使用失败,您的账号已经使用过这种新手礼包,不能再用
        /// </summary>
        MallUsedNewPlayerPack = 414,
        /// <summary>
        /// 没有球员处于休息状态,不需要恢复训练体力
        /// </summary>
        MallUsedNoRestTeammember = 415,
        /// <summary>
        /// 等级不足,无法使用该道具
        /// </summary>
        MallUsedLevelShortage = 416,

        /// <summary>
        /// 确定要花费{0}金币,购买{1}次pk机会吗?
        /// </summary>
        MallAddPkCount = 417,
        /// <summary>
        /// 购买失败,请重试
        /// </summary>
        MallItemBuyFail = 418,
        /// <summary>
        /// 已过刷新时间，请刷新后再购买
        /// </summary>
        GoldmallExpired = 419,
        /// <summary>
        /// 该物品购买次数已超过上限
        /// </summary>
        GoldmallBuyMax = 420,

        /// <summary>
        /// 球员成长等级不足
        /// </summary>
        TeammemberGrowLevelShortage = 421,
        /// <summary>
        /// 当前时间不能领取体力
        /// </summary>
        MallStaminaGiftNoTime = 422,
        /// <summary>
        /// 下单失败，请稍后再试
        /// </summary>
        BuyPointFail = 423,
        /// <summary>
        /// 下单失败，请稍后再试
        /// </summary>
        TxBuyPointFail = 424,
        /// <summary>
        /// 对不起,您不是首次充值，不能享受该活动
        /// </summary>
        FirstChargeNot = 425,
        /// <summary>
        /// 今日次数已经用完
        /// </summary>
        DayNumberNot=426,
        #endregion

        #region Dailycup 450-499
        /// <summary>
        /// 今日无杯赛
        /// </summary>
        DailycupNoMatch = 450,

        /// <summary>
        /// 明日无杯赛
        /// </summary>
        DailycupNoMatchTomorrow = 451,

        /// <summary>
        /// 你已经报过名了,请勿重复报名
        /// </summary>
        DailycupAttendRepeat = 452,

        /// <summary>
        /// 报名失败,该杯赛报名已结束
        /// </summary>
        DailycupTimeOut = 453,

        /// <summary>
        /// 系统繁忙,请稍后再试
        /// </summary>
        DailycupInsCompetitorFail = 454,

        /// <summary>
        /// 抱歉,押注已过期！
        /// </summary>
        DailycupGambleClosed = 455,

        /// <summary>
        /// 您最多只能押注{0}次,提升Vip等级,可增加次数！
        /// </summary>
        DailycupGamebleCountLimit = 456,

        /// <summary>
        /// 您只能押注{0}钻石，提升Vip等级,可提高押注级别！
        /// </summary>
        DailycupGamblePointLimit = 457,

        /// <summary>
        /// 该球员卡强化级别超过{0},提升Vip等级,可提高押注级别！
        /// </summary>
        DailycupGambleStrengthenLimit = 458,

        /// <summary>
        /// 该球员卡无法押注,等级不符
        /// </summary>
        DailycupgGambleCardlevelOver = 459,
        /// <summary>
        /// 今日无杯赛,您可以报名参加明日杯赛
        /// </summary>
        DailycupNotExists = 460,
        /// <summary>
        /// 杯赛状态报名未截止,无法开始
        /// </summary>
        DailycupStatusNotClose = 461,
        /// <summary>
        /// 杯赛状态,比赛未结束,无法开奖
        /// </summary>
        DailycupStatusNotEnd = 462,
        /// <summary>
        /// 该杯赛开奖已经完成
        /// </summary>
        DailycupGambleOpened = 463,
        /// <summary>
        /// 该杯赛没有选手
        /// </summary>
        DailycupNoCompetitors = 464,
        /// <summary>
        /// 该杯赛发奖时间还没到
        /// </summary>
        DailycupNotimetoSendPrize = 465,
        /// <summary>
        /// 该杯赛运行时间还没到
        /// </summary>
        DailycupNottimetoRun = 466,
        /// <summary>
        /// 该杯赛已经运行
        /// </summary>
        DailycupStatusNotOpen = 467,


        #endregion

        #region 球探 500-549
        /// <summary>
        /// 抽卡失败,请稍后再试
        /// </summary>
        ScoutingLotteryFail = 500,

        /// <summary>
        /// 无法开启金币10连抽,请提升Vip等级,获取Vip权限
        /// </summary>
        ScoutingCoinTenVipLevelShortage = 501,
        /// <summary>
        /// 金条数量不足
        /// </summary>
        ScoutingGoldBarNot=502,
        #endregion

        #region 联赛 550-569
        /// <summary>
        /// 抱歉,当前联赛还未解锁
        /// </summary>
        LeagueIdMarkNotLock = 550,
        /// <summary>
        /// 请先通关您正在战斗的联赛，或放弃那场联赛
        /// </summary>
        LeagueHasStart = 551,
        /// <summary>
        /// 对不起，本场比赛已经重赛过
        /// </summary>
        LeagueMatchHasReMatched = 552,
        /// <summary>
        /// 本赛季还未结束
        /// </summary>
        LeagueNoEnd = 553,
        /// <summary>
        /// 本联赛冠军奖励已领取
        /// </summary>
        LeaguePrizeReceivedToday = 554,
        /// <summary>
        /// 您不是本赛季冠军，无法领取奖励
        /// </summary>
        LeagueNotChampion = 555,
        /// <summary>
        /// 本场比赛已经打过了
        /// </summary>
        LeagueMatchHasFight = 556,
        /// <summary>
        /// 本轮比赛还未开始，请先进行其他轮比赛
        /// </summary>
        LeagueMatchWheelNotRight = 557,
        /// <summary>
        /// 本轮比赛已确认过
        /// </summary>
        LeagueMatchConfirmed = 558,
        /// <summary>
        /// 体力不足,无法进行比赛
        /// </summary>
        LeagueStaminaNotEnough = 559,
        /// <summary>
        /// 该物品兑换次数已用完
        /// </summary>
        LeagueExchangeTimesOver = 560,
        /// <summary>
        /// 您的联赛积分不足,无法兑换
        /// </summary>
        LeagueExchangeHonorShortage = 561,
        /// <summary>
        /// 该奖励条件未达成
        /// </summary>
        LeagueWincountPrizeCannotReceive = 562,
        /// <summary>
        ///该奖励已领取
        /// </summary>
        LeagueWincountPrizeReceived = 563,
        /// <summary>
        /// 您已经通关，请返回主页面后重试
        /// </summary>
        LeagueHavePass = 564,
        /// <summary>
        /// 对不起，比赛还未确认
        /// </summary>
        LeagueMatchNotConfirmed = 565,
        /// <summary>
        /// 您还未开启该联赛
        /// </summary>
        LeagueNotStart = 566,
        /// <summary>
        /// 您还有奖励没有领取，先领取奖励吧
        /// </summary>
        LeaguePrizeNotGet = 567,
        #endregion

        #region Task相关 600-649
        /// <summary>
        /// 未找到该任务配置,请稍后重试
        /// </summary>
        TaskNoConfig = 600,
        /// <summary>
        /// 任务尚未完成,无法提交
        /// </summary>
        TaskSubmitInvalid = 601,
        /// <summary>
        /// 当前没有每日任务
        /// </summary>
        TaskNoDaily = 602,
        /// <summary>
        /// 您的等级太低,等级达到15级后才开放每日任务
        /// </summary>
        TaskDailyLevelShortage = 603,
        /// <summary>
        /// 您今日任务次数已用完,请明日再来
        /// </summary>
        TaskDailyCountOver = 604,
        #endregion

        #region Mail相关 650-699
        /// <summary>
        /// 该邮件不含附件
        /// </summary>
        MailNoAttachment = 650,
        /// <summary>
        /// 请勿重复领取
        /// </summary>
        MailAttachmentReceiveRepeat = 651,
        /// <summary>
        /// 已经没有包含附件的邮件了
        /// </summary>
        MailNoAttachmentBatch = 652,
        #endregion

        #region Gamble 721-749
        /// <summary>
        /// 竞猜主题不存在
        /// </summary>
        GambleTitleNoExist = 721,
        /// <summary>
        /// 该竞猜主题只能官方发布
        /// </summary>
        ThisGambleTitleIsOfficial = 722,
        /// <summary>
        /// 竞猜系统错误,请联赛游戏管理员
        /// </summary>
        GambleParamError = 723,
        /// <summary>
        /// 竞猜奖池金额不足,最低5000钻石
        /// </summary>
        GambleNotEnoughMoney = 724,
        /// <summary>
        /// 发起竞猜人数已满
        /// </summary>
        GambleHostsIsFull = 725,
        /// <summary>
        /// 钻石不足,充点小钱玩玩
        /// </summary>
        GamblePayError = 726,
        /// <summary>
        /// 该竞猜还没有开始
        /// </summary>
        GambleNotStart = 727,
        /// <summary>
        /// 该竞猜已经结束
        /// </summary>
        GambleStoped = 728,
        /// <summary>
        /// 您没有发起该主题的竞猜活动
        /// </summary>
        GambleUareNotHost = 729,
        /// <summary>
        /// 增加奖金失败
        /// </summary>
        GambleAddMoneyError = 730,
        /// <summary>
        /// 竞猜金额不足
        /// </summary>
        GambleTooPoor = 731,
        /// <summary>
        /// 奖池钻石不足,不能兑现您的竞猜
        /// </summary>
        GambleNeedMoreTotalMoney = 732,
        /// <summary>
        /// 太多的人在参与本场竞猜,请稍后重试
        /// </summary>
        GambleTooManyPeopleIsGambling = 733,
        /// <summary>
        /// 您只能发起一次关于本主题的竞猜
        /// </summary>
        GambleOnlyOnce = 734,
        /// <summary>
        /// 服务器在打酱油了,请稍后重试
        /// </summary>
        GambleHostRateInsertError = 735,
        /// <summary>
        /// 您是本竞猜的发起人,不能投注
        /// </summary>
        GambleCannotGambleSelf = 736,
        #endregion

        #region Chat 750-799
        /// <summary>
        /// 消息不能为空
        /// </summary>
        ChatMsgIsEmpty = 750,

        /// <summary>
        /// 消息长度不能超过140个字符
        /// </summary>
        ChatMsgLengthRange = 751,
        /// <summary>
        /// 你已被禁言
        /// </summary>
        HaveBannedToPost = 752,
        #endregion

        #region Friend 850-899
        /// <summary>
        /// 该经理不存在,请检查
        /// </summary>
        FriendNotExistsName = 850,
        /// <summary>
        /// 该经理已经是您的好友了,不能重复添加
        /// </summary>
        FriendHasExists = 851,
        /// <summary>
        /// 该经理将您列为黑名单,您不能添加其为好友
        /// </summary>
        FriendIsByBlack = 852,
        /// <summary>
        /// 您的好友数量已达到上限
        /// </summary>
        FriendCountOver = 853,
        /// <summary>
        /// 不能添加自己
        /// </summary>
        FriendNotSelf = 854,
        /// <summary>
        /// 您今日帮助次数已用完,提升Vip等级可增加次数
        /// </summary>
        FriendHelpTrainOver = 855,
        /// <summary>
        /// 您的好友今日被帮助次数已用完,提升Vip等级可增加次数
        /// </summary>
        FriendByHelpTrainOver = 856,
        /// <summary>
        /// 您已经帮助他很多次了,别的好友也需要您的帮助
        /// </summary>
        FriendDayHelpOver = 857,
        /// <summary>
        /// 您的球员正在参加前一场友谊赛,请稍后再来
        /// </summary>
        FriendMatchWait = 858,
        /// <summary>
        /// 该经理已经在您的黑名单里了,不能重复添加
        /// </summary>
        FriendBlackExists = 859,
        /// <summary>
        /// 邀请人数不够
        /// </summary>
        FriendInviteNotCount = 860,
        /// <summary>
        /// 友情点不足
        /// </summary>
        FriendshipPointShortage = 861,
        /// <summary>
        /// 您今日已挑战此好友三次,请明天再来。
        /// </summary>
        FriendMatchOver = 862,
        /// <summary>
        /// 您今日已开启过该好友的宝箱
        /// </summary>
        FriendBoxHasOpen = 863,
        /// <summary>
        /// 您今日已开启好友宝箱10次,请明天再来。
        /// </summary>
        FriendBoxCountOver = 864,

        #endregion

        #region Activity 900-949
        /// <summary>
        /// 没有可领取的奖励
        /// </summary>
        ActivityNoPrize = 900,
        /// <summary>
        /// 该奖励已领取过,请勿重复领取.
        /// </summary>
        ActivityHasReceive = 901,
        /// <summary>
        /// 未找到对应的奖励配置
        /// </summary>
        ActivityNoConfigPrize = 902,
        /// <summary>
        /// 活动状态不为待发送
        /// </summary>
        ActivityStatusNotSend = 903,
        /// <summary>
        /// 配置的奖励为空 
        /// </summary>
        ActivityConfigPrizeIsNull = 904,
        /// <summary>
        /// 领取失败
        /// </summary>
        ActivityReceiveFail = 905,
        /// <summary>
        /// 发送作业已经在运行
        /// </summary>
        ActivityPrizeJobSending = 906,
        /// <summary>
        /// 该档已经进行过投资
        /// </summary>
        InvestDeposited = 907,

        /// <summary>
        /// 请选择一个物品领取奖励
        /// </summary>
        UserNotItemCode = 908,
        #endregion

        #region Exchange 950-999
        /// <summary>
        /// 兑换码不存在！
        /// </summary>
        ExchangeCodeNotExists = 950,
        /// <summary>
        /// 该兑换码已被使用,请勿重复领取
        /// </summary>
        ExchangeIsUsed = 951,
        /// <summary>
        /// 您已领取过该批次的礼包！
        /// </summary>
        ExchangeBatchLimit = 952,
        #endregion

        #region PlayerKill 1000-1049
        /// <summary>
        /// 您的球员正在参加前一场Pk赛,请稍后再来
        /// </summary>
        PlayerKillMatchWait = 1000,
        /// <summary>
        /// 您今天挑战次数已用完,请购买更多次数
        /// </summary>
        PlayerKillTimesOver = 1001,
        /// <summary>
        /// 对方今天已收到太多人的挑战,让他喘口气吧!
        /// </summary>
        PlayerKillByTimesOver = 1002,
        /// <summary>
        /// 不能重复复仇
        /// </summary>
        PlayerKillRevenged = 1003,
        /// <summary>
        /// 不能挑战自己
        /// </summary>
        PlayerKillNoSelf = 1004,
        /// <summary>
        /// 不能挑战低于9级的玩家
        /// </summary>
        PlayerKillMinLevel = 1005,

        /// <summary>
        /// 今日购买次数已达到上限
        /// </summary>
        PlayerKillBuyTimesOver = 1006,

        /// <summary>
        /// 您已战胜过他，不能再挑战他了
        /// </summary>
        PlayerKillWinOver = 1007,
        /// <summary>
        /// 找不到对手信息
        /// </summary>
        PlayerKillNoAway = 1008,
        #endregion

        #region Crowd 1050-1099
        /// <summary>
        /// 上一次活动还未结束
        /// </summary>
        CrowdNotEnding = 1051,
        /// <summary>
        /// 活动时间未到或已结束
        /// </summary>
        CrowdNoData = 1052,

        /// <summary>
        /// 您正参加上一轮比赛,请耐心等待比赛结束
        /// </summary>
        CrowdBusy = 1053,
        /// <summary>
        /// CD中，请耐心等待
        /// </summary>
        CrowdHasCd = 1054,
        /// <summary>
        /// 士气值已用完，请复活后再来
        /// </summary>
        CrowdNoMorale = 1055,
        /// <summary>
        /// 士气值还没用完呢，不需要复活
        /// </summary>
        CrowdHasMorale = 1056,
        /// <summary>
        /// 活动还未开始
        /// </summary>
        CrowdNotStart = 1057,
        #endregion

        #region 技能卡2000-2200
        /// <summary>
        /// 钻石不足,充点小钱玩玩
        /// </summary>
        LackofGold = 2000,
        /// <summary>
        /// 金币不足
        /// </summary>
        LackofCoin = 2001,
        /// <summary>
        /// 当前经理等级不够
        /// </summary>
        LackofManagerLevel = 2021,
        /// <summary>
        /// 当前Vip等级不够
        /// </summary>
        LackofVipLevel = 2022,
        /// <summary>
        /// 钻石支付失败
        /// </summary>
        FaillCostGold = 2030,
        /// <summary>
        /// 金币更新失败
        /// </summary>
        FailCostCoin = 2031,
        /// <summary>
        /// 数据更新失败
        /// </summary>
        FailUpdate = 2060,
        /// <summary>
        /// 无效参数
        /// </summary>
        InvalidArgs = 2061,
        /// <summary>
        /// 未找到经理
        /// </summary>
        MissManager = 2062,

        /// <summary>
        /// 非法操作
        /// </summary>
        InvalidOp = 2063,
        /// <summary>
        /// 未找到经理
        /// </summary>
        MissManagers = 2064,
        /// <summary>
        /// 无效跨服参数
        /// </summary>
        InvalidCrossConfig = 2080,
        /// <summary>
        /// 无效参数
        /// </summary>
        SkillInvalidCid = 2100,
        /// <summary>
        /// 未找到技能卡配置
        /// </summary>
        SkillMissConfig = 2101,
        /// <summary>
        /// 未找到技能经验卡
        /// </summary>
        SkillExpNotFind = 2102,
        /// <summary>
        /// 该技能还未开启
        /// </summary>
        SkillMissCard = 2103,
        /// <summary>
        /// 技能经验卡不足
        /// </summary>
        SkillExpNotEnough = 2111,
        /// <summary>
        /// 技能已达当前最高等级
        /// </summary>
        SkillMixOverCardLevel = 2112,
        /// <summary>
        /// 技能背包中已没有该技能的卡牌
        /// </summary>
        SkillMixMissSameCard = 2113,
        /// <summary>
        /// 无效参数
        /// </summary>
        SkillAskInvalidAskno = 2121,
        /// <summary>
        /// 未找到教练
        /// </summary>
        SkillAskMissNpc = 2122,
        /// <summary>
        /// 技能临时背包已满,请拾取
        /// </summary>
        SkillAskLackofCells = 2123,
        /// <summary>
        /// 金币不足了
        /// </summary>
        SkillAskLackofCoin = 2124,
        /// <summary>
        /// 该级别的学习已开放,请刷新
        /// </summary>
        SkillAskExistsNpc = 2125,
        /// <summary>
        /// 技能背包里的卡牌太多啦,请合并下吧
        /// </summary>
        SkillPickLackofCells = 2131,
        /// <summary>
        /// 技能临时背包已空
        /// </summary>
        SkillPickMissCardList = 2132,
        /// <summary>
        /// 无效参数
        /// </summary>
        SkillSetInvalidArgs = 2141,
        /// <summary>
        /// 可装备技能已达当前上限
        /// </summary>
        SkillSetLackofCells = 2142,
        /// <summary>
        /// 不可重复装备相同技能
        /// </summary>
        SkillSetLimitRepeat = 2143,
        #endregion

        #region 经理技能2200-2300
        /// <summary>
        /// 未找到天赋配置
        /// </summary>
        TalentMissConfig = 2201,
        /// <summary>
        /// 未找到意志配置
        /// </summary>
        WillMissConfig = 2202,
        /// <summary>
        /// 当前经理等级不够
        /// </summary>
        TalentHitLackofManagerLevel = 2211,
        /// <summary>
        /// 该天赋已激活
        /// </summary>
        TalentHitExisits = 2212,
        /// <summary>
        /// 当前等级阶段已选取了天赋
        /// </summary>
        TalentHitLimitStep = 2213,
        /// <summary>
        /// 天赋点不足
        /// </summary>
        TalentHitLimitNumber = 2214,
        /// <summary>
        /// 未找到已激活的天赋
        /// </summary>
        TalentSetMiss = 2221,
        /// <summary>
        /// 不可装备被动天赋
        /// </summary>
        TalentSetUnable = 2222,
        /// <summary>
        /// 不可重复装备相同天赋
        /// </summary>
        TalentSetLimitRepeat = 2223,
        /// <summary>
        /// 天赋已重置
        /// </summary>
        TalentResetMiss = 2231,
        /// <summary>
        /// 该意志已收集完成
        /// </summary>
        WillPutExists = 2241,
        /// <summary>
        /// 该球员已收集
        /// </summary>
        WillPutExistsCard = 2242,
        /// <summary>
        /// 未找到球员卡
        /// </summary>
        WillPutMissCard = 2243,
        /// <summary>
        /// 球员卡不匹配
        /// </summary>
        WillPutUnfitCard = 2244,
        /// <summary>
        /// 球员卡等级还不够哦,快去强化吧！
        /// </summary>
        WillPutUnfitCardStrength = 2245,
        /// <summary>
        /// 该意志还未收集完成
        /// </summary>
        WillSetMiss = 2251,
        /// <summary>
        /// 只可启用主动意志
        /// </summary>
        WillSetUnable = 2252,
        /// <summary>
        /// 可启用主动意志数量已达上限
        /// </summary>
        WillSetLimitNumber = 2253,
        /// <summary>
        /// 请使用非主力球员
        /// </summary>
        WillCardMain = 2254,
        /// <summary>
        /// 天赋类型不匹配
        /// </summary>
        MSkillTypeNotMatching = 2255,
        /// <summary>
        /// 该天赋已达到可加的最大点数,无法再增加
        /// </summary>
        MSkillMaxPoint = 2256,

        /// <summary>
        /// 前置天赋加点不足
        /// </summary>
        MSkillNotRequireSeries = 2257,
        /// <summary>
        /// 请先转职
        /// </summary>
        FirstTransfer = 2258,
        /// <summary>
        /// 已经转职成功，若想重新转职，请重置天赋点
        /// </summary>
        HaveFirstTransfer = 2259,
        #endregion



        #region 点球活动2300-2400
        /// <summary>
        /// 不在活动时间内
        /// </summary>
        AdMissSeason = 2300,
        /// <summary>
        /// 未找到参加活动的经理
        /// </summary>
        AdMissManager = 2301,
        /// <summary>
        /// 游戏币数量不足
        /// </summary>
        GameCurrencyNumberNot = 2302,
        /// <summary>
        /// 上一局游戏还未结束
        /// </summary>
        GameNotEnd = 2303,
        /// <summary>
        /// 本局游戏已经结束
        /// </summary>
        GameEnd = 2304,
        /// <summary>
        /// 兑换的物品不存在
        /// </summary>
        ExChangeItemNot = 2305,
        /// <summary>
        /// 您已经兑换过该奖励了，不能重复兑换
        /// </summary>
        RepeatExChange = 2306,


        #endregion



        #region 充值从5200-5209



        /// <summary>
        /// 充值或者消耗成功
        /// </summary>
        PaySuccess = 5200,

        /// <summary>
        /// 订单号重复
        /// </summary>
        PayOrderIsExist = 5201,

        /// <summary>
        /// 创建账号失败
        /// </summary>
        PayCreateAccountFail = 5202,

        /// <summary>
        /// 更新游戏点数失败
        /// </summary>
        PayUpdatePointFail = 5203,

        /// <summary>
        /// 插入充值历史失败
        /// </summary>
        PayInsertPayHistoryFail = 5204,

        /// <summary>
        /// 还未建立充值账号
        /// </summary>
        PayNoPayAccount = 5205,

        /// <summary>
        /// 没有足够的点数
        /// </summary>
        PayNoEnoughPoint = 5206,

        /// <summary>
        /// 扣除账号点数失败
        /// </summary>
        PayTakePointFail = 5207,

        /// <summary>
        /// 插入消耗历史失败
        /// </summary>
        PayInsertConsumeHistoryFail = 5208,

        /// <summary>
        /// 同一订单重复消耗
        /// </summary>
        PayDuplicateConsume = 5209,
        /// <summary>
        /// 用户未激活游戏
        /// </summary>
        PayNoActiveGame = 5210,
        /// <summary>
        ///没有找到订单
        /// </summary>
        PayNoOrderId = 5211,
        /// <summary>
        /// 下单失败
        /// </summary>
        PayCreateItemFail = 5212,
        /// <summary>
        /// 支付金额验证失败
        /// </summary>
        PayCashFail = 5213,
        /// <summary>
        /// 余额不足
        /// </summary>
        NotSufficientFunds = 5214,
        /// <summary>
        /// 账户余额不足
        /// </summary>
        TxNotSufficientFunds=5215,

        /// <summary>
        /// 您还没有登录
        /// </summary>
        UserNotLogin=5216,
        /// <summary>
        /// 账户被冻结
        /// </summary>
        UserFreeze=5217,

        /// <summary>
        /// 支付成功但是发货失败，请连续客服人员
        /// </summary>
        PaySuccessAndShipmentsBeDefeated=5218,
        #endregion

        #region 教练相关 从5310-5350

        /// <summary>
        /// 精力不足
        /// </summary>
        VigorInsufficient = 5310,
        /// <summary>
        /// 聘请教练失败
        /// </summary>
        HiringACoachFail = 5311,
        /// <summary>
        /// 未知的教练
        /// </summary>
        UnknownCoach = 5312,
        /// <summary>
        /// 声望不足
        /// </summary>
        PrestigeInsufficient = 5313,
        /// <summary>
        /// 教练达到上限
        /// </summary>
        CoachReachTheLimit = 5314,
        /// <summary>
        /// 已经聘用该教练
        /// </summary>
        AlreadyHaveCoach = 5315,

        /// <summary>
        /// 未聘用该教练
        /// </summary>
        NotToHireCoach = 5316,

        /// <summary>
        /// 跟换教练失败
        /// </summary>
        ChangeCoachFail = 5317,
        /// <summary>
        /// 只有1个教练了不能解聘
        /// </summary>
        OnlyOneCoach = 5318,
        /// <summary>
        /// 解聘教练失败
        /// </summary>
        ExpelCoachFail = 5319,
        /// <summary>
        /// 精力值不够
        /// </summary>
        VigorNotEnough = 5320,
        /// <summary>
        /// 该教练没有技能
        /// </summary>
        ThereIsNoSkills = 5321,
        /// <summary>
        /// 被传承的教练等级大于传承的教练
        /// </summary>
        ByIGreaterThanInherit = 5322,

        /// <summary>
        /// 升级所需精力值不够
        /// </summary>
        UpgradeVigorNotEnough = 5323,
        /// <summary>
        /// 没有正在使用的教练
        /// </summary>
        NotUseCoach = 5324,
        /// <summary>
        /// 正在使用该教练,不能解聘
        /// </summary>
        BeBCoach = 5325,
        /// <summary>
        /// 教练等级不能大于经理等级
        /// </summary>
        CoachLevelNotManager = 5326,
        /// <summary>
        /// 教练碎片不足
        /// </summary>
        CoachDebrisNot = 5327,

        /// <summary>
        /// 教练激活失败，请稍后再试
        /// </summary>
        CoachAlreadyNotActivation=5328,

        /// <summary>
        /// 教练未激活
        /// </summary>
        CoachNotAlready = 5329,

        /// <summary>
        /// 已经达到最高等级
        /// </summary>
        CoachHaveMaxLevel=5330,
        /// <summary>
        /// 没有可分配经验了
        /// </summary>
        CoachExpNot = 5331,
        /// <summary>
        /// 教练星级不够，请升星后再来
        /// </summary>
        CoachStarNot=5332,
        /// <summary>
        /// 教练等级不够，请先提升教练等级后再来
        /// </summary>
        CoachLevelNot= 5333,

        #endregion

        #region 球星启示录相关  5360-5399

        /// <summary>
        /// 未找到关卡
        /// </summary>
        RevelationNotFountCheckoint = 5360,

        /// <summary>
        /// 未找到该经理
        /// </summary>
        RevelationNotFountManager = 5361,
        /// <summary>
        /// 挑战次数用完
        /// </summary>
        RevelationChallengeUseUP = 5362,
        /// <summary>
        /// CD时间未到
        /// </summary>
        RevelationCDTimes = 5363,
        /// <summary>
        /// 不能挑战该关卡
        /// </summary>
        RevelationNoChallenge = 5364,
        /// <summary>
        /// CD时间已过,不需要消除
        /// </summary>
        RevelationNotEliminateCD = 5365,
        /// <summary>
        /// 未通过该关卡
        /// </summary>
        RevelationNotGeneral = 5366,
        /// <summary>
        /// 勇气值不够
        /// </summary>
        RevelationCourageNot = 5367,
        /// <summary>
        /// 已经领取过奖励,不能重复领取
        /// </summary>
        AlreadyPullDown = 5368,
        /// <summary>
        /// 今天本关通关次数已用完
        /// </summary>
        RevelationNotGeneralNums = 5369,
        /// <summary>
        /// 你还不能挑战该关卡
        /// </summary>
        RevelationNotChallengeCheck = 5370,

        /// <summary>
        /// 对不起，您当前关卡还未解锁
        /// </summary>
        RevelationNotLock=5371,
        /// <summary>
        /// 对不起,您还有未结束的关卡
        /// </summary>
        RevelationNotEndMark=5372,
        /// <summary>
        /// 关卡还未开始
        /// </summary>
        RevelationMarkNotStart=5373,
        /// <summary>
        /// 您正在自动挑战，请先停止
        /// </summary>
        RevelationIsHook=5374,
        /// <summary>
        /// 您已经通关了该关卡
        /// </summary>
        RevelationGeneralMark = 5375,
        /// <summary>
        /// 对不起，您的士气不足，请先补充士气
        /// </summary>
        RevelationMorale=5376,
        /// <summary>
        /// 士气不需要补充了
        /// </summary>
        RevelationMoraleFull=5377,
        /// <summary>
        /// 已经在挂机了
        /// </summary>
        RevelationHaveHook=5378,
        #endregion

        #region 活跃度 5541-5545
        /// <summary>
        /// 活跃度不够，不能领取该奖励
        /// </summary>
        ActiveNotReceive = 5541,

        /// <summary>
        /// 不能重复领取
        /// </summary>
        ActiveReceive = 5542,

        /// <summary>
        /// 活跃度不够，不能领取该奖励
        /// </summary>
        ActiveNotIntegral = 5543,
        /// <summary>
        /// 已经完成
        /// </summary>
        ActiveHaveComplete = 5544,
        /// <summary>
        /// 没有可领取的奖励
        /// </summary>
        ActiveNotPrize = 5545,
        #endregion

        /// <summary>
        /// 请明日再领取绑定点券
        /// </summary>
        InvestGetTomorrow = 5600,


        #region 球员卡合成(新) 5700-5720
        /// <summary>
        /// 欧冠卡不能与其他颜色卡牌混合合成
        /// </summary>
        EuroCardOnly = 5700,
        /// <summary>
        /// 对不起，您的背包中没有合成保护卡
        /// </summary>
        NoProtectCard = 5701,
        /// <summary>
        /// 对不起，您放入的不是合成保护卡
        /// </summary>
        NotProtectCard = 5702,
        /// <summary>
        /// 银卡及以上不可合成
        /// </summary>
        SilverCardCannotSynthesis = 5703,
        /// <summary>
        /// 主力球员你不可合成
        /// </summary>
        MainPlayerCannotSynthesis = 5704,
        #endregion

        #region 签到 5750-5780

        /// <summary>
        /// 该奖励已领取
        /// </summary>
        PrizeHaveSend = 5750,

        #endregion

        #region Vip签到经验 5781-5800

        /// <summary>
        ///Vip才可领取
        /// </summary>
        NotVip = 5781,
        /// <summary>
        /// 今日已领取
        /// </summary>
        HasReceiveToday = 5782,

        #endregion

        #region 道具出售 5801-5810
        /// <summary>
        /// 没有选择出售道具
        /// </summary>
        NotSellPrpo = 5801,

        /// <summary>
        /// 一次最多出售10个道具
        /// </summary>
        MaxThePrpo = 5802,

        #endregion

        #region 分享  5811-5820
        /// <summary>
        /// 对不起，您已经分享过了，不能得到奖励
        /// </summary>
        AlreadyShare = 5811,
        /// <summary>
        /// 您今天分享得到奖励已达上限，今天不能再得到奖励了
        /// </summary>
        MaxShareNumber = 5812,
        /// <summary>
        /// 您现在分享不能得到奖励哦，离上一次分享相差一小时才能得到奖励
        /// </summary>
        NowShareNotPrize = 5813,
        #endregion

        #region 欧洲杯竞猜 5830-5850
        /// <summary>
        /// 对不起，没有找到该场比赛
        /// </summary>
        NotHaveMatch = 5830,

        /// <summary>
        /// 对不起，该场比赛现在不能竞猜了哦
        /// </summary>
        HaveGambleTime = 5831,
        /// <summary>
        /// 您已经竞猜过该场比赛了,不能重复竞猜
        /// </summary>
        HaveGamble = 5832,
        /// <summary>
        /// 对不起，竞猜钻石有误
        /// </summary>
        PointConfigNotHave = 5833,
        /// <summary>
        /// 比赛开始2小时后才能结束比赛
        /// </summary>
        Match2HoursEnd = 5834,

        #endregion

        #region  转盘 5851-5900

        /// <summary>
        /// 幸运币不足
        /// </summary>
        LuckyCoinInsufficient = 5851,
        /// <summary>
        /// 至少获取一项奖励后才能重置
        /// </summary>
        TurntableNotReset = 5852,

        #endregion

        #region 球员升星 5901-5920
        /// <summary>
        /// 该物品不能升星
        /// </summary>
        ItemNotUpgradeTheStar = 5901,
        /// <summary>
        /// 您已达到升星的最高等级
        /// </summary>
        MaxTheStar = 5902,
        /// <summary>
        /// 对不起，球员卡不足
        /// </summary>
        PlayerInsufficient = 5903,
        /// <summary>
        /// 您还为激活潜力，去升星球员吧
        /// </summary>
        PotentialNot = 5904,
        #endregion

        #region 奥运会 5921
        /// <summary>
        /// 奥运金牌数量不足
        /// </summary>
        OlympicTheGoldMedalCountNot = 5921,

        #endregion

        #region 竞技场 5922-

        /// <summary>
        /// 该球员不符合上阵要求
        /// </summary>
        ArenaPlayerNotUpFormation = 5922,
        /// <summary>
        /// 参赛卡已达到最大值。
        /// </summary>
        StaminaHaveMax = 5923,
        /// <summary>
        /// 出场球员需满7人才能参赛
        /// </summary>
        TeammemberNotNumber = 5924,
        /// <summary>
        /// 参赛卡数量不足
        /// </summary>
        StaminaInsufficient = 5925,
        /// <summary>
        /// 赛季还未结束
        /// </summary>
        SeasonNotEnd = 5926,
        /// <summary>
        /// 还在准备阶段
        /// </summary>
        SeasonNotStart = 5927,
        /// <summary>
        /// 竞技币数量不足
        /// </summary>
        ArenaCoinNot = 5928,
        /// <summary>
        /// 本赛季结束后才可撤下阵容
        /// </summary>
        ArenaNotGoOffStage = 5929,
        #endregion

        #region 球场点击 6100-6200
        /// <summary>
        /// 错误码:6100
        /// </summary>
        MatchRewardLimitApp = 6100,
        /// <summary>
        /// 错误码:6101
        /// </summary>
        MatchRewardLimitType = 6101,
        /// <summary>
        /// 错误码:6102
        /// </summary>
        MatchRewardOverCoin = 6102,
        /// <summary>
        /// 错误码:6103
        /// </summary>
        MatchRewardOverPoint = 6103,
        /// <summary>
        /// 错误码:6105
        /// </summary>
        MatchRewardMissMatch = 6105,
        /// <summary>
        /// 错误码:6106
        /// </summary>
        MatchRewardOverGet = 6106,
        /// <summary>
        /// 错误码:6107
        /// </summary>
        MatchRewardOverSet = 6107,
        #endregion

        #region 拍卖行 6201-6250

        /// <summary>
        /// 起拍价格不能小于2金条
        /// </summary>
        StartPriceSmall = 6201,
        /// <summary>
        /// 这个物品不能挂牌
        /// </summary>
        NotDeal=6202,
        /// <summary>
        /// 该物品已下架
        /// </summary>
        ItemSoldOut=6203,
        /// <summary>
        /// 您的出价还未被超越
        /// </summary>
        AuctionTheSame=6204,
        /// <summary>
        /// 竞拍价格不能小于现有价格
        /// </summary>
        AuctionPriceSmall = 6205,
        /// <summary>
        /// 该物品已经卖出
        /// </summary>
        ItemHaveSellOut = 6206,
        /// <summary>
        /// 不能购买自己拍卖的物品，您可以挂牌界面下架该物品
        /// </summary>
        NotBuyOneself = 6207,
        /// <summary>
        /// 该物品不是您上架的，您没有下架权限
        /// </summary>
        NotSoldOutAuthority=6208,
        /// <summary>
        /// 挂牌数量已达上限，提高VIP等级可增加挂牌数量
        /// </summary>
        TransferNumberMax=6209,
        /// <summary>
        /// 转会市场需要等级达到30级或者VIP等级达到3才开启
        /// </summary>
        TransferNotOpen=6210,
        #endregion
    }
}
