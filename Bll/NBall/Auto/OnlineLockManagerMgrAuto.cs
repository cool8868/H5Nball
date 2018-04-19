
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
    /// OnlineLockmanager管理类
    /// </summary>
    public static partial class OnlineLockmanagerMgr
    {
        
		#region  GetById
		
        public static OnlineLockmanagerEntity GetById( System.Int64 id,string zoneId="")
        {
            var provider = new OnlineLockmanagerProvider(zoneId);
            return provider.GetById( id);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<OnlineLockmanagerEntity> GetAll(string zoneId="")
        {
            var provider = new OnlineLockmanagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int64 id,DbTransaction trans=null,string zoneId="")
        {
            OnlineLockmanagerProvider provider = new OnlineLockmanagerProvider(zoneId);

            return provider.Delete( id,trans);
            
        }
		
		#endregion
        
		#region  CheckLock
		
        public static bool CheckLock ( System.Guid managerId,ref  System.Boolean lockFlag,ref  System.DateTime lockDate,ref  System.DateTime breakDate,DbTransaction trans=null,string zoneId="")
        {
            OnlineLockmanagerProvider provider = new OnlineLockmanagerProvider(zoneId);

            return provider.CheckLock( managerId,ref  lockFlag,ref  lockDate,ref  breakDate,trans);
            
        }
		
		#endregion
        
		#region  Lock
		
        public static bool Lock ( System.Guid managerId, System.Int32 lockType, System.DateTime preBreakDate, System.String lockOperator, System.String memo,DbTransaction trans=null,string zoneId="")
        {
            OnlineLockmanagerProvider provider = new OnlineLockmanagerProvider(zoneId);

            return provider.Lock( managerId, lockType, preBreakDate, lockOperator, memo,trans);
            
        }
		
		#endregion
        
		#region  BreakLock
		
        public static bool BreakLock ( System.Guid managerId, System.String breakOperator, System.String memo,DbTransaction trans=null,string zoneId="")
        {
            OnlineLockmanagerProvider provider = new OnlineLockmanagerProvider(zoneId);

            return provider.BreakLock( managerId, breakOperator, memo,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(OnlineLockmanagerEntity onlineLockmanagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new OnlineLockmanagerProvider(zoneId);
            return provider.Insert(onlineLockmanagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(OnlineLockmanagerEntity onlineLockmanagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new OnlineLockmanagerProvider(zoneId);
            return provider.Update(onlineLockmanagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}

