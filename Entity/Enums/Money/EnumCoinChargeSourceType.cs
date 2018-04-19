using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity
{
    public enum EnumCoinChargeSourceType
    {
        /// <summary>
        /// None
        /// </summary>
        None = -1,
        /// <summary>
        /// 后台添加
        /// </summary>
        AdminAdd = 1,
        /// <summary>
        /// 邮件附件添加
        /// </summary>
        MailAttachment = 2,
        /// <summary>
        /// 帮助训练获得
        /// </summary>
        HelpTrain = 3,
        /// <summary>
        /// 巡回赛奖励
        /// </summary>
        Tour = 4,
        /// <summary>
        /// 世界挑战赛奖励
        /// </summary>
        WorldChallenge = 5,
        /// <summary>
        /// 任务奖励
        /// </summary>
        Task = 6,
        /// <summary>
        /// 精英巡回赛奖励
        /// </summary>
        Elite = 7,
        /// <summary>
        /// 巡回赛挂机奖励
        /// </summary>
        TourHook = 8,
        /// <summary>
        /// 新手礼包
        /// </summary>
        NewPlayerPack = 9,
        /// <summary>
        /// 活动奖励
        /// </summary>
        ActivityPrize = 10,
        /// <summary>
        /// 联赛奖励
        /// </summary>
        LeaguePrize = 11,
        /// <summary>
        /// 媒体兑换
        /// </summary>
        Exchange = 12,
        /// <summary>
        /// 点球活动奖励
        /// </summary>
        AdTopScorerKeepPrize = 13,
        /// <summary>
        /// 精彩活动奖励
        /// </summary>
        ActivityExPrize = 14,
        /// <summary>
        /// PK赛奖励
        /// </summary>
        PlayerKillPrize = 15,
        /// <summary>
        /// 图纸分解
        /// </summary>
        SuitdrawingDecompose = 16,
        /// <summary>
        /// 球员卡分解
        /// </summary>
        PlayerCardDecompose = 17,
        /// <summary>
        /// 好友邀请
        /// </summary>
        FriendInvite=18,
        /// <summary>
        /// 跨服群雄逐鹿
        /// </summary>
        CrossCrowd = 20,
        /// <summary>
        /// 活跃度
        /// </summary>
        Active = 21,
        /// <summary>
        /// 天梯赛
        /// </summary>
        Ladder = 93,
        /// <summary>
        /// 装备出售
        /// </summary>
        EquipmentSell = 145,
        /// <summary>
        /// 友谊赛
        /// </summary>
        FriendMatch=146,
        /// <summary>
        /// 购买VIP礼包
        /// </summary>
        BuyVipPackage=147,
        /// <summary>
        /// 分享
        /// </summary>
        Share = 148,
        /// <summary>
        /// 欧洲杯
        /// </summary>
        Eruope=149,
        /// <summary>
        /// 转盘
        /// </summary>
        Turntable =150,
        /// <summary>
        /// 球场点击奖励
        /// </summary>
        MatchReward=151,
        /// <summary>
        /// 球星启示录
        /// </summary>
        Revelation=152,
        
    }
}
