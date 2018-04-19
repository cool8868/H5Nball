
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Decompose 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigDecomposeEntity
	{
		
		public ConfigDecomposeEntity()
		{
		}

		public ConfigDecomposeEntity(
		System.Int32 idx
,				System.Int32 cardlevel
,				System.Int32 coin
,				System.Int32 critirate
,				System.Int32 equipmentrate
,				System.Int32 equipmentlotteryid
		)
		{
			this.Idx = idx;
			this.CardLevel = cardlevel;
			this.Coin = coin;
			this.CritiRate = critirate;
			this.EquipmentRate = equipmentrate;
			this.EquipmentLotteryId = equipmentlotteryid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///卡牌颜色
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CardLevel {get ; set ;}

		///<summary>
		///金币
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Coin {get ; set ;}

		///<summary>
		///暴击率
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CritiRate {get ; set ;}

		///<summary>
		///分解获得装备概率
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 EquipmentRate {get ; set ;}

		///<summary>
		///装备抽取id
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 EquipmentLotteryId {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigDecomposeEntity Clone()
        {
            ConfigDecomposeEntity entity = new ConfigDecomposeEntity();
			entity.Idx = this.Idx;
			entity.CardLevel = this.CardLevel;
			entity.Coin = this.Coin;
			entity.CritiRate = this.CritiRate;
			entity.EquipmentRate = this.EquipmentRate;
			entity.EquipmentLotteryId = this.EquipmentLotteryId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Decompose 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigDecomposeResponse : BaseResponse<ConfigDecomposeEntity>
    {

    }
}

