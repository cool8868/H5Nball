
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
    /// ActivityexSendlog管理类
    /// </summary>
    public static partial class ActivityexSendlogMgr
    {
        
		#region  GetById
		
        public static ActivityexSendlogEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ActivityexSendlogProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ActivityexSendlogEntity> GetAll(string zoneId="")
        {
            var provider = new ActivityexSendlogProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ActivityexSendlogProvider provider = new ActivityexSendlogProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Check
		
        public static bool Check ( System.Int32 excitingId, System.Int32 groupId, System.DateTime recordDate,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            ActivityexSendlogProvider provider = new ActivityexSendlogProvider(zoneId);

            return provider.Check( excitingId, groupId, recordDate,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ActivityexSendlogEntity activityexSendlogEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexSendlogProvider(zoneId);
            return provider.Insert(activityexSendlogEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ActivityexSendlogEntity activityexSendlogEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ActivityexSendlogProvider(zoneId);
            return provider.Update(activityexSendlogEntity,trans);
        }
		
		#endregion	
		
		
	}
}

