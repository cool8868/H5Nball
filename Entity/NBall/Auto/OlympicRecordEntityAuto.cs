
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Olympic_Record 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class OlympicRecordEntity
	{
		
		public OlympicRecordEntity()
		{
		}

		public OlympicRecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 extype
,				System.Int32 exitemcode
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ExType = extype;
			this.ExItemCode = exitemcode;
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
		///兑换类型 1强化 2球星碎片3巨星碎片4钻石5元老碎片
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ExType {get ; set ;}

		///<summary>
		///获得的物品
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ExItemCode {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public OlympicRecordEntity Clone()
        {
            OlympicRecordEntity entity = new OlympicRecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ExType = this.ExType;
			entity.ExItemCode = this.ExItemCode;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Olympic_Record 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class OlympicRecordResponse : BaseResponse<OlympicRecordEntity>
    {

    }
}
