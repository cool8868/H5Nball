
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
    /// LeagueRank管理类
    /// </summary>
    public static partial class LeagueRankMgr
    {
        
		#region  GetById
		
        public static LeagueRankEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new LeagueRankProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LeagueRankEntity> GetAll(string zoneId="")
        {
            var provider = new LeagueRankProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByLeagueRecordId
		
        public static List<LeagueRankEntity> GetByLeagueRecordId( System.Guid leagueRecordId,string zoneId="")
        {
            var provider = new LeagueRankProvider(zoneId);
            return provider.GetByLeagueRecordId( leagueRecordId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            LeagueRankProvider provider = new LeagueRankProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  UpdateRank
		
        public static bool UpdateRank ( System.Guid leagueRecordId,DbTransaction trans=null,string zoneId="")
        {
            LeagueRankProvider provider = new LeagueRankProvider(zoneId);

            return provider.UpdateRank( leagueRecordId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LeagueRankEntity leagueRankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueRankProvider(zoneId);
            return provider.Insert(leagueRankEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LeagueRankEntity leagueRankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueRankProvider(zoneId);
            return provider.Update(leagueRankEntity,trans);
        }
		
		#endregion	
		
		
	}
}

