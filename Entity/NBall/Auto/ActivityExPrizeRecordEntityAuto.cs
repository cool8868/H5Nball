
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ActivityEx_PrizeRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityexPrizerecordEntity
	{
		
		public ActivityexPrizerecordEntity()
		{
		}

		public ActivityexPrizerecordEntity(
		System.Int32 idx
,				System.String exkey
,				System.Int32 exrecordid
,				System.Guid managerid
,				System.Int32 zoneactivityid
,				System.Int32 excitingid
,				System.Int32 groupid
,				System.Int32 curdata
,				System.Int32 exdata
,				System.Int32 exstep
,				System.Int32 receivetimes
,				System.DateTime recorddate
,				System.Int32 sendtimes
,				System.Int32 returncode
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ExKey = exkey;
			this.ExRecordId = exrecordid;
			this.ManagerId = managerid;
			this.ZoneActivityId = zoneactivityid;
			this.ExcitingId = excitingid;
			this.GroupId = groupid;
			this.CurData = curdata;
			this.ExData = exdata;
			this.ExStep = exstep;
			this.ReceiveTimes = receivetimes;
			this.RecordDate = recorddate;
			this.SendTimes = sendtimes;
			this.ReturnCode = returncode;
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
		///ExKey
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String ExKey {get ; set ;}

		///<summary>
		///ExRecordId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ExRecordId {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///ZoneActivityId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ZoneActivityId {get ; set ;}

		///<summary>
		///ExcitingId
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ExcitingId {get ; set ;}

		///<summary>
		///GroupId
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 GroupId {get ; set ;}

		///<summary>
		///CurData
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 CurData {get ; set ;}

		///<summary>
		///ExData
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 ExData {get ; set ;}

		///<summary>
		///ExStep
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 ExStep {get ; set ;}

		///<summary>
		///ReceiveTimes
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 ReceiveTimes {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///SendTimes
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 SendTimes {get ; set ;}

		///<summary>
		///ReturnCode
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 ReturnCode {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(16)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(17)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityexPrizerecordEntity Clone()
        {
            ActivityexPrizerecordEntity entity = new ActivityexPrizerecordEntity();
			entity.Idx = this.Idx;
			entity.ExKey = this.ExKey;
			entity.ExRecordId = this.ExRecordId;
			entity.ManagerId = this.ManagerId;
			entity.ZoneActivityId = this.ZoneActivityId;
			entity.ExcitingId = this.ExcitingId;
			entity.GroupId = this.GroupId;
			entity.CurData = this.CurData;
			entity.ExData = this.ExData;
			entity.ExStep = this.ExStep;
			entity.ReceiveTimes = this.ReceiveTimes;
			entity.RecordDate = this.RecordDate;
			entity.SendTimes = this.SendTimes;
			entity.ReturnCode = this.ReturnCode;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ActivityEx_PrizeRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityexPrizerecordResponse : BaseResponse<ActivityexPrizerecordEntity>
    {

    }
}

