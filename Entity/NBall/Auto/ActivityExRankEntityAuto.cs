
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ActivityEx_Rank 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityexRankEntity
	{
		
		public ActivityexRankEntity()
		{
		}

		public ActivityexRankEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.String name
,				System.String rankkey
,				System.Int32 exdata
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.Name = name;
			this.RankKey = rankkey;
			this.ExData = exdata;
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
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///RankKey
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String RankKey {get ; set ;}

		///<summary>
		///ExData
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ExData {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityexRankEntity Clone()
        {
            ActivityexRankEntity entity = new ActivityexRankEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.Name = this.Name;
			entity.RankKey = this.RankKey;
			entity.ExData = this.ExData;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ActivityEx_Rank 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityexRankResponse : BaseResponse<ActivityexRankEntity>
    {

    }
}

