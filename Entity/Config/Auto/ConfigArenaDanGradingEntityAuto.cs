
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_ArenaDanGrading 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigArenadangradingEntity
	{
		
		public ConfigArenadangradingEntity()
		{
		}

		public ConfigArenadangradingEntity(
		System.Int32 idx
,				System.Int32 integral
,				System.Int32 prizearenacoin
		)
		{
			this.Idx = idx;
			this.Integral = integral;
			this.PrizeArenaCoin = prizearenacoin;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///所需积分
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Integral {get ; set ;}

		///<summary>
		///奖励竞技币
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PrizeArenaCoin {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigArenadangradingEntity Clone()
        {
            ConfigArenadangradingEntity entity = new ConfigArenadangradingEntity();
			entity.Idx = this.Idx;
			entity.Integral = this.Integral;
			entity.PrizeArenaCoin = this.PrizeArenaCoin;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_ArenaDanGrading 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigArenadangradingResponse : BaseResponse<ConfigArenadangradingEntity>
    {

    }
}
