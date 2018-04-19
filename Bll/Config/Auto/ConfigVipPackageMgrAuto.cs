
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
    /// ConfigVippackage管理类
    /// </summary>
    public static partial class ConfigVippackageMgr
    {
        
		#region  GetById
		
        public static ConfigVippackageEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigVippackageProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigVippackageEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigVippackageProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigVippackageProvider provider = new ConfigVippackageProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigVippackageEntity configVippackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigVippackageProvider(zoneId);
            return provider.Insert(configVippackageEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigVippackageEntity configVippackageEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigVippackageProvider(zoneId);
            return provider.Update(configVippackageEntity,trans);
        }
		
		#endregion	
		
		
	}
}

