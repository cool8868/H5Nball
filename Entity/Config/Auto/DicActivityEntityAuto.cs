
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Activity 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicActivityEntity
	{
		
		public DicActivityEntity()
		{
		}

		public DicActivityEntity(
		System.Int32 idx
,				System.Int32 activitytype
,				System.String name
,				System.DateTime startdate
,				System.DateTime enddate
,				System.Boolean isvalid
		)
		{
			this.Idx = idx;
			this.ActivityType = activitytype;
			this.Name = name;
			this.Startdate = startdate;
			this.EndDate = enddate;
			this.IsValid = isvalid;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ActivityType
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 ActivityType {get ; set ;}

		///<summary>
		///活动名称
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///Startdate
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime Startdate {get ; set ;}

		///<summary>
		///EndDate
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime EndDate {get ; set ;}

		///<summary>
		///IsValid
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Boolean IsValid {get ; set ;}
		#endregion
        
        #region Clone
        public DicActivityEntity Clone()
        {
            DicActivityEntity entity = new DicActivityEntity();
			entity.Idx = this.Idx;
			entity.ActivityType = this.ActivityType;
			entity.Name = this.Name;
			entity.Startdate = this.Startdate;
			entity.EndDate = this.EndDate;
			entity.IsValid = this.IsValid;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Activity 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicActivityResponse : BaseResponse<DicActivityEntity>
    {

    }
}

