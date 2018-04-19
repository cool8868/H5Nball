
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_DailyEventTime 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigDailyeventtimeEntity
	{
		
		public ConfigDailyeventtimeEntity()
		{
		}

		public ConfigDailyeventtimeEntity(
		System.Int32 idx
,				System.Int32 eventtype
,				System.Int32 openhour
,				System.Int32 openminute
,				System.Int32 starthour
,				System.Int32 startminute
,				System.Int32 endhour
,				System.Int32 endminute
,				System.Int32 version
		)
		{
			this.Idx = idx;
			this.EventType = eventtype;
			this.OpenHour = openhour;
			this.OpenMinute = openminute;
			this.StartHour = starthour;
			this.StartMinute = startminute;
			this.EndHour = endhour;
			this.EndMinute = endminute;
			this.Version = version;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///EventType
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 EventType {get ; set ;}

		///<summary>
		///OpenHour
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 OpenHour {get ; set ;}

		///<summary>
		///OpenMinute
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 OpenMinute {get ; set ;}

		///<summary>
		///StartHour
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 StartHour {get ; set ;}

		///<summary>
		///StartMinute
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 StartMinute {get ; set ;}

		///<summary>
		///EndHour
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 EndHour {get ; set ;}

		///<summary>
		///EndMinute
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 EndMinute {get ; set ;}

		///<summary>
		///Version
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Version {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigDailyeventtimeEntity Clone()
        {
            ConfigDailyeventtimeEntity entity = new ConfigDailyeventtimeEntity();
			entity.Idx = this.Idx;
			entity.EventType = this.EventType;
			entity.OpenHour = this.OpenHour;
			entity.OpenMinute = this.OpenMinute;
			entity.StartHour = this.StartHour;
			entity.StartMinute = this.StartMinute;
			entity.EndHour = this.EndHour;
			entity.EndMinute = this.EndMinute;
			entity.Version = this.Version;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_DailyEventTime 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigDailyeventtimeResponse : BaseResponse<ConfigDailyeventtimeEntity>
    {

    }
}

