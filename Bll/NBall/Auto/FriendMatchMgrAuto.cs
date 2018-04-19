
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
    /// FriendMatch管理类
    /// </summary>
    public static partial class FriendMatchMgr
    {
        
		#region  GetById
		
        public static FriendMatchEntity GetById( System.Guid idx,string zoneId="")
        {
            var provider = new FriendMatchProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<FriendMatchEntity> GetAll(string zoneId="")
        {
            var provider = new FriendMatchProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid idx,DbTransaction trans=null,string zoneId="")
        {
            FriendMatchProvider provider = new FriendMatchProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(FriendMatchEntity friendMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendMatchProvider(zoneId);
            return provider.Insert(friendMatchEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(FriendMatchEntity friendMatchEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendMatchProvider(zoneId);
            return provider.Update(friendMatchEntity,trans);
        }
		
		#endregion	
		
		
	}
}

