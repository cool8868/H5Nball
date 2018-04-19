
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
    /// ActivityexZone管理类
    /// </summary>
    public static partial class ActivityexZoneMgr
    {
        
		#region  GetById
		
        public static ActivityexZoneEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityexZoneProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityexZoneEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityexZoneProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityexZoneProvider provider = new ActivityexZoneProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityexZoneEntity activityexZoneEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexZoneProvider(zoneId);
            return provider.Insert(activityexZoneEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityexZoneEntity activityexZoneEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexZoneProvider(zoneId);
            return provider.Update(activityexZoneEntity,trans);
        }
		
		#endregion	
		
		
	}
}

