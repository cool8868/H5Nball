
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
    /// ConfigShare管理类
    /// </summary>
    public static partial class ConfigShareMgr
    {
        
		#region  GetById
		
        public static ConfigShareEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigShareProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigShareEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigShareProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigShareProvider provider = new ConfigShareProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigShareEntity configShareEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigShareProvider(zoneId);
            return provider.Insert(configShareEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigShareEntity configShareEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigShareProvider(zoneId);
            return provider.Update(configShareEntity,trans);
        }
		
		#endregion	
		
		
	}
}

