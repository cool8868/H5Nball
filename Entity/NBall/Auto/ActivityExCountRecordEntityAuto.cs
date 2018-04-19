
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ActivityEx_CountRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityexCountrecordEntity
	{
		
		public ActivityexCountrecordEntity()
		{
		}

		public ActivityexCountrecordEntity(
		System.Int32 idx
,				System.Int32 zoneactivityid
,				System.Guid managerid
,				System.Int32 excitingid
,				System.Int32 groupid
,				System.Int32 exdata
,				System.Int32 curdata
,				System.Int32 exstep
,				System.Int32 alreadysendcount
,				System.Int32 status
,				System.DateTime recorddate
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ZoneActivityId = zoneactivityid;
			this.ManagerId = managerid;
			this.ExcitingId = excitingid;
			this.GroupId = groupid;
			this.ExData = exdata;
			this.CurData = curdata;
			this.ExStep = exstep;
			this.AlreadySendCount = alreadysendcount;
			this.Status = status;
			this.RecordDate = recorddate;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ZoneActivityId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ZoneActivityId {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid ManagerId {get ; set ;}

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
		///ExData
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ExData {get ; set ;}

		///<summary>
		///CurData
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 CurData {get ; set ;}

		///<summary>
		///ExStep
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 ExStep {get ; set ;}

		///<summary>
		///已经领取过的数量
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 AlreadySendCount {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityexCountrecordEntity Clone()
        {
            ActivityexCountrecordEntity entity = new ActivityexCountrecordEntity();
			entity.Idx = this.Idx;
			entity.ZoneActivityId = this.ZoneActivityId;
			entity.ManagerId = this.ManagerId;
			entity.ExcitingId = this.ExcitingId;
			entity.GroupId = this.GroupId;
			entity.ExData = this.ExData;
			entity.CurData = this.CurData;
			entity.ExStep = this.ExStep;
			entity.AlreadySendCount = this.AlreadySendCount;
			entity.Status = this.Status;
			entity.RecordDate = this.RecordDate;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ActivityEx_CountRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityexCountrecordResponse : BaseResponse<ActivityexCountrecordEntity>
    {

    }
}

