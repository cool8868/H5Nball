
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Scouting_Record 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ScoutingRecordEntity
	{
		
		public ScoutingRecordEntity()
		{
		}

		public ScoutingRecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 scoutingtype
,				System.Int32 itemcode
,				System.String itemstring
,				System.Int32 strength
,				System.Boolean isbinding
,				System.DateTime rowtime
,				System.Int32 status
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ScoutingType = scoutingtype;
			this.ItemCode = itemcode;
			this.ItemString = itemstring;
			this.Strength = strength;
			this.IsBinding = isbinding;
			this.RowTime = rowtime;
			this.Status = status;
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
		///球探类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ScoutingType {get ; set ;}

		///<summary>
		///获得的物品code
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///物品串
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String ItemString {get ; set ;}

		///<summary>
		///强化等级
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Strength {get ; set ;}

		///<summary>
		///是否绑定
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean IsBinding {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(8)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///状态:0,初始;1,已领
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Status {get ; set ;}
		#endregion
        
        #region Clone
        public ScoutingRecordEntity Clone()
        {
            ScoutingRecordEntity entity = new ScoutingRecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ScoutingType = this.ScoutingType;
			entity.ItemCode = this.ItemCode;
			entity.ItemString = this.ItemString;
			entity.Strength = this.Strength;
			entity.IsBinding = this.IsBinding;
			entity.RowTime = this.RowTime;
			entity.Status = this.Status;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Scouting_Record 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ScoutingRecordResponse : BaseResponse<ScoutingRecordEntity>
    {

    }
}

