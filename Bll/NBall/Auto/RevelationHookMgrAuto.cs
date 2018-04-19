
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
    /// RevelationHook管理类
    /// </summary>
    public static partial class RevelationHookMgr
    {
        
		#region  GetById
		
        public static RevelationHookEntity GetById( System.Guid hookId,string zoneId="")
        {
            var provider = new RevelationHookProvider(zoneId);
            return provider.GetById( hookId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RevelationHookEntity> GetAll(string zoneId="")
        {
            var provider = new RevelationHookProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetHookList
		
        public static List<RevelationHookEntity> GetHookList( System.DateTime dateTime,string zoneId="")
        {
            var provider = new RevelationHookProvider(zoneId);
            return provider.GetHookList( dateTime);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid hookId,DbTransaction trans=null,string zoneId="")
        {
            RevelationHookProvider provider = new RevelationHookProvider(zoneId);

            return provider.Delete( hookId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RevelationHookEntity revelationHookEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationHookProvider(zoneId);
            return provider.Insert(revelationHookEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RevelationHookEntity revelationHookEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RevelationHookProvider(zoneId);
            return provider.Update(revelationHookEntity,trans);
        }
		
		#endregion	
		
		
	}
}
