
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
    /// CrossladderHook管理类
    /// </summary>
    public static partial class CrossladderHookMgr
    {
        
		#region  GetById
		
        public static CrossladderHookEntity GetById( System.Guid managerId, System.String siteId,string zoneId="")
        {
            var provider = new CrossladderHookProvider(zoneId);
            return provider.GetById( managerId, siteId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossladderHookEntity> GetAll(string zoneId="")
        {
            var provider = new CrossladderHookProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            CrossladderHookProvider provider = new CrossladderHookProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
		#region  End
		
        public static bool End ( System.Guid managerId, System.Int32 status,DbTransaction trans=null,string zoneId="")
        {
            CrossladderHookProvider provider = new CrossladderHookProvider(zoneId);

            return provider.End( managerId, status,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossladderHookEntity crossladderHookEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderHookProvider(zoneId);
            return provider.Insert(crossladderHookEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossladderHookEntity crossladderHookEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderHookProvider(zoneId);
            return provider.Update(crossladderHookEntity,trans);
        }
		
		#endregion	
		
		
	}
}
