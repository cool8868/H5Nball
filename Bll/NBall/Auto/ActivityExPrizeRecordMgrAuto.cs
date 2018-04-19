
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
    /// ActivityexPrizerecord管理类
    /// </summary>
    public static partial class ActivityexPrizerecordMgr
    {
        
		#region  GetById
		
        public static ActivityexPrizerecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityexPrizerecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetPrize
		
        public static ActivityexPrizerecordEntity GetPrize( System.String exKey,string zoneId="")
        {
            var provider = new ActivityexPrizerecordProvider(zoneId);
            return provider.GetPrize( exKey);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityexPrizerecordEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityexPrizerecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetForPrize
		
        public static List<ActivityexPrizerecordEntity> GetForPrize(string zoneId="")
        {
            var provider = new ActivityexPrizerecordProvider(zoneId);
            return provider.GetForPrize();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityexPrizerecordProvider provider = new ActivityexPrizerecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityexPrizerecordEntity activityexPrizerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexPrizerecordProvider(zoneId);
            return provider.Insert(activityexPrizerecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityexPrizerecordEntity activityexPrizerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexPrizerecordProvider(zoneId);
            return provider.Update(activityexPrizerecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

