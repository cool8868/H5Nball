using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{


    public enum EnumTurntableType
    {
        /// <summary>
        /// 青铜
        /// </summary>
        Bronze = 1,
        /// <summary>
        /// 白银
        /// </summary>
        Silver = 2,
        /// <summary>
        /// 黄金
        /// </summary>
        Gold = 3,
    }

    public enum EnumTurntablePrizeType 
    {
        /// <summary>
        /// 钻石
        /// </summary>
        Point =1,
        /// <summary>
        /// 金币
        /// </summary>
        Coin=2,
        /// <summary>
        /// 指定物品
        /// </summary>
        Item=3,
        /// <summary>
        /// 卡库
        /// </summary>
        Random =4,
        /// <summary>
        /// 转盘
        /// </summary>
        Turntable=5,
        /// <summary>
        /// 特殊物品
        /// </summary>
        Special=6,
    }
   
}
