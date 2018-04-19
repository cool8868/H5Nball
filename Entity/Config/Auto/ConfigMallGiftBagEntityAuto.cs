
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_MallGiftBag 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigMallgiftbagEntity
	{
		
		public ConfigMallgiftbagEntity()
		{
		}

		public ConfigMallgiftbagEntity(
		System.Int32 idx
,				System.Int32 mallcode
,				System.Int32 prizetype
,				System.Int32 subtype
,				System.Int32 itemcount
		)
		{
			this.Idx = idx;
			this.MallCode = mallcode;
			this.PrizeType = prizetype;
			this.SubType = subtype;
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
		///MallCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 MallCode {get ; set ;}

		///<summary>
		///奖励类型  1钻石 2金币  3指定物品
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///指定物品的code
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///ItemCount
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ItemCount {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigMallgiftbagEntity Clone()
        {
            ConfigMallgiftbagEntity entity = new ConfigMallgiftbagEntity();
			entity.Idx = this.Idx;
			entity.MallCode = this.MallCode;
			entity.PrizeType = this.PrizeType;
			entity.SubType = this.SubType;
			entity.ItemCount = this.ItemCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_MallGiftBag 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigMallgiftbagResponse : BaseResponse<ConfigMallgiftbagEntity>
    {

    }
}

