using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Crowd;
using Games.NBall.Entity.Response.Ladder;
using Games.NBall.Entity.Response.Match;
using Games.NBall.Entity.Response.Rank;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface ICrossDataService
    {
        #region Crowd
        [OperationContract]
        string CrowdBanner(string siteId);
        /// <summary>
        /// 获取群雄逐鹿信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        CrosscrowdInfoResponse CrowdGetCrowdInfo(string siteId);
        /// <summary>
        /// 获取群雄逐鹿经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        CrosscrowdManagerResponse CrowdGetManagerInfo(string siteId, Guid managerId);

        /// <summary>
        /// 开始配对
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse CrowdAttend(string siteId, Guid managerId);

        [OperationContract]
        MessageCodeResponse CrowdLeave(string siteId, Guid managerId);
        /// <summary>
        /// 配对成功后轮询，返回matchid时表示分组成功了
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        CrowdHeartResponse CrowdHeart(string siteId, Guid managerId);

        /// <summary>
        /// 获取比赛结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        CrosscrowdMatchResponse CrowdGetMatch(Guid managerId, Guid matchId);
        [OperationContract]
        CrosscrowdManagerResponse CrowdClearCd(string siteId, Guid managerId);
        [OperationContract]
        CrosscrowdManagerResponse CrowdResurrection(string siteId, Guid managerId);
        [OperationContract]
        RankResponse CrowdGetRank(string siteId, Guid managerId, int rankType, int pageIndex, int pageSize);
        #endregion

        #region Ladder

        /// <summary>
        /// 获取天梯赛信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        CrossladderManagerResponse LadderGetManagerInfo(string siteId, Guid managerId);

        /// <summary>
        /// Attends the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse LadderAttend(string siteId, Guid managerId);

        /// <summary>
        /// Leaves the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse LadderLeave(string siteId, Guid managerId);

        /// <summary>
        /// Ladders the heart.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        [OperationContract]
        CrossLadderHeartResponse LadderHeart(string siteId, Guid managerId);

        /// <summary>
        /// Fives the match.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        [OperationContract]
        LadderMatchEntityListResponse LadderGetMatchList(Guid managerId);

        /// <summary>
        /// GetMatch
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        CrossladderMatchResponse LadderGetMatch(Guid managerId, Guid matchId);

        /// <summary>
        /// 天梯荣誉兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        [OperationContract]
        LadderExchangeResponse LadderExchange(string siteId, Guid managerId, int exchangeKey);

        [OperationContract]
        LadderMatchMarqueeResponse LadderGetMatchMarqueeResponse(string siteId);

        [OperationContract]
        MessageCodeResponse LadderBuyStamina(string siteId, Guid managerId);

        [OperationContract]
        LadderHookInfoResponse LadderGetHookInfoResponse(Guid managerId);

        [OperationContract]
        LadderHookInfoResponse LadderStartHook(string siteId, Guid managerId, int maxTimes, int minScore, int maxScore, int winTimes);

        [OperationContract]
        MessageCodeResponse LadderStopHook(string siteId, Guid managerId);
        #endregion

        #region Arena

        /// <summary>
        /// 获取竞技场信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        [OperationContract]
        ArenaGetInfoResponse GetArenaResponse(Guid managerId, string zoneName);

        /// <summary>
        /// 获取对手
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        [OperationContract]
        ArenaGetOpponentResponse GetOpponent(Guid managerId, string zoneName);

        /// <summary>
        /// 购买通行证参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        [OperationContract]
        ArenaBuyStaminaParaResponse BuyStaminaPara(Guid managerId, string zoneName);

        /// <summary>
        /// 购买通行证
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        [OperationContract]
        ArenaBuyStaminaResponse BuyStamina(Guid managerId, string zoneName);

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <param name="opponentId"></param>
        /// <returns></returns>
        [OperationContract]
        ArenaFightResponse Fight(Guid managerId, Guid opponentId, string zoneName);

        /// <summary>
        /// 获取体力
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        [OperationContract]
        ArenaStaminaResponse GetStamina(Guid managerId, string zoneName);

        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        [OperationContract]
        NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId, string zoneName);
        #endregion

        #region Robot
         [OperationContract]
        RobotResponse RobotInfo(string siteId, Guid managerId);
         [OperationContract]
        RobotResponse StartRobot(string siteId, Guid managerId, int hookMatchType);
         [OperationContract]
        RobotResponse StopRobot(Guid managerId, int hookMatchType);

        #endregion

        #region Transfer

        /// <summary>
        /// 开始拍卖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <param name="itemId"></param>
        /// <param name="price"></param>
         /// <param name="transferDuration"></param>
         [OperationContract]
        AuctionItemResponse AuctionItem(Guid managerId, string zoneName, Guid itemId, int price, int transferDuration);

        /// <summary>
        /// 获取我的拍卖列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        [OperationContract]
        AuctionItemResponse GetMyAuctionList(Guid managerId, string zoneName);

        /// <summary>
        /// 获取拍卖列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="rankRule"></param>
        /// <param name="itemName"></param>
        /// <param name="zoneName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
         /// <returns></returns>
         [OperationContract]
        AuctionItemResponse GetTransferList(Guid managerId, int rankRule, string itemName, string zoneName, int pageSize, int pageIndex);

        /// <summary>
        /// 获取拍卖物品详情
         /// </summary>
         /// <param name="managerId"></param>
        /// <param name="transferId"></param>
        /// <param name="zoneName"></param>
         /// <returns></returns>
         [OperationContract]
         TransferMainResponse GetTransferInfo(Guid managerId, Guid transferId, string zoneName);

        /// <summary>
        /// 竞拍
        /// </summary>
        /// <param name="transferId"></param>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
         /// <returns></returns>
         [OperationContract]
        AuctionResponse Auction(Guid transferId, Guid managerId, string zoneName);

        /// <summary>
        /// 下架
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="transferId"></param>
        /// <param name="zoneName"></param>
         /// <returns></returns>
         [OperationContract]
        MessageCodeResponse SoldOut(Guid managerId, Guid transferId, string zoneName);

        #endregion

         #region 感恩回馈
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
         [OperationContract]
        CrossActivityInfoResponse GetActivityInfo(Guid managerId, string zoneName);

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
         [OperationContract]
         CrossActivityPrizeResponse Prize(Guid managerId, string zoneName);
        #endregion

         #region 战报

         /// <summary>
         /// 获取竞技场对阵
         /// </summary>
         /// <param name="matchId"></param>
         /// <returns></returns>
         [OperationContract]
         Match_FightinfoResponse GetArenaFightInfo(Guid matchId);

         /// <summary>
         /// 获取比赛战报
         /// </summary>
         /// <param name="matchId"></param>
         /// <param name="matchType"></param>
         /// <returns></returns>
         [OperationContract]
         MatchProcessResponse GetMatchProcess(Guid matchId, int matchType);

        #endregion
    }
}
