
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
    /// EuropeRecord管理类
    /// </summary>
    public static partial class EuropeRecordMgr
    {
        
		#region  GetById
		
        public static EuropeRecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new EuropeRecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<EuropeRecordEntity> GetAll(string zoneId="")
        {
            var provider = new EuropeRecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            EuropeRecordProvider provider = new EuropeRecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(EuropeRecordEntity europeRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeRecordProvider(zoneId);
            return provider.Insert(europeRecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(EuropeRecordEntity europeRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new EuropeRecordProvider(zoneId);
            return provider.Update(europeRecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
