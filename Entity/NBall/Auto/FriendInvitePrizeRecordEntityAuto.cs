
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.FriendInvite_PrizeRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class FriendinvitePrizerecordEntity
	{
		
		public FriendinvitePrizerecordEntity()
		{
		}

		public FriendinvitePrizerecordEntity(
		System.Int32 idx
,				System.String account
,				System.Int32 prizetype
,				System.String prizeinfo
,				System.String prizestring
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.Account = account;
			this.PrizeType = prizetype;
			this.PrizeInfo = prizeinfo;
			this.PrizeString = prizestring;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///领取人
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Account {get ; set ;}

		///<summary>
		///领取奖励类型 0= 邀请奖励 1= 成长奖励
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///奖励详情 prizeType =1时存的是领取的被邀请人ID,分割
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String PrizeInfo {get ; set ;}

		///<summary>
		///领取奖励物品串 prizeType = 0时 存的是 ItemCode,ItemNumber|ItemCode,ItemNumber = 1时存的是点卷数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String PrizeString {get ; set ;}

		///<summary>
		///领取时间
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public FriendinvitePrizerecordEntity Clone()
        {
            FriendinvitePrizerecordEntity entity = new FriendinvitePrizerecordEntity();
			entity.Idx = this.Idx;
			entity.Account = this.Account;
			entity.PrizeType = this.PrizeType;
			entity.PrizeInfo = this.PrizeInfo;
			entity.PrizeString = this.PrizeString;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.FriendInvite_PrizeRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class FriendinvitePrizerecordResponse : BaseResponse<FriendinvitePrizerecordEntity>
    {

    }
}

