
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
    /// CrossladderManagerdailyhistory管理类
    /// </summary>
    public static partial class CrossladderManagerdailyhistoryMgr
    {
        
		#region  GetById
		
        public static CrossladderManagerdailyhistoryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrossladderManagerdailyhistoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossladderManagerdailyhistoryEntity> GetAll(string zoneId="")
        {
            var provider = new CrossladderManagerdailyhistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetDailyPrizeManager
		
        public static List<CrossladderManagerdailyhistoryEntity> GetDailyPrizeManager( System.DateTime recordDate, System.Int32 domainId,string zoneId="")
        {
            var provider = new CrossladderManagerdailyhistoryProvider(zoneId);
            return provider.GetDailyPrizeManager( recordDate, domainId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrossladderManagerdailyhistoryProvider provider = new CrossladderManagerdailyhistoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  SaveDailyPrize
		
        public static bool SaveDailyPrize ( System.Int32 idx, System.String prizeItems,DbTransaction trans=null,string zoneId="")
        {
            CrossladderManagerdailyhistoryProvider provider = new CrossladderManagerdailyhistoryProvider(zoneId);

            return provider.SaveDailyPrize( idx, prizeItems,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossladderManagerdailyhistoryEntity crossladderManagerdailyhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderManagerdailyhistoryProvider(zoneId);
            return provider.Insert(crossladderManagerdailyhistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossladderManagerdailyhistoryEntity crossladderManagerdailyhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderManagerdailyhistoryProvider(zoneId);
            return provider.Update(crossladderManagerdailyhistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}
