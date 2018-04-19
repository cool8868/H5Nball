
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.League_WincountRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LeagueWincountrecordEntity
	{
		
		public LeagueWincountrecordEntity()
		{
		}

		public LeagueWincountrecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 leagueid
,				System.Int32 wincount1
,				System.Int32 wincount1status
,				System.Int32 wincount2
,				System.Int32 wincount2status
,				System.Int32 wincount3
,				System.Int32 wincount3status
,				System.Int32 maxwincount
,				System.DateTime updatetime
,				System.DateTime rowtime
,				System.DateTime prizedate
,				System.String prizestep
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.LeagueId = leagueid;
			this.WinCount1 = wincount1;
			this.WinCount1Status = wincount1status;
			this.WinCount2 = wincount2;
			this.WinCount2Status = wincount2status;
			this.WinCount3 = wincount3;
			this.WinCount3Status = wincount3status;
			this.MaxWinCount = maxwincount;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
			this.PrizeDate = prizedate;
			this.PrizeStep = prizestep;
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
		///LeagueId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 LeagueId {get ; set ;}

		///<summary>
		///WinCount1
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 WinCount1 {get ; set ;}

		///<summary>
		///0初始化 1可领取 2已领取
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 WinCount1Status {get ; set ;}

		///<summary>
		///WinCount2
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 WinCount2 {get ; set ;}

		///<summary>
		///0初始化 1可领取 2已领取
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 WinCount2Status {get ; set ;}

		///<summary>
		///WinCount3
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 WinCount3 {get ; set ;}

		///<summary>
		///0初始化 1可领取 2已领取
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 WinCount3Status {get ; set ;}

		///<summary>
		///MaxWinCount
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 MaxWinCount {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///PrizeDate
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.DateTime PrizeDate {get ; set ;}

		///<summary>
		///PrizeStep
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String PrizeStep {get ; set ;}
		#endregion
        
        #region Clone
        public LeagueWincountrecordEntity Clone()
        {
            LeagueWincountrecordEntity entity = new LeagueWincountrecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.LeagueId = this.LeagueId;
			entity.WinCount1 = this.WinCount1;
			entity.WinCount1Status = this.WinCount1Status;
			entity.WinCount2 = this.WinCount2;
			entity.WinCount2Status = this.WinCount2Status;
			entity.WinCount3 = this.WinCount3;
			entity.WinCount3Status = this.WinCount3Status;
			entity.MaxWinCount = this.MaxWinCount;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
			entity.PrizeDate = this.PrizeDate;
			entity.PrizeStep = this.PrizeStep;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.League_WincountRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LeagueWincountrecordResponse : BaseResponse<LeagueWincountrecordEntity>
    {

    }
}

