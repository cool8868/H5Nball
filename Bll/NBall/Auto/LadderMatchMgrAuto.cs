
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
    /// LadderMatch管理类
    /// </summary>
    public static partial class LadderMatchMgr
    {
        
		#region  GetById
		
        public static LadderMatchEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new LadderMatchProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LadderMatchEntity> GetAll(string zoneId="")
        {
            var provider = new LadderMatchProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetFiveMatch
		
        public static List<LadderMatchEntity> GetFiveMatch( System.Guid managerId,string zoneId="")
        {
            var provider = new LadderMatchProvider(zoneId);
            return provider.GetFiveMatch( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            LadderMatchProvider provider = new LadderMatchProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  SaveMatch
		
        public static bool SaveMatch ( System.Guid ladderId, System.Guid homeId, System.Guid awayId, System.String homeName, System.String awayName, System.Int32 homeLadderScore, System.Int32 awayLadderScore, System.Int32 homeScore, System.Int32 awayScore, System.Int32 homeCoin, System.Int32 awayCoin, System.Int32 homeExp, System.Int32 awayExp, System.Boolean homeIsBot, System.Boolean awayIsBot, System.Int32 groupIndex, System.Int32 prizeHomeScore, System.Int32 prizeAwayScore, System.DateTime rowTime, System.Guid idx,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            LadderMatchProvider provider = new LadderMatchProvider(zoneId);

            return provider.SaveMatch( ladderId, homeId, awayId, homeName, awayName, homeLadderScore, awayLadderScore, homeScore, awayScore, homeCoin, awayCoin, homeExp, awayExp, homeIsBot, awayIsBot, groupIndex, prizeHomeScore, prizeAwayScore, rowTime, idx,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  GetPrizeScoreByTime
		
        public static bool GetPrizeScoreByTime ( System.Guid managerId, System.DateTime beginTime, System.DateTime endTime,ref  System.Int32 score,DbTransaction trans=null,string zoneId="")
        {
            LadderMatchProvider provider = new LadderMatchProvider(zoneId);

            return provider.GetPrizeScoreByTime( managerId, beginTime, endTime,ref  score,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LadderMatchEntity ladderMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderMatchProvider(zoneId);
            return provider.Insert(ladderMatchEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LadderMatchEntity ladderMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LadderMatchProvider(zoneId);
            return provider.Update(ladderMatchEntity,trans);
        }
		
		#endregion	
		
		
	}
}

