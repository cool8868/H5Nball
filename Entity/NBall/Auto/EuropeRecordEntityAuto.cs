
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Europe_Record 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class EuropeRecordEntity
	{
		
		public EuropeRecordEntity()
		{
		}

		public EuropeRecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 season
,				System.Int32 correctnumber
,				System.String prizerecord
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.Season = season;
			this.CorrectNumber = correctnumber;
			this.PrizeRecord = prizerecord;
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
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///Season
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Season {get ; set ;}

		///<summary>
		///CorrectNumber
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CorrectNumber {get ; set ;}

		///<summary>
		///PrizeRecord
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String PrizeRecord {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public EuropeRecordEntity Clone()
        {
            EuropeRecordEntity entity = new EuropeRecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.Season = this.Season;
			entity.CorrectNumber = this.CorrectNumber;
			entity.PrizeRecord = this.PrizeRecord;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Europe_Record 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class EuropeRecordResponse : BaseResponse<EuropeRecordEntity>
    {

    }
}
