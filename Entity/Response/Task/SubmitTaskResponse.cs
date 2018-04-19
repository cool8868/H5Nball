using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Task
{
    /// <summary>
    /// 提交任务响应
    /// </summary>
    [DataContract]
    [Serializable]
    public class SubmitTaskResponse:BaseResponse<SubmitTaskEntity>
    {

    }

    /// <summary>
    /// 提交任务
    /// </summary>
    [DataContract]
    [Serializable]
    public class SubmitTaskEntity
    {
        /// <summary>
        /// 奖励经验
        /// </summary>
        [DataMember]
        public int PrizeExp { get; set; }
        /// <summary>
        /// 奖励游戏币
        /// </summary>
        [DataMember]
        public int PrizeCoin { get; set; }
        /// <summary>
        /// 经理当前经验
        /// </summary>
        [DataMember]
        public int ManagerExp { get; set; }
        /// <summary>
        /// 经理当前等级
        /// IsLevelup为true时用到
        /// </summary>
        [DataMember]
        public int ManagerLevel { get; set; }
        /// <summary>
        /// 升级所需经验
        /// IsLevelup为true时用到
        /// </summary>
        [DataMember]
        public int LevelupExp { get; set; }
        /// <summary>
        /// 是否升级
        /// </summary>
        [DataMember]
        public bool IsLevelup { get; set; }
        /// <summary>
        /// 经理当前游戏币
        /// </summary>
        [DataMember]
        public int ManagerCoin { get; set; }
        /// <summary>
        /// 奖励物品编码
        /// </summary>
        [DataMember]
        public int PrizeItemCode { get; set; }
        /// <summary>
        /// 新开放的任务
        /// </summary>
        [DataMember]
        public List<TaskEntity> NewTasks { get; set; }


        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }

    /// <summary>
    /// 提交任务响应
    /// </summary>
    [DataContract]
    [Serializable]
    public class SubmitDailyTaskResponse:BaseResponse<SubmitDailyTaskEntity>
    {

    }

    /// <summary>
    /// 提交每日任务
    /// </summary>
    [DataContract]
    [Serializable]
    public class SubmitDailyTaskEntity : SubmitTaskEntity
    {
        /// <summary>
        /// 每日任务
        /// </summary>
        [DataMember]
        public List<TaskEntity> DailyTasks { get; set; }

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
