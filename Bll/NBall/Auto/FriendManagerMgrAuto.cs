
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
    /// FriendManager管理类
    /// </summary>
    public static partial class FriendManagerMgr
    {
        
		#region  GetById
		
        public static FriendManagerEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new FriendManagerProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetOne
		
        public static FriendManagerEntity GetOne( System.Guid managerId, System.Guid friendId,string zoneId="")
        {
            var provider = new FriendManagerProvider(zoneId);
            return provider.GetOne( managerId, friendId);
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null,string zoneId="")
        {
            FriendManagerProvider provider = new FriendManagerProvider(zoneId);

            return provider.Delete( idx, rowVersion,trans);
            
        }
		
		#endregion
        
		#region  AddBlack
		
        public static bool AddBlack ( System.Guid managerId, System.Guid blackId,DbTransaction trans=null,string zoneId="")
        {
            FriendManagerProvider provider = new FriendManagerProvider(zoneId);

            return provider.AddBlack( managerId, blackId,trans);
            
        }
		
		#endregion
        
		#region  AddFriend
		
        public static bool AddFriend ( System.Guid managerId, System.Guid friendId, System.Int32 maxCount, System.Int32 maxMessageCode, System.Int32 existsMessageCode,ref  System.Int32 returnCode,DbTransaction trans=null,string zoneId="")
        {
            FriendManagerProvider provider = new FriendManagerProvider(zoneId);

            return provider.AddFriend( managerId, friendId, maxCount, maxMessageCode, existsMessageCode,ref  returnCode,trans);
            
        }
		
		#endregion
        
		#region  GetTotal
		
        public static bool GetTotal ( System.Guid managerId,ref  System.Int32 totalRecord,DbTransaction trans=null,string zoneId="")
        {
            FriendManagerProvider provider = new FriendManagerProvider(zoneId);

            return provider.GetTotal( managerId,ref  totalRecord,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(FriendManagerEntity friendManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendManagerProvider(zoneId);
            return provider.Insert(friendManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(FriendManagerEntity friendManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendManagerProvider(zoneId);
            return provider.Update(friendManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}

