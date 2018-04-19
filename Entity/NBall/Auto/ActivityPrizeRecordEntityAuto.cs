
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Activity_PrizeRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityPrizerecordEntity
	{
		
		public ActivityPrizerecordEntity()
		{
		}

		public ActivityPrizerecordEntity(
		System.Int64 idx
,				System.Guid managerid
,				System.String activitykey
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ActivityKey = activitykey;
			this.RowTime = rowtime;
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
		///ActivityKey
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ActivityKey {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityPrizerecordEntity Clone()
        {
            ActivityPrizerecordEntity entity = new ActivityPrizerecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ActivityKey = this.ActivityKey;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Activity_PrizeRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityPrizerecordResponse : BaseResponse<ActivityPrizerecordEntity>
    {

    }
}

