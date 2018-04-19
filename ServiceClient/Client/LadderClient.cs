using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Ladder;
using Games.NBall.Entity.Response.Rank;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class LadderClient
    {
        private static ILadderService proxy = ServiceProxy<ILadderService>.Create("NetTcp_ILadderService");

        /// <summary>
        /// 获取天梯赛信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LadderManagerResponse GetManagerInfo(Guid managerId)
        {
            return proxy.GetManagerInfo(managerId);
        }

        /// <summary>
        /// Attends the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse AttendLadder(Guid managerId, bool hasTask, bool isGuide = false)
        {
            return proxy.AttendLadder(managerId, hasTask, isGuide);
        }

        /// <summary>
        /// Leaves the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse LeaveLadder(Guid managerId)
        {
            return proxy.LeaveLadder(managerId);
        }

        /// <summary>
        /// Ladders the heart.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public Ladder_HeartResponse LadderHeart(Guid managerId)
        {
            return proxy.LadderHeart(managerId);
        }

        /// <summary>
        /// Fives the match.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public LadderMatchEntityListResponse GetMatchList(Guid managerId)
        {
            return proxy.GetMatchList(managerId);
        }

        /// <summary>
        /// GetMatch
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public LadderMatchEntityResponse GetMatch(Guid managerId, Guid matchId)
        {
            return proxy.GetMatch(managerId, matchId);
        }

        /// <summary>
        /// 获取天梯排行榜
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public RankResponse GetLadderRank(Guid managerId,int rankType, int pageIndex, int pageSize)
        {
            return proxy.GetLadderRank(managerId,rankType, pageIndex,pageSize);
        }

        /// <summary>
        /// 天梯荣誉兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        public LadderExchangeResponse Exchange(Guid managerId, string exchangeKey)
        {
            return proxy.Exchange(managerId, exchangeKey);
        }

        public LadderMatchMarqueeResponse GetMatchMarqueeResponse()
        {
            return proxy.GetMatchMarqueeResponse();
        }

        public int GetMyLadderRank(Guid managerId)
        {
            try
            {
                return proxy.GetMyLadderRank(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ladder client GetMyLadderRank", ex);
                return 0;
            }
        }

        public LadderRefreshExchangeResponse RefreshExchange(Guid managerId)
        {
            return proxy.RefreshExchange(managerId);
        }

        public LadderHookInfoResponse GetHookInfoResponse(Guid managerId)
        {
            return proxy.GetHookInfoResponse(managerId);
        }

        public LadderHookInfoResponse StartHook(Guid managerId, int maxTimes, int minScore, int maxScore, int winTimes)
        {
            return proxy.StartHook(managerId, maxTimes, minScore, maxScore, winTimes);
        }

        public MessageCodeResponse StopHook(Guid managerId)
        {
            return proxy.StopHook(managerId);
        }

        /// <summary>
        /// 天梯赛清除CD
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LadderClearCDResponse LadderClearCD(Guid managerId)
        {
            return proxy.LadderClearCD(managerId);
        }

        /// <summary>
        /// 天梯赛清除CD参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LadderClearCDParaResponse LadderClearCDPara(Guid managerId)
        {
            return proxy.LadderClearCDPara(managerId);
        }
    }
}
