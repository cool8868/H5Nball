
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
    /// ConfigRevelation管理类
    /// </summary>
    public static partial class ConfigRevelationMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigRevelationProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationProvider provider = new ConfigRevelationProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationEntity configRevelationEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationProvider(zoneId);
            return provider.Insert(configRevelationEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationEntity configRevelationEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationProvider(zoneId);
            return provider.Update(configRevelationEntity,trans);
        }
		
		#endregion	
		
		
	}
}
