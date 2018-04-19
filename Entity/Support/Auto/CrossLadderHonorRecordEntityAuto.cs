
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossLadder_HonorRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrossladderHonorrecordEntity
	{
		
		public CrossladderHonorrecordEntity()
		{
		}

		public CrossladderHonorrecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 honor
,				System.Int32 curhonor
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.Honor = honor;
			this.CurHonor = curhonor;
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
		///Honor
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Honor {get ; set ;}

		///<summary>
		///CurHonor
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CurHonor {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrossladderHonorrecordEntity Clone()
        {
            CrossladderHonorrecordEntity entity = new CrossladderHonorrecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.Honor = this.Honor;
			entity.CurHonor = this.CurHonor;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossLadder_HonorRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrossladderHonorrecordResponse : BaseResponse<CrossladderHonorrecordEntity>
    {

    }
}
