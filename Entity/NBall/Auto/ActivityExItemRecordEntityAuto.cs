
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ActivityEx_ItemRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ActivityexItemrecordEntity
	{
		
		public ActivityexItemrecordEntity()
		{
		}

		public ActivityexItemrecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 zoneactivityid
,				System.Int32 excitingid
,				System.Int32 groupid
,				System.String itemstring
,				System.DateTime recorddate
,				System.Int32 status
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ZoneActivityId = zoneactivityid;
			this.ExcitingId = excitingid;
			this.GroupId = groupid;
			this.ItemString = itemstring;
			this.RecordDate = recorddate;
			this.Status = status;
			this.UpdateTime = updatetime;
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
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///ZoneActivityId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ZoneActivityId {get ; set ;}

		///<summary>
		///ExcitingId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ExcitingId {get ; set ;}

		///<summary>
		///GroupId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 GroupId {get ; set ;}

		///<summary>
		///可得到的物品（itemtype,itemcode,itemcount|）
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String ItemString {get ; set ;}

		///<summary>
		///刷新时间
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///状态  0or1=未发奖 2=已发奖
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ActivityexItemrecordEntity Clone()
        {
            ActivityexItemrecordEntity entity = new ActivityexItemrecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ZoneActivityId = this.ZoneActivityId;
			entity.ExcitingId = this.ExcitingId;
			entity.GroupId = this.GroupId;
			entity.ItemString = this.ItemString;
			entity.RecordDate = this.RecordDate;
			entity.Status = this.Status;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ActivityEx_ItemRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ActivityexItemrecordResponse : BaseResponse<ActivityexItemrecordEntity>
    {

    }
}
