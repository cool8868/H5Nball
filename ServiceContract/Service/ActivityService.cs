using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
//using Games.NBall.Core.Active;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Turntable;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Activity;
//using Games.NBall.Entity.Response.Active;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Manager;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall,
        AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class ActivityService : IActivityService
    {
        /// <summary>
        /// 获取活动信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public ActivityRecordResponse GetActivityInfo(Guid managerId, int activityId)
        {
            return ResponseHelper.TryCatch(() => ActivityCore.Instance.GetActivityInfo(managerId, activityId));
        }

        /// <summary>
        /// 领取活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="activityId"></param>
        /// <param name="activityStep"></param>
        /// <returns></returns>
        public ActivityRecordResponse PrizeReceive(Guid managerId, int activityId, int activityStep)
        {
            return ResponseHelper.TryCatch(() => ActivityCore.Instance.PrizeReceive(managerId, activityId, activityStep));
        }

        /// <summary>
        /// 获取精彩活动列表
        /// </summary>
        /// <returns></returns>
        public ActivityExListResponse GetActivityExList()
        {
            return ResponseHelper.TryCatch(() => ActivityExThread.Instance.GetActivityExList());
        }

        /// <summary>
        /// 获取用户活动信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public ActivityExRecordListResponse GetUserActivityEx(Guid managerId, int activityId)
        {
            return ResponseHelper.TryCatch(() => ActivityExThread.Instance.GetUserActivityEx(managerId, activityId));
        }

        /// <summary>
        /// 领取精彩活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exRecordId"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public ActivityExReceivePrizeResponse ExPrizeReceive(Guid managerId, int exRecordId, int itemCode = -1,int iscount=0)
        {
            return ResponseHelper.TryCatch(() => ActivityExThread.Instance.PrizeReceive(managerId, exRecordId, itemCode, iscount));
        }

        public void ActivityHandler(int requireId, Guid managerId, int exData, EnumActivityExDataPlusType exDataPlusType)
        {
            ActivityExThread.Instance.ActivityHandler(requireId, managerId, exData, exDataPlusType);
        }

        /// <summary>
        /// 领取指定球员奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exRecordId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public ActivityExReceivePrizeResponse TeammemberReceive(Guid managerId, int exRecordId, int playerId)
        {
            return
                ResponseHelper.TryCatch(
                    () => ActivityExThread.Instance.TeammemberReceive(managerId, exRecordId, playerId));
        }

        public string ResetCache(int cacheType)
        {
            return CacheManager.Instance.ResetCache(cacheType);
        }

        /// <summary>
        /// 返回是否能够购买vip礼包和礼包参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>

        public NBManagerVipPackageResponse HasVipPackage(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => ActivityCore.Instance.HasVipPackage(managerId));
        }

        /// <summary>
        /// 购买VIP礼包
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public BuyVipPackageResponse BuyVipPackage(Guid managerId, int packageId)
        {
            return ResponseHelper.TryCatch(() => ActivityCore.Instance.BuyVipPackage(managerId, packageId));
        }

        /// <summary>
        /// 分享游戏奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="shareType"></param>
        /// <returns></returns>
        public MessageCodeResponse DoShare(Guid managerId, int shareType)
        {
            return ResponseHelper.TryCatch(()=>ShareCore.Instance.DoShare(managerId,shareType));
        }

        /// <summary>
        /// 获取用户分享信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="shareType"></param>
        /// <returns></returns>
        public ShareGetResponse GetShareInfo(Guid managerId, int shareType)
        {
            return ResponseHelper.TryCatch(() => ShareCore.Instance.GetShareInfo(managerId, shareType));
        }

        /// <summary>
        /// 分享任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse ShareTask(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => ShareCore.Instance.ShareTask(managerId));
        }


        /// <summary>
        /// 获取奥运金牌数量
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public OlympicGetInfoResponse GetManagerInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => OlympicCore.Instance.GetManagerInfo(managerId));
        }

        /// <summary>
        /// 兑换奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exChangerizeType"></param>
        /// <returns></returns>
        public OlympicExchangeResponse Exchange(Guid managerId, int exChangerizeType)
        {
            return ResponseHelper.TryCatch(() => OlympicCore.Instance.Exchange(managerId, exChangerizeType));
        }

        ///// <summary>
        ///// 获取某个经理的活跃度信息
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public ActiveGetManagerListResponse GetActiveList(Guid managerId)
        //{
        //    return ResponseHelper.TryCatch(() => ActiveCore.Instance.GetActiveList(managerId));
        //}

        ///// <summary>
        ///// 获取某个经理的活跃度信息
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public ActiveGetPrizeListResponse GetPrizeList(Guid managerId)
        //{
        //    return ResponseHelper.TryCatch(() => ActiveCore.Instance.GetPrizeList(managerId));
        //}

        ///// <summary>
        ///// 领取活跃度奖励
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="integralLevel"></param>
        ///// <returns></returns>
        //public DrawActivePrizeResponse DrawPrize(Guid managerId)
        //{
        //    return ResponseHelper.TryCatch(() => ActiveCore.Instance.DrawPrize(managerId));
        //}

        ///// <summary>
        ///// 投资理财 信息
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public InvestInfoResponse InvestInfo(Guid managerId)
        //{
        //    return ResponseHelper.TryCatch(() => InvestCore.Instance.InvestInfo(managerId));
        //}

        ///// <summary>
        ///// 投资理财 存入
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="step"></param>
        ///// <returns></returns>
        //public InvestInfoResponse InvestDeposit(Guid managerId, int step)
        //{
        //    return ResponseHelper.TryCatch(() => InvestCore.Instance.InvestDeposit(managerId, step));
        //}

        ///// <summary>
        ///// 投资理财 领取
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public InvestInfoResponse ReceiveBindPoint(Guid managerId, int index)
        //{
        //    return ResponseHelper.TryCatch(() => InvestCore.Instance.ReceiveBindPoint(managerId, index));
        //}

        //public InvestInfoResponse InvestDepositMonth(Guid managerId)
        //{
        //    return ResponseHelper.TryCatch(() => InvestCore.Instance.InvestDepositMonth(managerId));
        //}

        //public InvestInfoResponse ReceiveBindPointPerDay(Guid managerId, bool once)
        //{
        //    return ResponseHelper.TryCatch(() => InvestCore.Instance.ReceiveBindPointPerDay(managerId, once));
        //}
    }
}
