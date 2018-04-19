
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
    /// PayChargehistory管理类
    /// </summary>
    public static partial class PayChargehistoryMgr
    {
        
		#region  GetById
		
        public static PayChargehistoryEntity GetById( System.String idx,string zoneId="")
        {
            var provider = new PayChargehistoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<PayChargehistoryEntity> GetAll(string zoneId="")
        {
            var provider = new PayChargehistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByAccount
		
        public static List<PayChargehistoryEntity> GetByAccount( System.String account,string zoneId="")
        {
            var provider = new PayChargehistoryProvider(zoneId);
            return provider.GetByAccount( account);            
        }
		
		#endregion		  
		
		#region  GetForActivity
		
        public static List<PayChargehistoryEntity> GetForActivity( System.String account, System.DateTime startTime, System.DateTime endTime,string zoneId="")
        {
            var provider = new PayChargehistoryProvider(zoneId);
            return provider.GetForActivity( account, startTime, endTime);            
        }
		
		#endregion		  
		
		#region  SelectOrderNum
		
		public static Int32 SelectOrderNum ( System.Guid orderId, System.DateTime sTime, System.DateTime eTime,string zoneId="")
        {
            var provider = new PayChargehistoryProvider(zoneId);
            return provider.SelectOrderNum( orderId, sTime, eTime);
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String idx,DbTransaction trans=null,string zoneId="")
        {
            PayChargehistoryProvider provider = new PayChargehistoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  GetPointForActivity
		
        public static bool GetPointForActivity ( System.String account, System.DateTime startTime, System.DateTime endTime,ref  System.Int32 point,DbTransaction trans=null,string zoneId="")
        {
            PayChargehistoryProvider provider = new PayChargehistoryProvider(zoneId);

            return provider.GetPointForActivity( account, startTime, endTime,ref  point,trans);
            
        }
		
		#endregion
        
		#region  GetPointForActivityNoTime
		
        public static bool GetPointForActivityNoTime ( System.String account,ref  System.Int32 point,DbTransaction trans=null,string zoneId="")
        {
            PayChargehistoryProvider provider = new PayChargehistoryProvider(zoneId);

            return provider.GetPointForActivityNoTime( account,ref  point,trans);
            
        }
		
		#endregion
        
		#region  ChargeCSDK
		
        public static bool ChargeCSDK ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 gamePoint, System.Int32 chargePoint, System.Decimal cash, System.Int32 bonus,ref  System.Int32 result, System.Int32 eqid,DbTransaction trans=null,string zoneId="")
        {
            PayChargehistoryProvider provider = new PayChargehistoryProvider(zoneId);

            return provider.ChargeCSDK( account, sourceType, billingId, gamePoint, chargePoint, cash, bonus,ref  result, eqid,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(PayChargehistoryEntity payChargehistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PayChargehistoryProvider(zoneId);
            return provider.Insert(payChargehistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(PayChargehistoryEntity payChargehistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PayChargehistoryProvider(zoneId);
            return provider.Update(payChargehistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}
