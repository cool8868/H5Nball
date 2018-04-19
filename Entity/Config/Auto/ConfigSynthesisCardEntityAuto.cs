
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_SynthesisCard 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigSynthesiscardEntity
	{
		
		public ConfigSynthesiscardEntity()
		{
		}

		public ConfigSynthesiscardEntity(
		System.Int32 idx
,				System.Int32 cardlevel
,				System.Int32 coin
,				System.Int32 protectpoint
		)
		{
			this.Idx = idx;
			this.CardLevel = cardlevel;
			this.Coin = coin;
			this.ProtectPoint = protectpoint;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///球员卡级别
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CardLevel {get ; set ;}

		///<summary>
		///消耗金币
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Coin {get ; set ;}

		///<summary>
		///合成保护消耗点券
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ProtectPoint {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigSynthesiscardEntity Clone()
        {
            ConfigSynthesiscardEntity entity = new ConfigSynthesiscardEntity();
			entity.Idx = this.Idx;
			entity.CardLevel = this.CardLevel;
			entity.Coin = this.Coin;
			entity.ProtectPoint = this.ProtectPoint;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_SynthesisCard 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigSynthesiscardResponse : BaseResponse<ConfigSynthesiscardEntity>
    {

    }
}

