
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.PlayerKill_Info 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class PlayerkillInfoEntity
	{
		
		public PlayerkillInfoEntity()
		{
		}

		public PlayerkillInfoEntity(
		System.Guid managerid
,				System.Int32 remaintimes
,				System.Int32 remainbytimes
,				System.Int32 buytimes
,				System.Int32 daywintimes
,				System.DateTime recorddate
,				System.Int32 win
,				System.Int32 lose
,				System.Int32 draw
,				System.Guid lotterymatchid
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Byte[] opponentinfo
,				System.DateTime opponentrefreshtime
,				System.Int32 daypoint
,				System.Int32 specialitemnumber
		)
		{
			this.ManagerId = managerid;
			this.RemainTimes = remaintimes;
			this.RemainByTimes = remainbytimes;
			this.BuyTimes = buytimes;
			this.DayWinTimes = daywintimes;
			this.RecordDate = recorddate;
			this.Win = win;
			this.Lose = lose;
			this.Draw = draw;
			this.LotteryMatchId = lotterymatchid;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.OpponentInfo = opponentinfo;
			this.OpponentRefreshTime = opponentrefreshtime;
			this.DayPoint = daypoint;
			this.SpecialItemNumber = specialitemnumber;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///RemainTimes
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 RemainTimes {get ; set ;}

		///<summary>
		///RemainByTimes
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 RemainByTimes {get ; set ;}

		///<summary>
		///BuyTimes
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 BuyTimes {get ; set ;}

		///<summary>
		///DayWinTimes
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 DayWinTimes {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///Win
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Win {get ; set ;}

		///<summary>
		///Lose
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Lose {get ; set ;}

		///<summary>
		///Draw
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Draw {get ; set ;}

		///<summary>
		///LotteryMatchId
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Guid LotteryMatchId {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///OpponentInfo
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.Byte[] OpponentInfo {get ; set ;}

		///<summary>
		///OpponentRefreshTime
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime OpponentRefreshTime {get ; set ;}

		///<summary>
		///DayPoint
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 DayPoint {get ; set ;}

		///<summary>
		///SpecialItemNumber
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 SpecialItemNumber {get ; set ;}
		#endregion
        
        #region Clone
        public PlayerkillInfoEntity Clone()
        {
            PlayerkillInfoEntity entity = new PlayerkillInfoEntity();
			entity.ManagerId = this.ManagerId;
			entity.RemainTimes = this.RemainTimes;
			entity.RemainByTimes = this.RemainByTimes;
			entity.BuyTimes = this.BuyTimes;
			entity.DayWinTimes = this.DayWinTimes;
			entity.RecordDate = this.RecordDate;
			entity.Win = this.Win;
			entity.Lose = this.Lose;
			entity.Draw = this.Draw;
			entity.LotteryMatchId = this.LotteryMatchId;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.OpponentInfo = this.OpponentInfo;
			entity.OpponentRefreshTime = this.OpponentRefreshTime;
			entity.DayPoint = this.DayPoint;
			entity.SpecialItemNumber = this.SpecialItemNumber;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.PlayerKill_Info 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class PlayerkillInfoResponse : BaseResponse<PlayerkillInfoEntity>
    {

    }
}
