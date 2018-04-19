using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Activity;
//using Games.NBall.Entity.Response.Active;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Manager;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IActivityService
    {
        /// <summary>
        /// 获取活动信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [OperationContract]
        ActivityRecordResponse GetActivityInfo(Guid managerId, int activityId);

        /// <summary>
        /// 领取活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="activityId"></param>
        /// <param name="activityStep"></param>
        /// <returns></returns>
        [OperationContract]
        ActivityRecordResponse PrizeReceive(Guid managerId, int activityId, int activityStep);

        /// <summary>
        /// 获取精彩活动列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ActivityExListResponse GetActivityExList();

        /// <summary>
        /// 获取用户活动信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [OperationContract]
        ActivityExRecordListResponse GetUserActivityEx(Guid managerId, int activityId);

        /// <summary>
        /// 领取精彩活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exRecordId"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        [OperationContract]
        ActivityExReceivePrizeResponse ExPrizeReceive(Guid managerId, int exRecordId, int itemCode = -1, int iscount = 0);

        [OperationContract(IsOneWay = true)]
        void ActivityHandler(int requireId, Guid managerId, int exData, EnumActivityExDataPlusType exDataPlusType);
        /// <summary>
        /// 领取指定球员奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exRecordId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [OperationContract]
        ActivityExReceivePrizeResponse TeammemberReceive(Guid managerId, int exRecordId, int playerId);

        /// <summary>
        /// 返回是否能够购买vip礼包和礼包参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        NBManagerVipPackageResponse HasVipPackage(Guid managerId);

        /// <summary>
        /// 购买VIP礼包
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        [OperationContract]
        BuyVipPackageResponse BuyVipPackage(Guid managerId, int packageId);

        /// <summary>
        /// 分享游戏奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="shareType"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse DoShare(Guid managerId, int shareType);

        /// <summary>
        /// 获取用户分享信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="shareType"></param>
        /// <returns></returns>
        [OperationContract]
        ShareGetResponse GetShareInfo(Guid managerId, int shareType);

        /// <summary>
        /// 分享任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse ShareTask(Guid managerId);

        /// <summary>
        /// 获取奥运金牌数量
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        OlympicGetInfoResponse GetManagerInfo(Guid managerId);

        /// <summary>
        /// 兑换奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exChangerizeType"></param>
        /// <returns></returns>
        [OperationContract]
        OlympicExchangeResponse Exchange(Guid managerId, int exChangerizeType);

        //[OperationContract]
        //string ResetCache(int cacheType);

        ///// <summary>
        ///// 获取某个经理的活跃度信息
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //[OperationContract]
        //ActiveGetManagerListResponse GetActiveList(Guid managerId);

        ///// <summary>
        ///// 获取某个经理的活跃度信息
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //[OperationContract]
        //ActiveGetPrizeListResponse GetPrizeList(Guid managerId);

        ///// <summary>
        ///// 领取活跃度奖励
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="integralLevel"></param>
        ///// <returns></returns>
        //[OperationContract]
        //DrawActivePrizeResponse DrawPrize(Guid managerId);

        ///// <summary>
        ///// 投资理财 信息
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //[OperationContract]
        //InvestInfoResponse InvestInfo(Guid managerId);
        ///// <summary>
        ///// 投资理财 存入
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="step"></param>
        ///// <returns></returns>
        //[OperationContract]
        //InvestInfoResponse InvestDeposit(Guid managerId, int step);

        ///// <summary>
        ///// 投资理财 领取
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //[OperationContract]
        //InvestInfoResponse ReceiveBindPoint(Guid managerId, int index);

        ///// <summary>
        ///// 投资理财 购买月卡
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //[OperationContract]
        //InvestInfoResponse InvestDepositMonth(Guid managerId);

        ///// <summary>
        ///// 投资理财 月卡每日领取
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="once"></param>
        ///// <returns></returns>
        //[OperationContract]
        //InvestInfoResponse ReceiveBindPointPerDay(Guid managerId, bool once);
    }
}
