
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
    /// ActivityRecord管理类
    /// </summary>
    public static partial class ActivityRecordMgr
    {
        
		#region  GetById
		
        public static ActivityRecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityRecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerActivityId
		
        public static ActivityRecordEntity GetByManagerActivityId( System.Guid managerId, System.Int32 activityId,string zoneId="")
        {
            var provider = new ActivityRecordProvider(zoneId);
            return provider.GetByManagerActivityId( managerId, activityId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityRecordEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityRecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetCompleteByManager
		
        public static List<ActivityRecordEntity> GetCompleteByManager( System.Guid managerId,string zoneId="")
        {
            var provider = new ActivityRecordProvider(zoneId);
            return provider.GetCompleteByManager( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            ActivityRecordProvider provider = new ActivityRecordProvider(zoneId);

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  SavePrize
		
        public static bool SavePrize ( System.Int32 idx, System.Guid managerId, System.Int32 activityId, System.Int32 activityStep, System.String stepRecord, System.DateTime recordDate, System.DateTime settlementDate, System.Int32 status, System.DateTime updateTime, System.Byte[] rowVersion, System.String activityKey,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            ActivityRecordProvider provider = new ActivityRecordProvider(zoneId);

            return provider.SavePrize( idx, managerId, activityId, activityStep, stepRecord, recordDate, settlementDate, status, updateTime, rowVersion, activityKey,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityRecordEntity activityRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityRecordProvider(zoneId);
            return provider.Insert(activityRecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityRecordEntity activityRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityRecordProvider(zoneId);
            return provider.Update(activityRecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

