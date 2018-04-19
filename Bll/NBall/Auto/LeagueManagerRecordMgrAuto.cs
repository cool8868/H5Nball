
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
    /// LeagueManagerrecord管理类
    /// </summary>
    public static partial class LeagueManagerrecordMgr
    {
        
		#region  GetById
		
        public static LeagueManagerrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new LeagueManagerrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetManagerMarkInfo
		
        public static LeagueManagerrecordEntity GetManagerMarkInfo( System.Guid managerId, System.Int32 leagueId,string zoneId="")
        {
            var provider = new LeagueManagerrecordProvider(zoneId);
            return provider.GetManagerMarkInfo( managerId, leagueId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LeagueManagerrecordEntity> GetAll(string zoneId="")
        {
            var provider = new LeagueManagerrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetManagerAllMark
		
        public static List<LeagueManagerrecordEntity> GetManagerAllMark( System.Guid managerId,string zoneId="")
        {
            var provider = new LeagueManagerrecordProvider(zoneId);
            return provider.GetManagerAllMark( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            LeagueManagerrecordProvider provider = new LeagueManagerrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LeagueManagerrecordEntity leagueManagerrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueManagerrecordProvider(zoneId);
            return provider.Insert(leagueManagerrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LeagueManagerrecordEntity leagueManagerrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueManagerrecordProvider(zoneId);
            return provider.Update(leagueManagerrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

