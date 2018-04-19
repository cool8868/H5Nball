
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
    /// ActivityexCrossrank管理类
    /// </summary>
    public static partial class ActivityexCrossrankMgr
    {
        
		#region  GetById
		
        public static ActivityexCrossrankEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityexCrossrankProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerCrossRankKey
		
        public static ActivityexCrossrankEntity GetByManagerCrossRankKey( System.Guid managerId, System.String rankKey,string zoneId="")
        {
            var provider = new ActivityexCrossrankProvider(zoneId);
            return provider.GetByManagerCrossRankKey( managerId, rankKey);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityexCrossrankEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityexCrossrankProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetCrossRank
		
        public static List<ActivityexCrossrankEntity> GetCrossRank( System.String rankKey, System.Int32 rankCondition, System.Int32 number, System.Int32 domainId, System.String siteId, System.Guid managerId,ref  System.Int32 myData,string zoneId="")
        {
            var provider = new ActivityexCrossrankProvider(zoneId);
            return provider.GetCrossRank( rankKey, rankCondition, number, domainId, siteId, managerId,ref  myData);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityexCrossrankProvider provider = new ActivityexCrossrankProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityexCrossrankEntity activityexCrossrankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexCrossrankProvider(zoneId);
            return provider.Insert(activityexCrossrankEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityexCrossrankEntity activityexCrossrankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexCrossrankProvider(zoneId);
            return provider.Update(activityexCrossrankEntity,trans);
        }
		
		#endregion	
		
		
	}
}

