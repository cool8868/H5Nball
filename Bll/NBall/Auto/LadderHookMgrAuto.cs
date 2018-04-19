
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
    /// LadderHook管理类
    /// </summary>
    public static partial class LadderHookMgr
    {
        
		#region  GetById
		
        public static LadderHookEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new LadderHookProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LadderHookEntity> GetAll(string zoneId="")
        {
            var provider = new LadderHookProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            LadderHookProvider provider = new LadderHookProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
		#region  End
		
        public static bool End ( System.Guid managerId, System.Int32 status,DbTransaction trans=null,string zoneId="")
        {
            LadderHookProvider provider = new LadderHookProvider(zoneId);

            return provider.End( managerId, status,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LadderHookEntity ladderHookEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderHookProvider(zoneId);
            return provider.Insert(ladderHookEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LadderHookEntity ladderHookEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderHookProvider(zoneId);
            return provider.Update(ladderHookEntity,trans);
        }
		
		#endregion	
		
		
	}
}
