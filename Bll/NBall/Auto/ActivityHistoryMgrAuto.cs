
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
    /// ActivityHistory管理类
    /// </summary>
    public static partial class ActivityHistoryMgr
    {
        
		#region  GetById
		
        public static ActivityHistoryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityHistoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityHistoryEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityHistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityHistoryProvider provider = new ActivityHistoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityHistoryEntity activityHistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityHistoryProvider(zoneId);
            return provider.Insert(activityHistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityHistoryEntity activityHistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityHistoryProvider(zoneId);
            return provider.Update(activityHistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

