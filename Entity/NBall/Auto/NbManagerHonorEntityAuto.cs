
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerHonor 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagerhonorEntity
	{
		
		public NbManagerhonorEntity()
		{
		}

		public NbManagerhonorEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 matchtype
,				System.Int32 subtype
,				System.Int32 periodid
,				System.Int32 rank
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.MatchType = matchtype;
			this.SubType = subtype;
			this.PeriodId = periodid;
			this.Rank = rank;
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
		///ManagerId
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
		///二级类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///第几届
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PeriodId {get ; set ;}

		///<summary>
		///排名
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Rank {get ; set ;}

		///<summary>
		///Rowtime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime Rowtime {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagerhonorEntity Clone()
        {
            NbManagerhonorEntity entity = new NbManagerhonorEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.MatchType = this.MatchType;
			entity.SubType = this.SubType;
			entity.PeriodId = this.PeriodId;
			entity.Rank = this.Rank;
			entity.Rowtime = this.Rowtime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerHonor 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagerhonorResponse : BaseResponse<NbManagerhonorEntity>
    {

    }
}

