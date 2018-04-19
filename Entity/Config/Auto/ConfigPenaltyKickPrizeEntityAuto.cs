
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_PenaltyKickPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigPenaltykickprizeEntity
	{
		
		public ConfigPenaltykickprizeEntity()
		{
		}

		public ConfigPenaltykickprizeEntity(
		System.Int32 idx
,				System.Int32 prizetype
,				System.Int32 prizesub
,				System.Int32 itemtype
,				System.Int32 itemcode
,				System.Int32 itemcount
		)
		{
			this.Idx = idx;
			this.PrizeType = prizetype;
			this.PrizeSub = prizesub;
			this.ItemType = itemtype;
			this.ItemCode = itemcode;
			this.ItemCount = itemcount;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///奖励类型 1进球奖励 2连续进球奖励 3排名奖励 4兑换的物品
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///奖励二级类型 
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizeSub {get ; set ;}

		///<summary>
		///奖励物品类型  1=金币 2=点卷 3=物品 4=卡库
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemType {get ; set ;}

		///<summary>
		///奖励物品code
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///奖励物品数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ItemCount {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigPenaltykickprizeEntity Clone()
        {
            ConfigPenaltykickprizeEntity entity = new ConfigPenaltykickprizeEntity();
			entity.Idx = this.Idx;
			entity.PrizeType = this.PrizeType;
			entity.PrizeSub = this.PrizeSub;
			entity.ItemType = this.ItemType;
			entity.ItemCode = this.ItemCode;
			entity.ItemCount = this.ItemCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_PenaltyKickPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigPenaltykickprizeResponse : BaseResponse<ConfigPenaltykickprizeEntity>
    {

    }
}
