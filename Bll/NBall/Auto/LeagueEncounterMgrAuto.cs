
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
    /// LeagueEncounter管理类
    /// </summary>
    public static partial class LeagueEncounterMgr
    {
        
		#region  GetById
		
        public static LeagueEncounterEntity GetById( System.Guid matchId,string zoneId="")
        {
            var provider = new LeagueEncounterProvider(zoneId);
            return provider.GetById( matchId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LeagueEncounterEntity> GetAll(string zoneId="")
        {
            var provider = new LeagueEncounterProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetLeaguePair
		
        public static List<LeagueEncounterEntity> GetLeaguePair( System.Guid managerId, System.Guid leagueRecordId,string zoneId="")
        {
            var provider = new LeagueEncounterProvider(zoneId);
            return provider.GetLeaguePair( managerId, leagueRecordId);            
        }
		
		#endregion		  
		
		#region  GetWheelMatchs
		
        public static List<LeagueEncounterEntity> GetWheelMatchs( System.Guid leagueRecordId, System.Int32 wheel,string zoneId="")
        {
            var provider = new LeagueEncounterProvider(zoneId);
            return provider.GetWheelMatchs( leagueRecordId, wheel);            
        }
		
		#endregion		  
		
		#region  GetMatchsByHomeAwayIds
		
        public static List<LeagueEncounterEntity> GetMatchsByHomeAwayIds( System.Guid leagueRecordId, System.Guid managerId, System.Guid npcId,string zoneId="")
        {
            var provider = new LeagueEncounterProvider(zoneId);
            return provider.GetMatchsByHomeAwayIds( leagueRecordId, managerId, npcId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid matchId,DbTransaction trans=null,string zoneId="")
        {
            LeagueEncounterProvider provider = new LeagueEncounterProvider(zoneId);

            return provider.Delete( matchId,trans);
            
        }
		
		#endregion
        
		#region  GenerateFightdic
		
        public static bool GenerateFightdic ( System.Guid managerId, System.Int32 leagueId, System.Guid leagueRecordId, System.Int32 templateId,DbTransaction trans=null,string zoneId="")
        {
            LeagueEncounterProvider provider = new LeagueEncounterProvider(zoneId);

            return provider.GenerateFightdic( managerId, leagueId, leagueRecordId, templateId,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LeagueEncounterEntity leagueEncounterEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueEncounterProvider(zoneId);
            return provider.Insert(leagueEncounterEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LeagueEncounterEntity leagueEncounterEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueEncounterProvider(zoneId);
            return provider.Update(leagueEncounterEntity,trans);
        }
		
		#endregion	
		
		
	}
}
