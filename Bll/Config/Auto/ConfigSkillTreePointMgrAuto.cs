
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
    /// ConfigSkilltreepoint管理类
    /// </summary>
    public static partial class ConfigSkilltreepointMgr
    {
        
		#region  GetById
		
        public static ConfigSkilltreepointEntity GetById( System.Int32 managerLevel,string zoneId="")
        {
            var provider = new ConfigSkilltreepointProvider(zoneId);
            return provider.GetById( managerLevel);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigSkilltreepointEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigSkilltreepointProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 managerLevel,DbTransaction trans=null,string zoneId="")
        {
            ConfigSkilltreepointProvider provider = new ConfigSkilltreepointProvider(zoneId);

            return provider.Delete( managerLevel,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigSkilltreepointEntity configSkilltreepointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSkilltreepointProvider(zoneId);
            return provider.Insert(configSkilltreepointEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigSkilltreepointEntity configSkilltreepointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSkilltreepointProvider(zoneId);
            return provider.Update(configSkilltreepointEntity,trans);
        }
		
		#endregion	
		
		
	}
}

