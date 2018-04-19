using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Task
{
    /// <summary>
    /// 任务列表响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class TaskListResponse : BaseResponse<TaskListEntity>
    {

    }

    /// <summary>
    /// 任务列表
    /// </summary>
    [Serializable]
    [DataContract]
    public class TaskListEntity
    {
        /// <summary>
        /// 任务列表
        /// </summary>
        [DataMember]
        public List<TaskEntity> Tasks { get; set; }

        /// <summary>
        /// 今日任务次数
        /// DailyCount=MaxDailyCount时不返回DailyTask，提示 您今日任务次数已用完，请明日再来
        /// </summary>
        [DataMember]
        public int DailyCount { get; set; }
        /// <summary>
        /// 最大每日任务次数
        /// </summary>
        [DataMember]
        public int MaxDailyCount { get; set; }
        [DataMember]
        public List<TaskEntity> DailyTasks { get; set; }
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
    


    [Serializable]
    [DataContract]
    public class TaskEntity
    {
        ///<summary>
        ///Idx,当经理等级未到时，任务 idx为负数
        ///</summary>
        [DataMember]
        public System.Int32 Idx { get; set; }
        
        ///<summary>
        ///任务id
        ///</summary>
        [DataMember]
        public System.Int32 TaskId { get; set; }

        /// <summary>
        /// 任务状态：0，初始；1，已完成；2，放弃；-1，表示经理等级未到
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 当前次数
        /// </summary>
        [DataMember]
        public int CurTimes { get; set; }

        /// <summary>
        /// 任务执行情况
        /// </summary>
        public string StepRecord { get; set; }
    }
}
