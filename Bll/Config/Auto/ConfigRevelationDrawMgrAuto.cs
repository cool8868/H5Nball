
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
    /// ConfigRevelationdraw管理类
    /// </summary>
    public static partial class ConfigRevelationdrawMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationdrawEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigRevelationdrawProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationdrawEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationdrawProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationdrawProvider provider = new ConfigRevelationdrawProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationdrawEntity configRevelationdrawEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationdrawProvider(zoneId);
            return provider.Insert(configRevelationdrawEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationdrawEntity configRevelationdrawEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationdrawProvider(zoneId);
            return provider.Update(configRevelationdrawEntity,trans);
        }
		
		#endregion	
		
		
	}
}
