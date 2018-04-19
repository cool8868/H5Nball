using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Task
{
    /// <summary>
    /// 每日任务响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailyTaskResponse : BaseResponse<DailyTaskEntity>
    {
    }

    /// <summary>
    /// 每日任务
    /// </summary>
    [Serializable]
    [DataContract]
    public class DailyTaskEntity
    {
        /// <summary>
        /// 任务
        /// </summary>
        [DataMember]
        public List<TaskEntity> Tasks { get; set; }

        /// <summary>
        /// 今日任务次数
        /// DailyCount=MaxDailyCount时不返回TaskEntity，提示 您今日任务次数已用完，请明日再来
        /// </summary>
        [DataMember]
        public int DailyCount { get; set; }
        /// <summary>
        /// 最大每日任务次数
        /// </summary>
        [DataMember]
        public int MaxDailyCount { get; set; }
        /// <summary>
        /// 经理剩余点券,为-1时不更新
        /// </summary>
        [DataMember]
        public int ManagerPoint { get; set; }
        /// <summary>
        /// 是否已领取通关奖励
        /// </summary>
        [DataMember]
        public bool IsReceive { get; set; }
        /// <summary>
        /// 完成每日任务数量，FinishCount==MaxDailyCount时3008可领奖
        /// </summary>
        [DataMember]
        public int FinishCount { get; set; }
    }
}
