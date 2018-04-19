
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
    /// ConfigArenaprize管理类
    /// </summary>
    public static partial class ConfigArenaprizeMgr
    {
        
		#region  GetById
		
        public static ConfigArenaprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigArenaprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigArenaprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigArenaprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigArenaprizeProvider provider = new ConfigArenaprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigArenaprizeEntity configArenaprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenaprizeProvider(zoneId);
            return provider.Insert(configArenaprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigArenaprizeEntity configArenaprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenaprizeProvider(zoneId);
            return provider.Update(configArenaprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
