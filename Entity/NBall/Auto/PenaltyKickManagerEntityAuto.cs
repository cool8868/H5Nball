
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.PenaltyKick_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class PenaltykickManagerEntity
	{
		
		public PenaltykickManagerEntity()
		{
		}

		public PenaltykickManagerEntity(
		System.Guid managerid
,				System.Int32 shootnumber
,				System.Int32 freenumber
,				System.Int32 gamecurrency
,				System.Int32 dayproduceluckycoin
,				System.Int32 totalscore
,				System.Int32 availablescore
,				System.Int32 totalgoals
,				System.Int32 shooterattribute
,				System.String shootlog
,				System.Int32 combgoals
,				System.Int32 maxcombgoals
,				System.String exchangestring
,				System.Int32 status
,				System.Int32 rank
,				System.Boolean isprize
,				System.DateTime updatetime
,				System.DateTime rowtime
,				System.DateTime refreshdate
,				System.DateTime scorechangetime
		)
		{
			this.ManagerId = managerid;
			this.ShootNumber = shootnumber;
			this.FreeNumber = freenumber;
			this.GameCurrency = gamecurrency;
			this.DayProduceLuckyCoin = dayproduceluckycoin;
			this.TotalScore = totalscore;
			this.AvailableScore = availablescore;
			this.TotalGoals = totalgoals;
			this.ShooterAttribute = shooterattribute;
			this.ShootLog = shootlog;
			this.CombGoals = combgoals;
			this.MaxCombGoals = maxcombgoals;
			this.ExChangeString = exchangestring;
			this.Status = status;
			this.Rank = rank;
			this.IsPrize = isprize;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
			this.RefreshDate = refreshdate;
			this.ScoreChangeTime = scorechangetime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///游戏次数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ShootNumber {get ; set ;}

		///<summary>
		///免费次数
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 FreeNumber {get ; set ;}

		///<summary>
		///游戏币
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 GameCurrency {get ; set ;}

		///<summary>
		///每天可产出的游戏币数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 DayProduceLuckyCoin {get ; set ;}

		///<summary>
		///总积分
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 TotalScore {get ; set ;}

		///<summary>
		///可用于兑换的积分
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 AvailableScore {get ; set ;}

		///<summary>
		///总进球数
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 TotalGoals {get ; set ;}

		///<summary>
		///射门属性
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 ShooterAttribute {get ; set ;}

		///<summary>
		///踢球记录
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String ShootLog {get ; set ;}

		///<summary>
		///连续进球数
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 CombGoals {get ; set ;}

		///<summary>
		///当前游戏最大连续进球数
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 MaxCombGoals {get ; set ;}

		///<summary>
		///可兑换的物品
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String ExChangeString {get ; set ;}

		///<summary>
		///游戏状态  0=初始 1=踢球中 2=踢球结束可以领奖
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///Rank
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 Rank {get ; set ;}

		///<summary>
		///IsPrize
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Boolean IsPrize {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(17)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(18)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///刷新时间
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.DateTime RefreshDate {get ; set ;}

		///<summary>
		///积分变化时间 用于排名
		///</summary>
        [DataMember]
        [ProtoMember(20)]
        [JsonIgnore]
		public System.DateTime ScoreChangeTime {get ; set ;}
		#endregion
        
        #region Clone
        public PenaltykickManagerEntity Clone()
        {
            PenaltykickManagerEntity entity = new PenaltykickManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.ShootNumber = this.ShootNumber;
			entity.FreeNumber = this.FreeNumber;
			entity.GameCurrency = this.GameCurrency;
			entity.DayProduceLuckyCoin = this.DayProduceLuckyCoin;
			entity.TotalScore = this.TotalScore;
			entity.AvailableScore = this.AvailableScore;
			entity.TotalGoals = this.TotalGoals;
			entity.ShooterAttribute = this.ShooterAttribute;
			entity.ShootLog = this.ShootLog;
			entity.CombGoals = this.CombGoals;
			entity.MaxCombGoals = this.MaxCombGoals;
			entity.ExChangeString = this.ExChangeString;
			entity.Status = this.Status;
			entity.Rank = this.Rank;
			entity.IsPrize = this.IsPrize;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
			entity.RefreshDate = this.RefreshDate;
			entity.ScoreChangeTime = this.ScoreChangeTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.PenaltyKick_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class PenaltykickManagerResponse : BaseResponse<PenaltykickManagerEntity>
    {

    }
}
