
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
    /// TxLogininfo管理类
    /// </summary>
    public static partial class TxLogininfoMgr
    {
        
		#region  GetById
		
        public static TxLogininfoEntity GetById( System.String openId,string zoneId="")
        {
            var provider = new TxLogininfoProvider(zoneId);
            return provider.GetById( openId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TxLogininfoEntity> GetAll(string zoneId="")
        {
            var provider = new TxLogininfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String openId,DbTransaction trans=null,string zoneId="")
        {
            TxLogininfoProvider provider = new TxLogininfoProvider(zoneId);

            return provider.Delete( openId,trans);
            
        }
		
		#endregion
        
		#region Insert

        public static bool Insert(TxLogininfoEntity txLogininfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TxLogininfoProvider(zoneId);
            return provider.Insert(txLogininfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TxLogininfoEntity txLogininfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TxLogininfoProvider(zoneId);
            return provider.Update(txLogininfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
