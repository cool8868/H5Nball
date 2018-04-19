using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IPlayerKillService
    {
        /// <summary>
        /// 获取pk赛信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        PlayerkillInfoResponse GetInfo(Guid managerId);

        /// <summary>
        /// 刷新对手或者搜索对手
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="opponentName"></param>
        /// <returns></returns>
        [OperationContract]
        PlayerKillOpponentResponse GetOpponents(Guid managerId, string opponentName);

        /// <summary>
        /// 挑战
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="awayId"></param>
        /// <returns></returns>
        [OperationContract]
        MatchCreateResponse Fight(Guid managerId, Guid awayId, bool hasTask);
        /// <summary>
        /// 获取比赛结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        PlayerkillMatchResponse GetMatchResult(Guid managerId, Guid matchId);
        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        TourLotteryResponse Lottery(Guid managerId, Guid matchId);
    }
}
