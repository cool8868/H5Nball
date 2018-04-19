
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
    /// ConfigTask管理类
    /// </summary>
    public static partial class ConfigTaskMgr
    {
        
		#region  GetById
		
        public static ConfigTaskEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigTaskProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigTaskEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigTaskProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
        public static List<ConfigTaskEntity> GetAllForCache(string zoneId="")
        {
            var provider = new ConfigTaskProvider(zoneId);
            return provider.GetAllForCache();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigTaskProvider provider = new ConfigTaskProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigTaskEntity configTaskEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTaskProvider(zoneId);
            return provider.Insert(configTaskEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigTaskEntity configTaskEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTaskProvider(zoneId);
            return provider.Update(configTaskEntity,trans);
        }
		
		#endregion	
		
		
	}
}

