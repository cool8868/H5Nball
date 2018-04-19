
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.All_UaPlatform 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AllUaplatformEntity
	{
		
		public AllUaplatformEntity()
		{
		}

		public AllUaplatformEntity(
		System.Int32 idx
,				System.String factorycode
,				System.String platformcode
,				System.Int32 exchangerate
,				System.Int32 cashrate
,				System.String chargeurl
,				System.String platformurl
,				System.String loginkey
,				System.String chargekey
,				System.String clientversion
,				System.String useractionurl
		)
		{
			this.Idx = idx;
			this.FactoryCode = factorycode;
			this.PlatformCode = platformcode;
			this.ExchangeRate = exchangerate;
			this.CashRate = cashrate;
			this.ChargeUrl = chargeurl;
			this.PlatformUrl = platformurl;
			this.LoginKey = loginkey;
			this.ChargeKey = chargekey;
			this.ClientVersion = clientversion;
			this.UserActionUrl = useractionurl;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///工厂编码
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String FactoryCode {get ; set ;}

		///<summary>
		///平台编码
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String PlatformCode {get ; set ;}

		///<summary>
		///兑换点券比例
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ExchangeRate {get ; set ;}

		///<summary>
		///现金比例
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 CashRate {get ; set ;}

		///<summary>
		///ChargeUrl
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String ChargeUrl {get ; set ;}

		///<summary>
		///平台地址
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String PlatformUrl {get ; set ;}

		///<summary>
		///登录key
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String LoginKey {get ; set ;}

		///<summary>
		///充值key
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String ChargeKey {get ; set ;}

		///<summary>
		///客户端版本号
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String ClientVersion {get ; set ;}

		///<summary>
		///UserActionUrl
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String UserActionUrl {get ; set ;}
		#endregion
        
        #region Clone
        public AllUaplatformEntity Clone()
        {
            AllUaplatformEntity entity = new AllUaplatformEntity();
			entity.Idx = this.Idx;
			entity.FactoryCode = this.FactoryCode;
			entity.PlatformCode = this.PlatformCode;
			entity.ExchangeRate = this.ExchangeRate;
			entity.CashRate = this.CashRate;
			entity.ChargeUrl = this.ChargeUrl;
			entity.PlatformUrl = this.PlatformUrl;
			entity.LoginKey = this.LoginKey;
			entity.ChargeKey = this.ChargeKey;
			entity.ClientVersion = this.ClientVersion;
			entity.UserActionUrl = this.UserActionUrl;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.All_UaPlatform 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AllUaplatformResponse : BaseResponse<AllUaplatformEntity>
    {

    }
}
