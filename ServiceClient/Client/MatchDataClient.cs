using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class MatchDataClient
    {
        private static IMatchDataService proxy = ServiceProxy<IMatchDataService>.Create("NetTcp_IMatchDataService");

        /// <summary>
        /// 获取对阵信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="awayId"></param>
        /// <returns></returns>
        public Match_FightinfoResponse GetFightInfo(Guid managerId, Guid awayId)
        {
            return proxy.GetFightInfo(managerId, awayId);
        }

        /// <summary>
        /// 获取天梯赛对阵信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public Match_FightinfoResponse GetLadderFightInfo(Guid matchId, Guid managerId)
        {
            return proxy.GetLadderFightInfo(matchId, managerId);
        }

        /// <summary>
        /// 获取联赛对阵信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public Match_FightinfoResponse GetLeagueFightInfo(Guid matchId, Guid managerId)
        {
            return proxy.GetLeagueFightInfo(matchId, managerId);
        }

        public MatchProcessResponse GetMatchProcess(Guid matchId, int matchType)
        {
            try
            {
                var baseData = MemcachedFactory.MatchClient.Get<BaseMatchData>(matchId);
                if (baseData != null)
                {
                    if (baseData.ErrorCode != (int)MessageCode.Success)
                        return ResponseHelper.Create<MatchProcessResponse>(baseData.ErrorCode);
                }
                var process = MemcachedFactory.MatchProcessClient.Get<byte[]>(matchId);
                if (process == null)
                {
                    process = proxy.GetMatchProcess(matchId, matchType);
                    if (process == null)
                        return ResponseHelper.Create<MatchProcessResponse>(MessageCode.MatchMiss);
                }
                var response = ResponseHelper.CreateSuccess<MatchProcessResponse>();
                response.Data = process;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetMatchProcess", ex);
                return ResponseHelper.Exception<MatchProcessResponse>();
            }
        }

        /// <summary>
        /// 测试比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MatchCreateResponse MatchTest(Guid managerId)
        {
            return proxy.MatchTest(managerId);
        }


        public MessageCode CheackReward(Guid managerId, int matchType, Guid matchId)
        {
            return proxy.CheackReward(managerId, matchType, matchId);
        }

        public RootResponse<DTOAssetInfo> CommitReward(Guid managerId, int matchType, Guid matchId, string mask, string sig)
        {
            return proxy.CommitReward(managerId, matchType, matchId, mask, sig);
        }
    }
}
