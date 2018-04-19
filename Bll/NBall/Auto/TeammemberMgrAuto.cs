
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
    /// Teammember管理类
    /// </summary>
    public static partial class TeammemberMgr
    {
        
		#region  GetById
		
        public static TeammemberEntity GetById( System.Guid idx, System.Int32 mod,string zoneId="")
        {
            var provider = new TeammemberProvider(zoneId);
            return provider.GetById( idx, mod);
        }
		
		#endregion		  
		
		#region  GetByManager
		
        public static List<TeammemberEntity> GetByManager( System.Guid managerId, System.Int32 mod,string zoneId="")
        {
            var provider = new TeammemberProvider(zoneId);
            return provider.GetByManager( managerId, mod);            
        }
		
		#endregion		  
		
		#region  GetByStatus
		
        public static List<TeammemberEntity> GetByStatus( System.Guid managerId, System.Int32 status, System.Int32 mod,string zoneId="")
        {
            var provider = new TeammemberProvider(zoneId);
            return provider.GetByStatus( managerId, status, mod);            
        }
		
		#endregion		  
		
		#region  EquipmentExists
		
        public static bool EquipmentExists ( System.Guid managerId, System.Int32 mod,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.EquipmentExists( managerId, mod,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  PlayerCardExists
		
        public static bool PlayerCardExists ( System.Guid managerId, System.Int32 mod,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.PlayerCardExists( managerId, mod,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  ResetProperty
		
        public static bool ResetProperty ( System.Guid idx, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.ResetProperty( idx, mod,trans);
            
        }
		
		#endregion
        
		#region  UpdateProperty
		
        public static bool UpdateProperty ( System.Guid idx, System.Int32 usedProperty, System.Double speed, System.Double shoot, System.Double freeKick, System.Double balance, System.Double physique, System.Double bounce, System.Double aggression, System.Double disturb, System.Double interception, System.Double dribble, System.Double pass, System.Double mentality, System.Double response, System.Double positioning, System.Double handControl, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.UpdateProperty( idx, usedProperty, speed, shoot, freeKick, balance, physique, bounce, aggression, disturb, interception, dribble, pass, mentality, response, positioning, handControl, mod,trans);
            
        }
		
		#endregion
        
		#region  UpdateLevel
		
        public static bool UpdateLevel ( System.Guid teammemberId, System.Int32 level, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.UpdateLevel( teammemberId, level, mod,trans);
            
        }
		
		#endregion
        
		#region  GetForTransCheck
		
        public static bool GetForTransCheck ( System.Guid managerId, System.Int32 playerId, System.Int32 mod, System.Int32 repeatCode,ref  System.Int32 teammemberCount,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.GetForTransCheck( managerId, playerId, mod, repeatCode,ref  teammemberCount,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  TransSave
		
        public static bool TransSave ( System.Guid managerId, System.Guid idx, System.Int32 playerId, System.Int32 repeatCode, System.Byte[] itemString, System.Int32 mod,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.TransSave( managerId, idx, playerId, repeatCode, itemString, mod,ref  returnCode,ref  errorMessage,trans);
            
        }
		
		#endregion
        
		#region  SaveSolution
		
        public static bool SaveSolution ( System.Guid managerId, System.Int32 formationId, System.String playerString, System.Int32 veteranCount, System.Int32 orangeCount, System.Int32 combCount,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.SaveSolution( managerId, formationId, playerString, veteranCount, orangeCount, combCount,trans);
            
        }
		
		#endregion
        
		#region  Fire
		
        public static bool Fire ( System.Guid idx, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.Fire( idx, mod,trans);
            
        }
		
		#endregion
        
		#region  SetPlayerCard
		
        public static bool SetPlayerCard ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] equipmentData, System.Byte[] itemString, System.Byte[] itemRowVersion,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.SetPlayerCard( idx, managerId, mod, equipmentData, itemString, itemRowVersion,ref  returnCode,ref  errorMessage,trans);
            
        }
		
		#endregion
        
		#region  SetEquipment
		
        public static bool SetEquipment ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] equipmentData, System.Byte[] itemString, System.Byte[] itemRowVersion,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.SetEquipment( idx, managerId, mod, equipmentData, itemString, itemRowVersion,ref  returnCode,ref  errorMessage,trans);
            
        }
		
		#endregion
        
		#region  FormationLevelup
		
        public static bool FormationLevelup ( System.Guid managerId, System.Byte[] formationData, System.Int32 sophisticate, System.Int32 sophisticateShortageCode,ref  System.Int32 curSophisticate,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.FormationLevelup( managerId, formationData, sophisticate, sophisticateShortageCode,ref  curSophisticate,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  UpdateIsCopyed
		
        public static bool UpdateIsCopyed ( System.Guid teammemberId, System.Boolean isCopyed, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.UpdateIsCopyed( teammemberId, isCopyed, mod,trans);
            
        }
		
		#endregion
        
		#region  SaveInherit
		
        public static bool SaveInherit ( System.Guid teammemberId, System.Int32 level, System.Int32 exp, System.Guid targetTeammemberId, System.Int32 targetLevel, System.Int32 targetExp, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.SaveInherit( teammemberId, level, exp, targetTeammemberId, targetLevel, targetExp, mod,trans);
            
        }
		
		#endregion
        
		#region  SetBadge
		
        public static bool SetBadge ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] badgeData, System.Byte[] itemString, System.Byte[] itemRowVersion,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.SetBadge( idx, managerId, mod, badgeData, itemString, itemRowVersion,ref  returnCode,ref  errorMessage,trans);
            
        }
		
		#endregion
        
		#region  UpdateArousalLv
		
        public static bool UpdateArousalLv ( System.Guid teammemberId, System.Int32 arousalLv, System.Int32 mod,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.UpdateArousalLv( teammemberId, arousalLv, mod,trans);
            
        }
		
		#endregion
        
		#region  SetClubClothes
		
        public static bool SetClubClothes ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] clubClothesData, System.Byte[] itemString, System.Byte[] itemRowVersion,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.SetClubClothes( idx, managerId, mod, clubClothesData, itemString, itemRowVersion,ref  returnCode,ref  errorMessage,trans);
            
        }
		
		#endregion
        
		#region  SaveHireSolution
		
        public static bool SaveHireSolution ( System.Guid managerId, System.Int32 formationId, System.String playerString, System.String hirePlayerString, System.Int32 veteranCount, System.Int32 orangeCount, System.Int32 combCount,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.SaveHireSolution( managerId, formationId, playerString, hirePlayerString, veteranCount, orangeCount, combCount,trans);
            
        }
		
		#endregion
        
		#region  SetUsePlayerCard
		
        public static bool SetUsePlayerCard ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] playerCardData,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null,string zoneId="")
        {
            TeammemberProvider provider = new TeammemberProvider(zoneId);

            return provider.SetUsePlayerCard( idx, managerId, mod, playerCardData,ref  returnCode,ref  errorMessage,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TeammemberEntity teammemberEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TeammemberProvider(zoneId);
            return provider.Insert(teammemberEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TeammemberEntity teammemberEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TeammemberProvider(zoneId);
            return provider.Update(teammemberEntity,trans);
        }
		
		#endregion	
		
		
	}
}
