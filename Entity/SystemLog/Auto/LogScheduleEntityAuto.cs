
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Log_Schedule 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LogScheduleEntity
	{
		
		public LogScheduleEntity()
		{
		}

		public LogScheduleEntity(
		System.Int32 idx
,				System.Int32 scheduleid
,				System.Int32 appid
,				System.String terminalip
,				System.DateTime starttime
,				System.DateTime nextinvoketime
,				System.DateTime endtime
,				System.Int32 status
,				System.Int32 returncode
,				System.Int32 retrytimes
		)
		{
			this.Idx = idx;
			this.ScheduleId = scheduleid;
			this.AppId = appid;
			this.TerminalIp = terminalip;
			this.StartTime = starttime;
			this.NextInvokeTime = nextinvoketime;
			this.EndTime = endtime;
			this.Status = status;
			this.ReturnCode = returncode;
			this.RetryTimes = retrytimes;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///计划任务id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ScheduleId {get ; set ;}

		///<summary>
		///AppId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 AppId {get ; set ;}

		///<summary>
		///ip地址
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String TerminalIp {get ; set ;}

		///<summary>
		///开始时间
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///NextInvokeTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime NextInvokeTime {get ; set ;}

		///<summary>
		///结束时间
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///状态:1，开始；2，手工退出；3，成功；4，执行失败
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///ReturnCode
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 ReturnCode {get ; set ;}

		///<summary>
		///RetryTimes
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 RetryTimes {get ; set ;}
		#endregion
        
        #region Clone
        public LogScheduleEntity Clone()
        {
            LogScheduleEntity entity = new LogScheduleEntity();
			entity.Idx = this.Idx;
			entity.ScheduleId = this.ScheduleId;
			entity.AppId = this.AppId;
			entity.TerminalIp = this.TerminalIp;
			entity.StartTime = this.StartTime;
			entity.NextInvokeTime = this.NextInvokeTime;
			entity.EndTime = this.EndTime;
			entity.Status = this.Status;
			entity.ReturnCode = this.ReturnCode;
			entity.RetryTimes = this.RetryTimes;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Log_Schedule 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LogScheduleResponse : BaseResponse<LogScheduleEntity>
    {

    }
}

