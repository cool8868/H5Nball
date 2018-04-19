using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    /// <summary>
    /// 物品类型枚举
    /// </summary>
    public enum EnumItemType
    {
        
        /// 球员卡
        /// </summary>
        PlayerCard = 1,
        /// <summary>
        /// 装备
        /// </summary>
        Equipment = 2,
        /// <summary>
        /// 商城道具
        /// </summary>
        MallItem = 3,
        /// <summary>
        /// 球魂
        /// </summary>
        BallSoul = 4,
        /// <summary>
        /// 图纸
        /// </summary>
        SuitDrawing = 5,
        /// <summary>
        /// 徽章
        /// </summary>
        TheBadge = 6,
        /// <summary>
        /// 经典球衣
        /// </summary>
        ClubClothes = 7,
        /// <summary>
        /// 钻石
        /// </summary>
        Point = 8,
        /// <summary>
        /// 友情点
        /// </summary>
        FriendshipPoint = 9,
    }

    /// <summary>
    /// 套装类型
    /// </summary>
    public enum EnumSuitType
    {
        /// <summary>
        /// 七件套
        /// </summary>
        SevenSuit = 1,
        /// <summary>
        /// 五件套
        /// </summary>
        FiveSuit = 2,
        /// <summary>
        /// 三件套
        /// </summary>
        ThreeSuit = 3,
        /// <summary>
        /// 散件
        /// </summary>
        Normal = 4,
    }

    /// <summary>
    /// 技能buff匹配类型
    /// </summary>
    public enum EnumSBMType
    {
        /// <summary>
        /// 球魂
        /// </summary>
        BallSoul = 1,
        /// <summary>
        /// 套装
        /// </summary>
        Suit = 2,
        /// <summary>
        /// 阵型
        /// </summary>
        Formation = 3,
        /// <summary>
        /// 教练
        /// </summary>
        Coach = 4,
        /// <summary>
        /// 徽章
        /// </summary>
        Badge = 5,
    }
}
