
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Gamble_RankRewardLog 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GambleRankrewardlogEntity
	{
		
		public GambleRankrewardlogEntity()
		{
		}

		public GambleRankrewardlogEntity(
		System.Int32 idx
,				System.Int32 status
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.Status = status;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///0没做任何处理，1发奖中，2发奖完成
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public GambleRankrewardlogEntity Clone()
        {
            GambleRankrewardlogEntity entity = new GambleRankrewardlogEntity();
			entity.Idx = this.Idx;
			entity.Status = this.Status;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Gamble_RankRewardLog 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GambleRankrewardlogResponse : BaseResponse<GambleRankrewardlogEntity>
    {

    }
}
