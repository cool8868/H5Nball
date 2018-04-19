using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.League;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.League;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class LeagueService : ILeagueService
    {
        /// <summary>
        /// 获取所有联赛关卡锁住情况
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetLeagueLockResponse GetLeagueLock(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.GetLeagueLock(managerId));
        }

        /// <summary>
        /// 获取联赛情况
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public GetLeagueInfoResponse GetLeagueInfo(Guid managerId,int leagueId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.GetLeagueInfoResponse(managerId, leagueId));
        }

        /// <summary>
        /// 开启一个联赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public GetLeagueInfoResponse StarLeague(Guid managerId, int leagueId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.StarLeague(managerId, leagueId));
        }

        /// <summary>
        /// 重置联赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public MessageCodeResponse ResetLeague(Guid managerId, int leagueId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.ResetLeague(managerId, leagueId));
        }

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        public LeagueFightResultResponse FightMatch(Guid managerId,int leagueId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.FightMatch(managerId, leagueId));
        }

        /// <summary>
        /// 获取冠军奖励列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LeagueAllPrizeInfoResponse GetAllPrizeInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.GetAllPrizeInfo(managerId));
        }


        /// <summary>
        /// 领取冠军奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueRecordId"></param>
        public LeaguePrizeResponse GetRankPrize(Guid managerId, Guid leagueRecordId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.GetRankPrize(managerId, leagueRecordId));
        }

        /// <summary>
        /// 获取排名信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public LeagueRankListResponse GetRank(Guid managerId, int leagueId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.GetRank(managerId, leagueId));
        }
        
        /// <summary>
        /// 获取联赛积分商城数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LaegueManagerinfoResponse GetLeagueMallInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.GetLeagueMallInfo(managerId));
        }

        /// <summary>
        /// 联赛积分商城兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        public LeagueExchangeResponse Exchange(Guid managerId, string exchangeKey)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.Exchange(managerId, exchangeKey));
        }

        /// <summary>
        /// 联赛积分商城刷新
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        public LaegueRefreshExchangeResponse RefreshExchange(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.RefreshExchange(managerId));
        }

        /// <summary>
        ///  获取胜场奖励情况
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <returns></returns>
        public LeagueWincountrecordResponse GetWincountPrizeInfo(Guid managerId, int leagueId)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.GetWincountPrizeInfo(managerId, leagueId));
        }

        /// <summary>
        ///  领取胜场奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <param name="countType"></param>
        /// <returns></returns>
        public LeaguePrizeResponse GetWincountPrize(Guid managerId, int leagueId, int countType)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.GetWincountPrize(managerId, leagueId, countType));
        }

        /// <summary>
        /// 获取联赛某一轮对阵记录
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="leagueId"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        public LeagueGetFightMapRecordResponse GetLeagueFigMap(Guid managerId, int leagueId, int round)
        {
            return ResponseHelper.TryCatch(() => LeagueCore.Instance.GetLeagueFigMap(managerId, leagueId, round));
        }
    }
}
