
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Europe_Season 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class EuropeSeasonEntity
	{
		
		public EuropeSeasonEntity()
		{
		}

		public EuropeSeasonEntity(
		System.Int32 idx
,				System.DateTime startdate
,				System.DateTime enddate
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.StartDate = startdate;
			this.EndDate = enddate;
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
		///StartDate
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.DateTime StartDate {get ; set ;}

		///<summary>
		///EndDate
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.DateTime EndDate {get ; set ;}

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
        public EuropeSeasonEntity Clone()
        {
            EuropeSeasonEntity entity = new EuropeSeasonEntity();
			entity.Idx = this.Idx;
			entity.StartDate = this.StartDate;
			entity.EndDate = this.EndDate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Europe_Season 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class EuropeSeasonResponse : BaseResponse<EuropeSeasonEntity>
    {

    }
}
