
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerMatchAchievement 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagermatchachievementEntity
	{
		
		public NbManagermatchachievementEntity()
		{
		}

		public NbManagermatchachievementEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 matchtype
,				System.Int32 matchtypeid
,				System.Int32 rankindex
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.MatchType = matchtype;
			this.MatchTypeId = matchtypeid;
			this.RankIndex = rankindex;
			this.Status = status;
			this.Rowtime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///经理ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///比赛类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MatchType {get ; set ;}

		///<summary>
		///比赛ID（比如是某个杯赛的ID）
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 MatchTypeId {get ; set ;}

		///<summary>
		///排名
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 RankIndex {get ; set ;}

		///<summary>
		///状态
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///创建时间
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime Rowtime {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagermatchachievementEntity Clone()
        {
            NbManagermatchachievementEntity entity = new NbManagermatchachievementEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.MatchType = this.MatchType;
			entity.MatchTypeId = this.MatchTypeId;
			entity.RankIndex = this.RankIndex;
			entity.Status = this.Status;
			entity.Rowtime = this.Rowtime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerMatchAchievement 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagermatchachievementResponse : BaseResponse<NbManagermatchachievementEntity>
    {

    }
}

