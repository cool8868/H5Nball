using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Crowd;
using Games.NBall.Entity.Response.Ladder;
using Games.NBall.Entity.Response.Match;
using Games.NBall.Entity.Response.Rank;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class CrossDataClient
    {
        private static ICrossDataService proxy = ServiceProxy<ICrossDataService>.Create("NetTcp_ICrossDataService");

        #region Crowd
        public string CrowdBanner(string siteId)
        {
            return proxy.CrowdBanner(siteId);
        }

        /// <summary>
        /// 获取当前活动信息
        /// </summary>
        /// <returns></returns>
        public CrosscrowdInfoResponse CrowdGetCrowdInfo(string siteId)
        {
            return proxy.CrowdGetCrowdInfo(siteId);
        }

        /// <summary>
        /// 获取当前活动经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public CrosscrowdManagerResponse CrowdGetManagerInfo(string siteId, Guid managerId)
        {
            return proxy.CrowdGetManagerInfo(siteId,managerId);
        }

        /// <summary>
        /// 开始配对
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse CrowdAttend(string siteId, Guid managerId)
        {
            return proxy.CrowdAttend(siteId,managerId);
        }

        public MessageCodeResponse CrowdLeave(string siteId, Guid managerId)
        {
            return proxy.CrowdLeave(siteId,managerId);
        }

        /// <summary>
        /// 配对成功后轮询，返回matchid时表示分组成功了
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public CrowdHeartResponse CrowdHeart(string siteId, Guid managerId)
        {
            return proxy.CrowdHeart(siteId,managerId);
        }

        /// <summary>
        /// 获取比赛结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public CrosscrowdMatchResponse CrowdGetMatch(Guid managerId, Guid matchId)
        {
            return proxy.CrowdGetMatch(managerId, matchId);
        }

        public CrosscrowdManagerResponse CrowdClearCd(string siteId, Guid managerId)
        {
            return proxy.CrowdClearCd(siteId,managerId);
        }

        public CrosscrowdManagerResponse CrowdResurrection(string siteId, Guid managerId)
        {
            return proxy.CrowdResurrection(siteId,managerId);
        }

        public RankResponse CrowdGetRank(string siteId, Guid managerId, int rankType, int pageIndex, int pageSize)
        {
            return proxy.CrowdGetRank(siteId, managerId, rankType, pageIndex, pageSize);
        }
        #endregion

        #region Ladder
        /// <summary>
        /// 获取天梯赛信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public CrossladderManagerResponse LadderGetManagerInfo(string siteId, Guid managerId)
        {
            return proxy.LadderGetManagerInfo(siteId, managerId);
        }

        /// <summary>
        /// Attends the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse LadderAttend(string siteId, Guid managerId)
        {
            return proxy.LadderAttend(siteId, managerId);
        }

        /// <summary>
        /// Leaves the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse LadderLeave(string siteId, Guid managerId)
        {
            return proxy.LadderLeave(siteId, managerId);
        }

        /// <summary>
        /// Ladders the heart.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public CrossLadderHeartResponse LadderHeart(string siteId, Guid managerId)
        {
            return proxy.LadderHeart(siteId, managerId);
        }

        /// <summary>
        /// Fives the match.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public LadderMatchEntityListResponse LadderGetMatchList(Guid managerId)
        {
            return proxy.LadderGetMatchList(managerId);
        }

        /// <summary>
        /// GetMatch
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public CrossladderMatchResponse LadderGetMatch(Guid managerId, Guid matchId)
        {
            return proxy.LadderGetMatch(managerId, matchId);
        }

        /// <summary>
        /// 天梯荣誉兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        public LadderExchangeResponse LadderExchange(string siteId, Guid managerId, int exchangeKey)
        {
            return proxy.LadderExchange(siteId, managerId, exchangeKey);
        }

        public LadderMatchMarqueeResponse LadderGetMatchMarqueeResponse(string siteId)
        {
            return proxy.LadderGetMatchMarqueeResponse(siteId);
        }

        public MessageCodeResponse LadderBuyStamina(string siteId, Guid managerId)
        {
            return proxy.LadderBuyStamina(siteId, managerId);
        }

        public LadderHookInfoResponse LadderGetHookInfoResponse(Guid managerId)
        {
            return proxy.LadderGetHookInfoResponse(managerId);
        }


        public LadderHookInfoResponse LadderStartHook(string siteId, Guid managerId, int maxTimes, int minScore, int maxScore, int winTimes)
        {
            return proxy.LadderStartHook(siteId, managerId, maxTimes, minScore, maxScore, winTimes);
        }


        public MessageCodeResponse LadderStopHook(string siteId, Guid managerId)
        {
            return proxy.LadderStopHook(siteId, managerId);
        }
        #endregion

        #region Arena

        /// <summary>
        /// 获取竞技场信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaGetInfoResponse GetArenaResponse(Guid managerId, string zoneName)
        {
            return proxy.GetArenaResponse(managerId,zoneName);
        }

        /// <summary>
        /// 获取对手
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaGetOpponentResponse GetOpponent(Guid managerId, string zoneName)
        {
            return proxy.GetOpponent(managerId, zoneName);
        }

        /// <summary>
        /// 购买通行证参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaBuyStaminaParaResponse BuyStaminaPara(Guid managerId, string zoneName)
        {
            return proxy.BuyStaminaPara(managerId, zoneName);
        }

        /// <summary>
        /// 购买通行证
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaBuyStaminaResponse BuyStamina(Guid managerId, string zoneName)
        {
            return proxy.BuyStamina(managerId, zoneName);
        }

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="opponentId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaFightResponse Fight(Guid managerId, Guid opponentId, string zoneName)
        {
            return proxy.Fight(managerId, opponentId, zoneName);
        }

        /// <summary>
        /// 获取体力
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaStaminaResponse GetStamina(Guid managerId, string zoneName)
        {
            return proxy.GetStamina(managerId, zoneName);
        }

        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId, string zoneName)
        {
            return proxy.SolutionAndTeammemberResponse(managerId, zoneName);
        }

        #endregion

        #region Robot
        public RobotResponse RobotInfo(string siteId, Guid managerId)
        {
            return proxy.RobotInfo(siteId, managerId);
        }

        public RobotResponse StartRobot(string siteId, Guid managerId, int hookMatchType)
        {
            return proxy.StartRobot(siteId, managerId, hookMatchType);
        }

        public RobotResponse StopRobot(Guid managerId, int hookMatchType)
        {
            return proxy.StopRobot(managerId, hookMatchType);
        }
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
        public AuctionItemResponse AuctionItem(Guid managerId, string zoneName, Guid itemId, int price, int transferDuration)
        {
           return proxy.AuctionItem(managerId, zoneName, itemId, price, transferDuration);
        }

        /// <summary>
        /// 获取我的拍卖列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public AuctionItemResponse GetMyAuctionList(Guid managerId, string zoneName)
        {
            return proxy.GetMyAuctionList(managerId, zoneName);
        }

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
        public AuctionItemResponse GetTransferList(Guid managerId, int rankRule, string itemName, string zoneName, int pageSize, int pageIndex)
        {
            return proxy.GetTransferList(managerId, rankRule, itemName, zoneName, pageSize, pageIndex);
        }

        /// <summary>
        /// 获取拍卖物品详情
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="transferId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public TransferMainResponse GetTransferInfo(Guid managerId, Guid transferId, string zoneName)
        {
            return proxy.GetTransferInfo(managerId, transferId, zoneName);
        }

        /// <summary>
        /// 竞拍
        /// </summary>
        /// <param name="transferId"></param>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public AuctionResponse Auction(Guid transferId, Guid managerId, string zoneName)
        {
            return proxy.Auction(transferId, managerId, zoneName);
        }

        /// <summary>
        /// 下架
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="transferId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public MessageCodeResponse SoldOut(Guid managerId, Guid transferId, string zoneName)
        {
            return proxy.SoldOut(managerId, transferId, zoneName);
        }

        #endregion

        #region 感恩回馈

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public CrossActivityInfoResponse GetActivityInfo(Guid managerId, string zoneName)
        {
            return proxy.GetActivityInfo(managerId, zoneName);
        }

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public CrossActivityPrizeResponse Prize(Guid managerId, string zoneName)
        {
           return proxy.Prize(managerId, zoneName);
        }

        #endregion
        #region 战报

        /// <summary>
        /// 获取竞技场对阵信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public Match_FightinfoResponse GetArenaFightInfo(Guid matchId)
        {
            return proxy.GetArenaFightInfo(matchId);
        }

        /// <summary>
        /// 获取战报
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="matchType"></param>
        /// <returns></returns>
        public MatchProcessResponse GetMatchProcess(Guid matchId, int matchType)
        {
            return proxy.GetMatchProcess(matchId, matchType);
        }

        #endregion
    }
}
