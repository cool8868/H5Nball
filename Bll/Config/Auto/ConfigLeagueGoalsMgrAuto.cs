
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
    /// ConfigLeaguegoals管理类
    /// </summary>
    public static partial class ConfigLeaguegoalsMgr
    {
        
		#region  GetById
		
        public static ConfigLeaguegoalsEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigLeaguegoalsProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLeaguegoalsEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLeaguegoalsProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigLeaguegoalsProvider provider = new ConfigLeaguegoalsProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLeaguegoalsEntity configLeaguegoalsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguegoalsProvider(zoneId);
            return provider.Insert(configLeaguegoalsEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLeaguegoalsEntity configLeaguegoalsEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguegoalsProvider(zoneId);
            return provider.Update(configLeaguegoalsEntity,trans);
        }
		
		#endregion	
		
		
	}
}

