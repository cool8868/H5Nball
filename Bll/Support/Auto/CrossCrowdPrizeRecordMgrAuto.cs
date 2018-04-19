
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
    /// CrosscrowdPrizerecord管理类
    /// </summary>
    public static partial class CrosscrowdPrizerecordMgr
    {
        
		#region  GetById
		
        public static CrosscrowdPrizerecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new CrosscrowdPrizerecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrosscrowdPrizerecordEntity> GetAll(string zoneId="")
        {
            var provider = new CrosscrowdPrizerecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            CrosscrowdPrizerecordProvider provider = new CrosscrowdPrizerecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrosscrowdPrizerecordEntity crosscrowdPrizerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdPrizerecordProvider(zoneId);
            return provider.Insert(crosscrowdPrizerecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrosscrowdPrizerecordEntity crosscrowdPrizerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrosscrowdPrizerecordProvider(zoneId);
            return provider.Update(crosscrowdPrizerecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}
