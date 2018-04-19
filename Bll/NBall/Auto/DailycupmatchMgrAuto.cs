
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
    /// DailycupMatch管理类
    /// </summary>
    public static partial class DailycupMatchMgr
    {
        
		#region  GetById
		
        public static DailycupMatchEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new DailycupMatchProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DailycupMatchEntity> GetAll(string zoneId="")
        {
            var provider = new DailycupMatchProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetMatchByManager
		
        public static List<DailycupMatchEntity> GetMatchByManager( System.Int32 dailyCupId, System.Guid managerId, System.Int32 endRound,string zoneId="")
        {
            var provider = new DailycupMatchProvider(zoneId);
            return provider.GetMatchByManager( dailyCupId, managerId, endRound);            
        }
		
		#endregion		  
		
		#region  GetMatchByRound
		
        public static List<DailycupMatchEntity> GetMatchByRound( System.Int32 dailyCupId, System.Int32 beginRound, System.Int32 endRound,string zoneId="")
        {
            var provider = new DailycupMatchProvider(zoneId);
            return provider.GetMatchByRound( dailyCupId, beginRound, endRound);            
        }
		
		#endregion		  
		
		#region  UpdateMatchChipInCount
		
        public static bool UpdateMatchChipInCount ( System.Guid matchId,DbTransaction trans=null,string zoneId="")
        {
            DailycupMatchProvider provider = new DailycupMatchProvider(zoneId);

            return provider.UpdateMatchChipInCount( matchId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DailycupMatchEntity dailycupMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailycupMatchProvider(zoneId);
            return provider.Insert(dailycupMatchEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DailycupMatchEntity dailycupMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailycupMatchProvider(zoneId);
            return provider.Update(dailycupMatchEntity,trans);
        }
		
		#endregion	
		
		
	}
}

