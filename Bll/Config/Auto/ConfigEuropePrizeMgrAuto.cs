
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
    /// ConfigEuropeprize管理类
    /// </summary>
    public static partial class ConfigEuropeprizeMgr
    {
        
		#region  GetById
		
        public static ConfigEuropeprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigEuropeprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigEuropeprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigEuropeprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigEuropeprizeProvider provider = new ConfigEuropeprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigEuropeprizeEntity configEuropeprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEuropeprizeProvider(zoneId);
            return provider.Insert(configEuropeprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigEuropeprizeEntity configEuropeprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigEuropeprizeProvider(zoneId);
            return provider.Update(configEuropeprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

