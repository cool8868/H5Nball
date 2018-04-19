
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.PlayerKill_Revenge 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class PlayerkillRevengeEntity
	{
		
		public PlayerkillRevengeEntity()
		{
		}

		public PlayerkillRevengeEntity(
		System.Int64 idx
,				System.Guid managerid
,				System.Guid homeid
,				System.String homelogo
,				System.String homename
,				System.Int32 homescore
,				System.Int32 awayscore
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.HomeId = homeid;
			this.HomeLogo = homelogo;
			this.HomeName = homename;
			this.HomeScore = homescore;
			this.AwayScore = awayscore;
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
		public System.Int64 Idx {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///HomeId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid HomeId {get ; set ;}

		///<summary>
		///HomeLogo
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String HomeLogo {get ; set ;}

		///<summary>
		///对手名称
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String HomeName {get ; set ;}

		///<summary>
		///HomeScore
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 HomeScore {get ; set ;}

		///<summary>
		///AwayScore
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 AwayScore {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public PlayerkillRevengeEntity Clone()
        {
            PlayerkillRevengeEntity entity = new PlayerkillRevengeEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.HomeId = this.HomeId;
			entity.HomeLogo = this.HomeLogo;
			entity.HomeName = this.HomeName;
			entity.HomeScore = this.HomeScore;
			entity.AwayScore = this.AwayScore;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.PlayerKill_Revenge 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class PlayerkillRevengeResponse : BaseResponse<PlayerkillRevengeEntity>
    {

    }
}

