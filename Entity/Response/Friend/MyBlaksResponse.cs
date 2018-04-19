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
    public class MyBlacksResponse : BaseResponse<MyBlacksData>
    {
    }

    [Serializable]
    [DataContract]
    public class MyBlacksData
    {
        /// <summary>
        /// 好友列表
        /// </summary>
        [DataMember]
        public List<MyBlacksEntity> Blaks { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember]
        public int TotalPage { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }
    }

    [Serializable]
    [DataContract]
    public class MyBlacksEntity
    {
        /// <summary>
        /// 记录id
        /// </summary>
        [DataMember]
        public int Idx { get; set; }
        /// <summary>
        /// blak经理id
        /// </summary>
        [DataMember]
        public Guid FriendId { get; set; }
        /// <summary>
        /// 经理名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        [DataMember]
        public int Level { get; set; }
    }
}
