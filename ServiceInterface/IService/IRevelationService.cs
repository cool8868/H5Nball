using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Revelation;

namespace Games.NBall.ServiceContract.IService
{
    /// <summary>
    /// 球星启示录接口类
    /// </summary>
     [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IRevelationService
    {
        /// <summary>
        /// 根据经理ID获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationGetManagerResponse RevelationGetManagerId(Guid managerId);

        /// <summary>
        /// 开始一个大关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="markId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationStartMarkResponse StartMark(Guid managerId, int markId);

        /// <summary>
        /// 根据关卡获取关卡信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationGetIsGeneralResponse GetMarkInfo(Guid managerId);

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns></returns>
        [OperationContract]
        RevelationTheGameResponse RevelationTheGame(Guid managerId);

        /// <summary>
        /// 获取翻牌信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationDrawResponse GetDrawInfo(Guid managerId, Guid drawId);

        /// <summary>
        /// 翻牌结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        [OperationContract]
         RevelationDrawResultResponse DrawResult(Guid managerId, Guid drawId);

        /// <summary>
        /// 增加士气
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationAddChallengeResponse RevelationAddMorale(Guid managerId);

        /// <summary>
        /// 重置关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse RevelationResetMark(Guid managerId);

        /// <summary>
        /// 获取勇气商城信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationGetShopResponse RevelationGetShopInfo(Guid managerId);

        /// <summary>
        /// 勇气商城兑换物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        [OperationContract]
        RevelationPurchaseItemsResponse RevelationPurchaseItems(Guid managerId, int idx);
    }
}
