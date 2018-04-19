
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_TurntableReset 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigTurntableresetEntity
	{
		
		public ConfigTurntableresetEntity()
		{
		}

		public ConfigTurntableresetEntity(
		System.Int32 idx
,				System.Int32 turntabletype
,				System.Int32 resetnumber
,				System.Int32 point
		)
		{
			this.Idx = idx;
			this.TurntableType = turntabletype;
			this.ResetNumber = resetnumber;
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
		///TurntableType
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 TurntableType {get ; set ;}

		///<summary>
		///ResetNumber
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ResetNumber {get ; set ;}

		///<summary>
		///Point
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Point {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigTurntableresetEntity Clone()
        {
            ConfigTurntableresetEntity entity = new ConfigTurntableresetEntity();
			entity.Idx = this.Idx;
			entity.TurntableType = this.TurntableType;
			entity.ResetNumber = this.ResetNumber;
			entity.Point = this.Point;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_TurntableReset 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigTurntableresetResponse : BaseResponse<ConfigTurntableresetEntity>
    {

    }
}
