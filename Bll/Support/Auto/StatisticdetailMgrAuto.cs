
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
    /// StatisticDetail管理类
    /// </summary>
    public static partial class StatisticDetailMgr
    {
        
		#region  GetById
		
        public static StatisticDetailEntity GetById( System.Int64 idx)
        {
            var provider = new StatisticDetailProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<StatisticDetailEntity> GetAll()
        {
            var provider = new StatisticDetailProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetbyDate
		
        public static List<StatisticDetailEntity> GetbyDate( System.Int32 zoneId, System.DateTime startTime, System.DateTime endTime)
        {
            var provider = new StatisticDetailProvider();
            return provider.GetbyDate( zoneId, startTime, endTime);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int64 idx,DbTransaction trans=null)
        {
            StatisticDetailProvider provider = new StatisticDetailProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Create
		
        public static bool Create ( System.Int32 zoneId, System.DateTime curDate,DbTransaction trans=null)
        {
            StatisticDetailProvider provider = new StatisticDetailProvider();

            return provider.Create( zoneId, curDate,trans);
            
        }
		
		#endregion
        
		#region  Update
		
        public static bool Update ( System.Int32 zoneId, System.Int32 analyseType, System.String hour, System.DateTime recordDate, System.DateTime curTime, System.Int32 curValue,DbTransaction trans=null)
        {
            StatisticDetailProvider provider = new StatisticDetailProvider();

            return provider.Update( zoneId, analyseType, hour, recordDate, curTime, curValue,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(StatisticDetailEntity statisticDetailEntity,DbTransaction trans=null)
        {
            var provider = new StatisticDetailProvider();
            return provider.Insert(statisticDetailEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(StatisticDetailEntity statisticDetailEntity,DbTransaction trans=null)
        {
            var provider = new StatisticDetailProvider();
            return provider.Update(statisticDetailEntity,trans);
        }
		
		#endregion	
		
		
	}
}
