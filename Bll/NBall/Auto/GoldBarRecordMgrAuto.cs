
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
    /// GoldbarRecord管理类
    /// </summary>
    public static partial class GoldbarRecordMgr
    {
        
		#region  GetById
		
        public static GoldbarRecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new GoldbarRecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<GoldbarRecordEntity> GetAll(string zoneId="")
        {
            var provider = new GoldbarRecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
        
		#region Insert

        public static bool Insert(GoldbarRecordEntity goldbarRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GoldbarRecordProvider(zoneId);
            return provider.Insert(goldbarRecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(GoldbarRecordEntity goldbarRecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new GoldbarRecordProvider(zoneId);
            return provider.Update(goldbarRecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
