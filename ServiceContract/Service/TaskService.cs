using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Task;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class TaskService:ITaskService
    {
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TaskListResponse GetTaskListResponse(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => TaskCore.Instance.GetTaskListResponse(managerId));
        }

         /// <summary>
        /// 获取已完成任务列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TaskListResponse GetTaskCompleteListResponse(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => TaskCore.Instance.GetTaskCompleteListResponse(managerId));
        }
        

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="managerId">经理id</param>
        /// <param name="recordId">任务记录表id</param>
        /// <returns></returns>
        public SubmitTaskResponse SubmitTask(Guid managerId, int recordId)
        {
            return ResponseHelper.TryCatch(() => TaskCore.Instance.SubmitTask(managerId,recordId));
        }
        
        /// <summary>
        /// 提交每日任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public SubmitDailyTaskResponse SubmitDailyTask(Guid managerId, int taskId)
        {
            return ResponseHelper.TryCatch(() => TaskCore.Instance.SubmitDailyTask(managerId, taskId));
        }

        /// <summary>
        /// 领取新手引导次日奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse ReceiveGuidePrize(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => TaskCore.Instance.ReceiveGuidePrize(managerId));
        }
        ///// <summary>
        ///// 立即完成每日任务
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public DailyTaskResponse QuickFinishDailyTask(Guid managerId)
        //{
        //    return ResponseHelper.TryCatch(() => TaskCore.Instance.QuickFinishDailyTask(managerId));
        //}

        ///// <summary>
        ///// 领取每日任务通关奖励
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public SubmitDailyTaskResponse ReceiveFinishPrize(Guid managerId)
        //{
        //    return ResponseHelper.TryCatch(() => TaskCore.Instance.ReceiveFinishPrize(managerId));
        //}
    }
}
