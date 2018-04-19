
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
    /// ConfigCrowdscore管理类
    /// </summary>
    public static partial class ConfigCrowdscoreMgr
    {
        
		#region  GetById
		
        public static ConfigCrowdscoreEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigCrowdscoreProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigCrowdscoreEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigCrowdscoreProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigCrowdscoreProvider provider = new ConfigCrowdscoreProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigCrowdscoreEntity configCrowdscoreEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrowdscoreProvider(zoneId);
            return provider.Insert(configCrowdscoreEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigCrowdscoreEntity configCrowdscoreEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrowdscoreProvider(zoneId);
            return provider.Update(configCrowdscoreEntity,trans);
        }
		
		#endregion	
		
		
	}
}
