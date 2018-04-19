
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
    /// PayContinuing管理类
    /// </summary>
    public static partial class PayContinuingMgr
    {
        
		#region  GetById
		
        public static PayContinuingEntity GetById( System.String account,string zoneId="")
        {
            var provider = new PayContinuingProvider(zoneId);
            return provider.GetById( account);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<PayContinuingEntity> GetAll(string zoneId="")
        {
            var provider = new PayContinuingProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String account,DbTransaction trans=null,string zoneId="")
        {
            PayContinuingProvider provider = new PayContinuingProvider(zoneId);

            return provider.Delete( account,trans);
            
        }
		
		#endregion
        
		#region  UpdateContinueday
		
        public static bool UpdateContinueday ( System.String account, System.Int32 curPoint, System.Int32 needPoint, System.DateTime yesterday, System.DateTime today, System.DateTime curTime,DbTransaction trans=null,string zoneId="")
        {
            PayContinuingProvider provider = new PayContinuingProvider(zoneId);

            return provider.UpdateContinueday( account, curPoint, needPoint, yesterday, today, curTime,trans);
            
        }
		
		#endregion
        
		#region  ContinueReset
		
        public static bool ContinueReset ( System.String account, System.DateTime today,DbTransaction trans=null,string zoneId="")
        {
            PayContinuingProvider provider = new PayContinuingProvider(zoneId);

            return provider.ContinueReset( account, today,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(PayContinuingEntity payContinuingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PayContinuingProvider(zoneId);
            return provider.Insert(payContinuingEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(PayContinuingEntity payContinuingEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PayContinuingProvider(zoneId);
            return provider.Update(payContinuingEntity,trans);
        }
		
		#endregion	
		
		
	}
}
