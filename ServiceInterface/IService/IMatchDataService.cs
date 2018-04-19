using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IMatchDataService
    {
        /// <summary>
        /// 获取对阵信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="awayId"></param>
        /// <returns></returns>
        [OperationContract]
        Match_FightinfoResponse GetFightInfo(Guid managerId, Guid awayId);
     
        /// <summary>
        /// 获取天梯赛对阵信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        Match_FightinfoResponse GetLadderFightInfo(Guid matchId, Guid managerId);

        /// <summary>
        /// 获取联赛对阵信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        Match_FightinfoResponse GetLeagueFightInfo(Guid matchId, Guid managerId);
       
        /// <summary>
        /// 获取比赛战报
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="matchType"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] GetMatchProcess(Guid matchId, int matchType);

        /// <summary>
        /// 测试比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MatchCreateResponse MatchTest(Guid managerId);

        [OperationContract]
        MessageCode CheackReward(Guid managerId, int matchType, Guid matchId);

        [OperationContract]
        RootResponse<DTOAssetInfo> CommitReward(Guid managerId, int matchType, Guid matchId, string mask, string sig);

    }
}
