
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
    /// StatisticClick管理类
    /// </summary>
    public static partial class StatisticClickMgr
    {
        
		#region  GetById
		
        public static StatisticClickEntity GetById( System.Int64 idx)
        {
            var provider = new StatisticClickProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<StatisticClickEntity> GetAll()
        {
            var provider = new StatisticClickProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int64 idx,DbTransaction trans=null)
        {
            StatisticClickProvider provider = new StatisticClickProvider();

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Update
		
        public static bool Update ( System.Int32 type, System.DateTime recordDate,DbTransaction trans=null)
        {
            StatisticClickProvider provider = new StatisticClickProvider();

            return provider.Update( type, recordDate,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(StatisticClickEntity statisticClickEntity,DbTransaction trans=null)
        {
            var provider = new StatisticClickProvider();
            return provider.Insert(statisticClickEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(StatisticClickEntity statisticClickEntity,DbTransaction trans=null)
        {
            var provider = new StatisticClickProvider();
            return provider.Update(statisticClickEntity,trans);
        }
		
		#endregion	
		
		
	}
}

