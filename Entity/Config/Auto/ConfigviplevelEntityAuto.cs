
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_VipLevel 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigViplevelEntity
	{
		
		public ConfigViplevelEntity()
		{
		}

		public ConfigViplevelEntity(
		System.Int32 effectid
,				System.String name
,				System.Int32 vip0
,				System.Int32 vip1
,				System.Int32 vip2
,				System.Int32 vip3
,				System.Int32 vip4
,				System.Int32 vip5
,				System.Int32 vip6
,				System.Int32 vip7
,				System.Int32 vip8
,				System.Int32 vip9
,				System.Int32 vip10
		)
		{
			this.EffectId = effectid;
			this.Name = name;
			this.Vip0 = vip0;
			this.Vip1 = vip1;
			this.Vip2 = vip2;
			this.Vip3 = vip3;
			this.Vip4 = vip4;
			this.Vip5 = vip5;
			this.Vip6 = vip6;
			this.Vip7 = vip7;
			this.Vip8 = vip8;
			this.Vip9 = vip9;
			this.Vip10 = vip10;
		}
		
		#region Public Properties
		
		///<summary>
		///EffectId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 EffectId {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///Vip0
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Vip0 {get ; set ;}

		///<summary>
		///Vip1
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Vip1 {get ; set ;}

		///<summary>
		///Vip2
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Vip2 {get ; set ;}

		///<summary>
		///Vip3
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Vip3 {get ; set ;}

		///<summary>
		///Vip4
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Vip4 {get ; set ;}

		///<summary>
		///Vip5
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Vip5 {get ; set ;}

		///<summary>
		///Vip6
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Vip6 {get ; set ;}

		///<summary>
		///Vip7
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Vip7 {get ; set ;}

		///<summary>
		///Vip8
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Vip8 {get ; set ;}

		///<summary>
		///Vip9
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 Vip9 {get ; set ;}

		///<summary>
		///Vip10
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Vip10 {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigViplevelEntity Clone()
        {
            ConfigViplevelEntity entity = new ConfigViplevelEntity();
			entity.EffectId = this.EffectId;
			entity.Name = this.Name;
			entity.Vip0 = this.Vip0;
			entity.Vip1 = this.Vip1;
			entity.Vip2 = this.Vip2;
			entity.Vip3 = this.Vip3;
			entity.Vip4 = this.Vip4;
			entity.Vip5 = this.Vip5;
			entity.Vip6 = this.Vip6;
			entity.Vip7 = this.Vip7;
			entity.Vip8 = this.Vip8;
			entity.Vip9 = this.Vip9;
			entity.Vip10 = this.Vip10;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_VipLevel 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigViplevelResponse : BaseResponse<ConfigViplevelEntity>
    {

    }
}

