
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ActivityEx_Record 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityexRecordEntity
	{
		
		public ActivityexRecordEntity()
		{
		}

		public ActivityexRecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 zoneactivityid
,				System.Int32 excitingid
,				System.Int32 groupid
,				System.Int32 curdata
,				System.Int32 exdata
,				System.Int32 exstep
,				System.Int32 receivetimes
,				System.DateTime recorddate
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Byte[] rowversion
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ZoneActivityId = zoneactivityid;
			this.ExcitingId = excitingid;
			this.GroupId = groupid;
			this.CurData = curdata;
			this.ExData = exdata;
			this.ExStep = exstep;
			this.ReceiveTimes = receivetimes;
			this.RecordDate = recorddate;
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
		///ZoneActivityId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ZoneActivityId {get ; set ;}

		///<summary>
		///ExcitingId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ExcitingId {get ; set ;}

		///<summary>
		///GroupId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 GroupId {get ; set ;}

		///<summary>
		///CurData
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 CurData {get ; set ;}

		///<summary>
		///ExData
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 ExData {get ; set ;}

		///<summary>
		///ExStep
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 ExStep {get ; set ;}

		///<summary>
		///ReceiveTimes
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 ReceiveTimes {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///是否可领奖,0：否；1：是；2：今日已领完
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityexRecordEntity Clone()
        {
            ActivityexRecordEntity entity = new ActivityexRecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ZoneActivityId = this.ZoneActivityId;
			entity.ExcitingId = this.ExcitingId;
			entity.GroupId = this.GroupId;
			entity.CurData = this.CurData;
			entity.ExData = this.ExData;
			entity.ExStep = this.ExStep;
			entity.ReceiveTimes = this.ReceiveTimes;
			entity.RecordDate = this.RecordDate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ActivityEx_Record 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityexRecordResponse : BaseResponse<ActivityexRecordEntity>
    {

    }
}

