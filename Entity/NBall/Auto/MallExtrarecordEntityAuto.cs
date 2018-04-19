
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Mall_ExtraRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class MallExtrarecordEntity
	{
		
		public MallExtrarecordEntity()
		{
		}

		public MallExtrarecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 extratype
,				System.Int32 usedcount
,				System.DateTime recorddate
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ExtraType = extratype;
			this.UsedCount = usedcount;
			this.RecordDate = recorddate;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
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
		///extra类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ExtraType {get ; set ;}

		///<summary>
		///使用次数/当天使用次数
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 UsedCount {get ; set ;}

		///<summary>
		///记录日期
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public MallExtrarecordEntity Clone()
        {
            MallExtrarecordEntity entity = new MallExtrarecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ExtraType = this.ExtraType;
			entity.UsedCount = this.UsedCount;
			entity.RecordDate = this.RecordDate;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Mall_ExtraRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class MallExtrarecordResponse : BaseResponse<MallExtrarecordEntity>
    {

    }
}

