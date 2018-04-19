
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
    /// ConfigArenanpclink管理类
    /// </summary>
    public static partial class ConfigArenanpclinkMgr
    {
        
		#region  GetById
		
        public static ConfigArenanpclinkEntity GetById( System.Guid npcId,string zoneId="")
        {
            var provider = new ConfigArenanpclinkProvider(zoneId);
            return provider.GetById( npcId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigArenanpclinkEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigArenanpclinkProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid npcId,DbTransaction trans=null,string zoneId="")
        {
            ConfigArenanpclinkProvider provider = new ConfigArenanpclinkProvider(zoneId);

            return provider.Delete( npcId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigArenanpclinkEntity configArenanpclinkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenanpclinkProvider(zoneId);
            return provider.Insert(configArenanpclinkEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigArenanpclinkEntity configArenanpclinkEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigArenanpclinkProvider(zoneId);
            return provider.Update(configArenanpclinkEntity,trans);
        }
		
		#endregion	
		
		
	}
}
