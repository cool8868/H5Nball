
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_Scouting 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigScoutingEntity
	{
		
		public ConfigScoutingEntity()
		{
		}

		public ConfigScoutingEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 type
,				System.Int32 mallcode
,				System.Boolean hasten
,				System.Int32 orangelib
,				System.Int32 lowlib
,				System.String description
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.Type = type;
			this.MallCode = mallcode;
			this.HasTen = hasten;
			this.OrangeLib = orangelib;
			this.LowLib = lowlib;
			this.Description = description;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///球探名称
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///商品编码
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 MallCode {get ; set ;}

		///<summary>
		///HasTen
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Boolean HasTen {get ; set ;}

		///<summary>
		///橙卡包卡库
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 OrangeLib {get ; set ;}

		///<summary>
		///LowLib
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 LowLib {get ; set ;}

		///<summary>
		///描述
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigScoutingEntity Clone()
        {
            ConfigScoutingEntity entity = new ConfigScoutingEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.Type = this.Type;
			entity.MallCode = this.MallCode;
			entity.HasTen = this.HasTen;
			entity.OrangeLib = this.OrangeLib;
			entity.LowLib = this.LowLib;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_Scouting 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigScoutingResponse : BaseResponse<ConfigScoutingEntity>
    {

    }
}

