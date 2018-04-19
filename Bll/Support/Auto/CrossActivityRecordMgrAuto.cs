
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
    /// CrossactivityRecord管理类
    /// </summary>
    public static partial class CrossactivityRecordMgr
    {
        
		#region  GetById
		
        public static CrossactivityRecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrossactivityRecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossactivityRecordEntity> GetAll(string zoneId="")
        {
            var provider = new CrossactivityRecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetActivityNumber
		
		public static Int32 GetActivityNumber ( System.Guid managerId, System.DateTime date,string zoneId="")
        {
            var provider = new CrossactivityRecordProvider(zoneId);
            return provider.GetActivityNumber( managerId, date);
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrossactivityRecordProvider provider = new CrossactivityRecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossactivityRecordEntity crossactivityRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossactivityRecordProvider(zoneId);
            return provider.Insert(crossactivityRecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossactivityRecordEntity crossactivityRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossactivityRecordProvider(zoneId);
            return provider.Update(crossactivityRecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
