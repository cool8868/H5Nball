using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Activity
{
    public enum EnumActivityType
    {
        /// <summary>
        /// 登录送礼
        /// </summary>
        LoginPrize=1,
        /// <summary>
        /// 在线奖励
        /// </summary>
        OnlinePrize=2,
        /// <summary>
        /// 首次充值
        /// </summary>
        PayFirst=3,
        /// <summary>
        /// 每日充值
        /// </summary>
        PayDaily = 4,
        /// <summary>
        /// 每日获得40分天梯积分
        /// </summary>
        LadderScore = 5,
        /// <summary>
        /// 完成5个每日任务
        /// </summary>
        DailyTaskCount=6,
        /// <summary>
        /// 购买2次体力
        /// </summary>
        BuyStamina = 7,
        /// <summary>
        /// 联赛获胜5场
        /// </summary>
        LeagueWin=8,

        /// <summary>
        /// 新手礼包
        /// </summary>
        NewPlayerGift = 9,

        /// <summary>
        /// 每日首次充值
        /// </summary>
        PayFirstDaily = 10,

        /// <summary>
        /// 新手礼包(1元)
        /// </summary>
        GiftPackNewPlayer=100,
        /// <summary>
        /// 超值礼包(18元)
        /// </summary>
        GiftPackValues=101,
        /// <summary>
        /// 强化礼包(60元)
        /// </summary>
        GiftPackStrength=102,
        /// <summary>
        /// 建队礼包(288元)
        /// </summary>
        GiftPackTeam=103,
        /// <summary>
        /// 周末优惠礼包(58元)
        /// </summary>
        GiftPackWeekend=104,

        VipGift=108,

        VipDaily=109,

        LevelGift=110,

        GuidePrize=111,
        



    }
}
