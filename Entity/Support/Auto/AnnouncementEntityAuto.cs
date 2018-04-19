
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Announcement 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AnnouncementEntity
	{
		
		public AnnouncementEntity()
		{
		}

		public AnnouncementEntity(
		System.Int32 idx
,				System.String platform
,				System.Boolean istop
,				System.Boolean isrnable
,				System.String title
,				System.String contentstring
,				System.DateTime starttime
,				System.DateTime endtime
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.Platform = platform;
			this.IsTop = istop;
			this.IsRnable = isrnable;
			this.Title = title;
			this.ContentString = contentstring;
			this.StartTime = starttime;
			this.EndTime = endtime;
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
		///Platform
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Platform {get ; set ;}

		///<summary>
		///IsTop
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Boolean IsTop {get ; set ;}

		///<summary>
		///IsRnable
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Boolean IsRnable {get ; set ;}

		///<summary>
		///Title
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Title {get ; set ;}

		///<summary>
		///ContentString
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String ContentString {get ; set ;}

		///<summary>
		///StartTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///EndTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public AnnouncementEntity Clone()
        {
            AnnouncementEntity entity = new AnnouncementEntity();
			entity.Idx = this.Idx;
			entity.Platform = this.Platform;
			entity.IsTop = this.IsTop;
			entity.IsRnable = this.IsRnable;
			entity.Title = this.Title;
			entity.ContentString = this.ContentString;
			entity.StartTime = this.StartTime;
			entity.EndTime = this.EndTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Announcement 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AnnouncementResponse : BaseResponse<AnnouncementEntity>
    {

    }
}
