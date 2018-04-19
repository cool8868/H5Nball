
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
    /// NbManagerextra管理类
    /// </summary>
    public static partial class NbManagerextraMgr
    {
        
		#region  GetById
		
        public static NbManagerextraEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagerextraProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagerextraEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagerextraProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetTODayLoginManager
		
        public static List<NbManagerextraEntity> GetTODayLoginManager(string zoneId="")
        {
            var provider = new NbManagerextraProvider(zoneId);
            return provider.GetTODayLoginManager();            
        }
		
		#endregion		  
		
		#region  GetInviteFriendCount
		
		public static Int32 GetInviteFriendCount ( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagerextraProvider(zoneId);
            return provider.GetInviteFriendCount( managerId);
        }
		
		#endregion		  
		
		#region  UpdateGuidePrize
		
        public static bool UpdateGuidePrize ( System.Guid managerId, System.Boolean hasGuidePrize, System.DateTime guidePrizeExpired, System.Int32 guidePrizeCount, System.DateTime guidePrizeLastDate,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.UpdateGuidePrize( managerId, hasGuidePrize, guidePrizeExpired, guidePrizeCount, guidePrizeLastDate,trans);
            
        }
		
		#endregion
        
		#region  UpdateGuideScouting
		
        public static bool UpdateGuideScouting ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.UpdateGuideScouting( managerId,trans);
            
        }
		
		#endregion
        
		#region  UpdateScoutingPointFirst
		
        public static bool UpdateScoutingPointFirst ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.UpdateScoutingPointFirst( managerId,trans);
            
        }
		
		#endregion
        
		#region  UpdateKpi
		
        public static bool UpdateKpi ( System.Guid managerId, System.Int32 kpi,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.UpdateKpi( managerId, kpi,trans);
            
        }
		
		#endregion
        
		#region  SaveHelpCount
		
        public static bool SaveHelpCount ( System.Guid managerId, System.Int32 helpCount, System.Int32 byHelpCount, System.DateTime recordDate,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.SaveHelpCount( managerId, helpCount, byHelpCount, recordDate,trans);
            
        }
		
		#endregion
        
		#region  SaveByHelpCount
		
        public static bool SaveByHelpCount ( System.Guid managerId, System.Int32 byHelpCount, System.DateTime recordDate,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.SaveByHelpCount( managerId, byHelpCount, recordDate,trans);
            
        }
		
		#endregion
        
		#region  UpdatePayFirst
		
        public static bool UpdatePayFirst ( System.Guid managerId, System.Boolean payFirstFlag,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.UpdatePayFirst( managerId, payFirstFlag,trans);
            
        }
		
		#endregion
        
		#region  UpdateLevelGift
		
        public static bool UpdateLevelGift ( System.Guid managerId, System.DateTime expired, System.Int32 step,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.UpdateLevelGift( managerId, expired, step,trans);
            
        }
		
		#endregion
        
		#region  UpdateScouting
		
        public static bool UpdateScouting ( System.Guid managerId, System.DateTime date,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.UpdateScouting( managerId, date,trans);
            
        }
		
		#endregion
        
		#region  UpdateCoinScouting
		
        public static bool UpdateCoinScouting ( System.Guid managerId, System.DateTime date,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.UpdateCoinScouting( managerId, date,trans);
            
        }
		
		#endregion
        
		#region  UpdateFriendScouting
		
        public static bool UpdateFriendScouting ( System.Guid managerId, System.DateTime date,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.UpdateFriendScouting( managerId, date,trans);
            
        }
		
		#endregion
        
		#region  AddActive
		
        public static bool AddActive ( System.Guid managerID, System.Int32 number,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.AddActive( managerID, number,trans);
            
        }
		
		#endregion
        
		#region  AddLeagueScore
		
        public static bool AddLeagueScore ( System.Guid managerId, System.Int32 score,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.AddLeagueScore( managerId, score,trans);
            
        }
		
		#endregion
        
		#region  AddSkillPoint
		
        public static bool AddSkillPoint ( System.Guid managerId, System.Int32 skillPoint,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.AddSkillPoint( managerId, skillPoint,trans);
            
        }
		
		#endregion
        
		#region  ToDeductSkillPoint
		
        public static bool ToDeductSkillPoint ( System.Guid managerId, System.Int32 skillPoint,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.ToDeductSkillPoint( managerId, skillPoint,trans);
            
        }
		
		#endregion
        
		#region  SetSkillType
		
        public static bool SetSkillType ( System.Guid managerId, System.Int32 skillType,DbTransaction trans=null,string zoneId="")
        {
            NbManagerextraProvider provider = new NbManagerextraProvider(zoneId);

            return provider.SetSkillType( managerId, skillType,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagerextraEntity nbManagerextraEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerextraProvider(zoneId);
            return provider.Insert(nbManagerextraEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagerextraEntity nbManagerextraEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerextraProvider(zoneId);
            return provider.Update(nbManagerextraEntity,trans);
        }
		
		#endregion	
		
		
	}
}
