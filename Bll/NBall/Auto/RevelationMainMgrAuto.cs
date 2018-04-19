
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
    /// RevelationMain管理类
    /// </summary>
    public static partial class RevelationMainMgr
    {
        
		#region  GetById
		
        public static RevelationMainEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new RevelationMainProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationMainEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationMainProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            RevelationMainProvider provider = new RevelationMainProvider(zoneId);

            return provider.Delete( managerId, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  C_RevelationEverDay
		
        public static bool C_RevelationEverDay ( System.Guid managerid,DbTransaction trans=null,string zoneId="")
        {
            RevelationMainProvider provider = new RevelationMainProvider(zoneId);

            return provider.C_RevelationEverDay( managerid,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationMainEntity revelationMainEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationMainProvider(zoneId);
            return provider.Insert(revelationMainEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationMainEntity revelationMainEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationMainProvider(zoneId);
            return provider.Update(revelationMainEntity,trans);
        }
		
		#endregion	
		
		
	}
}

