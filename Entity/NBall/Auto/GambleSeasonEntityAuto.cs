
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Gamble_Season 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GambleSeasonEntity
	{
		
		public GambleSeasonEntity()
		{
		}

		public GambleSeasonEntity(
		System.Int32 idx
,				System.DateTime starttime
,				System.DateTime endtime
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.StartTime = starttime;
			this.EndTime = endtime;
			this.Status = status;
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
		///StartTime
		///</summary>
        [DataMember]
        [ProtoMember(2)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///EndTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public GambleSeasonEntity Clone()
        {
            GambleSeasonEntity entity = new GambleSeasonEntity();
			entity.Idx = this.Idx;
			entity.StartTime = this.StartTime;
			entity.EndTime = this.EndTime;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Gamble_Season 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GambleSeasonResponse : BaseResponse<GambleSeasonEntity>
    {

    }
}
