
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_PlayerkillPoint 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigPlayerkillpointEntity
	{
		
		public ConfigPlayerkillpointEntity()
		{
		}

		public ConfigPlayerkillpointEntity(
		System.Int32 idx
,				System.Int32 viplevel
,				System.Int32 prizepoint
,				System.Int32 totalpoint
		)
		{
			this.Idx = idx;
			this.VipLevel = viplevel;
			this.PrizePoint = prizepoint;
			this.TotalPoint = totalpoint;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///VipLevel
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 VipLevel {get ; set ;}

		///<summary>
		///PrizePoint
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizePoint {get ; set ;}

		///<summary>
		///TotalPoint
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 TotalPoint {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigPlayerkillpointEntity Clone()
        {
            ConfigPlayerkillpointEntity entity = new ConfigPlayerkillpointEntity();
			entity.Idx = this.Idx;
			entity.VipLevel = this.VipLevel;
			entity.PrizePoint = this.PrizePoint;
			entity.TotalPoint = this.TotalPoint;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_PlayerkillPoint 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigPlayerkillpointResponse : BaseResponse<ConfigPlayerkillpointEntity>
    {

    }
}
