
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Gamble_Detail 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GambleDetailEntity
	{
		
		public GambleDetailEntity()
		{
		}

		public GambleDetailEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.String managername
,				System.Int32 hostoptionid
,				System.Int32 gamblemoney
,				System.Int32 resultmoney
,				System.Int32 status
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ManagerName = managername;
			this.HostOptionId = hostoptionid;
			this.GambleMoney = gamblemoney;
			this.ResultMoney = resultmoney;
			this.Status = status;
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
		///经理ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///经理名
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ManagerName {get ; set ;}

		///<summary>
		///押注选项ID
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 HostOptionId {get ; set ;}

		///<summary>
		///押注金额
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 GambleMoney {get ; set ;}

		///<summary>
		///结算奖金
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ResultMoney {get ; set ;}

		///<summary>
		///状态 0 未开奖，1已猜中，2未猜中
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///创建时间
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///Version
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public GambleDetailEntity Clone()
        {
            GambleDetailEntity entity = new GambleDetailEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ManagerName = this.ManagerName;
			entity.HostOptionId = this.HostOptionId;
			entity.GambleMoney = this.GambleMoney;
			entity.ResultMoney = this.ResultMoney;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Gamble_Detail 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GambleDetailResponse : BaseResponse<GambleDetailEntity>
    {

    }
}
