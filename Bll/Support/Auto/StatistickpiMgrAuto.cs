
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
    /// StatisticKpi管理类
    /// </summary>
    public static partial class StatisticKpiMgr
    {
        
		#region  GetById
		
        public static StatisticKpiEntity GetById( System.Int64 idx)
        {
            var provider = new StatisticKpiProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<StatisticKpiEntity> GetAll()
        {
            var provider = new StatisticKpiProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetbyDate
		
        public static List<StatisticKpiEntity> GetbyDate( System.Int32 zoneId, System.DateTime startTime, System.DateTime endTime)
        {
            var provider = new StatisticKpiProvider();
            return provider.GetbyDate( zoneId, startTime, endTime);            
        }
		
		#endregion		  
		
		#region  GetZonebyDate
		
        public static List<StatisticKpiEntity> GetZonebyDate( System.Int32 zoneId, System.DateTime startTime, System.DateTime endTime)
        {
            var provider = new StatisticKpiProvider();
            return provider.GetZonebyDate( zoneId, startTime, endTime);            
        }
		
		#endregion		  
		
		#region  GetbyPlatform
		
        public static List<StatisticKpiEntity> GetbyPlatform( System.Int32 zoneId, System.String platCode, System.DateTime startTime, System.DateTime endTime)
        {
            var provider = new StatisticKpiProvider();
            return provider.GetbyPlatform( zoneId, platCode, startTime, endTime);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int64 idx,DbTransaction trans=null)
        {
            StatisticKpiProvider provider = new StatisticKpiProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Create
		
        public static bool Create ( System.Int32 zoneId, System.DateTime curDate,DbTransaction trans=null)
        {
            StatisticKpiProvider provider = new StatisticKpiProvider();

            return provider.Create( zoneId, curDate,trans);
            
        }
		
		#endregion
        
		#region  Update
		
        public static bool Update ( System.Int32 zoneId, System.String recordMonth, System.DateTime recordDate, System.Int32 totalUser, System.Int32 totalManager, System.Int32 dau, System.Int32 dUniqueIp, System.Int32 dNewUser, System.Int32 dNewManager, System.Int32 dLostUser7, System.Int32 dLostUser15, System.Int32 dLostUser30, System.Int32 retention2, System.Int32 retention3, System.Int32 retention4, System.Int32 retention5, System.Int32 retention6, System.Int32 retention7, System.Int32 retention15, System.Int32 retention30, System.Int32 wau, System.Int32 wLost, System.Int32 wHonor, System.Int32 wHonorLost, System.Int32 mau, System.Int32 payUserCount, System.Int32 payCount, System.Int32 payTotal, System.Int64 paySum, System.Int32 payFirst, System.Int64 pointRemain, System.Int64 pointConsume, System.Int64 pointCirculate, System.DateTime updateTime,DbTransaction trans=null)
        {
            StatisticKpiProvider provider = new StatisticKpiProvider();

            return provider.Update( zoneId, recordMonth, recordDate, totalUser, totalManager, dau, dUniqueIp, dNewUser, dNewManager, dLostUser7, dLostUser15, dLostUser30, retention2, retention3, retention4, retention5, retention6, retention7, retention15, retention30, wau, wLost, wHonor, wHonorLost, mau, payUserCount, payCount, payTotal, paySum, payFirst, pointRemain, pointConsume, pointCirculate, updateTime,trans);
            
        }
		
		#endregion
        
		#region  UpdateImmediate
		
        public static bool UpdateImmediate ( System.Int32 zoneId, System.String recordMonth, System.DateTime recordDate, System.Int32 totalUser, System.Int32 totalManager, System.Int32 dau, System.Int32 dUniqueIp, System.Int32 dNewUser, System.Int32 dNewManager, System.Int32 payUserCount, System.Int32 payCount, System.Int32 payTotal, System.Int64 paySum, System.Int32 payFirst, System.Int32 mau, System.DateTime updateTime,DbTransaction trans=null)
        {
            StatisticKpiProvider provider = new StatisticKpiProvider();

            return provider.UpdateImmediate( zoneId, recordMonth, recordDate, totalUser, totalManager, dau, dUniqueIp, dNewUser, dNewManager, payUserCount, payCount, payTotal, paySum, payFirst, mau, updateTime,trans);
            
        }
		
		#endregion
        
		#region  UpdateSame
		
        public static bool UpdateSame ( System.Int32 zoneId, System.DateTime recordDate, System.Int32 getPoint, System.Int32 energyConsume, System.Int64 coinConsume, System.Int64 getCion,DbTransaction trans=null)
        {
            StatisticKpiProvider provider = new StatisticKpiProvider();

            return provider.UpdateSame( zoneId, recordDate, getPoint, energyConsume, coinConsume, getCion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(StatisticKpiEntity statisticKpiEntity,DbTransaction trans=null)
        {
            var provider = new StatisticKpiProvider();
            return provider.Insert(statisticKpiEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(StatisticKpiEntity statisticKpiEntity,DbTransaction trans=null)
        {
            var provider = new StatisticKpiProvider();
            return provider.Update(statisticKpiEntity,trans);
        }
		
		#endregion	
		
		
	}
}
