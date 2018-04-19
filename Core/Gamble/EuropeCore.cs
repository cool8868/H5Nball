using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.League;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Dal;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response.Auction;
using Games.NBall.Entity.Response.Gamble;
using MsEntLibWrapper.Caching;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Gamble
{
    public class EuropeCore
    {
        #region
       
        private static EuropeCore _instance = null;
        static readonly object _lockObj = new object();

        
        #endregion

        public EuropeCore(int p)
        {

        }

        #region Instance
        public static EuropeCore Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new EuropeCore(265);
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 发布一场比赛
        /// </summary>
        /// <param name="homeName"></param>
        /// <param name="awayName"></param>
        /// <param name="matchTime"></param>
        /// <returns></returns>
        public MessageCodeResponse ReleaseMatch(string homeName,string awayName,DateTime matchTime)
        {
            EuropeMatchEntity entity = new EuropeMatchEntity(0, homeName, awayName, matchTime.Date, matchTime, 0, 0, 0,
                1,DateTime.Now, DateTime.Now);
            if (!EuropeMatchMgr.Insert(entity))
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail);
            return ResponseHelper.CreateSuccess<MessageCodeResponse>();
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
            DateTime date = DateTime.Now;
            var match = EuropeConfig.Instance.GetOneMatch(matchId);
            if (match == null)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
            if (match.MatchTime.AddHours(2) > date)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Match2HoursEnd);
            if (match.States != (int) EnumEuropeStatus.MatchIng)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
            match.HomeGoals = homeGoals;
            match.AwayGoals = awayGoals;
            if (homeGoals > awayGoals)
                match.ResultType = 1;
            else if (homeGoals == awayGoals)
                match.ResultType = 2;
            else
                match.ResultType = 3;
            match.States = (int) EnumEuropeStatus.MatchEnd;
            if (!EuropeMatchMgr.Update(match))
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail);
            EuropeConfig.Instance.SendGamblePrize(match);
            match.States = (int)EnumEuropeStatus.PrizeEnd;
            EuropeMatchMgr.Update(match);
            return ResponseHelper.CreateSuccess<MessageCodeResponse>();
        }

        /// <summary>
        /// 获取比赛日历
        /// </summary>
        /// <returns></returns>
        public MatchTheCalendarResponse GetMatchTheCalendar()
        {
            MatchTheCalendarResponse response = new MatchTheCalendarResponse();
            response.Data = new MatchTheCalendar();
            try
            {
               response.Data.TheCalendar = EuropeConfig.Instance.MatchTheCalendar;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取比赛日历", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 获取比赛列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchDate">比赛日期</param>
        /// <returns></returns>
        public GetEuropeMatchListResponse GetMatchList(Guid managerId,DateTime matchDate,int isDefault)
        {
            GetEuropeMatchListResponse response = new GetEuropeMatchListResponse();
            try
            {
                DateTime date = DateTime.Now;
                var list = EuropeConfig.Instance.GetMatchDay(matchDate);
                List<EuropeMatchEntity> matchList = new List<EuropeMatchEntity>();
                if (isDefault == 1)
                {
                    var notMatchList = list.FindAll(r => r.MatchTime > date);
                    for (int i = 0; i < 5; i++)
                    {
                        if (notMatchList.Count > 0)
                            break;
                        matchDate = matchDate.AddDays(1);
                        notMatchList = EuropeConfig.Instance.GetMatchDay(matchDate);
                    }
                    matchList.AddRange(notMatchList);
                }
                else
                {
                    matchList.AddRange(list);
                }
                var gambleRecord = EuropeGamblerecordMgr.GambleRecord(managerId);
                List<EuropeMatchEntity> resultList = new List<EuropeMatchEntity>();
                foreach (var item in matchList)
                {
                    var entity = new EuropeMatchEntity();
                    entity.AwayGoals = item.AwayGoals;
                    entity.AwayLogo = item.AwayLogo;
                    entity.AwayName = item.AwayName;
                    entity.HomeGoals = item.HomeGoals;
                    entity.HomeLogo = item.HomeLogo;
                    entity.HomeName = item.HomeName;
                    entity.IsAlreadyGamble = false;
                    entity.MatchDate = item.MatchDate;
                    entity.MatchId = item.MatchId;
                    entity.MatchTimeTick = item.MatchTimeTick;
                    entity.ResultType = item.ResultType;
                    entity.States = item.States;
                    if (gambleRecord.Exists(r => r.MatchId == item.MatchId))
                        entity.IsAlreadyGamble = true;
                    resultList.Add(entity);
                }
                response.Data = new GetEuropeMatchList();
                response.Data.MatchList = resultList;
                response.Data.MatchDateTick = ShareUtil.GetTimeTick(matchDate);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取欧洲杯比赛列表", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 竞猜比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="matchId"></param>
        /// <param name="gambleType"></param>
        /// <param name="pointType"></param>
        /// <returns></returns>
        public EuropeGambleMatchResponse GambleMatch(Guid managerId,int matchId,int gambleType,int pointType)
        {
            EuropeGambleMatchResponse response = new EuropeGambleMatchResponse();
            try
            {
                DateTime date = DateTime.Now;
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                {
                    response.Code = (int)MessageCode.MissManager;
                    return response;
                }
                var match =EuropeConfig.Instance.GetOneMatch(matchId);
                if (match == null)
                {
                    response.Code = (int) MessageCode.NotHaveMatch;
                    return response;
                }
                if (match.States != (int)EnumEuropeStatus.Gamble || match.MatchTime < date)//不可以竞猜
                {
                    response.Code = (int) MessageCode.HaveGambleTime;
                    return response;
                }
                var gambleRecord = EuropeGamblerecordMgr.GambleRecord(managerId, matchId);
                if (gambleRecord != null)//已经竞猜过
                {
                    response.Code = (int) MessageCode.HaveGamble;
                    return response;
                }
                var pointConfig = EuropeConfig.Instance.GetGamblePoint(pointType);
                if (pointConfig == null || pointConfig.VipLevel>manager.VipLevel)//竞猜点卷有误
                {
                    response.Code = (int) MessageCode.PointConfigNotHave;
                    return response;
                }
                var managerPoint = PayCore.Instance.GetPoint(managerId);
                if (managerPoint < pointConfig.Point)//钻石不足
                {
                    response.Code = (int) MessageCode.NbPointShortage;
                    return response;
                }
                gambleRecord = new EuropeGamblerecordEntity(0, managerId, matchId, gambleType, pointConfig.Point,0, false, false, date, date);

                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    MessageCode code = MessageCode.FailUpdate;
                    do
                    {
                        if (!EuropeGamblerecordMgr.Insert(gambleRecord, transactionManager.TransactionObject))
                            break;
                        code = PayCore.Instance.GambleTrueMatch(managerId, pointConfig.Point, ShareUtil.GenerateComb().ToString(), transactionManager.TransactionObject);
                    } while (false); 
                   
                    if (code != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        response.Code = (int)code;
                        return response;
                    }
                    transactionManager.Commit();
                    response.Data = new EuropeGambleMatch();
                    response.Data.Point = managerPoint - pointConfig.Point;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("欧洲杯竞猜", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 获取我的竞猜列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetMyEuropeGambleResponse GetMyGamble(Guid managerId)
        {
            GetMyEuropeGambleResponse response = new GetMyEuropeGambleResponse();
            try
            {
                var managerList = EuropeGamblerecordMgr.GambleRecord(managerId);
                response.Data = new GetMyEuropeGamble();
                
                foreach (var item in managerList)
                {
                    var match = EuropeConfig.Instance.GetOneMatch(item.MatchId);
                    if (match != null)
                    {
                        item.HomeName = match.HomeName;
                        item.AwayName = match.AwayName;
                        item.TimeTick = ShareUtil.GetTimeTick(item.RowTime);
                    }
                }
                response.Data.GambleList = managerList;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取我的竞猜列表", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 获取我的竞猜活动
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public EuropeGambleResponse GetMyActivity(Guid managerId)
        {
            EuropeGambleResponse response = new EuropeGambleResponse();
            try
            {
                var info = GetMyGambleInfo(managerId);
                var prizeList = info.PrizeRecord.Split(',');
                int setp = EuropeConfig.Instance.GetStepByWin(info.CorrectNumber);
                bool isUpdate = false;
                for (int i = 1; i <= 4; i++)
                {
                    if (prizeList.Length < i)
                    {
                        isUpdate = true;
                        prizeList[i - 1] = "0";
                    }
                    if (prizeList[i - 1] == "0" && setp >= i)
                    {
                        isUpdate = true;
                        prizeList[i - 1] = "1";
                    }
                }
                info.PrizeRecord = String.Join(",", prizeList);
                if (isUpdate)
                {
                    info.UpdateTime = DateTime.Now;
                    EuropeGambleMgr.Update(info);
                }
                info.ActivityEndTime = ShareUtil.GetTimeTick(EuropeConfig.Instance.EndTime);
                response.Data = info;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("欧洲杯获取我的竞猜活动", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 领取竞猜正确活动奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public EuropeGambleMatchResponse DrawPrize(Guid managerId, int step)
        {
            EuropeGambleMatchResponse response = new EuropeGambleMatchResponse();
            response.Data = new EuropeGambleMatch();
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                {
                    response.Code = (int) MessageCode.MissManager;
                    return response;
                }
                var info = GetMyGambleInfo(managerId);
                var prizeList = info.PrizeRecord.Split(',');
                if (prizeList.Length < step || prizeList[step - 1] == "0")
                {
                    response.Code = (int) MessageCode.TourNoPassPrize;
                    return response;
                }
                if (prizeList[step - 1] == "2")
                {
                    response.Code = (int) MessageCode.NbPrizeRepeat;
                    return response;
                }
                var prizeConfig = EuropeConfig.Instance.GetPrize(step);
                if (prizeConfig.Count <= 0)
                {
                    response.Code = (int) MessageCode.ActivityNoConfigPrize;
                    return response;
                }
                ItemPackageFrame package = null;
                
                prizeList[step - 1] = "2";
                info.PrizeRecord = String.Join(",", prizeList);
                info.UpdateTime = DateTime.Now;
                int addPoint = 0;
                int addCoin = 0;
                foreach (var prize in prizeConfig)
                {
                    switch (prize.PrizeType)
                    {
                        case 1:
                            addPoint += prize.PrizeCount;
                            break;
                        case 2:
                            addCoin += prize.PrizeCount;
                            break;
                        case 3:
                            if (package == null)
                            {
                                package = ItemCore.Instance.GetPackage(managerId,
                                    Entity.Enums.Shadow.EnumTransactionType.EuropeConfig);
                                if (package == null)
                                    return ResponseHelper.Create<EuropeGambleMatchResponse>(MessageCode.NbNoPackage);
                            }
                            package.AddItems(prize.PrizeCode, prize.PrizeCount);
                            break;
                    }
                }
                if (addCoin > 0)
                    ManagerUtil.AddManagerData(manager, 0, addCoin, 0, EnumCoinChargeSourceType.Eruope,
                        ShareUtil.GenerateComb().ToString());

                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var code = MessageCode.FailUpdate;
                    do
                    {
                        if (addPoint > 0)
                        {
                            code = PayCore.Instance.AddBonus(managerId, addPoint, EnumChargeSourceType.Europe,
                                ShareUtil.GenerateComb().ToString(), transactionManager.TransactionObject);
                            if (code != MessageCode.Success)
                                break;
                        }
                        if (addCoin > 0)
                        {
                            if (!ManagerUtil.SaveManagerData(manager, null, transactionManager.TransactionObject))
                                break;
                        }
                        if (package != null)
                        {
                            if (!package.Save(transactionManager.TransactionObject))
                                break;
                            package.Shadow.Save();
                        }
                        if (!EuropeGambleMgr.Update(info, transactionManager.TransactionObject))
                            break;
                        code = MessageCode.Success;
                    } while (false);

                    if (code != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        response.Code = (int)MessageCode.GamblePayError;
                        return response;
                    }
                    transactionManager.Commit();
                    response.Data.Point = PayCore.Instance.GetPoint(managerId);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("领取竞猜正确活动奖励", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 获取我的竞猜信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        private EuropeGambleEntity GetMyGambleInfo(Guid managerId)
        {
            DateTime date = DateTime.Now;
            var gambleInfo = EuropeGambleMgr.GetById(managerId);
            if (gambleInfo == null)
            {
                gambleInfo = new EuropeGambleEntity(managerId, 0, "0,0,0,0", date, date,EuropeConfig.Instance.Season);
                EuropeGambleMgr.Insert(gambleInfo);
            }
            else if (gambleInfo.SeasonId != EuropeConfig.Instance.Season)
            {
                //插入记录
                EuropeRecordMgr.Insert(new EuropeRecordEntity(0, gambleInfo.ManagerId, gambleInfo.SeasonId,
                    gambleInfo.CorrectNumber, gambleInfo.PrizeRecord, date));
                //更新活动
                gambleInfo.CorrectNumber = 0;
                gambleInfo.PrizeRecord = "0,0,0,0";
                gambleInfo.SeasonId = EuropeConfig.Instance.Season;
                gambleInfo.UpdateTime = date;
                EuropeGambleMgr.Update(gambleInfo);
            }
            return gambleInfo;
        }

    }
}
