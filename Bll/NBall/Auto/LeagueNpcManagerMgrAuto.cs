
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
    /// LeagueNpcmanager管理类
    /// </summary>
    public static partial class LeagueNpcmanagerMgr
    {
        
		#region  GetById
		
        public static LeagueNpcmanagerEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new LeagueNpcmanagerProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetNpcManager
		
        public static LeagueNpcmanagerEntity GetNpcManager( System.Guid npcId, System.Guid leagueRecordId,string zoneId="")
        {
            var provider = new LeagueNpcmanagerProvider(zoneId);
            return provider.GetNpcManager( npcId, leagueRecordId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<LeagueNpcmanagerEntity> GetAll(string zoneId="")
        {
            var provider = new LeagueNpcmanagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            LeagueNpcmanagerProvider provider = new LeagueNpcmanagerProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(LeagueNpcmanagerEntity leagueNpcmanagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueNpcmanagerProvider(zoneId);
            return provider.Insert(leagueNpcmanagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(LeagueNpcmanagerEntity leagueNpcmanagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new LeagueNpcmanagerProvider(zoneId);
            return provider.Update(leagueNpcmanagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}

