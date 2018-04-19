
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
    /// CrossladderMatch管理类
    /// </summary>
    public static partial class CrossladderMatchMgr
    {
        
		#region  GetById
		
        public static CrossladderMatchEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new CrossladderMatchProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<CrossladderMatchEntity> GetAll(string zoneId="")
        {
            var provider = new CrossladderMatchProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetFiveMatch
		
        public static List<CrossladderMatchEntity> GetFiveMatch( System.Guid managerId,string zoneId="")
        {
            var provider = new CrossladderMatchProvider(zoneId);
            return provider.GetFiveMatch( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            CrossladderMatchProvider provider = new CrossladderMatchProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  SaveMatch
		
        public static bool SaveMatch ( System.Int32 domainId, System.Guid ladderId, System.Guid homeId, System.Guid awayId, System.String homeName, System.String awayName, System.String homeLogo, System.String awayLogo, System.String homeSiteId, System.String awaySiteId, System.Int32 homeLadderScore, System.Int32 awayLadderScore, System.Int32 homeScore, System.Int32 awayScore, System.Int32 homeCoin, System.Int32 awayCoin, System.Int32 homeExp, System.Int32 awayExp, System.Boolean homeIsBot, System.Boolean awayIsBot, System.Int32 groupIndex, System.Int32 prizeHomeScore, System.Int32 prizeAwayScore, System.DateTime rowTime, System.Guid idx,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            CrossladderMatchProvider provider = new CrossladderMatchProvider(zoneId);

            return provider.SaveMatch( domainId, ladderId, homeId, awayId, homeName, awayName, homeLogo, awayLogo, homeSiteId, awaySiteId, homeLadderScore, awayLadderScore, homeScore, awayScore, homeCoin, awayCoin, homeExp, awayExp, homeIsBot, awayIsBot, groupIndex, prizeHomeScore, prizeAwayScore, rowTime, idx,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(CrossladderMatchEntity crossladderMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderMatchProvider(zoneId);
            return provider.Insert(crossladderMatchEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(CrossladderMatchEntity crossladderMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new CrossladderMatchProvider(zoneId);
            return provider.Update(crossladderMatchEntity,trans);
        }
		
		#endregion	
		
		
	}
}
