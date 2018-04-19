using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Revelation;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Revelation;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class RevelationService:IRevelationService
    {
        /// <summary>
        /// 根据经理ID获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetManagerResponse RevelationGetManagerId(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationGetManagerId(managerId));
        }

        /// <summary>
        /// 开始一个大关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="markId"></param>
        /// <returns></returns>
        public RevelationStartMarkResponse StartMark(Guid managerId, int markId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.StartMark(managerId, markId));
        }

        /// <summary>
        /// 根据关卡获取关卡信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetIsGeneralResponse GetMarkInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.GetMarkInfo(managerId));
        }

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns></returns>
        public RevelationTheGameResponse RevelationTheGame(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationTheGame(managerId));
        }

        /// <summary>
        /// 获取翻牌信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        public RevelationDrawResponse GetDrawInfo(Guid managerId, Guid drawId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.GetDrawInfo(managerId, drawId));
        }

        /// <summary>
        /// 翻牌结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        public RevelationDrawResultResponse DrawResult(Guid managerId, Guid drawId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.DrawResult(managerId, drawId));
        }

        /// <summary>
        /// 增加士气
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationAddChallengeResponse RevelationAddMorale(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationAddMorale(managerId));
        }

        /// <summary>
        /// 重置关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse RevelationResetMark(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationResetMark(managerId));
        }

        /// <summary>
        /// 获取勇气商城信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetShopResponse RevelationGetShopInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationGetShopInfo(managerId));
        }

        /// <summary>
        /// 勇气商城兑换物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public RevelationPurchaseItemsResponse RevelationPurchaseItems(Guid managerId, int idx)
        {
            return ResponseHelper.TryCatch(() => RevelationNewCore.Instance.RevelationPurchaseItems(managerId, idx));
        }
    }
}
