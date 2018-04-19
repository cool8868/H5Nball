using System;
using System.Collections.Generic;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Ladder;

namespace Games.NBall.Core.CrossLadder
{
    public partial class CrossLadderThread
    {
        #region Schedule
        public void CheckStatusJob(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            try
            {
                CheckStatus();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CheckStatusJob", ex);
            }
            finally
            {
                _timer.Start();
            }
        }

        /// <summary>
        /// 检查是否达到进入配对条件.
        /// </summary>
        void CheckStatus()
        {
            if (_status == EnumLadderStatus.Grouping)//前一轮天梯赛还在分组中，请勿打扰
                return;

            double timeInterval = DateTime.Now.Subtract(_startTime).TotalSeconds;
            int playerNumber = CompetitorDic.Count;

            //SystemlogMgr.Info("CrossLadderThread-CheckStatus", string.Format("当前天梯赛状态：时间{0}s,人数{1}", timeInterval, playerNumber));

            foreach (var condition in _conditions)
            {
                if (timeInterval >= condition.WaitTime && playerNumber >= condition.PlayerNumber)
                {
                    timer2.Stop();
                    RunLadder();
                    break;
                }
            }
        }

        public MessageCode ScoreToHonorJob()
        {
            var season = CacheFactory.CrossLadderCache.GetCurrentSeason();
            if (season == null)
            {
                return MessageCode.LadderNoSeason;
            }
            var curDate = DateTime.Today;
            if (season.Idx == 1 && curDate == season.Startdate)
                return MessageCode.LadderSeasonDonotNeedSend;
            int isNewSeason = 0;
            int curSeason = season.Idx;
            if (season.Startdate == curDate && season.Idx > 1)
            {
                isNewSeason = 1;
                curSeason = curSeason - 1;
            }
            CrossladderInfoMgr.ScoreToHonor(DateTime.Today, curSeason, isNewSeason,_domainId);
            SendDailyHonor();
            //SendDailyPrize(curDate.AddDays(-1),season.Status);
            if (isNewSeason == 1)
            {
                var curSeasonEntity = CacheFactory.CrossLadderCache.GetEntity(curSeason);
                SendSeasonPrize(curSeason, curSeasonEntity==null?0:curSeasonEntity.Status);
            }
            return MessageCode.Success;
        }

