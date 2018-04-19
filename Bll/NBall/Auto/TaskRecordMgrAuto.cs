
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
    /// TaskRecord管理类
    /// </summary>
    public static partial class TaskRecordMgr
    {
        
		#region  GetById
		
        public static TaskRecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new TaskRecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TaskRecordEntity> GetAll(string zoneId="")
        {
            var provider = new TaskRecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManager
		
        public static List<TaskRecordEntity> GetByManager( System.Guid managerId,string zoneId="")
        {
            var provider = new TaskRecordProvider(zoneId);
            return provider.GetByManager( managerId);            
        }
		
		#endregion		  
		
		#region  GetPending
		
        public static List<TaskRecordEntity> GetPending( System.Guid managerId, System.Int32 level,string zoneId="")
        {
            var provider = new TaskRecordProvider(zoneId);
            return provider.GetPending( managerId, level);            
        }
		
		#endregion		  
		
		#region  GetForHandler
		
        public static List<TaskRecordEntity> GetForHandler( System.Guid managerId,string zoneId="")
        {
            var provider = new TaskRecordProvider(zoneId);
            return provider.GetForHandler( managerId);            
        }
		
		#endregion		  
		
		#region  GetManagerTaskList
		
        public static List<TaskRecordEntity> GetManagerTaskList( System.Guid managerId,string zoneId="")
        {
            var provider = new TaskRecordProvider(zoneId);
            return provider.GetManagerTaskList( managerId);            
        }
		
		#endregion		  
		
		#region  Submit
		
        public static bool Submit ( System.Int32 idx, System.Int32 prizeExp, System.Int32 prizeCoin, System.Int32 prizeItemCode,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TaskRecordProvider provider = new TaskRecordProvider(zoneId);

            return provider.Submit( idx, prizeExp, prizeCoin, prizeItemCode,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  Add
		
        public static bool Add ( System.Guid managerId, System.Int32 taskId, System.Int32 curTimes, System.String stepRecord, System.Byte[] doneParam, System.Int32 managerLevel, System.Int32 status, System.DateTime rowTime, System.DateTime updateTime,ref  System.Int32 idx,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TaskRecordProvider provider = new TaskRecordProvider(zoneId);

            return provider.Add( managerId, taskId, curTimes, stepRecord, doneParam, managerLevel, status, rowTime, updateTime,ref  idx,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TaskRecordEntity taskRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TaskRecordProvider(zoneId);
            return provider.Insert(taskRecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TaskRecordEntity taskRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TaskRecordProvider(zoneId);
            return provider.Update(taskRecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

