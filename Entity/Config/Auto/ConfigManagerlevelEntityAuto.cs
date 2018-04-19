
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_ManagerLevel 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigManagerlevelEntity
	{
		
		public ConfigManagerlevelEntity()
		{
		}

		public ConfigManagerlevelEntity(
		System.Int32 level
,				System.Int32 exp
,				System.Int32 skillcount
,				System.Int32 maxstamina
		)
		{
			this.Level = level;
			this.Exp = exp;
			this.SkillCount = skillcount;
			this.MaxStamina = maxstamina;
		}
		
		#region Public Properties
		
		///<summary>
		///Level
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///Exp
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Exp {get ; set ;}

		///<summary>
		///SkillCount
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SkillCount {get ; set ;}

		///<summary>
		///MaxStamina
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 MaxStamina {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigManagerlevelEntity Clone()
        {
            ConfigManagerlevelEntity entity = new ConfigManagerlevelEntity();
			entity.Level = this.Level;
			entity.Exp = this.Exp;
			entity.SkillCount = this.SkillCount;
			entity.MaxStamina = this.MaxStamina;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_ManagerLevel 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigManagerlevelResponse : BaseResponse<ConfigManagerlevelEntity>
    {

    }
}

