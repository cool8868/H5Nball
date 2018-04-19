
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Activity_Record 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityRecordEntity
	{
		
		public ActivityRecordEntity()
		{
		}

		public ActivityRecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 activityid
,				System.Int32 activitystep
,				System.String steprecord
,				System.DateTime recorddate
,				System.DateTime settlementdate
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Byte[] rowversion
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ActivityId = activityid;
			this.ActivityStep = activitystep;
			this.StepRecord = steprecord;
			this.RecordDate = recorddate;
			this.SettlementDate = settlementdate;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.RowVersion = rowversion;
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
		///SettlementDate
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime SettlementDate {get ; set ;}

		///<summary>
		///是否可领奖,0：否；1：是；2：今日已领完
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityRecordEntity Clone()
        {
            ActivityRecordEntity entity = new ActivityRecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ActivityId = this.ActivityId;
			entity.ActivityStep = this.ActivityStep;
			entity.StepRecord = this.StepRecord;
			entity.RecordDate = this.RecordDate;
			entity.SettlementDate = this.SettlementDate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Activity_Record 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityRecordResponse : BaseResponse<ActivityRecordEntity>
    {

    }
}

