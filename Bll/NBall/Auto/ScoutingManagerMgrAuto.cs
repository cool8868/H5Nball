
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
    /// ScoutingManager管理类
    /// </summary>
    public static partial class ScoutingManagerMgr
    {
        
		#region  GetById
		
        public static ScoutingManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ScoutingManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ScoutingManagerEntity> GetAll(string zoneId="")
        {
            var provider = new ScoutingManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            ScoutingManagerProvider provider = new ScoutingManagerProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ScoutingManagerEntity scoutingManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ScoutingManagerProvider(zoneId);
            return provider.Insert(scoutingManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ScoutingManagerEntity scoutingManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ScoutingManagerProvider(zoneId);
            return provider.Update(scoutingManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
