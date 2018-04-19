
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
    /// CrossladderManagerhistory管理类
    /// </summary>
    public static partial class CrossladderManagerhistoryMgr
    {
        
		#region  GetById
		
        public static CrossladderManagerhistoryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrossladderManagerhistoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossladderManagerhistoryEntity> GetAll(string zoneId="")
        {
            var provider = new CrossladderManagerhistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetPrizeManager
		
        public static List<CrossladderManagerhistoryEntity> GetPrizeManager( System.Int32 seasonId, System.Int32 domainId,string zoneId="")
        {
            var provider = new CrossladderManagerhistoryProvider(zoneId);
            return provider.GetPrizeManager( seasonId, domainId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrossladderManagerhistoryProvider provider = new CrossladderManagerhistoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  SavePrize
		
        public static bool SavePrize ( System.Int32 idx, System.String prizeItems,DbTransaction trans=null,string zoneId="")
        {
            CrossladderManagerhistoryProvider provider = new CrossladderManagerhistoryProvider(zoneId);

            return provider.SavePrize( idx, prizeItems,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossladderManagerhistoryEntity crossladderManagerhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderManagerhistoryProvider(zoneId);
            return provider.Insert(crossladderManagerhistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossladderManagerhistoryEntity crossladderManagerhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderManagerhistoryProvider(zoneId);
            return provider.Update(crossladderManagerhistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}
