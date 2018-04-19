
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
    /// ConfigFriendinviteprize管理类
    /// </summary>
    public static partial class ConfigFriendinviteprizeMgr
    {
        
		#region  GetById
		
        public static ConfigFriendinviteprizeEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigFriendinviteprizeProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigFriendinviteprizeEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigFriendinviteprizeProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigFriendinviteprizeProvider provider = new ConfigFriendinviteprizeProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigFriendinviteprizeEntity configFriendinviteprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigFriendinviteprizeProvider(zoneId);
            return provider.Insert(configFriendinviteprizeEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigFriendinviteprizeEntity configFriendinviteprizeEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigFriendinviteprizeProvider(zoneId);
            return provider.Update(configFriendinviteprizeEntity,trans);
        }
		
		#endregion	
		
		
	}
}

