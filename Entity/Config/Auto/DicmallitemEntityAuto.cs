
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_MallItem 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicMallitemEntity
	{
		
		public DicMallitemEntity()
		{
		}

		public DicMallitemEntity(
		System.Int32 mallcode
,				System.String name
,				System.Int32 malltype
,				System.Int32 quality
,				System.Int32 showorder
,				System.Int32 imageid
,				System.String itemintro
,				System.String itemtip
,				System.String usenote
,				System.String usemsg
,				System.Int32 uselevel
,				System.Boolean showuse
,				System.Int32 currencytype
,				System.Int32 currencycount
,				System.String currencydiscount
,				System.Int32 effecttype
,				System.Int32 effectvalue
,				System.Boolean showflag
,				System.Boolean hotflag
,				System.Boolean packageflag
,				System.Boolean showbatch
		)
		{
			this.MallCode = mallcode;
			this.Name = name;
			this.MallType = malltype;
			this.Quality = quality;
			this.ShowOrder = showorder;
			this.ImageId = imageid;
			this.ItemIntro = itemintro;
			this.ItemTip = itemtip;
			this.UseNote = usenote;
			this.UseMsg = usemsg;
			this.UseLevel = uselevel;
			this.ShowUse = showuse;
			this.CurrencyType = currencytype;
			this.CurrencyCount = currencycount;
			this.CurrencyDiscount = currencydiscount;
			this.EffectType = effecttype;
			this.EffectValue = effectvalue;
			this.ShowFlag = showflag;
			this.HotFlag = hotflag;
			this.PackageFlag = packageflag;
			this.ShowBatch = showbatch;
		}
		
		#region Public Properties
		
		///<summary>
		///商品Id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 MallCode {get ; set ;}

		///<summary>
		///商品名称
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///商品类别
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MallType {get ; set ;}

		///<summary>
		///道具品质:1,橙色；2，紫色；3，蓝色；4，绿色
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Quality {get ; set ;}

		///<summary>
		///ShowOrder
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ShowOrder {get ; set ;}

		///<summary>
		///商品图标
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ImageId {get ; set ;}

		///<summary>
		///商品介绍
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String ItemIntro {get ; set ;}

		///<summary>
		///提示
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String ItemTip {get ; set ;}

		///<summary>
		///使用方法
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String UseNote {get ; set ;}

		///<summary>
		///使用结果提示
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String UseMsg {get ; set ;}

		///<summary>
		///使用需等级
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 UseLevel {get ; set ;}

		///<summary>
		///是否显示使用按钮
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Boolean ShowUse {get ; set ;}

		///<summary>
		///货币类型：1，点券；2，金币
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 CurrencyType {get ; set ;}

		///<summary>
		///货币数量
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 CurrencyCount {get ; set ;}

		///<summary>
		///点券折扣，按时间配置，格式：0,0~100&77870,122510~60
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String CurrencyDiscount {get ; set ;}

		///<summary>
		///效果类型
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 EffectType {get ; set ;}

		///<summary>
		///效果值
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 EffectValue {get ; set ;}

		///<summary>
		///是否在商城显示
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Boolean ShowFlag {get ; set ;}

		///<summary>
		///是否在热卖里显示
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Boolean HotFlag {get ; set ;}

		///<summary>
		///是否进背包
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Boolean PackageFlag {get ; set ;}

		///<summary>
		///ShowBatch
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Boolean ShowBatch {get ; set ;}
		#endregion
        
        #region Clone
        public DicMallitemEntity Clone()
        {
            DicMallitemEntity entity = new DicMallitemEntity();
			entity.MallCode = this.MallCode;
			entity.Name = this.Name;
			entity.MallType = this.MallType;
			entity.Quality = this.Quality;
			entity.ShowOrder = this.ShowOrder;
			entity.ImageId = this.ImageId;
			entity.ItemIntro = this.ItemIntro;
			entity.ItemTip = this.ItemTip;
			entity.UseNote = this.UseNote;
			entity.UseMsg = this.UseMsg;
			entity.UseLevel = this.UseLevel;
			entity.ShowUse = this.ShowUse;
			entity.CurrencyType = this.CurrencyType;
			entity.CurrencyCount = this.CurrencyCount;
			entity.CurrencyDiscount = this.CurrencyDiscount;
			entity.EffectType = this.EffectType;
			entity.EffectValue = this.EffectValue;
			entity.ShowFlag = this.ShowFlag;
			entity.HotFlag = this.HotFlag;
			entity.PackageFlag = this.PackageFlag;
			entity.ShowBatch = this.ShowBatch;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_MallItem 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicMallitemResponse : BaseResponse<DicMallitemEntity>
    {

    }
}

