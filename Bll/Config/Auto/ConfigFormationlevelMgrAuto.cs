
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
    /// ConfigFormationlevel管理类
    /// </summary>
    public static partial class ConfigFormationlevelMgr
    {
        
		#region  GetById
		
        public static ConfigFormationlevelEntity GetById( System.Int32 level,string zoneId="")
        {
            var provider = new ConfigFormationlevelProvider(zoneId);
            return provider.GetById( level);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigFormationlevelEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigFormationlevelProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 level,DbTransaction trans=null,string zoneId="")
        {
            ConfigFormationlevelProvider provider = new ConfigFormationlevelProvider(zoneId);

            return provider.Delete( level,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigFormationlevelEntity configFormationlevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigFormationlevelProvider(zoneId);
            return provider.Insert(configFormationlevelEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigFormationlevelEntity configFormationlevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigFormationlevelProvider(zoneId);
            return provider.Update(configFormationlevelEntity,trans);
        }
		
		#endregion	
		
		
	}
}

