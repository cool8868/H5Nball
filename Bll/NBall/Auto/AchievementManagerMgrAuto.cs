
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
    /// AchievementManager管理类
    /// </summary>
    public static partial class AchievementManagerMgr
    {
        
		#region  GetById
		
        public static AchievementManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new AchievementManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AchievementManagerEntity> GetAll(string zoneId="")
        {
            var provider = new AchievementManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            AchievementManagerProvider provider = new AchievementManagerProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AchievementManagerEntity achievementManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AchievementManagerProvider(zoneId);
            return provider.Insert(achievementManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AchievementManagerEntity achievementManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AchievementManagerProvider(zoneId);
            return provider.Update(achievementManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}

