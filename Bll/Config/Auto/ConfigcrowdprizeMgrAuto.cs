
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
    /// ConfigCrowdprize管理类
    /// </summary>
    public static partial class ConfigCrowdprizeMgr
    {
        
		#region  GetById
		
        public static ConfigCrowdprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigCrowdprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigCrowdprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigCrowdprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigCrowdprizeProvider provider = new ConfigCrowdprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigCrowdprizeEntity configCrowdprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrowdprizeProvider(zoneId);
            return provider.Insert(configCrowdprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigCrowdprizeEntity configCrowdprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrowdprizeProvider(zoneId);
            return provider.Update(configCrowdprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
