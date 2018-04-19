
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
    /// ConfigMallgiftbag管理类
    /// </summary>
    public static partial class ConfigMallgiftbagMgr
    {
        
		#region  GetById
		
        public static ConfigMallgiftbagEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigMallgiftbagProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigMallgiftbagEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigMallgiftbagProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigMallgiftbagProvider provider = new ConfigMallgiftbagProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigMallgiftbagEntity configMallgiftbagEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigMallgiftbagProvider(zoneId);
            return provider.Insert(configMallgiftbagEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigMallgiftbagEntity configMallgiftbagEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigMallgiftbagProvider(zoneId);
            return provider.Update(configMallgiftbagEntity,trans);
        }
		
		#endregion	
		
		
	}
}

