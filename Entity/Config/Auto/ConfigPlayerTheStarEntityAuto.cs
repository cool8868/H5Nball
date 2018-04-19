
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_PlayerTheStar 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigPlayerthestarEntity
	{
		
		public ConfigPlayerthestarEntity()
		{
		}

		public ConfigPlayerthestarEntity(
		System.Int32 idx
,				System.Int32 exp
,				System.Int32 coin
,				System.Int32 playercard
,				System.Int32 potentialcount
		)
		{
			this.Idx = idx;
			this.Exp = exp;
			this.Coin = coin;
			this.PlayerCard = playercard;
			this.PotentialCount = potentialcount;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///需要星级
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Exp {get ; set ;}

		///<summary>
		///每次升星需要金币
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Coin {get ; set ;}

		///<summary>
		///需要球员卡数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PlayerCard {get ; set ;}

		///<summary>
		///可获得潜力数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PotentialCount {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigPlayerthestarEntity Clone()
        {
            ConfigPlayerthestarEntity entity = new ConfigPlayerthestarEntity();
			entity.Idx = this.Idx;
			entity.Exp = this.Exp;
			entity.Coin = this.Coin;
			entity.PlayerCard = this.PlayerCard;
			entity.PotentialCount = this.PotentialCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_PlayerTheStar 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigPlayerthestarResponse : BaseResponse<ConfigPlayerthestarEntity>
    {

    }
}
