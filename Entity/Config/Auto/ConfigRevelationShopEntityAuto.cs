
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationShop 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationshopEntity
	{
		
		public ConfigRevelationshopEntity()
		{
		}

		public ConfigRevelationshopEntity(
		System.Int32 idx
,				System.Int32 itemtype
,				System.Int32 subtype
,				System.Int32 itemcount
,				System.Int32 price
		)
		{
			this.Idx = idx;
			this.ItemType = itemtype;
			this.SubType = subtype;
			this.ItemCount = itemcount;
			this.Price = price;
		}
		
		#region Public Properties
		
		///<summary>
		///物品ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///价格
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ItemType {get ; set ;}

		///<summary>
		///经理达到等级
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///通关关卡要求
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemCount {get ; set ;}

		///<summary>
		///是否启用
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Price {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationshopEntity Clone()
        {
            ConfigRevelationshopEntity entity = new ConfigRevelationshopEntity();
			entity.Idx = this.Idx;
			entity.ItemType = this.ItemType;
			entity.SubType = this.SubType;
			entity.ItemCount = this.ItemCount;
			entity.Price = this.Price;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationShop 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationshopResponse : BaseResponse<ConfigRevelationshopEntity>
    {

    }
}
