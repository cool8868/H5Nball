
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
    /// ConfigBuffpool管理类
    /// </summary>
    public static partial class ConfigBuffpoolMgr
    {
        
		#region  GetById
		
        public static ConfigBuffpoolEntity GetById( System.Int32 id,string zoneId="")
        {
            var provider = new ConfigBuffpoolProvider(zoneId);
            return provider.GetById( id);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigBuffpoolEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigBuffpoolProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 id,DbTransaction trans=null,string zoneId="")
        {
            ConfigBuffpoolProvider provider = new ConfigBuffpoolProvider(zoneId);

            return provider.Delete( id,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigBuffpoolEntity configBuffpoolEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigBuffpoolProvider(zoneId);
            return provider.Insert(configBuffpoolEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigBuffpoolEntity configBuffpoolEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigBuffpoolProvider(zoneId);
            return provider.Update(configBuffpoolEntity,trans);
        }
		
		#endregion	
		
		
	}
}

