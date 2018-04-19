using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Common
{
    public enum EnumWorthType
    {
        /// <summary>
        /// 点券
        /// </summary>
        Gold = 1,
        /// <summary>
        /// 游戏币
        /// </summary>
        Coin = 2,
        /// <summary>
        /// 经理等级
        /// </summary>
        ManagerLevel = 91,
        /// <summary>
        /// Vip等级
        /// </summary>
        VipLevel = 92,
    }

    public enum EnumItemGenType
    {
        /// <summary>
        /// 指定物品
        /// </summary>
        SpecItem = 1,
        /// <summary>
        /// 随机物品
        /// </summary>
        RandItem = 2,
        /// <summary>
        /// 指定随机物品
        /// </summary>
        SpecRandItem = 3,
        /// <summary>
        /// 卡库随机物品
        /// </summary>
        LibRandItem = 4,
        /// <summary>
        /// 随机一套套装
        /// </summary>
        MultiRandSuit = 5,
        /// <summary>
        /// 游戏币
        /// </summary>
        Coin = 91,
        /// <summary>
        /// 点券
        /// </summary>
        Point = 92,
        /// <summary>
        /// 声望
        /// </summary>
        Prestige = 93,
        /// <summary>
        /// 兑换礼品
        /// </summary>
        Gift = 99,
    }
}
