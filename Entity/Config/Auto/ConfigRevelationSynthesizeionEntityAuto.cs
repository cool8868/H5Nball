
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationSynthesizeion 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationsynthesizeionEntity
	{
		
		public ConfigRevelationsynthesizeionEntity()
		{
		}

		public ConfigRevelationsynthesizeionEntity(
		System.Int32 idx
,				System.Int32 value
		)
		{
			this.Idx = idx;
			this.Value = value;
		}
		
		#region Public Properties
		
		///<summary>
		///类型
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///值
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Value {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationsynthesizeionEntity Clone()
        {
            ConfigRevelationsynthesizeionEntity entity = new ConfigRevelationsynthesizeionEntity();
			entity.Idx = this.Idx;
			entity.Value = this.Value;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationSynthesizeion 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationsynthesizeionResponse : BaseResponse<ConfigRevelationsynthesizeionEntity>
    {

    }
}

