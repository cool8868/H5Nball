
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
    /// ConfigDailyeventtime管理类
    /// </summary>
    public static partial class ConfigDailyeventtimeMgr
    {
        
		#region  GetById
		
        public static ConfigDailyeventtimeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigDailyeventtimeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigDailyeventtimeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigDailyeventtimeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigDailyeventtimeProvider provider = new ConfigDailyeventtimeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigDailyeventtimeEntity configDailyeventtimeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigDailyeventtimeProvider(zoneId);
            return provider.Insert(configDailyeventtimeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigDailyeventtimeEntity configDailyeventtimeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigDailyeventtimeProvider(zoneId);
            return provider.Update(configDailyeventtimeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

