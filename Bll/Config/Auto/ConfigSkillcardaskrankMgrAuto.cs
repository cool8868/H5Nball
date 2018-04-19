
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
    /// ConfigSkillcardaskrank管理类
    /// </summary>
    public static partial class ConfigSkillcardaskrankMgr
    {
        
		#region  GetById
		
        public static ConfigSkillcardaskrankEntity GetById( System.Int32 npcId,string zoneId="")
        {
            var provider = new ConfigSkillcardaskrankProvider(zoneId);
            return provider.GetById( npcId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigSkillcardaskrankEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigSkillcardaskrankProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 npcId,DbTransaction trans=null,string zoneId="")
        {
            ConfigSkillcardaskrankProvider provider = new ConfigSkillcardaskrankProvider(zoneId);

            return provider.Delete( npcId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigSkillcardaskrankEntity configSkillcardaskrankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSkillcardaskrankProvider(zoneId);
            return provider.Insert(configSkillcardaskrankEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigSkillcardaskrankEntity configSkillcardaskrankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigSkillcardaskrankProvider(zoneId);
            return provider.Update(configSkillcardaskrankEntity,trans);
        }
		
		#endregion	
		
		
	}
}

