
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
    /// TransferMain管理类
    /// </summary>
    public static partial class TransferMainMgr
    {
        
		#region  GetById
		
        public static TransferMainEntity GetById( System.Guid transferId,string zoneId="")
        {
            var provider = new TransferMainProvider(zoneId);
            return provider.GetById( transferId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TransferMainEntity> GetAll(string zoneId="")
        {
            var provider = new TransferMainProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetTransferList
		
        public static List<TransferMainEntity> GetTransferList( System.Int32 domainId,string zoneId="")
        {
            var provider = new TransferMainProvider(zoneId);
            return provider.GetTransferList( domainId);            
        }
		
		#endregion		  
		
		#region  GetTransferNumber
		
		public static Int32 GetTransferNumber ( System.Guid managerId,string zoneId="")
        {
            var provider = new TransferMainProvider(zoneId);
            return provider.GetTransferNumber( managerId);
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid transferId,DbTransaction trans=null,string zoneId="")
        {
            TransferMainProvider provider = new TransferMainProvider(zoneId);

            return provider.Delete( transferId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TransferMainEntity transferMainEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TransferMainProvider(zoneId);
            return provider.Insert(transferMainEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TransferMainEntity transferMainEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TransferMainProvider(zoneId);
            return provider.Update(transferMainEntity,trans);
        }
		
		#endregion	
		
		
	}
}
