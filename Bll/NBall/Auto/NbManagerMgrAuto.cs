
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
    /// NbManager管理类
    /// </summary>
    public static partial class NbManagerMgr
    {
        
		#region  GetById
		
        public static NbManagerEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new NbManagerProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByName
		
        public static NbManagerEntity GetByName( System.String name,string zoneId="")
        {
            var provider = new NbManagerProvider(zoneId);
            return provider.GetByName( name);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagerEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByAccount
		
        public static List<NbManagerEntity> GetByAccount( System.String account,string zoneId="")
        {
            var provider = new NbManagerProvider(zoneId);
            return provider.GetByAccount( account);            
        }
		
		#endregion		  
		
		#region  GetMaxLevel
		
		public static Int32 GetMaxLevel (string zoneId="")
        {
            var provider = new NbManagerProvider(zoneId);
            return provider.GetMaxLevel();
        }
		
		#endregion		  
		
		#region  DeleteRole
		
        public static bool DeleteRole ( System.String account, System.Guid bindCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.DeleteRole( account, bindCode,trans);
            
        }
		
		#endregion
        
		#region  BindAccount
		
        public static bool BindAccount ( System.String newAccount, System.String newName, System.String newMid, System.String newMod, System.Guid bindCode,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.BindAccount( newAccount, newName, newMid, newMod, bindCode,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  UpdateName
		
        public static bool UpdateName ( System.Guid managerId, System.String oldName, System.String newName,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.UpdateName( managerId, oldName, newName,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  TransferZoneByAccount
		
        public static bool TransferZoneByAccount ( System.String sourceDbFullName, System.String account, System.String name, System.String mid, System.String mod, System.String newAccount, System.String newName, System.String newMid, System.String newMod,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.TransferZoneByAccount( sourceDbFullName, account, name, mid, mod, newAccount, newName, newMid, newMod,trans);
            
        }
		
		#endregion
        
		#region  AccountExists
		
        public static bool AccountExists ( System.String account,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.AccountExists( account,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  UpdateLogo
		
        public static bool UpdateLogo ( System.Guid managerId, System.String logo,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.UpdateLogo( managerId, logo,trans);
            
        }
		
		#endregion
        
		#region  AddScore
		
        public static bool AddScore ( System.Guid managerId, System.Int32 score,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.AddScore( managerId, score,trans);
            
        }
		
		#endregion
        
		#region  Delete
		
        public static bool Delete ( System.Guid idx, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  AddCoinAndScore
		
        public static bool AddCoinAndScore ( System.Guid managerId, System.Int32 coin, System.Int32 score,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.AddCoinAndScore( managerId, coin, score,trans);
            
        }
		
		#endregion
        
		#region  Create
		
        public static bool Create ( System.Guid managerId, System.Int32 mod, System.String name, System.Int32 stamina, System.Int32 packageSize, System.Int32 itemVersion, System.Int32 teammemberMax, System.Int32 registerTrainSeat, System.Int32 registerLadderScore, System.Int32 nameRepeatCode, System.Int32 existsManagerCode,ref  System.String errorMsg,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.Create( managerId, mod, name, stamina, packageSize, itemVersion, teammemberMax, registerTrainSeat, registerLadderScore, nameRepeatCode, existsManagerCode,ref  errorMsg,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  Register
		
        public static bool Register ( System.String account, System.String name, System.String logo, System.Int32 templateId, System.String playerString, System.Int32 registerFormationId, System.Int32 kpi, System.Int32 nameRepeatCode, System.Int32 existsManagerCode, System.Int32 registerFailCode, System.Boolean isBot,ref  System.Guid managerId,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.Register( account, name, logo, templateId, playerString, registerFormationId, kpi, nameRepeatCode, existsManagerCode, registerFailCode, isBot,ref  managerId,ref  returnCode,ref  errorMessage,trans);
            
        }
		
		#endregion
        
		#region  NameExists
		
        public static bool NameExists ( System.String name,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.NameExists( name,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  AddCoin
		
        public static bool AddCoin ( System.Guid managerId, System.Int32 coin,ref  System.Int32 curCoin,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.AddCoin( managerId, coin,ref  curCoin,trans);
            
        }
		
		#endregion
        
		#region  CostCoin
		
        public static bool CostCoin ( System.Guid managerId, System.Int32 coin,ref  System.Int32 curCoin,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.CostCoin( managerId, coin,ref  curCoin,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  AddReiki
		
        public static bool AddReiki ( System.Guid managerId, System.Int32 reiki,ref  System.Int32 curReiki,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.AddReiki( managerId, reiki,ref  curReiki,trans);
            
        }
		
		#endregion
        
		#region  CostReiki
		
        public static bool CostReiki ( System.Guid managerId, System.Int32 reiki,ref  System.Int32 curReiki,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.CostReiki( managerId, reiki,ref  curReiki,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  AddSophisticate
		
        public static bool AddSophisticate ( System.Guid managerId, System.Int32 sophisticate,ref  System.Int32 curSophisticate,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.AddSophisticate( managerId, sophisticate,ref  curSophisticate,trans);
            
        }
		
		#endregion
        
		#region  CostSophisticate
		
        public static bool CostSophisticate ( System.Guid managerId, System.Int32 sophisticate, System.Int32 sophisticateShortageCode,ref  System.Int32 curSophisticate,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.CostSophisticate( managerId, sophisticate, sophisticateShortageCode,ref  curSophisticate,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  Save
		
        public static bool Save ( System.Guid idx, System.Int32 level, System.Int32 eXP, System.Int32 sophisticate, System.Int32 score, System.Int32 coin, System.Int32 reiki, System.Int32 teammemberMax, System.Int32 trainSeatMax, System.Int32 vipLevel, System.String functionList, System.DateTime levelGiftExpired, System.DateTime levelGiftExpired2, System.DateTime levelGiftExpired3, System.Int32 levelGiftStep, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.Save( idx, level, eXP, sophisticate, score, coin, reiki, teammemberMax, trainSeatMax, vipLevel, functionList, levelGiftExpired, levelGiftExpired2, levelGiftExpired3, levelGiftStep, rowVersion,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  AddCoinAndReiki
		
        public static bool AddCoinAndReiki ( System.Guid managerId, System.Int32 coin, System.Int32 reiki,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.AddCoinAndReiki( managerId, coin, reiki,trans);
            
        }
		
		#endregion
        
		#region  AddFriendShipPoint
		
        public static bool AddFriendShipPoint ( System.Guid managerId, System.Int32 friendShipPoint,ref  System.Int32 curFriendShipPoint,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.AddFriendShipPoint( managerId, friendShipPoint,ref  curFriendShipPoint,trans);
            
        }
		
		#endregion
        
		#region  CostFriendShipPoint
		
        public static bool CostFriendShipPoint ( System.Guid managerId, System.Int32 friendShipPoint,ref  System.Int32 curFriendShipPoint,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerProvider provider = new NbManagerProvider(zoneId);

            return provider.CostFriendShipPoint( managerId, friendShipPoint,ref  curFriendShipPoint,ref  returnCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagerEntity nbManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerProvider(zoneId);
            return provider.Insert(nbManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagerEntity nbManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerProvider(zoneId);
            return provider.Update(nbManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
