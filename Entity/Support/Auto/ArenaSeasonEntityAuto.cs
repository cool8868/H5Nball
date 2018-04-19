
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Arena_Season 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ArenaSeasonEntity
	{
		
		public ArenaSeasonEntity()
		{
		}

		public ArenaSeasonEntity(
		System.Int32 idx
,				System.Int32 seasonid
,				System.DateTime preparetime
,				System.DateTime starttime
,				System.DateTime endtime
,				System.Int32 arenatype
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.SeasonId = seasonid;
			this.PrepareTime = preparetime;
			this.StartTime = starttime;
			this.EndTime = endtime;
			this.ArenaType = arenatype;
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
		///赛季ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SeasonId {get ; set ;}

		///<summary>
		///准备时间
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime PrepareTime {get ; set ;}

		///<summary>
		///开始时间
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///结束时间
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///开启的竞技场类型
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ArenaType {get ; set ;}

		///<summary>
		///状态
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
		#endregion
        
        #region Clone
        public ArenaSeasonEntity Clone()
        {
            ArenaSeasonEntity entity = new ArenaSeasonEntity();
			entity.Idx = this.Idx;
			entity.SeasonId = this.SeasonId;
			entity.PrepareTime = this.PrepareTime;
			entity.StartTime = this.StartTime;
			entity.EndTime = this.EndTime;
			entity.ArenaType = this.ArenaType;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Arena_Season 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ArenaSeasonResponse : BaseResponse<ArenaSeasonEntity>
    {

    }
}