        void SendDailyHonor()
        {
            try
            {
                var list = CrossladderManagerMgr.GetDailyHonor();
                if (list != null)
                {
                    foreach (var entity in list)
                    {
                        LadderManagerMgr.AddDailyHonor(entity.ManagerId, entity.NewlyHonor, entity.NewlyLadderCoin,null, entity.SiteId);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SendDailyHonor", ex);
            }   
        }

        void SendSeasonPrize(int curSeason,int seasonStatus)
        {
            var managers = CrossladderManagerhistoryMgr.GetPrizeManager(curSeason,_domainId);
            if (managers != null)
            {
                foreach (var manager in managers)
                {
                    SendPrize(manager, EnumCrossLadderPrizeType.Season, EnumMailType.CrossLadderPrize, seasonStatus);
                    if (manager.Rank < 3)
                    {
                        NbManagerhonorMgr.Add(manager.ManagerId, (int)EnumMatchType.CrossLadder, 0, manager.Season,
                                                manager.Rank,null,manager.SiteId);
                    }
                }
            }
        }

        public static void ReSendSeasonPrize(int curSeason,int domainId)
        {
            var curSeasonEntity = CacheFactory.CrossLadderCache.GetEntity(curSeason);
            var managers = CrossladderManagerhistoryMgr.GetPrizeManager(curSeason, domainId);
            if (managers != null)
            {
                foreach (var manager in managers)
                {
                    SendPrize(manager, EnumCrossLadderPrizeType.Season, EnumMailType.CrossLadderPrize, curSeasonEntity == null ? 0 : curSeasonEntity.Status);
                }
            }
        }

        void SendDailyPrize(DateTime date, int seasonStatus)
        {
            return;
            var managers = CrossladderManagerdailyhistoryMgr.GetDailyPrizeManager(date,_domainId);
            if (managers != null)
            {
                foreach (var manager in managers)
                {
                    SendPrize(manager, EnumCrossLadderPrizeType.Daily, EnumMailType.CrossLadderDailyPrize,seasonStatus);
                }
            }
        }
        
        public static void ReSendDailyPrize(DateTime date, int curSeason, int domainId)
        {
            return;
            var curSeasonEntity = CacheFactory.CrossLadderCache.GetEntity(curSeason);
            var managers = CrossladderManagerdailyhistoryMgr.GetDailyPrizeManager(date, domainId);
            if (managers != null)
            {
                foreach (var manager in managers)
                {
                    SendPrize(manager, EnumCrossLadderPrizeType.Daily, EnumMailType.CrossLadderDailyPrize, curSeasonEntity == null ? 0 : curSeasonEntity.Status);
                }
            }
        }


        public static void SendPrize(CrossladderManagerdailyhistoryEntity manager, EnumCrossLadderPrizeType crossLadderPrizeType, EnumMailType mailType, int seasonStatus)
        {
            if (string.IsNullOrEmpty(manager.PrizeItems))
            {
                manager.PrizeItems = "";
                MailBuilder mail = null;

                int packId = CacheFactory.CrossLadderCache.GetRankPrize(crossLadderPrizeType, manager.Rank,seasonStatus);
                if (packId <= 0)
                {
                    SystemlogMgr.Info("CrossLadderSendPrize", "no packid for rank:" + manager.Rank);
                    return;
                }
                mail = new MailBuilder(mailType, manager.ManagerId, manager.Season, manager.Rank, manager.RecordDate);
                var code = MallCore.Instance.BuildPackMail(packId, ref mail);
                if (code != MessageCode.Success)
                {
                    SystemlogMgr.Info("CrossLadderSendPrize", "build pack fail rank:" + manager.Rank + ",packId:" + packId);
                    return;
                }
                manager.PrizeItems = "pack:" + packId;
                manager.UpdateTime = DateTime.Now;
                try
                {
                    CrossladderManagerdailyhistoryMgr.SaveDailyPrize(manager.Idx, manager.PrizeItems);
                    mail.Save(manager.SiteId);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("CrossLadderSendPrize", ex);
                }
            }
        }

        public static void SendPrize(CrossladderManagerhistoryEntity manager, EnumCrossLadderPrizeType crossLadderPrizeType, EnumMailType mailType, int seasonStatus)
        {
            if (string.IsNullOrEmpty(manager.PrizeItems))
            {
                manager.PrizeItems = "";
                MailBuilder mail = null;

                int packId = CacheFactory.CrossLadderCache.GetRankPrize(crossLadderPrizeType, manager.Rank,seasonStatus);
                if (packId <= 0)
                {
                    SystemlogMgr.Info("CrossLadderSendPrize", "no packid for rank:" + manager.Rank);
                    return;
                }
                mail = new MailBuilder(mailType, manager.ManagerId,manager.Season,manager.Rank,manager.RecordDate);
                var code = MallCore.Instance.BuildPackMail(packId, ref mail);
                if (code != MessageCode.Success)
                {
                    SystemlogMgr.Info("CrossLadderSendPrize", "build pack fail rank:" + manager.Rank + ",packId:" + packId);
                    return;
                }
                manager.PrizeItems = "pack:" + packId;
                manager.UpdateTime = DateTime.Now;
                try
                {
                    CrossladderManagerhistoryMgr.SavePrize(manager.Idx,manager.PrizeItems);
                    mail.Save(manager.SiteId);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("CrossLadderSendPrize", ex);
                }
            }
        }

        public MessageCode GetMatchMarqueeJob()
        {
            try
            {
                _matchMarqueeResponse = ResponseHelper.CreateSuccess<LadderMatchMarqueeResponse>();
                _matchMarqueeResponse.Data = new LadderMatchMarqueeData();
                _matchMarqueeResponse.Data.Matchs = CrossladderMatchMgr.GetMatchTop10(_domainId);
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetMatchMarqueeJob", ex);
                return MessageCode.Exception;
            }
        }
        #endregion
    }
}
