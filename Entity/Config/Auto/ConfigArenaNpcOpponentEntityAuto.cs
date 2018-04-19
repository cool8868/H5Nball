
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_ArenaNpcOpponent 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigArenanpcopponentEntity
	{
		
		public ConfigArenanpcopponentEntity()
		{
		}

		public ConfigArenanpcopponentEntity(
		System.Int32 idx
,				System.Int32 opponent
,				System.Int32 groupid
		)
		{
			this.Idx = idx;
			this.Opponent = opponent;
			this.GroupId = groupid;
		}
		
		#region Public Properties
		
		///<summary>
		///段位
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///对手序号
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Opponent {get ; set ;}

		///<summary>
		///分组ID
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 GroupId {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigArenanpcopponentEntity Clone()
        {
            ConfigArenanpcopponentEntity entity = new ConfigArenanpcopponentEntity();
			entity.Idx = this.Idx;
			entity.Opponent = this.Opponent;
			entity.GroupId = this.GroupId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_ArenaNpcOpponent 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigArenanpcopponentResponse : BaseResponse<ConfigArenanpcopponentEntity>
    {

    }
}
