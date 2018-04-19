
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
    /// StatisticInfo管理类
    /// </summary>
    public static partial class StatisticInfoMgr
    {
        
		#region  GetById
		
        public static StatisticInfoEntity GetById( System.Int32 zoneId)
        {
            var provider = new StatisticInfoProvider();
            return provider.GetById( zoneId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<StatisticInfoEntity> GetAll()
        {
            var provider = new StatisticInfoProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 zoneId,DbTransaction trans=null)
        {
            StatisticInfoProvider provider = new StatisticInfoProvider();

            return provider.Delete( zoneId,trans);
            
        }
		
		#endregion
        
		#region  Create
		
        public static bool Create ( System.Int32 zoneId,DbTransaction trans=null)
        {
            StatisticInfoProvider provider = new StatisticInfoProvider();

            return provider.Create( zoneId,trans);
            
        }
		
		#endregion
        
		#region  Update
		
        public static bool Update ( System.Int32 zoneId, System.Int32 totalUser, System.Int32 totalManager, System.Int64 totalPay, System.Int64 pointRemain, System.Int64 onlineMinutes, System.DateTime updateTime,DbTransaction trans=null)
        {
            StatisticInfoProvider provider = new StatisticInfoProvider();

            return provider.Update( zoneId, totalUser, totalManager, totalPay, pointRemain, onlineMinutes, updateTime,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(StatisticInfoEntity statisticInfoEntity,DbTransaction trans=null)
        {
            var provider = new StatisticInfoProvider();
            return provider.Insert(statisticInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(StatisticInfoEntity statisticInfoEntity,DbTransaction trans=null)
        {
            var provider = new StatisticInfoProvider();
            return provider.Update(statisticInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
