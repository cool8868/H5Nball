
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_EquipmentSynthesize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigEquipmentsynthesizeEntity
	{
		
		public ConfigEquipmentsynthesizeEntity()
		{
		}

		public ConfigEquipmentsynthesizeEntity(
		System.Int32 idx
,				System.Int32 quality
,				System.Int32 ratequality1
,				System.Int32 ratequality2
,				System.Int32 ratequality3
,				System.Int32 ratequality4
,				System.Int32 coin
		)
		{
			this.Idx = idx;
			this.Quality = quality;
			this.RateQuality1 = ratequality1;
			this.RateQuality2 = ratequality2;
			this.RateQuality3 = ratequality3;
			this.RateQuality4 = ratequality4;
			this.Coin = coin;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///Quality
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Quality {get ; set ;}

		///<summary>
		///概率(0-10000)
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 RateQuality1 {get ; set ;}

		///<summary>
		///RateQuality2
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 RateQuality2 {get ; set ;}

		///<summary>
		///RateQuality3
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 RateQuality3 {get ; set ;}

		///<summary>
		///RateQuality4
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 RateQuality4 {get ; set ;}

		///<summary>
		///消耗游戏币
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Coin {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigEquipmentsynthesizeEntity Clone()
        {
            ConfigEquipmentsynthesizeEntity entity = new ConfigEquipmentsynthesizeEntity();
			entity.Idx = this.Idx;
			entity.Quality = this.Quality;
			entity.RateQuality1 = this.RateQuality1;
			entity.RateQuality2 = this.RateQuality2;
			entity.RateQuality3 = this.RateQuality3;
			entity.RateQuality4 = this.RateQuality4;
			entity.Coin = this.Coin;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_EquipmentSynthesize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigEquipmentsynthesizeResponse : BaseResponse<ConfigEquipmentsynthesizeEntity>
    {

    }
}

