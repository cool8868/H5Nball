
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
    /// ScoutingGoldbar管理类
    /// </summary>
    public static partial class ScoutingGoldbarMgr
    {
        
		#region  GetById
		
        public static ScoutingGoldbarEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ScoutingGoldbarProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ScoutingGoldbarEntity> GetAll(string zoneId="")
        {
            var provider = new ScoutingGoldbarProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            ScoutingGoldbarProvider provider = new ScoutingGoldbarProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ScoutingGoldbarEntity scoutingGoldbarEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ScoutingGoldbarProvider(zoneId);
            return provider.Insert(scoutingGoldbarEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ScoutingGoldbarEntity scoutingGoldbarEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ScoutingGoldbarProvider(zoneId);
            return provider.Update(scoutingGoldbarEntity,trans);
        }
		
		#endregion	
		
		
	}
}
