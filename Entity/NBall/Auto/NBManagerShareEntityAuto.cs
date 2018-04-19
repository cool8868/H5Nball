
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerShare 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagershareEntity
	{
		
		public NbManagershareEntity()
		{
		}

		public NbManagershareEntity(
		System.Guid managerid
,				System.DateTime intime
,				System.DateTime outtime
,				System.Int32 input
,				System.Int32 output
		)
		{
			this.ManagerId = managerid;
			this.InTime = intime;
			this.OutTime = outtime;
			this.InPut = input;
			this.OutPut = output;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///InTime
		///</summary>
        [DataMember]
        [ProtoMember(2)]
        [JsonIgnore]
		public System.DateTime InTime {get ; set ;}

		///<summary>
		///OutTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime OutTime {get ; set ;}

		///<summary>
		///InPut
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 InPut {get ; set ;}

		///<summary>
		///OutPut
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 OutPut {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagershareEntity Clone()
        {
            NbManagershareEntity entity = new NbManagershareEntity();
			entity.ManagerId = this.ManagerId;
			entity.InTime = this.InTime;
			entity.OutTime = this.OutTime;
			entity.InPut = this.InPut;
			entity.OutPut = this.OutPut;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerShare 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagershareResponse : BaseResponse<NbManagershareEntity>
    {

    }
}
