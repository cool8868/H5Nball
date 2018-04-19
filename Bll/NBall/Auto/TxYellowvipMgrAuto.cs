
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
    /// TxYellowvip管理类
    /// </summary>
    public static partial class TxYellowvipMgr
    {
        
		#region  GetById
		
        public static TxYellowvipEntity GetById( System.String account,string zoneId="")
        {
            var provider = new TxYellowvipProvider(zoneId);
            return provider.GetById( account);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TxYellowvipEntity> GetAll(string zoneId="")
        {
            var provider = new TxYellowvipProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String account,DbTransaction trans=null,string zoneId="")
        {
            TxYellowvipProvider provider = new TxYellowvipProvider(zoneId);

            return provider.Delete( account,trans);
            
        }
		
		#endregion
        
		#region  Add
		
        public static bool Add ( System.String account, System.Boolean isYellowVip, System.Boolean isYellowYearVip, System.Boolean isYellowHighVip, System.Int32 yellowVipLevel, System.String extraData,DbTransaction trans=null,string zoneId="")
        {
            TxYellowvipProvider provider = new TxYellowvipProvider(zoneId);

            return provider.Add( account, isYellowVip, isYellowYearVip, isYellowHighVip, yellowVipLevel, extraData,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TxYellowvipEntity txYellowvipEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TxYellowvipProvider(zoneId);
            return provider.Insert(txYellowvipEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TxYellowvipEntity txYellowvipEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TxYellowvipProvider(zoneId);
            return provider.Update(txYellowvipEntity,trans);
        }
		
		#endregion	
		
		
	}
}
