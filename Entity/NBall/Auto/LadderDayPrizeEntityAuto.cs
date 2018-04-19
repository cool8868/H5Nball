
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Ladder_DayPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LadderDayprizeEntity
	{
		
		public LadderDayprizeEntity()
		{
		}

		public LadderDayprizeEntity(
		System.Guid managerid
,				System.Int32 winnumber
,				System.String prizerecord
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.WinNumber = winnumber;
			this.PrizeRecord = prizerecord;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///获胜场次
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 WinNumber {get ; set ;}

		///<summary>
		///奖励记录（1,1,1,1）
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
		#endregion
        
        #region Clone
        public LadderDayprizeEntity Clone()
        {
            LadderDayprizeEntity entity = new LadderDayprizeEntity();
			entity.ManagerId = this.ManagerId;
			entity.WinNumber = this.WinNumber;
			entity.PrizeRecord = this.PrizeRecord;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Ladder_DayPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LadderDayprizeResponse : BaseResponse<LadderDayprizeEntity>
    {

    }
}

