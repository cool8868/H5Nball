
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Zone_DailyEventTime 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ZoneDailyeventtimeEntity
	{
		
		public ZoneDailyeventtimeEntity()
		{
		}

		public ZoneDailyeventtimeEntity(
		System.Int32 idx
,				System.Int32 zoneid
,				System.Int32 eventtype
,				System.Int32 openhour
,				System.Int32 openminute
,				System.Int32 starthour
,				System.Int32 startminute
,				System.Int32 endhour
,				System.Int32 endminute
,				System.Int32 startday
,				System.Int32 endday
		)
		{
			this.Idx = idx;
			this.ZoneId = zoneid;
			this.EventType = eventtype;
			this.OpenHour = openhour;
			this.OpenMinute = openminute;
			this.StartHour = starthour;
			this.StartMinute = startminute;
			this.EndHour = endhour;
			this.EndMinute = endminute;
			this.StartDay = startday;
			this.EndDay = endday;
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
		///OpenHour
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 OpenHour {get ; set ;}

		///<summary>
		///OpenMinute
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 OpenMinute {get ; set ;}

		///<summary>
		///StartHour
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 StartHour {get ; set ;}

		///<summary>
		///StartMinute
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 StartMinute {get ; set ;}

		///<summary>
		///EndHour
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 EndHour {get ; set ;}

		///<summary>
		///EndMinute
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 EndMinute {get ; set ;}

		///<summary>
		///StartDay
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 StartDay {get ; set ;}

		///<summary>
		///EndDay
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 EndDay {get ; set ;}
		#endregion
        
        #region Clone
        public ZoneDailyeventtimeEntity Clone()
        {
            ZoneDailyeventtimeEntity entity = new ZoneDailyeventtimeEntity();
			entity.Idx = this.Idx;
			entity.ZoneId = this.ZoneId;
			entity.EventType = this.EventType;
			entity.OpenHour = this.OpenHour;
			entity.OpenMinute = this.OpenMinute;
			entity.StartHour = this.StartHour;
			entity.StartMinute = this.StartMinute;
			entity.EndHour = this.EndHour;
			entity.EndMinute = this.EndMinute;
			entity.StartDay = this.StartDay;
			entity.EndDay = this.EndDay;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Zone_DailyEventTime 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ZoneDailyeventtimeResponse : BaseResponse<ZoneDailyeventtimeEntity>
    {

    }
}
