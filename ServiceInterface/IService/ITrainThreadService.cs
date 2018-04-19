using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Item;
using Games.NBall.Entity.Response.Teammember;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface ITrainThreadService
    {
        /// <summary>
        /// 球员训练里的球员列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        TeammemberTrainListResponse GetTeammemberListForTrain(Guid managerId);

        /// <summary>
        /// 球员开始训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        [OperationContract]
        TeammemberTrainResponse StartTrain(Guid managerId, Guid teammemberId);

        /// <summary>
        /// 球员结束训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse StopTrain(Guid managerId, Guid teammemberId);


        /// <summary>
        /// 获取球员训练信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        [OperationContract]
        TeammemberTrainResponse GetTrainInfo(Guid managerId, Guid teammemberId);

        /// <summary>
        /// 使用经验药水
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <param name="expType">经验药水级别1初级 2中级 3高级 4超级</param>
        /// <returns></returns>
        [OperationContract]
        TeammemberTrainSpeedUpResponse SpeedUpTrain(Guid managerId, Guid teammemberId, int expType);

        /// <summary>
        /// 获取好友训练列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="friendRecordId"></param>
        /// <returns></returns>
        [OperationContract]
        TeammemberTrainHelpResponse GetHelpTrainList(Guid managerId, int friendRecordId);

        /// <summary>
        /// 帮助好友训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="friendRecordId"></param>
        /// <param name="trainId"></param>
        /// <param name="hasTask"></param>
        /// <returns></returns>
        [OperationContract]
        TeammemberTrainActionResponse HelpTrain(Guid managerId, int friendRecordId, Guid trainId, bool hasTask);

        /// <summary>
        /// 开启好友宝箱
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="friendRecordId"></param>
        /// <returns></returns>
        [OperationContract]
        TeammemberTrainOpenBoxResponse OpenBox(Guid managerId, int friendRecordId);

    }
}
