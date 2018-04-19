
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Ladder_Info 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LadderInfoEntity
	{
		
		public LadderInfoEntity()
		{
		}

		public LadderInfoEntity(
		System.Guid idx
,				System.Int32 avgwaittime
,				System.Int32 playernumber
,				System.Int32 groups
,				System.Int32 season
,				System.DateTime starttime
,				System.DateTime groupingtime
,				System.DateTime countdowntime
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.AvgWaitTime = avgwaittime;
			this.PlayerNumber = playernumber;
			this.Groups = groups;
			this.Season = season;
			this.StartTime = starttime;
			this.GroupingTime = groupingtime;
			this.CountdownTime = countdowntime;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///等待时间
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 AvgWaitTime {get ; set ;}

		///<summary>
		///玩家数量
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PlayerNumber {get ; set ;}

		///<summary>
		///分组数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Groups {get ; set ;}

		///<summary>
		///所属赛季
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Season {get ; set ;}

		///<summary>
		///开始时间
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///分组时间
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime GroupingTime {get ; set ;}

		///<summary>
		///倒计时时间
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime CountdownTime {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public LadderInfoEntity Clone()
        {
            LadderInfoEntity entity = new LadderInfoEntity();
			entity.Idx = this.Idx;
			entity.AvgWaitTime = this.AvgWaitTime;
			entity.PlayerNumber = this.PlayerNumber;
			entity.Groups = this.Groups;
			entity.Season = this.Season;
			entity.StartTime = this.StartTime;
			entity.GroupingTime = this.GroupingTime;
			entity.CountdownTime = this.CountdownTime;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Ladder_Info 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LadderInfoResponse : BaseResponse<LadderInfoEntity>
    {

    }
}

