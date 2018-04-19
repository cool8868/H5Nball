using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.CrossCrowd;
using Games.NBall.Core.CrossLadder;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Rank;
using Games.NBall.Core.Robot;
using Games.NBall.Core.Transfer;
using Games.NBall.Core.UerCrossPara;
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

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall,
        AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class CrossDataService : ICrossDataService
    {
        #region Crowd

        public string CrowdBanner(string siteId)
        {
            return CrossCrowdManager.Instance.GetBanner(siteId);
        }

        /// <summary>
        /// 获取群雄逐鹿信息
        /// </summary>
        /// <returns></returns>
        public CrosscrowdInfoResponse CrowdGetCrowdInfo(string siteId)
        {
            return ResponseHelper.TryCatch(() => CrossCrowdCore.Instance.GetCrowdInfo(siteId));
        }

        /// <summary>
        /// 获取群雄逐鹿经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public CrosscrowdManagerResponse CrowdGetManagerInfo(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossCrowdCore.Instance.GetManagerInfo(siteId,managerId));
        }

        /// <summary>
        /// 开始配对
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse CrowdAttend(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossCrowdManager.Instance.Attend(siteId, managerId));
        }

        public MessageCodeResponse CrowdLeave(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossCrowdManager.Instance.Leave(siteId, managerId));
        }

        /// <summary>
        /// 配对成功后轮询，返回matchid时表示分组成功了
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public CrowdHeartResponse CrowdHeart(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossCrowdManager.Instance.Heart(siteId, managerId));
        }

        /// <summary>
        /// 获取比赛结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public CrosscrowdMatchResponse CrowdGetMatch(Guid managerId, Guid matchId)
        {
            return ResponseHelper.TryCatch(() => CrossCrowdCore.Instance.GetMatch(managerId, matchId));
        }

        public CrosscrowdManagerResponse CrowdClearCd(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossCrowdCore.Instance.ClearCd(siteId,managerId));
        }

        public CrosscrowdManagerResponse CrowdResurrection(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossCrowdCore.Instance.Resurrection(siteId,managerId));
        }

        public RankResponse CrowdGetRank(string siteId, Guid managerId, int rankType, int pageIndex, int pageSize)
        {
            return ResponseHelper.TryCatch<RankResponse>(() => CrossRankThread.Instance.GetRanking(siteId, managerId, rankType, pageIndex, pageSize));
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
            return ResponseHelper.TryCatch(() => CrossLadderCore.Instance.GetManagerInfo(siteId, managerId));
        }

        /// <summary>
        /// Attends the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse LadderAttend(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossLadderManager.Instance.Attend(siteId, managerId));
        }

        /// <summary>
        /// Leaves the ladder.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse LadderLeave(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossLadderManager.Instance.Leave(siteId, managerId));
        }

        /// <summary>
        /// Ladders the heart.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public CrossLadderHeartResponse LadderHeart(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossLadderManager.Instance.Heart(siteId, managerId));
        }

        /// <summary>
        /// Fives the match.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public LadderMatchEntityListResponse LadderGetMatchList(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossLadderCore.Instance.GetMatchList(managerId));
        }

        /// <summary>
        /// GetMatch
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public CrossladderMatchResponse LadderGetMatch(Guid managerId, Guid matchId)
        {
            return ResponseHelper.TryCatch(() => CrossLadderCore.Instance.GetMatch(managerId, matchId));
        }

        /// <summary>
        /// 天梯荣誉兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="exchangeKey"></param>
        /// <returns></returns>
        public LadderExchangeResponse LadderExchange(string siteId, Guid managerId, int exchangeKey)
        {
            return ResponseHelper.TryCatch(() => CrossLadderCore.Instance.Exchange(siteId, managerId, exchangeKey));
        }

        public LadderMatchMarqueeResponse LadderGetMatchMarqueeResponse(string siteId)
        {
            return ResponseHelper.TryCatch(() => CrossLadderManager.Instance.GetMatchMarqueeResponse(siteId));
        }

        public MessageCodeResponse LadderBuyStamina(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossLadderCore.Instance.BuyStamina(siteId, managerId));
        }


        public LadderHookInfoResponse LadderGetHookInfoResponse(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossLadderManager.Instance.GetHookInfoResponse(managerId));
        }


        public LadderHookInfoResponse LadderStartHook(string siteId, Guid managerId, int maxTimes, int minScore, int maxScore, int winTimes)
        {
            return ResponseHelper.TryCatch(() => CrossLadderManager.Instance.StartHook(managerId,siteId,maxTimes,minScore,maxScore,winTimes));
        }


        public MessageCodeResponse LadderStopHook(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => CrossLadderManager.Instance.StopHook(managerId,siteId));
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
            return ResponseHelper.TryCatch(() => ArenaCore.Instance.GetArenaResponse(managerId, zoneName));
        }

        /// <summary>
        /// 获取对手
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaGetOpponentResponse GetOpponent(Guid managerId, string zoneName)
        {
            return ResponseHelper.TryCatch(() => ArenaCore.Instance.GetOpponent(managerId, zoneName));
        }

        /// <summary>
        /// 购买通行证参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaBuyStaminaParaResponse BuyStaminaPara(Guid managerId, string zoneName)
        {
            return ResponseHelper.TryCatch(() => ArenaCore.Instance.BuyStaminaPara(managerId, zoneName));
        }

        /// <summary>
        /// 购买通行证
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaBuyStaminaResponse BuyStamina(Guid managerId, string zoneName)
        {
            return ResponseHelper.TryCatch(() => ArenaCore.Instance.BuyStamina(managerId, zoneName));
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
            return ResponseHelper.TryCatch(() => ArenaCore.Instance.Fight(managerId, opponentId, zoneName));
        }

        /// <summary>
        /// 获取体力
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public ArenaStaminaResponse GetStamina(Guid managerId, string zoneName)
        {
            return ResponseHelper.TryCatch(() => ArenaCore.Instance.GetStamina(managerId, zoneName));
        }

        /// <summary>
        /// 获取阵型数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public NBSolutionInfoResponse SolutionAndTeammemberResponse(Guid managerId, string zoneName)
        {
            return ResponseHelper.TryCatch(() => ArenaCore.Instance.SolutionAndTeammemberResponse(managerId, zoneName));
        }

        #endregion

        #region Robot
        public RobotResponse RobotInfo(string siteId, Guid managerId)
        {
            return ResponseHelper.TryCatch(() => RobotCore.Instance.RobotInfo(siteId, managerId));
        }

        public RobotResponse StartRobot(string siteId, Guid managerId, int hookMatchType)
        {
            return ResponseHelper.TryCatch(() => RobotCore.Instance.StartRobot(siteId, managerId, hookMatchType));
        }

        public RobotResponse StopRobot(Guid managerId, int hookMatchType)
        {
            return ResponseHelper.TryCatch(() => RobotCore.Instance.StopRobot(managerId, hookMatchType));
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
            return ResponseHelper.TryCatch(() => TransferCore.Instance.AuctionItem(managerId, zoneName, itemId, price, transferDuration));
        }

        /// <summary>
        /// 获取我的拍卖列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public AuctionItemResponse GetMyAuctionList(Guid managerId, string zoneName)
        {
            return ResponseHelper.TryCatch(() => TransferCore.Instance.GetMyAuctionList(managerId, zoneName));
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
        public AuctionItemResponse GetTransferList(Guid managerId,int rankRule, string itemName, string zoneName, int pageSize, int pageIndex)
        {
            return ResponseHelper.TryCatch(() => TransferCore.Instance.GetTransferList(managerId,rankRule, itemName, zoneName, pageSize, pageIndex));
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
            return ResponseHelper.TryCatch(() => TransferCore.Instance.GetTransferInfo(managerId,transferId, zoneName));
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
            return ResponseHelper.TryCatch(() => TransferCore.Instance.Auction(transferId, managerId, zoneName));
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
            return ResponseHelper.TryCatch(() => TransferCore.Instance.SoldOut(managerId, transferId, zoneName));
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
            return ResponseHelper.TryCatch(() => CrossActivityCore.Instance.GetActivityInfo(managerId, zoneName));
        }

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public CrossActivityPrizeResponse Prize(Guid managerId, string zoneName)
        {
            return ResponseHelper.TryCatch(() => CrossActivityCore.Instance.Prize(managerId, zoneName));
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
            return ResponseHelper.TryCatch(() => MatchDataCore.Instance.GetArenaFightInfo(matchId));
        }

        /// <summary>
        /// 获取战报
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="matchType"></param>
        /// <returns></returns>
        public MatchProcessResponse GetMatchProcess(Guid matchId, int matchType)
        {
            var baseData = MemcachedFactory.MatchClient.Get<BaseMatchData>(matchId);
            if (baseData != null)
            {
                if (baseData.ErrorCode != (int)MessageCode.Success)
                    return ResponseHelper.Create<MatchProcessResponse>(baseData.ErrorCode);
            }
            var process = MemcachedFactory.MatchProcessClient.Get<byte[]>(matchId);
            if (null == process)
            {
                var dateChar = ShareUtil.GetDateFromComb(matchId);
                var match = MatchprocessMgr.GetByMatchId(dateChar, matchType, matchId);
                if (null != match)
                    process = match.Process;
            }
            if (null == process)
                return ResponseHelper.Create<MatchProcessResponse>(MessageCode.MatchMiss);
            var response = ResponseHelper.CreateSuccess<MatchProcessResponse>();
            response.Data = process;
            return response;
        }
        #endregion
    }
}
