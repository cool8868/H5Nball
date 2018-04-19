
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_Solution 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbSolutionEntity
	{
		
		public NbSolutionEntity()
		{
		}

		public NbSolutionEntity(
		System.Guid managerid
,				System.Int32 formationid
,				System.String playerstring
,				System.String skillstring
,				System.Byte[] formationdata
,				System.Int32 veterancount
,				System.Int32 status
,				System.DateTime rowtime
,				System.Int32 orangecount
,				System.Int32 combcount
,				System.String hireplayerstring
		)
		{
			this.ManagerId = managerid;
			this.FormationId = formationid;
			this.PlayerString = playerstring;
			this.SkillString = skillstring;
			this.FormationData = formationdata;
			this.VeteranCount = veterancount;
			this.Status = status;
			this.RowTime = rowtime;
			this.OrangeCount = orangecount;
			this.CombCount = combcount;
			this.HirePlayerString = hireplayerstring;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///阵型id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 FormationId {get ; set ;}

		///<summary>
		///球员串,11个球员的pid，从守门员开始，以逗号分隔
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String PlayerString {get ; set ;}

		///<summary>
		///技能串，11个位置，从守门员开始，没有填空,以逗号分隔
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String SkillString {get ; set ;}

		///<summary>
		///阵型等级信息
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.Byte[] FormationData {get ; set ;}

		///<summary>
		///元老数量
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 VeteranCount {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///OrangeCount
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 OrangeCount {get ; set ;}

		///<summary>
		///CombCount
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 CombCount {get ; set ;}

		///<summary>
		///HirePlayerString
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String HirePlayerString {get ; set ;}
		#endregion
        
        #region Clone
        public NbSolutionEntity Clone()
        {
            NbSolutionEntity entity = new NbSolutionEntity();
			entity.ManagerId = this.ManagerId;
			entity.FormationId = this.FormationId;
			entity.PlayerString = this.PlayerString;
			entity.SkillString = this.SkillString;
			entity.FormationData = this.FormationData;
			entity.VeteranCount = this.VeteranCount;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.OrangeCount = this.OrangeCount;
			entity.CombCount = this.CombCount;
			entity.HirePlayerString = this.HirePlayerString;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_Solution 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbSolutionResponse : BaseResponse<NbSolutionEntity>
    {

    }
}

