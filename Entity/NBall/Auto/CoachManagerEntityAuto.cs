
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Coach_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CoachManagerEntity
	{
		
		public CoachManagerEntity()
		{
		}

		public CoachManagerEntity(
		System.Guid managerid
,				System.Int32 haveexp
,				System.Byte[] coachstring
,				System.Int32 enablecoachid
,				System.Int32 enablecoachlevel
,				System.Int32 enablecoachstar
,				System.Int32 enablecoachskilllevel
,				System.Decimal offensive
,				System.Decimal organizational
,				System.Decimal defense
,				System.Decimal bodyattr
,				System.Decimal goalkeeping
,				System.Int32 status
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.HaveExp = haveexp;
			this.CoachString = coachstring;
			this.EnableCoachId = enablecoachid;
			this.EnableCoachLevel = enablecoachlevel;
			this.EnableCoachStar = enablecoachstar;
			this.EnableCoachSkillLevel = enablecoachskilllevel;
			this.Offensive = offensive;
			this.Organizational = organizational;
			this.Defense = defense;
			this.BodyAttr = bodyattr;
			this.Goalkeeping = goalkeeping;
			this.Status = status;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///可用教练经验
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 HaveExp {get ; set ;}

		///<summary>
		///CoachString
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.Byte[] CoachString {get ; set ;}

		///<summary>
		///EnableCoachId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 EnableCoachId {get ; set ;}

		///<summary>
		///EnableCoachLevel
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 EnableCoachLevel {get ; set ;}

		///<summary>
		///EnableCoachStar
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 EnableCoachStar {get ; set ;}

		///<summary>
		///EnableCoachSkillLevel
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 EnableCoachSkillLevel {get ; set ;}

		///<summary>
		///Offensive
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Decimal Offensive {get ; set ;}

		///<summary>
		///Organizational
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Decimal Organizational {get ; set ;}

		///<summary>
		///Defense
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Decimal Defense {get ; set ;}

		///<summary>
		///BodyAttr
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Decimal BodyAttr {get ; set ;}

		///<summary>
		///Goalkeeping
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Decimal Goalkeeping {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public CoachManagerEntity Clone()
        {
            CoachManagerEntity entity = new CoachManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.HaveExp = this.HaveExp;
			entity.CoachString = this.CoachString;
			entity.EnableCoachId = this.EnableCoachId;
			entity.EnableCoachLevel = this.EnableCoachLevel;
			entity.EnableCoachStar = this.EnableCoachStar;
			entity.EnableCoachSkillLevel = this.EnableCoachSkillLevel;
			entity.Offensive = this.Offensive;
			entity.Organizational = this.Organizational;
			entity.Defense = this.Defense;
			entity.BodyAttr = this.BodyAttr;
			entity.Goalkeeping = this.Goalkeeping;
			entity.Status = this.Status;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Coach_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CoachManagerResponse : BaseResponse<CoachManagerEntity>
    {

    }
}
