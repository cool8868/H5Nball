
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
    /// ActivityexCountrecord管理类
    /// </summary>
    public static partial class ActivityexCountrecordMgr
    {
        
		#region  GetById
		
        public static ActivityexCountrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityexCountrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
        public static ActivityexCountrecordEntity GetByManagerId( System.Guid managerId, System.Int32 zoneActivityId, System.Int32 groupId,string zoneId="")
        {
            var provider = new ActivityexCountrecordProvider(zoneId);
            return provider.GetByManagerId( managerId, zoneActivityId, groupId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityexCountrecordEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityexCountrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManagerList
		
        public static List<ActivityexCountrecordEntity> GetByManagerList( System.Guid managerId, System.Int32 zoneActivityId,string zoneId="")
        {
            var provider = new ActivityexCountrecordProvider(zoneId);
            return provider.GetByManagerList( managerId, zoneActivityId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityexCountrecordProvider provider = new ActivityexCountrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityexCountrecordEntity activityexCountrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexCountrecordProvider(zoneId);
            return provider.Insert(activityexCountrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityexCountrecordEntity activityexCountrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexCountrecordProvider(zoneId);
            return provider.Update(activityexCountrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

