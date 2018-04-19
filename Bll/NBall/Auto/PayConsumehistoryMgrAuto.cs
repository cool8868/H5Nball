
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
    /// PayConsumehistory管理类
    /// </summary>
    public static partial class PayConsumehistoryMgr
    {
        
		#region  GetById
		
        public static PayConsumehistoryEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new PayConsumehistoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<PayConsumehistoryEntity> GetAll(string zoneId="")
        {
            var provider = new PayConsumehistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByAccount
		
        public static List<PayConsumehistoryEntity> GetByAccount( System.String account,string zoneId="")
        {
            var provider = new PayConsumehistoryProvider(zoneId);
            return provider.GetByAccount( account);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            PayConsumehistoryProvider provider = new PayConsumehistoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  GetPointForActivity
		
        public static bool GetPointForActivity ( System.String account, System.DateTime startTime, System.DateTime endTime,ref  System.Int32 point,DbTransaction trans=null,string zoneId="")
        {
            PayConsumehistoryProvider provider = new PayConsumehistoryProvider(zoneId);

            return provider.GetPointForActivity( account, startTime, endTime,ref  point,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(PayConsumehistoryEntity payConsumehistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PayConsumehistoryProvider(zoneId);
            return provider.Insert(payConsumehistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(PayConsumehistoryEntity payConsumehistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PayConsumehistoryProvider(zoneId);
            return provider.Update(payConsumehistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}
