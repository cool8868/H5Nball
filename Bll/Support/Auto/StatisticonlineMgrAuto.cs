
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
    /// StatisticOnline管理类
    /// </summary>
    public static partial class StatisticOnlineMgr
    {
        
		#region  GetById
		
        public static StatisticOnlineEntity GetById( System.Int64 idx)
        {
            var provider = new StatisticOnlineProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<StatisticOnlineEntity> GetAll()
        {
            var provider = new StatisticOnlineProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetbyDate
		
        public static List<StatisticOnlineEntity> GetbyDate( System.Int32 zoneId, System.DateTime startTime, System.DateTime endTime)
        {
            var provider = new StatisticOnlineProvider();
            return provider.GetbyDate( zoneId, startTime, endTime);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int64 idx,DbTransaction trans=null)
        {
            StatisticOnlineProvider provider = new StatisticOnlineProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Create
		
        public static bool Create ( System.Int32 zoneId, System.DateTime curDate,DbTransaction trans=null)
        {
            StatisticOnlineProvider provider = new StatisticOnlineProvider();

            return provider.Create( zoneId, curDate,trans);
            
        }
		
		#endregion
        
		#region  Update
		
        public static bool Update ( System.Int32 zoneId, System.String hour, System.DateTime recordDate, System.DateTime curTime, System.Int32 curValue, System.Int64 totalMinutes,DbTransaction trans=null)
        {
            StatisticOnlineProvider provider = new StatisticOnlineProvider();

            return provider.Update( zoneId, hour, recordDate, curTime, curValue, totalMinutes,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(StatisticOnlineEntity statisticOnlineEntity,DbTransaction trans=null)
        {
            var provider = new StatisticOnlineProvider();
            return provider.Insert(statisticOnlineEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(StatisticOnlineEntity statisticOnlineEntity,DbTransaction trans=null)
        {
            var provider = new StatisticOnlineProvider();
            return provider.Update(statisticOnlineEntity,trans);
        }
		
		#endregion	
		
		
	}
}
