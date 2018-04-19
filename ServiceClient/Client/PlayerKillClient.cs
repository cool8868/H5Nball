using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class PlayerKillClient
    {
        private static IPlayerKillService proxy = ServiceProxy<IPlayerKillService>.Create("NetTcp_IPlayerKillService");

        /// <summary>
        /// 获取pk赛信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PlayerkillInfoResponse GetInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => proxy.GetInfo(managerId));
        }

        /// <summary>
        /// 刷新对手或者搜索对手
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="opponentName"></param>
        /// <returns></returns>
        public PlayerKillOpponentResponse GetOpponents(Guid managerId, string opponentName)
        {
            return ResponseHelper.TryCatch(() => proxy.GetOpponents(managerId, opponentName));
        }

        /// <summary>
        /// 挑战
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="awayId"></param>
        /// <returns></returns>
        public MatchCreateResponse Fight(Guid managerId, Guid awayId, bool hasTask)
        {
            return ResponseHelper.TryCatch(() => proxy.Fight(managerId, awayId, hasTask));
        }

        /// <summary>
        /// 获取比赛结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public PlayerkillMatchResponse GetMatchResult(Guid managerId, Guid matchId)
        {
            return ResponseHelper.TryCatch(() => proxy.GetMatchResult(managerId, matchId));
        }
        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public TourLotteryResponse Lottery(Guid managerId, Guid matchId)
        {
            return ResponseHelper.TryCatch(() => proxy.Lottery(managerId, matchId));
        }
    }
}
