
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
    /// MailshareInfo管理类
    /// </summary>
    public static partial class MailshareInfoMgr
    {
        
		#region  GetById
		
        public static MailshareInfoEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new MailshareInfoProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<MailshareInfoEntity> GetAll(string zoneId="")
        {
            var provider = new MailshareInfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByPage
		
        public static List<MailshareInfoEntity> GetByPage( System.String account, System.Int32 pageIndex, System.Int32 pageSize,ref  System.Int32 totalCount,string zoneId="")
        {
            var provider = new MailshareInfoProvider(zoneId);
            return provider.GetByPage( account, pageIndex, pageSize,ref  totalCount);            
        }
		
		#endregion		  
		
		#region  GetForAttachmentBatch
		
        public static List<MailshareInfoEntity> GetForAttachmentBatch( System.String account,string zoneId="")
        {
            var provider = new MailshareInfoProvider(zoneId);
            return provider.GetForAttachmentBatch( account);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String account, System.String recordIds,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            MailshareInfoProvider provider = new MailshareInfoProvider(zoneId);

            return provider.Delete( account, recordIds,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  Read
		
        public static bool Read ( System.String account, System.Int32 recordId,DbTransaction trans=null,string zoneId="")
        {
            MailshareInfoProvider provider = new MailshareInfoProvider(zoneId);

            return provider.Read( account, recordId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(MailshareInfoEntity mailshareInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MailshareInfoProvider(zoneId);
            return provider.Insert(mailshareInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(MailshareInfoEntity mailshareInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MailshareInfoProvider(zoneId);
            return provider.Update(mailshareInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}

