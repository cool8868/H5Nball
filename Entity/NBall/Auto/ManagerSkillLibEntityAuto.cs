
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ManagerSkill_Lib 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ManagerskillLibEntity
	{
		
		public ManagerskillLibEntity()
		{
		}

		public ManagerskillLibEntity(
		System.Guid managerid
,				System.Int32 synctalentpoint
,				System.Int32 maxtalentpoint
,				System.Int32 maxwillnumber
,				System.String todotalents
,				System.String nodotalents
,				System.String todowills
,				System.String nodowills
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.ManagerId = managerid;
			this.SyncTalentPoint = synctalentpoint;
			this.MaxTalentPoint = maxtalentpoint;
			this.MaxWillNumber = maxwillnumber;
			this.TodoTalents = todotalents;
			this.NodoTalents = nodotalents;
			this.TodoWills = todowills;
			this.NodoWills = nodowills;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///SyncTalentPoint
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SyncTalentPoint {get ; set ;}

		///<summary>
		///技能点
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 MaxTalentPoint {get ; set ;}

		///<summary>
		///意志数
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 MaxWillNumber {get ; set ;}

		///<summary>
		///主动天赋
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String TodoTalents {get ; set ;}

		///<summary>
		///被动天赋
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String NodoTalents {get ; set ;}

		///<summary>
		///主动意志
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String TodoWills {get ; set ;}

		///<summary>
		///被动意志
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String NodoWills {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public ManagerskillLibEntity Clone()
        {
            ManagerskillLibEntity entity = new ManagerskillLibEntity();
			entity.ManagerId = this.ManagerId;
			entity.SyncTalentPoint = this.SyncTalentPoint;
			entity.MaxTalentPoint = this.MaxTalentPoint;
			entity.MaxWillNumber = this.MaxWillNumber;
			entity.TodoTalents = this.TodoTalents;
			entity.NodoTalents = this.NodoTalents;
			entity.TodoWills = this.TodoWills;
			entity.NodoWills = this.NodoWills;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ManagerSkill_Lib 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ManagerskillLibResponse : BaseResponse<ManagerskillLibEntity>
    {

    }
}

