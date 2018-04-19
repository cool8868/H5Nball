using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Games.NBall.WpfEmulator.Entity
{

    public class ItemTipsEntity
    {

        public ItemTipsEntity()
        {
            Summary = new List<WpfSummaryEntity>();
            Summary.Add(new WpfSummaryEntity("PlayerCard", "球员卡信息"));
            Summary.Add(new WpfSummaryEntity("Equipment", "装备信息"));
            Summary.Add(new WpfSummaryEntity("MallItem", "商城道具信息"));
            Summary.Add(new WpfSummaryEntity("Ballsoul", "球魂信息"));
            Summary.Add(new WpfSummaryEntity("SuitDrawing", "套装图纸信息"));
            Summary.Add(new WpfSummaryEntity("TheBadge", "球星徽章"));
            Summary.Add(new WpfSummaryEntity("ClubClothes", "经典球衣"));
        }

        public List<WpfSummaryEntity> Summary { get; set; }

        /// <summary>
        /// 球员卡
        /// </summary>
        public List<PlayerCardDescriptionEntity> PlayerCard { get; set; }

        /// <summary>
        /// 装备
        /// </summary>
        public List<EquipmentDescriptionEntity> Equipment { get; set; }
        /// <summary>
        /// 商城道具
        /// </summary>
        public List<MallItemDescriptionEntity> MallItem { get; set; }
        /// <summary>
        /// 球魂
        /// </summary>
        public List<BallsoulDescriptionEntity> Ballsoul { get; set; }
        /// <summary>
        /// 套装图纸
        /// </summary>
        public List<SuitDrawingDescriptionEntity> SuitDrawing { get; set; }

        /// <summary>
        /// 徽章
        /// </summary>
        public List<DicThebadgementDescriptionEntity> TheBadge { get; set; }


        /// <summary>
        /// 经典球衣
        /// </summary>
        public List<DicClubClothesmentDescriptionEntity> ClubClothes { get; set; }
    }

    public class DescriptionEntity
    {
        /// <summary>
        /// itemcode
        /// </summary>
        public int ItemCode { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 物品类型:1,球员卡;2,装备3,商城道具;4,球魂;5,图纸;
        /// </summary>
        public int ItemType { get; set; }
        /// <summary>
        /// 图片id
        /// </summary>
        public int ImageId { get; set; }

        public string Description { get; set; }
    }

    public class PlayerCardDescriptionEntity : DescriptionEntity
    {

        public int PlayerId { get; set; }
        /// <summary>
        /// 所有位置
        /// </summary>
        public string AllPosition { get; set; }
        /// <summary>
        /// 擅长位置
        /// </summary>
        public double Specific { get; set; }

        ///<summary>
        ///场上位置
        ///</summary>
        public System.Int32 Position { get; set; }
        ///<summary>
        ///卡牌颜色
        ///</summary>
        public System.Int32 CardLevel { get; set; }
        ///<summary>
        ///联赛级别
        ///</summary>
        public System.Int32 LeagueLevel { get; set; }
        ///<summary>
        ///关键属性
        ///</summary>
        public System.Double Kpi { get; set; }
        /// <summary>
        /// 评级
        /// </summary>
        public System.String KpiLevel { get; set; }
        /// <summary>
        /// 能力值
        /// </summary>
        public System.Double Capacity { get; set; }

        ///<summary>
        ///进攻
        ///</summary>
        public System.Double Attack { get; set; }
        /// <summary>
        /// 身体
        /// </summary>
        public System.Double Body { get; set; }
        /// <summary>
        /// 组织
        /// </summary>
        public System.Double Organize { get; set; }
        /// <summary>
        /// 防守
        /// </summary>
        public System.Double Defense { get; set; }
        /// <summary>
        /// 守门
        /// </summary>
        public System.Double Goalkeep { get; set; }
        /// <summary>
        /// 球星技能
        /// </summary>
        public string Starskill { get; set; }
        /// <summary>
        /// 球星技能Code
        /// </summary>
        public string StarskillCode { get; set; }
        /// <summary>
        /// 组合技能
        /// </summary>
        public string CombSkill { get; set; }
        /// <summary>
        /// 俱乐部
        /// </summary>
        public string Club { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public string Stature { get; set; }
        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string Nationality { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        ///<summary>
        ///速度
        ///</summary>
        public System.Double Speed { get; set; }

        ///<summary>
        ///射门
        ///</summary>
        public System.Double Shoot { get; set; }

        ///<summary>
        ///任意球
        ///</summary>
        public System.Double FreeKick { get; set; }

        ///<summary>
        ///控制
        ///</summary>
        public System.Double Balance { get; set; }

        ///<summary>
        ///体质
        ///</summary>
        public System.Double Physique { get; set; }

        ///<summary>
        ///弹跳
        ///</summary>
        public System.Double Bounce { get; set; }

        ///<summary>
        ///侵略性
        ///</summary>
        public System.Double Aggression { get; set; }

        ///<summary>
        ///干扰
        ///</summary>
        public System.Double Disturb { get; set; }

        ///<summary>
        ///抢断
        ///</summary>
        public System.Double Interception { get; set; }

        ///<summary>
        ///控球
        ///</summary>
        public System.Double Dribble { get; set; }

        ///<summary>
        ///传球
        ///</summary>
        public System.Double Pass { get; set; }

        ///<summary>
        ///意识
        ///</summary>
        public System.Double Mentality { get; set; }

        ///<summary>
        ///反应
        ///</summary>
        public System.Double Response { get; set; }

        ///<summary>
        ///位置感
        ///</summary>
        public System.Double Positioning { get; set; }

        ///<summary>
        ///手控球
        ///</summary>
        public System.Double HandControl { get; set; }

        ///<summary>
        ///加速度
        ///</summary>
        public System.Double Acceleration { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        public Double Power { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnName { get;set; }
    }

    public class DicThebadgementDescriptionEntity : DescriptionEntity
    {
        /// <summary>
        /// 徽章ID
        /// </summary>
        public int Idx { get; set; }

    }

    public class DicClubClothesmentDescriptionEntity : DescriptionEntity
    {
        /// <summary>
        /// 经典球衣ID
        /// </summary>
        public int Idx { get; set; }

        /// <summary>
        /// 是否可以鉴定
        /// </summary>
        public bool IsAuthenticate { get; set; }

        /// <summary>
        /// 品质
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// 球衣号码
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 第一个属性强化增加属性百分比
        /// </summary>
        public decimal Plus1 { get; set; }
        /// <summary>
        /// 第二个属性强化增加属性百分比
        /// </summary>
        public decimal Plus2 { get; set; }
        /// <summary>
        /// 第三个属性强化增加属性百分比
        /// </summary>
        public decimal Plus3 { get; set; }

        /// <summary>
        /// 俱乐部ID
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// 基础属性1
        /// </summary>
        public decimal Base1 { get; set; }

        /// <summary>
        /// 基础属性2
        /// </summary>
        public decimal Base2 { get; set; }

        /// <summary>
        /// 基础属性3
        /// </summary>
        public decimal Base3 { get; set; }

        /// <summary>
        /// 对应球星ID
        /// </summary>
        public int PlayerId { get; set; }

    }

    public class EquipmentDescriptionEntity : DescriptionEntity
    {
        /// <summary>
        /// 装备id
        /// </summary>
        public int Idx { get; set; }
        /// <summary>
        /// 套装类型：1,7件套套装;2,5件套套装;3,3件套套装;4,散装;
        /// </summary>
        public int SuitType { get; set; }
        /// <summary>
        /// 套装id
        /// </summary>
        public int SuitId { get; set; }
        /// <summary>
        /// 品质
        /// </summary>
        public int Quality { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        public int Prefix { get; set; }

        public int Property1 { get; set; }

        public int Property2 { get; set; }
    }
    /// <summary>
    /// 商城道具
    /// </summary>
    public class MallItemDescriptionEntity : DescriptionEntity
    {
        public int Idx { get; set; }

        ///<summary>
        ///道具品质:1,橙色；2，紫色；3，蓝色；4，绿色
        ///</summary>
        public System.Int32 Quality { get; set; }

        /// <summary>
        /// 使用需等级
        /// </summary>
        public int UseLevel { get; set; }

        ///<summary>
        ///商品介绍
        ///</summary>
        public System.String ItemIntro { get; set; }

        ///<summary>
        ///提示
        ///</summary>
        public System.String ItemTip { get; set; }

        ///<summary>
        ///使用方法
        ///</summary>
        public System.String UseNote { get; set; }

        ///<summary>
        ///使用结果提示
        ///</summary>
        public System.String UseMsg { get; set; }

        public int ShowUse { get; set; }

        public int ShowBatch { get; set; }
    }
    /// <summary>
    /// 球魂
    /// </summary>
    public class BallsoulDescriptionEntity : DescriptionEntity
    {
        public int Idx { get; set; }
        /// <summary>
        /// 球魂颜色
        /// </summary>
        public int Color { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public int Type { get; set; }

        public int Level { get; set; }
    }

    public class SuitDrawingDescriptionEntity : DescriptionEntity
    {
        public int Idx { get; set; }
        /// <summary>
        /// 品质
        /// </summary>
        public int SuitType { get; set; }

        public int SuitId { get; set; }

        public string UseNote { get; set; }

        public int Color { get; set; }

        public int UseLevel { get; set; }

        public string FormulaItemString { get; set; }
    }
}
