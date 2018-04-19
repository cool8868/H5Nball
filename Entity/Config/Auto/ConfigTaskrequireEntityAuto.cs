
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_TaskRequire 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigTaskrequireEntity
	{
		
		public ConfigTaskrequireEntity()
		{
		}

		public ConfigTaskrequireEntity(
		System.Int32 idx
,				System.Int32 taskid
,				System.Int32 requiretype
,				System.Int32 requiresub
,				System.Int32 requirethird
,				System.Int32 overstate
		)
		{
			this.Idx = idx;
			this.TaskId = taskid;
			this.RequireType = requiretype;
			this.RequireSub = requiresub;
			this.RequireThird = requirethird;
			this.OverState = overstate;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///任务id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 TaskId {get ; set ;}

		///<summary>
		///需求类型
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 RequireType {get ; set ;}

		///<summary>
		///需求二级
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 RequireSub {get ; set ;}

		///<summary>
		///需求三级
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 RequireThird {get ; set ;}

		///<summary>
		///OverState
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 OverState {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigTaskrequireEntity Clone()
        {
            ConfigTaskrequireEntity entity = new ConfigTaskrequireEntity();
			entity.Idx = this.Idx;
			entity.TaskId = this.TaskId;
			entity.RequireType = this.RequireType;
			entity.RequireSub = this.RequireSub;
			entity.RequireThird = this.RequireThird;
			entity.OverState = this.OverState;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_TaskRequire 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigTaskrequireResponse : BaseResponse<ConfigTaskrequireEntity>
    {

    }
}

