using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Task;

namespace Games.NBall.ServiceContract.IService
{
    /// <summary>
    /// 任务接口
    /// </summary>
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface ITaskService
    {
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        TaskListResponse GetTaskListResponse(Guid managerId);

           /// <summary>
        /// 获取已完成任务列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        TaskListResponse GetTaskCompleteListResponse(Guid managerId);
        
        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="managerId">经理id</param>
        /// <param name="recordId">任务记录表id</param>
        /// <returns></returns>
        [OperationContract]
        SubmitTaskResponse SubmitTask(Guid managerId, int recordId);

        /// <summary>
        /// 提交每日任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [OperationContract]
        SubmitDailyTaskResponse SubmitDailyTask(Guid managerId, int taskId);
        /// <summary>
        /// 领取新手引导次日奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse ReceiveGuidePrize(Guid managerId);
        ///// <summary>
        ///// 立即完成每日任务
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //[OperationContract]
        //DailyTaskResponse QuickFinishDailyTask(Guid managerId);

        ///// <summary>
        ///// 领取每日任务通关奖励
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //[OperationContract]
        //SubmitDailyTaskResponse ReceiveFinishPrize(Guid managerId);
    }
}
