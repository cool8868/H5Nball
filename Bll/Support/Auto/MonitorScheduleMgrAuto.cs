
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
    /// MonitorSchedule管理类
    /// </summary>
    public static partial class MonitorScheduleMgr
    {
        
		#region  GetById
		
        public static MonitorScheduleEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new MonitorScheduleProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByZone
		
        public static MonitorScheduleEntity GetByZone( System.Int32 zoneId, System.Int32 scheduleId, System.Int32 appId, System.String terminalIp)
        {
            var provider = new MonitorScheduleProvider();
            return provider.GetByZone( zoneId, scheduleId, appId, terminalIp);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<MonitorScheduleEntity> GetAll(string zoneId="")
        {
            var provider = new MonitorScheduleProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  UpdateApp
		
        public static bool UpdateApp ( System.Int32 idx, System.Int32 appId, System.String terminalIp,DbTransaction trans=null,string zoneId="")
        {
            MonitorScheduleProvider provider = new MonitorScheduleProvider(zoneId);

            return provider.UpdateApp( idx, appId, terminalIp,trans);
            
        }
		
		#endregion
        
		#region  Start
		
        public static bool Start ( System.Int32 idx, System.Int32 status, System.DateTime startTime,DbTransaction trans=null,string zoneId="")
        {
            MonitorScheduleProvider provider = new MonitorScheduleProvider(zoneId);

            return provider.Start( idx, status, startTime,trans);
            
        }
		
		#endregion
        
		#region  Finish
		
        public static bool Finish ( System.Int32 idx, System.Int32 status, System.DateTime invokeTime, System.DateTime endTime, System.DateTime lastFailTime, System.Int64 successTimes, System.Int64 failTimes, System.Int32 retryTimes,DbTransaction trans=null,string zoneId="")
        {
            MonitorScheduleProvider provider = new MonitorScheduleProvider(zoneId);

            return provider.Finish( idx, status, invokeTime, endTime, lastFailTime, successTimes, failTimes, retryTimes,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(MonitorScheduleEntity monitorScheduleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MonitorScheduleProvider(zoneId);
            return provider.Insert(monitorScheduleEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(MonitorScheduleEntity monitorScheduleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MonitorScheduleProvider(zoneId);
            return provider.Update(monitorScheduleEntity,trans);
        }
		
		#endregion	
		
		
	}
}

