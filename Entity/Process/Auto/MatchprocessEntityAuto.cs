
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.MatchProcess 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class MatchprocessEntity
	{
		
		public MatchprocessEntity()
		{
		}

		public MatchprocessEntity(
		System.Guid idx
,				System.Byte[] process
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.Process = process;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///Process
		///</summary>
        [DataMember]
        [ProtoMember(2)]
        [JsonIgnore]
		public System.Byte[] Process {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public MatchprocessEntity Clone()
        {
            MatchprocessEntity entity = new MatchprocessEntity();
			entity.Idx = this.Idx;
			entity.Process = this.Process;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.MatchProcess 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class MatchprocessResponse : BaseResponse<MatchprocessEntity>
    {

    }
}

