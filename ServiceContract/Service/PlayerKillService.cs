using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core.FriendShip;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class PlayerKillService : IPlayerKillService
    {
        /// <summary>
        /// 获取pk赛信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PlayerkillInfoResponse GetInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => PlayerKillCore.Instance.GetInfo(managerId));
        }

        /// <summary>
        /// 刷新对手或者搜索对手
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="opponentName"></param>
        /// <returns></returns>
        public PlayerKillOpponentResponse GetOpponents(Guid managerId, string opponentName)
        {
            return ResponseHelper.TryCatch(() => PlayerKillCore.Instance.GetOpponents(managerId, opponentName));
        }

        /// <summary>
        /// 挑战
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="awayId"></param>
        /// <returns></returns>
        public MatchCreateResponse Fight(Guid managerId, Guid awayId, bool hasTask)
        {
            return ResponseHelper.TryCatch(() => PlayerKillCore.Instance.Fight(managerId, awayId, hasTask));
        }

        /// <summary>
        /// 获取比赛结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public PlayerkillMatchResponse GetMatchResult(Guid managerId, Guid matchId)
        {
            return ResponseHelper.TryCatch(() => PlayerKillCore.Instance.GetMatchResult(managerId, matchId));
        }
        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public TourLotteryResponse Lottery(Guid managerId, Guid matchId)
        {
            return ResponseHelper.TryCatch(() => PlayerKillCore.Instance.Lottery(managerId, matchId));
        }
    }
}
