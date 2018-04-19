
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Template_ActivityExGroup 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TemplateActivityexgroupEntity
	{
		
		public TemplateActivityexgroupEntity()
		{
		}

		public TemplateActivityexgroupEntity(
		System.Int32 idx
,				System.Int32 excitingid
,				System.Int32 groupid
,				System.Int32 activityextype
,				System.Int32 exrequireid
,				System.Int32 statisticcycle
,				System.Boolean isrank
,				System.Int32 rankcondition
,				System.Int32 rankcount
		)
		{
			this.Idx = idx;
			this.ExcitingId = excitingid;
			this.GroupId = groupid;
			this.ActivityExType = activityextype;
			this.ExRequireId = exrequireid;
			this.StatisticCycle = statisticcycle;
			this.IsRank = isrank;
			this.RankCondition = rankcondition;
			this.RankCount = rankcount;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ExcitingId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ExcitingId {get ; set ;}

		///<summary>
		///GroupId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 GroupId {get ; set ;}

		///<summary>
		///ActivityExType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ActivityExType {get ; set ;}

		///<summary>
		///ExRequireId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ExRequireId {get ; set ;}

		///<summary>
		///StatisticCycle
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 StatisticCycle {get ; set ;}

		///<summary>
		///IsRank
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean IsRank {get ; set ;}

		///<summary>
		///RankCondition
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 RankCondition {get ; set ;}

		///<summary>
		///RankCount
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 RankCount {get ; set ;}
		#endregion
        
        #region Clone
        public TemplateActivityexgroupEntity Clone()
        {
            TemplateActivityexgroupEntity entity = new TemplateActivityexgroupEntity();
			entity.Idx = this.Idx;
			entity.ExcitingId = this.ExcitingId;
			entity.GroupId = this.GroupId;
			entity.ActivityExType = this.ActivityExType;
			entity.ExRequireId = this.ExRequireId;
			entity.StatisticCycle = this.StatisticCycle;
			entity.IsRank = this.IsRank;
			entity.RankCondition = this.RankCondition;
			entity.RankCount = this.RankCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Template_ActivityExGroup 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TemplateActivityexgroupResponse : BaseResponse<TemplateActivityexgroupEntity>
    {

    }
}

