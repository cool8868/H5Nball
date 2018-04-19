
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_SkillBuffMatch 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicSkillbuffmatchEntity
	{
		
		public DicSkillbuffmatchEntity()
		{
		}

		public DicSkillbuffmatchEntity(
		System.Int32 idx
,				System.Int32 type
,				System.String linkid
,				System.String linktype
,				System.String buffengineid
		)
		{
			this.Idx = idx;
			this.Type = type;
			this.LinkId = linkid;
			this.LinkType = linktype;
			this.BuffEngineId = buffengineid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///Type
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///LinkId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String LinkId {get ; set ;}

		///<summary>
		///LinkType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String LinkType {get ; set ;}

		///<summary>
		///BuffEngineId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String BuffEngineId {get ; set ;}
		#endregion
        
        #region Clone
        public DicSkillbuffmatchEntity Clone()
        {
            DicSkillbuffmatchEntity entity = new DicSkillbuffmatchEntity();
			entity.Idx = this.Idx;
			entity.Type = this.Type;
			entity.LinkId = this.LinkId;
			entity.LinkType = this.LinkType;
			entity.BuffEngineId = this.BuffEngineId;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_SkillBuffMatch 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicSkillbuffmatchResponse : BaseResponse<DicSkillbuffmatchEntity>
    {

    }
}

