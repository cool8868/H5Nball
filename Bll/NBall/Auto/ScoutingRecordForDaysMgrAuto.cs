
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
    /// ScoutingRecordfordays管理类
    /// </summary>
    public static partial class ScoutingRecordfordaysMgr
    {
        
		#region  GetById
		
        public static ScoutingRecordfordaysEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ScoutingRecordfordaysProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ScoutingRecordfordaysEntity> GetAll(string zoneId="")
        {
            var provider = new ScoutingRecordfordaysProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetCountByTime
		
        public static bool GetCountByTime ( System.Guid managerId, System.DateTime startTime, System.DateTime endTime, System.Int32 scoutingType,ref  System.Int32 count,DbTransaction trans=null,string zoneId="")
        {
            ScoutingRecordfordaysProvider provider = new ScoutingRecordfordaysProvider(zoneId);

            return provider.GetCountByTime( managerId, startTime, endTime, scoutingType,ref  count,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ScoutingRecordfordaysEntity scoutingRecordfordaysEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ScoutingRecordfordaysProvider(zoneId);
            return provider.Insert(scoutingRecordfordaysEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ScoutingRecordfordaysEntity scoutingRecordfordaysEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ScoutingRecordfordaysProvider(zoneId);
            return provider.Update(scoutingRecordfordaysEntity,trans);
        }
		
		#endregion	
		
		
	}
}
