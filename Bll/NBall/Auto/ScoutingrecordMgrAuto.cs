
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
    /// ScoutingRecord管理类
    /// </summary>
    public static partial class ScoutingRecordMgr
    {
        
		#region  GetById
		
        public static ScoutingRecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ScoutingRecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ScoutingRecordEntity> GetAll(string zoneId="")
        {
            var provider = new ScoutingRecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(ScoutingRecordEntity scoutingRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ScoutingRecordProvider(zoneId);
            return provider.Insert(scoutingRecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ScoutingRecordEntity scoutingRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ScoutingRecordProvider(zoneId);
            return provider.Update(scoutingRecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

