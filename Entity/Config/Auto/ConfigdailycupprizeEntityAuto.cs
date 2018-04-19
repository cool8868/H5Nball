
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_DailycupPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigDailycupprizeEntity
	{
		
		public ConfigDailycupprizeEntity()
		{
		}

		public ConfigDailycupprizeEntity(
		System.Int32 idx
,				System.Int32 rank
,				System.Int32 worldscore
,				System.Int32 sophisticate
,				System.Int32 coin
,				System.String prizeitems
		)
		{
			this.Idx = idx;
			this.Rank = rank;
			this.WorldScore = worldscore;
			this.Sophisticate = sophisticate;
			this.Coin = coin;
			this.PrizeItems = prizeitems;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///杯赛排名，从冠军开始
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Rank {get ; set ;}

		///<summary>
		///冠军杯积分
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 WorldScore {get ; set ;}

		///<summary>
		///阅历
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Sophisticate {get ; set ;}

		///<summary>
		///金币
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Coin {get ; set ;}

		///<summary>
		///PrizeItems
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String PrizeItems {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigDailycupprizeEntity Clone()
        {
            ConfigDailycupprizeEntity entity = new ConfigDailycupprizeEntity();
			entity.Idx = this.Idx;
			entity.Rank = this.Rank;
			entity.WorldScore = this.WorldScore;
			entity.Sophisticate = this.Sophisticate;
			entity.Coin = this.Coin;
			entity.PrizeItems = this.PrizeItems;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_DailycupPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigDailycupprizeResponse : BaseResponse<ConfigDailycupprizeEntity>
    {

    }
}

