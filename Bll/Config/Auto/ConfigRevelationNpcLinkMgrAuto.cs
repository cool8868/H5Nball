
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
    /// ConfigRevelationnpclink管理类
    /// </summary>
    public static partial class ConfigRevelationnpclinkMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationnpclinkEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigRevelationnpclinkProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationnpclinkEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationnpclinkProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationnpclinkProvider provider = new ConfigRevelationnpclinkProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationnpclinkEntity configRevelationnpclinkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationnpclinkProvider(zoneId);
            return provider.Insert(configRevelationnpclinkEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationnpclinkEntity configRevelationnpclinkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationnpclinkProvider(zoneId);
            return provider.Update(configRevelationnpclinkEntity,trans);
        }
		
		#endregion	
		
		
	}
}

