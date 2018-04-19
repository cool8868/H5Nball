
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
    /// ConfigDailycupprize管理类
    /// </summary>
    public static partial class ConfigDailycupprizeMgr
    {
        
		#region  GetById
		
        public static ConfigDailycupprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigDailycupprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigDailycupprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigDailycupprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigDailycupprizeProvider provider = new ConfigDailycupprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigDailycupprizeEntity configDailycupprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigDailycupprizeProvider(zoneId);
            return provider.Insert(configDailycupprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigDailycupprizeEntity configDailycupprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigDailycupprizeProvider(zoneId);
            return provider.Update(configDailycupprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

