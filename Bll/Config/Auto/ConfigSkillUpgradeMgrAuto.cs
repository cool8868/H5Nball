
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
    /// ConfigSkillupgrade管理类
    /// </summary>
    public static partial class ConfigSkillupgradeMgr
    {
        
		#region  GetById
		
        public static ConfigSkillupgradeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigSkillupgradeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigSkillupgradeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigSkillupgradeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigSkillupgradeProvider provider = new ConfigSkillupgradeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigSkillupgradeEntity configSkillupgradeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSkillupgradeProvider(zoneId);
            return provider.Insert(configSkillupgradeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigSkillupgradeEntity configSkillupgradeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSkillupgradeProvider(zoneId);
            return provider.Update(configSkillupgradeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

