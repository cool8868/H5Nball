
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Revelation_MyHistory 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class RevelationMyhistoryEntity
	{
		
		public RevelationMyhistoryEntity()
		{
		}

		public RevelationMyhistoryEntity(
		System.Guid managerid
,				System.Byte[] goalsstring
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.GoalsString = goalsstring;
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
		///GoalsString
		///</summary>
        [DataMember]
        [ProtoMember(2)]
        [JsonIgnore]
		public System.Byte[] GoalsString {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public RevelationMyhistoryEntity Clone()
        {
            RevelationMyhistoryEntity entity = new RevelationMyhistoryEntity();
			entity.ManagerId = this.ManagerId;
			entity.GoalsString = this.GoalsString;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Revelation_MyHistory 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class RevelationMyhistoryResponse : BaseResponse<RevelationMyhistoryEntity>
    {

    }
}
