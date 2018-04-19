
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Gamble_HostOptionRate 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GambleHostoptionrateEntity
	{
		
		public GambleHostoptionrateEntity()
		{
		}

		public GambleHostoptionrateEntity(
		System.Int32 idx
,				System.Int32 hostid
,				System.Guid optionid
,				System.Decimal winrate
,				System.Int32 gamblemoney
,				System.Int32 attendpeoplecount
,				System.Int32 status
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.Idx = idx;
			this.HostId = hostid;
			this.OptionId = optionid;
			this.WinRate = winrate;
			this.GambleMoney = gamblemoney;
			this.AttendPeopleCount = attendpeoplecount;
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
		///庄家ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 HostId {get ; set ;}

		///<summary>
		///选项ID
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid OptionId {get ; set ;}

		///<summary>
		///赔率
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Decimal WinRate {get ; set ;}

		///<summary>
		///玩家押注总额
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 GambleMoney {get ; set ;}

		///<summary>
		///参与人数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 AttendPeopleCount {get ; set ;}

		///<summary>
		///状态
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
        public GambleHostoptionrateEntity Clone()
        {
            GambleHostoptionrateEntity entity = new GambleHostoptionrateEntity();
			entity.Idx = this.Idx;
			entity.HostId = this.HostId;
			entity.OptionId = this.OptionId;
			entity.WinRate = this.WinRate;
			entity.GambleMoney = this.GambleMoney;
			entity.AttendPeopleCount = this.AttendPeopleCount;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Gamble_HostOptionRate 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GambleHostoptionrateResponse : BaseResponse<GambleHostoptionrateEntity>
    {

    }
}
