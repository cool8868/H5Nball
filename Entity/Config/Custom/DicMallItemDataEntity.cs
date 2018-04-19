using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProtoBuf;

namespace Games.NBall.Entity.Config.Custom
{
    /// <summary>
    /// 商城道具数据封装
    /// </summary>
    [Serializable]
    [DataContract]
    public class DicMallItemDataEntity
    {
        /// <summary>
        /// 点券列表，按时间
        /// </summary>
        [JsonIgnore]
        public List<PointInTime> PointInTimes { get; set; }
        /// <summary>
        /// 原始点数
        /// </summary>
        [DataMember]
        public int RawCurrencyCount { get; set; }

        public int MysteryShopId { get; set; }

        public int MaxBuyCount { get; set; }

        #region Public Properties

        ///<summary>
        ///商品Id
        ///</summary>
        [DataMember]
        [ProtoMember(1)]
        public System.Int32 MallCode { get; set; }

        ///<summary>
        ///商品名称
        ///</summary>
        [DataMember]
        [ProtoMember(2)]
        public System.String Name { get; set; }

        ///<summary>
        ///商品类别
        ///</summary>
        [DataMember]
        [ProtoMember(3)]
        public System.Int32 MallType { get; set; }

        ///<summary>
        ///道具品质:1,橙色；2，紫色；3，蓝色；4，绿色
        ///</summary>
        [DataMember]
        [ProtoMember(4)]
        public System.Int32 Quality { get; set; }

        ///<summary>
        ///ShowOrder
        ///</summary>
        [DataMember]
        [ProtoMember(5)]
        public System.Int32 ShowOrder { get; set; }

        ///<summary>
        ///商品图标
        ///</summary>
        [DataMember]
        [ProtoMember(6)]
        public System.Int32 ImageId { get; set; }

        ///<summary>
        ///使用需等级
        ///</summary>
        [DataMember]
        [ProtoMember(11)]
        public System.Int32 UseLevel { get; set; }

        ///<summary>
        ///货币类型：1，点券；2，金币
        ///</summary>
        [DataMember]
        [ProtoMember(12)]
        public System.Int32 CurrencyType { get; set; }

        ///<summary>
        ///货币数量
        ///</summary>
        [DataMember]
        [ProtoMember(13)]
        public System.Int32 CurrencyCount { get; set; }

        ///<summary>
        ///点券折扣，按时间配置，格式：0,0~100&77870,122510~60
        ///</summary>
        [DataMember]
        [ProtoMember(14)]
        public System.String CurrencyDiscount { get; set; }

        ///<summary>
        ///效果类型
        ///</summary>
        [DataMember]
        [ProtoMember(15)]
        public System.Int32 EffectType { get; set; }

        ///<summary>
        ///效果值
        ///</summary>
        [DataMember]
        [ProtoMember(16)]
        public System.Int32 EffectValue { get; set; }

        ///<summary>
        ///是否在商城显示
        ///</summary>
        [DataMember]
        [ProtoMember(17)]
        public System.Boolean ShowFlag { get; set; }

        ///<summary>
        ///是否在热卖里显示
        ///</summary>
        [DataMember]
        [ProtoMember(18)]
        public System.Boolean HotFlag { get; set; }

        ///<summary>
        ///是否进背包
        ///</summary>
        [DataMember]
        [ProtoMember(19)]
        public System.Boolean PackageFlag { get; set; }

        [DataMember]
        [ProtoMember(20)]
        public System.String Description { get; set; }

        [DataMember]
        [ProtoMember(21)]
        public System.Boolean ShowBatch { get; set; }
        #endregion

        #region Clone
        public DicMallItemDataEntity Clone()
        {
            DicMallItemDataEntity entity = new DicMallItemDataEntity();
            entity.MallCode = this.MallCode;
            entity.MallType = this.MallType;
            entity.Quality = this.Quality;
            entity.ShowOrder = this.ShowOrder;
            entity.ImageId = this.ImageId;
            entity.RawCurrencyCount = this.RawCurrencyCount;
            entity.PointInTimes = this.PointInTimes;
            entity.CurrencyCount = this.CurrencyCount;
            entity.CurrencyType = this.CurrencyType;
            entity.UseLevel = this.UseLevel;
            entity.EffectType = this.EffectType;
            entity.EffectValue = this.EffectValue;
            entity.ShowFlag = this.ShowFlag;
            entity.HotFlag = this.HotFlag;
            entity.Description = this.Description;
            entity.Name = this.Name;
            entity.ShowBatch = this.ShowBatch;
            return entity;
        }
        #endregion
    }

    /// <summary>
    /// 点券数，按时间
    /// </summary>
    public class PointInTime
    {
        /// <summary>
        /// 点券数
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
