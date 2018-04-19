using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Revelation;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;


namespace Games.NBall.ServiceContract.Client.Client
{
    public class RevelationClient
    {
        private static IRevelationService proxy = ServiceProxy<IRevelationService>.Create("NetTcp_IRevelationService");

        /// <summary>
        /// 根据经理ID获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetManagerResponse RevelationGetManagerId(Guid managerId)
        {
            return proxy.RevelationGetManagerId(managerId);
        }

        /// <summary>
        /// 开始一个大关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="markId"></param>
        /// <returns></returns>
        public RevelationStartMarkResponse StartMark(Guid managerId, int markId)
        {
            return proxy.StartMark(managerId, markId);
        }

        /// <summary>
        /// 根据关卡获取关卡信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetIsGeneralResponse GetMarkInfo(Guid managerId)
        {
            return proxy.GetMarkInfo(managerId);
        }

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns></returns>
        public RevelationTheGameResponse RevelationTheGame(Guid managerId)
        {
            return proxy.RevelationTheGame(managerId);
        }

        /// <summary>
        /// 获取翻牌信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        public RevelationDrawResponse GetDrawInfo(Guid managerId, Guid drawId)
        {
            return proxy.GetDrawInfo(managerId, drawId);
        }

        /// <summary>
        /// 翻牌结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        public RevelationDrawResultResponse DrawResult(Guid managerId, Guid drawId)
        {
            return proxy.DrawResult(managerId, drawId);
        }

        /// <summary>
        /// 增加士气
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationAddChallengeResponse RevelationAddMorale(Guid managerId)
        {
            return proxy.RevelationAddMorale(managerId);
        }

        /// <summary>
        /// 重置关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse RevelationResetMark(Guid managerId)
        {
            return proxy.RevelationResetMark(managerId);
        }

        /// <summary>
        /// 获取勇气商城信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetShopResponse RevelationGetShopInfo(Guid managerId)
        {
            return proxy.RevelationGetShopInfo(managerId);
        }

        /// <summary>
        /// 勇气商城兑换物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public RevelationPurchaseItemsResponse RevelationPurchaseItems(Guid managerId, int idx)
        {
            return proxy.RevelationPurchaseItems(managerId, idx);
        }
    }
}
