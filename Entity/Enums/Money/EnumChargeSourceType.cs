using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 充值类型枚举
    /// </summary>
    public enum EnumChargeSourceType
    {
        /// <summary>
        /// 充值
        /// </summary>
        System = 0,
        /// <summary>
        /// 联赛竞猜
        /// </summary>
        LeagueGamble = 1,
        /// <summary>
        /// 邮件收取
        /// </summary>
        MailAttachment = 2,
        /// <summary>
        /// 拍卖行竞标失败返回的
        /// </summary>
        AuctionReturn = 3,
        /// <summary>
        /// 拍卖行拍卖物品成功
        /// </summary>
        AuctionSaleSuccess = 4,
        /// <summary>
        /// 后台发送
        /// </summary>
        AdminSend = 5,
        /// <summary>
        /// 模拟器添加
        /// </summary>
        Emulator = 6,
        /// <summary>
        /// 删档测试注册添加
        /// </summary>
        BetaSend = 7,
        /// <summary>
        /// gm充值
        /// </summary>
        GmCharge = 8,
        /// <summary>
        /// 精彩活动奖励
        /// </summary>
        ActivityExPrize = 9,
        /// <summary>
        /// 腾讯直购道具
        /// </summary>
        TxBuy = 10,
        /// <summary>
        /// 活动赠送(任务集市)
        /// </summary>
        ActivityPrize = 11,
        /// <summary>
        /// 每日任务通关奖励
        /// </summary>
        DailyTaskFinishPrize = 12,
        /// <summary>
        /// 礼包兑换
        /// </summary>
        ExchangePrize = 13,
        /// <summary>
        /// 使用道具获得
        /// </summary>
        UseItem = 14,
        /// <summary>
        /// 老玩家召回返点
        /// </summary>
        RecallReturn = 15,
        /// <summary>
        /// 圣诞转盘活动
        /// </summary>
        RotaryTableReward = 16,
        /// <summary>
        /// 好友邀请
        /// </summary>
        FriendInvite = 17,

        /// <summary>
        /// 腾讯金券
        /// </summary>
        TxCouponBuy = 18,

        /// <summary>
        /// 联赛奖励
        /// </summary>
        LeaguePrize = 19,
        /// <summary>
        /// 友谊赛奖励
        /// </summary>
        PkMatchPrize = 20,
        /// <summary>
        /// 球员训练奖励
        /// </summary>
        OpenBoxPrize = 21, 
        /// <summary>
        /// 任务奖励
        /// </summary>
        TaskPrize = 22,
        /// <summary>
        /// 分享
        /// </summary>
        Share = 23,
        /// <summary>
        /// 欧洲杯
        /// </summary>
        Europe=24,
        /// <summary>
        /// 转盘
        /// </summary>
        Turntable =25,
        /// <summary>
        /// 球场点击奖励
        /// </summary>
        MatchReward = 26,
        /// <summary>
        /// 跨服活动
        /// </summary>
        CrossActivity = 27,
    }
}
