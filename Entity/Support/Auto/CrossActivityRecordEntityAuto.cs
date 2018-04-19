
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossActivity_Record 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrossactivityRecordEntity
	{
		
		public CrossactivityRecordEntity()
		{
		}

		public CrossactivityRecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.String sitename
,				System.Int32 prizeid
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.SiteName = sitename;
			this.PrizeId = prizeid;
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
		///SiteName
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SiteName {get ; set ;}

		///<summary>
		///PrizeId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeId {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrossactivityRecordEntity Clone()
        {
            CrossactivityRecordEntity entity = new CrossactivityRecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.SiteName = this.SiteName;
			entity.PrizeId = this.PrizeId;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossActivity_Record 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrossactivityRecordResponse : BaseResponse<CrossactivityRecordEntity>
    {

    }
}
