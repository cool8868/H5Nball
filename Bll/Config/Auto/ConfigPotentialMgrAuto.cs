
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
    /// ConfigPotential管理类
    /// </summary>
    public static partial class ConfigPotentialMgr
    {
        
		#region  GetById
		
        public static ConfigPotentialEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigPotentialProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigPotentialEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigPotentialProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigPotentialProvider provider = new ConfigPotentialProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigPotentialEntity configPotentialEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPotentialProvider(zoneId);
            return provider.Insert(configPotentialEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigPotentialEntity configPotentialEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigPotentialProvider(zoneId);
            return provider.Update(configPotentialEntity,trans);
        }
		
		#endregion	
		
		
	}
}
