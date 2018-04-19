
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
    /// RevelationInfo管理类
    /// </summary>
    public static partial class RevelationInfoMgr
    {
        
		#region  GetById
		
        public static RevelationInfoEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new RevelationInfoProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationInfoEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationInfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            RevelationInfoProvider provider = new RevelationInfoProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationInfoEntity revelationInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationInfoProvider(zoneId);
            return provider.Insert(revelationInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationInfoEntity revelationInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationInfoProvider(zoneId);
            return provider.Update(revelationInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
