
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
    /// ConfigLeague管理类
    /// </summary>
    public static partial class ConfigLeagueMgr
    {
        
		#region  GetById
		
        public static ConfigLeagueEntity GetById( System.Int32 leagueID,string zoneId="")
        {
            var provider = new ConfigLeagueProvider(zoneId);
            return provider.GetById( leagueID);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLeagueEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLeagueProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 leagueID,DbTransaction trans=null,string zoneId="")
        {
            ConfigLeagueProvider provider = new ConfigLeagueProvider(zoneId);

            return provider.Delete( leagueID,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLeagueEntity configLeagueEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeagueProvider(zoneId);
            return provider.Insert(configLeagueEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLeagueEntity configLeagueEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeagueProvider(zoneId);
            return provider.Update(configLeagueEntity,trans);
        }
		
		#endregion	
		
		
	}
}

