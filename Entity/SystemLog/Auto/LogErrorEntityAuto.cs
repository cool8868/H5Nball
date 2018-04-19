
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Log_Error 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LogErrorEntity
	{
		
		public LogErrorEntity()
		{
		}

		public LogErrorEntity(
		System.Int32 idx
,				System.Int32 appid
,				System.String terminalip
,				System.Int32 moduleid
,				System.Int32 functionid
,				System.String message
,				System.String stacktrace
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.AppId = appid;
			this.TerminalIP = terminalip;
			this.ModuleId = moduleid;
			this.FunctionId = functionid;
			this.Message = message;
			this.StackTrace = stacktrace;
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
		///AppId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 AppId {get ; set ;}

		///<summary>
		///TerminalIP
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String TerminalIP {get ; set ;}

		///<summary>
		///ModuleId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ModuleId {get ; set ;}

		///<summary>
		///FunctionId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 FunctionId {get ; set ;}

		///<summary>
		///Message
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Message {get ; set ;}

		///<summary>
		///StackTrace
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String StackTrace {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public LogErrorEntity Clone()
        {
            LogErrorEntity entity = new LogErrorEntity();
			entity.Idx = this.Idx;
			entity.AppId = this.AppId;
			entity.TerminalIP = this.TerminalIP;
			entity.ModuleId = this.ModuleId;
			entity.FunctionId = this.FunctionId;
			entity.Message = this.Message;
			entity.StackTrace = this.StackTrace;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Log_Error 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LogErrorResponse : BaseResponse<LogErrorEntity>
    {

    }
}

