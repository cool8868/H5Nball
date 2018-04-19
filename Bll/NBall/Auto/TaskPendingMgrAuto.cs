
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    /// <summary>
    /// TaskPending管理类
    /// </summary>
    public static partial class TaskPendingMgr
    {
        
		#region  GetById
		
        public static TaskPendingEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new TaskPendingProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TaskPendingEntity> GetAll(string zoneId="")
        {
            var provider = new TaskPendingProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            TaskPendingProvider provider = new TaskPendingProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TaskPendingEntity taskPendingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TaskPendingProvider(zoneId);
            return provider.Insert(taskPendingEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TaskPendingEntity taskPendingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TaskPendingProvider(zoneId);
            return provider.Update(taskPendingEntity,trans);
        }
		
		#endregion	
		
		
	}
}

