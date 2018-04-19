
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
    /// ConfigArenashop管理类
    /// </summary>
    public static partial class ConfigArenashopMgr
    {
        
		#region  GetById
		
        public static ConfigArenashopEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigArenashopProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigArenashopEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigArenashopProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigArenashopProvider provider = new ConfigArenashopProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigArenashopEntity configArenashopEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenashopProvider(zoneId);
            return provider.Insert(configArenashopEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigArenashopEntity configArenashopEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenashopProvider(zoneId);
            return provider.Update(configArenashopEntity,trans);
        }
		
		#endregion	
		
		
	}
}
