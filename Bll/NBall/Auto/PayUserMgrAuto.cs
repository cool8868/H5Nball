
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
    /// PayUser管理类
    /// </summary>
    public static partial class PayUserMgr
    {
        
		#region  GetById
		
        public static PayUserEntity GetById( System.String account,string zoneId="")
        {
            var provider = new PayUserProvider(zoneId);
            return provider.GetById( account);
        }
		
		#endregion		  
		
		#region  GetPointByManagerId
		
        public static PayUserEntity GetPointByManagerId( System.Guid managerId,string zoneId="")
        {
            var provider = new PayUserProvider(zoneId);
            return provider.GetPointByManagerId( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<PayUserEntity> GetAll(string zoneId="")
        {
            var provider = new PayUserProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Charge
		
        public static bool Charge ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 gamePoint, System.Int32 chargePoint, System.Decimal cash, System.Int32 bonus,ref  System.Int32 result,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.Charge( account, sourceType, billingId, gamePoint, chargePoint, cash, bonus,ref  result,trans);
            
        }
		
		#endregion
        
		#region  ConsumePoint
		
        public static bool ConsumePoint ( System.String account, System.Guid managerId, System.Int32 sourceType, System.String sourceId, System.Int32 consumePoint, System.DateTime consumeTime, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.ConsumePoint( account, managerId, sourceType, sourceId, consumePoint, consumeTime, rowVersion,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  ConsumePointForGamble
		
        public static bool ConsumePointForGamble ( System.String account, System.Guid managerId, System.Int32 sourceType, System.String sourceId, System.Int32 consumePoint, System.DateTime consumeTime,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.ConsumePointForGamble( account, managerId, sourceType, sourceId, consumePoint, consumeTime,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  Stat
		
        public static bool Stat ( System.String account,ref  System.Int32 cash,ref  System.Int32 point,ref  System.Int32 bonus,ref  System.Int32 cPoint,ref  System.Int32 cBonus,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.Stat( account,ref  cash,ref  point,ref  bonus,ref  cPoint,ref  cBonus,trans);
            
        }
		
		#endregion
        
		#region  ChargeTest
		
        public static bool ChargeTest ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 gamePoint, System.Int32 cash, System.Int32 bonus, System.DateTime curTime,ref  System.Int32 result,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.ChargeTest( account, sourceType, billingId, gamePoint, cash, bonus, curTime,ref  result,trans);
            
        }
		
		#endregion
        
		#region  GetGmChargePoint
		
        public static bool GetGmChargePoint ( System.String account,ref  System.Int32 totalPoint,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.GetGmChargePoint( account,ref  totalPoint,trans);
            
        }
		
		#endregion
        
		#region  ChargeForBonus
		
        public static bool ChargeForBonus ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 bonus,ref  System.Int32 result,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.ChargeForBonus( account, sourceType, billingId, bonus,ref  result,trans);
            
        }
		
		#endregion
        
		#region  GetGmChargePointByTime
		
        public static bool GetGmChargePointByTime ( System.String account, System.DateTime startTime, System.DateTime endTime,ref  System.Int32 totalPoint,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.GetGmChargePointByTime( account, startTime, endTime,ref  totalPoint,trans);
            
        }
		
		#endregion
        
		#region  ChargeForBindPoint
		
        public static bool ChargeForBindPoint ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 bindPoint,ref  System.Int32 result,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.ChargeForBindPoint( account, sourceType, billingId, bindPoint,ref  result,trans);
            
        }
		
		#endregion
        
		#region  ConsumeBindPoint
		
        public static bool ConsumeBindPoint ( System.String account, System.Guid managerId, System.Int32 sourceType, System.String sourceId, System.Int32 consumeBindPoint, System.DateTime consumeTime,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.ConsumeBindPoint( account, managerId, sourceType, sourceId, consumeBindPoint, consumeTime,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  ChargeForPoint
		
        public static bool ChargeForPoint ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 point, System.Int32 bonus,ref  System.Int32 result,DbTransaction trans=null,string zoneId="")
        {
            PayUserProvider provider = new PayUserProvider(zoneId);

            return provider.ChargeForPoint( account, sourceType, billingId, point, bonus,ref  result,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(PayUserEntity payUserEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PayUserProvider(zoneId);
            return provider.Insert(payUserEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(PayUserEntity payUserEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PayUserProvider(zoneId);
            return provider.Update(payUserEntity,trans);
        }
		
		#endregion	
		
		
	}
}
