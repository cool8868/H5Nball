
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ActivityEx_Zone 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityexZoneEntity
	{
		
		public ActivityexZoneEntity()
		{
		}

		public ActivityexZoneEntity(
		System.Int32 idx
,				System.Int32 zoneid
,				System.Int32 excitingid
,				System.DateTime starttime
,				System.DateTime endtime
,				System.DateTime closetime
		)
		{
			this.Idx = idx;
			this.ZoneId = zoneid;
			this.ExcitingId = excitingid;
			this.StartTime = starttime;
			this.EndTime = endtime;
			this.CloseTime = closetime;
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
		///ExcitingId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ExcitingId {get ; set ;}

		///<summary>
		///StartTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///EndTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///CloseTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime CloseTime {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityexZoneEntity Clone()
        {
            ActivityexZoneEntity entity = new ActivityexZoneEntity();
			entity.Idx = this.Idx;
			entity.ZoneId = this.ZoneId;
			entity.ExcitingId = this.ExcitingId;
			entity.StartTime = this.StartTime;
			entity.EndTime = this.EndTime;
			entity.CloseTime = this.CloseTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ActivityEx_Zone 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityexZoneResponse : BaseResponse<ActivityexZoneEntity>
    {

    }
}

