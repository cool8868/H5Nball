using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
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
    public class EuropeConfig
    {
        #region
        /// <summary>
        /// 每天所有比赛
        /// </summary>
        public ConcurrentDictionary<DateTime, List<EuropeMatchEntity>> _allMatch;
        /// <summary>
        /// 一场比赛
        /// </summary>
        public ConcurrentDictionary<int, EuropeMatchEntity> _oneMatch;

        private Dictionary<int, GamblePointConfig> _gamblePoint;

        private Dictionary<string, int> _logoDic;

        private Dictionary<int, List<ConfigEuropeprizeEntity>> _prizeDic;
        private Dictionary<int, int> _winStepDic;

        private static EuropeConfig _instance = null;

        //private Timer _timer;
        //private Timer _timer1;

        /// <summary>
        /// 每月结算配置
        /// </summary>
        private EuropeSeasonEntity _season;

        /// <summary>
        /// 是否有比赛
        /// </summary>
        private Dictionary<long, bool> _matchInfoDic;
        static readonly object _lockObj = new object();
        private EuropeConfig(int p)
        {
            DateTime date = DateTime.Now;
            _season = EuropeSeasonMgr.GetSeason(date);
            _gamblePoint = new Dictionary<int, GamblePointConfig>();
            _logoDic = new Dictionary<string, int>();
            _prizeDic = new Dictionary<int, List<ConfigEuropeprizeEntity>>();
            _winStepDic = new Dictionary<int, int>();
            InitMatch();
            var europeGamble = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.EuropeGamblePoint);
            if (europeGamble.Length > 0)
            {
                var europe = europeGamble.Split('|');
                foreach (var s in europe)
                {
                    var list = s.Split(',');
                    if (list.Length > 2)
                    {
                        GamblePointConfig entity = new GamblePointConfig();
                        entity.GambleType = ConvertHelper.ConvertToInt(list[0]);
                        entity.Point = ConvertHelper.ConvertToInt(list[1]);
                        entity.VipLevel = ConvertHelper.ConvertToInt(list[2]);
                        _gamblePoint.Add(entity.GambleType, entity);
                    }
                }
            }
            var allPrize = ConfigEuropeprizeMgr.GetAll();
            foreach (var item in allPrize)
            {
                if (!_prizeDic.ContainsKey(item.Step))
                    _prizeDic.Add(item.Step, new List<ConfigEuropeprizeEntity>());
                _prizeDic[item.Step].Add(item);

                if (!_winStepDic.ContainsKey(item.WinNumber))
                    _winStepDic.Add(item.WinNumber, item.Step);
            }
            //_timer = new Timer();
            //_timer.Interval = 10000;
            //_timer.Elapsed += Timer_Elapsed;
            //_timer.Start();
            //_timer1 = new Timer();
            //_timer1.Interval = 600000;
            //_timer1.Elapsed += Timer_Elapsed1;
            //_timer1.Start();
        }

        private void InitMatch()
        {
            DateTime date = DateTime.Now;
            //排除1个月之前的比赛
            var allMatch = EuropeMatchMgr.GetAllMatvch(date.AddDays(-30));
            var allLogo = ConfigGambleiconMgr.GetAll();
            _logoDic = allLogo.ToDictionary(r => r.Name.Trim(), r => r.Idx);
            _allMatch = new ConcurrentDictionary<DateTime, List<EuropeMatchEntity>>();
            _oneMatch = new ConcurrentDictionary<int, EuropeMatchEntity>();
            foreach (var item in allMatch)
            {
                item.HomeLogo = GetLogo(item.HomeName);
                item.AwayLogo = GetLogo(item.AwayName);
                item.MatchTimeTick = ShareUtil.GetTimeTick(item.MatchTime);
                if (!_allMatch.ContainsKey(item.MatchDate))
                    _allMatch.TryAdd(item.MatchDate, new List<EuropeMatchEntity>());
                _allMatch[item.MatchDate].Add(item);
                _oneMatch.TryAdd(item.MatchId, item);
            }

            //是否有比赛
            _matchInfoDic = new Dictionary<long, bool>();
            for (int i = -7; i <= 7; i++)
            {
                var isMatch = EuropeMatchMgr.GetIsMatch(date.Date.AddDays(i)) > 0;
                _matchInfoDic.Add(ShareUtil.GetTimeTick(date.Date.AddDays(i)), isMatch);
            }
        }

        #endregion


        #region Instance
        public static EuropeConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new EuropeConfig(265);
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion


        //private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    _timer.Stop();
        //    DateTime date = DateTime.Now;
        //    try
        //    {
        //        var allMatch = new List<EuropeMatchEntity>();
        //        if (_allMatch.ContainsKey(date.Date.AddDays(-1)))
        //            allMatch.AddRange(_allMatch[date.Date.AddDays(-1)]);
        //        if (_allMatch.ContainsKey(date.Date)) //获取当天的比赛
        //            allMatch.AddRange(_allMatch[date.Date]);
        //        foreach (var entity in allMatch)
        //        {
        //            if (entity.States ==(int)EnumEuropeStatus.Gamble && entity.MatchTime<= date)
        //            {
        //                entity.States = (int) EnumEuropeStatus.MatchIng;
        //                EuropeMatchMgr.Update(entity);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        SystemlogMgr.Error("欧洲杯更新比赛状态", ex);
        //    }
        //    _timer.Start();
        //}

        /// <summary>
        /// 刷新比赛  发奖
        /// </summary>
        public MessageCode RefreshMatch()
        {
            DateTime date = DateTime.Now;
            InitMatch();
            try
            {
                var allMatch = new List<EuropeMatchEntity>();
                if (_allMatch.ContainsKey(date.Date.AddDays(-1)))
                    allMatch.AddRange(_allMatch[date.Date.AddDays(-1)]);
                if (_allMatch.ContainsKey(date.Date)) //获取当天的比赛
                    allMatch.AddRange(_allMatch[date.Date]);
                foreach (var entity in allMatch)
                {
                    if (entity.States == (int) EnumEuropeStatus.Gamble && entity.MatchTime <= date)
                    {
                        entity.States = (int) EnumEuropeStatus.MatchIng;
                        EuropeMatchMgr.Update(entity);
                    }
                    if (entity.States == (int) EnumEuropeStatus.MatchEnd)
                    {
                        if (entity.ResultType == 0)
                        {
                            if (entity.HomeGoals > entity.AwayGoals)
                                entity.ResultType = 1;
                            else if (entity.HomeGoals == entity.AwayGoals)
                                entity.ResultType = 2;
                            else
                                entity.ResultType = 3;
                        }
                        SendGamblePrize(entity);
                        entity.States = (int) EnumEuropeStatus.PrizeEnd;
                        //EuropeMatchMgr.Update(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("欧洲杯刷新比赛  发奖", ex);
                return MessageCode.NbParameterError;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 刷新活动信息
        /// </summary>
        /// <returns></returns>
        public MessageCode RefreshActivity()
        {
            try
            {
                DateTime date = DateTime.Now;
                _season = EuropeSeasonMgr.GetSeason(date);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("欧洲杯刷活动信息", ex);
                return MessageCode.NbParameterError;
            }
            return MessageCode.Success;
        }

        public void SendGamblePrize(EuropeMatchEntity match)
        {
            try
            {
                if (match == null || match.States != (int)EnumEuropeStatus.MatchEnd || match.ResultType==0)
                    return;
                SendPrize(match);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("欧洲杯发奖", ex);
            }
        }

        public void SendPrize(EuropeMatchEntity match)
        {
            DateTime date = DateTime.Now;
            //获取未发奖的竞猜
            var sendPrizeList = EuropeGamblerecordMgr.GetNotPrize(match.MatchId);
            foreach (var item in sendPrizeList)
            {
                if (item.IsSendPrize)
                    continue;
                item.IsSendPrize = true;
                item.UpdateTime = date;
                MailBuilder mail = null;
                EuropeGambleEntity gambleInfo = null;
                bool isInsertInfo = false;
                if (item.GambleType == match.ResultType) //竞猜正确
                {
                    item.IsGambleCorrect = true;
                    item.ReturnPoint = item.Point * 2;
                    //发邮件
                    mail = new MailBuilder(item.ManagerId, EnumMailType.Europe, item.ReturnPoint, match.HomeName,
                        match.AwayName);
                    gambleInfo = EuropeGambleMgr.GetById(item.ManagerId);
                    if (gambleInfo == null)
                    {
                        isInsertInfo = true;
                        gambleInfo = new EuropeGambleEntity(item.ManagerId, 1, "0,0,0,0", date, date,this.Season);
                    }
                    else
                    {
                        if (gambleInfo.SeasonId != this.Season)
                        {
                            //插入记录
                            EuropeRecordMgr.Insert(new EuropeRecordEntity(0, gambleInfo.ManagerId, gambleInfo.SeasonId,
                                gambleInfo.CorrectNumber, gambleInfo.PrizeRecord, date));
                            //更新活动
                            gambleInfo.CorrectNumber = 0;
                            gambleInfo.PrizeRecord = "0,0,0,0";
                            gambleInfo.SeasonId = this.Season;
                        }
                        gambleInfo.CorrectNumber++;
                        gambleInfo.UpdateTime = date;
                    }
                }
                else
                {
                    item.IsGambleCorrect = false;
                }
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    MessageCode code = MessageCode.FailUpdate;
                    do
                    {
                        if (mail != null)
                        {
                            if (!mail.Save(transactionManager.TransactionObject))
                                break;
                        }
                        if (gambleInfo != null)
                        {
                            if (isInsertInfo)
                            {
                                if (!EuropeGambleMgr.Insert(gambleInfo, transactionManager.TransactionObject))
                                    break;
                            }
                            else
                            {
                                if (!EuropeGambleMgr.Update(gambleInfo, transactionManager.TransactionObject))
                                    break;
                            }
                        }
                        if (!EuropeGamblerecordMgr.Update(item, transactionManager.TransactionObject))
                            break;
                        code = MessageCode.Success;
                    } while (false);

                    if (code != MessageCode.Success)
                        transactionManager.Rollback();
                    else
                        transactionManager.Commit();
                }
            }
        }

        public DateTime EndTime {
            get
            {
                if (_season != null)
                    return _season.EndDate.AddDays(1).AddSeconds(-1);
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 赛季信息
        /// </summary>
        public int Season{
            get
            {
                if (_season != null)
                    return _season.Idx;
                return 0;
            }
        }

        /// <summary>
        /// 比赛日历
        /// </summary>
        public Dictionary<long, bool> MatchTheCalendar
        {
            get { return _matchInfoDic; }
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
                gambleInfo = new EuropeGambleEntity(managerId, 0, "0,0,0,0", date, date,this.Season);
                EuropeGambleMgr.Insert(gambleInfo);
            }
            else if (gambleInfo.SeasonId != this.Season)
            {
                //插入记录
                EuropeRecordMgr.Insert(new EuropeRecordEntity(0, gambleInfo.ManagerId, gambleInfo.SeasonId,
                    gambleInfo.CorrectNumber, gambleInfo.PrizeRecord, date));
                //更新活动
                gambleInfo.CorrectNumber = 0;
                gambleInfo.PrizeRecord = "0,0,0,0";
                gambleInfo.SeasonId = this.Season;
                gambleInfo.UpdateTime = date;
                EuropeGambleMgr.Update(gambleInfo);
            }
            return gambleInfo;
        }

        /// <summary>
        /// 获取某一天的比赛
        /// </summary>
        /// <param name="matchDate"></param>
        /// <returns></returns>
        public List<EuropeMatchEntity> GetMatchDay(DateTime matchDate)
        {
            if (_allMatch.ContainsKey(matchDate))
                return _allMatch[matchDate];
            return new List<EuropeMatchEntity>();
        }

        /// <summary>
        /// 获取一场比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public EuropeMatchEntity  GetOneMatch(int matchId)
        {
            if (_oneMatch.ContainsKey(matchId))
                return _oneMatch[matchId];
            return null;
        }

        /// <summary>
        /// 获取竞猜点卷配置
        /// </summary>
        /// <param name="pointType"></param>
        /// <returns></returns>
        public GamblePointConfig GetGamblePoint(int pointType)
        {
            if (_gamblePoint.ContainsKey(pointType))
                return _gamblePoint[pointType];
            return null;
        }

        public int GetLogo(string name)
        {
            if (_logoDic.ContainsKey(name))
                return _logoDic[name];
            return 0;
        }

        /// <summary>
        /// 根据步骤获取奖励
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public List<ConfigEuropeprizeEntity> GetPrize(int step)
        {
            if (_prizeDic.ContainsKey(step))
                return _prizeDic[step];
            return new List<ConfigEuropeprizeEntity>();
        }

        /// <summary>
        /// 根据押对场次获取奖励步骤
        /// </summary>
        /// <param name="winNumber"></param>
        /// <returns></returns>
        public int GetStepByWin(int winNumber)
        {
            int setp = 0;
            foreach (var item in _winStepDic)
            {
                if (item.Key <= winNumber && setp < item.Value)
                    setp = item.Value;
            }
            return setp;
        }
    }
}
