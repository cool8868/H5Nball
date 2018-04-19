using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Ladder;
using Games.NBall.Entity.Response.Rank;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface ILadderService
    {
        /// <summary>
        /// 获取天梯赛信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        LadderManagerResponse GetManagerInfo(Guid managerId);

        /// <summary>
        /// Attends the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse AttendLadder(Guid managerId, bool hasTask, bool isGuide = false);

        /// <summary>
        /// Leaves the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse LeaveLadder(Guid managerId);

        /// <summary>
        /// Ladders the heart.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        [OperationContract]
        Ladder_HeartResponse LadderHeart(Guid managerId);

        /// <summary>
        /// Fives the match.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        [OperationContract]
        LadderMatchEntityListResponse GetMatchList(Guid managerId);

        /// <summary>
        /// GetMatch
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        LadderMatchEntityResponse GetMatch(Guid managerId, Guid matchId);

        /// <summary>
        /// 获取天梯排行榜
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [OperationContract]
        RankResponse GetLadderRank(Guid managerId, int rankType, int pageIndex, int pageSize);

        /// <summary>
        /// 天梯荣誉兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        [OperationContract]
        LadderExchangeResponse Exchange(Guid managerId, string exchangeKey);

        /// <summary>
        /// 获取经理排行
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        int GetMyLadderRank(Guid managerId);

        [OperationContract]
        LadderMatchMarqueeResponse GetMatchMarqueeResponse();

        [OperationContract]
        LadderRefreshExchangeResponse RefreshExchange(Guid managerId);

        [OperationContract]
        LadderHookInfoResponse GetHookInfoResponse(Guid managerId);

        [OperationContract]
        LadderHookInfoResponse StartHook(Guid managerId, int maxTimes, int minScore, int maxScore, int winTimes);

        [OperationContract]
        MessageCodeResponse StopHook(Guid managerId);
        /// <summary>
        /// 天梯赛清除CD参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        LadderClearCDParaResponse LadderClearCDPara(Guid managerId);

        /// <summary>
        /// 天梯赛清除CD
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        LadderClearCDResponse LadderClearCD(Guid managerId);
    }
}
