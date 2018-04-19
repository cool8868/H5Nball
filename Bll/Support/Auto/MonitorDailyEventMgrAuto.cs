
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
    /// MonitorDailyevent管理类
    /// </summary>
    public static partial class MonitorDailyeventMgr
    {
        
		#region  GetById
		
        public static MonitorDailyeventEntity GetById( System.Int32 idx)
        {
            var provider = new MonitorDailyeventProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByZoneEvent
		
        public static MonitorDailyeventEntity GetByZoneEvent( System.Int32 zoneId, System.Int32 eventType)
        {
            var provider = new MonitorDailyeventProvider();
            return provider.GetByZoneEvent( zoneId, eventType);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<MonitorDailyeventEntity> GetAll()
        {
            var provider = new MonitorDailyeventProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByZone
		
        public static List<MonitorDailyeventEntity> GetByZone( System.Int32 zoneId)
        {
            var provider = new MonitorDailyeventProvider();
            return provider.GetByZone( zoneId);            
        }
		
		#endregion		  
		
		#region  Save
		
        public static bool Save ( System.Int32 zoneId, System.Int32 eventType, System.DateTime openTime, System.DateTime startTime, System.DateTime endTime, System.DateTime recordDate,DbTransaction trans=null)
        {
            MonitorDailyeventProvider provider = new MonitorDailyeventProvider();

            return provider.Save( zoneId, eventType, openTime, startTime, endTime, recordDate,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(MonitorDailyeventEntity monitorDailyeventEntity,DbTransaction trans=null)
        {
            var provider = new MonitorDailyeventProvider();
            return provider.Insert(monitorDailyeventEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(MonitorDailyeventEntity monitorDailyeventEntity,DbTransaction trans=null)
        {
            var provider = new MonitorDailyeventProvider();
            return provider.Update(monitorDailyeventEntity,trans);
        }
		
		#endregion	
		
		
	}
}
