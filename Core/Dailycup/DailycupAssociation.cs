using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Shadow;

namespace Games.NBall.Core.Dailycup
{
    public class DailycupAssociation
    {
        private static string _dailycupAttendPrizeItem;//杯赛报名奖励物品
        private static string _dailycupWinPrizeItem;//杯赛胜场奖励物品
        private static int _dailycupWinPrizeCoin;
        #region .ctor
        public DailycupAssociation(int p)
        {
            _dailycupAttendPrizeItem =
                CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.DailycupAttendPrizeItem);
            _dailycupWinPrizeCoin =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.DailycupWinPrizeCoin);
            _dailycupWinPrizeItem =
                CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.DailycupWinPrizeItem);
        }
        #endregion

        #region Facade
        public static DailycupAssociation Instance
        { get { return SingletonFactory<DailycupAssociation>.SInstance; } }

        #region RunDailycup
        private int _syncRunDailycup = 0;
        public MessageCode RunDailycup(int dailycupId)
        {
            var dailycup = DailycupInfoMgr.GetById(dailycupId);
            return RunDailycup(dailycup,true);
        }

        public MessageCode RunDailycup()
        {
            DailycupInfoEntity dailycup = DailycupInfoMgr.GetByDate(DateTime.Today);
            return RunDailycup(dailycup);
        }

        /// <summary>
        /// 运行杯赛
        /// </summary>
        /// <param name="dailycup"></param>
        /// <param name="isForce">是否强制执行</param>
        /// <returns></returns>
        public MessageCode RunDailycup(DailycupInfoEntity dailycup,bool isForce=false)
        {
            if (0 == Interlocked.Exchange(ref _syncRunDailycup, 1))
            {
                try
                {
                    if (dailycup == null)
                    {
                        return MessageCode.DailycupNotExists;
                    }
                    if(dailycup.RunDate>DateTime.Today)
                        return MessageCode.DailycupNottimetoRun;
                    if (dailycup.Status == (int) EnumDailycupStatus.Opening)
                    {
                        dailycup.Status = (int) EnumDailycupStatus.Close;
                        dailycup.UpdateTime = DateTime.Now;
                        DailycupInfoMgr.Update(dailycup);
                    }
                    else 
                    {
                        if(dailycup.Status==(int)EnumDailycupStatus.End)
                            return MessageCode.DailycupStatusNotOpen;
                        else if(dailycup.Status==(int)EnumDailycupStatus.Close)
                        {
                            if (dailycup.UpdateTime.AddHours(1) > DateTime.Now)
                            {
                                return MessageCode.DailycupStatusNotOpen;
                            }
                        }
                        else
                        {
                            return MessageCode.DailycupStatusNotOpen;
                        }
                    }
                    //查找所有这轮报名
                    var competitorList = DailycupCompetitorsMgr.GetForFight(dailycup.Idx);
                    var dailycupProcess = new DailycupProcess(competitorList, dailycup);
                    return dailycupProcess.StartDailycup();
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("DailycupAssociation RunDailycup", ex);
                    return MessageCode.Exception;
                }
                finally
                {
                    Interlocked.Exchange(ref _syncRunDailycup, 0);
                }
            }
            else
            {
                return MessageCode.SystemBusy;
            }
        }
        #endregion

        #region CreateDailycup

        private int _syncCreateDailycup = 0;
        public MessageCode CreateDailycup()
        {
            if (0 == Interlocked.Exchange(ref _syncCreateDailycup, 1))
            {
                try
                {
                    if (DailycupInfoMgr.Create())
                        return MessageCode.Success;
                    else
                    {
                        return MessageCode.NbUpdateFail;
                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("CreateDailycup", ex);
                    return MessageCode.Exception;
                }
                finally
                {
                    Interlocked.Exchange(ref _syncCreateDailycup, 0);
                }
            }
            else
            {
                return MessageCode.SystemBusy;
            }
        }
        #endregion

        #region OpenGamble
        public MessageCode OpenGamble(int dailycupId)
        {
            var dailycup = DailycupInfoMgr.GetById(dailycupId);
            return OpenGamble(dailycup);
        }

        public MessageCode OpenGamble()
        {
            var dailycup = DailycupInfoMgr.GetByDate(DateTime.Today);
            return OpenGamble(dailycup);
        }

        private int _syncOpenGamble = 0;
        public MessageCode OpenGamble(DailycupInfoEntity dailycup)
        {
            if (0 == Interlocked.Exchange(ref _syncOpenGamble, 1))
            {
                //不可重入
                try
                {
                    if (dailycup == null)
                    {
                        return MessageCode.DailycupNotExists;
                    }
                    if (dailycup.Status != (int) EnumDailycupStatus.End) //只能计算已打完的杯赛
                    {
                        return MessageCode.DailycupStatusNotEnd;
                    }

                    var beginRound = dailycup.OpenGambleRound; //获取已开奖的轮数
                    var endRound = DailycupCore.Instance.NowGambleOpenRound(dailycup.Round, DateTime.Now);
                    if (beginRound == endRound) //该杯赛已全部开过奖了
                    {
                        return MessageCode.DailycupGambleOpened;
                    }
                    if (beginRound == 0)
                        beginRound = DailycupCore.Instance.BeginRound(dailycup.Round);
                    else
                    {
                        beginRound = beginRound + 1;
                    }
                    for (int i = beginRound; i <= endRound; i++)
                    {
                        //SystemlogMgr.Info("DailycupGambleOpen", string.Format("杯赛{0}的第{1}轮开奖启动", dailycup.Idx, i));
                        OpenGamble(dailycup.Idx, i);
                        dailycup.OpenGambleRound = i;
                        dailycup.UpdateTime = DateTime.Now;
                        DailycupInfoMgr.Update(dailycup);
                        //SystemlogMgr.Info("DailycupGambleOpen", string.Format("杯赛{0}的第{1}轮开奖完成", dailycup.Idx, i));
                    }
                    return MessageCode.Success;
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("Dailycup OpenGamble", ex);
                    return MessageCode.Exception;
                }
                finally
                {
                    //Release the lock
                    Interlocked.Exchange(ref _syncOpenGamble, 0);
                }
            }
            else
            {
                return MessageCode.SystemBusy;
            }
        }

        /// <summary>
        /// 开奖
        /// </summary>
        /// <param name="dailycupId">The dailycup id.</param>
        /// <param name="round">比赛轮次</param>
        public static void OpenGamble(int dailycupId, int round)
        {
            List<DailycupMatchEntity> listMatch = DailycupMatchMgr.GetMatchByRound(dailycupId, round,round);
            foreach (var match in listMatch)
            {
                OpenGamble(match);
            }
        }

        /// <summary>
        /// 针对某场比赛开奖
        /// </summary>
        /// <param name="match">The match.</param>
        static void OpenGamble(DailycupMatchEntity match)
        {
            List<DailycupGambleEntity> list = DailycupGambleMgr.GetGambleByMatchId(match.Idx);
            foreach (var gamble in list)
            {
                if (gamble.Status == (int)EnumGambleStatus.Init)
                    OpenGamble(match, gamble);
            }
        }

        /// <summary>
        /// 针对某场比赛押注的某个经理开奖，如果博彩中了，就给奖励，否则给予惩罚
        /// </summary>
        /// <param name="match">The match.</param>
        /// <param name="gamble">The gamble.</param>
        private static void OpenGamble(DailycupMatchEntity match, DailycupGambleEntity gamble)
        {
            int matchResult;
            if (match.HomeScore > match.AwayScore)
            {
                matchResult = 1;
            }
            else if (match.HomeScore == match.AwayScore)
            {
                matchResult = 1;//平局算主队赢
            }
            else
            {
                matchResult = 2;
            }

            var resultPoint = 0;

            try
            {
                int newLevel = 0;
                if (matchResult == gamble.GambleResult) //押中
                {
                    gamble.Status = (int)EnumGambleStatus.Success;
                    resultPoint = gamble.GamblePoint * 2;
                    //欧洲杯狂欢
                    ActivityExThread.Instance.EuropeCarnival(2, ref resultPoint);
                }
                else //押失败
                {
                    gamble.Status = (int)EnumGambleStatus.Fail;
                }
                gamble.ResultPoint = resultPoint;

                //猜中发奖励
                if (resultPoint > 0)
                {
                    var mail = new MailBuilder(gamble);
                    mail.Save();
                }

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("OpenGamble", string.Format("每日杯赛开奖error{2},matchId:{0},gambleId:{1}", match.Idx, gamble.Idx, ex.Message), ex.StackTrace);
            }
        }
        #endregion

        #region SendPrize

        private int _syncSendPrize = 0;
        public MessageCode SendPrize()
        {
            try
            {
                var dailycup = DailycupInfoMgr.GetByDate(DateTime.Today);
                return SendPrize(dailycup);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Dailycup SendPrize", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode SendPrize(int dailycupId)
        {
            try
            {
                var dailycup = DailycupInfoMgr.GetById(dailycupId);
                return SendPrize(dailycup);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Dailycup SendPrize", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode SendPrize(DailycupInfoEntity dailycup)
        {
            if (0 == Interlocked.Exchange(ref _syncSendPrize, 1))
            {
                try
                {
                    if (dailycup == null)
                    {
                        return MessageCode.DailycupNotExists;
                    }
                    if (dailycup.Status != (int) EnumDailycupStatus.End) //只能计算已打完的杯赛
                    {
                        return MessageCode.DailycupStatusNotEnd;
                    }
                    var beginRound = dailycup.Round; //获取已开奖的轮数
                    var endRound = DailycupCore.Instance.NowGambleOpenRound(dailycup.Round, DateTime.Now);
                    if (beginRound > endRound)
                    {
                        return MessageCode.DailycupNotimetoSendPrize;
                    }
                    dailycup.Status = (int) EnumDailycupStatus.StartSend;
                    dailycup.UpdateTime = DateTime.Now;
                    DailycupInfoMgr.Update(dailycup);

                    var competitorsList = DailycupCompetitorsMgr.GetByDailycupId(dailycup.Idx);
                    if (competitorsList == null)
                        return MessageCode.DailycupNoCompetitors;

                    List<MailBuilder> mailList = new List<MailBuilder>(competitorsList.Count);
                    List<NbManagerhonorEntity> honorList = new List<NbManagerhonorEntity>(2);
                    foreach (var entity in competitorsList)
                    {
                        mailList.Add(CalPrize(entity));
                        ManagerCore.Instance.DeleteCache(entity.ManagerId);
                        if (entity.Rank ==-1)
                        {
                            honorList.Add(new NbManagerhonorEntity(){ManagerId = entity.ManagerId,MatchType = (int)EnumMatchType.Dailycup,PeriodId = entity.DailyCupId,Rank = 1,SubType = 0,Rowtime = DateTime.Now});
                        }
                        else if (entity.Rank == 1)
                        {
                            honorList.Add(new NbManagerhonorEntity() { ManagerId = entity.ManagerId, MatchType = (int)EnumMatchType.Dailycup, PeriodId = entity.DailyCupId, Rank = 2, SubType = 0, Rowtime = DateTime.Now });
                        }
                    }

                    DailycupSqlHelper.SaveCompetitorsPrize(competitorsList);

                    MailCore.SaveMailBulk(mailList);

                    dailycup.Status = (int) EnumDailycupStatus.Finish;
                    dailycup.UpdateTime = DateTime.Now;
                    DailycupInfoMgr.Update(dailycup);
                    foreach (var entity in honorList)
                    {
                        NbManagerhonorMgr.Add(entity.ManagerId, entity.MatchType, entity.SubType, entity.PeriodId,
                                              entity.Rank);
                    }
                    return MessageCode.Success;

                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("Dailycup SendPrize", ex);
                    return MessageCode.Exception;
                }
                finally
                {
                    Interlocked.Exchange(ref _syncSendPrize, 0);
                }
            }
            else
            {
                return MessageCode.SystemBusy;
            }
        }

        MailBuilder CalPrize(DailycupCompetitorsEntity competitors)
        {
            //S1(报名奖励物品)|S1C(报名奖励物品数量)|W(获胜场次)|S2(获胜奖励物品)|S2C(获胜奖励物品数量)|C1(获胜金币)|R(名次)|O(冠军杯积分)|S3(排名阅历)|C2(排名金币)|AT(货币类型)
            //报名奖励物品
            string[] attendPrizeItem = _dailycupAttendPrizeItem.Split(',');
            string prizeItem1Code = attendPrizeItem[0];
            int prizeItem1Count = Convert.ToInt32(attendPrizeItem[1]);
            string prizeItem1Str = _dailycupAttendPrizeItem;

            //胜场奖励物品
            string[] winPrizeItem = _dailycupWinPrizeItem.Split(',');
            string prizeItem2Code = winPrizeItem[0];
            int prizeItem2Count = Convert.ToInt32(winPrizeItem[1]) * competitors.WinCount;
            string prizeItem2Str = "";
            if (prizeItem2Count > 0)
                prizeItem2Str = prizeItem2Code + "," + prizeItem2Count;

            //获胜金币
            int coin1 = _dailycupWinPrizeCoin * competitors.WinCount;

            int score = 0;
            string prizeItem3Str = "";
            int coin2 = 0;
            var prize = CacheFactory.DailycupCache.GetEntity(competitors.Rank);
            if (prize != null)
            {
                score = prize.WorldScore;
                prizeItem3Str = prize.PrizeItems;
                coin2 = prize.Coin;
            }
            competitors.PrizeItems = prizeItem1Str + "|" + prizeItem2Str + "|" + prizeItem3Str;
            competitors.PrizeItems = competitors.PrizeItems.TrimEnd('|');
            competitors.PrizeCoin = coin1 + coin2;
            competitors.PrizeScore = score;

            MailBuilder mailBuilder = null;
            if (prize != null)
                mailBuilder = new MailBuilder(competitors, prizeItem1Code, prizeItem1Count, prizeItem2Code, prizeItem2Count, prizeItem3Str, coin1, coin2);
            else
            {
                mailBuilder = new MailBuilder(competitors, prizeItem1Code, prizeItem1Count, prizeItem2Code, prizeItem2Count, coin1);
            }
            //记录成就相关数据
            AchievementTaskCore.Instance.UpdateDailyCupRank(competitors.ManagerId, competitors.Rank);
            TaskHandler.Instance.DailyCupRank(competitors.ManagerId);

            return mailBuilder;
        }
        #endregion
        #endregion
    }
}
