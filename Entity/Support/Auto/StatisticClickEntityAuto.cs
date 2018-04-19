
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Statistic_Click 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class StatisticClickEntity
	{
		
		public StatisticClickEntity()
		{
		}

		public StatisticClickEntity(
		System.Int64 idx
,				System.Int32 type
,				System.DateTime recorddate
,				System.Int32 count
		)
		{
			this.Idx = idx;
			this.Type = type;
			this.RecordDate = recorddate;
			this.Count = count;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int64 Idx {get ; set ;}

		///<summary>
		///Type
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Type {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///Count
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Count {get ; set ;}
		#endregion
        
        #region Clone
        public StatisticClickEntity Clone()
        {
            StatisticClickEntity entity = new StatisticClickEntity();
			entity.Idx = this.Idx;
			entity.Type = this.Type;
			entity.RecordDate = this.RecordDate;
			entity.Count = this.Count;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Statistic_Click 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class StatisticClickResponse : BaseResponse<StatisticClickEntity>
    {

    }
}

