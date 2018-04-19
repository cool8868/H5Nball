
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_EuropePrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigEuropeprizeEntity
	{
		
		public ConfigEuropeprizeEntity()
		{
		}

		public ConfigEuropeprizeEntity(
		System.Int32 idx
,				System.Int32 step
,				System.Int32 winnumber
,				System.Int32 prizetype
,				System.Int32 prizecode
,				System.Int32 prizecount
		)
		{
			this.Idx = idx;
			this.Step = step;
			this.WinNumber = winnumber;
			this.PrizeType = prizetype;
			this.PrizeCode = prizecode;
			this.PrizeCount = prizecount;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///活动步骤
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Step {get ; set ;}

		///<summary>
		///需要获胜场次
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 WinNumber {get ; set ;}

		///<summary>
		///奖励类型 1钻石 2金币 3物品
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///奖励物品
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PrizeCode {get ; set ;}

		///<summary>
		///物品数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 PrizeCount {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigEuropeprizeEntity Clone()
        {
            ConfigEuropeprizeEntity entity = new ConfigEuropeprizeEntity();
			entity.Idx = this.Idx;
			entity.Step = this.Step;
			entity.WinNumber = this.WinNumber;
			entity.PrizeType = this.PrizeType;
			entity.PrizeCode = this.PrizeCode;
			entity.PrizeCount = this.PrizeCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_EuropePrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigEuropeprizeResponse : BaseResponse<ConfigEuropeprizeEntity>
    {

    }
}

