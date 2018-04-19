
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
    /// ConfigArenadangrading管理类
    /// </summary>
    public static partial class ConfigArenadangradingMgr
    {
        
		#region  GetById
		
        public static ConfigArenadangradingEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigArenadangradingProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigArenadangradingEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigArenadangradingProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigArenadangradingProvider provider = new ConfigArenadangradingProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigArenadangradingEntity configArenadangradingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenadangradingProvider(zoneId);
            return provider.Insert(configArenadangradingEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigArenadangradingEntity configArenadangradingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenadangradingProvider(zoneId);
            return provider.Update(configArenadangradingEntity,trans);
        }
		
		#endregion	
		
		
	}
}
