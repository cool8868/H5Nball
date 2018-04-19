
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
    /// UserloginZone管理类
    /// </summary>
    public static partial class UserloginZoneMgr
    {
        
		#region  GetById
		
        public static UserloginZoneEntity GetById( System.String account,string zoneId="")
        {
            var provider = new UserloginZoneProvider(zoneId);
            return provider.GetById( account);
        }
		
		#endregion		  
		
		#region  GetByAccountPlatform
		
        public static UserloginZoneEntity GetByAccountPlatform( System.String account, System.String platform,string zoneId="")
        {
            var provider = new UserloginZoneProvider(zoneId);
            return provider.GetByAccountPlatform( account, platform);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<UserloginZoneEntity> GetAll(string zoneId="")
        {
            var provider = new UserloginZoneProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String account,DbTransaction trans=null,string zoneId="")
        {
            UserloginZoneProvider provider = new UserloginZoneProvider(zoneId);

            return provider.Delete( account,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(UserloginZoneEntity userloginZoneEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new UserloginZoneProvider(zoneId);
            return provider.Insert(userloginZoneEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(UserloginZoneEntity userloginZoneEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new UserloginZoneProvider(zoneId);
            return provider.Update(userloginZoneEntity,trans);
        }
		
		#endregion	
		
		
	}
}

