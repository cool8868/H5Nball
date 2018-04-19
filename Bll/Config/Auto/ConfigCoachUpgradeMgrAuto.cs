
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
    /// ConfigCoachupgrade管理类
    /// </summary>
    public static partial class ConfigCoachupgradeMgr
    {
        
		#region  GetById
		
        public static ConfigCoachupgradeEntity GetById( System.Int32 level,string zoneId="")
        {
            var provider = new ConfigCoachupgradeProvider(zoneId);
            return provider.GetById( level);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigCoachupgradeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigCoachupgradeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 level,DbTransaction trans=null,string zoneId="")
        {
            ConfigCoachupgradeProvider provider = new ConfigCoachupgradeProvider(zoneId);

            return provider.Delete( level,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigCoachupgradeEntity configCoachupgradeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCoachupgradeProvider(zoneId);
            return provider.Insert(configCoachupgradeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigCoachupgradeEntity configCoachupgradeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCoachupgradeProvider(zoneId);
            return provider.Update(configCoachupgradeEntity,trans);
        }
		
		#endregion	
		
		
	}
}
