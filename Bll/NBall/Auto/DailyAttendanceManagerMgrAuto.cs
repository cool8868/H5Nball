
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
    /// DailyattendanceManager管理类
    /// </summary>
    public static partial class DailyattendanceManagerMgr
    {
        
		#region  GetManager
		
        public static DailyattendanceManagerEntity GetManager( System.Guid managerId, System.String name, System.Int32 month,string zoneId="")
        {
            var provider = new DailyattendanceManagerProvider(zoneId);
            return provider.GetManager( managerId, name, month);
        }
		
		#endregion		  
		
		#region  UpdateStatus
		
        public static DailyattendanceManagerEntity UpdateStatus( System.Guid managerId,string zoneId="")
        {
            var provider = new DailyattendanceManagerProvider(zoneId);
            return provider.UpdateStatus( managerId);
        }
		
		#endregion		  
		
		#region  UpdateMonth
		
        public static DailyattendanceManagerEntity UpdateMonth( System.Guid managerId, System.Int32 month,string zoneId="")
        {
            var provider = new DailyattendanceManagerProvider(zoneId);
            return provider.UpdateMonth( managerId, month);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DailyattendanceManagerEntity> GetAll(string zoneId="")
        {
            var provider = new DailyattendanceManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int64 idx,DbTransaction trans=null,string zoneId="")
        {
            DailyattendanceManagerProvider provider = new DailyattendanceManagerProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DailyattendanceManagerEntity dailyattendanceManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailyattendanceManagerProvider(zoneId);
            return provider.Insert(dailyattendanceManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DailyattendanceManagerEntity dailyattendanceManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailyattendanceManagerProvider(zoneId);
            return provider.Update(dailyattendanceManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}

