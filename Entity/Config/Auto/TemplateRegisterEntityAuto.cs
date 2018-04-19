
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Template_Register 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TemplateRegisterEntity
	{
		
		public TemplateRegisterEntity()
		{
		}

		public TemplateRegisterEntity(
		System.Int32 idx
,				System.String solutionstring
,				System.Int32 kpi
		)
		{
			this.Idx = idx;
			this.SolutionString = solutionstring;
			this.Kpi = kpi;
		}
		
		#region Public Properties
		
		///<summary>
		///模板id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///阵型字符串
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SolutionString {get ; set ;}

		///<summary>
		///Kpi
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Kpi {get ; set ;}
		#endregion
        
        #region Clone
        public TemplateRegisterEntity Clone()
        {
            TemplateRegisterEntity entity = new TemplateRegisterEntity();
			entity.Idx = this.Idx;
			entity.SolutionString = this.SolutionString;
			entity.Kpi = this.Kpi;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Template_Register 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TemplateRegisterResponse : BaseResponse<TemplateRegisterEntity>
    {

    }
}

