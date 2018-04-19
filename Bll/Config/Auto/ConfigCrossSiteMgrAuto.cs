
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
    /// ConfigCrosssite管理类
    /// </summary>
    public static partial class ConfigCrosssiteMgr
    {
        
		#region  GetById
		
        public static ConfigCrosssiteEntity GetById( System.Int32 id,string zoneId="")
        {
            var provider = new ConfigCrosssiteProvider(zoneId);
            return provider.GetById( id);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigCrosssiteEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigCrosssiteProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 id,DbTransaction trans=null,string zoneId="")
        {
            ConfigCrosssiteProvider provider = new ConfigCrosssiteProvider(zoneId);

            return provider.Delete( id,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigCrosssiteEntity configCrosssiteEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrosssiteProvider(zoneId);
            return provider.Insert(configCrosssiteEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigCrosssiteEntity configCrosssiteEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCrosssiteProvider(zoneId);
            return provider.Update(configCrosssiteEntity,trans);
        }
		
		#endregion	
		
		
	}
}

