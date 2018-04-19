
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.GoldBar_Record 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GoldbarRecordEntity
	{
		
		public GoldbarRecordEntity()
		{
		}

		public GoldbarRecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Boolean isadd
,				System.Int32 number
,				System.Int32 operationtype
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.IsAdd = isadd;
			this.Number = number;
			this.OperationType = operationtype;
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
		///是否增加
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Boolean IsAdd {get ; set ;}

		///<summary>
		///操作数量
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Number {get ; set ;}

		///<summary>
		///操作类型
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 OperationType {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public GoldbarRecordEntity Clone()
        {
            GoldbarRecordEntity entity = new GoldbarRecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.IsAdd = this.IsAdd;
			entity.Number = this.Number;
			entity.OperationType = this.OperationType;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.GoldBar_Record 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GoldbarRecordResponse : BaseResponse<GoldbarRecordEntity>
    {

    }
}
