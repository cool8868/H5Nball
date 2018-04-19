
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Manager_ChargeNumber 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ManagerChargenumberEntity
	{
		
		public ManagerChargenumberEntity()
		{
		}

		public ManagerChargenumberEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 mallcode
,				System.Int32 buynumber
,				System.DateTime rowtime
,				System.DateTime updatetiem
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.MallCode = mallcode;
			this.BuyNumber = buynumber;
			this.RowTime = rowtime;
			this.UpdateTiem = updatetiem;
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
		///购买的物品code
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MallCode {get ; set ;}

		///<summary>
		///购买次数
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 BuyNumber {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTiem
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime UpdateTiem {get ; set ;}
		#endregion
        
        #region Clone
        public ManagerChargenumberEntity Clone()
        {
            ManagerChargenumberEntity entity = new ManagerChargenumberEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.MallCode = this.MallCode;
			entity.BuyNumber = this.BuyNumber;
			entity.RowTime = this.RowTime;
			entity.UpdateTiem = this.UpdateTiem;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Manager_ChargeNumber 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ManagerChargenumberResponse : BaseResponse<ManagerChargenumberEntity>
    {

    }
}

