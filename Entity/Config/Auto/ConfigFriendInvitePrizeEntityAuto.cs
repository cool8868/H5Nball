
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_FriendInvitePrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigFriendinviteprizeEntity
	{
		
		public ConfigFriendinviteprizeEntity()
		{
		}

		public ConfigFriendinviteprizeEntity(
		System.Int32 idx
,				System.Int32 succeedcount
,				System.Int32 prizetype
,				System.Int32 itemcode
,				System.Int32 count
,				System.Boolean isbinding
		)
		{
			this.Idx = idx;
			this.SucceedCount = succeedcount;
			this.PrizeType = prizetype;
			this.ItemCode = itemcode;
			this.Count = count;
			this.IsBinding = isbinding;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///邀请成功人数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SucceedCount {get ; set ;}

		///<summary>
		///奖励类型 1=金币 2= 物品
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///物品ID
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///数量
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Count {get ; set ;}

		///<summary>
		///是否绑定
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Boolean IsBinding {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigFriendinviteprizeEntity Clone()
        {
            ConfigFriendinviteprizeEntity entity = new ConfigFriendinviteprizeEntity();
			entity.Idx = this.Idx;
			entity.SucceedCount = this.SucceedCount;
			entity.PrizeType = this.PrizeType;
			entity.ItemCode = this.ItemCode;
			entity.Count = this.Count;
			entity.IsBinding = this.IsBinding;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_FriendInvitePrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigFriendinviteprizeResponse : BaseResponse<ConfigFriendinviteprizeEntity>
    {

    }
}

