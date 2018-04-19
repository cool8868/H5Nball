
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
    /// ActivityexRecord管理类
    /// </summary>
    public static partial class ActivityexRecordMgr
    {
        
		#region  GetById
		
        public static ActivityexRecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityexRecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerExcitingId
		
        public static ActivityexRecordEntity GetByManagerExcitingId( System.Guid managerId, System.Int32 zoneActivityId, System.Int32 groupId,string zoneId="")
        {
            var provider = new ActivityexRecordProvider(zoneId);
            return provider.GetByManagerExcitingId( managerId, zoneActivityId, groupId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityexRecordEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityexRecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetForSend
		
        public static List<ActivityexRecordEntity> GetForSend( System.Int32 zoneActivityId, System.Int32 groupId,string zoneId="")
        {
            var provider = new ActivityexRecordProvider(zoneId);
            return provider.GetForSend( zoneActivityId, groupId);            
        }
		
		#endregion		  
		
		#region  GetByActivityId
		
        public static List<ActivityexRecordEntity> GetByActivityId( System.Guid managerId, System.Int32 zoneActivityId,string zoneId="")
        {
            var provider = new ActivityexRecordProvider(zoneId);
            return provider.GetByActivityId( managerId, zoneActivityId);            
        }
		
		#endregion		  
		
		#region  GetManagerRank
		
        public static List<ActivityexRecordEntity> GetManagerRank( System.Int32 zoneActivityId, System.Int32 excitingId, System.Int32 groupId, System.DateTime recordDate,string zoneId="")
        {
            var provider = new ActivityexRecordProvider(zoneId);
            return provider.GetManagerRank( zoneActivityId, excitingId, groupId, recordDate);            
        }
		
		#endregion		  
		
		#region  GetCompleteByManager
		
        public static List<ActivityexRecordEntity> GetCompleteByManager( System.Guid managerId,string zoneId="")
        {
            var provider = new ActivityexRecordProvider(zoneId);
            return provider.GetCompleteByManager( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            ActivityexRecordProvider provider = new ActivityexRecordProvider(zoneId);

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  SavePrize
		
        public static bool SavePrize ( System.Int32 idx, System.String exKey, System.Int32 curData, System.Int32 exData, System.Int32 exStep, System.Int32 receiveTimes, System.DateTime recordDate, System.Int32 status, System.DateTime updateTime, System.Byte[] rowVersion, System.Boolean saveHistory,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            ActivityexRecordProvider provider = new ActivityexRecordProvider(zoneId);

            return provider.SavePrize( idx, exKey, curData, exData, exStep, receiveTimes, recordDate, status, updateTime, rowVersion, saveHistory,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityexRecordEntity activityexRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexRecordProvider(zoneId);
            return provider.Insert(activityexRecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityexRecordEntity activityexRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexRecordProvider(zoneId);
            return provider.Update(activityexRecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

