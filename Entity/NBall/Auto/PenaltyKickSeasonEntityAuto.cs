
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.PenaltyKick_Season 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class PenaltykickSeasonEntity
	{
		
		public PenaltykickSeasonEntity()
		{
		}

		public PenaltykickSeasonEntity(
		System.Int32 idx
,				System.DateTime starttime
,				System.DateTime endtime
,				System.Boolean isprize
,				System.DateTime prizetime
		)
		{
			this.Idx = idx;
			this.StartTime = starttime;
			this.EndTime = endtime;
			this.IsPrize = isprize;
			this.PrizeTime = prizetime;
		}
		
		#region Public Properties
		
		///<summary>
		///赛季ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///开始时间
		///</summary>
        [DataMember]
        [ProtoMember(2)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///结束时间
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///是否发奖
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Boolean IsPrize {get ; set ;}

		///<summary>
		///发奖时间
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime PrizeTime {get ; set ;}
		#endregion
        
        #region Clone
        public PenaltykickSeasonEntity Clone()
        {
            PenaltykickSeasonEntity entity = new PenaltykickSeasonEntity();
			entity.Idx = this.Idx;
			entity.StartTime = this.StartTime;
			entity.EndTime = this.EndTime;
			entity.IsPrize = this.IsPrize;
			entity.PrizeTime = this.PrizeTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.PenaltyKick_Season 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class PenaltykickSeasonResponse : BaseResponse<PenaltykickSeasonEntity>
    {

    }
}
