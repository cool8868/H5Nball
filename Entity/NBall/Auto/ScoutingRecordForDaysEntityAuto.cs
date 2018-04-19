
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Scouting_RecordForDays 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ScoutingRecordfordaysEntity
	{
		
		public ScoutingRecordfordaysEntity()
		{
		}

		public ScoutingRecordfordaysEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 carditemcodethen89
,				System.DateTime rowtime
,				System.Int32 scoutingtype
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.CardItemCodeThen89 = carditemcodethen89;
			this.RowTime = rowtime;
			this.ScoutingType = scoutingtype;
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
		///大于89的球员卡itemCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 CardItemCodeThen89 {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///ScoutingType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ScoutingType {get ; set ;}
		#endregion
        
        #region Clone
        public ScoutingRecordfordaysEntity Clone()
        {
            ScoutingRecordfordaysEntity entity = new ScoutingRecordfordaysEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.CardItemCodeThen89 = this.CardItemCodeThen89;
			entity.RowTime = this.RowTime;
			entity.ScoutingType = this.ScoutingType;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Scouting_RecordForDays 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ScoutingRecordfordaysResponse : BaseResponse<ScoutingRecordfordaysEntity>
    {

    }
}
