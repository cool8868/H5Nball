
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationVIP 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationvipEntity
	{
		
		public ConfigRevelationvipEntity()
		{
		}

		public ConfigRevelationvipEntity(
		System.Int32 viplevel
,				System.Int32 challenges
,				System.Int32 cdtime
,				System.Boolean itemisbind
		)
		{
			this.VipLevel = viplevel;
			this.Challenges = challenges;
			this.CDTime = cdtime;
			this.ItemIsBind = itemisbind;
		}
		
		#region Public Properties
		
		///<summary>
		///VipLevel
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 VipLevel {get ; set ;}

		///<summary>
		///可通关次数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Challenges {get ; set ;}

		///<summary>
		///挑战失败CD时间
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 CDTime {get ; set ;}

		///<summary>
		///勇气商城物品是否绑定
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Boolean ItemIsBind {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationvipEntity Clone()
        {
            ConfigRevelationvipEntity entity = new ConfigRevelationvipEntity();
			entity.VipLevel = this.VipLevel;
			entity.Challenges = this.Challenges;
			entity.CDTime = this.CDTime;
			entity.ItemIsBind = this.ItemIsBind;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationVIP 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationvipResponse : BaseResponse<ConfigRevelationvipEntity>
    {

    }
}

