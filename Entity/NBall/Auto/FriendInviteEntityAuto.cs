
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.FriendInvite 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class FriendinviteEntity
	{
		
		public FriendinviteEntity()
		{
		}

		public FriendinviteEntity(
		System.String byaccount
,				System.String account
,				System.Int32 level
,				System.Boolean isprize
,				System.Int32 mayprize
,				System.Int32 alreadyprize
		)
		{
			this.ByAccount = byaccount;
			this.Account = account;
			this.Level = level;
			this.IsPrize = isprize;
			this.MayPrize = mayprize;
			this.AlreadyPrize = alreadyprize;
		}
		
		#region Public Properties
		
		///<summary>
		///被邀请的经理ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String ByAccount {get ; set ;}

		///<summary>
		///邀请的经理ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Account {get ; set ;}

		///<summary>
		///等级
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///是否可以领取
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Boolean IsPrize {get ; set ;}

		///<summary>
		///可以领取多少点
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 MayPrize {get ; set ;}

		///<summary>
		///已经领取了多少点
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 AlreadyPrize {get ; set ;}
		#endregion
        
        #region Clone
        public FriendinviteEntity Clone()
        {
            FriendinviteEntity entity = new FriendinviteEntity();
			entity.ByAccount = this.ByAccount;
			entity.Account = this.Account;
			entity.Level = this.Level;
			entity.IsPrize = this.IsPrize;
			entity.MayPrize = this.MayPrize;
			entity.AlreadyPrize = this.AlreadyPrize;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.FriendInvite 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class FriendinviteResponse : BaseResponse<FriendinviteEntity>
    {

    }
}
