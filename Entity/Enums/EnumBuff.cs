using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Enums
{
    [Flags]
    public enum EnumSkillLiveFlag
    {
        None = 0,
        /// <summary>
        /// 常效
        /// </summary>
        Firm = 1,
        /// <summary>
        /// 开场前效果
        /// </summary>
        Ready = 2,
        /// <summary>
        /// 场上效果
        /// </summary>
        Live = 4
    }
    public enum EnumBuffSrcType
    {

        /// <summary>
        /// 商城道具
        /// </summary>
        MallItem = 1,
        /// <summary>
        /// 球星封印
        /// </summary>
        EquipWash = 2,
        /// <summary>
        /// 教练
        /// </summary>
        Coach = 3,
        /// <summary>
        /// 装备
        /// </summary>
        Equip = 4,
        /// <summary>
        /// 球魂
        /// </summary>
        EquipSoul = 5,
        /// <summary>
        /// 套装
        /// </summary>
        EquipSuit = 6,
        /// <summary>
        /// 经理天赋
        /// </summary>
        ManagerTalent = 7,
        /// <summary>
        /// 意志
        /// </summary>
        Will = 8,
        /// <summary>
        /// 公会技能
        /// </summary>
        GuildSkill = 9,
        /// <summary>
        /// 阵型
        /// </summary>
        Formation = 10,

    }

    [Flags]
    public enum EnumBuffUnitType
    {
        None = 0,
        /// <summary>
        /// 球员属性
        /// </summary>
        PlayerProp = 1,
        /// <summary>
        /// 经理显示Buff
        /// </summary>
        ManagerShow = 2,
    }
    public enum EnumBuffCode
    {
        #region 球员属性
        /// <summary>
        /// 速度
        /// </summary>
        PlayerSpeed = 100,
        /// <summary>
        /// 射门
        /// </summary>
        PlayerShoot = 101,
        /// <summary>
        /// 任意球
        /// </summary>
        PlayerFreeKick = 102,
        /// <summary>
        /// 控制
        /// </summary>
        PlayerBalance = 103,
        /// <summary>
        /// 体能
        /// </summary>
        PlayerStamina = 104,
        /// <summary>
        /// 力量
        /// </summary>
        PlayerStrength = 105,
        /// <summary>
        /// 侵略性
        /// </summary>
        PlayerAggression = 106,
        /// <summary>
        /// 干扰
        /// </summary>
        PlayerDisturb = 107,
        /// <summary>
        /// 抢断
        /// </summary>
        PlayerInterception = 108,
        /// <summary>
        /// 控球
        /// </summary>
        PlayerDribble = 109,
        /// <summary>
        /// 传球
        /// </summary>
        PlayerPass = 110,
        /// <summary>
        /// 意识
        /// </summary>
        PlayerMentality = 111,
        /// <summary>
        /// 反应
        /// </summary>
        PlayerResponse = 112,
        /// <summary>
        /// 位置感
        /// </summary>
        PlayerPositioning = 113,
        /// <summary>
        /// 手控球
        /// </summary>
        PlayerHandControl = 114,
        /// <summary>
        /// 加速度
        /// </summary>
        PlayerAcceleration = 115,
        /// <summary>
        /// 全属性
        /// </summary>
        PlayerProp = 120,
        #endregion

        #region 经理Buff
        /// <summary>
        ///友谊赛经验加成
        /// </summary>
        PkMatchExp = 201,
        /// <summary>
        ///巡回赛金币奖励
        /// </summary>
        TourCoin = 202,
        /// <summary>
        ///世界挑战赛经验
        /// </summary>
        WorldChallengeExp = 203,
        /// <summary>
        ///世界挑战赛金币奖励
        /// </summary>
        WorldChallengeCoin = 204,
        /// <summary>
        /// 球员卡分解暴击率
        /// </summary>
        DestroyCardCritRate = 205,
        /// <summary>
        /// 球员训练经验加成
        /// </summary>
        TrainExpPlusRate = 206,
        /// <summary>
        /// 众志成城
        /// </summary>
        TeamClubBuff = 281,
        #endregion

        #region 队服
        /// <summary>
        /// AC米兰队服
        /// </summary>
        TeamCloth1 = 301,
        /// <summary>
        /// 巴塞罗那队服
        /// </summary>
        TeamCloth2 = 302,
        /// <summary>
        /// 拜仁慕尼黑队服
        /// </summary>
        TeamCloth3 = 303,
        /// <summary>
        /// 皇家马德里队服
        /// </summary>
        TeamCloth4 = 304,
        /// <summary>
        /// 曼联队服
        /// </summary>
        TeamCloth5 = 305,
        /// <summary>
        /// 切尔西队服
        /// </summary>
        TeamCloth6 = 306,
        /// <summary>
        /// 尤文图斯队服
        /// </summary>
        TeamCloth7 = 307,
        /// <summary>
        /// 阿森纳队服
        /// </summary>
        TeamCloth8 = 308,
        /// <summary>
        /// 多特蒙特队服
        /// </summary>
        TeamCloth9 = 309,
        /// <summary>
        /// 曼城队服
        /// </summary>
        TeamCloth10 = 310,
        TeamCloth11 = 311,
        TeamCloth12 = 312,
        TeamCloth13 = 313,
        TeamCloth14 = 314,
        TeamCloth15 = 315,
        TeamCloth16 = 316,
        TeamCloth17 = 317,
        TeamCloth18 = 318,
        TeamCloth19 = 319,
        TeamCloth20 = 320,
        TeamCloth21 = 321,
        TeamCloth22 = 322,
        TeamCloth23 = 323,
        TeamCloth24 = 324,
        TeamCloth25 = 325,
        TeamCloth26 = 326,
        TeamCloth27 = 327,
        TeamCloth28 = 328,
        /// <summary>
        /// 公会强化身体
        /// </summary>
        GuildBodyProp = 901,
        /// <summary>
        /// 公会强化防守
        /// </summary>
        GuildDefenceProp = 902,
        /// <summary>
        /// 公会强化守门
        /// </summary>
        GuildGoalKeepProp = 903,
        /// <summary>
        /// 公会强化组织
        /// </summary>
        GuildOrganizeProp = 904,
        /// <summary>
        ///公会强化进攻
        /// </summary>
        GuildAttackProp = 905,
        /// <summary>
        /// 钢铁护盾
        /// </summary>
        GuildAnti1 = 906,
        /// <summary>
        /// 圣光祝福
        /// </summary>
        GuildAnti2 = 907,
        /// <summary>
        /// 神圣意志
        /// </summary>
        GuildAnti3 = 908
        #endregion




    }

}
