
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
    /// ShareManager管理类
    /// </summary>
    public static partial class ShareManagerMgr
    {
        
		#region  GetById
		
        public static ShareManagerEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ShareManagerProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
        public static ShareManagerEntity GetByManagerId( System.Guid managerId, System.Int32 shareType,string zoneId="")
        {
            var provider = new ShareManagerProvider(zoneId);
            return provider.GetByManagerId( managerId, shareType);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ShareManagerEntity> GetAll(string zoneId="")
        {
            var provider = new ShareManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManagerList
		
        public static List<ShareManagerEntity> GetByManagerList( System.Guid managerId,string zoneId="")
        {
            var provider = new ShareManagerProvider(zoneId);
            return provider.GetByManagerList( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ShareManagerProvider provider = new ShareManagerProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ShareManagerEntity shareManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ShareManagerProvider(zoneId);
            return provider.Insert(shareManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ShareManagerEntity shareManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ShareManagerProvider(zoneId);
            return provider.Update(shareManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
