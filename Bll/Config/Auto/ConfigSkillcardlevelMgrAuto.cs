
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
    /// ConfigSkillcardlevel管理类
    /// </summary>
    public static partial class ConfigSkillcardlevelMgr
    {
        
		#region  GetById
		
        public static ConfigSkillcardlevelEntity GetById( System.Int32 rowId,string zoneId="")
        {
            var provider = new ConfigSkillcardlevelProvider(zoneId);
            return provider.GetById( rowId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigSkillcardlevelEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigSkillcardlevelProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 rowId,DbTransaction trans=null,string zoneId="")
        {
            ConfigSkillcardlevelProvider provider = new ConfigSkillcardlevelProvider(zoneId);

            return provider.Delete( rowId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigSkillcardlevelEntity configSkillcardlevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSkillcardlevelProvider(zoneId);
            return provider.Insert(configSkillcardlevelEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigSkillcardlevelEntity configSkillcardlevelEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSkillcardlevelProvider(zoneId);
            return provider.Update(configSkillcardlevelEntity,trans);
        }
		
		#endregion	
		
		
	}
}

