
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationAwary 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationawaryEntity
	{
		
		public ConfigRevelationawaryEntity()
		{
		}

		public ConfigRevelationawaryEntity(
		System.Int32 itemcore
,				System.Int32 itemnums
,				System.Boolean isbinding
		)
		{
			this.ItemCore = itemcore;
			this.ItemNums = itemnums;
			this.IsBinding = isbinding;
		}
		
		#region Public Properties
		
		///<summary>
		///物品ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 ItemCore {get ; set ;}

		///<summary>
		///物品数量
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ItemNums {get ; set ;}

		///<summary>
		///是否绑定
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Boolean IsBinding {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationawaryEntity Clone()
        {
            ConfigRevelationawaryEntity entity = new ConfigRevelationawaryEntity();
			entity.ItemCore = this.ItemCore;
			entity.ItemNums = this.ItemNums;
			entity.IsBinding = this.IsBinding;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationAwary 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationawaryResponse : BaseResponse<ConfigRevelationawaryEntity>
    {

    }
}

