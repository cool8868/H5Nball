
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Gamble_Host 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GambleHostEntity
	{
		
		public GambleHostEntity()
		{
		}

		public GambleHostEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.String managername
,				System.Guid titleid
,				System.Int32 hostmoney
,				System.Int32 totalmoney
,				System.Int32 attendpeoplecount
,				System.Int32 hostwinmoney
,				System.Int32 status
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ManagerName = managername;
			this.TitleId = titleid;
			this.HostMoney = hostmoney;
			this.TotalMoney = totalmoney;
			this.AttendPeopleCount = attendpeoplecount;
			this.HostWinMoney = hostwinmoney;
			this.Status = status;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///标识
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
		///TitleId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid TitleId {get ; set ;}

		///<summary>
		///庄家的资金
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 HostMoney {get ; set ;}

		///<summary>
		///奖池总奖金
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 TotalMoney {get ; set ;}

		///<summary>
		///参与人数
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 AttendPeopleCount {get ; set ;}

		///<summary>
		///庄家赢的钱
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 HostWinMoney {get ; set ;}

		///<summary>
		///状态
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///创建时间
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///版本号
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public GambleHostEntity Clone()
        {
            GambleHostEntity entity = new GambleHostEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ManagerName = this.ManagerName;
			entity.TitleId = this.TitleId;
			entity.HostMoney = this.HostMoney;
			entity.TotalMoney = this.TotalMoney;
			entity.AttendPeopleCount = this.AttendPeopleCount;
			entity.HostWinMoney = this.HostWinMoney;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Gamble_Host 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GambleHostResponse : BaseResponse<GambleHostEntity>
    {

    }
}
