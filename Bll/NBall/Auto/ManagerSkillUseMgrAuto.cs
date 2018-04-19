
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
    /// ManagerskillUse管理类
    /// </summary>
    public static partial class ManagerskillUseMgr
    {
        
		#region  GetById
		
        public static ManagerskillUseEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new ManagerskillUseProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ManagerskillUseEntity> GetAll(string zoneId="")
        {
            var provider = new ManagerskillUseProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  HitTalent
		
        public static bool HitTalent ( System.Guid managerId, System.Int32 syncTalentPoint, System.Int32 maxTalentPoint, System.Boolean todoFlag, System.String talents, System.Byte[] libRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            ManagerskillUseProvider provider = new ManagerskillUseProvider(zoneId);

            return provider.HitTalent( managerId, syncTalentPoint, maxTalentPoint, todoFlag, talents, libRowVersion,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  SetTalent
		
        public static bool SetTalent ( System.Guid managerId, System.String talents, System.Byte[] useRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            ManagerskillUseProvider provider = new ManagerskillUseProvider(zoneId);

            return provider.SetTalent( managerId, talents, useRowVersion,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  ResetTalent
		
        public static bool ResetTalent ( System.Guid managerId, System.Int32 managerHash, System.String account, System.Int32 costGold, System.Int32 costGoldItemNo, System.Guid costGoldOrderId, System.Int32 costCoin, System.Byte[] costRowVersion, System.Int32 poolBuffType, System.Byte[] useRowVersion, System.Byte[] libRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            ManagerskillUseProvider provider = new ManagerskillUseProvider(zoneId);

            return provider.ResetTalent( managerId, managerHash, account, costGold, costGoldItemNo, costGoldOrderId, costCoin, costRowVersion, poolBuffType, useRowVersion, libRowVersion,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  PutWill
		
        public static bool PutWill ( System.Guid managerId, System.String willCode, System.String putMap, System.Int32 enableFlag, System.Byte[] srcRowVersion, System.Int32 maxWillNumber, System.Boolean todoFlag, System.String useWills, System.String libWills, System.Byte[] libRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            ManagerskillUseProvider provider = new ManagerskillUseProvider(zoneId);

            return provider.PutWill( managerId, willCode, putMap, enableFlag, srcRowVersion, maxWillNumber, todoFlag, useWills, libWills, libRowVersion,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  SetWill
		
        public static bool SetWill ( System.Guid managerId, System.String wills, System.Byte[] useRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            ManagerskillUseProvider provider = new ManagerskillUseProvider(zoneId);

            return provider.SetWill( managerId, wills, useRowVersion,ref  errorCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ManagerskillUseEntity managerskillUseEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerskillUseProvider(zoneId);
            return provider.Insert(managerskillUseEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ManagerskillUseEntity managerskillUseEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ManagerskillUseProvider(zoneId);
            return provider.Update(managerskillUseEntity,trans);
        }
		
		#endregion	
		
		
	}
}

