using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using System.ServiceModel;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.League;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface ILeagueService
    {
        /// <summary>
        /// 获取所有联赛关卡锁住情况
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        GetLeagueLockResponse GetLeagueLock(Guid managerId);

        /// <summary>
        /// 获取联赛情况
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        [OperationContract]
        GetLeagueInfoResponse GetLeagueInfo(Guid managerId, int leagueId);

        /// <summary>
        /// 开启一个联赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        [OperationContract]
        GetLeagueInfoResponse StarLeague(Guid managerId, int leagueId);

        /// <summary>
        /// 重置联赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse ResetLeague(Guid managerId, int leagueId);

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        [OperationContract]
        LeagueFightResultResponse FightMatch(Guid managerId, int leagueId);

        /// <summary>
        /// 获取冠军奖励列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        LeagueAllPrizeInfoResponse GetAllPrizeInfo(Guid managerId);

        /// <summary>
        /// 领取冠军奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueRecordId"></param>
        [OperationContract]
        LeaguePrizeResponse GetRankPrize(Guid managerId, Guid leagueRecordId);

        /// <summary>
        /// 获取排名信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        [OperationContract]
        LeagueRankListResponse GetRank(Guid managerId, int leagueId);

        /// <summary>
        /// 获取联赛积分商城数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        LaegueManagerinfoResponse GetLeagueMallInfo(Guid managerId);

        /// <summary>
        /// 联赛积分商城兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        [OperationContract]
        LeagueExchangeResponse Exchange(Guid managerId, string exchangeKey);

        /// <summary>
        /// 联赛积分商城刷新
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        [OperationContract]
        LaegueRefreshExchangeResponse RefreshExchange(Guid managerId);

        /// <summary>
        /// 获取胜场奖励情况
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        [OperationContract]
        LeagueWincountrecordResponse GetWincountPrizeInfo(Guid managerId, int leagueId);

        /// <summary>
        /// 领取胜场奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <param name="countType"></param>
        /// <returns></returns>
        [OperationContract]
        LeaguePrizeResponse GetWincountPrize(Guid managerId, int leagueId, int countType);

        /// <summary>
        /// 获取联赛某一轮对阵记录
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        [OperationContract]
        LeagueGetFightMapRecordResponse GetLeagueFigMap(Guid managerId, int leagueId, int round);
    }
}
