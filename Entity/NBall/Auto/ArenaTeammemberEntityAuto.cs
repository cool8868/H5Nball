
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Arena_Teammember 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ArenaTeammemberEntity
	{
		
		public ArenaTeammemberEntity()
		{
		}

		public ArenaTeammemberEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 arenatype
,				System.Byte[] teammemberstring
,				System.String skillstring
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ArenaType = arenatype;
			this.TeammemberString = teammemberstring;
			this.SkillString = skillstring;
			this.UpdateTime = updatetime;
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
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///竞技场类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ArenaType {get ; set ;}

		///<summary>
		///球员数据
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.Byte[] TeammemberString {get ; set ;}

		///<summary>
		///SkillString
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String SkillString {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ArenaTeammemberEntity Clone()
        {
            ArenaTeammemberEntity entity = new ArenaTeammemberEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ArenaType = this.ArenaType;
			entity.TeammemberString = this.TeammemberString;
			entity.SkillString = this.SkillString;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Arena_Teammember 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ArenaTeammemberResponse : BaseResponse<ArenaTeammemberEntity>
    {

    }
}
