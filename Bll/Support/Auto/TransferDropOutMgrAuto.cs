
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
    /// TransferDropout管理类
    /// </summary>
    public static partial class TransferDropoutMgr
    {
        
		#region  GetById
		
        public static TransferDropoutEntity GetById( System.Int32 domaId,string zoneId="")
        {
            var provider = new TransferDropoutProvider(zoneId);
            return provider.GetById( domaId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TransferDropoutEntity> GetAll(string zoneId="")
        {
            var provider = new TransferDropoutProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 domaId,DbTransaction trans=null,string zoneId="")
        {
            TransferDropoutProvider provider = new TransferDropoutProvider(zoneId);

            return provider.Delete( domaId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TransferDropoutEntity transferDropoutEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TransferDropoutProvider(zoneId);
            return provider.Insert(transferDropoutEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TransferDropoutEntity transferDropoutEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TransferDropoutProvider(zoneId);
            return provider.Update(transferDropoutEntity,trans);
        }
		
		#endregion	
		
		
	}
}
