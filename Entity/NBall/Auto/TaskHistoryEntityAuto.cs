
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Task_History 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TaskHistoryEntity
	{
		
		public TaskHistoryEntity()
		{
		}

		public TaskHistoryEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 taskid
,				System.Int32 curtimes
,				System.String steprecord
,				System.Byte[] doneparam
,				System.Int32 prizeexp
,				System.Int32 prizecoin
,				System.Int32 prizeitemcode
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.TaskId = taskid;
			this.CurTimes = curtimes;
			this.StepRecord = steprecord;
			this.DoneParam = doneparam;
			this.PrizeExp = prizeexp;
			this.PrizeCoin = prizecoin;
			this.PrizeItemCode = prizeitemcode;
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
		///任务id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 TaskId {get ; set ;}

		///<summary>
		///CurTimes
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CurTimes {get ; set ;}

		///<summary>
		///任务执行情况
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String StepRecord {get ; set ;}

		///<summary>
		///任务执行参数记录，要求唯一id的任务需要用到
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.Byte[] DoneParam {get ; set ;}

		///<summary>
		///PrizeExp
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 PrizeExp {get ; set ;}

		///<summary>
		///PrizeCoin
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PrizeCoin {get ; set ;}

		///<summary>
		///PrizeItemCode
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 PrizeItemCode {get ; set ;}

		///<summary>
		///任务状态：0，初始；1，已完成；2，放弃
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public TaskHistoryEntity Clone()
        {
            TaskHistoryEntity entity = new TaskHistoryEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.TaskId = this.TaskId;
			entity.CurTimes = this.CurTimes;
			entity.StepRecord = this.StepRecord;
			entity.DoneParam = this.DoneParam;
			entity.PrizeExp = this.PrizeExp;
			entity.PrizeCoin = this.PrizeCoin;
			entity.PrizeItemCode = this.PrizeItemCode;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Task_History 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TaskHistoryResponse : BaseResponse<TaskHistoryEntity>
    {

    }
}

