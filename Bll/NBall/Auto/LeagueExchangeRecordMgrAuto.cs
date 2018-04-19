
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
    /// LeagueExchangerecord管理类
    /// </summary>
    public static partial class LeagueExchangerecordMgr
    {
        
		#region  GetById
		
        public static LeagueExchangerecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new LeagueExchangerecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LeagueExchangerecordEntity> GetAll(string zoneId="")
        {
            var provider = new LeagueExchangerecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            LeagueExchangerecordProvider provider = new LeagueExchangerecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  ScoreToHonor
		
        public static bool ScoreToHonor ( System.DateTime curDate, System.Int32 curSeasonId, System.Int32 isNewSeason,DbTransaction trans=null,string zoneId="")
        {
            LeagueExchangerecordProvider provider = new LeagueExchangerecordProvider(zoneId);

            return provider.ScoreToHonor( curDate, curSeasonId, isNewSeason,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LeagueExchangerecordEntity leagueExchangerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueExchangerecordProvider(zoneId);
            return provider.Insert(leagueExchangerecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LeagueExchangerecordEntity leagueExchangerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueExchangerecordProvider(zoneId);
            return provider.Update(leagueExchangerecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

