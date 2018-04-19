
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_MallExtra 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigMallextraEntity
	{
		
		public ConfigMallextraEntity()
		{
		}

		public ConfigMallextraEntity(
		System.Int32 idx
,				System.Int32 extratype
,				System.Int32 usedcount
,				System.Int32 mallcode
		)
		{
			this.Idx = idx;
			this.ExtraType = extratype;
			this.UsedCount = usedcount;
			this.MallCode = mallcode;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///特殊商品类型
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ExtraType {get ; set ;}

		///<summary>
		///使用次数
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 UsedCount {get ; set ;}

		///<summary>
		///对应商城道具code
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 MallCode {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigMallextraEntity Clone()
        {
            ConfigMallextraEntity entity = new ConfigMallextraEntity();
			entity.Idx = this.Idx;
			entity.ExtraType = this.ExtraType;
			entity.UsedCount = this.UsedCount;
			entity.MallCode = this.MallCode;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_MallExtra 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigMallextraResponse : BaseResponse<ConfigMallextraEntity>
    {

    }
}

