using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;

namespace Games.NBall.WpfEmulator.Entity
{
    public class SkillTipsEntity
    {
        public SkillTipsEntity()
        {
            Summary = new List<WpfSummaryEntity>();
            Summary.Add(new WpfSummaryEntity("SkillCard", "技能卡信息"));
            Summary.Add(new WpfSummaryEntity("StarSkill", "球星技能"));
            Summary.Add(new WpfSummaryEntity("ManagerTalent", "经理天赋信息"));
            Summary.Add(new WpfSummaryEntity("LowWill", "低级意志"));
            Summary.Add(new WpfSummaryEntity("HighWill", "高级意志"));
            Summary.Add(new WpfSummaryEntity("Combs", "球员组合"));
            Summary.Add(new WpfSummaryEntity("StarArousalSkills", "球员觉醒技能"));
            Summary.Add(new WpfSummaryEntity("ConfigStrength", "球员卡强化配置"));
            Summary.Add(new WpfSummaryEntity("ConfigSkillUpgrade", "技能升级配置"));
            Summary.Add(new WpfSummaryEntity("ConfigPrpoSell", "物品出售价格配置"));
            Summary.Add(new WpfSummaryEntity("ManagerSkillTree", "经理天赋信息New"));
            Summary.Add(new WpfSummaryEntity("ClubSkill", "俱乐部技能"));
            Summary.Add(new WpfSummaryEntity("Potential", "球员潜力"));
            Summary.Add(new WpfSummaryEntity("StarSkillLevel", "球星技能等级"));
        }
        public List<WpfSummaryEntity> Summary { get; set; }

        /// <summary>
        /// 球星技能
        /// </summary>
        public List<DicStarskilltipsEntity> StarSkill
        {
            get;
            set;
        }
        /// <summary>
        /// 技能卡
        /// </summary>
        public List<DicSkillcardtipsEntity> SkillCard
        {
            get;
            set;
        }
        /// <summary>
        /// 经理天赋
        /// </summary>
        public List<DicManagertalenttipsEntity> ManagerTalent
        {
            get;
            set;
        }

        /// <summary>
        /// 经理天赋 New
        /// </summary>
        public List<ManagerSkillTree> ManagerSkillTree { get; set; }

        public List<DicClubskillEntity> ClubSkills { get; set; }
        /// <summary>
        /// 低级意志
        /// </summary>
        public List<DicManagerwilltipsEntity> LowWill
        {
            get;
            set;
        }
        /// <summary>
        /// 高级意志
        /// </summary>
        public List<DicManagerwilltipsEntity> HighWill
        {
            get;
            set;
        }
        /// <summary>
        /// 球员组合
        /// </summary>
        public List<CombTipsEntity> Combs
        {
            get;
            set;
        }

        /// <summary>
        /// 球星觉醒技能
        /// </summary>
        public List<DicStararousalskillsEntity> StarArousalSkills { get; set; }

        /// <summary>
        /// 球员卡强化配置
        /// </summary>
        public Dictionary<int, ConfigStrengthEntity> PlayerStrengthDic { get; set; }

        /// <summary>
        /// 技能升级配置
        /// </summary>
        public Dictionary<int, ConfigSkillupgradeEntity> SkillUpGradeDic { get; set; }

        /// <summary>
        /// 物品出售价格配置
        /// </summary>
        public List<ConfigPrposellEntity> PrpoSelllist { get; set; }

        /// <summary>
        /// 球员潜力
        /// </summary>
        public Dictionary<int, List<ConfigPotentialEntity>> Potential { get; set; }

        public List<DicStarskillleveltipsEntity> StarSkillLevels { get; set; }
    }

    public class CombTipsEntity
    {
        public CombTipsEntity()
        {
        }

        public CombTipsEntity(DicManagerwilltipsEntity dic)
        {
            this.SkillCode = dic.SkillCode.Replace("W", "C");
            this.SkillName = dic.SkillName;
            this.Icon = dic.Icon;
        }

        #region Data
        /// <summary>
        /// 组合技能Code
        /// </summary>
        public System.String SkillCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public System.String SkillName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public System.String Icon { get; set; }
        /// <summary>
        /// 组成的球员列表
        /// </summary>
        public List<CombPartTipsEntity> PartList
        {
            get;
            set;
        }
        #endregion


    }

    public class ManagerSkillTree
    {
        /// <summary>
        /// 技能编号
        /// </summary>
        public string SkillCode { get; set; }

        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName { get; set; }

        /// <summary>
        /// 技能等级
        /// </summary>
        public int SkillLevel { get; set; }

        /// <summary>
        /// 经理天赋类型 2 攻势 3防守 4攻守平衡
        /// </summary>
        public int ManagerType { get; set; }

        /// <summary>
        /// 开放等级
        /// </summary>
        public int ManagerLevel { get; set; }

        /// <summary>
        /// 前置技能要求
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// 前置点数要求
        /// </summary>
        public int CoditionPoint { get; set; }

        /// <summary>
        /// 最大可加多少点
        /// </summary>
        public int MaxPoint { get; set; }

        /// <summary>
        /// 技能类型 0被动  1主动 2混合
        /// </summary>
        public int Opener { get; set; }

        /// <summary>
        /// 技能描述
        /// </summary>
        public string Description { get; set; }
    }

    public class CombPartTipsEntity
    {
        public CombPartTipsEntity()
        { 
        }
        public CombPartTipsEntity(DicManagerwillparttipsEntity dic)
        {
            this.ItemCode = dic.ItemCode;
            this.Pid = dic.Pid;
            this.PName = dic.PName;
            this.BuffMemo = dic.BuffMemo;
            this.LinkPid = dic.LinkPid;
        }

        #region Data
        /// <summary>
        /// 物品代码
        /// </summary>
        public System.Int32 ItemCode { get; set; }
        /// <summary>
        /// 球员id
        /// </summary>
        public System.Int32 Pid { get; set; }
        /// <summary>
        /// 球员名称
        /// </summary>
        public System.String PName { get; set; }
        /// <summary>
        /// 效果描述
        /// </summary>
        public System.String BuffMemo { get; set; }

        public string LinkPid { get; set; }
        #endregion

    }

    
}
