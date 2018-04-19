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
    public class DescriptionConfigEntity
    {
        public DescriptionConfigEntity()
        {
            Summary = new List<WpfSummaryEntity>();
            Summary.Add(new WpfSummaryEntity("GrowDic", "球员成长等级描述"));
            Summary.Add(new WpfSummaryEntity("EquipmentSuit", "套装描述"));
            Summary.Add(new WpfSummaryEntity("TourLeague", "巡回赛联赛信息"));
            Summary.Add(new WpfSummaryEntity("TourStage", "巡回赛关卡信息"));
            Summary.Add(new WpfSummaryEntity("TourPrize", "巡回赛奖励"));
            Summary.Add(new WpfSummaryEntity("TourElitePrize", "精英巡回赛奖励"));
            Summary.Add(new WpfSummaryEntity("TourElite", "精英巡回赛信息"));
            Summary.Add(new WpfSummaryEntity("Scouting", "球探信息"));
            Summary.Add(new WpfSummaryEntity("Tasks", "任务信息"));
            Summary.Add(new WpfSummaryEntity("Mails", "邮件信息"));
            Summary.Add(new WpfSummaryEntity("LadderExchanges","天梯兑换荣誉表"));
            Summary.Add(new WpfSummaryEntity("LeagueExchanges", "联赛兑积分表"));
            Summary.Add(new WpfSummaryEntity("Viplevels", "vip等级配置"));
            Summary.Add(new WpfSummaryEntity("MailContentTypes", "邮件内容匹配表"));
            Summary.Add(new WpfSummaryEntity("PopupContentTypes", "弹出消息内容匹配表"));
            Summary.Add(new WpfSummaryEntity("Functionopens", "功能开放匹配表"));
            Summary.Add(new WpfSummaryEntity("PopBanners", "推送系统通知表"));
            Summary.Add(new WpfSummaryEntity("EquipmentPlus", "装备加成配置表"));
            Summary.Add(new WpfSummaryEntity("Activityprizes", "活动奖励配置表"));
            Summary.Add(new WpfSummaryEntity("BuffTips", "Buff配置表"));
            Summary.Add(new WpfSummaryEntity("BuffSrcTips", "Buff来源配置表"));
            Summary.Add(new WpfSummaryEntity("AdTips", "点球活动配置表"));
            Summary.Add(new WpfSummaryEntity("ArenaRankingAwayr", "竞技场排行奖励表"));
            Summary.Add(new WpfSummaryEntity("ArenaWinningAwayr", "竞技场连胜奖励表"));
            Summary.Add(new WpfSummaryEntity("ConfigCoachGrouop", "教练技能升阶配置"));
            Summary.Add(new WpfSummaryEntity("ConfigCoachInfo", "教练详细表"));
            Summary.Add(new WpfSummaryEntity("ConfigCoachInherit", "教练传承配置"));
            Summary.Add(new WpfSummaryEntity("ConfigCoachSkill", "教练技能表"));
            Summary.Add(new WpfSummaryEntity("ConfigCoachUpgrade", "教练升级配置表"));
            Summary.Add(new WpfSummaryEntity("ConfigSudokuRankingAwary", "九宫格排名奖励配置"));
            Summary.Add(new WpfSummaryEntity("ConfigSudokuAwary", "九宫格踢球奖励配置"));
            Summary.Add(new WpfSummaryEntity("ConfigSudokuGrid", "九宫格格子配置"));
            Summary.Add(new WpfSummaryEntity("PrecisionCastingPro", "精铸属性描述"));
            Summary.Add(new WpfSummaryEntity("CrossSiteTips", "跨服区服列表"));
            Summary.Add(new WpfSummaryEntity("GiftPack", "礼包描述"));
            Summary.Add(new WpfSummaryEntity("Turntable", "转盘"));
            Summary.Add(new WpfSummaryEntity("EliteHookCont", "精英巡回赛掉落合同"));
        }
        public List<WpfSummaryEntity> Summary { get; set; }

        /// <summary>
        /// 球员成长等级描述
        /// </summary>
        public List<LDescriptionEntity> GrowDic { get; set; }
        /// <summary>
        /// 套装描述
        /// </summary>
        public List<EquipmentsuitEntity> EquipmentSuit { get; set; }
        /// <summary>
        /// 巡回赛联赛信息
        /// </summary>
        public List<TourleagueEntity> TourLeague { get; set; }
        /// <summary>
        /// 巡回赛关卡
        /// </summary>
        public List<TourstageEntity> TourStage { get; set; }
        /// <summary>
        /// 精英巡回赛
        /// </summary>
        public List<ToureliteEntity> TourElite { get; set; }
        /// <summary>
        /// 球探
        /// </summary>
        public List<ScoutingEntity> Scouting { get; set; }
        /// <summary>
        /// 任务
        /// </summary>
        public List<TaskDescriptionEntity> Tasks { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public List<DicMailEntity> Mails { get; set; }

        /// <summary>
        /// vip等级配置
        /// </summary>
        public List<ConfigViplevelEntity> Viplevels { get; set; }

        /// <summary>
        /// 天梯兑换
        /// </summary>
        public List<LadderExchangeEntity> LadderExchanges { get; set; }

        /// <summary>
        /// 联赛兑换
        /// </summary>
        public List<LeagueExchangeEntity> LeagueExchanges { get; set; }

        public List<WpfConfigFunctionopenEntity> Functionopens { get; set; }


        public List<ConfigEquipmentplusEntity> Equipmentplus { get; set; }


        public List<BuffTipsEntity> BuffTips { get; set; }

        public List<BuffSrcTipsEntity> BuffSrcTips { get; set; }


        public string WChallengeNameTemplate { get; set; }
        public string WildCardName { get; set; }

        public string HighBallsoulName { get; set; }

        public string NormalBallsoulName { get; set; }

        public string PlayerCardStrengthPlus { get; set; }

        
     
        /// <summary>
        /// 区服列表
        /// </summary>
        public List<CrossSiteTipsEntity> CrossSiteTips { get; set; }

        /// <summary>
        /// 精英巡回赛掉落合同
        /// </summary>
        public List<EliteHookContEntity> EliteHookCont { get; set; }

        /// <summary>
        /// 转盘配置
        /// </summary>
        public List<ConfigTurntableprizeEntity> Turntable { get; set; }

    }


    [Serializable]
    public class EliteHookContEntity
    {
        public int PlayerCont { get; set; }
        public List<HookStageEntity> StageIdList { get; set; }
        public bool IsElite { get; set; }
    }

    [Serializable]
    public class HookStageEntity
    {
        public int Idx { get; set; }
        public int Cost { get; set; }
    }

    [Serializable]
    public class LDescriptionEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int Idx { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public string GrowTip { get; set; }
    }

    public class TourstageEntity
    {

        #region Public Properties

        ///<summary>
        ///Idx
        ///</summary>
        public System.Int32 Idx { get; set; }

        public int LeagueId { get; set; }

        ///<summary>
        ///Name
        ///</summary>
        public System.String Name { get; set; }
        /// <summary>
        /// 需要经理等级
        /// </summary>
        public int ManagerLevel { get; set; }

        ///<summary>
        ///LockMemo
        ///</summary>
        public System.String LockMemo { get; set; }


        ///<summary>
        ///Description
        ///</summary>
        public System.String Description { get; set; }

        public string PrizeEquipment1 { get; set; }

        public string PrizeEquipment2 { get; set; }
        #endregion
    }

    public class TourleagueEntity
    {

        #region Public Properties

        ///<summary>
        ///Idx
        ///</summary>
        public System.Int32 Idx { get; set; }

        ///<summary>
        ///Name
        ///</summary>
        public System.String Name { get; set; }

        ///<summary>
        ///LockMemo
        ///</summary>
        public System.String LockMemo { get; set; }

        ///<summary>
        ///Description
        ///</summary>
        public System.String Description { get; set; }
        #endregion
    }

    public class ToureliteEntity
    {

        #region Public Properties

        ///<summary>
        ///Idx
        ///</summary>
        public System.Int32 Idx { get; set; }

        ///<summary>
        ///Name
        ///</summary>
        public System.String Name { get; set; }

        ///<summary>
        ///解锁提示
        ///</summary>
        public System.String LockMemo { get; set; }

        ///<summary>
        ///描述
        ///</summary>
        public System.String Description { get; set; }

        /// <summary>
        /// 需要经理等级
        /// </summary>
        public int ManagerLevel { get; set; }
        /// <summary>
        /// 综合实力
        /// </summary>
        public int kpi { get; set; }
        #endregion
    }

    public class EquipmentsuitEntity
    {
        ///<summary>
        ///Idx
        ///</summary>
        public System.Int32 Idx { get; set; }

        ///<summary>
        ///套装类型
        ///</summary>
        public System.Int32 SuitType { get; set; }

        ///<summary>
        ///套装名称
        ///</summary>
        public System.String Name { get; set; }

        ///<summary>
        ///3件套描述
        ///</summary>
        public System.String Memo1 { get; set; }

        ///<summary>
        ///5件套描述
        ///</summary>
        public System.String Memo2 { get; set; }

        ///<summary>
        ///7件套描述
        ///</summary>
        public System.String Memo3 { get; set; }
    }

    public class ScoutingEntity
    {
        public int Idx { get; set; }

        public string Name { get; set; }

        public bool HasTen { get; set; }

        public string Description { get; set; }
    }

    public class TaskDescriptionEntity
    {
        public int Idx { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public string Description { get; set; }

        ///<summary>
        ///奖励经验
        ///</summary>
        public System.Int32 PrizeExp { get; set; }

        ///<summary>
        ///奖励游戏币
        ///</summary>
        public System.Int32 PrizeCoin { get; set; }

        ///<summary>
        ///奖励物品
        ///</summary>
        public System.Int32 PrizeItemCode { get; set; }
        /// <summary>
        /// 具体奖励物品以|分割
        /// </summary>
        public System.String PrizeItems { get; set; }

        public string RequireFuncs { get; set; }

        public int TaskType { get; set; }

        public int Times { get; set; }

        /// <summary>
        /// 巡回赛任务专用
        /// </summary>
        public int NpcIdx { get; set; }
    }

  
    public class LadderExchangeEntity
    {
        ///<summary>
        ///Idx
        ///</summary>
        public System.Int32 Idx { get; set; }

        /////<summary>
        /////NeedScore
        /////</summary>
        //public System.Int32 NeedScore { get; set; }

        ///<summary>
        ///CostHonor
        ///</summary>
        public System.Int32 CostHonor { get; set; }

        /// <summary>
        /// 天梯币
        /// </summary>
        public int LadderCoin { get; set; }
    }

    public class LeagueExchangeEntity
    {

        ///<summary>
        ///Idx
        ///</summary>
        public System.Int32 Idx { get; set; }


        ///<summary>
        ///CostHonor
        ///</summary>
        public System.Int32 CostScore { get; set; }
    }


    public class WpfConfigFunctionopenEntity
    {
        #region Public Properties

        ///<summary>
        ///Idx
        ///</summary>
        public System.Int32 Idx { get; set; }

        ///<summary>
        ///Name
        ///</summary>
        public System.String Name { get; set; }

        ///<summary>
        ///ManagerLevel
        ///</summary>
        public System.Int32 ManagerLevel { get; set; }

        ///<summary>
        ///LockMemo
        ///</summary>
        public System.String LockMemo { get; set; }
        #endregion
    }

    public class BuffTipsEntity
    {
        public BuffTipsEntity()
        { }

        public BuffTipsEntity(DicBuffEntity dic)
        {
            this.BuffId = dic.BuffId;
            this.BuffName = dic.BuffName;
            this.Icon = dic.Icon;
            this.BuffMemo = dic.Memo;
        }

        public int BuffId { get; set; }
        public string BuffName { get; set; }
        public string Icon { get; set; }
        public string BuffMemo { get; set; }
    }
    public class BuffSrcTipsEntity
    {
        public BuffSrcTipsEntity()
        { }
        public BuffSrcTipsEntity(DicSkillEntity dic)
        {
            this.SkillCode = dic.SkillCode;
            this.SkillLevel = dic.SkillLevel;
            this.SkillName = dic.SkillName;
        }
        public string SkillCode { get; set; }
        public int SkillLevel { get; set; }
        public string SkillName { get; set; }
    }

    public class CrossSiteTipsEntity
    {
        public CrossSiteTipsEntity()
        { }
        public CrossSiteTipsEntity(AllZoneinfoEntity src)
        {
            this.SiteId = src.ZoneName;
            this.SiteName = src.Name;
        }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
    }
}
