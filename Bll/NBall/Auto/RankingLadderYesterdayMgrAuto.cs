
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
    /// RankingLadderyesterday管理类
    /// </summary>
    public static partial class RankingLadderyesterdayMgr
    {
        
		#region  GetById
		
        public static RankingLadderyesterdayEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new RankingLadderyesterdayProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RankingLadderyesterdayEntity> GetAll(string zoneId="")
        {
            var provider = new RankingLadderyesterdayProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            RankingLadderyesterdayProvider provider = new RankingLadderyesterdayProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  StartRecord
		
        public static bool StartRecord (DbTransaction trans=null,string zoneId="")
        {
            RankingLadderyesterdayProvider provider = new RankingLadderyesterdayProvider(zoneId);

            return provider.StartRecord(trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RankingLadderyesterdayEntity rankingLadderyesterdayEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RankingLadderyesterdayProvider(zoneId);
            return provider.Insert(rankingLadderyesterdayEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RankingLadderyesterdayEntity rankingLadderyesterdayEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RankingLadderyesterdayProvider(zoneId);
            return provider.Update(rankingLadderyesterdayEntity,trans);
        }
		
		#endregion	
		
		
	}
}

