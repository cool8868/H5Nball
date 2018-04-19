
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
    /// ConfigAppsetting管理类
    /// </summary>
    public static partial class ConfigAppsettingMgr
    {
        
		#region  GetById
		
        public static ConfigAppsettingEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigAppsettingProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigAppsettingEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigAppsettingProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<ConfigAppsettingEntity> GetAllForCache(string zoneId="")
        {
            var provider = new ConfigAppsettingProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigAppsettingProvider provider = new ConfigAppsettingProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigAppsettingEntity configAppsettingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigAppsettingProvider(zoneId);
            return provider.Insert(configAppsettingEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigAppsettingEntity configAppsettingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigAppsettingProvider(zoneId);
            return provider.Update(configAppsettingEntity,trans);
        }
		
		#endregion	
		
		
	}
}

