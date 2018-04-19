
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
    /// ConfigScouting管理类
    /// </summary>
    public static partial class ConfigScoutingMgr
    {
        
		#region  GetById
		
        public static ConfigScoutingEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigScoutingProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigScoutingEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigScoutingProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigScoutingProvider provider = new ConfigScoutingProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigScoutingEntity configScoutingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigScoutingProvider(zoneId);
            return provider.Insert(configScoutingEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigScoutingEntity configScoutingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigScoutingProvider(zoneId);
            return provider.Update(configScoutingEntity,trans);
        }
		
		#endregion	
		
		
	}
}

