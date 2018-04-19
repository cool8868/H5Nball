
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_MallDirect 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigMalldirectEntity
	{
		
		public ConfigMalldirectEntity()
		{
		}

		public ConfigMalldirectEntity(
		System.Int32 idx
,				System.Int32 consumesourcetype
,				System.Int32 usedcount
,				System.Int32 point
		)
		{
			this.Idx = idx;
			this.ConsumeSourceType = consumesourcetype;
			this.UsedCount = usedcount;
			this.Point = point;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ConsumeSourceType
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ConsumeSourceType {get ; set ;}

		///<summary>
		///UsedCount
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 UsedCount {get ; set ;}

		///<summary>
		///Point
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Point {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigMalldirectEntity Clone()
        {
            ConfigMalldirectEntity entity = new ConfigMalldirectEntity();
			entity.Idx = this.Idx;
			entity.ConsumeSourceType = this.ConsumeSourceType;
			entity.UsedCount = this.UsedCount;
			entity.Point = this.Point;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_MallDirect 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigMalldirectResponse : BaseResponse<ConfigMalldirectEntity>
    {

    }
}

