
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Tx_YellowVip 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TxYellowvipEntity
	{
		
		public TxYellowvipEntity()
		{
		}

		public TxYellowvipEntity(
		System.String account
,				System.Boolean isyellowvip
,				System.Boolean isyellowyearvip
,				System.Boolean isyellowhighvip
,				System.Int32 yellowviplevel
,				System.Int32 openingtimes
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.String extradata
		)
		{
			this.Account = account;
			this.IsYellowVip = isyellowvip;
			this.IsYellowYearVip = isyellowyearvip;
			this.IsYellowHighVip = isyellowhighvip;
			this.YellowVipLevel = yellowviplevel;
			this.OpeningTimes = openingtimes;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.ExtraData = extradata;
		}
		
		#region Public Properties
		
		///<summary>
		///Account
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String Account {get ; set ;}

		///<summary>
		///IsYellowVip
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Boolean IsYellowVip {get ; set ;}

		///<summary>
		///IsYellowYearVip
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Boolean IsYellowYearVip {get ; set ;}

		///<summary>
		///IsYellowHighVip
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Boolean IsYellowHighVip {get ; set ;}

		///<summary>
		///YellowVipLevel
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 YellowVipLevel {get ; set ;}

		///<summary>
		///OpeningTimes
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 OpeningTimes {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///ExtraData
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String ExtraData {get ; set ;}
		#endregion
        
        #region Clone
        public TxYellowvipEntity Clone()
        {
            TxYellowvipEntity entity = new TxYellowvipEntity();
			entity.Account = this.Account;
			entity.IsYellowVip = this.IsYellowVip;
			entity.IsYellowYearVip = this.IsYellowYearVip;
			entity.IsYellowHighVip = this.IsYellowHighVip;
			entity.YellowVipLevel = this.YellowVipLevel;
			entity.OpeningTimes = this.OpeningTimes;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.ExtraData = this.ExtraData;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Tx_YellowVip 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TxYellowvipResponse : BaseResponse<TxYellowvipEntity>
    {

    }
}
