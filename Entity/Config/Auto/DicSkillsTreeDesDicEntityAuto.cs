
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_SkillsTreeDesDic 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicSkillstreedesdicEntity
	{
		
		public DicSkillstreedesdicEntity()
		{
		}

		public DicSkillstreedesdicEntity(
		System.Int32 idx
,				System.String skillcode
,				System.Int32 skilllevel
,				System.String description
,				System.Int32 duration
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.SkillCode = skillcode;
			this.SkillLevel = skilllevel;
			this.Description = description;
			this.Duration = duration;
			this.Status = status;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///SkillCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SkillCode {get ; set ;}

		///<summary>
		///SkillLevel
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Description {get ; set ;}

		///<summary>
		///Duration
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Duration {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicSkillstreedesdicEntity Clone()
        {
            DicSkillstreedesdicEntity entity = new DicSkillstreedesdicEntity();
			entity.Idx = this.Idx;
			entity.SkillCode = this.SkillCode;
			entity.SkillLevel = this.SkillLevel;
			entity.Description = this.Description;
			entity.Duration = this.Duration;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_SkillsTreeDesDic 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicSkillstreedesdicResponse : BaseResponse<DicSkillstreedesdicEntity>
    {

    }
}

