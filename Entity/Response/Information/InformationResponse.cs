using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
//using Games.NBall.Entity.Response.Active;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Task;
using Games.NBall.Entity.Support.Custom;

namespace Games.NBall.Entity.Response.Information
{
    [Serializable]
    [DataContract]
    public class InformationResponse : BaseResponse<InformationEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class InformationEntity
    {
        /// <summary>
        /// 有完成任务
        /// </summary>
        [DataMember]
        public bool TaskFinish { get; set; }

        ///// <summary>
        ///// 活动信息
        ///// </summary>
        //[DataMember]
        //public bool NewActivity { get; set; }

        /// <summary>
        /// 有好友邀请
        /// </summary>
        [DataMember]
        public bool NewFriend { get; set; }

        /// <summary>
        /// 有未读邮件
        /// </summary>
        [DataMember]
        public bool MailUnRead { get; set; }

        /// <summary>
        /// 有未分配的天赋点
        /// </summary>
        [DataMember]
        public bool TalentUnUsed { get; set; }

        /// <summary>
        /// 可领取的活动
        /// </summary>
        [DataMember]
        public List<int> ActivityComplete { get; set; }

        /// <summary>
        /// 可领取的精彩活动
        /// </summary>
        [DataMember]
        public List<int> ActivityExComplete { get; set; }

        /// <summary>
        /// 是否可以签到
        /// </summary>
        [DataMember]
        public bool IsHaveDailyAttendance { get; set; }

        /// <summary>
        /// 是否有购买钻石
        /// </summary>
        [DataMember]
        public bool IsHaveBuyPoint { get; set; }

        /// <summary>
        /// 经理等级
        /// </summary>
        [DataMember]
        public int ManagerLevel { get; set; }

        /// <summary>
        /// 经理经验
        /// </summary>
        [DataMember]
        public int ManagerExp { get; set; }
    }

    
}
