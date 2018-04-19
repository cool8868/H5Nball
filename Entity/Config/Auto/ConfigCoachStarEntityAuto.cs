
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CoachStar 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCoachstarEntity
	{
		
		public ConfigCoachstarEntity()
		{
		}

		public ConfigCoachstarEntity(
		System.Int32 idx
,				System.Int32 starlevel
,				System.Int32 coachid
,				System.Int32 consumedebris
,				System.Int32 maxlevel
		)
		{
			this.Idx = idx;
			this.StarLevel = starlevel;
			this.CoachId = coachid;
			this.ConsumeDebris = consumedebris;
			this.MaxLevel = maxlevel;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///StarLevel
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 StarLevel {get ; set ;}

		///<summary>
		///CoachId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 CoachId {get ; set ;}

		///<summary>
		///ConsumeDebris
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ConsumeDebris {get ; set ;}

		///<summary>
		///MaxLevel
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 MaxLevel {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCoachstarEntity Clone()
        {
            ConfigCoachstarEntity entity = new ConfigCoachstarEntity();
			entity.Idx = this.Idx;
			entity.StarLevel = this.StarLevel;
			entity.CoachId = this.CoachId;
			entity.ConsumeDebris = this.ConsumeDebris;
			entity.MaxLevel = this.MaxLevel;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CoachStar 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCoachstarResponse : BaseResponse<ConfigCoachstarEntity>
    {

    }
}
