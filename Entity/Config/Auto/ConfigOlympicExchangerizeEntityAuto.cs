
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_OlympicExchangerize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigOlympicexchangerizeEntity
	{
		
		public ConfigOlympicexchangerizeEntity()
		{
		}

		public ConfigOlympicexchangerizeEntity(
		System.Int32 idx
,				System.Int32 exchangeid
,				System.Int32 prizeitemcode
,				System.Int32 itemcount
,				System.Int32 thegoldmedalid
,				System.Int32 thegoldmedalcount
		)
		{
			this.Idx = idx;
			this.ExchangeId = exchangeid;
			this.PrizeItemCode = prizeitemcode;
			this.ItemCount = itemcount;
			this.TheGoldMedalId = thegoldmedalid;
			this.TheGoldMedalCount = thegoldmedalcount;
		}
		
		#region Public Properties
		
		///<summary>
		///0
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///兑换ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ExchangeId {get ; set ;}

		///<summary>
		///要兑换的物品
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizeItemCode {get ; set ;}

		///<summary>
		///物品数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemCount {get ; set ;}

		///<summary>
		///需要金牌ID
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 TheGoldMedalId {get ; set ;}

		///<summary>
		///需要金牌数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 TheGoldMedalCount {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigOlympicexchangerizeEntity Clone()
        {
            ConfigOlympicexchangerizeEntity entity = new ConfigOlympicexchangerizeEntity();
			entity.Idx = this.Idx;
			entity.ExchangeId = this.ExchangeId;
			entity.PrizeItemCode = this.PrizeItemCode;
			entity.ItemCount = this.ItemCount;
			entity.TheGoldMedalId = this.TheGoldMedalId;
			entity.TheGoldMedalCount = this.TheGoldMedalCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_OlympicExchangerize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigOlympicexchangerizeResponse : BaseResponse<ConfigOlympicexchangerizeEntity>
    {

    }
}
