using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Activity;
//using Games.NBall.Entity.Response.Active;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.Entity.Response.Manager;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class ActivityClient
    {
        private static IActivityService proxy = ServiceProxy<IActivityService>.Create("NetTcp_IActivityService");

        /// <summary>
        /// 获取活动信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public ActivityRecordResponse GetActivityInfo(Guid managerId, int activityId)
        {
            return proxy.GetActivityInfo(managerId, activityId);
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
            return proxy.PrizeReceive(managerId, activityId, activityStep);
        }

        /// <summary>
        /// 获取精彩活动列表
        /// </summary>
        /// <returns></returns>
        public ActivityExListResponse GetActivityExList()
        {
            return proxy.GetActivityExList();
        }

        /// <summary>
        /// 获取用户活动信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public ActivityExRecordListResponse GetUserActivityEx(Guid managerId, int activityId)
        {
            return proxy.GetUserActivityEx(managerId, activityId);
        }

        /// <summary>
        /// 领取精彩活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exRecordId"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public ActivityExReceivePrizeResponse ExPrizeReceive(Guid managerId, int exRecordId, int itemCode = -1, int iscount = 0)
        {
            return proxy.ExPrizeReceive(managerId, exRecordId, itemCode,iscount);
        }

        public void ActivityHandler(int requireId, Guid managerId, int exData, EnumActivityExDataPlusType exDataPlusType)
        {
            proxy.ActivityHandler(requireId, managerId, exData, exDataPlusType);
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
            return proxy.TeammemberReceive(managerId, exRecordId, playerId);
        }

        /// <summary>
        /// 返回是否能够购买vip礼包和礼包参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public NBManagerVipPackageResponse HasVipPackage(Guid managerId)
        {
            return proxy.HasVipPackage(managerId);
        }

        /// <summary>
        /// 购买VIP礼包
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public BuyVipPackageResponse BuyVipPackage(Guid managerId, int packageId)
        {
            return proxy.BuyVipPackage(managerId, packageId);
        }
        /// <summary>
        /// 分享游戏奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="shareType"></param>
        /// <returns></returns>

        public MessageCodeResponse DoShare(Guid managerId, int shareType = 1)
        {
            return proxy.DoShare(managerId, shareType);
        }

        /// <summary>
        /// 获取用户分享信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="shareType"></param>
        /// <returns></returns>
        public ShareGetResponse GetShareInfo(Guid managerId, int shareType)
        {
            return proxy.GetShareInfo(managerId, shareType);
        }

        /// <summary>
        /// 分享任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse ShareTask(Guid managerId)
        {
            return proxy.ShareTask(managerId);
        }

        /// <summary>
        /// 获取奥运金牌数量
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public OlympicGetInfoResponse GetManagerInfo(Guid managerId)
        {
             return proxy.GetManagerInfo(managerId);
        }

        /// <summary>
        /// 兑换奥运奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exChangerizeType"></param>
        /// <returns></returns>
        public OlympicExchangeResponse Exchange(Guid managerId, int exChangerizeType)
        {
            return proxy.Exchange(managerId, exChangerizeType);
        }

        //public string ResetCache(int cacheType)
        //{
        //    return proxy.ResetCache(cacheType);
        //}

        ///// <summary>
        ///// 获取某个经理的活跃度信息
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public ActiveGetManagerListResponse GetActiveList(Guid managerId)
        //{
        //    return proxy.GetActiveList(managerId);
        //}

        ///// <summary>
        ///// 获取某个经理的活跃度信息
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public ActiveGetPrizeListResponse GetPrizeList(Guid managerId)
        //{
        //    return proxy.GetPrizeList(managerId);
        //}

        ///// <summary>
        ///// 领取活跃度奖励
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="integralLevel"></param>
        ///// <returns></returns>
        //public DrawActivePrizeResponse DrawPrize(Guid managerId)
        //{
        //    return proxy.DrawPrize(managerId);
        //}


        ///// <summary>
        ///// 投资理财 信息
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public InvestInfoResponse InvestInfo(Guid managerId)
        //{
        //    return proxy.InvestInfo(managerId);
        //}

        ///// <summary>
        ///// 投资理财 存入
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="step"></param>
        ///// <returns></returns>
        //public InvestInfoResponse InvestDeposit(Guid managerId, int step)
        //{
        //    return proxy.InvestDeposit(managerId, step);
        //}

        ///// <summary>
        ///// 投资理财 领取
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="index"></param>
        ///// <returns></returns>
        //public InvestInfoResponse ReceiveBindPoint(Guid managerId, int index)
        //{
        //    return proxy.ReceiveBindPoint(managerId, index);
        //}

        ///// <summary>
        ///// 投资理财 充月卡
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <returns></returns>
        //public InvestInfoResponse InvestDepositMonth(Guid managerId)
        //{
        //    return proxy.InvestDepositMonth(managerId);
        //}

        ///// <summary>
        ///// 投资理财 每日领取
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="once"></param>
        ///// <returns></returns>
        //public InvestInfoResponse ReceiveBindPointPerDay(Guid managerId, bool once)
        //{
        //    return proxy.ReceiveBindPointPerDay(managerId, once);
        //}
    }
}
