
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
    /// TeammemberGrow管理类
    /// </summary>
    public static partial class TeammemberGrowMgr
    {
        
		#region  GetById
		
        public static TeammemberGrowEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new TeammemberGrowProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManager
		
        public static List<TeammemberGrowEntity> GetByManager( System.Guid managerId,string zoneId="")
        {
            var provider = new TeammemberGrowProvider(zoneId);
            return provider.GetByManager( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            TeammemberGrowProvider provider = new TeammemberGrowProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TeammemberGrowEntity teammemberGrowEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TeammemberGrowProvider(zoneId);
            return provider.Insert(teammemberGrowEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TeammemberGrowEntity teammemberGrowEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TeammemberGrowProvider(zoneId);
            return provider.Update(teammemberGrowEntity,trans);
        }
		
		#endregion	
		
		
	}
}

