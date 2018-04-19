
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.All_CSDK 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class AllCsdkEntity
	{
		
		public AllCsdkEntity()
		{
		}

		public AllCsdkEntity(
		System.Int32 idx
,				System.String _sign
,				System.Int32 orderid
,				System.String gameorderid
,				System.Int32 price
,				System.String channelalias
,				System.String playerid
,				System.String serverid
,				System.Int32 goodsid
,				System.Int32 payresult
,				System.String _state
,				System.DateTime paytime
		)
		{
			this.Idx = idx;
			this._sign = _sign;
			this.orderId = orderid;
			this.gameOrderId = gameorderid;
			this.price = price;
			this.channelAlias = channelalias;
			this.playerId = playerid;
			this.serverId = serverid;
			this.goodsId = goodsid;
			this.payResult = payresult;
			this._state = _state;
			this.payTime = paytime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///_sign
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String _sign {get ; set ;}

		///<summary>
		///orderId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 orderId {get ; set ;}

		///<summary>
		///gameOrderId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String gameOrderId {get ; set ;}

		///<summary>
		///price
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 price {get ; set ;}

		///<summary>
		///channelAlias
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String channelAlias {get ; set ;}

		///<summary>
		///playerId
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String playerId {get ; set ;}

		///<summary>
		///serverId
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String serverId {get ; set ;}

		///<summary>
		///goodsId
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 goodsId {get ; set ;}

		///<summary>
		///payResult
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 payResult {get ; set ;}

		///<summary>
		///_state
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String _state {get ; set ;}

		///<summary>
		///payTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.DateTime payTime {get ; set ;}
		#endregion
        
        #region Clone
        public AllCsdkEntity Clone()
        {
            AllCsdkEntity entity = new AllCsdkEntity();
			entity.Idx = this.Idx;
			entity._sign = this._sign;
			entity.orderId = this.orderId;
			entity.gameOrderId = this.gameOrderId;
			entity.price = this.price;
			entity.channelAlias = this.channelAlias;
			entity.playerId = this.playerId;
			entity.serverId = this.serverId;
			entity.goodsId = this.goodsId;
			entity.payResult = this.payResult;
			entity._state = this._state;
			entity.payTime = this.payTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.All_CSDK 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class AllCsdkResponse : BaseResponse<AllCsdkEntity>
    {

    }
}
