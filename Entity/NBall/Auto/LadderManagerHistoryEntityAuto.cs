
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Ladder_ManagerHistory 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LadderManagerhistoryEntity
	{
		
		public LadderManagerhistoryEntity()
		{
		}

		public LadderManagerhistoryEntity(
		System.Int32 idx
,				System.Int32 season
,				System.Guid managerid
,				System.Int32 rank
,				System.Int32 score
,				System.String prizeitems
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.Season = season;
			this.ManagerId = managerid;
			this.Rank = rank;
			this.Score = score;
			this.PrizeItems = prizeitems;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///所属赛季
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Season {get ; set ;}

		///<summary>
		///经理id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///Rank
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Rank {get ; set ;}

		///<summary>
		///积分
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///格式：ItemCode,Strength,IsBinding|ItemCode,Strength,IsBinding
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String PrizeItems {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public LadderManagerhistoryEntity Clone()
        {
            LadderManagerhistoryEntity entity = new LadderManagerhistoryEntity();
			entity.Idx = this.Idx;
			entity.Season = this.Season;
			entity.ManagerId = this.ManagerId;
			entity.Rank = this.Rank;
			entity.Score = this.Score;
			entity.PrizeItems = this.PrizeItems;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Ladder_ManagerHistory 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LadderManagerhistoryResponse : BaseResponse<LadderManagerhistoryEntity>
    {

    }
}

