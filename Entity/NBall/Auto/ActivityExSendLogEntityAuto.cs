
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ActivityEx_SendLog 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityexSendlogEntity
	{
		
		public ActivityexSendlogEntity()
		{
		}

		public ActivityexSendlogEntity(
		System.Int32 idx
,				System.Int32 excitingid
,				System.Int32 groupid
,				System.DateTime recorddate
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ExcitingId = excitingid;
			this.GroupId = groupid;
			this.RecordDate = recorddate;
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
		///ExcitingId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ExcitingId {get ; set ;}

		///<summary>
		///GroupId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 GroupId {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityexSendlogEntity Clone()
        {
            ActivityexSendlogEntity entity = new ActivityexSendlogEntity();
			entity.Idx = this.Idx;
			entity.ExcitingId = this.ExcitingId;
			entity.GroupId = this.GroupId;
			entity.RecordDate = this.RecordDate;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ActivityEx_SendLog 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityexSendlogResponse : BaseResponse<ActivityexSendlogEntity>
    {

    }
}

