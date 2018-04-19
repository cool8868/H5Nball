
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
    /// TaskDailyrecord管理类
    /// </summary>
    public static partial class TaskDailyrecordMgr
    {
        
		#region  GetById
		
        public static TaskDailyrecordEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new TaskDailyrecordProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TaskDailyrecordEntity> GetAll(string zoneId="")
        {
            var provider = new TaskDailyrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Submit
		
        public static bool Submit ( System.Guid managerId, System.Int32 newTaskId, System.String stepRecord, System.Int32 curTimes, System.String allStepRecords, System.String allCurTimes, System.String allStatus, System.Int32 prizeExp, System.Int32 prizeCoin, System.Int32 prizeItemCode, System.Int32 finishCount,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TaskDailyrecordProvider provider = new TaskDailyrecordProvider(zoneId);

            return provider.Submit( managerId, newTaskId, stepRecord, curTimes, allStepRecords, allCurTimes, allStatus, prizeExp, prizeCoin, prizeItemCode, finishCount,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  Giveup
		
        public static bool Giveup ( System.Guid managerId, System.Int32 newTaskId,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TaskDailyrecordProvider provider = new TaskDailyrecordProvider(zoneId);

            return provider.Giveup( managerId, newTaskId,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  FinishPrize
		
        public static bool FinishPrize ( System.Guid managerId, System.Int32 prizeExp, System.Int32 prizeCoin, System.Int32 prizeItemCode, System.Int32 prizePoint, System.DateTime recordDate, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TaskDailyrecordProvider provider = new TaskDailyrecordProvider(zoneId);

            return provider.FinishPrize( managerId, prizeExp, prizeCoin, prizeItemCode, prizePoint, recordDate, rowVersion,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TaskDailyrecordEntity taskDailyrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TaskDailyrecordProvider(zoneId);
            return provider.Insert(taskDailyrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TaskDailyrecordEntity taskDailyrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TaskDailyrecordProvider(zoneId);
            return provider.Update(taskDailyrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

