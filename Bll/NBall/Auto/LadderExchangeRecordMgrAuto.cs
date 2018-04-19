
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
    /// LadderExchangerecord管理类
    /// </summary>
    public static partial class LadderExchangerecordMgr
    {
        
		#region  GetById
		
        public static LadderExchangerecordEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new LadderExchangerecordProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LadderExchangerecordEntity> GetAll(string zoneId="")
        {
            var provider = new LadderExchangerecordProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            LadderExchangerecordProvider provider = new LadderExchangerecordProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  ScoreToHonor
		
        public static bool ScoreToHonor ( System.DateTime curDate, System.Int32 curSeasonId, System.Int32 isNewSeason,DbTransaction trans=null,string zoneId="")
        {
            LadderExchangerecordProvider provider = new LadderExchangerecordProvider(zoneId);

            return provider.ScoreToHonor( curDate, curSeasonId, isNewSeason,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LadderExchangerecordEntity ladderExchangerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderExchangerecordProvider(zoneId);
            return provider.Insert(ladderExchangerecordEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LadderExchangerecordEntity ladderExchangerecordEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderExchangerecordProvider(zoneId);
            return provider.Update(ladderExchangerecordEntity,trans);
        }
		
		#endregion	
		
		
	}
}

