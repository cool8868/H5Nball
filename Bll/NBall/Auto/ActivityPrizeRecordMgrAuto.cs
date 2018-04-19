
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
    /// ActivityPrizerecord管理类
    /// </summary>
    public static partial class ActivityPrizerecordMgr
    {
        
		#region  GetById
		
        public static ActivityPrizerecordEntity GetById( System.Int64 idx,string zoneId="")
        {
            var provider = new ActivityPrizerecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityPrizerecordEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityPrizerecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int64 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityPrizerecordProvider provider = new ActivityPrizerecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityPrizerecordEntity activityPrizerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityPrizerecordProvider(zoneId);
            return provider.Insert(activityPrizerecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityPrizerecordEntity activityPrizerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityPrizerecordProvider(zoneId);
            return provider.Update(activityPrizerecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

