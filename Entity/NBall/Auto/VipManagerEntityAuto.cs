
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Vip_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class VipManagerEntity
	{
		
		public VipManagerEntity()
		{
		}

		public VipManagerEntity(
		System.Guid managerid
,				System.Int32 vipexp
,				System.DateTime receivedate
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.ManagerId = managerid;
			this.VipExp = vipexp;
			this.ReceiveDate = receivedate;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///签到获得的VIP经验
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 VipExp {get ; set ;}

		///<summary>
		///ReceiveDate
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime ReceiveDate {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public VipManagerEntity Clone()
        {
            VipManagerEntity entity = new VipManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.VipExp = this.VipExp;
			entity.ReceiveDate = this.ReceiveDate;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Vip_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class VipManagerResponse : BaseResponse<VipManagerEntity>
    {

    }
}

