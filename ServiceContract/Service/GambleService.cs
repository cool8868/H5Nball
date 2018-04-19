using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Gamble;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Gamble;
using Games.NBall.ServiceContract.IService;
using Games.NBall.Entity.Response.Auction;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class GambleService:IGambleService
    {
        /// <summary>
        /// 坐庄
        /// </summary>
        /// <param name="managerId">庄家ID</param>
        /// <param name="rateStr">赔率字符串（以‘|’分割)</param>
        /// <param name="gambleTitleId">竞猜的主题ID</param>
        /// <param name="hostMoney">奖池的奖金</param>
        public AuctionBuyResponse ToBeHost(Guid managerId, string rateStr, Guid gambleTitleId, int hostMoney)
        {
            return ResponseHelper.TryCatch(() => GambleCore.Instance.ToBeHost(managerId, rateStr, gambleTitleId, hostMoney));
        }

        /// <summary>
        /// 增加奖池奖金
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="gambleTitleId">竞猜主题ID</param>
        /// <param name="addMoney">需要增加的金额</param>
        /// <returns>是否增加奖金成功</returns>
        public AuctionBuyResponse AddMoney(Guid managerId, Guid gambleTitleId, int addMoney)
        {
            return ResponseHelper.TryCatch(() => GambleCore.Instance.AddMoney(managerId, gambleTitleId, addMoney));
        }

        /// <summary>
        /// 玩家参与竞猜
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="gambleMoney">下注金额</param>
        /// <param name="optionRateId">庄家的哪个选项的竞猜</param>
        /// <returns>是否参与成功</returns>
        public AuctionBuyResponse AttendGamble(Guid managerId, int gambleMoney, int optionRateId)
        {
            return ResponseHelper.TryCatch(() => GambleCore.Instance.AttendGamble(managerId, gambleMoney, optionRateId));
        }

        /// <summary>
        /// top10 奖金排行榜
        /// </summary>
        /// <returns>top10 奖金排行榜</returns>
        public GambleRankListResponse GetTop10Rank(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => GambleCore.Instance.GetTop10Rank(managerId));
        }

        /// <summary>
        /// 获取我的排行
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns>我的排行</returns>
        public GambleRankResponse GetMyRank(Guid managerId)
        {
           // return ResponseHelper.TryCatch(() => GambleCore.Instance.GetMyRank(managerId));
            return new GambleRankResponse();
        }

        /// <summary>
        /// 获取我参与的竞猜
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns>获取我参与的竞猜</returns>
        public GambleDetailListResponse GetMyGambleList(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => GambleCore.Instance.GetMyGambleList(managerId));
        }

        /// <summary>
        /// 获取我发起的竞猜
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns>我发起的竞猜</returns>
        public GambleHostListResponse GetMyHostList(Guid managerId)
        {
            //return ResponseHelper.TryCatch(() => GambleCore.Instance.GetMyHostList(managerId));
            return new GambleHostListResponse();
        }

        /// <summary>
        /// 查看当前可以坐庄的主题
        /// </summary>
        /// <returns>查看当前可以发起的主题</returns>
        public GambleTitleListResponse GetCanHostTitleList()
        {
            //return ResponseHelper.TryCatch(() => GambleCore.Instance.GetCanHostTitleList());
            return new GambleTitleListResponse();
        }

        /// <summary>
        /// 可以押注的主题
        /// </summary>
        /// <returns>可以押注的主题</returns>
        public GambleHostListResponse GetCanGambleTitleList()
        {
            return ResponseHelper.TryCatch(() => GambleCore.Instance.GetCanGambleTitleList());
        }

        /// <summary>
        /// 获取竞猜列表
        /// </summary>
        /// <param name="BeforeDay"></param>
        /// <param name="AfterDay"></param>
        /// <returns></returns>
        public GambleTitleListResponse GetGambleByTime(string beforeDay, string afterDay)
        {
            return ResponseHelper.TryCatch(() => GambleCore.Instance.GetGambleByTime(beforeDay, afterDay));
        }

        /// <summary>
        /// 获取比赛列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchDate"></param>
        /// <returns></returns>
        public GetEuropeMatchListResponse GetMatchList(Guid managerId,DateTime matchDate,int isDefault)
        {
            return ResponseHelper.TryCatch(() => EuropeCore.Instance.GetMatchList(managerId, matchDate, isDefault));
        }

        /// <summary>
        /// 竞猜一场比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <param name="gambleType"></param>
        /// <param name="pointType"></param>
        /// <returns></returns>
        public EuropeGambleMatchResponse GambleMatch(Guid managerId, int matchId, int gambleType, int pointType)
        {
            return ResponseHelper.TryCatch(() => EuropeCore.Instance.GambleMatch(managerId, matchId, gambleType, pointType));
        }

        /// <summary>
        /// 获取我的竞猜列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetMyEuropeGambleResponse GetMyGamble(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => EuropeCore.Instance.GetMyGamble(managerId));
        }

        /// <summary>
        /// 获取我的竞猜活动信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public EuropeGambleResponse GetMyActivity(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => EuropeCore.Instance.GetMyActivity(managerId));
        }

        /// <summary>
        /// 领取竞猜活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public EuropeGambleMatchResponse DrawPrize(Guid managerId, int step)
        {
            return ResponseHelper.TryCatch(() => EuropeCore.Instance.DrawPrize(managerId,step));
        }

        /// <summary>
        /// 发布一场比赛
        /// </summary>
        /// <param name="homeName"></param>
        /// <param name="awayName"></param>
        /// <param name="matchTime"></param>
        /// <returns></returns>
        public MessageCodeResponse ReleaseMatch(string homeName, string awayName, DateTime matchTime)
        {
            return ResponseHelper.TryCatch(() => EuropeCore.Instance.ReleaseMatch(homeName, awayName, matchTime));
        }

        /// <summary>
        /// 结束一场比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="homeGoals"></param>
        /// <param name="awayGoals"></param>
        /// <returns></returns>
        public MessageCodeResponse EndMatch(int matchId, int homeGoals, int awayGoals)
        {
            return ResponseHelper.TryCatch(() => EuropeCore.Instance.EndMatch(matchId, homeGoals, awayGoals));
        }

        /// <summary>
        /// 比赛日历
        /// </summary>
        /// <returns></returns>
        public MatchTheCalendarResponse GetMatchTheCalendar()
        {
            return ResponseHelper.TryCatch(() => EuropeCore.Instance.GetMatchTheCalendar());
        }
    }
}
