
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;
using Games.NBall.Entity.Response.Friend;

namespace Games.NBall.Bll
{
    
    public partial class FriendManagerMgr
    {
        public static List<MyFriendsEntity> GetMyFriends(System.Guid managerId, System.Int32 pageIndex, System.Int32 pageSize, ref  System.Int32 totalRecord)
        {
            FriendManagerProvider provider = new FriendManagerProvider();

            return provider.GetMyFriends(managerId, pageIndex, pageSize, ref totalRecord);
        }

        public static List<MyBlacksEntity> GetMyBlacks(System.Guid managerId, System.Int32 pageIndex, System.Int32 pageSize, ref  System.Int32 totalRecord)
        {
            FriendManagerProvider provider = new FriendManagerProvider();

            return provider.GetMyBlacks(managerId, pageIndex, pageSize, ref totalRecord);
        }

        public static List<FriendsAdd> GetFriendAddList(System.Guid managerId)
        {
            FriendManagerProvider provider = new FriendManagerProvider();

            return provider.GetFriendAddList(managerId);
        }

        public static bool IgnoreAddFriend(System.Guid managerId, System.Guid friendId, ref int returnCode)
        {
            FriendManagerProvider provider = new FriendManagerProvider();

            return provider.IgnoreAddFriend(managerId, friendId,ref returnCode);
        }


	}
}

