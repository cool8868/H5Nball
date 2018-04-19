using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums.Activity
{
    public enum EnumActivityExPrizeType
    {
        /// <summary>
        /// 点券
        /// </summary>
        Point=1,
        /// <summary>
        /// 金币
        /// </summary>
        Coin=2,
        /// <summary>
        /// 指定物品
        /// </summary>
        Item=3,
        /// <summary>
        /// 随机物品
        /// </summary>
        RandomItem=4,
        /// <summary>
        /// 返点
        /// </summary>
        ReturnPoint=5,
        /// <summary>
        /// 随机套装一套
        /// </summary>
        RandomSuit=6,
        /// <summary>
        /// 阵容中球员
        /// </summary>
        Teammember=7,
        /// <summary>
        /// 声望
        /// </summary>
        Prestige = 8,
        /// <summary>
        /// 阅历
        /// </summary>
        Experience = 9,
        /// <summary>
        /// 绑定点卷
        /// </summary>
        BindPoint =10,
        /// <summary>
        /// 天梯荣誉
        /// </summary>
        Honor = 11,
        /// <summary>
        /// 金条
        /// </summary>
        GoldBar = 12,
        /// <summary>
        /// 点球游戏币
        /// </summary>
        GameCurrency=99,
        /// <summary>
        /// 用户指定物品
        /// </summary>
        ItemCode = 100,
    }
}
