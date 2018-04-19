using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Robot;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Crowd;
using Games.NBall.Entity.Response.Crowd;
using Timer = System.Timers.Timer;

namespace Games.NBall.Core.CrossCrowd
{
    public partial class CrossCrowdThread
    {
        public delegate void ClearFightDicDelegate();

        private NBThreadPool _nbThreadPool;
        private System.Timers.Timer _clearTimer;
        private Timer _timer = null;
        private const double ClearFightdicTime = 60000;//清空上一轮比赛池时间，毫秒
        private Dictionary<int, Dictionary<int, int[]>> _groupTemplates;
        private CrosscrowdInfoEntity _crowdInfo;
        private bool _needClearFightDic = false;
        private object _competitorLock = new object();
        private int _crowdResurrectionCd;
        private int _crowdCd;
        private int _crowdMaxPoint;
        private int _crowdMaxLegendCount;
        private int _domainId;
        private CrossCrowdProcess _crowdProcess;

        private static bool isKillDouble = false;

        /// <summary>
        /// 配对服务状态
        /// </summary>
        private EnumLadderStatus _status;
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime _startTime;

        /// <summary>
        /// 本轮比赛参数选手
        /// </summary>
        /// <value>The competitor dic.</value>
        public ConcurrentDictionary<Guid, CrosscrowdManagerEntity> CompetitorDic { get; private set; }

        /// <summary>
        /// 经理-比赛对应字典
        /// </summary>
        public Dictionary<Guid, CrowdHeartEntity> ManagerFightDic { get; private set; }
        /// <summary>
        /// 所有参与经理列表
        /// </summary>
        public ConcurrentDictionary<Guid, int> CrowdManagerDic { get; private set; }

        private List<ArenaCondition> _conditions;

        #region .ctor
        public CrossCrowdThread(int domainId)
        {
            _domainId = domainId;
            Initialize();
            _timer.Start();
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Close();
                _timer.Dispose();
                _timer = null;
            }
        }

        #endregion

        ~CrossCrowdThread()
        {
            Dispose();
        }
        #endregion

        #region Facade

        public CrosscrowdInfoEntity GetCurrent()
        {
            if (_crowdInfo == null || _status == EnumLadderStatus.End)
                return null;
            DateTime curTime = DateTime.Now;
            if (curTime >= _crowdInfo.EndTime)
                return null;
            if (_crowdInfo.StartTime <= curTime)
            {
                if (_crowdInfo.EndTime > curTime)
                {
                    _crowdInfo.Status = 1;
                }
                else
                {
                    return null;
                }
            }
            return _crowdInfo;
        }

        public bool IsManagerBusy(Guid managerId)
        {
            if (ManagerFightDic != null && ManagerFightDic.ContainsKey(managerId))
                return true;
            else
            {
                return false;
            }
        }

