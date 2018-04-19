using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 商城道具类型
    /// </summary>
    public enum EnumMallType
    {
        /// <summary>
        /// 消耗类
        /// </summary>
        XH = 1,
        /// <summary>
        /// 增益类
        /// </summary>
        ZY = 2,
        /// <summary>
        /// 球衣类
        /// </summary>
        QY = 3,
        /// <summary>
        /// 活动类
        /// </summary>
        HD = 4,
        /// <summary>
        /// 虚拟类
        /// </summary>
        XN = 5,
        /// <summary>
        /// 球票类
        /// </summary>
        QP = 7,
        /// <summary>
        /// 限时礼包
        /// </summary>
        Mystery = 8,
        /// <summary>
        /// 金券
        /// </summary>
        JQ = 9,
    }

    public enum EnumGoldMallType
    {
        /// <summary>
        /// 道具
        /// </summary>
        MallItem = 1,
        /// <summary>
        /// 球员卡
        /// </summary>
        PlayerCard = 2,
        /// <summary>
        /// 球魂
        /// </summary>
        Ballsoul = 3,
        /// <summary>
        /// 装备
        /// </summary>
        Equipment = 4,
    }
}
