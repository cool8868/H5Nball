
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
    /// ConfigManagerlevel管理类
    /// </summary>
    public static partial class ConfigManagerlevelMgr
    {
        
		#region  GetById
		
        public static ConfigManagerlevelEntity GetById( System.Int32 level,string zoneId="")
        {
            var provider = new ConfigManagerlevelProvider(zoneId);
            return provider.GetById( level);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigManagerlevelEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigManagerlevelProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 level,DbTransaction trans=null,string zoneId="")
        {
            ConfigManagerlevelProvider provider = new ConfigManagerlevelProvider(zoneId);

            return provider.Delete( level,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigManagerlevelEntity configManagerlevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigManagerlevelProvider(zoneId);
            return provider.Insert(configManagerlevelEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigManagerlevelEntity configManagerlevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigManagerlevelProvider(zoneId);
            return provider.Update(configManagerlevelEntity,trans);
        }
		
		#endregion	
		
		
	}
}

