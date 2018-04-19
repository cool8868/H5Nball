
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
    /// ExchangeInfo管理类
    /// </summary>
    public static partial class ExchangeInfoMgr
    {
        
		#region  GetById
		
        public static ExchangeInfoEntity GetById( System.String idx)
        {
            var provider = new ExchangeInfoProvider();
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ExchangeInfoEntity> GetAll()
        {
            var provider = new ExchangeInfoProvider();
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            ExchangeInfoProvider provider = new ExchangeInfoProvider();

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  Save
		
        public static bool Save ( System.String idx, System.String platformCode, System.String account, System.Guid managerId, System.Int32 zoneName, System.Int32 packId, System.Byte[] rowVersion, System.Int32 exchangeBatchLimitCode,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            ExchangeInfoProvider provider = new ExchangeInfoProvider();

            return provider.Save( idx, platformCode, account, managerId, zoneName, packId, rowVersion, exchangeBatchLimitCode,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ExchangeInfoEntity exchangeInfoEntity,DbTransaction trans=null)
        {
            var provider = new ExchangeInfoProvider();
            return provider.Insert(exchangeInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ExchangeInfoEntity exchangeInfoEntity,DbTransaction trans=null)
        {
            var provider = new ExchangeInfoProvider();
            return provider.Update(exchangeInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
