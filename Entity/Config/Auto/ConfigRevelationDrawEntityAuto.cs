
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationDraw 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationdrawEntity
	{
		
		public ConfigRevelationdrawEntity()
		{
		}

		public ConfigRevelationdrawEntity(
		System.Int32 idx
,				System.Int32 prizetype
,				System.Int32 subtype
,				System.Int32 itemcount
,				System.Int32 startrate
,				System.Int32 endrate
		)
		{
			this.Idx = idx;
			this.PrizeType = prizetype;
			this.SubType = subtype;
			this.ItemCount = itemcount;
			this.StartRate = startrate;
			this.EndRate = endrate;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///PrizeType
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PrizeType {get ; set ;}

		///<summary>
		///SubType
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///ItemCount
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemCount {get ; set ;}

		///<summary>
		///StartRate
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 StartRate {get ; set ;}

		///<summary>
		///EndRate
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 EndRate {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationdrawEntity Clone()
        {
            ConfigRevelationdrawEntity entity = new ConfigRevelationdrawEntity();
			entity.Idx = this.Idx;
			entity.PrizeType = this.PrizeType;
			entity.SubType = this.SubType;
			entity.ItemCount = this.ItemCount;
			entity.StartRate = this.StartRate;
			entity.EndRate = this.EndRate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationDraw 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationdrawResponse : BaseResponse<ConfigRevelationdrawEntity>
    {

    }
}
