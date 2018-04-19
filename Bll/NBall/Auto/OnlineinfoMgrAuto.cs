
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
    /// OnlineInfo管理类
    /// </summary>
    public static partial class OnlineInfoMgr
    {
        
		#region  GetById
		
        public static OnlineInfoEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new OnlineInfoProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<OnlineInfoEntity> GetAll(string zoneId="")
        {
            var provider = new OnlineInfoProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            OnlineInfoProvider provider = new OnlineInfoProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
		#region  Login
		
        public static bool Login ( System.Guid managerId, System.String loginIp, System.DateTime today,DbTransaction trans=null,string zoneId="")
        {
            OnlineInfoProvider provider = new OnlineInfoProvider(zoneId);

            return provider.Login( managerId, loginIp, today,trans);
            
        }
		
		#endregion
        
		#region  GetByManagerId
		
        public static bool GetByManagerId ( System.Guid managerId,ref  System.Boolean activeFlag,ref  System.DateTime loginTime,ref  System.DateTime activeTime,ref  System.Int32 onlineMinutes,DbTransaction trans=null,string zoneId="")
        {
            OnlineInfoProvider provider = new OnlineInfoProvider(zoneId);

            return provider.GetByManagerId( managerId,ref  activeFlag,ref  loginTime,ref  activeTime,ref  onlineMinutes,trans);
            
        }
		
		#endregion
        
		#region  GetOnlineMinutes
		
        public static bool GetOnlineMinutes ( System.Guid managerId,ref  System.Boolean activeFlag,ref  System.DateTime guildInTime,ref  System.Int32 cntOnlineMinutes,DbTransaction trans=null,string zoneId="")
        {
            OnlineInfoProvider provider = new OnlineInfoProvider(zoneId);

            return provider.GetOnlineMinutes( managerId,ref  activeFlag,ref  guildInTime,ref  cntOnlineMinutes,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(OnlineInfoEntity onlineInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new OnlineInfoProvider(zoneId);
            return provider.Insert(onlineInfoEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(OnlineInfoEntity onlineInfoEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new OnlineInfoProvider(zoneId);
            return provider.Update(onlineInfoEntity,trans);
        }
		
		#endregion	
		
		
	}
}

