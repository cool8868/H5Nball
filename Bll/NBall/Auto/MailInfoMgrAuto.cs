
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
    /// MailInfo管理类
    /// </summary>
    public static partial class MailInfoMgr
    {
        
		#region  GetById
		
        public static MailInfoEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new MailInfoProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<MailInfoEntity> GetAll(string zoneId="")
        {
            var provider = new MailInfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManager
		
        public static List<MailInfoEntity> GetByManager( System.Guid managerId,ref  System.Int32 totalCount,string zoneId="")
        {
            var provider = new MailInfoProvider(zoneId);
            return provider.GetByManager( managerId,ref  totalCount);            
        }
		
		#endregion		  
		
		#region  GetForAttachmentBatch
		
        public static List<MailInfoEntity> GetForAttachmentBatch( System.Guid managerId,string zoneId="")
        {
            var provider = new MailInfoProvider(zoneId);
            return provider.GetForAttachmentBatch( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId, System.String recordIds,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            MailInfoProvider provider = new MailInfoProvider(zoneId);

            return provider.Delete( managerId, recordIds,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  Read
		
        public static bool Read ( System.Guid managerId, System.Int32 recordId,DbTransaction trans=null,string zoneId="")
        {
            MailInfoProvider provider = new MailInfoProvider(zoneId);

            return provider.Read( managerId, recordId,trans);
            
        }
		
		#endregion
        
		#region  ClearExpired
		
        public static bool ClearExpired ( System.DateTime curTime,DbTransaction trans=null,string zoneId="")
        {
            MailInfoProvider provider = new MailInfoProvider(zoneId);

            return provider.ClearExpired( curTime,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(MailInfoEntity mailInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MailInfoProvider(zoneId);
            return provider.Insert(mailInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(MailInfoEntity mailInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new MailInfoProvider(zoneId);
            return provider.Update(mailInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}

