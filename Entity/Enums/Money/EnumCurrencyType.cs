using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 货币类型
    /// </summary>
    public enum EnumCurrencyType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 点券
        /// </summary>
        Point = 1,
        /// <summary>
        /// 金币
        /// </summary>
        Coin = 2,
        /// <summary>
        /// 声望
        /// </summary>
        Prestige = 3,
        /// <summary>
        /// 金券
        /// </summary>
        Coupon = 4,
        /// <summary>
        /// 绑定点券
        /// </summary>
        BindPoint = 8,
        /// <summary>
        /// 友情点
        /// </summary>
        FriendshipPoint=9,
        /// <summary>
        /// 金条
        /// </summary>
        GoldBar = 10,
        /// <summary>
        /// 幸运币 转盘用
        /// </summary>
        LuckyCoin=11,

        /// <summary>
        /// 游戏币 点球用
        /// </summary>
        GameCoin=12,
        /// <summary>
        /// 勇气值
        /// </summary>
        Courage = 13,
    }
}
