
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_SkillUpgrade 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigSkillupgradeEntity
	{
		
		public ConfigSkillupgradeEntity()
		{
		}

		public ConfigSkillupgradeEntity(
		System.Int32 idx
,				System.Int32 skilllevel
,				System.Int32 quality
,				System.Int32 coin
		)
		{
			this.Idx = idx;
			this.SkillLevel = skilllevel;
			this.Quality = quality;
			this.Coin = coin;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///技能等级
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///品质
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Quality {get ; set ;}

		///<summary>
		///消耗金币
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Coin {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigSkillupgradeEntity Clone()
        {
            ConfigSkillupgradeEntity entity = new ConfigSkillupgradeEntity();
			entity.Idx = this.Idx;
			entity.SkillLevel = this.SkillLevel;
			entity.Quality = this.Quality;
			entity.Coin = this.Coin;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_SkillUpgrade 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigSkillupgradeResponse : BaseResponse<ConfigSkillupgradeEntity>
    {

    }
}

