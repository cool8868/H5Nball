using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Friend
{
    [Serializable]
    [DataContract]
    public class FriendsAddInfoResponse : BaseResponse<FriendsAddInfo>
    {
    }

    [Serializable]
    [DataContract]
    public class FriendsAddInfo
    {
        /// <summary>
        /// 好友列表
        /// </summary>
        [DataMember]
        public List<FriendsAdd> FriendAddList { get; set; }
    }

    [Serializable]
    [DataContract]
    public class FriendsAdd
    {
        /// <summary>
        /// friend经理id
        /// </summary>
        [DataMember]
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 经理名
        /// </summary>
        [DataMember]
        public string Name{ get; set; }
    }
}
