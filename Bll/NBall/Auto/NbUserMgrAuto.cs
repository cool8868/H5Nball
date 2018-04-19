
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
    /// NbUser管理类
    /// </summary>
    public static partial class NbUserMgr
    {
        
		#region  GetById
		
        public static NbUserEntity GetById( System.String account,string zoneId="")
        {
            var provider = new NbUserProvider(zoneId);
            return provider.GetById( account);
        }
		
		#endregion		  
		
		#region  GetByAccount
		
        public static NbUserEntity GetByAccount( System.String account, System.String userIp, System.DateTime yesterday, System.DateTime today, System.Int32 status,string zoneId="")
        {
            var provider = new NbUserProvider(zoneId);
            return provider.GetByAccount( account, userIp, yesterday, today, status);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbUserEntity> GetAll(string zoneId="")
        {
            var provider = new NbUserProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String account,DbTransaction trans=null,string zoneId="")
        {
            NbUserProvider provider = new NbUserProvider(zoneId);

            return provider.Delete( account,trans);
            
        }
		
		#endregion
        
		#region  GetTotal
		
        public static bool GetTotal (ref  System.Int32 totalUser,ref  System.Int32 totalManager,ref  System.Int64 totalPay,ref  System.Int64 pointRemain,ref  System.Int64 onlineMinutes,DbTransaction trans=null,string zoneId="")
        {
            NbUserProvider provider = new NbUserProvider(zoneId);

            return provider.GetTotal(ref  totalUser,ref  totalManager,ref  totalPay,ref  pointRemain,ref  onlineMinutes,trans);
            
        }
		
		#endregion
        
		#region  GetDetail
		
        public static bool GetDetail ( System.DateTime startTime, System.DateTime endTime,ref  System.Int32 loginUser,ref  System.Int32 chargeUser,ref  System.Int32 chargeCash,ref  System.Int32 consumePoint,ref  System.Int32 newUser,ref  System.Int32 newManager,DbTransaction trans=null,string zoneId="")
        {
            NbUserProvider provider = new NbUserProvider(zoneId);

            return provider.GetDetail( startTime, endTime,ref  loginUser,ref  chargeUser,ref  chargeCash,ref  consumePoint,ref  newUser,ref  newManager,trans);
            
        }
		
		#endregion
        
		#region  GetKpi
		
        public static bool GetKpi ( System.DateTime curTime,ref  System.Int32 registerUser,ref  System.Int32 registerManager,ref  System.Int32 dau,ref  System.Int32 dUniqueIp,ref  System.Int32 dNewUser,ref  System.Int32 dNewManager,ref  System.Int32 dLostUser7,ref  System.Int32 dLostUser15,ref  System.Int32 dLostUser30,ref  System.Int32 retention2,ref  System.Int32 retention3,ref  System.Int32 retention4,ref  System.Int32 retention5,ref  System.Int32 retention6,ref  System.Int32 retention7,ref  System.Int32 retention15,ref  System.Int32 retention30,ref  System.Int32 wau,ref  System.Int32 wLost,ref  System.Int32 wHonor,ref  System.Int32 wHonorLost,ref  System.Int32 mau,ref  System.Int32 payUserCount,ref  System.Int32 payCount,ref  System.Int32 payTotal,ref  System.Int64 paySum,ref  System.Int32 payFirst,ref  System.Int32 payWLost,ref  System.Int32 lTV,ref  System.Int64 pointRemain,ref  System.Int64 pointConsume,ref  System.Int64 pointCirculate,DbTransaction trans=null,string zoneId="")
        {
            NbUserProvider provider = new NbUserProvider(zoneId);

            return provider.GetKpi( curTime,ref  registerUser,ref  registerManager,ref  dau,ref  dUniqueIp,ref  dNewUser,ref  dNewManager,ref  dLostUser7,ref  dLostUser15,ref  dLostUser30,ref  retention2,ref  retention3,ref  retention4,ref  retention5,ref  retention6,ref  retention7,ref  retention15,ref  retention30,ref  wau,ref  wLost,ref  wHonor,ref  wHonorLost,ref  mau,ref  payUserCount,ref  payCount,ref  payTotal,ref  paySum,ref  payFirst,ref  payWLost,ref  lTV,ref  pointRemain,ref  pointConsume,ref  pointCirculate,trans);
            
        }
		
		#endregion
        
		#region  GetOnline
		
        public static bool GetOnline (ref  System.Int32 userCount,ref  System.Int64 totalTime,DbTransaction trans=null,string zoneId="")
        {
            NbUserProvider provider = new NbUserProvider(zoneId);

            return provider.GetOnline(ref  userCount,ref  totalTime,trans);
            
        }
		
		#endregion
        
		#region  UpdateContinueday
		
        public static bool UpdateContinueday ( System.Guid managerId, System.String account, System.DateTime yesterday, System.DateTime today,DbTransaction trans=null,string zoneId="")
        {
            NbUserProvider provider = new NbUserProvider(zoneId);

            return provider.UpdateContinueday( managerId, account, yesterday, today,trans);
            
        }
		
		#endregion
        
		#region  GetKpiImmediate
		
        public static bool GetKpiImmediate ( System.DateTime curTime,ref  System.Int32 registerUser,ref  System.Int32 registerManager,ref  System.Int32 dau,ref  System.Int32 dUniqueIp,ref  System.Int32 dNewUser,ref  System.Int32 dNewManager,ref  System.Int32 payUserCount,ref  System.Int32 payCount,ref  System.Int32 payTotal,ref  System.Int64 paySum,ref  System.Int32 payFirst,ref  System.Int32 curOnline,DbTransaction trans=null,string zoneId="")
        {
            NbUserProvider provider = new NbUserProvider(zoneId);

            return provider.GetKpiImmediate( curTime,ref  registerUser,ref  registerManager,ref  dau,ref  dUniqueIp,ref  dNewUser,ref  dNewManager,ref  payUserCount,ref  payCount,ref  payTotal,ref  paySum,ref  payFirst,ref  curOnline,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbUserEntity nbUserEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbUserProvider(zoneId);
            return provider.Insert(nbUserEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbUserEntity nbUserEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbUserProvider(zoneId);
            return provider.Update(nbUserEntity,trans);
        }
		
		#endregion	
		
		
	}
}
