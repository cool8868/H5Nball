
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Mail 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicMailEntity
	{
		
		public DicMailEntity()
		{
		}

		public DicMailEntity(
		System.Int32 idx
,				System.Int32 category
,				System.Int32 sourcetype
,				System.String title
,				System.String contenttemplate
,				System.String contentparam
,				System.Boolean hasattach
		)
		{
			this.Idx = idx;
			this.Category = category;
			this.SourceType = sourcetype;
			this.Title = title;
			this.ContentTemplate = contenttemplate;
			this.ContentParam = contentparam;
			this.HasAttach = hasattach;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///邮件类别
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Category {get ; set ;}

		///<summary>
		///来源类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SourceType {get ; set ;}

		///<summary>
		///标题
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Title {get ; set ;}

		///<summary>
		///内容模板
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String ContentTemplate {get ; set ;}

		///<summary>
		///内容参数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String ContentParam {get ; set ;}

		///<summary>
		///是否有附件
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Boolean HasAttach {get ; set ;}
		#endregion
        
        #region Clone
        public DicMailEntity Clone()
        {
            DicMailEntity entity = new DicMailEntity();
			entity.Idx = this.Idx;
			entity.Category = this.Category;
			entity.SourceType = this.SourceType;
			entity.Title = this.Title;
			entity.ContentTemplate = this.ContentTemplate;
			entity.ContentParam = this.ContentParam;
			entity.HasAttach = this.HasAttach;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Mail 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicMailResponse : BaseResponse<DicMailEntity>
    {

    }
}

