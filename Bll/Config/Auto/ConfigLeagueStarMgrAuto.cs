
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
    /// ConfigLeaguestar管理类
    /// </summary>
    public static partial class ConfigLeaguestarMgr
    {
        
		#region  GetById
		
        public static ConfigLeaguestarEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigLeaguestarProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLeaguestarEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLeaguestarProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigLeaguestarProvider provider = new ConfigLeaguestarProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLeaguestarEntity configLeaguestarEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguestarProvider(zoneId);
            return provider.Insert(configLeaguestarEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLeaguestarEntity configLeaguestarEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguestarProvider(zoneId);
            return provider.Update(configLeaguestarEntity,trans);
        }
		
		#endregion	
		
		
	}
}

