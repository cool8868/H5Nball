
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Scouting_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ScoutingManagerEntity
	{
		
		public ScoutingManagerEntity()
		{
		}

		public ScoutingManagerEntity(
		System.Guid managerid
,				System.Int32 coinlotterycount
,				System.Int32 cointenlotterycount
,				System.Int32 pointlotterycount
,				System.Int32 pointtenlotterycount
,				System.Int32 friendlotterycount
,				System.Int32 friendtenlotterycount
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Int32 specialitemcoin
,				System.Int32 specialitempoint
,				System.Int32 specialitemfriend
		)
		{
			this.ManagerId = managerid;
			this.CoinLotteryCount = coinlotterycount;
			this.CoinTenLotteryCount = cointenlotterycount;
			this.PointLotteryCount = pointlotterycount;
			this.PointTenLotteryCount = pointtenlotterycount;
			this.FriendLotteryCount = friendlotterycount;
			this.FriendTenLotteryCount = friendtenlotterycount;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.SpecialItemCoin = specialitemcoin;
			this.SpecialItemPoint = specialitempoint;
			this.SpecialItemFriend = specialitemfriend;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///金币单抽次数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CoinLotteryCount {get ; set ;}

		///<summary>
		///金币十连抽次数
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 CoinTenLotteryCount {get ; set ;}

		///<summary>
		///点券单抽次数
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PointLotteryCount {get ; set ;}

		///<summary>
		///点券十连抽次数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PointTenLotteryCount {get ; set ;}

		///<summary>
		///友情点单抽次数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 FriendLotteryCount {get ; set ;}

		///<summary>
		///友情点十连抽次数
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 FriendTenLotteryCount {get ; set ;}

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
		///SpecialItemCoin
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 SpecialItemCoin {get ; set ;}

		///<summary>
		///SpecialItemPoint
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 SpecialItemPoint {get ; set ;}

		///<summary>
		///SpecialItemFriend
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 SpecialItemFriend {get ; set ;}
		#endregion
        
        #region Clone
        public ScoutingManagerEntity Clone()
        {
            ScoutingManagerEntity entity = new ScoutingManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.CoinLotteryCount = this.CoinLotteryCount;
			entity.CoinTenLotteryCount = this.CoinTenLotteryCount;
			entity.PointLotteryCount = this.PointLotteryCount;
			entity.PointTenLotteryCount = this.PointTenLotteryCount;
			entity.FriendLotteryCount = this.FriendLotteryCount;
			entity.FriendTenLotteryCount = this.FriendTenLotteryCount;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.SpecialItemCoin = this.SpecialItemCoin;
			entity.SpecialItemPoint = this.SpecialItemPoint;
			entity.SpecialItemFriend = this.SpecialItemFriend;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Scouting_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ScoutingManagerResponse : BaseResponse<ScoutingManagerEntity>
    {

    }
}
