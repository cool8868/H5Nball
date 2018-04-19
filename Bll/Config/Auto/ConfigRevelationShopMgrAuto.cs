
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
    /// ConfigRevelationshop管理类
    /// </summary>
    public static partial class ConfigRevelationshopMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationshopEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigRevelationshopProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationshopEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationshopProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationshopProvider provider = new ConfigRevelationshopProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationshopEntity configRevelationshopEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationshopProvider(zoneId);
            return provider.Insert(configRevelationshopEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationshopEntity configRevelationshopEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationshopProvider(zoneId);
            return provider.Update(configRevelationshopEntity,trans);
        }
		
		#endregion	
		
		
	}
}
