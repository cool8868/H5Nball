
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
    /// ConfigArenanpcopponent管理类
    /// </summary>
    public static partial class ConfigArenanpcopponentMgr
    {
        
		#region  GetById
		
        public static ConfigArenanpcopponentEntity GetById( System.Int32 idx, System.Int32 opponent,string zoneId="")
        {
            var provider = new ConfigArenanpcopponentProvider(zoneId);
            return provider.GetById( idx, opponent);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigArenanpcopponentEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigArenanpcopponentProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx, System.Int32 opponent,DbTransaction trans=null,string zoneId="")
        {
            ConfigArenanpcopponentProvider provider = new ConfigArenanpcopponentProvider(zoneId);

            return provider.Delete( idx, opponent,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigArenanpcopponentEntity configArenanpcopponentEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenanpcopponentProvider(zoneId);
            return provider.Insert(configArenanpcopponentEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigArenanpcopponentEntity configArenanpcopponentEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenanpcopponentProvider(zoneId);
            return provider.Update(configArenanpcopponentEntity,trans);
        }
		
		#endregion	
		
		
	}
}
