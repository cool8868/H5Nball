
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
    /// ConfigLeagueprize管理类
    /// </summary>
    public static partial class ConfigLeagueprizeMgr
    {
        
		#region  GetById
		
        public static ConfigLeagueprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigLeagueprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLeagueprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLeagueprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigLeagueprizeProvider provider = new ConfigLeagueprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLeagueprizeEntity configLeagueprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeagueprizeProvider(zoneId);
            return provider.Insert(configLeagueprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLeagueprizeEntity configLeagueprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeagueprizeProvider(zoneId);
            return provider.Update(configLeagueprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

