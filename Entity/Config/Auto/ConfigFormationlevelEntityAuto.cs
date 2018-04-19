
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_FormationLevel 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigFormationlevelEntity
	{
		
		public ConfigFormationlevelEntity()
		{
		}

		public ConfigFormationlevelEntity(
		System.Int32 level
,				System.Int32 sophisticate
		)
		{
			this.Level = level;
			this.Sophisticate = sophisticate;
		}
		
		#region Public Properties
		
		///<summary>
		///等级
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///需要的阅历
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Sophisticate {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigFormationlevelEntity Clone()
        {
            ConfigFormationlevelEntity entity = new ConfigFormationlevelEntity();
			entity.Level = this.Level;
			entity.Sophisticate = this.Sophisticate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_FormationLevel 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigFormationlevelResponse : BaseResponse<ConfigFormationlevelEntity>
    {

    }
}

