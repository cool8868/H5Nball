using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Match;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class MatchDataService : IMatchDataService
    {
        /// <summary>
        /// 获取对阵信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="awayId"></param>
        /// <returns></returns>
        public Match_FightinfoResponse GetFightInfo(Guid managerId, Guid awayId)
        {
            return ResponseHelper.TryCatch(() => MatchDataCore.Instance.GetFightInfo(managerId, awayId));
        }

        /// <summary>
        /// 获取天梯赛对阵信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public Match_FightinfoResponse GetLadderFightInfo(Guid matchId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => MatchDataCore.Instance.GetLadderFightInfo(matchId, managerId));
        }

        /// <summary>
        /// 获取联赛对阵信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public Match_FightinfoResponse GetLeagueFightInfo(Guid matchId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => MatchDataCore.Instance.GetLeagueFightInfo(matchId, managerId));
        }

        /// <summary>
        /// 获取战报
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="matchType"></param>
        /// <returns></returns>
        public byte[] GetMatchProcess(Guid matchId, int matchType)
        {
            return MatchDataCore.Instance.GetMatchProcess(matchId, matchType);
        }

        /// <summary>
        /// 测试比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MatchCreateResponse MatchTest(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => MatchDataCore.Instance.MatchTest(managerId));
        }

        public MessageCode CheackReward(Guid managerId, int matchType, Guid matchId)
        {
            return ResponseHelper.TryCatch(() => MatchRewardRules.CheackReward(managerId, matchType, matchId));
        }

        public RootResponse<DTOAssetInfo> CommitReward(Guid managerId, int matchType, Guid matchId, string mask, string sig)
        {
            return ResponseHelper.TryCatch(() => MatchRewardRules.CommitReward(managerId, matchType, matchId, mask, sig));
        }
    }
}
