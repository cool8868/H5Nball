
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
    /// ConfigCoachskill管理类
    /// </summary>
    public static partial class ConfigCoachskillMgr
    {
        
		#region  GetById
		
        public static ConfigCoachskillEntity GetById( System.Int32 coachId,string zoneId="")
        {
            var provider = new ConfigCoachskillProvider(zoneId);
            return provider.GetById( coachId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigCoachskillEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigCoachskillProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 coachId,DbTransaction trans=null,string zoneId="")
        {
            ConfigCoachskillProvider provider = new ConfigCoachskillProvider(zoneId);

            return provider.Delete( coachId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigCoachskillEntity configCoachskillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCoachskillProvider(zoneId);
            return provider.Insert(configCoachskillEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigCoachskillEntity configCoachskillEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigCoachskillProvider(zoneId);
            return provider.Update(configCoachskillEntity,trans);
        }
		
		#endregion	
		
		
	}
}
