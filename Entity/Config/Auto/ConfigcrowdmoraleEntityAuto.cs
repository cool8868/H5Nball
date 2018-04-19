
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CrowdMorale 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCrowdmoraleEntity
	{
		
		public ConfigCrowdmoraleEntity()
		{
		}

		public ConfigCrowdmoraleEntity(
		System.Int32 idx
,				System.Int32 costmorela
		)
		{
			this.Idx = idx;
			this.CostMorela = costmorela;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///CostMorela
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CostMorela {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCrowdmoraleEntity Clone()
        {
            ConfigCrowdmoraleEntity entity = new ConfigCrowdmoraleEntity();
			entity.Idx = this.Idx;
			entity.CostMorela = this.CostMorela;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CrowdMorale 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCrowdmoraleResponse : BaseResponse<ConfigCrowdmoraleEntity>
    {

    }
}
