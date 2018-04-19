
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
    /// TaskDailyhistory管理类
    /// </summary>
    public static partial class TaskDailyhistoryMgr
    {
        
		#region  GetById
		
        public static TaskDailyhistoryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new TaskDailyhistoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TaskDailyhistoryEntity> GetAll(string zoneId="")
        {
            var provider = new TaskDailyhistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            TaskDailyhistoryProvider provider = new TaskDailyhistoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TaskDailyhistoryEntity taskDailyhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TaskDailyhistoryProvider(zoneId);
            return provider.Insert(taskDailyhistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TaskDailyhistoryEntity taskDailyhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TaskDailyhistoryProvider(zoneId);
            return provider.Update(taskDailyhistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

