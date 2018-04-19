using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{


    public enum EnumSkillSrcType
    {
        /// <summary>
        /// 通用技能卡
        /// </summary>
        SkillCard = 1,
        /// <summary>
        /// 球星技能
        /// </summary>
        StarSkill = 2,
        /// <summary>
        /// 经理天赋
        /// </summary>
        ManagerTalent = 6,
        /// <summary>
        /// 意志
        /// </summary>
        ManagerWill = 7,
        /// <summary>
        /// 组合
        /// </summary>
        ManagerComb = 8,
    }
    public enum EnumSkillRefType
    {
        /// <summary>
        /// 通用技能卡
        /// </summary>
        A = 1,
        /// <summary>
        /// 球星技能
        /// </summary>
        S = 2,
        /// <summary>
        /// 经理天赋
        /// </summary>
        T = 6,
        /// <summary>
        /// 意志
        /// </summary>
        W = 7,
        /// <summary>
        /// 组合
        /// </summary>
        C = 8,
    }
    /// <summary>
    /// 技能品质
    /// </summary>
    public enum EnumSkillCardClass
    {
        /// <summary>
        /// 绿
        /// </summary>
        Green = 1,
        /// <summary>
        /// 蓝
        /// </summary>
        Blue = 2,
        /// <summary>
        /// 紫
        /// </summary>
        Purple = 3,
        /// <summary>
        /// 橙
        /// </summary>
        Orange = 4,
    }
    /// <summary>
    /// 技能动作类型
    /// </summary>
    public enum EnumSKillActType
    {
        /// 射门
        /// </summary>
        Shoot = 1,
        /// <summary>
        /// 过人
        /// </summary>
        Through = 2,
        /// <summary>
        ///防守 
        /// </summary>
        Defence = 3,
        /// <summary>
        /// 组织
        /// </summary>
        Organize = 4,
        /// <summary>
        /// 守门
        /// </summary>
        GoalKeep = 5,
    }

    public enum EnumSkillDriveType
    {
        /// <summary>
        /// 被动
        /// </summary>
        Passive = 0,
        /// <summary>
        /// 主动
        /// </summary>
        Active = 1,
        /// <summary>
        /// 混合
        /// </summary>
        Hybrid = 2,

    }

    public enum EnumManagerType
    {
        /// <summary>
        /// 初级经理
        /// </summary> 
        Rookie = 1,
        /// <summary>
        /// 攻势足球
        /// </summary>
        Attack = 2,
        /// <summary>
        /// 防守教练
        /// </summary>
        Defence = 3,
        /// <summary>
        /// 攻守平衡
        /// </summary>
        Balance = 4,

    }
}
