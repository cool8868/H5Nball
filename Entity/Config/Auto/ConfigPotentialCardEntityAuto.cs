
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_PotentialCard 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigPotentialcardEntity
	{
		
		public ConfigPotentialcardEntity()
		{
		}

		public ConfigPotentialcardEntity(
		System.Int32 idx
,				System.String cardlevel
,				System.Int32 potentiallevel
,				System.Int32 rate
		)
		{
			this.Idx = idx;
			this.CardLevel = cardlevel;
			this.PotentialLevel = potentiallevel;
			this.Rate = rate;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///CardLevel
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String CardLevel {get ; set ;}

		///<summary>
		///PotentialLevel
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PotentialLevel {get ; set ;}

		///<summary>
		///Rate
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Rate {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigPotentialcardEntity Clone()
        {
            ConfigPotentialcardEntity entity = new ConfigPotentialcardEntity();
			entity.Idx = this.Idx;
			entity.CardLevel = this.CardLevel;
			entity.PotentialLevel = this.PotentialLevel;
			entity.Rate = this.Rate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_PotentialCard 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigPotentialcardResponse : BaseResponse<ConfigPotentialcardEntity>
    {

    }
}
