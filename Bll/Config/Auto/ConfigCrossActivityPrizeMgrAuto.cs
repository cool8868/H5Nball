
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
    /// ConfigCrossactivityprize管理类
    /// </summary>
    public static partial class ConfigCrossactivityprizeMgr
    {
        
		#region  GetById
		
        public static ConfigCrossactivityprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigCrossactivityprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigCrossactivityprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigCrossactivityprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigCrossactivityprizeProvider provider = new ConfigCrossactivityprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigCrossactivityprizeEntity configCrossactivityprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrossactivityprizeProvider(zoneId);
            return provider.Insert(configCrossactivityprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigCrossactivityprizeEntity configCrossactivityprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrossactivityprizeProvider(zoneId);
            return provider.Update(configCrossactivityprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
