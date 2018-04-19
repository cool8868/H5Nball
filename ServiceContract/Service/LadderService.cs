using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Rank;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Item;
using Games.NBall.Entity.Response.Ladder;
using Games.NBall.Entity.Response.Rank;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class LadderService : ILadderService
    {

        /// <summary>
        /// 获取天梯赛信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LadderManagerResponse GetManagerInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch<LadderManagerResponse>(() => LadderCore.Instance.GetManagerInfo(managerId));
        }

        /// <summary>
        /// Attends the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse AttendLadder(Guid managerId, bool hasTask, bool isGuide = false)
        {
            //if (!ManagerUtil.CheckFunction(managerId, EnumOpenFunction.Ladder))
            //    return ResponseHelper.InvalidFunction<MessageCodeResponse>();
            return ResponseHelper.TryCatch<MessageCodeResponse>(() => LadderCore.Instance.AttendLadder(managerId, hasTask, isGuide));
        }

        /// <summary>
        /// Leaves the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse LeaveLadder(Guid managerId)
        {
            return ResponseHelper.TryCatch<MessageCodeResponse>(() => LadderCore.Instance.LeaveLadder(managerId));
        }

        /// <summary>
        /// Ladders the heart.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public Ladder_HeartResponse LadderHeart(Guid managerId)
        {
            return ResponseHelper.TryCatch<Ladder_HeartResponse>(() => LadderCore.Instance.LadderHeart(managerId));
        }

        /// <summary>
        /// Fives the match.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public LadderMatchEntityListResponse GetMatchList(Guid managerId)
        {
            return ResponseHelper.TryCatch<LadderMatchEntityListResponse>(() => LadderCore.Instance.GetMatchList(managerId));
        }

        /// <summary>
        /// GetMatch
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public LadderMatchEntityResponse GetMatch(Guid managerId, Guid matchId)
        {
            return ResponseHelper.TryCatch<LadderMatchEntityResponse>(() => LadderCore.Instance.GetMatch(managerId, matchId));
        }

        /// <summary>
        /// 获取天梯排行榜
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public RankResponse GetLadderRank(Guid managerId, int rankType, int pageIndex, int pageSize)
        {
            return ResponseHelper.TryCatch<RankResponse>(() => RankLadderThread.Instance.GetRanking(managerId,rankType, pageIndex,pageSize));
        }

        /// <summary>
        /// 天梯荣誉兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        public LadderExchangeResponse Exchange(Guid managerId, string exchangeKey)
        {
            return ResponseHelper.TryCatch<LadderExchangeResponse>(() => LadderCore.Instance.Exchange(managerId, exchangeKey));
        }

        /// <summary>
        /// 获取天梯战报滚动
        /// </summary>
        /// <returns></returns>
        public LadderMatchMarqueeResponse GetMatchMarqueeResponse()
        {
            return ResponseHelper.TryCatch<LadderMatchMarqueeResponse>(() => LadderThread.Instance.GetMatchMarqueeResponse());
        }

        /// <summary>
        /// 获取经理排行
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public int GetMyLadderRank(Guid managerId)
        {
            try
            {
                return LadderCore.Instance.GetLadderRank(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ladderservice GetMyLadderRank", ex);
                return 0;
            }
        }

        public LadderRefreshExchangeResponse RefreshExchange(Guid managerId)
        {
            return ResponseHelper.TryCatch<LadderRefreshExchangeResponse>(() => LadderCore.Instance.RefreshExchange(managerId));
        }

        public LadderHookInfoResponse GetHookInfoResponse(Guid managerId)
        {
            return ResponseHelper.TryCatch<LadderHookInfoResponse>(() => LadderThread.Instance.GetHookInfoResponse(managerId));
        }

        public LadderHookInfoResponse StartHook(Guid managerId, int maxTimes, int minScore, int maxScore, int winTimes)
        {
            return ResponseHelper.TryCatch<LadderHookInfoResponse>(() => LadderThread.Instance.StartHook(managerId,maxTimes,minScore,maxScore,winTimes));
        }

        public MessageCodeResponse StopHook(Guid managerId)
        {
            return ResponseHelper.TryCatch<MessageCodeResponse>(() => LadderThread.Instance.StopHook(managerId));
        }

        /// <summary>
        /// 天梯赛清除CD
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LadderClearCDResponse LadderClearCD(Guid managerId)
        {
            return ResponseHelper.TryCatch<LadderClearCDResponse>(() => LadderCore.Instance.LadderClearCD(managerId));
        }

        /// <summary>
        /// 天梯赛清除CD参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LadderClearCDParaResponse LadderClearCDPara(Guid managerId)
        {
            return ResponseHelper.TryCatch<LadderClearCDParaResponse>(() => LadderCore.Instance.LadderClearCDPara(managerId));
        }
    }
}
