
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Activity_History 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityHistoryEntity
	{
		
		public ActivityHistoryEntity()
		{
		}

		public ActivityHistoryEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 activityid
,				System.Int32 activitystep
,				System.String steprecord
,				System.DateTime recorddate
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ActivityId = activityid;
			this.ActivityStep = activitystep;
			this.StepRecord = steprecord;
			this.RecordDate = recorddate;
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
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///活动id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ActivityId {get ; set ;}

		///<summary>
		///活动步骤
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ActivityStep {get ; set ;}

		///<summary>
		///步骤参数记录
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String StepRecord {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityHistoryEntity Clone()
        {
            ActivityHistoryEntity entity = new ActivityHistoryEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ActivityId = this.ActivityId;
			entity.ActivityStep = this.ActivityStep;
			entity.StepRecord = this.StepRecord;
			entity.RecordDate = this.RecordDate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Activity_History 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityHistoryResponse : BaseResponse<ActivityHistoryEntity>
    {

    }
}

