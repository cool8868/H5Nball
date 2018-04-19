using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
//using Games.NBall.Core.Active;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Rank;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response.Ladder;
using Games.NBall.ServiceEngine;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Ladder
{
    public class LadderCore
    {
        //competitor锁
        private object _competitorLock = new object();
        //总玩家数量
        private int _playerNum;

        private readonly int _ladderRegisterScore;
        private readonly int _ladderExchangeShowDay;

        #region Public Fields

        /// <summary>
        /// 本轮天梯赛开始时间
        /// </summary>
        public DateTime StartTime;
        /// <summary>
        /// 天梯赛服务状态
        /// 控制同一时间只有一场天梯赛在分组状态
        /// 如前一场天梯赛分组未完成，则当前天梯赛不会进入比赛队列
        /// </summary>
        public EnumLadderStatus Status;

        /// <summary>
        /// 本轮天梯赛参数选手,需要加读写锁.
        /// </summary>
        /// <value>The competitor dic.</value>
        public Dictionary<Guid, LadderManagerEntity> CompetitorDic { get; private set; }

        /// <summary>
        /// 经理-比赛对应字典
        /// </summary>
        public Dictionary<Guid, Guid> ManagerFightDic { get; set; }
        /// <summary>
        /// 经理-引导比赛对应字典
        /// </summary>
        public Dictionary<Guid, Guid> ManagerGuideFightDic { get; set; }

        /// <summary>
        /// 天梯用户比赛CD
        /// </summary>
        public ConcurrentDictionary<Guid, DateTime> _ManagerMatchCD { get; set; }
        /// <summary>
        /// 天梯赛VIP用户比赛CD
        /// </summary>
        public int LadderVipMatchCD;
        /// <summary>
        /// 天梯赛非VIP用户比赛CD
        /// </summary>
        public int LadderNotVipMatchCD;
        /// <summary>
        /// 天梯赛VIP用户清除CD价格
        /// </summary>
        public int LadderVipClearCDPrice;
        /// <summary>
        /// 天梯赛非VIP用户清除CD价格
        /// </summary>
        public int LadderNotVipClearCDPrice;
        /// <summary>
        /// 最近一次平均等待时间
        /// </summary>
        public int RecentlyAvgWaitSecond = 60;
        #endregion

        #region .ctor
        public LadderCore(int p)
        {
            _ManagerMatchCD = new ConcurrentDictionary<Guid, DateTime>();
            _ladderRegisterScore =
            CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RegisterLadderScore);
            _ladderExchangeShowDay =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderExchangeShowDay);
            LadderVipMatchCD = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderVipMatchCD);
            LadderNotVipMatchCD = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderNotVipMatchCD);
            LadderVipClearCDPrice = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderVipClearCDPrice);
            LadderNotVipClearCDPrice = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderNotVipClearCDPrice);
        }
        #endregion

        #region Front interface
        public static LadderCore Instance
        {
            get { return SingletonFactory<LadderCore>.SInstance; }
        }

        public LadderManagerEntity GetLadderManager(Guid managerId)
        {
            var info = InnerGetLadderManager(managerId);
            if (info.Season == null)
            {
                var season = CacheFactory.SeasonCache.GetCurrentSeason();
                info.Season = season;
            }
            return info;
        }

        public LadderRefreshExchangeResponse RefreshExchange(Guid managerId)
        {
            var entity = GetLadderManager(managerId);
            entity.RefreshTimes++;
            var mallDirect = new MallDirectFrame(managerId, EnumConsumeSourceType.RefreshLadderExchange,
                entity.RefreshTimes);
            var checkCode = mallDirect.Check();
            if (checkCode != MessageCode.Success)
                return ResponseHelper.Create<LadderRefreshExchangeResponse>(checkCode);
            var equipmentProperties = "";
            var equipmentItemcode = "";
            entity.ExchangeIds = CacheFactory.LadderCache.GetExchanges(out equipmentItemcode, out equipmentProperties);
            entity.ExchangedIds = "";
            entity.EquipmentProperties = equipmentProperties;
            entity.EquipmentItems = equipmentItemcode;
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                checkCode = mallDirect.Save(Guid.NewGuid().ToString(), transactionManager.TransactionObject);
                if (checkCode != MessageCode.Success)
                {
                    transactionManager.Rollback();
                    return ResponseHelper.Create<LadderRefreshExchangeResponse>(checkCode);
                }
                if (!LadderManagerMgr.Update(entity, transactionManager.TransactionObject))
                {
                    transactionManager.Rollback();
                    return ResponseHelper.Create<LadderRefreshExchangeResponse>(MessageCode.NbUpdateFail);
                }
                transactionManager.Commit();
            }
            var response = ResponseHelper.CreateSuccess<LadderRefreshExchangeResponse>();
            response.Data = new LadderRefreshExchangeEntity();
            response.Data.ExchangeIds = entity.ExchangeIds;
            response.Data.ManagerPoint = mallDirect.RemainPoint;
            response.Data.RefreshPoint =
                CacheFactory.MallCache.GetDirectPoint(EnumConsumeSourceType.RefreshLadderExchange,
                    entity.RefreshTimes + 1);
            response.Data.Honor = entity.Honor;
            if (DateTime.Now.Hour >= 21)
                response.Data.ExchangeRefreshTick = ShareUtil.GetTimeTick(DateTime.Today.AddDays(1).AddHours(21));
            else
                response.Data.ExchangeRefreshTick = ShareUtil.GetTimeTick(DateTime.Today.AddHours(21));

            response.Data.AllEquipmentProperties =
                CacheFactory.LadderCache.AnalysisProperties(entity.EquipmentProperties);
            response.Data.LadderCoin = entity.LadderCoin;
            return response;
        }

        public LadderExchangeResponse Exchange(Guid managerId, string exchangeKey)
        {
            var manager = GetLadderManager(managerId);
            if (manager == null || string.IsNullOrEmpty(manager.ExchangeIds)
                || !manager.ExchangeIds.Contains(exchangeKey))
                return ResponseHelper.InvalidParameter<LadderExchangeResponse>();
            var exchangeCache = CacheFactory.LadderCache.GetExchangeEntity(exchangeKey);
            if (exchangeCache == null)
                return ResponseHelper.InvalidParameter<LadderExchangeResponse>();

            if (manager.ExchangedIds.Contains(exchangeKey))
                return ResponseHelper.Create<LadderExchangeResponse>(MessageCode.LadderExchangeTimesOver);

            //if (manager.Score < exchangeCache.NeedScore)
            //    return ResponseHelper.Create<LadderExchangeResponse>(MessageCode.LadderExchangeScoreShortage);
            if (manager.Honor < exchangeCache.CostHonor)
                return ResponseHelper.Create<LadderExchangeResponse>(MessageCode.LadderExchangeHonorShortage);
            if (manager.LadderCoin < exchangeCache.LadderCoin)
                return ResponseHelper.Create<LadderExchangeResponse>(MessageCode.LadderExchangeLadderCoinShortage);
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.LadderExchange);

            var itemcode = exchangeCache.ItemCode;
            var exchanagerItemCode = Convert.ToInt32(exchangeKey.Split(',')[1]);
            if (exchangeCache.ItemCode != exchanagerItemCode)
                itemcode = exchanagerItemCode;

            var itemInfo = CacheFactory.ItemsdicCache.GetItem(itemcode);
            var code = MessageCode.Success;
            if (itemInfo.ItemType == (int) EnumItemType.Equipment)
            {
                var allEquipmentProperties = CacheFactory.LeagueCache.AnalysisProperties(manager.EquipmentProperties);
                var property = GetEquipmentProperty(manager.EquipmentItems, allEquipmentProperties, itemcode);
                code = package.AddEquipment(itemcode, true,false, property);
            }
            else
            {
                code = package.AddItem(itemcode, true,false);

            }

            if (code != MessageCode.Success)
                return ResponseHelper.Create<LadderExchangeResponse>(code);
            manager.Honor = manager.Honor - exchangeCache.CostHonor;
            manager.LadderCoin = manager.LadderCoin - exchangeCache.LadderCoin;
            manager.UpdateTime = DateTime.Now;
            manager.ExchangedIds = manager.ExchangedIds + "|" + exchangeKey;

            var record = new LadderExchangerecordEntity()
            {
                CostHonor = exchangeCache.CostHonor,
                ItemCode = itemcode,
                ManagerId = managerId,
                RowTime = DateTime.Now
            };
            code = SaveExchange(manager, package, record);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<LadderExchangeResponse>(code);
            else
            {
                package.Shadow.Save();
                var response = ResponseHelper.CreateSuccess<LadderExchangeResponse>();
                response.Data = new LadderExchangeEntity() {CurHonor = manager.Honor, ItemCode = itemcode,LadderCoin = manager.LadderCoin};
                return response;
            }
        }

        public int GetLadderRank(Guid managerId)
        {
            var rankEntity = RankLadderThread.Instance.GetMyRank(managerId, (int)EnumRankType.LadderRank);
            if (rankEntity != null)
            {
                return rankEntity.Rank;
            }
            else
            {
                return 0;
            }
        }

        EquipmentProperty GetEquipmentProperty(string itemcodes, List<EquipmentProperty> allEquipmentProperties, int curItemcode)
        {
            var itemList = itemcodes.Split('|');
            if (itemList.Length > 0)
            {
                if (itemList.Length != allEquipmentProperties.Count)
                    return null;

                for (int i = 0; i < itemList.Length; i++)
                {
                    var itemcode = Convert.ToInt32(itemList[i]);
                    if (itemcode == curItemcode)
                    {
                        return allEquipmentProperties[i];
                    }
                }
            }
            return null;
        }

        public LadderManagerResponse GetManagerInfo(Guid managerId)
        {
            var manager = GetLadderManager(managerId);
            if (string.IsNullOrEmpty(manager.ExchangeIds) || CheckExchangeRefresh(manager.RefreshDate))
            {
                var equipmentProperties = "";
                var equipmentItemcode = "";
                manager.ExchangeIds = CacheFactory.LadderCache.GetExchanges(out equipmentItemcode, out equipmentProperties);
                manager.RefreshDate = DateTime.Today.AddHours(21);//每天21点刷新
                manager.RefreshTimes = 0;
                manager.ExchangedIds = "";
                manager.EquipmentProperties = equipmentProperties;
                manager.EquipmentItems = equipmentItemcode;
                LadderManagerMgr.Update(manager);
            }
            var season = CacheFactory.SeasonCache.GetCurrentSeason();
            season.StartTick = ShareUtil.GetTimeTick(season.Startdate);
            season.EndTick = ShareUtil.GetTimeTick(season.Enddate.Date.AddDays(1).AddSeconds(-1));
            season.NowTick = ShareUtil.GetTimeTick(DateTime.Now);
            var response = ResponseHelper.CreateSuccess<LadderManagerResponse>();
            response.Data = manager;
            response.Data.Season = season;
            if (DateTime.Now.Hour >= 21)
                response.Data.ExchangeRefreshTick =
                    ShareUtil.GetTimeTick(DateTime.Today.AddDays(1).AddHours(21));
            else
                response.Data.ExchangeRefreshTick =
                    ShareUtil.GetTimeTick(DateTime.Today.AddHours(21));
            var rankEntity = RankLadderThread.Instance.GetMyRank(managerId, (int)EnumRankType.LadderRank);
            if (rankEntity != null)
            {
                response.Data.MyRank = rankEntity.Rank;
                response.Data.YesterdayRank = rankEntity.YesterdayRank;
            }
            else
            {
                response.Data.MyRank = -1;
                response.Data.YesterdayRank = -1;
            }
            response.Data.WinRate = ManagerUtil.GetWinRate(managerId, EnumMatchType.Ladder);
            response.Data.RefreshPoint =
                CacheFactory.MallCache.GetDirectPoint(EnumConsumeSourceType.RefreshLadderExchange, manager.RefreshTimes + 1);
            response.Data.AllEquipmentProperties =
                CacheFactory.LeagueCache.AnalysisProperties(manager.EquipmentProperties);
            if (LadderCore.Instance._ManagerMatchCD.ContainsKey(managerId))
                response.Data.CDTick = ShareUtil.GetTimeTick(_ManagerMatchCD[managerId]);
            else
                response.Data.CDTick = ShareUtil.GetTimeTick(DateTime.Now);
            return response;
        }

        /// <summary>
        /// 天梯赛清除CD
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LadderClearCDResponse LadderClearCD(Guid managerId)
        {
            LadderClearCDResponse response = new LadderClearCDResponse();
            response.Data = new LadderClearCD();
            try
            {
                if (_ManagerMatchCD.ContainsKey(managerId))
                {
                    if (DateTime.Now >= _ManagerMatchCD[managerId])
                        response.Code = (int)MessageCode.LadderCdEnd;
                    var manager = ManagerCore.Instance.GetManager(managerId);
                    int point = 0;
                    if (manager.VipLevel > 0)
                        point = LadderVipClearCDPrice;
                    else
                        point = LadderNotVipClearCDPrice;
                    var pointNumber = PayCore.Instance.GetPoint(managerId);
                    if (pointNumber < point)
                    {
                        response.Code = (int) MessageCode.NbPointShortage;
                        return response;
                    }
                    var messCode = PayCore.Instance.GambleConsume(managerId, point, ShareUtil.GenerateComb(),
                        EnumConsumeSourceType.LadderClearCD);
                    if (messCode == MessageCode.Success)
                    {
                        _ManagerMatchCD[managerId] = DateTime.Now;
                        response.Data.CDTick = ShareUtil.GetTimeTick(DateTime.Now);
                        response.Data.Point = pointNumber - point;
                    }
                }
                else
                    response.Code = (int)MessageCode.LadderCdEnd;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("天梯赛清除CD", ex);
            }
            return response;
        }

        /// <summary>
        /// 天梯赛清除CD参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LadderClearCDParaResponse LadderClearCDPara(Guid managerId)
        {
            LadderClearCDParaResponse response = new LadderClearCDParaResponse();
            response.Data = new LadderClearCDPara();
            try
            {
                if (_ManagerMatchCD.ContainsKey(managerId))
                {
                    if (DateTime.Now >= _ManagerMatchCD[managerId])
                        response.Code = (int) MessageCode.LadderCdEnd;
                    var manager = ManagerCore.Instance.GetManager(managerId);
                    if (manager.VipLevel > 0)
                        response.Data.Point = LadderVipClearCDPrice;
                    else
                        response.Data.Point = LadderNotVipClearCDPrice;
                }
                else
                    response.Code = (int) MessageCode.LadderCdEnd;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("天梯赛清除cd参数", ex);
            }
            return response;
        }

        /// <summary>
        /// 报名天梯赛.
        /// </summary>
        /// <returns></returns>
        public MessageCodeResponse AttendLadder(Guid managerId, bool hasTask, bool isGuide = false)
        {
            if (_ManagerMatchCD.ContainsKey(managerId))
            {
                if (_ManagerMatchCD[managerId] > DateTime.Now)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.LadderMatchCding);
            }
            if (ManagerFightDic != null && ManagerFightDic.ContainsKey(managerId))
                ManagerFightDic.Remove(managerId);
            if (isGuide) //引导
            {
                var response = GuideMatch(managerId);
                if (response.Code == (int) MessageCode.Success)
                    return response;
            }
            if (!CompetitorDic.ContainsKey(managerId))
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.LoginNoManager);
                var arenaManager = InnerGetLadderManager(managerId);
                if (arenaManager == null)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
                arenaManager.IsBot = false;
                arenaManager.Name = manager.Name;
                arenaManager.UpdateTime = DateTime.Now;
                arenaManager.HasTask = true;
                //锁住
                lock (_competitorLock)
                {
                    if (_playerNum == 0)
                        StartTime = DateTime.Now;
                    CompetitorDic.Add(managerId, arenaManager);
                    _playerNum++;
                }
            }
            return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
        }

        /// <summary>
        /// 引导比赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        private MessageCodeResponse GuideMatch(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
            var arenaManager = InnerGetLadderManager(managerId);
            if (arenaManager == null)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);


            if (ManagerGuideFightDic == null)
                ManagerGuideFightDic = new Dictionary<Guid, Guid>();
            var laddermanager = GetLadderManager(managerId);
            if (laddermanager.MatchTime > 0)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError) ;
            }
            LadderManagerEntity bot = new LadderManagerEntity();
            var botList = LadderManagerMgr.GetBot(1, 0, 1201);
            if (botList != null)
            {
                bot = botList[0];
                bot.Name = "送分小王子";
                bot.IsBot = true;
                bot.Score = laddermanager.Score;
            }

            laddermanager.IsBot = false;
            laddermanager.Name = manager.Name;
            laddermanager.UpdateTime = DateTime.Now;
            laddermanager.HasTask = true;

            var ladderInfo = new LadderInfoEntity();
            ladderInfo.Idx = ShareUtil.GenerateComb();
            ladderInfo.FightList = new List<LadderManagerEntity>() { laddermanager, bot };
            ladderInfo.StartTime = DateTime.Now;
            ladderInfo.GroupingTime = DateTime.Now;
            ladderInfo.CountdownTime = DateTime.Now;

            ladderInfo.CountdownTime = DateTime.Now.AddSeconds(1);

            ladderInfo.PlayerNumber = 2;
            ladderInfo.AvgWaitTime = 2;

            var matchId = ShareUtil.GenerateComb();
            var ladderMatch = new LadderMatchEntity(laddermanager, bot, matchId, ladderInfo.Idx, 1);
            ConcurrentDictionary<Guid, LadderMatchEntity> fightDic = new ConcurrentDictionary<Guid, LadderMatchEntity>();
            fightDic.TryAdd(ladderMatch.Idx, ladderMatch);

            MemcachedFactory.LadderMatchClient.Set(ladderMatch.Idx, ladderMatch);
            var process = new LadderProcess(fightDic, ladderInfo, LadderThread.Instance._ladderProctiveScore, true);
            process.StartProcess();
            if (!ManagerGuideFightDic.ContainsKey(managerId))
                ManagerGuideFightDic.Add(managerId, matchId);
            return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
        }


        public MessageCode HookAttend(LadderHookEntity entity)
        {
            if (!IsManagerBusy(entity.ManagerId))
            {
                if (!CompetitorDic.ContainsKey(entity.ManagerId))
                {
                    entity.LadderManager.UpdateTime = DateTime.Now;
                    //锁住
                    lock (_competitorLock)
                    {
                        if (_playerNum == 0)
                            StartTime = DateTime.Now;

                        CompetitorDic.Add(entity.ManagerId, entity.LadderManager);
                        _playerNum++;
                    }
                }
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.LadderBusy;
            }
        }

        /// <summary>
        /// 退出天梯赛.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public MessageCodeResponse LeaveLadder(Guid managerId)
        {
            if (!IsManagerBusy(managerId))
            {
                if (CompetitorDic.ContainsKey(managerId))
                {
                    lock (_competitorLock)
                    {
                        _playerNum--;
                        CompetitorDic.Remove(managerId);
                    }
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
                }
                else
                {
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
                }
            }
            else
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.LadderCountdown);
            }
        }

        /// <summary>
        /// 天梯赛状态轮询.
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns></returns>
        public Ladder_HeartResponse LadderHeart(Guid managerId)
        {
            Ladder_HeartResponse response;
            if (CompetitorDic.ContainsKey(managerId))
            {
                //锁住
                //lock (_competitorLock)
                //{
                //    if (CompetitorDic.ContainsKey(managerId))
                //    {
                //        CompetitorDic[managerId].UpdateTime = DateTime.Now;
                //    }
                //}
                response = ResponseHelper.CreateSuccess<Ladder_HeartResponse>();
            }
            else if (ManagerFightDic != null && ManagerFightDic.ContainsKey(managerId))
            {
                var matchId = ManagerFightDic[managerId];

                if (matchId == Guid.Empty)
                {
                    if (Status == EnumLadderStatus.Grouping)
                    {
                        response = ResponseHelper.Create<Ladder_HeartResponse>(MessageCode.Success);
                    }
                    else
                    {
                        response = ResponseHelper.Create<Ladder_HeartResponse>(MessageCode.NbParameterError);
                    }
                }
                else
                {
                    response = ResponseHelper.Create<Ladder_HeartResponse>(MessageCode.Success);
                    response.Data = new Ladder_Heart();
                    response.Data.MatchId = matchId;
                }
            }
            else if (ManagerGuideFightDic != null && ManagerGuideFightDic.ContainsKey(managerId))//引导比赛
            {
                var matchId = ManagerGuideFightDic[managerId];
                response = ResponseHelper.Create<Ladder_HeartResponse>(MessageCode.Success);
                response.Data = new Ladder_Heart();
                response.Data.MatchId = matchId;
                if (ManagerGuideFightDic.ContainsKey(managerId))
                    ManagerGuideFightDic.Remove(managerId);
            }
            else
            {
                response = ResponseHelper.Create<Ladder_HeartResponse>(MessageCode.NbParameterError);
            }
            if (response.Data == null)
                response.Data = new Ladder_Heart();
            if (RecentlyAvgWaitSecond > 60)
                response.Data.AvgWaitTime = 60;
            else
            {
                response.Data.AvgWaitTime = RecentlyAvgWaitSecond;
            }

            return response;
        }

        public LadderMatchEntityListResponse GetMatchList(Guid managerId)
        {
            var response = ResponseHelper.CreateSuccess<LadderMatchEntityListResponse>();
            response.Data = new LadderMatchEntityList();
            var list = LadderMatchMgr.GetFiveMatch(managerId);
            if (list != null)
            {
                response.Data.Matchs = new List<LadderMatchEntityView>(list.Count);
                foreach (var match in list)
                {
                    string awayName = "";
                    EnumWinType winType;//0：负; 1：胜;2：平
                    int prizeScore;
                    string score = ""; //比分 2:3
                    if (managerId == match.HomeId && match.HomeIsBot == false)
                    {
                        awayName = match.AwayName;
                        prizeScore = match.PrizeHomeScore;
                        score = string.Format("{0}:{1}", match.HomeScore, match.AwayScore);
                        winType = ShareUtil.CalWinType(match.HomeScore, match.AwayScore);
                    }
                    else
                    {
                        if (match.AwayIsBot)
                        {
                            continue;
                        }
                        awayName = match.HomeName;
                        prizeScore = match.PrizeAwayScore;
                        score = string.Format("{0}:{1}", match.AwayScore, match.HomeScore);
                        winType = ShareUtil.CalWinType(match.AwayScore, match.HomeScore);
                    }
                    response.Data.Matchs.Add(BuildMatchView(match.Idx, awayName, prizeScore, score, winType));
                }
            }
            return response;
        }

        public LadderMatchEntityResponse GetMatch(Guid managerId, Guid matchId)
        {
            var match = MemcachedFactory.LadderMatchClient.Get<LadderMatchEntity>(matchId);
            if (match == null)
            {
                match = LadderMatchMgr.GetById(matchId);
                if (match == null)
                {
                    return ResponseHelper.InvalidParameter<LadderMatchEntityResponse>();
                }
            }

            var response = ResponseHelper.CreateSuccess<LadderMatchEntityResponse>();
            response.Data = match;
            response.Data.PopMsg = MemcachedFactory.MatchPopClient.Get<List<PopMessageEntity>>(managerId);
            if (response.Data.PopMsg != null)
            {
                MemcachedFactory.MatchPopClient.Delete(managerId);
            }
            return response;
        }

        #endregion

        #region Back service
        /// <summary>
        /// Creates the ladder.
        /// </summary>
        public void CreateLadder()
        {
            StartTime = DateTime.Now;
            _playerNum = 0;
            CompetitorDic = new Dictionary<Guid, LadderManagerEntity>();
        }

        /// <summary>
        /// 将加入本轮天梯赛的经理推进比赛池.
        /// </summary>
        /// <returns></returns>
        public LadderInfoEntity GetCompetitorToMatch()
        {
            //将状态置为分组
            var fightList = new List<LadderManagerEntity>();
            var arenaLadder = new LadderInfoEntity();
            lock (_competitorLock)
            {
                ManagerFightDic = new Dictionary<Guid, Guid>();
                foreach (var dic in CompetitorDic)
                {
                    //将经理推进比赛池
                    ManagerFightDic.Add(dic.Key, Guid.Empty);
                    fightList.Add(dic.Value);
                }
                Status = EnumLadderStatus.Grouping;
                arenaLadder.Idx = ShareUtil.GenerateComb();
                arenaLadder.FightList = fightList;
                arenaLadder.StartTime = StartTime;
                arenaLadder.GroupingTime = DateTime.Now;

                //开始新一轮报名
                CreateLadder();
            }

            return arenaLadder;
        }

        /// <summary>
        /// 将已进入比赛池的经理重新丢回排序池.
        /// </summary>
        /// <param name="arenaCompetitor">The arena competitor.</param>
        public void PushFightToCompetitor(LadderManagerEntity arenaCompetitor)
        {
            lock (_competitorLock)
            {
                if (ManagerFightDic != null && ManagerFightDic.ContainsKey(arenaCompetitor.ManagerId))
                {
                    ManagerFightDic.Remove(arenaCompetitor.ManagerId);
                    if (!CompetitorDic.ContainsKey(arenaCompetitor.ManagerId))
                    {
                        if (_playerNum == 0)
                            StartTime = DateTime.Now;
                        CompetitorDic.Add(arenaCompetitor.ManagerId, arenaCompetitor);
                        _playerNum++;
                    }
                }

            }
        }
        #endregion

        #region encapsulation

        MessageCode SaveExchange(LadderManagerEntity ladderManager, ItemPackageFrame package, LadderExchangerecordEntity ladderExchangerecord)
        {
            if (ladderManager == null || package == null || ladderExchangerecord == null)
            {
                return MessageCode.NbUpdateFail;
            }
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveExchange(transactionManager.TransactionObject, ladderManager, package, ladderExchangerecord);

                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return messageCode;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveExchange", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_SaveExchange(DbTransaction transaction, LadderManagerEntity ladderManager, ItemPackageFrame package, LadderExchangerecordEntity ladderExchangerecord)
        {
            if (!LadderManagerMgr.Update(ladderManager, transaction))
                return MessageCode.NbUpdateFail;
            if (!package.Save(transaction))
                return MessageCode.NbUpdateFail;
            if (!LadderExchangerecordMgr.Insert(ladderExchangerecord, transaction))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }

        bool CheckExchangeRefresh(DateTime refreshDate)
        {
            if (refreshDate.AddDays(_ladderExchangeShowDay) < DateTime.Today.AddHours(21))
                return true;

            if (DateTime.Now >= DateTime.Today.AddHours(21) && refreshDate < DateTime.Today.AddHours(21))
                return true;

            return false;
        }

        LadderMatchEntityView BuildMatchView(Guid matchId, string awayName, int prizeScore, string score, EnumWinType winType)
        {
            var matchView = new LadderMatchEntityView();
            matchView.AwayName = awayName;
            matchView.MatchId = matchId;
            matchView.PrizeScore = prizeScore;
            matchView.ScoreView = score;
            matchView.WinType = (int)winType;
            return matchView;
        }

        public LadderManagerEntity InnerGetLadderManager(Guid managerId)
        {
            return LadderManagerMgr.GetById(managerId, _ladderRegisterScore);
        }

        /// <summary>
        /// Determines whether [is manager busy] [the specified manager id].
        /// </summary>
        /// <param name="managerId">The manager id.</param>
        /// <returns>
        /// 	<c>true</c> if [is manager busy] [the specified manager id]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsManagerBusy(Guid managerId)
        {
            if (ManagerFightDic != null && ManagerFightDic.ContainsKey(managerId))
                return true;
            else
            {
                return false;
            }
        }
        #endregion
    }
}
