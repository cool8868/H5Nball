using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Activity
{
    /// <summary>
    /// 签到信息响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailyAttendanceInfoResponse : BaseResponse<DailyAttendanceInfo>
    {
    }

    /// <summary>
    /// 签到
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailyAttendanceInfo
    {
        /// <summary>
        /// 签到次数
        /// </summary>
        [DataMember]
        public int AttendTimes { get; set; }

        /// <summary>
        /// 最多签到次数
        /// </summary>
        [DataMember]
        public int MaxAttendTimes { get; set; }

        /// <summary>
        /// 当前是否已经签到
        /// </summary>
        [DataMember]
        public bool IsAttend { get; set; }
    }
}
