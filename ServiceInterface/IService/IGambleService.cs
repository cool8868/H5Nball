using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using System.ServiceModel;
using Games.NBall.Entity;
using Games.NBall.Entity.Response.Gamble;
using Games.NBall.Entity.Response.Auction;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IGambleService
    {
        /// <summary>
        /// 可以押注的主题
        /// </summary>
        /// <returns>可以押注的主题</returns>
        [OperationContract]
        GambleHostListResponse GetCanGambleTitleList();

        /// <summary>
        /// 获取竞猜列表
        /// </summary>
        /// <param name="BeforeDay"></param>
        /// <param name="AfterDay"></param>
        /// <returns></returns>
       [OperationContract]
        GambleTitleListResponse GetGambleByTime(string beforeDay, string afterDay);



        /// <summary>
        /// 查看当前可以坐庄的主题
        /// </summary>
        /// <returns>查看当前可以发起的主题</returns>
        [OperationContract]
        GambleTitleListResponse GetCanHostTitleList();

        /// <summary>
        /// 获取我发起的竞猜
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns>我发起的竞猜</returns>
        [OperationContract]
        GambleHostListResponse GetMyHostList(Guid managerId);

        /// <summary>
        /// 获取我参与的竞猜
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns>获取我参与的竞猜</returns>
        [OperationContract]
        GambleDetailListResponse GetMyGambleList(Guid managerId);

        /// <summary>
        /// 获取我的排行
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns>我的排行</returns>
        [OperationContract]
        GambleRankResponse GetMyRank(Guid managerId);

        /// <summary>
        /// top10 奖金排行榜
        /// </summary>
        /// <returns>top10 奖金排行榜</returns>
        [OperationContract]
        GambleRankListResponse GetTop10Rank(Guid managerId);

        /// <summary>
        /// 玩家参与竞猜
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="gambleMoney">下注金额</param>
        /// <param name="optionRateId">庄家的哪个选项的竞猜</param>
        /// <returns>是否参与成功</returns>
        [OperationContract]
        AuctionBuyResponse AttendGamble(Guid managerId, int gambleMoney, int optionRateId);

        /// <summary>
        /// 增加奖池奖金
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="gambleTitleId">竞猜主题ID</param>
        /// <param name="addMoney">需要增加的金额</param>
        /// <returns>是否增加奖金成功</returns>
        [OperationContract]
        AuctionBuyResponse AddMoney(Guid managerId, Guid gambleTitleId, int addMoney);

        /// <summary>
        /// 坐庄
        /// </summary>
        /// <param name="managerId">庄家ID</param>
        /// <param name="rateStr">赔率字符串（以‘|’分割)</param>
        /// <param name="gambleTitleId">竞猜的主题ID</param>
        /// <param name="hostMoney">奖池的奖金</param>
        [OperationContract]
        AuctionBuyResponse ToBeHost(Guid managerId, string rateStr, Guid gambleTitleId, int hostMoney);

        /// <summary>
        /// 获取比赛列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchDate"></param>
        /// <returns></returns>
        [OperationContract]
        GetEuropeMatchListResponse GetMatchList(Guid managerId, DateTime matchDate,int isDefault);

        /// <summary>
        /// 竞猜比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <param name="gambleType"></param>
        /// <param name="pointType"></param>
        /// <returns></returns>
        [OperationContract]
        EuropeGambleMatchResponse GambleMatch(Guid managerId, int matchId, int gambleType, int pointType);

        /// <summary>
        /// 获取我的竞猜列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        GetMyEuropeGambleResponse GetMyGamble(Guid managerId);

        /// <summary>
        /// 获取我的竞猜活动信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        EuropeGambleResponse GetMyActivity(Guid managerId);

        /// <summary>
        /// 领取竞猜活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        [OperationContract]
        EuropeGambleMatchResponse DrawPrize(Guid managerId, int step);

        /// <summary>
        /// 发布一场比赛
        /// </summary>
        /// <param name="homeName"></param>
        /// <param name="awayName"></param>
        /// <param name="matchTime"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse ReleaseMatch(string homeName, string awayName, DateTime matchTime);

        /// <summary>
        /// 结束一场比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="homeGoals"></param>
        /// <param name="awayGoals"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse EndMatch(int matchId, int homeGoals, int awayGoals);

        /// <summary>
        /// 比赛日历
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MatchTheCalendarResponse GetMatchTheCalendar();
    }
}
