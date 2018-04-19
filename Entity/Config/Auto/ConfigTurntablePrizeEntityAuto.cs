
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_TurntablePrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigTurntableprizeEntity
	{
		
		public ConfigTurntableprizeEntity()
		{
		}

		public ConfigTurntableprizeEntity(
		System.Int32 idx
,				System.Int32 turntableid
,				System.Int32 turntabletype
,				System.Int32 prizetype
,				System.Int32 subtype
,				System.Int32 itemcount
,				System.String specialstring
,				System.Int32 initialrate
		)
		{
			this.Idx = idx;
			this.TurntableId = turntableid;
			this.TurntableType = turntabletype;
			this.PrizeType = prizetype;
			this.SubType = subtype;
			this.ItemCount = itemcount;
			this.SpecialString = specialstring;
			this.InitialRate = initialrate;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///转盘项ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 TurntableId {get ; set ;}

		///<summary>
		///转盘类型 1青铜 2白银 3黄金
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 TurntableType {get ; set ;}

		///<summary>
		///奖励类型  1钻石  2金币  3指定物品  4 卡库 5转盘 6特殊处理的物品
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///二级分类
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///物品数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ItemCount {get ; set ;}

		///<summary>
		///特殊物品串
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String SpecialString {get ; set ;}

		///<summary>
		///初始概率
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 InitialRate {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigTurntableprizeEntity Clone()
        {
            ConfigTurntableprizeEntity entity = new ConfigTurntableprizeEntity();
			entity.Idx = this.Idx;
			entity.TurntableId = this.TurntableId;
			entity.TurntableType = this.TurntableType;
			entity.PrizeType = this.PrizeType;
			entity.SubType = this.SubType;
			entity.ItemCount = this.ItemCount;
			entity.SpecialString = this.SpecialString;
			entity.InitialRate = this.InitialRate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_TurntablePrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigTurntableprizeResponse : BaseResponse<ConfigTurntableprizeEntity>
    {

    }
}
