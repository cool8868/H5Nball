
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_ArenaShop 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigArenashopEntity
	{
		
		public ConfigArenashopEntity()
		{
		}

		public ConfigArenashopEntity(
		System.Int32 idx
,				System.Int32 itemtype
,				System.Int32 itemcode
,				System.Int32 itemcount
,				System.Int32 price
		)
		{
			this.Idx = idx;
			this.ItemType = itemtype;
			this.ItemCode = itemcode;
			this.ItemCount = itemcount;
			this.Price = price;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///物品类型
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ItemType {get ; set ;}

		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///ItemCount
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemCount {get ; set ;}

		///<summary>
		///价格
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Price {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigArenashopEntity Clone()
        {
            ConfigArenashopEntity entity = new ConfigArenashopEntity();
			entity.Idx = this.Idx;
			entity.ItemType = this.ItemType;
			entity.ItemCode = this.ItemCode;
			entity.ItemCount = this.ItemCount;
			entity.Price = this.Price;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_ArenaShop 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigArenashopResponse : BaseResponse<ConfigArenashopEntity>
    {

    }
}
