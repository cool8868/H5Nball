
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
    /// CrosscrowdManager管理类
    /// </summary>
    public static partial class CrosscrowdManagerMgr
    {
        
		#region  GetById
		
        public static CrosscrowdManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new CrosscrowdManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrosscrowdManagerEntity> GetAll(string zoneId="")
        {
            var provider = new CrosscrowdManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            CrosscrowdManagerProvider provider = new CrosscrowdManagerProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrosscrowdManagerEntity crosscrowdManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdManagerProvider(zoneId);
            return provider.Insert(crosscrowdManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrosscrowdManagerEntity crosscrowdManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdManagerProvider(zoneId);
            return provider.Update(crosscrowdManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
