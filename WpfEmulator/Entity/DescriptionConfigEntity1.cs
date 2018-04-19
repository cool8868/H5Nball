using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Games.NBall.Entity;
using Games.NBall.Entity.Config.Custom;

namespace Games.NBall.WpfEmulator.Entity
{
    [Serializable]
    public class DescriptionConfigEntity1
    {
        public DescriptionConfigEntity1()
        {
            Summary = new List<WpfSummaryEntity>();
            
            Summary.Add(new WpfSummaryEntity("LeagueStar", "联赛星星奖励配置"));
            Summary.Add(new WpfSummaryEntity("LeagueFightMap", "联赛对阵配置"));
            Summary.Add(new WpfSummaryEntity("LeagueNpc", "联赛Npc配置"));
            Summary.Add(new WpfSummaryEntity("RevelationMarkEntity","球星启示录关卡配置"));
            Summary.Add(new WpfSummaryEntity("CoachInfoEntity", "教练信息配置"));
            Summary.Add(new WpfSummaryEntity("CoachUpgradeEntity", "教练升级配置"));
            Summary.Add(new WpfSummaryEntity("CoachStarEntity", "教练升星配置"));

        }
        public List<WpfSummaryEntity> Summary { get; set; }

        /// <summary>
        /// 联赛星星奖励配置
        /// </summary>
        public Dictionary<int, List<ConfigLeaguestarEntity>> LeagueStar { get; set; }

        /// <summary>
        /// 联赛对阵配置
        /// </summary>
        public Dictionary<int, List<ConfigLeaguefightmapEntity>> LeagueFightMap { get; set; }

        /// <summary>
        /// 联赛NPC配置
        /// </summary>
        public Dictionary<int, List<LeagueNpc>> LeagueNpc { get; set; }

        /// <summary>
        /// 球星启示录关卡列表
        /// </summary>
        public List<RevelationMarkEntity> RevelationList { get; set; }

        /// <summary>
        /// 教练升级配置
        /// </summary>
        public List<CoachUpgradeEntity> CoachUpgrade { get; set; }

        /// <summary>
        /// 教练升星配置
        /// </summary>
        public List<CoachStarEntity> CoachStar { get; set; }

        /// <summary>
        /// 教练信息配置
        /// </summary>
        public List<CoachInfoEntity> CoachInfo { get; set; }
    }

     [Serializable]
    public class LeagueNpc
    {
         public int LeagueId { get; set; }

         public int TeamId { get; set; }

         public string Name { get; set; }

         public string Logo { get; set; }
    }

    /// <summary>
    /// 球星启示录关卡信息
    /// </summary>
    public class RevelationMarkEntity
    {
        /// <summary>
        /// 关卡ID
        /// </summary>
        public int MarkId { get; set; }

        /// <summary>
        /// 进度
        /// </summary>
        public int Schedule { get; set; }

        /// <summary>
        /// 关卡球星
        /// </summary>
        public string MarkPlayer { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 球队
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 对手球队
        /// </summary>
        public string OpponentTeamName { get; set; }

        /// <summary>
        /// 阵型
        /// </summary>
        public string Formation { get; set; }

        /// <summary>
        /// 对手阵型
        /// </summary>
        public string OpponentFormation { get; set; }

        /// <summary>
        /// 通关几率获得教练碎片
        /// </summary>
        public string PassPrizeItems { get; set; }

        /// <summary>
        /// 首次通关可活动奖励
        /// </summary>
        public string FirstPassItem { get; set; }

        /// <summary>
        /// 关卡故事
        /// </summary>
        public string Story { get; set; }

    }

    /// <summary>
    /// 教练详情表
    /// </summary>
    public class CoachInfoEntity
    {
        /// <summary>
        /// 教练ID
        /// </summary>
        public int CoachId;
        /// <summary>
        /// 教练名
        /// </summary>
        public string Name;
        /// <summary>
        ///进攻
        /// </summary>
        public int Offensive;
        /// <summary>
        /// 组织
        /// </summary>
        public int Organizational;
        /// <summary>
        /// 防守
        /// </summary>
        public int Defense;
        /// <summary>
        /// 身体
        /// </summary>
        public int BodyAttr;
        /// <summary>
        /// 守门
        /// </summary>
        public int Goalkeeping;

        /// <summary>
        /// 对应的碎片ID
        /// </summary>
        public int DebrisCode;

        /// <summary>
        /// 是否有技能
        /// </summary>
        public bool IsSkill;
        /// <summary>
        /// 技能ID
        /// </summary>
        public int SkillId;

        /// <summary>
        /// 技能名
        /// </summary>
        public string SkillName;
        /// <summary>
        /// 触发条件
        /// </summary>
        public string TriggerCondition;
        /// <summary>
        /// cd
        /// </summary>
        public string Cd;
        /// <summary>
        /// 持续时间
        /// </summary>
        public string TimeOfDuration;
        /// <summary>
        /// 触发概率
        /// </summary>
        public int TriggerProbability;
        /// <summary>
        /// 技能效果
        /// </summary>
        public string Description;
        /// <summary>
        /// 升级效果
        /// </summary>
        public string PlusDescription;
        /// <summary>
        /// 基本数值
        /// </summary>
        public string Base0;
        /// <summary>
        /// 基本数值
        /// </summary>
        public string Base1;
        /// <summary>
        /// 升级数值
        /// </summary>
        public string Plus0;
        /// <summary>
        /// 升级数值
        /// </summary>
        public string Plus1;


    }

    /// <summary>
    /// 教练升级配置
    /// </summary>
    public class CoachUpgradeEntity
    {
        public int Level;
        public int UpgradeExp;
        public int UpgradeSkillCoin;
    }

    /// <summary>
    /// 教练升星配置
    /// </summary>
    public class CoachStarEntity
    {
        /// <summary>
        /// 星级  0=激活条件
        /// </summary>
        public int StarLevel;
        /// <summary>
        /// 教练ID
        /// </summary>
        public int CoachId;
        /// <summary>
        /// 需要消耗的碎片code
        /// </summary>
        public int CosumeDebrisCode;
        /// <summary>
        /// 需要消耗碎片数量
        /// </summary>
        public int ConsumeDebris;
        /// <summary>
        /// 技能最大可达到等级
        /// </summary>
        public int MaxSkillLevel;
    }
}
