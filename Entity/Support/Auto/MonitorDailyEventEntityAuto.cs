
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Monitor_DailyEvent 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class MonitorDailyeventEntity
	{
		
		public MonitorDailyeventEntity()
		{
		}

		public MonitorDailyeventEntity(
		System.Int32 idx
,				System.Int32 zoneid
,				System.Int32 eventtype
,				System.DateTime opentime
,				System.DateTime starttime
,				System.DateTime endtime
,				System.DateTime recorddate
,				System.Int32 status
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ZoneId = zoneid;
			this.EventType = eventtype;
			this.OpenTime = opentime;
			this.StartTime = starttime;
			this.EndTime = endtime;
			this.RecordDate = recorddate;
			this.Status = status;
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
		///ZoneId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ZoneId {get ; set ;}

		///<summary>
		///EventType
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 EventType {get ; set ;}

		///<summary>
		///OpenTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime OpenTime {get ; set ;}

		///<summary>
		///StartTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///EndTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public MonitorDailyeventEntity Clone()
        {
            MonitorDailyeventEntity entity = new MonitorDailyeventEntity();
			entity.Idx = this.Idx;
			entity.ZoneId = this.ZoneId;
			entity.EventType = this.EventType;
			entity.OpenTime = this.OpenTime;
			entity.StartTime = this.StartTime;
			entity.EndTime = this.EndTime;
			entity.RecordDate = this.RecordDate;
			entity.Status = this.Status;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Monitor_DailyEvent 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class MonitorDailyeventResponse : BaseResponse<MonitorDailyeventEntity>
    {

    }
}
