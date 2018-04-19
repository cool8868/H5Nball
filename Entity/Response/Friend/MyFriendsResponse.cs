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
    public class MyFriendsResponse : BaseResponse<MyFriendsData>
    {
    }

    [Serializable]
    [DataContract]
    public class MyFriendsData
    {
        /// <summary>
        /// 好友列表
        /// </summary>
        [DataMember]
        public List<MyFriendsEntity> Friends { get; set; }

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

        /// <summary>
        /// 今日可帮助次数
        /// </summary>
        [DataMember]
        public int DayHelpTrainCount { get; set; }
    }

    [Serializable]
    [DataContract]
    public class MyFriendsEntity
    {
        /// <summary>
        /// 记录id
        /// </summary>
        [DataMember]
        public int Idx { get; set; }
        /// <summary>
        /// friend经理id
        /// </summary>
        [DataMember]
        public Guid FriendId { get; set; }
        /// <summary>
        /// 亲密度
        /// </summary>
        [DataMember]
        public int Intimacy{ get; set; }
        /// <summary>
        /// 经理名
        /// </summary>
        [DataMember]
        public string Name{ get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        [DataMember]
        public int Level{ get; set; }
        /// <summary>
        /// 今日可帮助次数
        /// </summary>
        [DataMember]
        public int DayCanHelpTrainCount { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        [DataMember]
        public bool IsOnline { get; set; }

        /// <summary>
        /// 我今天帮助了他多少次
        /// </summary>
        [DataMember]
        public int DayHelpTrainCount { get; set; }

        /// <summary>
        /// 被帮助训练次数
        /// </summary>
        public int ByHelpTrainCount{ get; set; }

        /// <summary>
        /// 今日可挑战次数
        /// </summary>
        [DataMember]
        public int DayCanMatchCount { get; set; }
        /// <summary>
        /// 今日已挑战次数
        /// </summary>
        public int DayMatchCount { get; set; }


        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime RecordDate{ get; set; }
        /// <summary>
        /// 好友记录日期
        /// </summary>
        public DateTime FRecordDate { get; set; }
        /// <summary>
        /// 训练经理id，用于判断是否在训练
        /// </summary>
        public Guid TrainManagerId { get; set; }
        /// <summary>
        /// 经理等级
        /// </summary>
        public int VipLevel { get; set; }

        /// <summary>
        /// 是否在训练
        /// </summary>
        [DataMember]
        public bool IsTrain { get; set; }
    }
}
