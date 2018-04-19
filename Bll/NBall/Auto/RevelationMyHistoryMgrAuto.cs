
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
    /// RevelationMyhistory管理类
    /// </summary>
    public static partial class RevelationMyhistoryMgr
    {
        
		#region  GetById
		
        public static RevelationMyhistoryEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new RevelationMyhistoryProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationMyhistoryEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationMyhistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            RevelationMyhistoryProvider provider = new RevelationMyhistoryProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationMyhistoryEntity revelationMyhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationMyhistoryProvider(zoneId);
            return provider.Insert(revelationMyhistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationMyhistoryEntity revelationMyhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationMyhistoryProvider(zoneId);
            return provider.Update(revelationMyhistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}
