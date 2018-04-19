
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_PlayerLevel 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigPlayerlevelEntity
	{
		
		public ConfigPlayerlevelEntity()
		{
		}

		public ConfigPlayerlevelEntity(
		System.Int32 level
,				System.Int32 exp
,				System.Int32 propertycount
		)
		{
			this.Level = level;
			this.Exp = exp;
			this.PropertyCount = propertycount;
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
		///PropertyCount
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PropertyCount {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigPlayerlevelEntity Clone()
        {
            ConfigPlayerlevelEntity entity = new ConfigPlayerlevelEntity();
			entity.Level = this.Level;
			entity.Exp = this.Exp;
			entity.PropertyCount = this.PropertyCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_PlayerLevel 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigPlayerlevelResponse : BaseResponse<ConfigPlayerlevelEntity>
    {

    }
}

