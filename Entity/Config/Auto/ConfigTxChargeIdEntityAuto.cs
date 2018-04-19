
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_TxChargeId 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigTxchargeidEntity
	{
		
		public ConfigTxchargeidEntity()
		{
		}

		public ConfigTxchargeidEntity(
		System.Int32 idx
,				System.Int32 zonetype
,				System.Int32 mallcode
,				System.Int32 txitemid
		)
		{
			this.Idx = idx;
			this.ZoneType = zonetype;
			this.MallCode = mallcode;
			this.TxItemId = txitemid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ZoneType
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ZoneType {get ; set ;}

		///<summary>
		///MallCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MallCode {get ; set ;}

		///<summary>
		///TxItemId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 TxItemId {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigTxchargeidEntity Clone()
        {
            ConfigTxchargeidEntity entity = new ConfigTxchargeidEntity();
			entity.Idx = this.Idx;
			entity.ZoneType = this.ZoneType;
			entity.MallCode = this.MallCode;
			entity.TxItemId = this.TxItemId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_TxChargeId 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigTxchargeidResponse : BaseResponse<ConfigTxchargeidEntity>
    {

    }
}
