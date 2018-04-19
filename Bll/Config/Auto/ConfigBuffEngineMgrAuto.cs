
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
    /// ConfigBuffengine管理类
    /// </summary>
    public static partial class ConfigBuffengineMgr
    {
        
		#region  GetById
		
        public static ConfigBuffengineEntity GetById( System.Int32 id,string zoneId="")
        {
            var provider = new ConfigBuffengineProvider(zoneId);
            return provider.GetById( id);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigBuffengineEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigBuffengineProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 id,DbTransaction trans=null,string zoneId="")
        {
            ConfigBuffengineProvider provider = new ConfigBuffengineProvider(zoneId);

            return provider.Delete( id,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigBuffengineEntity configBuffengineEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigBuffengineProvider(zoneId);
            return provider.Insert(configBuffengineEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigBuffengineEntity configBuffengineEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigBuffengineProvider(zoneId);
            return provider.Update(configBuffengineEntity,trans);
        }
		
		#endregion	
		
		
	}
}

