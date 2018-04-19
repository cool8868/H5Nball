
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
    /// LadderManagerhistory管理类
    /// </summary>
    public static partial class LadderManagerhistoryMgr
    {
        
		#region  GetById
		
        public static LadderManagerhistoryEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new LadderManagerhistoryProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LadderManagerhistoryEntity> GetAll(string zoneId="")
        {
            var provider = new LadderManagerhistoryProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetPrizeManager
		
        public static List<LadderManagerhistoryEntity> GetPrizeManager( System.Int32 seasonId,string zoneId="")
        {
            var provider = new LadderManagerhistoryProvider(zoneId);
            return provider.GetPrizeManager( seasonId);            
        }
		
		#endregion		  
		
		#region  GetPrizeManagerAll
		
        public static List<LadderManagerhistoryEntity> GetPrizeManagerAll( System.Int32 seasonId,string zoneId="")
        {
            var provider = new LadderManagerhistoryProvider(zoneId);
            return provider.GetPrizeManagerAll( seasonId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            LadderManagerhistoryProvider provider = new LadderManagerhistoryProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LadderManagerhistoryEntity ladderManagerhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderManagerhistoryProvider(zoneId);
            return provider.Insert(ladderManagerhistoryEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LadderManagerhistoryEntity ladderManagerhistoryEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderManagerhistoryProvider(zoneId);
            return provider.Update(ladderManagerhistoryEntity,trans);
        }
		
		#endregion	
		
		
	}
}

