
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_GambleIcon 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigGambleiconEntity
	{
		
		public ConfigGambleiconEntity()
		{
		}

		public ConfigGambleiconEntity(
		System.String name
,				System.Int32 idx
		)
		{
			this.Name = name;
			this.Idx = idx;
		}
		
		#region Public Properties
		
		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String Name {get ; set ;}

		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Idx {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigGambleiconEntity Clone()
        {
            ConfigGambleiconEntity entity = new ConfigGambleiconEntity();
			entity.Name = this.Name;
			entity.Idx = this.Idx;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_GambleIcon 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigGambleiconResponse : BaseResponse<ConfigGambleiconEntity>
    {

    }
}
