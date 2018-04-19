
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
    /// PlayerkillInfo管理类
    /// </summary>
    public static partial class PlayerkillInfoMgr
    {
        
		#region  GetById
		
        public static PlayerkillInfoEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new PlayerkillInfoProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<PlayerkillInfoEntity> GetAll(string zoneId="")
        {
            var provider = new PlayerkillInfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  ResetByTimes
		
        public static bool ResetByTimes ( System.Int32 configTimes, System.Int32 configByTimes, System.DateTime updateDate,DbTransaction trans=null,string zoneId="")
        {
            PlayerkillInfoProvider provider = new PlayerkillInfoProvider(zoneId);

            return provider.ResetByTimes( configTimes, configByTimes, updateDate,trans);
            
        }
		
		#endregion
        
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            PlayerkillInfoProvider provider = new PlayerkillInfoProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
		#region  SaveFightResult
		
        public static bool SaveFightResult ( System.Guid managerId, System.String logo, System.Guid awayId, System.Guid lotteryMatchId, System.Int32 win, System.Int32 lose, System.Int32 draw, System.DateTime curTime, System.Guid matchId, System.String homeName, System.String awayName, System.Int32 homeScore, System.Int32 awayScore, System.Int32 prizeExp, System.Int32 prizeCoin, System.Int32 prizeItemCode, System.String prizeItemString, System.Boolean isRevenge, System.Int64 revengeRecordId, System.Int32 prizeItemCount,ref  System.Int64 outRevengeRecordId,DbTransaction trans=null,string zoneId="")
        {
            PlayerkillInfoProvider provider = new PlayerkillInfoProvider(zoneId);

            return provider.SaveFightResult( managerId, logo, awayId, lotteryMatchId, win, lose, draw, curTime, matchId, homeName, awayName, homeScore, awayScore, prizeExp, prizeCoin, prizeItemCode, prizeItemString, isRevenge, revengeRecordId, prizeItemCount,ref  outRevengeRecordId,trans);
            
        }
		
		#endregion
        
		#region  LotterySave
		
        public static bool LotterySave ( System.Guid matchId, System.Guid managerId, System.Int32 lotteryRepeatCode,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            PlayerkillInfoProvider provider = new PlayerkillInfoProvider(zoneId);

            return provider.LotterySave( matchId, managerId, lotteryRepeatCode,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  GetMatchTimes
		
        public static bool GetMatchTimes ( System.Guid managerId,ref  System.Int32 matchCount,DbTransaction trans=null,string zoneId="")
        {
            PlayerkillInfoProvider provider = new PlayerkillInfoProvider(zoneId);

            return provider.GetMatchTimes( managerId,ref  matchCount,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(PlayerkillInfoEntity playerkillInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PlayerkillInfoProvider(zoneId);
            return provider.Insert(playerkillInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(PlayerkillInfoEntity playerkillInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new PlayerkillInfoProvider(zoneId);
            return provider.Update(playerkillInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}
