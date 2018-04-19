
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Task_DailyRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TaskDailyrecordEntity
	{
		
		public TaskDailyrecordEntity()
		{
		}

		public TaskDailyrecordEntity(
		System.Guid managerid
,				System.Int32 dailycount
,				System.DateTime recorddate
,				System.String taskids
,				System.String curtimes
,				System.String steprecords
,				System.Byte[] doneparam
,				System.String status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Byte[] rowversion
,				System.Boolean isreceive
,				System.Int32 finishcount
		)
		{
			this.ManagerId = managerid;
			this.DailyCount = dailycount;
			this.RecordDate = recorddate;
			this.TaskIds = taskids;
			this.CurTimes = curtimes;
			this.StepRecords = steprecords;
			this.DoneParam = doneparam;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.RowVersion = rowversion;
			this.IsReceive = isreceive;
			this.FinishCount = finishcount;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///DailyCount
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DailyCount {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///任务id 以逗号分割
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String TaskIds {get ; set ;}

		///<summary>
		///CurTimes
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String CurTimes {get ; set ;}

		///<summary>
		///任务执行情况 以|分割
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String StepRecords {get ; set ;}

		///<summary>
		///任务执行参数记录，要求唯一id的任务需要用到
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.Byte[] DoneParam {get ; set ;}

		///<summary>
		///任务状态：0，初始；1，已完成；多任务逗号分割
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String Status {get ; set ;}

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

		///<summary>
		///IsReceive
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Boolean IsReceive {get ; set ;}

		///<summary>
		///FinishCount
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 FinishCount {get ; set ;}
		#endregion
        
        #region Clone
        public TaskDailyrecordEntity Clone()
        {
            TaskDailyrecordEntity entity = new TaskDailyrecordEntity();
			entity.ManagerId = this.ManagerId;
			entity.DailyCount = this.DailyCount;
			entity.RecordDate = this.RecordDate;
			entity.TaskIds = this.TaskIds;
			entity.CurTimes = this.CurTimes;
			entity.StepRecords = this.StepRecords;
			entity.DoneParam = this.DoneParam;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.RowVersion = this.RowVersion;
			entity.IsReceive = this.IsReceive;
			entity.FinishCount = this.FinishCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Task_DailyRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TaskDailyrecordResponse : BaseResponse<TaskDailyrecordEntity>
    {

    }
}

