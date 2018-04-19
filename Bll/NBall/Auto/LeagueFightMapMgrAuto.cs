
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
    /// LeagueFightmap管理类
    /// </summary>
    public static partial class LeagueFightmapMgr
    {
        
		#region  GetById
		
        public static LeagueFightmapEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new LeagueFightmapProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LeagueFightmapEntity> GetAll(string zoneId="")
        {
            var provider = new LeagueFightmapProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            LeagueFightmapProvider provider = new LeagueFightmapProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LeagueFightmapEntity leagueFightmapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueFightmapProvider(zoneId);
            return provider.Insert(leagueFightmapEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LeagueFightmapEntity leagueFightmapEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueFightmapProvider(zoneId);
            return provider.Update(leagueFightmapEntity,trans);
        }
		
		#endregion	
		
		
	}
}

