
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
    /// RankingYesterday管理类
    /// </summary>
    public static partial class RankingYesterdayMgr
    {
        
		#region  GetById
		
        public static RankingYesterdayEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new RankingYesterdayProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<RankingYesterdayEntity> GetAll(string zoneId="")
        {
            var provider = new RankingYesterdayProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            RankingYesterdayProvider provider = new RankingYesterdayProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  StartRecord
		
        public static bool StartRecord (DbTransaction trans=null,string zoneId="")
        {
            RankingYesterdayProvider provider = new RankingYesterdayProvider(zoneId);

            return provider.StartRecord(trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(RankingYesterdayEntity rankingYesterdayEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RankingYesterdayProvider(zoneId);
            return provider.Insert(rankingYesterdayEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(RankingYesterdayEntity rankingYesterdayEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new RankingYesterdayProvider(zoneId);
            return provider.Update(rankingYesterdayEntity,trans);
        }
		
		#endregion	
		
		
	}
}

