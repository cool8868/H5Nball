
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
    /// ArenaManagerrecord管理类
    /// </summary>
    public static partial class ArenaManagerrecordMgr
    {
        
		#region  GetById
		
        public static ArenaManagerrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ArenaManagerrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetChampion
		
        public static ArenaManagerrecordEntity GetChampion( System.Int32 seasonId, System.Int32 domainId,string zoneId="")
        {
            var provider = new ArenaManagerrecordProvider(zoneId);
            return provider.GetChampion( seasonId, domainId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ArenaManagerrecordEntity> GetAll(string zoneId="")
        {
            var provider = new ArenaManagerrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetNotPrize
		
        public static List<ArenaManagerrecordEntity> GetNotPrize(string zoneId="")
        {
            var provider = new ArenaManagerrecordProvider(zoneId);
            return provider.GetNotPrize();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ArenaManagerrecordProvider provider = new ArenaManagerrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ArenaManagerrecordEntity arenaManagerrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaManagerrecordProvider(zoneId);
            return provider.Insert(arenaManagerrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ArenaManagerrecordEntity arenaManagerrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ArenaManagerrecordProvider(zoneId);
            return provider.Update(arenaManagerrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
