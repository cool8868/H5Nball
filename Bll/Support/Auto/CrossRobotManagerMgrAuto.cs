
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
    /// CrossrobotManager管理类
    /// </summary>
    public static partial class CrossrobotManagerMgr
    {
        
		#region  GetById
		
        public static CrossrobotManagerEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new CrossrobotManagerProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossrobotManagerEntity> GetAll(string zoneId="")
        {
            var provider = new CrossrobotManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetCrossCrowd
		
        public static List<CrossrobotManagerEntity> GetCrossCrowd(string zoneId="")
        {
            var provider = new CrossrobotManagerProvider(zoneId);
            return provider.GetCrossCrowd();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            CrossrobotManagerProvider provider = new CrossrobotManagerProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  UpdateStatus
		
        public static bool UpdateStatus ( System.Guid managerId, System.Int32 status,DbTransaction trans=null,string zoneId="")
        {
            CrossrobotManagerProvider provider = new CrossrobotManagerProvider(zoneId);

            return provider.UpdateStatus( managerId, status,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossrobotManagerEntity crossrobotManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossrobotManagerProvider(zoneId);
            return provider.Insert(crossrobotManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossrobotManagerEntity crossrobotManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossrobotManagerProvider(zoneId);
            return provider.Update(crossrobotManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
