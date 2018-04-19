
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
    /// ConfigRevelationvip管理类
    /// </summary>
    public static partial class ConfigRevelationvipMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationvipEntity GetById( System.Int32 vipLevel,string zoneId="")
        {
            var provider = new ConfigRevelationvipProvider(zoneId);
            return provider.GetById( vipLevel);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationvipEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationvipProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 vipLevel,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationvipProvider provider = new ConfigRevelationvipProvider(zoneId);

            return provider.Delete( vipLevel,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationvipEntity configRevelationvipEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationvipProvider(zoneId);
            return provider.Insert(configRevelationvipEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationvipEntity configRevelationvipEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationvipProvider(zoneId);
            return provider.Update(configRevelationvipEntity,trans);
        }
		
		#endregion	
		
		
	}
}

