
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Template_ActivityEx 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TemplateActivityexEntity
	{
		
		public TemplateActivityexEntity()
		{
		}

		public TemplateActivityexEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 imageid
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.ImageId = imageid;
			this.Status = status;
			this.RowTime = rowtime;
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
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///ImageId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ImageId {get ; set ;}

		///<summary>
		/// 活动状态：0：正常，1：已结束
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public TemplateActivityexEntity Clone()
        {
            TemplateActivityexEntity entity = new TemplateActivityexEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.ImageId = this.ImageId;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Template_ActivityEx 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TemplateActivityexResponse : BaseResponse<TemplateActivityexEntity>
    {

    }
}

