
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Template_RegisterPlayer 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TemplateRegisterplayerEntity
	{
		
		public TemplateRegisterplayerEntity()
		{
		}

		public TemplateRegisterplayerEntity(
		System.Int32 idx
,				System.Int32 templateid
,				System.Int32 playerid
,				System.Int32 position
		)
		{
			this.Idx = idx;
			this.TemplateId = templateid;
			this.PlayerId = playerid;
			this.Position = position;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///模板id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 TemplateId {get ; set ;}

		///<summary>
		///球员pid
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PlayerId {get ; set ;}

		///<summary>
		///球员位置
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Position {get ; set ;}
		#endregion
        
        #region Clone
        public TemplateRegisterplayerEntity Clone()
        {
            TemplateRegisterplayerEntity entity = new TemplateRegisterplayerEntity();
			entity.Idx = this.Idx;
			entity.TemplateId = this.TemplateId;
			entity.PlayerId = this.PlayerId;
			entity.Position = this.Position;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Template_RegisterPlayer 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TemplateRegisterplayerResponse : BaseResponse<TemplateRegisterplayerEntity>
    {

    }
}

