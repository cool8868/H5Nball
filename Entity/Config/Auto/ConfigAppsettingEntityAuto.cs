
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_AppSetting 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigAppsettingEntity
	{
		
		public ConfigAppsettingEntity()
		{
		}

		public ConfigAppsettingEntity(
		System.Int32 idx
,				System.String key
,				System.String value
,				System.String description
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.Key = key;
			this.Value = value;
			this.Description = description;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///Key
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Key {get ; set ;}

		///<summary>
		///Value
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Value {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Description {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigAppsettingEntity Clone()
        {
            ConfigAppsettingEntity entity = new ConfigAppsettingEntity();
			entity.Idx = this.Idx;
			entity.Key = this.Key;
			entity.Value = this.Value;
			entity.Description = this.Description;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_AppSetting 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigAppsettingResponse : BaseResponse<ConfigAppsettingEntity>
    {

    }
}

