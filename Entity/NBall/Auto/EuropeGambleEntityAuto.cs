
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Europe_Gamble 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class EuropeGambleEntity
	{
		
		public EuropeGambleEntity()
		{
		}

		public EuropeGambleEntity(
		System.Guid managerid
,				System.Int32 correctnumber
,				System.String prizerecord
,				System.DateTime updatetime
,				System.DateTime rowtime
,				System.Int32 seasonid
		)
		{
			this.ManagerId = managerid;
			this.CorrectNumber = correctnumber;
			this.PrizeRecord = prizerecord;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
			this.SeasonId = seasonid;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///竞猜正确次数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CorrectNumber {get ; set ;}

		///<summary>
		///奖励记录  1,1,1,1,1 
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String PrizeRecord {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///SeasonId
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 SeasonId {get ; set ;}
		#endregion
        
        #region Clone
        public EuropeGambleEntity Clone()
        {
            EuropeGambleEntity entity = new EuropeGambleEntity();
			entity.ManagerId = this.ManagerId;
			entity.CorrectNumber = this.CorrectNumber;
			entity.PrizeRecord = this.PrizeRecord;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
			entity.SeasonId = this.SeasonId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Europe_Gamble 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class EuropeGambleResponse : BaseResponse<EuropeGambleEntity>
    {

    }
}
