
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
    /// LeagueRecord管理类
    /// </summary>
    public static partial class LeagueRecordMgr
    {
        
		#region  GetById
		
        public static LeagueRecordEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new LeagueRecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LeagueRecordEntity> GetAll(string zoneId="")
        {
            var provider = new LeagueRecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            LeagueRecordProvider provider = new LeagueRecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LeagueRecordEntity leagueRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueRecordProvider(zoneId);
            return provider.Insert(leagueRecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LeagueRecordEntity leagueRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueRecordProvider(zoneId);
            return provider.Update(leagueRecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

