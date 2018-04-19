
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
    /// ConfigLeaguemark管理类
    /// </summary>
    public static partial class ConfigLeaguemarkMgr
    {
        
		#region  GetById
		
        public static ConfigLeaguemarkEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new ConfigLeaguemarkProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigLeaguemarkEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigLeaguemarkProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigLeaguemarkProvider provider = new ConfigLeaguemarkProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigLeaguemarkEntity configLeaguemarkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguemarkProvider(zoneId);
            return provider.Insert(configLeaguemarkEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigLeaguemarkEntity configLeaguemarkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigLeaguemarkProvider(zoneId);
            return provider.Update(configLeaguemarkEntity,trans);
        }
		
		#endregion	
		
		
	}
}

