using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Shadow
{
    public class ShadowItemEntity : BaseShadowEntity
    {
        #region Public Properties

        ///<summary>
        ///ItemId
        ///</summary>
        public System.Guid ItemId { get; set; }

        ///<summary>
        ///ItemCode
        ///</summary>
        public System.Int32 ItemCode { get; set; }

        ///<summary>
        ///堆叠数量
        ///</summary>
        public System.Int32 ItemCount { get; set; }

        /// <summary>
        /// 物品类型
        /// </summary>
        public int ItemType { get; set; }

        ///<summary>
        ///是否绑定
        ///</summary>
        public System.Boolean IsBinding { get; set; }

        /// <summary>
        /// 物品属性
        /// </summary>
        public byte[] ItemProperty { get; set; }

        ///<summary>
        ///所属格数
        ///</summary>
        public System.Int32 GridIndex { get; set; }

        ///<summary>
        ///锁定状态
        ///</summary>
        public System.Int32 Status { get; set; }

        /// <summary>
        /// 操作数量
        /// </summary>
        public int OperationCount { get; set; }

        #endregion
    }

    public class ShadowItemPackageEntity : BaseShadowEntity
    {
        #region Public Properties
        ///<summary>
        ///背包大小
        ///</summary>
        public System.Int32 PackageSize { get; set; }

        ///<summary>
        ///物品序列化版本
        ///</summary>
        public System.Byte ItemVersion { get; set; }

        ///<summary>
        ///物品串
        ///</summary>
        public System.Byte[] ItemString { get; set; }
        #endregion

    }

    public class ShadowPandoraDecomposeEntity : BaseShadowEntity
    {
        #region Public Properties

        ///<summary>
        ///ItemId
        ///</summary>
        public System.String ItemIds { get; set; }

        ///<summary>
        ///ItemCodes
        ///</summary>
        public System.String ItemCodes { get; set; }

        ///<summary>
        ///暴击概率
        ///</summary>
        public System.Int32 CritRate { get; set; }

        ///<summary>
        ///是否暴击
        ///</summary>
        public System.Boolean IsCrit { get; set; }

        ///<summary>
        ///金币
        ///</summary>
        public System.Int32 Coin { get; set; }

        /// <summary>
        /// 装备列表
        /// </summary>
        public System.String EquipmentList { get; set; }

        #endregion

    }

    public class ShadowPandoraEquipmentwashEntity : BaseShadowEntity
    {
        #region Public Properties
        
        ///<summary>
        ///ItemId
        ///</summary>
        public System.Guid ItemId { get; set; }

        ///<summary>
        ///ItemCode
        ///</summary>
        public System.Int32 ItemCode { get; set; }

        ///<summary>
        ///LockPropertyId
        ///</summary>
        public System.Int32 LockPropertyId { get; set; }

        ///<summary>
        ///BuyStone
        ///</summary>
        public System.Boolean BuyStone { get; set; }

        ///<summary>
        ///BuyFusogen
        ///</summary>
        public System.Boolean BuyFusogen { get; set; }

        ///<summary>
        ///CostPoint
        ///</summary>
        public System.Int32 CostPoint { get; set; }
        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    public class ShadowPandoraEquipmentUpgradeEntity : BaseShadowEntity
    {
        #region Public Properties

        public System.Guid ManagerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Guid ItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 CurLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ResultLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Properties { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 CostCoin { get; set; }

        #endregion
    }

    public class ShadowPandoraEquipmentSellEntity : BaseShadowEntity
    {
        #region Public Properties

        public System.Guid ManagerId { get; set; }

        public System.Guid ItemId { get; set; }

        public System.Int32 ItemCode { get; set; }

        public System.Int32 Coin { get; set; }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShadowPandoraEquipmentPrecisionCastingEntity : BaseShadowEntity
    {
        #region Public Properties

        public System.Guid ManagerId { get; set; }
        public System.Guid ItemId { get; set; }
        public System.String LockCondition { get; set; }
        public System.String ExProperties { get; set; }
        public System.String CurProperties { get; set; }
        public System.Int32 Coin{get;set;}
        public System.Int32 Point { get; set; }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class ShadowPandoraArousalEntity : BaseShadowEntity
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public System.Guid ManagerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Guid SourceCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 SourceCardLv { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 CurArousalLv { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 TarArousalLv { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Guid UseCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 UseCardStrenth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean IsBinding { get; set; }


        #endregion
    }
    public class ShadowPandoraMosaicEntity : BaseShadowEntity
    {
        #region Public Properties

        ///<summary>
        ///ItemId
        ///</summary>
        public System.Guid ItemId { get; set; }

        ///<summary>
        ///ItemCode
        ///</summary>
        public System.Int32 ItemCode { get; set; }

        ///<summary>
        ///SlotId
        ///</summary>
        public System.Int32 SlotId { get; set; }

        ///<summary>
        ///BallsoulId
        ///</summary>
        public System.Guid BallsoulId { get; set; }

        ///<summary>
        ///BallsoulItemCode
        ///</summary>
        public System.Int32 BallsoulItemCode { get; set; }
        #endregion

    }

    public class ShadowPandoraStrengthEntity : BaseShadowEntity
    {
        #region Public Properties


        ///<summary>
        ///ItemId1
        ///</summary>
        public System.Guid ItemId1 { get; set; }

        ///<summary>
        ///ItemCode1
        ///</summary>
        public System.Int32 ItemCode1 { get; set; }

        ///<summary>
        ///Strength1
        ///</summary>
        public System.Int32 Strength1 { get; set; }

        ///<summary>
        ///ItemId2
        ///</summary>
        public System.Guid ItemId2 { get; set; }

        ///<summary>
        ///ItemCode2
        ///</summary>
        public System.Int32 ItemCode2 { get; set; }

        ///<summary>
        ///Strength2
        ///</summary>
        public System.Int32 Strength2 { get; set; }

        ///<summary>
        ///IsProtect
        ///</summary>
        public System.Boolean IsProtect { get; set; }

        ///<summary>
        ///CostCoin
        ///</summary>
        public System.Int32 CostCoin { get; set; }

        ///<summary>
        ///CostPoint
        ///</summary>
        public System.Int32 CostPoint { get; set; }

        ///<summary>
        ///ResultType
        ///</summary>
        public System.Int32 ResultType { get; set; }

        ///<summary>
        ///ResultItemId
        ///</summary>
        public System.Guid ResultItemId { get; set; }

        ///<summary>
        ///ResultItemCode
        ///</summary>
        public System.Int32 ResultItemCode { get; set; }

        ///<summary>
        ///ResultStrength
        ///</summary>
        public System.Int32 ResultStrength { get; set; }
        public System.Guid LuckyItemId { get; set; }
        public System.Int32 LuckyItemCode { get; set; }
        public System.Decimal Rate { get; set; }
        #endregion

    }

    public class ShadowPandoraSynthesisEntity : BaseShadowEntity
    {
        #region Public Properties

        ///<summary>
        ///SynthesisType
        ///</summary>
        public System.Int32 SynthesisType { get; set; }
        
        ///<summary>
        ///ItemId1
        ///</summary>
        public System.Guid ItemId1 { get; set; }

        ///<summary>
        ///ItemCode1
        ///</summary>
        public System.Int32 ItemCode1 { get; set; }

        ///<summary>
        ///ItemId2
        ///</summary>
        public System.Guid ItemId2 { get; set; }

        ///<summary>
        ///ItemCode2
        ///</summary>
        public System.Int32 ItemCode2 { get; set; }

        ///<summary>
        ///ItemId3
        ///</summary>
        public System.Guid ItemId3 { get; set; }

        ///<summary>
        ///ItemCode3
        ///</summary>
        public System.Int32 ItemCode3 { get; set; }

        ///<summary>
        ///ItemId4
        ///</summary>
        public System.Guid ItemId4 { get; set; }

        ///<summary>
        ///ItemCode4
        ///</summary>
        public System.Int32 ItemCode4 { get; set; }

        ///<summary>
        ///ItemId5
        ///</summary>
        public System.Guid ItemId5 { get; set; }

        ///<summary>
        ///ItemCode5
        ///</summary>
        public System.Int32 ItemCode5 { get; set; }

        ///<summary>
        ///SuitdrawingId
        ///</summary>
        public System.Guid SuitdrawingId { get; set; }

        ///<summary>
        ///SuitdrawingItemCode
        ///</summary>
        public System.Int32 SuitdrawingItemCode { get; set; }

        ///<summary>
        ///IsProtect
        ///</summary>
        public System.Boolean IsProtect { get; set; }

        ///<summary>
        ///CostCoin
        ///</summary>
        public System.Int32 CostCoin { get; set; }

        ///<summary>
        ///CostPoint
        ///</summary>
        public System.Int32 CostPoint { get; set; }

        ///<summary>
        ///ResultType
        ///</summary>
        public System.Int32 ResultType { get; set; }

        ///<summary>
        ///ResultItemId
        ///</summary>
        public System.Guid ResultItemId { get; set; }

        ///<summary>
        ///ResultItemCode
        ///</summary>
        public System.Int32 ResultItemCode { get; set; }

        public System.Guid LuckyItemId { get; set; }
public System.Int32 LuckyItemCode { get; set; }

public System.Guid GoldFormulaItemId { get; set; }
public System.Int32 GoldFormulaItemCode { get; set; }
public System.Decimal Rate { get; set; }
        #endregion

    }

    public class ShadowPandoraMedalEntity : BaseShadowEntity
    {
        public Guid ManagerId { get; set; }

        public int Type { get; set; }

        public string ItemIds { get; set; }

        public string ItemCodes { get; set; }

        public int MedalCount { get; set; }
    }
}
