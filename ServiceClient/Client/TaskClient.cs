using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Task;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class TaskClient
    {
        private static ITaskService proxy = ServiceProxy<ITaskService>.Create("NetTcp_ITaskService");
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TaskListResponse GetTaskListResponse(Guid managerId)
        {
            return proxy.GetTaskListResponse(managerId);
        }

        /// <summary>
        /// 获取已完成任务列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TaskListResponse GetTaskCompleteListResponse(Guid managerId)
        {
            return proxy.GetTaskCompleteListResponse(managerId);
        }
        

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="managerId">经理id</param>
        /// <param name="recordId">任务记录表id</param>
        /// <returns></returns>
        public SubmitTaskResponse SubmitTask(Guid managerId, int recordId)
        {
            return proxy.SubmitTask(managerId, recordId);
        }
        
        /// <summary>
        /// 提交每日任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public SubmitDailyTaskResponse SubmitDailyTask(Guid managerId, int taskId)
        {
            return proxy.SubmitDailyTask(managerId, taskId);
        }

        /// <summary>
        /// 领取新手引导次日奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse ReceiveGuidePrize(Guid managerId)
        {
            return proxy.ReceiveGuidePrize(managerId);
        }

        ///// <summary>
        ///// 立即完成每日任务
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public DailyTaskResponse QuickFinishDailyTask(Guid managerId)
        //{
        //    return proxy.QuickFinishDailyTask(managerId);
        //}

        ///// <summary>
        ///// 领取每日任务通关奖励
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public SubmitDailyTaskResponse ReceiveFinishPrize(Guid managerId)
        //{
        //    return proxy.ReceiveFinishPrize(managerId);
        //}
    }
}
