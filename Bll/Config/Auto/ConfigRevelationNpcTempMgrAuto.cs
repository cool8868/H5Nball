
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
    /// ConfigRevelationnpctemp管理类
    /// </summary>
    public static partial class ConfigRevelationnpctempMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationnpctempEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigRevelationnpctempProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationnpctempEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationnpctempProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationnpctempProvider provider = new ConfigRevelationnpctempProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationnpctempEntity configRevelationnpctempEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationnpctempProvider(zoneId);
            return provider.Insert(configRevelationnpctempEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationnpctempEntity configRevelationnpctempEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationnpctempProvider(zoneId);
            return provider.Update(configRevelationnpctempEntity,trans);
        }
		
		#endregion	
		
		
	}
}
