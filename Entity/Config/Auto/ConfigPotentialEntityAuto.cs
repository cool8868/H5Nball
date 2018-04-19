
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Potential 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigPotentialEntity
	{
		
		public ConfigPotentialEntity()
		{
		}

		public ConfigPotentialEntity(
		System.Int32 idx
,				System.Int32 potentialid
,				System.String name
,				System.Int32 level
,				System.Decimal minbuff
,				System.Decimal maxbuff
,				System.Int32 bufftype
,				System.Int32 gkgettype
,				System.Int32 buffid
		)
		{
			this.Idx = idx;
			this.PotentialId = potentialid;
			this.Name = name;
			this.Level = level;
			this.MinBuff = minbuff;
			this.MaxBuff = maxbuff;
			this.BuffType = bufftype;
			this.GKGetType = gkgettype;
			this.BuffId = buffid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///潜力ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PotentialId {get ; set ;}

		///<summary>
		///潜力名称
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///潜力等级 1低级 2中级  3高级 
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Level {get ; set ;}

		///<summary>
		///Buff范围值
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Decimal MinBuff {get ; set ;}

		///<summary>
		///buff值
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Decimal MaxBuff {get ; set ;}

		///<summary>
		///BUff类型  1=值 2=百分比
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 BuffType {get ; set ;}

		///<summary>
		///守门员获得此属性类型  1所有可活动  2守门员不可得  3只限守门员获得
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 GKGetType {get ; set ;}

		///<summary>
		///BuffId
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 BuffId {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigPotentialEntity Clone()
        {
            ConfigPotentialEntity entity = new ConfigPotentialEntity();
			entity.Idx = this.Idx;
			entity.PotentialId = this.PotentialId;
			entity.Name = this.Name;
			entity.Level = this.Level;
			entity.MinBuff = this.MinBuff;
			entity.MaxBuff = this.MaxBuff;
			entity.BuffType = this.BuffType;
			entity.GKGetType = this.GKGetType;
			entity.BuffId = this.BuffId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Potential 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigPotentialResponse : BaseResponse<ConfigPotentialEntity>
    {

    }
}
