
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
    /// DailycupGamble管理类
    /// </summary>
    public static partial class DailycupGambleMgr
    {
        
		#region  GetById
		
        public static DailycupGambleEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DailycupGambleProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DailycupGambleEntity> GetAll(string zoneId="")
        {
            var provider = new DailycupGambleProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetMyGamebleData
		
        public static List<DailycupGambleEntity> GetMyGamebleData( System.Int32 dailyCupId, System.Guid managerId,string zoneId="")
        {
            var provider = new DailycupGambleProvider(zoneId);
            return provider.GetMyGamebleData( dailyCupId, managerId);            
        }
		
		#endregion		  
		
		#region  GetGambleByMatchId
		
        public static List<DailycupGambleEntity> GetGambleByMatchId( System.Guid matchId,string zoneId="")
        {
            var provider = new DailycupGambleProvider(zoneId);
            return provider.GetGambleByMatchId( matchId);            
        }
		
		#endregion		  
		
		#region  GambleCheck
		
		public static Int32 GambleCheck ( System.Int32 dailyCupId, System.Guid managerId, System.Guid matchId,string zoneId="")
        {
            var provider = new DailycupGambleProvider(zoneId);
            return provider.GambleCheck( dailyCupId, managerId, matchId);
        }
		
		#endregion		  
		
		#region  GambleNumber
		
		public static Int32 GambleNumber ( System.Int32 dailyCupId, System.Guid managerId,string zoneId="")
        {
            var provider = new DailycupGambleProvider(zoneId);
            return provider.GambleNumber( dailyCupId, managerId);
        }
		
		#endregion		  
		
		#region  UnlockCardCheck
		
        public static bool UnlockCardCheck ( System.Guid itemId,DbTransaction trans=null,string zoneId="")
        {
            DailycupGambleProvider provider = new DailycupGambleProvider(zoneId);

            return provider.UnlockCardCheck( itemId,trans);
            
        }
		
		#endregion
        
		#region  Open
		
        public static bool Open ( System.Guid managerId, System.Int32 idx, System.Int32 resultLevel, System.Int32 status, System.Byte[] itemString, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            DailycupGambleProvider provider = new DailycupGambleProvider(zoneId);

            return provider.Open( managerId, idx, resultLevel, status, itemString, rowVersion,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DailycupGambleEntity dailycupGambleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailycupGambleProvider(zoneId);
            return provider.Insert(dailycupGambleEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DailycupGambleEntity dailycupGambleEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailycupGambleProvider(zoneId);
            return provider.Update(dailycupGambleEntity,trans);
        }
		
		#endregion	
		
		
	}
}

