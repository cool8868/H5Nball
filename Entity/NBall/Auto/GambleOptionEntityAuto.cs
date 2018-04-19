
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Gamble_Option 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class GambleOptionEntity
	{
		
		public GambleOptionEntity()
		{
		}

		public GambleOptionEntity(
		System.Guid idx
,				System.Guid titleid
,				System.String optioncontent
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.TitleId = titleid;
			this.OptionContent = optioncontent;
			this.Status = status;
			this.Rowtime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///TitleId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid TitleId {get ; set ;}

		///<summary>
		///选项
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String OptionContent {get ; set ;}

		///<summary>
		///状态（1表示该选项获胜）
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///创建时间
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime Rowtime {get ; set ;}
		#endregion
        
        #region Clone
        public GambleOptionEntity Clone()
        {
            GambleOptionEntity entity = new GambleOptionEntity();
			entity.Idx = this.Idx;
			entity.TitleId = this.TitleId;
			entity.OptionContent = this.OptionContent;
			entity.Status = this.Status;
			entity.Rowtime = this.Rowtime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Gamble_Option 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class GambleOptionResponse : BaseResponse<GambleOptionEntity>
    {

    }
}
