
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_ChallengeRevord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationChallengerevordEntity
	{
		
		public RevelationChallengerevordEntity()
		{
		}

		public RevelationChallengerevordEntity(
		System.Guid gameid
,				System.Guid managerid
,				System.Int32 mark
,				System.Int32 schedule
,				System.Int32 goals
,				System.Int32 toconcede
,				System.DateTime gamedate
		)
		{
			this.GameId = gameid;
			this.ManagerId = managerid;
			this.Mark = mark;
			this.Schedule = schedule;
			this.Goals = goals;
			this.ToConcede = toconcede;
			this.GameDate = gamedate;
		}
		
		#region Public Properties
		
		///<summary>
		///游戏ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid GameId {get ; set ;}

		///<summary>
		///经理ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///球星关卡ID
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Mark {get ; set ;}

		///<summary>
		///小关卡ID
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Schedule {get ; set ;}

		///<summary>
		///进球数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Goals {get ; set ;}

		///<summary>
		///失球数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ToConcede {get ; set ;}

		///<summary>
		///游戏时间
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime GameDate {get ; set ;}
		#endregion
        
        #region Clone
        public RevelationChallengerevordEntity Clone()
        {
            RevelationChallengerevordEntity entity = new RevelationChallengerevordEntity();
			entity.GameId = this.GameId;
			entity.ManagerId = this.ManagerId;
			entity.Mark = this.Mark;
			entity.Schedule = this.Schedule;
			entity.Goals = this.Goals;
			entity.ToConcede = this.ToConcede;
			entity.GameDate = this.GameDate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_ChallengeRevord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationChallengerevordResponse : BaseResponse<RevelationChallengerevordEntity>
    {

    }
}

