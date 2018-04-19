
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
    /// OlympicRecord管理类
    /// </summary>
    public static partial class OlympicRecordMgr
    {
        
		#region  GetById
		
        public static OlympicRecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new OlympicRecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<OlympicRecordEntity> GetAll(string zoneId="")
        {
            var provider = new OlympicRecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            OlympicRecordProvider provider = new OlympicRecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(OlympicRecordEntity olympicRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new OlympicRecordProvider(zoneId);
            return provider.Insert(olympicRecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(OlympicRecordEntity olympicRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new OlympicRecordProvider(zoneId);
            return provider.Update(olympicRecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
