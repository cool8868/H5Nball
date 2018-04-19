
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
    /// DailycupInfo管理类
    /// </summary>
    public static partial class DailycupInfoMgr
    {
        
		#region  GetById
		
        public static DailycupInfoEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new DailycupInfoProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByDate
		
        public static DailycupInfoEntity GetByDate( System.DateTime rundate,string zoneId="")
        {
            var provider = new DailycupInfoProvider(zoneId);
            return provider.GetByDate( rundate);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<DailycupInfoEntity> GetAll(string zoneId="")
        {
            var provider = new DailycupInfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetListByTime
		
        public static List<DailycupInfoEntity> GetListByTime( System.DateTime beginTime, System.DateTime endTime,string zoneId="")
        {
            var provider = new DailycupInfoProvider(zoneId);
            return provider.GetListByTime( beginTime, endTime);            
        }
		
		#endregion		  
		
		#region  Create
		
        public static bool Create (DbTransaction trans=null,string zoneId="")
        {
            DailycupInfoProvider provider = new DailycupInfoProvider(zoneId);

            return provider.Create(trans);
            
        }
		
		#endregion
        
		#region  SendPrize
		
        public static bool SendPrize (DbTransaction trans=null,string zoneId="")
        {
            DailycupInfoProvider provider = new DailycupInfoProvider(zoneId);

            return provider.SendPrize(trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(DailycupInfoEntity dailycupInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailycupInfoProvider(zoneId);
            return provider.Insert(dailycupInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(DailycupInfoEntity dailycupInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new DailycupInfoProvider(zoneId);
            return provider.Update(dailycupInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}

