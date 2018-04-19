
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
    /// ActivityexItemrecord管理类
    /// </summary>
    public static partial class ActivityexItemrecordMgr
    {
        
		#region  GetById
		
        public static ActivityexItemrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityexItemrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerExcitingId
		
        public static ActivityexItemrecordEntity GetByManagerExcitingId( System.Guid managerId, System.Int32 zoneActivityId, System.Int32 groupId,string zoneId="")
        {
            var provider = new ActivityexItemrecordProvider(zoneId);
            return provider.GetByManagerExcitingId( managerId, zoneActivityId, groupId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityexItemrecordEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityexItemrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManagerList
		
        public static List<ActivityexItemrecordEntity> GetByManagerList( System.Guid managerId, System.Int32 zoneActivityId,string zoneId="")
        {
            var provider = new ActivityexItemrecordProvider(zoneId);
            return provider.GetByManagerList( managerId, zoneActivityId);            
        }
		
		#endregion		  
		
		#region  GetForSend
		
        public static List<ActivityexItemrecordEntity> GetForSend( System.Int32 zoneActivityId, System.Int32 groupId,string zoneId="")
        {
            var provider = new ActivityexItemrecordProvider(zoneId);
            return provider.GetForSend( zoneActivityId, groupId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityexItemrecordProvider provider = new ActivityexItemrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityexItemrecordEntity activityexItemrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexItemrecordProvider(zoneId);
            return provider.Insert(activityexItemrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityexItemrecordEntity activityexItemrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexItemrecordProvider(zoneId);
            return provider.Update(activityexItemrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
