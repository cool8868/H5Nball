
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossLadder_Season 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrossladderSeasonEntity
	{
		
		public CrossladderSeasonEntity()
		{
		}

		public CrossladderSeasonEntity(
		System.Int32 idx
,				System.DateTime startdate
,				System.DateTime enddate
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.Startdate = startdate;
			this.Enddate = enddate;
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
		///赛季开始日期
		///</summary>
        [DataMember]
        [ProtoMember(2)]
        [JsonIgnore]
		public System.DateTime Startdate {get ; set ;}

		///<summary>
		///赛季结束日期
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime Enddate {get ; set ;}

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
        public CrossladderSeasonEntity Clone()
        {
            CrossladderSeasonEntity entity = new CrossladderSeasonEntity();
			entity.Idx = this.Idx;
			entity.Startdate = this.Startdate;
			entity.Enddate = this.Enddate;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossLadder_Season 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrossladderSeasonResponse : BaseResponse<CrossladderSeasonEntity>
    {

    }
}