        public MessageCodeResponse Attend(string siteId,Guid managerId)
        {
            
            var crowd = GetCurrent();
            if (crowd == null || crowd.Status != 1)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.CrowdNoData);
            DateTime curTime = DateTime.Now;
            if (curTime < crowd.StartTime)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.CrowdNotStart);
            }
            if (IsManagerBusy(managerId))
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.CrowdBusy);
            }
            if (!CompetitorDic.ContainsKey(managerId))
            {
                var response = CrossCrowdCore.Instance.GetManagerInfo(siteId, managerId, crowd.Idx, curTime);
                if (response.Code != (int)MessageCode.Success)
                    return ResponseHelper.Create<MessageCodeResponse>(response.Code);
                lock (_competitorLock)
                {
                    if (CompetitorDic.Count == 0)
                        _startTime = DateTime.Now;
                    CompetitorDic.TryAdd(managerId, response.Data);
                    CrowdManagerDic.TryAdd(managerId, 1);
                    _crowdInfo.PlayerCount = CrowdManagerDic.Count;
                }
                return ResponseHelper.CreateSuccess<MessageCodeResponse>();
            }
            else
            {
                return ResponseHelper.CreateSuccess<MessageCodeResponse>();
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse Leave(Guid managerId)
        {
            var crowd = GetCurrent();
            if (crowd == null || crowd.Status != 1)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.CrowdNoData);
            DateTime curTime = DateTime.Now;
            if (curTime < crowd.StartTime)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.CrowdNotStart);
            }
            if (!IsManagerBusy(managerId))
            {
                if (CompetitorDic.ContainsKey(managerId))
                {
                    lock (_competitorLock)
                    {
                        CrosscrowdManagerEntity entity = null;
                        CompetitorDic.TryRemove(managerId,out entity);
                    }
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
                }
                else
                {
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
                }
            }
            else
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.CrowdBusy);
            }
        }

        /// <summary>
        /// 状态轮询.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public CrowdHeartResponse Heart(Guid managerId)
        {
            if (CompetitorDic.ContainsKey(managerId))
            {
                ////锁住
                //lock (_competitorLock)
                //{
                //    if (CompetitorDic.ContainsKey(managerId))
                //    {
                //        CompetitorDic[managerId].UpdateTime = DateTime.Now;
                //    }
                //}
                return ResponseHelper.CreateSuccess<CrowdHeartResponse>();
            }
            else if (ManagerFightDic != null && ManagerFightDic.ContainsKey(managerId))
            {
                var heartEntity = ManagerFightDic[managerId];
                if (heartEntity == null)
                {
                    if (_status == EnumLadderStatus.Grouping)
                    {
                        return ResponseHelper.Create<CrowdHeartResponse>(MessageCode.Success);
                    }
                    else
                    {
                        return ResponseHelper.Create<CrowdHeartResponse>(MessageCode.Success);
                    }
                }
                else
                {
                    var response = ResponseHelper.Create<CrowdHeartResponse>(MessageCode.Success);
                    response.Data = heartEntity;
                    return response;
                }
            }
            else
            {
                var heartEntity = MemcachedFactory.CrowdHeartClient.Get<CrowdHeartEntity>(managerId);
                if (heartEntity == null)
                {
                    return ResponseHelper.Create<CrowdHeartResponse>(MessageCode.Success);
                }
                else
                {
                    var response = ResponseHelper.Create<CrowdHeartResponse>(MessageCode.Success);
                    response.Data = heartEntity;
                    return response;
                }
            }
        }
        #endregion

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

        public MessageCode StartJob(string timeConfig)
        {
            try
            {
                return Start(timeConfig);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("StartJob", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode StartJob(DateTime startTime, DateTime endTime)
        {
            try
            {
                return Start(startTime,endTime);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("StartJob", ex);
                return MessageCode.Exception;
            }
        }

        public string AdminStart(DateTime startTime,DateTime endTime)
        {
            try
            {
                var code = Start(startTime, endTime);
                return code.ToString();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("AdminStart", ex);
                return "cause exception:" + ex.Message;
            }
        }

        public static MessageCode SendPrize()
        {
            var prizeList = CrosscrowdInfoMgr.C_CrossCrowdNotSendPrize();
            foreach (var item in prizeList)
            {
                AdminSendPrize(item.Idx);
            }
            return MessageCode.Success;
        }

        public static string AdminSendPrize(int crowdId)
        {
            try
            {
                var crowd = CrosscrowdInfoMgr.GetById(crowdId);
                if (crowd == null)
                    return "crowd is null";
                SendMaxKillerPrize(crowd);
                if (crowd.IsSendKillPrize && crowd.IsSendRankPrize)
                    return "prize has sended";
                DateTime curTime = DateTime.Now;
                var crowdMaxPoint = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.CrowdMaxPoint);
                int maxLegendCount=CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.CrowdMaxLegendCount);
                if (curTime >= crowd.EndTime)
                {
                    ISKillDouble();
                    SendKillPrize(crowd, crowdMaxPoint, maxLegendCount);
                    SendRankPrize(crowd);
                    crowd.Status = (int)EnumLadderStatus.End;
                    CrosscrowdInfoMgr.Update(crowd);
                    return "success";
                }
                else
                {
                    return "the crowd not ending";
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("AdminSendPrize",ex);
                return "cause exception:"+ex.Message;
            }
        }
        #endregion

        #region encapsulation

        #region Initialize
        private void Initialize()
        {
            CrowdManagerDic = new ConcurrentDictionary<Guid, int>();
            CompetitorDic = new ConcurrentDictionary<Guid, CrosscrowdManagerEntity>();
            ManagerFightDic = new Dictionary<Guid, CrowdHeartEntity>();
            _crowdResurrectionCd = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.CrowdResurrectionCd);
            _crowdCd = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.CrowdCd);
            _crowdMaxPoint = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.CrowdMaxPoint);
            _crowdMaxLegendCount = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.CrowdMaxLegendCount);
            _startTime = DateTime.Now;
            //初始化分组模板
            InitGroupTemplate();

            #region Init Condition dic
            string condition = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.CrowdCondition);
            _conditions = new List<ArenaCondition>();
            if (!string.IsNullOrEmpty(condition))
            {
                string[] tempConditions = condition.Split('|');
                foreach (var s in tempConditions)
                {
                    string[] temp = s.Split(',');
                    _conditions.Add(new ArenaCondition(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1])));
                }
            }
            #endregion
            _nbThreadPool = new NBThreadPool(5);
            _clearTimer = new Timer { Interval = ClearFightdicTime };
            _clearTimer.Elapsed += new ElapsedEventHandler(ClearFightDic);

            _timer = new Timer { Interval = 5000 };
            _timer.Elapsed += new ElapsedEventHandler(CheckStatusJob);
            var crowd = CrosscrowdInfoMgr.GetCurrent(_domainId);
            DateTime curTime = DateTime.Now;
            if (crowd != null && crowd.EndTime > curTime)
            {
                _crowdInfo = crowd;
                _status = EnumLadderStatus.Running;
                RobotCore.Instance.RunHook(EnumMatchType.CrossCrowd, _crowdInfo.EndTime);
            }
            else
            {
                _status = EnumLadderStatus.End;
            }
        }
        #endregion

        #region ClearFightDic
        void ClearFightDic(object sender, ElapsedEventArgs e)
        {
            doClearFightDic();
        }

        void doClearFightDic()
        {
            _clearTimer.Stop();
            if (!_needClearFightDic)
                return;
            try
            {
                ManagerFightDic = new Dictionary<Guid, CrowdHeartEntity>();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowdThread-ClearFightdic", ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region CheckStatus

        bool CheckProcessEnd()
        {
            if (_crowdProcess == null)
                return true;
            else
            {
                if (_crowdProcess.Status == EnumLadderStatus.End)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 检查是否达到进入配对条件.
        /// </summary>
        void CheckStatus()
        {
            if (_status != EnumLadderStatus.Running)//前一轮还在分组中，请勿打扰
                return;
            DateTime curTime = DateTime.Now;
            if (curTime >= _crowdInfo.EndTime)
            {
                if (CheckProcessEnd())
                {
                    _status = EnumLadderStatus.End;
                    ISKillDouble();
                    SendKillPrize(_crowdInfo,_crowdMaxPoint,_crowdMaxLegendCount);
                    SendRankPrize(_crowdInfo);
                    SendMaxKillerPrize(_crowdInfo);
                    CrowdEnd();
                }
                return;
            }
            double timeInterval = curTime.Subtract(_startTime).TotalSeconds;
            int playerNumber = CompetitorDic.Count;
            //SystemlogMgr.Info("CrowdThread-CheckStatus", string.Format("当前配对池状态：时间{0}s,人数{1}", timeInterval, playerNumber));
            foreach (var condition in _conditions)
            {
                if (timeInterval >= condition.WaitTime && playerNumber >= condition.PlayerNumber)
                {
                    _clearTimer.Stop();
                    RunPair();
                    break;
                }
            }
        }

        private static void ISKillDouble()
        {
            var crowdKillDoubleTime = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.CrowdKillDoubleTime);
            if (crowdKillDoubleTime.Length > 0)
            {
                var times = crowdKillDoubleTime.Split('|');
                var startTime = ConvertHelper.StringToDateTime(times[0]);
                var endTime = ConvertHelper.StringToDateTime(times[1]);
                isKillDouble = DateTime.Now > startTime && DateTime.Now < endTime;
            }
        }

        #endregion

        #region Start

        private MessageCode Start(DateTime startTime,DateTime endTime)
        {
            var crowd = CrosscrowdInfoMgr.GetCurrent(_domainId);
            DateTime curTime = DateTime.Now;
            if (crowd != null && crowd.EndTime > curTime)
            {
                return MessageCode.CrowdNotEnding;
            }
            if(startTime>=endTime || endTime<curTime)
                return MessageCode.NbParameterError;
            crowd = new CrosscrowdInfoEntity();
            crowd.DomainId = _domainId;
            crowd.StartTime = startTime;
            crowd.EndTime = endTime;
            crowd.RowTime = DateTime.Now;
            if (CrosscrowdInfoMgr.Insert(crowd))
            {
                CrowdManagerDic = new ConcurrentDictionary<Guid, int>();
                _crowdInfo = crowd;
                _status = EnumLadderStatus.Running;
                //CrossChatHelper.SendBannerCrowdOpen(_domainId);
                RobotCore.Instance.RunHook(EnumMatchType.CrossCrowd, endTime);
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbUpdateFail;
            }
        }

        MessageCode Start(string timeConfig)
        {
            var ss = timeConfig.Split(',');
            var startSecond = Convert.ToInt32(ss[0]);
            var endSecond = Convert.ToInt32(ss[1]);
            DateTime curTime = DateTime.Now;

            var startTime = curTime.Date.AddSeconds(startSecond);
            var endTime = curTime.Date.AddSeconds(endSecond);
            return Start(startTime, endTime);
        }
        #endregion

        private const int _maxKillerPrizeItem = 310149;
        static void SendMaxKillerPrize(CrosscrowdInfoEntity crowdInfo)
        {
            var entity = CrosscrowdMaxkillerrecordMgr.GetMaxKiller(crowdInfo.Idx);
            if (entity != null && entity.Status==0)
            {
                try
                {
                    var mail = new MailBuilder(entity.ManagerId, EnumMailType.CrowdMaxKiller);
                    mail.AddAttachment(1, _maxKillerPrizeItem, false, 0);
                    mail.Save(entity.SiteId);
                    entity.Status = 1;
                    entity.PrizeItems = _maxKillerPrizeItem.ToString();
                    CrosscrowdMaxkillerrecordMgr.Update(entity);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("CrossCrowd-SendMaxKillerPrize", ex);
                }
            }
        }

        #region SendKillPrize
        static void SendKillPrize(CrosscrowdInfoEntity crowdInfo, int maxPoint, int maxLegendCount)
        {
            if (crowdInfo.IsSendKillPrize)
                return;
            try
            {
                var list = CrosscrowdInfoMgr.GetForSendKillPrize(crowdInfo.Idx);
                if (list != null)
                {
                    foreach (var entity in list)
                    {
                        doSendKillPrize(crowdInfo, entity, maxPoint, maxLegendCount);
                    }
                }
                crowdInfo.IsSendKillPrize = true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowd-SendKillPrize", ex);
            }
        }

        static void doSendKillPrize(CrosscrowdInfoEntity crowd, CrosscrowdSendKillPrizeEntity entity, int maxPoint, int maxLegendCount)
        {
            try
            {
                if (entity.Status != 0)
                    return;
                if (entity.HomeMorale <= 0)
                    doSendKillPrize(crowd, entity.Idx, entity.AwayId, entity.HomeName, entity.AwaySiteId, maxPoint, maxLegendCount);
                if (entity.AwayMorale <= 0)
                    doSendKillPrize(crowd, entity.Idx, entity.HomeId, entity.AwayName, entity.HomeSiteId, maxPoint, maxLegendCount);
                entity.Status = 1;
                CrosscrowdMatchMgr.SaveKillPrizeStatus(entity.Idx, entity.Status);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowd-doSendKillPrize", ex);
            }
        }

        static void doSendKillPrize(CrosscrowdInfoEntity crowd, Guid matchId, Guid managerId, string awayName, string siteId, int maxPoint, int maxLegendCount)
        {
            var mail = new MailBuilder(managerId, EnumMailType.CrossCrowdKill, awayName);
            string prizeItemString = "";
            BuildPrizeMail(crowd, mail, EnumCrowdPrizeCategory.CrossKill,1,maxPoint,maxLegendCount,ref prizeItemString);
            if (string.IsNullOrEmpty(prizeItemString))
                return;
            mail.Save(siteId);
            SavePrizeRecord(managerId, EnumCrowdPrizeCategory.CrossKill, matchId.ToString(), prizeItemString, siteId);
        }
        #endregion

        #region SendRankPrize
        static void SendRankPrize(CrosscrowdInfoEntity crowdInfo)
        {
            if (crowdInfo.IsSendRankPrize)
                return;
            try
            {
                var maxKiller = CrosscrowdInfoMgr.GetMaxKiller(crowdInfo.Idx);
                var list = CrosscrowdInfoMgr.GetForSendRankPrize(crowdInfo.Idx);
                bool isSend = true;
                if (list != null)
                {
                    bool isMaxKiller = false;
                    foreach (var entity in list)
                    {
                        isMaxKiller = false;
                        if (doSendRankPrize(crowdInfo, entity, -1, -1) != MessageCode.Success)
                        {
                            SystemlogMgr.Error("跨服群雄发奖", "发奖失败，idx=" + entity.Idx);
                            isSend = false;
                        }
                    }
                }
                crowdInfo.IsSendRankPrize = isSend;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowd-SendRankPrize", ex);
            }
        }


        static MessageCode doSendRankPrize(CrosscrowdInfoEntity crowd, CrosscrowdSendRankPrizeEntity entity,int maxPoint,int maxLegendCount)
        {
            try
            {
                if (entity.Status != 0)
                    return MessageCode.Success;
                if (entity.Score <= 0)
                    return MessageCode.Success;
                string prizeItemString = "";
                var mail = new MailBuilder(entity.ManagerId, EnumMailType.CrossCrowdRank, entity.Rank);
                MessageCode mess = BuildPrizeMail(crowd, mail, EnumCrowdPrizeCategory.CrossRank, entity.Rank,maxPoint,maxLegendCount, ref prizeItemString);
                if (mess != MessageCode.Success)
                    return mess;
                entity.Status = 1;
                CrosscrowdInfoMgr.SaveRankPrizeStatus(entity.Idx, entity.Status);
                if (!mail.Save(entity.SiteId))
                    return MessageCode.NbParameterError;
                SavePrizeRecord(entity.ManagerId, EnumCrowdPrizeCategory.CrossRank, "history:" + entity.Idx,
                        prizeItemString, entity.SiteId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowd-doSendRankPrize", ex);
                return MessageCode.NbParameterError;
            }
            return MessageCode.Success;
        }

        #endregion

        #region BuildPrizeMail
        static MessageCode BuildPrizeMail(CrosscrowdInfoEntity crowd, MailBuilder mail, EnumCrowdPrizeCategory category, int categorySub,int maxPoint,int maxLegendCount, ref string prizeItemString)
        {
            try
            {//11 天梯奖励     12击杀奖励
                var prizes = CacheFactory.CrowdCache.GetRankPrizes(category, categorySub);
                if (prizes != null)
                {
                    //1	金币	
                    //2	卡库	卡库id
                    //3	物品	物品code
                    //4	非彩色球魂	
                    //5	点券
                    //7 绑定点卷
                    var itemCode = 0;
                    foreach (var entity in prizes)
                    {
                        if (!RandomHelper.CheckPercentage(entity.Rate))
                            continue;
                        switch (entity.Type)
                        {
                            case 1:
                                mail.AddAttachment(EnumCurrencyType.Coin, entity.Count);
                                prizeItemString += string.Format("1Coin:{0}|", entity.Count);
                                break;
                            case 2:
                                itemCode = CacheFactory.LotteryCache.LotteryByLib(entity.SubType);
                                if (itemCode > 0)
                                {
                                    mail.AddAttachment(entity.Count, itemCode, entity.IsBinding, entity.Strength);
                                    prizeItemString += string.Format("2Item-C:{0},I:{1},B:{2},S:{3}|", entity.Count,
                                        itemCode, entity.IsBinding, entity.Strength);
                                }
                                break;
                            case 3:
                                if (maxLegendCount != -1 && entity.SubType == 310165)
                                {
                                    if (crowd.PrizeLegendCount < maxLegendCount)
                                    {
                                        BuildPrizeMailItem(mail, entity, "3Item", ref prizeItemString);
                                        crowd.PrizeLegendCount++;
                                    }
                                }
                                else
                                {
                                    BuildPrizeMailItem(mail, entity, "3Item", ref prizeItemString);
                                }
                                break;
                            case 4:
                                itemCode = CacheFactory.ItemsdicCache.RandomBallsoulOthercolorForWCH();
                                if (itemCode > 0)
                                {
                                    mail.AddAttachment(entity.Count, itemCode, entity.IsBinding, entity.Strength);
                                    prizeItemString += string.Format("4Item-C:{0},I:{1},B:{2},S:{3}|", entity.Count,
                                        itemCode, entity.IsBinding, entity.Strength);
                                }
                                break;
                            case 5:
                                var totalPrizePoint = crowd.PrizePoint;
                                if (category == EnumCrowdPrizeCategory.CrossRank)
                                {
                                    int point = RandomHelper.GetInt32(entity.Min, entity.Max);
                                    if (point > 0)
                                    {
                                        mail.AddAttachment(EnumCurrencyType.Point, point);
                                        prizeItemString += string.Format("5Point:{0}|", point);
                                    }
                                }
                                else if (maxPoint == -1 || totalPrizePoint < maxPoint)
                                {
                                    int point = RandomHelper.GetInt32(entity.Min, entity.Max);
                                    totalPrizePoint += point;
                                    if (totalPrizePoint > maxPoint)
                                    {
                                        point = maxPoint - crowd.PrizePoint;
                                        totalPrizePoint = maxPoint;
                                    }
                                    if (maxPoint != -1)
                                    {
                                        crowd.PrizePoint = totalPrizePoint;
                                    }
                                    if (point > 0)
                                    {
                                        mail.AddAttachment(EnumCurrencyType.Point, point);
                                        prizeItemString += string.Format("5Point:{0}|", point);
                                    }
                                }
                                break;
                            case 6:
                                int prizeCount = 0;
                                if (RandomHelper.CheckPercentage(entity.Min))
                                {
                                    prizeCount = RandomHelper.GetInt32(21, 30);
                                }
                                else if (RandomHelper.CheckPercentage(entity.Max))
                                {
                                    prizeCount = RandomHelper.GetInt32(16, 20);
                                }
                                else
                                {
                                    prizeCount = RandomHelper.GetInt32(6, 15);
                                }
                                //var prizeCount = RandomHelper.GetInt32(entity.Min, entity.Max);
                                mail.AddAttachment(prizeCount, entity.SubType, entity.IsBinding, entity.Strength);
                                prizeItemString += string.Format("3Item-C:{0},I:{1},B:{2},S:{3}|", prizeCount,
                                    entity.SubType, entity.IsBinding, entity.Strength);
                                break;
                            case 7:
                                var totalBindPoint = crowd.PrizePoint;
                                if (category == EnumCrowdPrizeCategory.CrossRank)
                                {
                                    int point = RandomHelper.GetInt32(entity.Min, entity.Max);
                                    if (point > 0)
                                    {
                                        mail.AddAttachment(EnumCurrencyType.BindPoint, point);
                                        prizeItemString += string.Format("8Point:{0}|", point);
                                    }
                                }
                                else if (maxPoint == -1 || totalBindPoint < maxPoint)
                                {
                                    int point = RandomHelper.GetInt32(entity.Min, entity.Max);
                                    totalBindPoint += point;
                                    if (totalBindPoint > maxPoint)
                                    {
                                        point = maxPoint - crowd.PrizePoint;
                                        totalBindPoint = maxPoint;
                                    }
                                    if (maxPoint != -1)
                                    {
                                        crowd.PrizePoint = totalBindPoint;
                                    }
                                    if (point > 0)
                                    {
                                        mail.AddAttachment(EnumCurrencyType.BindPoint, point);
                                        prizeItemString += string.Format("8Point:{0}|", point);
                                    }
                                }
                                break;
                        }
                    }
                    if (category == EnumCrowdPrizeCategory.CrossKill)
                    {
                        if (isKillDouble)
                        {
                            mail.AddAttachment(1, 310154,false, 1);
                            prizeItemString += string.Format("3Item-C:{0},I:{1},B:{2},S:{3}|", 1,
                                310154, false, 1);
                        }
                    }
                    prizeItemString = prizeItemString.TrimEnd('|');
                }
            }
            catch (Exception ex)
            {
                return MessageCode.NbParameterError;
            }
            return MessageCode.Success;
        }

        static void BuildPrizeMailItem(MailBuilder mail,ConfigCrowdrankprizeEntity entity, string title, ref string prizeItemString)
        {
            mail.AddAttachment(entity.Count, entity.SubType, entity.IsBinding, entity.Strength);
            prizeItemString += string.Format("{4}-C:{0},I:{1},B:{2},S:{3}|", entity.Count, entity.SubType, entity.IsBinding, entity.Strength, title);
        }
        #endregion

        #region SavePrizeReccord()
        static bool SavePrizeRecord(Guid managerId, EnumCrowdPrizeCategory category,string source,string prizeItemString,string siteId)
        {
            CrosscrowdPrizerecordEntity record = new CrosscrowdPrizerecordEntity();
            record.Category = (int) category;
            record.ManagerId = managerId;
            record.PrizeItems = prizeItemString;
            record.RowTime = DateTime.Now;
            record.Source = source;
            record.SiteId = siteId;
            return CrosscrowdPrizerecordMgr.Insert(record);
        }
        #endregion

        #region CrowdEnd
        void CrowdEnd()
        {
            try
            {
                _crowdInfo.Status = (int)EnumLadderStatus.End;
                CrosscrowdInfoMgr.Update(_crowdInfo);
                _crowdInfo = null;
                CompetitorDic = new ConcurrentDictionary<Guid, CrosscrowdManagerEntity>();
                //CrossChatHelper.SendBannerCrowdEnd(_domainId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowd-CrowdEnd", ex);
            }
            finally
            {
                _status = EnumLadderStatus.End;
            }
        }
        #endregion

        #endregion
    }
}
