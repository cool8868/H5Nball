using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Coach;
using Games.NBall.Entity.Response.Revelation;
using Games.NBall.Entity.Response.Scouting;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;
using Games.NBall.Entity.Response;

namespace Games.NBall.ServiceContract.Client
{
    public class ScoutingClient
    {
        private static IScoutingService proxy = ServiceProxy<IScoutingService>.Create("NetTcp_IScoutingService");
        /// <summary>
        /// 获取球探信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public ScoutingInfoResponse GetScoutingInfo(Guid managerId)
        {
            return proxy.GetScoutingInfo(managerId);
        }

        /// <summary>
        /// 球探抽卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="scoutingId"></param>
        /// <returns></returns>
        public ScoutingLotteryResponse ScoutingLottery(Guid managerId, int scoutingId, bool hasTask, int count, bool isAutoDec)
        {
            return proxy.ScoutingLottery(managerId, scoutingId,hasTask,count,isAutoDec);
        }

        /// <summary>
        /// 获取用户转盘信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetTurntableInfoResponse GetManagerInfo(Guid managerId)
        {
           return proxy.GetManagerInfo(managerId);
        }

        /// <summary>
        /// 转盘抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TurntableLuckDrawResponse TurntableLuckDraw(Guid managerId)
        {
            return proxy.TurntableLuckDraw(managerId);
        }

        /// <summary>
        /// 重置转盘
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TurntableResetResponse ResetTurntable(Guid managerId)
        {
            return proxy.ResetTurntable(managerId);
        }


        #region 球星启示录
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

        /// <summary>
        /// 勇气商城刷新物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetShopResponse RevelationShopRefresh(Guid managerId)
        {
             return proxy.RevelationShopRefresh(managerId);
        }

        #endregion


        #region 教练

        /// <summary>
        /// 获取教练列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public CoachGetListResponse GetCoachList(Guid managerId)
        {
            return proxy.GetCoachList(managerId);
        }

        /// <summary>
        /// 激活教练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse ActivationCoach(Guid managerId, int coachId)
        {
             return proxy.ActivationCoach(managerId, coachId);
        }

        /// <summary>
        /// 替换启用的教练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse ReplaceCoach(Guid managerId, int coachId)
        {
             return proxy.ReplaceCoach(managerId, coachId);
        }

        /// <summary>
        /// 教练经验升级
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse CoachUpgrade(Guid managerId, int coachId)
        {
             return proxy.CoachUpgrade(managerId, coachId);
        }

        /// <summary>
        /// 教练技能升级
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse CoachSkillUpgrade(Guid managerId, int coachId)
        {
             return proxy.CoachSkillUpgrade(managerId, coachId);
        }

        /// <summary>
        /// 教练升星
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachGetInfoResponse CoachStarUpgrade(Guid managerId, int coachId)
        {
             return proxy.CoachStarUpgrade(managerId, coachId);
        }

        #endregion
    }
}
