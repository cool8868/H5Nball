
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
    /// ActivityexRank管理类
    /// </summary>
    public static partial class ActivityexRankMgr
    {
        
		#region  GetById
		
        public static ActivityexRankEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityexRankProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerRankKey
		
        public static ActivityexRankEntity GetByManagerRankKey( System.Guid managerId, System.String rankKey,string zoneId="")
        {
            var provider = new ActivityexRankProvider(zoneId);
            return provider.GetByManagerRankKey( managerId, rankKey);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityexRankEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityexRankProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetRank
		
        public static List<ActivityexRankEntity> GetRank( System.String rankKey, System.Int32 rankCondition, System.Int32 number, System.Guid managerId,ref  System.Int32 myData,string zoneId="")
        {
            var provider = new ActivityexRankProvider(zoneId);
            return provider.GetRank( rankKey, rankCondition, number, managerId,ref  myData);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityexRankProvider provider = new ActivityexRankProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityexRankEntity activityexRankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexRankProvider(zoneId);
            return provider.Insert(activityexRankEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityexRankEntity activityexRankEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexRankProvider(zoneId);
            return provider.Update(activityexRankEntity,trans);
        }
		
		#endregion	
		
		
	}
}

