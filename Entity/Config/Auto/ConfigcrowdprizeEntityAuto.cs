
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CrowdPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCrowdprizeEntity
	{
		
		public ConfigCrowdprizeEntity()
		{
		}

		public ConfigCrowdprizeEntity(
		System.Int32 idx
,				System.Int32 wintype
,				System.Int32 coin
,				System.Int32 honor
		)
		{
			this.Idx = idx;
			this.WinType = wintype;
			this.Coin = coin;
			this.Honor = honor;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///WinType
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 WinType {get ; set ;}

		///<summary>
		///Coin
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Coin {get ; set ;}

		///<summary>
		///Honor
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Honor {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCrowdprizeEntity Clone()
        {
            ConfigCrowdprizeEntity entity = new ConfigCrowdprizeEntity();
			entity.Idx = this.Idx;
			entity.WinType = this.WinType;
			entity.Coin = this.Coin;
			entity.Honor = this.Honor;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CrowdPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCrowdprizeResponse : BaseResponse<ConfigCrowdprizeEntity>
    {

    }
}
