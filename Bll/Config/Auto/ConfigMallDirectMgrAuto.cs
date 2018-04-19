
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
    /// ConfigMalldirect管理类
    /// </summary>
    public static partial class ConfigMalldirectMgr
    {
        
		#region  GetById
		
        public static ConfigMalldirectEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigMalldirectProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigMalldirectEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigMalldirectProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigMalldirectProvider provider = new ConfigMalldirectProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigMalldirectEntity configMalldirectEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigMalldirectProvider(zoneId);
            return provider.Insert(configMalldirectEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigMalldirectEntity configMalldirectEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigMalldirectProvider(zoneId);
            return provider.Update(configMalldirectEntity,trans);
        }
		
		#endregion	
		
		
	}
}

