using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
//using Games.NBall.Core.Active;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response.Dailycup;

namespace Games.NBall.Core.Dailycup
{
    public class DailycupCore
    {
        #region .ctor

        
        public DailycupCore(int b)
        {
            string[] dcFinalRoundStarts = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.DailycupFinalRoundStart).Split(',');
            int dcFinalRoundMatchTime = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DailycupFinalRoundMatchTime);
            int dcGambleCloseTime = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DailycupGambleCloseTime);
            int dcGambleOpenTime = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DailycupGambleOpenTime);
            _dailycupTimeDic = new Dictionary<int, DailycupRoundTimeEntity>(dcFinalRoundStarts.Length);
            for (int i = 0; i < dcFinalRoundStarts.Length; i++)
            {
                var roundTime = new DailycupRoundTimeEntity();
                var matchTime = Convert.ToInt32(dcFinalRoundStarts[i]);
                roundTime.MatchStartTime = matchTime;
                roundTime.MatchEndTime = matchTime + dcFinalRoundMatchTime;
                roundTime.GambelCloseTime = matchTime - dcGambleCloseTime;
                roundTime.GambelOpenTime = matchTime + dcGambleOpenTime;
                _dailycupTimeDic.Add(i, roundTime);
            }
            
        }
        #endregion

        #region Facade

        public static DailycupCore Instance
        {
            get { return SingletonFactory<DailycupCore>.SInstance; }
        }

        public Dictionary<int, DailycupRoundTimeEntity> DailycupTimeDic { get { return _dailycupTimeDic; } }

        #region 获得杯赛数据

        public DailycupFullDataResponse GetDailycupData(Guid managerId, int dailycupId)
        {
            return GetDailycupData(managerId, dailycupId, DateTime.Now);
        }

        /// <summary>
        /// Gets the full dailycup data by day.
        /// </summary>
        /// <param name="managerId">The manager.</param>
        /// <param name="dailycupId">The daily.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        public DailycupFullDataResponse GetDailycupData(Guid managerId, int dailycupId,DateTime curTime)
        {
            try
            {
                DailycupInfoEntity dailycup = null;
                if (dailycupId == 0)
                {
                    dailycup = DailycupInfoMgr.GetByDate(DateTime.Today);
                    if (dailycup == null)
                    {
                        return ResponseHelper.Create<DailycupFullDataResponse>(MessageCode.DailycupNotExists);
                    }
                }
                else
                {
                    dailycup = DailycupInfoMgr.GetById(dailycupId);
                }
                if (dailycup == null)
                {
                    return ResponseHelper.InvalidParameter<DailycupFullDataResponse>();
                }
                if (dailycup.RunDate == DateTime.Today) //判断是否是当日杯赛，今天的杯赛是昨天创建
                {
                    return GetDailycupDataToday(managerId, dailycup, curTime);
                }
                else
                {
                    //计算开始回合
                    int beginRound = BeginRound(dailycup.Round);
                    int endRound = dailycup.Round;
                    DailycupFullDataEntity dailycupData = new DailycupFullDataEntity();
                    dailycupData.DailycupId = dailycup.Idx;
                    GetDailycupData(dailycupData, managerId, beginRound, endRound);
                    foreach (DailycupMatchEntity match in dailycupData.Matchs)
                    {
                        match.Status = (int)EnumDailycupMatchStatus.ShowScore;
                    }
                    dailycupData.RoundType = CalRoundType(endRound, beginRound);
                    dailycupData.HasNext = dailycup.RunDate < DateTime.Today.AddDays(1);
                    dailycupData.AttendState = GetAttendState(managerId) ? 1 : 0;
                    var response = ResponseHelper.CreateSuccess<DailycupFullDataResponse>();
                    response.Data = dailycupData;
                    return response;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetDailycupData", ex);
                return ResponseHelper.Create<DailycupFullDataResponse>(MessageCode.Exception);
            }
        }

        DailycupFullDataResponse GetDailycupDataToday(Guid managerId, DailycupInfoEntity dailycup,DateTime curTime)
        {
            DailycupFullDataEntity dailycupData = new DailycupFullDataEntity();
            dailycupData.DailycupId = dailycup.Idx;
            dailycupData.HasNext = true;
            dailycupData.AttendState = GetAttendState(managerId) ? 1 : 0;
            if (dailycup.Status > (int)EnumDailycupStatus.Close)
            {
                //计算开始回合
                int beginRound = BeginRound(dailycup.Round);
                int endRound = NowShowRound(beginRound, dailycup.Round, curTime);
                dailycupData.RoundType = CalRoundType(endRound, beginRound);
                if (endRound >= beginRound)
                {
                    GetDailycupData(dailycupData, managerId, beginRound, endRound);
                    //最新轮时间
                    DateTime cTime = NowRoundEndTime(dailycup.Round, endRound);
                    if (curTime < cTime) //最新轮比分是否隐藏
                    {
                        int maxCount = 0;
                        bool hasGambleRight = CheckGambleCount(dailycupData.MyGambleData.Count, managerId, out maxCount);
                        foreach (DailycupMatchEntity match in dailycupData.Matchs)
                        {
                            if (match.Round == endRound)
                            {
                                match.HomeScore = -1;
                                match.AwayScore = -1;
                                if (dailycupData.MyGambleData.Exists(gamble => gamble.MatchId == match.Idx))
                                {
                                    match.Status = (int) EnumDailycupMatchStatus.HasGamble;
                                }
                                else
                                {
                                    if (hasGambleRight)
                                        match.Status = (int) EnumDailycupMatchStatus.Gamble;
                                    else
                                    {
                                        match.Status = (int) EnumDailycupMatchStatus.NoGambel;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var response = ResponseHelper.CreateSuccess<DailycupFullDataResponse>();
            response.Data = dailycupData;
            return response;
        }

        bool GetAttendState(Guid managerId)
        {
            var dailycup = DailycupInfoMgr.GetByDate(DateTime.Today.AddDays(1));
            if (dailycup == null || dailycup.Idx <= 0)
                return false;
            bool val = false;
            DailycupCompetitorsMgr.ExistsByManager(dailycup.Idx, managerId, ref val);
            return val;
        }

        void GetDailycupData(DailycupFullDataEntity dailycupData, Guid managerId, int beginRound, int endRound)
        {
            try
            {
                List<DailycupMatchEntity> matchlist = DailycupMatchMgr.GetMatchByRound(dailycupData.DailycupId, beginRound, endRound);
                var manager = ManagerCore.Instance.GetManager(managerId);
                var gambleCountMax = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.DailycupGambleCount);
                //BuffPlusHelper.DailycupGambleCount(ref gambleCountMax);
                dailycupData.GambleCountMax = gambleCountMax;
                dailycupData.MyGambleData = DailycupGambleMgr.GetMyGamebleData(dailycupData.DailycupId, managerId);
                if (dailycupData.MyGambleData != null)
                {
                    foreach (var entity in dailycupData.MyGambleData)
                    {
                        entity.TimeTick = ShareUtil.GetTimeTick(entity.RowTime);
                    }
                }
                foreach (var entity in matchlist)
                {
                    entity.RoundType = CalRoundType(entity.Round, beginRound);
                }

                dailycupData.Matchs = matchlist;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("doGetDailycupData", ex);
            }
        }
        #endregion

        #region 获取我的杯赛历程
        /// <summary>
        /// 获取我的杯赛历程.
        /// </summary>
        /// <param name="managerId">The manager.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        public MyDailycupMatchResponse GetMyDailycupMatch(Guid managerId)
        {
            var response = ResponseHelper.CreateSuccess<MyDailycupMatchResponse>();
            response.Data = new MyDailycupMatchEntity();
            
            DailycupInfoEntity dailycup = DailycupInfoMgr.GetByDate(DateTime.Today);
            if (dailycup != null && dailycup.Status > (int)EnumDailycupStatus.Close)
            {
                //计算开始回合
                int beginRound = BeginRound(dailycup.Round);
                int endRound = NowShowRound(beginRound, dailycup.Round, DateTime.Now);
                if (endRound < beginRound)
                {
                    response.Data = new MyDailycupMatchEntity();
                    response.Data.Matchs = new List<DailycupMatchEntity>(0);
                    return response;
                }
                //最新轮时间
                DateTime cTime = NowRoundEndTime(dailycup.Round, endRound);
                if (DateTime.Now < cTime)//最新轮是否隐藏
                {
                    endRound--;
                }
                List<DailycupMatchEntity> dailycupmatchs = DailycupMatchMgr.GetMatchByManager(dailycup.Idx, managerId, endRound);
                response.Data.Matchs = dailycupmatchs;
            }
            else
            {
                response.Data.Matchs = new List<DailycupMatchEntity>(0);
            }
            return response;
        }
        #endregion

        #region 杯赛Round相关
        /// <summary>
        /// Nows the round time.
        /// </summary>
        /// <param name="allRound">All round.</param>
        /// <param name="nowShowRound">The now show round.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        public DateTime NowRoundEndTime(int allRound, int nowShowRound)
        {
            if (nowShowRound < 0)
                return DateTime.Now;
            //计算显示轮数间隔
            int roundInterval = 2 + nowShowRound - allRound;
            DailycupRoundTimeEntity dailycupRoundTimeEntity = GetDailycupRoundTime(roundInterval);
            if (dailycupRoundTimeEntity == null)
                return DateTime.Now;
            return ConvertToDateTime(dailycupRoundTimeEntity.MatchEndTime);
        }

        /// <summary>
        /// 当前该显示第几轮,
        /// <param name="allRound">杯赛共几轮</param>
        /// </summary>
        /// <param name="allRound">All round.</param>
        /// <param name="currentTime">The current time.</param>
        /// <returns>如果不显示，返还-1，显示返还正常第几轮</returns>
        public int NowShowRound(int startRound,int allRound, DateTime currentTime)
        {
            //先检查是否满足16强
            int endRound = startRound;
            //从开始轮算
            Dictionary<int, DailycupRoundTimeEntity> dcRoundTimes = _dailycupTimeDic;
            var compareTime = ConvertToDateTime(dcRoundTimes[0].MatchEndTime);
            if (currentTime < compareTime)//还未到显示杯赛的时间
            {
                compareTime = ConvertToDateTime(CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DailycupMyFirstRoundTime));
                //显示第一轮
                if (currentTime < compareTime)
                {
                    return -1;
                }
                else
                {
                    return startRound; //显示到16强为止
                }

            }
            for (int i = 0; i <=1; i++)
            {
                compareTime = ConvertToDateTime(dcRoundTimes[i].MatchEndTime);
                if (currentTime > compareTime)
                {
                    endRound=startRound+i+1;
                }
                else
                {
                    break;
                }
            }
            return endRound;
        }

        /// <summary>
        /// 当前该开奖到第几轮,
        /// <param name="allRound">杯赛共几轮</param>
        /// </summary>
        /// <param name="allRound">All round.</param>
        /// <param name="currentTime">The current time.</param>
        /// <returns>如果不显示，返还-1</returns>
        public int NowGambleOpenRound(int allRound, DateTime currentTime)
        {
            //先检查是否满足16强
            int startRound = BeginRound(allRound);
            int endRound = startRound;

            DateTime compareTime;

            //从开始轮算
            Dictionary<int, DailycupRoundTimeEntity> dcRoundTimes = _dailycupTimeDic;
            compareTime = ConvertToDateTime(dcRoundTimes[0].GambelOpenTime);
            if (currentTime < compareTime)//还未到显示杯赛的事件
            {
                return -1;
            }
            for (int i = 0; i <= 2; i++)
            {
                compareTime = ConvertToDateTime(dcRoundTimes[i].GambelOpenTime);
                if (currentTime >= compareTime)
                {
                    endRound = startRound + i;
                }
                else
                {
                    break;
                }
            }
            return endRound;
        }

        /// <summary>
        /// Begins the round.
        /// </summary>
        /// <param name="allRound">All round.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        public int BeginRound(int allRound)
        {
            //先检查是否满足16强
            int startRound = allRound - CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DailycupSemiFinalRoundCount);
            if (startRound <= 0)
            {
                startRound = 1;
            }
            return startRound + 1;
        }

        /// <summary>
        /// Converts to date time.
        /// </summary>
        /// <param name="second">The second.</param>
        /// <returns></returns>
        public DateTime ConvertToDateTime(int second)
        {
            return DateTime.Today.AddSeconds(second);
        }
        #endregion

        #region AttendGamble

        /// <summary>
        /// 杯赛竞猜任务，仅需打开
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="hasTask"></param>
        /// <returns></returns>
        public MessageCodeResponse AttendGambleTask(Guid managerId, bool hasTask)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<MessageCodeResponse>();

            var response = ResponseHelper.CreateSuccess<MessageCodeResponse>();
            response.Data = new MessageDataEntity();
            if (hasTask)
            {
                response.Data.PopMsg = TaskHandler.Instance.DailycupGamble(managerId);
            }
            return response;
        }
        


        /// <summary>
        /// 竞猜每日杯赛.
        /// </summary>
        /// <param name="managerId">The manager.</param>
        /// <param name="gamblePoint">押注金额</param>
        /// <param name="gambleResult">竞猜结果：1，主队胜；2，客队胜</param>
        /// <param name="matchId">比赛Id</param>
        /// <returns>如果为空，则押注成功，否则就是不成功的信息</returns>
        public DailycupAttendGambleResponse AttendGamble(Guid managerId, int gamblePoint, int gambleResult, Guid matchId,bool hasTask)
        {
            DailycupMatchEntity match = DailycupMatchMgr.GetById(matchId);

            #region Check

            if (match == null)
            {
                return ResponseHelper.InvalidParameter<DailycupAttendGambleResponse>();
            }
            int dailycupId = match.DailyCupId;
            var dailycup = DailycupInfoMgr.GetById(dailycupId);
            if (dailycup == null)
            {
                return ResponseHelper.InvalidParameter<DailycupAttendGambleResponse>();
            }
            //押注已过期
            if (IsGambleClosed(dailycup, match))
            {
                return ResponseHelper.Create<DailycupAttendGambleResponse>(MessageCode.DailycupGambleClosed);
            }
            int maxCount = 0;
            var manager = ManagerCore.Instance.GetManager(managerId);


            //超过两个（包含2）球员被下注，就不能再下注了
            var gambleCount = DailycupGambleMgr.GambleCheck(dailycupId, manager.Idx, matchId);
            if (gambleCount == -1)
            {
                return ResponseHelper.InvalidParameter<DailycupAttendGambleResponse>();
            }
            if (!CheckGambleCount(gambleCount, manager, out maxCount))
            {
                var response =
                    ResponseHelper.Create<DailycupAttendGambleResponse>(MessageCode.DailycupGamebleCountLimit);
                response.Data = new DailycupAttendGambleEntity();
                response.Data.MaxGambleCount = maxCount;
                return response;
            }

            if (!CheckGamblePoint(gamblePoint, manager,out maxCount))
            {
                var response =
                      ResponseHelper.Create<DailycupAttendGambleResponse>(MessageCode.DailycupGamblePointLimit);
                response.Data = new DailycupAttendGambleEntity();
                response.Data.MaxGambleCount = maxCount;
                return response;
            }
            //验证钻石数量
            var pointCount = PayCore.Instance.GetPoint(managerId);
            if (pointCount < gamblePoint)
            {
                return ResponseHelper.Create<DailycupAttendGambleResponse>(MessageCode.LackofGold);
            }
            #endregion

           
            DailycupGambleEntity dailycupgamble = new DailycupGambleEntity();

            dailycupgamble.DailyCupId = dailycupId;
            if (gambleResult == 1)//押主队
            {
                dailycupgamble.GambleManagerId = match.HomeManager;
                dailycupgamble.GambleManagerName = match.HomeName;
            }
            else//押客队
            {
                dailycupgamble.GambleManagerId = match.AwayManager;
                dailycupgamble.GambleManagerName = match.AwayName;
            }
            dailycupgamble.GambleResult = gambleResult;
            dailycupgamble.GamblePoint = gamblePoint;
            dailycupgamble.ResultPoint = 0;
            dailycupgamble.ManagerId = manager.Idx;
            dailycupgamble.MatchId = matchId;
            dailycupgamble.Status = 0;
            dailycupgamble.RowTime = DateTime.Now;
            dailycupgamble.RoundLevel = CalRoundType(dailycup.Round, match.Round);
            dailycupgamble.HomeName = match.HomeName;
            dailycupgamble.AwayName = match.AwayName;
            var isSuccess = DailycupGambleMgr.Insert(dailycupgamble);
            if (isSuccess)
            {
                DailycupMatchMgr.UpdateMatchChipInCount(matchId);
                //扣除钻石
                var code = PayCore.Instance.GambleTrueMatch(managerId, gamblePoint, Guid.NewGuid().ToString());
                if (code != MessageCode.Success)
                {
                    SystemlogMgr.Error("杯赛押注", "押注成功，扣除钻石失败");
                }

                var response= ResponseHelper.CreateSuccess<DailycupAttendGambleResponse>();
                response.Data = new DailycupAttendGambleEntity();
                dailycupgamble.TimeTick = ShareUtil.GetTimeTick(dailycupgamble.RowTime);
                response.Data.GambleData = dailycupgamble;

                response.Data.PopMsg = TaskHandler.Instance.DailycupGamble(managerId);
                if (hasTask)
                {
                    //response.Data.PopMsg = TaskHandler.Instance.DailycupGamble(managerId);
                }
                //ActiveCore.Instance.AddActive(managerId, EnumActiveType.CupLeagueGuessing, 1);
                return response;
            }
            else
            {
                return ResponseHelper.Create<DailycupAttendGambleResponse>(MessageCode.NbUpdateFail);
            }

        }

        /// <summary>
        /// 判断投注是否关闭
        /// </summary>
        /// <param name="dailycup">The dailycup.</param>
        /// <param name="match">The match.</param>
        /// <returns>
        /// 	<c>true</c> if [is gamble closed] [the specified dailycup id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsGambleClosed(DailycupInfoEntity dailycup, DailycupMatchEntity match)
        {
            DateTime now = DateTime.Now;
            DateTime gambleLimitTime = DateTime.Today;

            //第一轮比赛投注截止时间
            gambleLimitTime = gambleLimitTime.AddSeconds(GetDailycupRoundTime(0).GambelCloseTime);
            if (match.Round == BeginRound(dailycup.Round))//当前押注的是第一轮比赛
            {
                if (DateTime.Now < gambleLimitTime)
                {
                    if (DateTime.Now > DateTime.Today.AddSeconds(CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DailycupMyFirstRoundTime)))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            int interValround = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DailycupSemiFinalRoundCount) - (dailycup.Round - match.Round + 1);
            if (interValround < 0 || interValround > 2)
                return true;

            //投注截止时间           
            gambleLimitTime = DateTime.Today.AddSeconds(GetDailycupRoundTime(interValround).GambelCloseTime);

            //投注开始时间
            DateTime preTime = DateTime.Today.AddSeconds(GetDailycupRoundTime(interValround - 1).MatchEndTime);

            if (now >= gambleLimitTime || now < preTime)
                return true;

            return false;
        }
        #endregion

        #region 报名
        /// <summary>
        /// 报名每日杯赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse Attend(Guid managerId, bool hasTask)
        {
            var dailycup = DailycupInfoMgr.GetByDate(DateTime.Today.AddDays(1));
            if (dailycup == null || dailycup.Idx <= 0)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.DailycupNoMatchTomorrow);
            }
            
            if (dailycup.RowTime.Date != DateTime.Now.Date)//检查时间
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.DailycupTimeOut);
            }
            //报名需要500金币
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<MessageCodeResponse>();
            //if (manager.Coin < 500)
            //    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.LackofCoin);

           //var meesscode =  ActiveCore.Instance.AddActive(managerId, EnumActiveType.CupLeague, 1);
           // if(meesscode!= MessageCode.Success)
           //     return ResponseHelper.Create<MessageCodeResponse>((int)meesscode);
            int returnCode = -2;
            DailycupCompetitorsMgr.Attend(dailycup.Idx, managerId, (int) MessageCode.DailycupAttendRepeat,
                                          ref returnCode);
            if (returnCode == (int) MessageCode.Success)
            {
                //扣除金币
                //ManagerCore.Instance.CostCoin(manager, 500, EnumCoinConsumeSourceType.DailycupAttend,
                //    ShareUtil.CreateSequential().ToString());

                var response = ResponseHelper.CreateSuccess<MessageCodeResponse>();
                response.Data = new MessageDataEntity();

                response.Data.PopMsg = TaskHandler.Instance.DailycupAttend(managerId);
                
                return response;
            }
            else
            {
                return ResponseHelper.Create<MessageCodeResponse>(returnCode);
            }
        }
        #endregion

        #endregion

        #region encapsulation
        private Dictionary<int, DailycupRoundTimeEntity> _dailycupTimeDic = null;

        DailycupRoundTimeEntity GetDailycupRoundTime(int roundInterval)
        {
            DailycupRoundTimeEntity dailycupRoundTime = null;
            _dailycupTimeDic.TryGetValue(roundInterval, out dailycupRoundTime);
            return dailycupRoundTime;
        }
        
        bool CheckGambleCount(int gambleCount, Guid managerId, out int maxCount)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);

            return CheckGambleCount(gambleCount, manager, out maxCount);
        }

        /// <summary>
        /// 每日押注次数核对(月卡2次，无月卡1次)
        /// </summary>
        /// <param name="gambleCount"></param>
        /// <param name="manager"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        bool CheckGambleCount(int gambleCount, NbManagerEntity manager, out int maxCount)
        {

            maxCount = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.DailycupGambleCount);

            //BuffPlusHelper.DailycupGambleCount(ref maxCount);
            var monthCard = ManagerMonthcardMgr.GetById(manager.Idx);
            if (monthCard != null && monthCard.DueToTime.Date >= DateTime.Now.Date)
                maxCount++;
            if (gambleCount >= maxCount)
                return false;
            return true;
        }

        /// <summary>
        /// 押注金额核对
        /// </summary>
        /// <param name="gamblePoint"></param>
        /// <param name="manager"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        private bool CheckGamblePoint(int gamblePoint, NbManagerEntity manager, out int maxCount)
        {
            maxCount = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.DaiycupGamblePoint);
            if (gamblePoint > maxCount)
                return false;
            return true;
        }

        int CalRoundType(int round, int beginRound)
        {
            var sub = round - beginRound;
            int roundType = 1;
            switch (sub)
            {
                case 0:
                    roundType = 1;
                    break;
                case 1:
                    roundType = 2;
                    break;
                case 2:
                    roundType = 3;
                    break;
            }
            return roundType;
        }
        #endregion

    }
}
