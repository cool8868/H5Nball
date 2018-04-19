
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Synthesis 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigSynthesisEntity
	{
		
		public ConfigSynthesisEntity()
		{
		}

		public ConfigSynthesisEntity(
		System.Int32 idx
,				System.Int32 cardlevel
,				System.Int32 rate
,				System.Int32 coin
,				System.Int32 cardlibrary
,				System.Int32 protectcode
		)
		{
			this.Idx = idx;
			this.CardLevel = cardlevel;
			this.Rate = rate;
			this.Coin = coin;
			this.CardLibrary = cardlibrary;
			this.ProtectCode = protectcode;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///球员卡颜色
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CardLevel {get ; set ;}

		///<summary>
		///概率(0-10000)
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Rate {get ; set ;}

		///<summary>
		///消耗游戏币
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Coin {get ; set ;}

		///<summary>
		///合成结果对应的卡库
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 CardLibrary {get ; set ;}

		///<summary>
		///合成保护商品编码
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ProtectCode {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigSynthesisEntity Clone()
        {
            ConfigSynthesisEntity entity = new ConfigSynthesisEntity();
			entity.Idx = this.Idx;
			entity.CardLevel = this.CardLevel;
			entity.Rate = this.Rate;
			entity.Coin = this.Coin;
			entity.CardLibrary = this.CardLibrary;
			entity.ProtectCode = this.ProtectCode;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Synthesis 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigSynthesisResponse : BaseResponse<ConfigSynthesisEntity>
    {

    }
}

