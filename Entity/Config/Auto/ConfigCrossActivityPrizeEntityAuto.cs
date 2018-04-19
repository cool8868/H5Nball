
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CrossActivityPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCrossactivityprizeEntity
	{
		
		public ConfigCrossactivityprizeEntity()
		{
		}

		public ConfigCrossactivityprizeEntity(
		System.Int32 idx
,				System.Int32 activityid
,				System.Int32 prizeid
,				System.Int32 prizetype
,				System.Int32 prizecode
,				System.Int32 prizecount
,				System.Int32 prizecount2
,				System.Int32 rate
		)
		{
			this.Idx = idx;
			this.ActivityId = activityid;
			this.PrizeId = prizeid;
			this.PrizeType = prizetype;
			this.PrizeCode = prizecode;
			this.PrizeCount = prizecount;
			this.PrizeCount2 = prizecount2;
			this.Rate = rate;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///活动ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ActivityId {get ; set ;}

		///<summary>
		///奖励ID
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizeId {get ; set ;}

		///<summary>
		///奖励物品类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///奖励物品code
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PrizeCode {get ; set ;}

		///<summary>
		///奖励数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 PrizeCount {get ; set ;}

		///<summary>
		///PrizeCount2
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 PrizeCount2 {get ; set ;}

		///<summary>
		///概率
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Rate {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCrossactivityprizeEntity Clone()
        {
            ConfigCrossactivityprizeEntity entity = new ConfigCrossactivityprizeEntity();
			entity.Idx = this.Idx;
			entity.ActivityId = this.ActivityId;
			entity.PrizeId = this.PrizeId;
			entity.PrizeType = this.PrizeType;
			entity.PrizeCode = this.PrizeCode;
			entity.PrizeCount = this.PrizeCount;
			entity.PrizeCount2 = this.PrizeCount2;
			entity.Rate = this.Rate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CrossActivityPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCrossactivityprizeResponse : BaseResponse<ConfigCrossactivityprizeEntity>
    {

    }
}
