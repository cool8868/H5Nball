using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Scouting;
using Games.NBall.Entity.Response.Teammember;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class TrainThreadService : ITrainThreadService
    {
        /// <summary>
        /// 球员训练里的球员列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TeammemberTrainListResponse GetTeammemberListForTrain(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => PlayerTrain.Instance.GetTeammemberListForTrain(managerId));
        }

        /// <summary>
        /// 球员开始训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public TeammemberTrainResponse StartTrain(Guid managerId, Guid teammemberId)
        {
            //if (!ManagerUtil.CheckFunction(managerId, EnumOpenFunction.Train))
            //    return ResponseHelper.InvalidFunction<MessageCodeResponse>();
            return ResponseHelper.TryCatch(() => PlayerTrain.Instance.StartTrain(managerId, teammemberId));
        }

        /// <summary>
        /// 球员结束训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public MessageCodeResponse StopTrain(Guid managerId, Guid teammemberId)
        {
            return ResponseHelper.TryCatch(() => PlayerTrain.Instance.EndTrain(managerId, teammemberId));
        }


        /// <summary>
        /// 获取球员训练信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public TeammemberTrainResponse GetTrainInfo(Guid managerId, Guid teammemberId)
        {
            return ResponseHelper.TryCatch(() => PlayerTrain.Instance.GetTrainInfo(managerId, teammemberId));
        }

        /// <summary>
        /// 使用经验药水
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <param name="expType">经验药水级别1初级 2中级 3高级 4超级</param>
        /// <returns></returns>
        public TeammemberTrainSpeedUpResponse SpeedUpTrain(Guid managerId, Guid teammemberId, int expType)
        {
            return ResponseHelper.TryCatch(() => PlayerTrain.Instance.SpeedUpTrain(managerId, teammemberId, expType));
        }

        /// <summary>
        /// 获取好友训练列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="friendRecordId"></param>
        /// <returns></returns>
        public TeammemberTrainHelpResponse GetHelpTrainList(Guid managerId, int friendRecordId)
        {
            return ResponseHelper.TryCatch(() => PlayerTrain.Instance.GetHelpTrainList(managerId, friendRecordId));
        }

        /// <summary>
        /// 帮助好友训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="friendRecordId"></param>
        /// <param name="trainId"></param>
        /// <param name="hasTask"></param>
        /// <returns></returns>
        public TeammemberTrainActionResponse HelpTrain(Guid managerId, int friendRecordId, Guid trainId, bool hasTask)
        {
            return ResponseHelper.TryCatch(() => PlayerTrain.Instance.HelpTrain(managerId, friendRecordId, trainId, hasTask));
        }

        /// <summary>
        /// 开启好友宝箱
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="friendRecordId"></param>
        /// <returns></returns>
        public TeammemberTrainOpenBoxResponse OpenBox(Guid managerId, int friendRecordId)
        {
            return ResponseHelper.TryCatch(() => PlayerTrain.Instance.OpenBox(managerId, friendRecordId));
        }
    }
}
