
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_PrpoSell 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigPrposellEntity
	{
		
		public ConfigPrposellEntity()
		{
		}

		public ConfigPrposellEntity(
		System.Int32 idx
,				System.Int32 itemtype
,				System.Int32 quality
,				System.Int32 coin
		)
		{
			this.Idx = idx;
			this.ItemType = itemtype;
			this.Quality = quality;
			this.Coin = coin;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///道具类型 1球员  2装备 3商城物品
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ItemType {get ; set ;}

		///<summary>
		///品质
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Quality {get ; set ;}

		///<summary>
		///获得的金币
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Coin {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigPrposellEntity Clone()
        {
            ConfigPrposellEntity entity = new ConfigPrposellEntity();
			entity.Idx = this.Idx;
			entity.ItemType = this.ItemType;
			entity.Quality = this.Quality;
			entity.Coin = this.Coin;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_PrpoSell 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigPrposellResponse : BaseResponse<ConfigPrposellEntity>
    {

    }
}

