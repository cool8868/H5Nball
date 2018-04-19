
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
    /// GambleHost管理类
    /// </summary>
    public static partial class GambleHostMgr
    {
        
		#region  GetByManagerIdAndTitleId
		
        public static GambleHostEntity GetByManagerIdAndTitleId( System.Guid managerId, System.Guid titleId,string zoneId="")
        {
            var provider = new GambleHostProvider(zoneId);
            return provider.GetByManagerIdAndTitleId( managerId, titleId);
        }
		
		#endregion		  
		
		#region  GetById
		
        public static GambleHostEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new GambleHostProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByTitleId
		
        public static List<GambleHostEntity> GetByTitleId( System.Guid titleId,string zoneId="")
        {
            var provider = new GambleHostProvider(zoneId);
            return provider.GetByTitleId( titleId);            
        }
		
		#endregion		  
		
		#region  GetByManagerId
		
        public static List<GambleHostEntity> GetByManagerId( System.Guid managerId,string zoneId="")
        {
            var provider = new GambleHostProvider(zoneId);
            return provider.GetByManagerId( managerId);            
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GambleHostEntity> GetAll(string zoneId="")
        {
            var provider = new GambleHostProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  AddAttendCount
		
        public static bool AddAttendCount ( System.Int32 hostId,DbTransaction trans=null,string zoneId="")
        {
            GambleHostProvider provider = new GambleHostProvider(zoneId);

            return provider.AddAttendCount( hostId,trans);
            
        }
		
		#endregion
        
		#region  InsertOnce
		
        public static bool InsertOnce ( System.Guid managerId, System.String managerName, System.Guid titleId, System.Int32 hostMoney, System.Int32 totalMoney, System.DateTime rowTime,ref  System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            GambleHostProvider provider = new GambleHostProvider(zoneId);

            return provider.InsertOnce( managerId, managerName, titleId, hostMoney, totalMoney, rowTime,ref  idx,trans);
            
        }
		
		#endregion
        
		#region  Delete
		
        public static bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            GambleHostProvider provider = new GambleHostProvider(zoneId);

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(GambleHostEntity gambleHostEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleHostProvider(zoneId);
            return provider.Insert(gambleHostEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GambleHostEntity gambleHostEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GambleHostProvider(zoneId);
            return provider.Update(gambleHostEntity,trans);
        }
		
		#endregion	
		
		
	}
}
