
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerSkillAsk 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagerskillaskEntity
	{
		
		public NbManagerskillaskEntity()
		{
		}

		public NbManagerskillaskEntity(
		System.Guid managerid
,				System.Int32 ask1
,				System.Int32 ask2
,				System.Int32 ask3
,				System.Int32 ask4
,				System.Int32 ask5
,				System.String askx
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.ManagerId = managerid;
			this.Ask1 = ask1;
			this.Ask2 = ask2;
			this.Ask3 = ask3;
			this.Ask4 = ask4;
			this.Ask5 = ask5;
			this.AskX = askx;
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
		///Ask1
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Ask1 {get ; set ;}

		///<summary>
		///Ask2
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Ask2 {get ; set ;}

		///<summary>
		///Ask3
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Ask3 {get ; set ;}

		///<summary>
		///Ask4
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Ask4 {get ; set ;}

		///<summary>
		///Ask5
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Ask5 {get ; set ;}

		///<summary>
		///AskX
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String AskX {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(9)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagerskillaskEntity Clone()
        {
            NbManagerskillaskEntity entity = new NbManagerskillaskEntity();
			entity.ManagerId = this.ManagerId;
			entity.Ask1 = this.Ask1;
			entity.Ask2 = this.Ask2;
			entity.Ask3 = this.Ask3;
			entity.Ask4 = this.Ask4;
			entity.Ask5 = this.Ask5;
			entity.AskX = this.AskX;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerSkillAsk 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagerskillaskResponse : BaseResponse<NbManagerskillaskEntity>
    {

    }
}

