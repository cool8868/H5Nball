
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
    /// ConfigTxchargeid管理类
    /// </summary>
    public static partial class ConfigTxchargeidMgr
    {
        
		#region  GetById
		
        public static ConfigTxchargeidEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigTxchargeidProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigTxchargeidEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigTxchargeidProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigTxchargeidProvider provider = new ConfigTxchargeidProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigTxchargeidEntity configTxchargeidEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTxchargeidProvider(zoneId);
            return provider.Insert(configTxchargeidEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigTxchargeidEntity configTxchargeidEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigTxchargeidProvider(zoneId);
            return provider.Update(configTxchargeidEntity,trans);
        }
		
		#endregion	
		
		
	}
}
