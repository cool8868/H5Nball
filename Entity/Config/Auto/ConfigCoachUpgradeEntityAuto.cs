
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CoachUpgrade 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCoachupgradeEntity
	{
		
		public ConfigCoachupgradeEntity()
		{
		}

		public ConfigCoachupgradeEntity(
		System.Int32 level
,				System.Int32 upgradeexp
,				System.Int32 upgradesumexp
,				System.Int32 upgradeskillcoin
		)
		{
			this.Level = level;
			this.UpgradeExp = upgradeexp;
			this.UpgradeSumExp = upgradesumexp;
			this.UpgradeSkillCoin = upgradeskillcoin;
		}
		
		#region Public Properties
		
		///<summary>
		///Level
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///UpgradeExp
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 UpgradeExp {get ; set ;}

		///<summary>
		///UpgradeSumExp
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 UpgradeSumExp {get ; set ;}

		///<summary>
		///UpgradeSkillCoin
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 UpgradeSkillCoin {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCoachupgradeEntity Clone()
        {
            ConfigCoachupgradeEntity entity = new ConfigCoachupgradeEntity();
			entity.Level = this.Level;
			entity.UpgradeExp = this.UpgradeExp;
			entity.UpgradeSumExp = this.UpgradeSumExp;
			entity.UpgradeSkillCoin = this.UpgradeSkillCoin;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CoachUpgrade 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCoachupgradeResponse : BaseResponse<ConfigCoachupgradeEntity>
    {

    }
}
