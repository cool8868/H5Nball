
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
    /// ActivityexHistory管理类
    /// </summary>
    public static partial class ActivityexHistoryMgr
    {
        
		#region  GetById
		
        public static ActivityexHistoryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityexHistoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityexHistoryEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityexHistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityexHistoryProvider provider = new ActivityexHistoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityexHistoryEntity activityexHistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexHistoryProvider(zoneId);
            return provider.Insert(activityexHistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityexHistoryEntity activityexHistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexHistoryProvider(zoneId);
            return provider.Update(activityexHistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

