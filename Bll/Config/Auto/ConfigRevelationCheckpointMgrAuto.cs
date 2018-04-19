
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
    /// ConfigRevelationcheckpoint管理类
    /// </summary>
    public static partial class ConfigRevelationcheckpointMgr
    {
        
		#region  GetById
		
        public static ConfigRevelationcheckpointEntity GetById( System.Int32 mark, System.Int32 smallClearance,string zoneId="")
        {
            var provider = new ConfigRevelationcheckpointProvider(zoneId);
            return provider.GetById( mark, smallClearance);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigRevelationcheckpointEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigRevelationcheckpointProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 mark, System.Int32 smallClearance,DbTransaction trans=null,string zoneId="")
        {
            ConfigRevelationcheckpointProvider provider = new ConfigRevelationcheckpointProvider(zoneId);

            return provider.Delete( mark, smallClearance,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigRevelationcheckpointEntity configRevelationcheckpointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationcheckpointProvider(zoneId);
            return provider.Insert(configRevelationcheckpointEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigRevelationcheckpointEntity configRevelationcheckpointEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigRevelationcheckpointProvider(zoneId);
            return provider.Update(configRevelationcheckpointEntity,trans);
        }
		
		#endregion	
		
		
	}
}

