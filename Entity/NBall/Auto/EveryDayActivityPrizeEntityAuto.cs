
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.EveryDayActivityPrize 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class EverydayactivityprizeEntity
	{
		
		public EverydayactivityprizeEntity()
		{
		}

		public EverydayactivityprizeEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 activityid
,				System.Int32 activitystep
,				System.Int32 subtype
,				System.Int32 itemcode
,				System.DateTime rowdate
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ActivityId = activityid;
			this.ActivityStep = activitystep;
			this.SubType = subtype;
			this.ItemCode = itemcode;
			this.RowDate = rowdate;
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
		///ActivityId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ActivityId {get ; set ;}

		///<summary>
		///ActivityStep
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ActivityStep {get ; set ;}

		///<summary>
		///SubType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 SubType {get ; set ;}

		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///RowDate
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowDate {get ; set ;}
		#endregion
        
        #region Clone
        public EverydayactivityprizeEntity Clone()
        {
            EverydayactivityprizeEntity entity = new EverydayactivityprizeEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ActivityId = this.ActivityId;
			entity.ActivityStep = this.ActivityStep;
			entity.SubType = this.SubType;
			entity.ItemCode = this.ItemCode;
			entity.RowDate = this.RowDate;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.EveryDayActivityPrize 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class EverydayactivityprizeResponse : BaseResponse<EverydayactivityprizeEntity>
    {

    }
}

