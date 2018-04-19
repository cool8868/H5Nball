
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_LotteryRelation 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigLotteryrelationEntity
	{
		
		public ConfigLotteryrelationEntity()
		{
		}

		public ConfigLotteryrelationEntity(
		System.Int32 idx
,				System.Int32 lotteryid
,				System.Int32 libraryid
,				System.Int32 rate
		)
		{
			this.Idx = idx;
			this.LotteryId = lotteryid;
			this.LibraryId = libraryid;
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
		///抽奖库id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 LotteryId {get ; set ;}

		///<summary>
		///卡库id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 LibraryId {get ; set ;}

		///<summary>
		///权重
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Rate {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigLotteryrelationEntity Clone()
        {
            ConfigLotteryrelationEntity entity = new ConfigLotteryrelationEntity();
			entity.Idx = this.Idx;
			entity.LotteryId = this.LotteryId;
			entity.LibraryId = this.LibraryId;
			entity.Rate = this.Rate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_LotteryRelation 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigLotteryrelationResponse : BaseResponse<ConfigLotteryrelationEntity>
    {

    }
}

