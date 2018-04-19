using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Bll.Schedule
{
    /// <summary>
    /// 计划任务枚举
    /// </summary>
    public enum EnumSchedule
    {
        /// <summary>
        /// 球员训练
        /// </summary>
        Train = 1,
        /// <summary>
        /// 创建杯赛
        /// </summary>
        DailycupCreate = 2,
        /// <summary>
        /// 运行杯赛
        /// </summary>
        DailycupRun = 3,
        /// <summary>
        /// 杯赛8强竞猜开奖
        /// </summary>
        DailycupGambleopen8 = 4,
        /// <summary>
        /// 杯赛4强竞猜开奖
        /// </summary>
        DailycupGambleopen4 = 5,
        /// <summary>
        /// 杯赛决赛竞猜开奖
        /// </summary>
        DailycupGambleopen2 = 6,
        /// <summary>
        /// 杯赛排名奖励发放
        /// </summary>
        DailycupSendPrize = 7,
        /// <summary>
        /// 世界挑战赛挂机
        /// </summary>
        WorldChallengeHook = 8,
        /// <summary>
        /// 巡回赛挂机
        /// </summary>
        TourHook = 9,
        /// <summary>
        /// 世界挑战赛排名显示
        /// </summary>
        WorldChallengeRankShow = 10,
        /// <summary>
        /// 世界挑战赛排名奖励发放
        /// </summary>
        WorldChallengeSendPrize = 11,
        /// <summary>
        /// 运行天梯赛
        /// </summary>
        LadderCheckStatus = 12,
        /// <summary>
        /// 天梯赛排行榜显示
        /// </summary>
        LadderRankShow = 13,
        /// <summary>
        /// 天梯赛积分换荣誉并发赛季奖励
        /// </summary>
        LadderScoreToHonorPrize = 14,
        /// <summary>
        /// 通用排行榜
        /// </summary>
        CommonRanking = 15,
        /// <summary>
        /// 更新在线时间
        /// </summary>
        OnlineUpdateActive = 16,
        /// <summary>
        /// 重置在线时间
        /// </summary>
        OnlineReset = 17,
        /// <summary>
        /// 更新kpi
        /// </summary>
        OnlineUpdateKpi = 18,
        /// <summary>
        /// 统计在线
        /// </summary>
        StatisticOnline = 19,
        /// <summary>
        /// 统计登录充值等
        /// </summary>
        StatisticDetail = 20,
        /// <summary>
        /// 统计每日数据
        /// </summary>
        StatisticYesterdayKpi = 21,
        /// <summary>
        /// 创建表记录
        /// </summary>
        StatisticCreate = 22,
        /// <summary>
        /// 统计今日数据
        /// </summary>
        StatisticTodayKpi = 23,
        /// <summary>
        /// 统计总数据
        /// </summary>
        StatisticTotal = 24,
        /// <summary>
        /// 发送精彩活动奖励
        /// </summary>
        SendActivityExPrize = 25,
        /// <summary>
        /// 更新pk赛对手缓存
        /// </summary>
        UpdatePKOpponent = 26,
        /// <summary>
        /// 天梯战报滚动
        /// </summary>
        LadderMatchMarquee=31,
        /// <summary>
        /// 清除过期邮件
        /// </summary>
        ClearMailExpired=39,
        /// <summary>
        /// 创建战报日表
        /// </summary>
        CreateProcessTable=40,
        /// <summary>
        /// 每周一0点1分刷新
        /// </summary>
        WeeklyRefresh=46,
        /// <summary>
        /// 天梯赛停止挂机
        /// </summary>
        LadderStopHook =64,
        /// <summary>
        /// 月卡发奖
        /// </summary>
        MonthCard=65,

        /// <summary>
        /// 跨服群雄发奖
        /// </summary>
        CrossCrowdPrize = 63,
        /// <summary>
        /// 跨服天梯赛积分换荣誉并发赛季奖励
        /// </summary>
        CrossScoreToHonorPrize = 70,/// <summary>
        /// 跨服天梯赛排行榜显示
        /// </summary>
        CrossRankShow = 71,
        /// <summary>
        /// 跨服天梯战报滚动
        /// </summary>
        CrossMatchMarquee = 72,
        /// <summary>
        /// 创建战报日表
        /// </summary>
        CreateProcessTable2 = 73,
        /// <summary>
        /// 竞技场每天刷新
        /// </summary>
        ArenaDayRefresh = 68,
        /// <summary>
        /// 竞技场发奖
        /// </summary>
        ArenaDaySendPrize = 69,
        /// <summary>
        /// 跨服排名
        /// </summary>
        CrossRank = 74,
        /// <summary>
        /// 刷新点球排名
        /// </summary>
        PenaltyKickRank = 75,
        /// <summary>
        /// 刷新点球排名
        /// </summary>
        PenaltyKickPrize = 76,
        /// <summary>
        /// 欧洲杯竞猜刷新比赛
        /// </summary>
        EuropeRefreshMatch=66,
        /// <summary>
        /// 欧洲杯竞猜刷新活动
        /// </summary>
        EuropeRefreshActivity=67,
        /// <summary>
        /// 转会市场刷新状态
        /// </summary>
        TransferRefreshStatus=77,
        /// <summary>
        /// 每日充值活动刷新传奇碎片
        /// </summary>
        RefreshLegendCode=78,
        /// <summary>
        /// 跨服活动
        /// </summary>
        CrossActivity=79,
    }
}
