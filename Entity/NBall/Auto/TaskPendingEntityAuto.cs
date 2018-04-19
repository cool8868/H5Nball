
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Task_Pending 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TaskPendingEntity
	{
		
		public TaskPendingEntity()
		{
		}

		public TaskPendingEntity(
		System.Guid managerid
,				System.String taskstring
		)
		{
			this.ManagerId = managerid;
			this.TaskString = taskstring;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///TaskString
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String TaskString {get ; set ;}
		#endregion
        
        #region Clone
        public TaskPendingEntity Clone()
        {
            TaskPendingEntity entity = new TaskPendingEntity();
			entity.ManagerId = this.ManagerId;
			entity.TaskString = this.TaskString;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Task_Pending 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TaskPendingResponse : BaseResponse<TaskPendingEntity>
    {

    }
}

