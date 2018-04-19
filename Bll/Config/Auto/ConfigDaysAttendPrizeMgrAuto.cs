
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
    /// ConfigDaysattendprize管理类
    /// </summary>
    public static partial class ConfigDaysattendprizeMgr
    {
        
		#region  GetById
		
        public static ConfigDaysattendprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigDaysattendprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigDaysattendprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigDaysattendprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigDaysattendprizeProvider provider = new ConfigDaysattendprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigDaysattendprizeEntity configDaysattendprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigDaysattendprizeProvider(zoneId);
            return provider.Insert(configDaysattendprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigDaysattendprizeEntity configDaysattendprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigDaysattendprizeProvider(zoneId);
            return provider.Update(configDaysattendprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

