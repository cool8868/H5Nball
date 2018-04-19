
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
    /// ConfigTaskrequire管理类
    /// </summary>
    public static partial class ConfigTaskrequireMgr
    {
        
		#region  GetById
		
        public static ConfigTaskrequireEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigTaskrequireProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigTaskrequireEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigTaskrequireProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigTaskrequireProvider provider = new ConfigTaskrequireProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigTaskrequireEntity configTaskrequireEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTaskrequireProvider(zoneId);
            return provider.Insert(configTaskrequireEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigTaskrequireEntity configTaskrequireEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTaskrequireProvider(zoneId);
            return provider.Update(configTaskrequireEntity,trans);
        }
		
		#endregion	
		
		
	}
}

