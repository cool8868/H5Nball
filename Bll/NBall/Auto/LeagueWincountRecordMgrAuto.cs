
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
    /// LeagueWincountrecord管理类
    /// </summary>
    public static partial class LeagueWincountrecordMgr
    {
        
		#region  GetById
		
        public static LeagueWincountrecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new LeagueWincountrecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetRecord
		
        public static LeagueWincountrecordEntity GetRecord( System.Guid managerId, System.Int32 leagueId,string zoneId="")
        {
            var provider = new LeagueWincountrecordProvider(zoneId);
            return provider.GetRecord( managerId, leagueId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LeagueWincountrecordEntity> GetAll(string zoneId="")
        {
            var provider = new LeagueWincountrecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            LeagueWincountrecordProvider provider = new LeagueWincountrecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LeagueWincountrecordEntity leagueWincountrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueWincountrecordProvider(zoneId);
            return provider.Insert(leagueWincountrecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LeagueWincountrecordEntity leagueWincountrecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueWincountrecordProvider(zoneId);
            return provider.Update(leagueWincountrecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

