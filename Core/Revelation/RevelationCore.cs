//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Games.NBall.Bll;
//using Games.NBall.Bll.Share;
//using Games.NBall.Common;
//using Games.NBall.Core.Activity;
//using Games.NBall.Core.Mail;
//using Games.NBall.Core.Mall;
//using Games.NBall.Core.Manager;
//using Games.NBall.Core.Online;
//using Games.NBall.Entity;
//using Games.NBall.Entity.Enums;
//using Games.NBall.Entity.Enums.Common;
//using Games.NBall.Entity.Response.Revelation;
//using MsEntLibWrapper.Data;
//using Games.NBall.Core.Item;
//using Games.NBall.Entity.Enums.Shadow;
//using Games.NBall.Entity.Match;
//using Games.NBall.Core.Match;


//namespace Games.NBall.Core.Revelation
//{
//    /// <summary>
//    /// 球星启示录主表 处理客户端的请求
//    /// </summary>
//    public class RevelationCore
//    {
//        private static RevelationCore instance;
//        private static object __LOCK__ = new object();
//        private RevelationCore() 
//        {
//            _challengeMaxCheck = new ConcurrentDictionary<Guid, ConcurrentDictionary<int, int>>();
//            _historyDic = new ConcurrentDictionary<int, ConcurrentDictionary<int, RevelationHistoryofthegapEntity>>();
//            _myHistoryDic = new ConcurrentDictionary<string, ConcurrentDictionary<int, RevelationMyhistoryofthegapEntity>>();
//        }
//        /// <summary>
//        /// 静态构造方法 返回本类的实例
//        /// </summary>
//        public static RevelationCore Instance 
//      { 
//          get 
//          {
//              if (instance == null)
//              {
//                  lock (__LOCK__)
//                  {
//                      if(instance == null)
//                          instance = new RevelationCore();
//                  }
//              }
//              return instance; 
//          } 
//      }

//        /// <summary>
//        /// 可挑战的最高关卡集合
//        /// </summary>
//        private static ConcurrentDictionary<Guid, ConcurrentDictionary<int, int>> _challengeMaxCheck;

//        /// <summary>
//        /// 关卡霸主
//        /// </summary>
//        private static ConcurrentDictionary<int, ConcurrentDictionary<int, RevelationHistoryofthegapEntity>> _historyDic;

//        /// <summary>
//        /// 自己的最高记录
//        /// </summary>
//        private static ConcurrentDictionary<string, ConcurrentDictionary<int, RevelationMyhistoryofthegapEntity>> _myHistoryDic;

//        /// <summary>
//        /// 根据经理ID 获取可挑战的最高球星关卡
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <returns></returns>
//        ConcurrentDictionary<int, int> MaxChallengeCheck(Guid managerId)
//        {
//            if (_challengeMaxCheck.ContainsKey(managerId))
//                return _challengeMaxCheck[managerId];
//            ConcurrentDictionary<int, int> dic = new ConcurrentDictionary<int, int>();
//            //获取关卡配置
//            List<ConfigRevelationthebadgeawaryEntity> config = CacheFactory.RevelationCache.GetAllCheck();
//            //获取关卡信息
//            List<RevelationCheckpointEntity> list = RevelationCheckpointMgr.C_RevelationCountGenerl(managerId);
//            config = config.OrderByDescending(r => r.Idx).ToList();
//            var generalList = list.FindAll(r => r.IsGeneral).ToList();
//            if (list.Count == 0 || generalList.Count == 0)
//            {
//                for (int i = 0; i < config.Count; i++)
//                {
//                    if (i == 0)
//                        dic.TryAdd(config[i].Idx, 1);
//                    else
//                        dic.TryAdd(config[i].Idx, 0);
//                }

//            }
//            else
//            {
//                var maxCheck = generalList.Min(r => r.CustomsPass);
//                for (int i = 0; i < config.Count; i++)
//                {
//                    if (config[i].Idx > maxCheck)
//                        dic.TryAdd(config[i].Idx, 1);
//                    else if (config[i].Idx == maxCheck)
//                    {
//                        dic.TryAdd(config[i].Idx, 1);
//                        if(i < config.Count - 1)
//                          dic.TryAdd(config[i + 1].Idx, 1);
//                        i++;
//                    }
//                    else
//                    {
//                        dic.TryAdd(config[i].Idx, 0);
//                    }
//                }
//            }
//            _challengeMaxCheck.TryAdd(managerId, dic);
//            return dic;
//        }

//        /// <summary>
//        /// 获取关卡霸主
//        /// </summary>
//        /// <param name="mark"></param>
//        /// <returns></returns>
//        ConcurrentDictionary<int, RevelationHistoryofthegapEntity> GetHistoryOfTheGap(int mark) 
//        {
//            if (_historyDic.ContainsKey(mark))
//                return _historyDic[mark];
//            ConcurrentDictionary<int, RevelationHistoryofthegapEntity> dic = new ConcurrentDictionary<int, RevelationHistoryofthegapEntity>();

//            var list = RevelationHistoryofthegapMgr.C_RevelationHistoryOfTheGapGetId(mark);
//            foreach (var item in list)
//            {
//                dic.TryAdd(item.Schedule, item);
//            }
//            _historyDic.TryAdd(mark, dic);
//            return dic;
//        }
//         /// <summary>
//        /// 自己的关卡最高记录
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="mark"></param>
//        /// <returns></returns>
//        ConcurrentDictionary<int, RevelationMyhistoryofthegapEntity> GetMyHistoryOfTheGap(Guid managerId, int mark) 
//        {
//            if (_myHistoryDic.ContainsKey(managerId.ToString()+ mark))
//                return _myHistoryDic[managerId.ToString() + mark];
//            ConcurrentDictionary<int, RevelationMyhistoryofthegapEntity> dic = new ConcurrentDictionary<int, RevelationMyhistoryofthegapEntity>();

//            var list = RevelationMyhistoryofthegapMgr.GetMyHistoryOfTheGap(managerId, mark);
//            foreach (var item in list)
//            {
//                dic.TryAdd(item.Schedule, item);
//            }
//            _myHistoryDic.TryAdd(managerId.ToString()+mark, dic);
//            return dic;
//        }

//        /// <summary>
//        /// 球星启示录背包加物品
//        /// </summary>
//        /// <param name="managerId">经理ID</param>
//        /// <param name="itemCode">物品ID</param>
//        /// <param name="itemCount">物品数量</param>
//        /// <param name="strength">强化等级</param>
//        /// <param name="isBinding">是否绑定</param>
//        /// <returns></returns>
//        public MessageCode AddItems(Guid managerId, int itemCode, int itemCount, int strength, bool isBinding,bool isDeal, DbTransaction tran)
//        {
//            try
//            {
//                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.RevelationClearanceAward);
//                if (package == null)
//                    return MessageCode.NbParameterError;
//                var result = package.AddItems(itemCode, itemCount, strength, isBinding,isDeal);
//                if (result == MessageCode.Success)
//                {
//                    bool isSuccess = package.Save(tran);
//                    if (isSuccess)
//                    {
//                        package.Shadow.Save();
//                    }
//                    return MessageCode.Success;
//                }
//                else
//                {
//                    return result;
//                }
//            }
//            catch (Exception ex)
//            {
//                SystemlogMgr.Error("Revelation.AddItems", ex);
//                return MessageCode.Exception;
//            }
//        }

//        /// <summary>
//        /// 获取球星启示录经理信息
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <returns></returns>
//        public RevelationMainEntity GetById(Guid managerId)
//        {
//            //获取主表信息
//            NbManagerEntity manager = NbManagerMgr.GetById(managerId);
//            if (manager == null)
//                return null;
//            //获取球星启示录信息
//            RevelationMainEntity entity = RevelationMainMgr.GetById(managerId);
//            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
//            try
//            {
//                DateTime date = DateTime.Today.AddDays(-1);
//                //获取球星启示录等级限制
//                int level = CacheFactory.RevelationCache.GetSunthesizeion((int)EnumRecelation.LevelLimits);
//                if (entity != null && entity.RefreshTime!= DateTime.Today)
//                {
//                    entity.ChallengesNums = 0;
//                    entity.FailCD = date;
//                    entity.OnHookCD = date;
//                    entity.BuyTheNumber = 0;
//                    entity.RefreshTime = DateTime.Today;
//                    RevelationMainMgr.C_RevelationEverDay(managerId);
//                    if (!RevelationMainMgr.Update(entity))
//                    {
//                        SystemlogMgr.Error("球星启示录每天刷新出错", "经理ID" + managerId);
//                        return null;
//                    }
//                }
//                else if (entity == null && manager.Level >= level)
//                {
//                    entity = new RevelationMainEntity();
//                    entity.ChallengesNums = 0;
//                    entity.Courage = 0;
//                    entity.FailCD = date;
//                    entity.BuyTheNumber = 0;
//                    entity.ManagerId = managerId;
//                    entity.OnHookCD = date;
//                    entity.RefreshTime = DateTime.Today;
//                    entity.States = 0;
//                    RevelationMainMgr.Insert(entity);
//                }
//            }
//            catch (Exception ex)
//            {
//                SystemlogMgr.Error("球星启示录获取经理信息", ex);
//                return null;
//            }
//            return entity;
//        }

//        /// <summary>
//        /// 根据经理ID获取经理信息
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <returns></returns>
//        public RevelationGetManagerResponse RevelationGetManagerId(Guid managerId) 
//        {
//            //返回对象
//            RevelationGetManagerResponse response = new RevelationGetManagerResponse();
//            response.Data = new RevelationGetManager();
//            try
//            {
//                //获取经理信息
//                var manager = GetById(managerId);
//                var nbManger = NbManagerMgr.GetById(managerId);
//                if (manager == null || nbManger == null)
//                    return ResponseHelper.InvalidParameter<RevelationGetManagerResponse>("manager is null");
//                //根据VIP等级获取可挑战次数
//                int challengeNum = CacheFactory.RevelationCache.GetSunthesizeion((int)EnumRecelation.ChallengeTime);
//                //挑战失败CD时间
//                var cdTime = CacheFactory.RevelationCache.GetVipPrivilege(nbManger.VipLevel);
//                //挂机CD
//                int onHookCd = CacheFactory.RevelationCache.GetSunthesizeion((int)EnumRecelation.HangUpCDTimes);
//                response.Data.ChallengesNums = manager.ChallengesNums;
//                response.Data.Courage = manager.Courage;
//                response.Data.OnHookCDTick =ShareUtil.GetTimeTick(manager.OnHookCD.AddSeconds(onHookCd));
//                response.Data.FailCDTick = DateTime.Now >= manager.FailCD.AddSeconds(cdTime.CDTime) ? 0 : DateTime.Now.GetSpanTime(manager.FailCD.AddSeconds(cdTime.CDTime));
//                response.Data.SumChallengesNums = manager.BuyTheNumber + challengeNum;
//                response.Data.BuyNumber = manager.BuyTheNumber;
//                response.Data.CanChallengeList = MaxChallengeCheck(managerId);
//            }
//            catch (Exception ex) 
//            {
//                response.Code = (int)MessageCode.NbParameterError;
//                SystemlogMgr.Error("球星启示录获取经理信息", ex);
//            }
//            return response;
//        }

//        /// <summary>
//        /// 根据关卡获取关卡信息
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="mark"></param>
//        /// <returns></returns>
//        public RevelationGetIsGeneralResponse RevelationGetIsGeneral(Guid managerId, int mark)
//        {
//            //返回对象
//            RevelationGetIsGeneralResponse response = new RevelationGetIsGeneralResponse();
//            response.Data = new RevelationGetIsGeneral();
//            response.Data.Entity = new CheckPointEntity();
//            response.Data.HistoryList = new Dictionary<int,HistoryOgThegapEntity>();
//            try
//            {
//                var manager = GetById(managerId);
//                var nbManager = NbManagerMgr.GetById(managerId);
//                //未找到经理
//                if (manager == null || nbManager == null)
//                    return ResponseHelper.InvalidParameter<RevelationGetIsGeneralResponse>("manager is null");

//                //获取关卡配置
//                var list = CacheFactory.RevelationCache.GetCheckpointAll(mark);
//                //获取奖励配置
//                var awary = CacheFactory.RevelationCache.GetGeneralAwary(mark);
//                if (list.Count == 0 || awary== null)
//                    return ResponseHelper.InvalidParameter<RevelationGetIsGeneralResponse>("mark is null");
                
//                RevelationCheckpointEntity entity = RevelationCheckpointMgr.C_RevelationGetCheckoint(managerId, mark);
//                //未找到关卡
//                if (entity == null)
//                {
//                    entity = new RevelationCheckpointEntity();
//                    entity.AwaryItem = "";
//                    entity.CustomsPass = mark;
//                    entity.GeneralAwaryTime = ShareUtil.BaseTime;
//                    entity.GeneralTime = ShareUtil.BaseTime;
//                    entity.IsGeneral = false;
//                    entity.IsGeneralAwary = false;
//                    entity.ManagerId = managerId;
//                    entity.Schedule = 0;
//                    entity.States = 0;
//                    entity.ToDayGeneralNums = 0;
//                    RevelationCheckpointMgr.Insert(entity);
//                }

//                response.Data.Entity.IsCanChallenge = 1;
//                response.Data.Entity.CustomsPass = entity.CustomsPass;
//                response.Data.Entity.IsGeneral = entity.IsGeneral;
//                response.Data.Entity.IsGeneralAwary = entity.IsGeneralAwary;
//                response.Data.Entity.Schedule = entity.Schedule + 1;
//                response.Data.Entity.ToDayGeneralNums = entity.ToDayGeneralNums;
//                response.Data.Entity.GeneraNumber = CacheFactory.RevelationCache.GetVipPrivilege(nbManager.VipLevel).Challenges;
//                var history = GetHistoryOfTheGap(mark);
//                var myHistory = GetMyHistoryOfTheGap(managerId, mark);
//                //历史记录
//               Dictionary<int,HistoryOgThegapEntity> historyDic  = new Dictionary<int,HistoryOgThegapEntity>();
//               foreach (var item in history.Values)
//               {
//                   HistoryOgThegapEntity historyEntity = new HistoryOgThegapEntity();
//                   historyEntity.Goals = item.Goals;
//                   historyEntity.ToConcede = item.ToConcede;
//                   historyEntity.ManagerName = item.ManagerName;
//                   historyDic.Add(item.Schedule, historyEntity);
//               }
//               foreach (var item in myHistory.Values)
//               {
//                   if (historyDic.ContainsKey(item.Schedule)) 
//                   {
//                       historyDic[item.Schedule].MyGoals = item.Goals;
//                       historyDic[item.Schedule].MyToConcede = item.ToConcede;
//                   }
//               }
//                response.Data.HistoryList = historyDic;

//                response.Code = (int) MessageCode.Success;
//            }
//            catch (Exception ex) 
//            {
//                response.Code = (int)MessageCode.NbParameterError;
//                SystemlogMgr.Error("球星启示录获取关卡信息", ex);
//            }
//            return response;
//        }

//        #region 比赛
        
//        /// <summary>
//        /// 过关
//        /// </summary>
//        /// <param name="managerId">经理ID</param>
//        /// <param name="mark">大关卡</param>
//        /// <param name="littleLevels">小关卡</param>
//        public RevelationTheGameResponse RevelationTheGame(Guid managerId, int mark, int littleLevels)
//        {
//            //返回对象
//            RevelationTheGameResponse response = new RevelationTheGameResponse();
//            response.Data = new RevelationTheGameEntity();
//            try
//            {
//                #region 获取数据

//                //获取主表经理信息
//                var nbManager = NbManagerMgr.GetById(managerId);
//                //获取启示录经理信息
//                var manager = GetById(managerId);
//                //根据关卡获取启示录进度
//                var checkpoint = RevelationCheckpointMgr.C_RevelationGetCheckoint(managerId, mark);
//                //获取关卡配置
//                var configCheckpoint = CacheFactory.RevelationCache.GetCheckAwary(littleLevels * 1000000 + mark);
//                //获取VIP特权
//                var vipPrivilege = CacheFactory.RevelationCache.GetVipPrivilege(nbManager.VipLevel);
//                //获取默认挑战次数
//                int challengeTime = CacheFactory.RevelationCache.GetSunthesizeion((int) EnumRecelation.ChallengeTime);
//                //随机物品掉落概率
//                int dropRate = CacheFactory.RevelationCache.GetSunthesizeion((int) EnumRecelation.RandomDropRate);
//                //获取关卡NPC
//                ConfigRevelationnpclinkEntity npcEntity = CacheFactory.RevelationCache.GetNpc(littleLevels * 1000000 + mark);

//                #endregion

//                #region 验证

//                //未找到改关卡
//                if (configCheckpoint == null)
//                {
//                    response.Code = (int) MessageCode.RevelationNotFountCheckoint;
//                    return response;
//                }
//                //未找到关卡
//                if (checkpoint == null)
//                {
//                    //插入数据
//                    if (!RevelationCheckpointCore.Instance.InsertInTo(managerId, mark))
//                    {
//                        response.Code = (int) MessageCode.NbParameterError;
//                        return response;
//                    }
//                }
//                //获取关卡可不可以挑战
//                var allCheck = MaxChallengeCheck(managerId);
//                if (!allCheck.ContainsKey(mark))
//                    return ResponseHelper.InvalidParameter<RevelationTheGameResponse>("mark is null");
//                if (allCheck[mark] == 0)
//                    return ResponseHelper.Create<RevelationTheGameResponse>(MessageCode.RevelationNotChallengeCheck);

//                if (npcEntity == null)
//                    return ResponseHelper.InvalidParameter<RevelationTheGameResponse>("NPC is null");
//                //未找到经理
//                if (nbManager == null || manager == null)
//                    return ResponseHelper.InvalidParameter<RevelationTheGameResponse>("manager is null");
//                //挑战次数用完
//                if (manager.BuyTheNumber >= (challengeTime + manager.BuyTheNumber))
//                {
//                    response.Code = (int) MessageCode.RevelationChallengeUseUP;
//                    return response;
//                }
//                //CD时间未到
//                if (manager.FailCD.GetSpanTime(DateTime.Now) <= vipPrivilege.CDTime)
//                {
//                    response.Code = (int) MessageCode.RevelationCDTimes;
//                    return response;
//                }
//                //进度不一致
//                if (checkpoint.Schedule + 1 != littleLevels)
//                {
//                    response.Code = (int) MessageCode.RevelationNoChallenge;
//                    return response;
//                }
//                //通关机会用完
//                if (vipPrivilege.Challenges <= checkpoint.ToDayGeneralNums)
//                {
//                    response.Code = (int) MessageCode.RevelationNotGeneralNums;
//                    return response;
//                }

//                #endregion

//                int goals = 0; //进球数
//                int toConcede = 0; //失球数
//                bool isGeneral = false; //是否通关
//                bool isVictory = false; //是否胜利
//                int courage = 0; //勇气
//                int exp = 0; //奖励经验
//                int gold = 0; //奖励金币

//                #region 比赛

//                //构建主队
//                var matchHome = new MatchManagerInfo(managerId, false, false);
//                //构建客队
//                var matchAway = new MatchManagerInfo(npcEntity.NpcId, true, false);
//                //创建一场比赛
//                Guid idx = ShareUtil.GenerateComb();
//                var matchData = new BaseMatchData((int) EnumMatchType.Revelation, idx, matchHome, matchAway);
//                matchData.ErrorCode = (int) MessageCode.MatchWait;

//                MemcachedFactory.MatchClient.Set(matchData.MatchId, matchData);
//                MatchCore.CreateMatch(matchData);
//                response.Code = matchData.ErrorCode;
//                if (matchData.ErrorCode != (int) MessageCode.Success)
//                {
//                    response.Code = (int) matchData.ErrorCode;
//                    return response;
//                }
//                goals = matchData.Home.Score;
//                toConcede = matchData.Away.Score;

//                #endregion

//                int theGap = goals - toConcede; //分差
//                if (theGap > 0) //赢了比赛
//                {
//                    isVictory = true;
//                    //奖励勇气值
//                    string[] courages = configCheckpoint.AwaryTheCourageTo.Split(',');
//                    int sRan = Convert.ToInt32(courages[0]);
//                    int eRan = Convert.ToInt32(courages[1]);
//                    Random ran = new Random();
//                    courage = ran.Next(sRan, eRan);
//                    exp = configCheckpoint.Exp;
//                    gold = configCheckpoint.Gold;

//                    //获取配置  是否通关
//                    isGeneral = CacheFactory.RevelationCache.GetIsGeneral(mark, littleLevels);
//                }
//                OnlineCore.Instance.CalIndulgePrize(nbManager, ref exp, ref gold,ref courage);
//                //加经理数据  不保存
//                ManagerUtil.AddManagerData(nbManager, exp, gold, 0, EnumCoinChargeSourceType.Revelation,
//                    idx.ToString());

//                MessageCode code = SaveTourMatch(nbManager,mark,littleLevels,goals,theGap,toConcede,isGeneral,courage,isVictory,idx);

//                if (code == MessageCode.Success)
//                {
//                    if (isGeneral)
//                    {
//                        if (_challengeMaxCheck.ContainsKey(managerId))
//                        {
//                            ConcurrentDictionary<int, int> dic;
//                            _challengeMaxCheck.TryRemove(managerId, out dic);
//                        }
//                        MaxChallengeCheck(managerId);
//                    }
//                        MaxChallengeCheck(managerId);
//                    response.Code = (int) MessageCode.Success;
//                    response.Data.ChallengesNums = manager.ChallengesNums+ 1;
//                    response.Data.Courage = courage;
//                    response.Data.Goals = goals;
//                    response.Data.IsAwary = false;
//                    response.Data.IsGeneral = isGeneral;
//                    response.Data.Schedule =checkpoint.Schedule;
//                    response.Data.SumChallengesNums = challengeTime + manager.BuyTheNumber;
//                    response.Data.TheGap = goals - toConcede;
//                    response.Data.ToConcede = toConcede;
//                    response.Data.Exp = exp;
//                    response.Data.Coin = gold;
//                    response.Data.MatchId = idx;
//                }
//                else
//                    response.Code = (int)code;
                
//                if (theGap > 0) //赢了比赛
//                {
//                    response.Data.Schedule = checkpoint.Schedule + 1 <= littleLevels ? littleLevels + 1:checkpoint.Schedule + 1 ;

//                    #region 随机奖励
//                    try
//                    {
//                        //随机概率
//                        if (RandomHelper.CheckPercentage(dropRate))
//                        {
//                            if (!OnlineCore.Instance.CheckIndulgeNoPrize(managerId))
//                            {
//                                response.Data.IsAwary = true;
//                                //随机奖励
//                                var item = CacheFactory.RevelationCache.GetAwary();
//                                response.Data.AwaryCode = item.ItemCore;
//                                //获取背包
//                                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ArenaAward);
//                                if (package == null)
//                                {
//                                    response.Code = (int) MessageCode.NbParameterError;
//                                    return response;
//                                }
//                                //背包空间不足
//                                if (package.BlankCount < 1)
//                                {
//                                    //发邮件
//                                    var mail = new MailBuilder(managerId, item.ItemCore, item.IsBinding, 0);
//                                    mail.Save();
//                                }
//                                else
//                                {
//                                    //TODO..添加到背包
//                                    MessageCode messcode = AddItems(managerId, item.ItemCore, item.ItemNums, 0,
//                                        item.IsBinding,false, null);
//                                    //添加到背包出错
//                                    if (messcode != MessageCode.Success)
//                                    {
//                                        response.Data.IsAwary = false;
//                                        SystemlogMgr.Error("球星启示录背包添加物品出错", "过关随机奖励");
//                                    }
//                                }
//                            }
//                        }
//                    }
//                    catch { response.Data.IsAwary = false; }

//                    #endregion
//                }

//                //推送
//                ManagerUtil.SaveManagerAfter(nbManager, null, true);

//            }
//            catch (Exception ex)
//            {
//                SystemlogMgr.Error("球星启示录过关卡失败", ex);
//                response.Code = (int) MessageCode.NbParameterError;
//            }
//            return response;
//        }

//        /// <summary>
//        /// 更新数据库   --事务
//        /// </summary>
//        /// <param name="manager">经理</param>
//        /// <param name="mark">关卡</param>
//        /// <param name="littleLevels">进度</param>
//        /// <param name="goals">进球数</param>
//        /// <param name="theGap">分差</param>
//        /// <param name="toConcede">失球数</param>
//        /// <param name="isGeneral">是否通关</param>
//        /// <param name="courage">奖励勇气值</param>
//        /// <param name="isVictory">是否胜利</param>
//        /// <param name="idx">游戏ID</param>
//        /// <returns></returns>
//        MessageCode SaveTourMatch( NbManagerEntity manager,int mark,int littleLevels,int goals,int theGap,int toConcede,bool isGeneral,int courage,bool isVictory,Guid idx)
//        {
//            MessageCode messageCode = MessageCode.NbParameterError;
//            if (manager == null )
//            {
//                return MessageCode.NbUpdateFail;
//            }
//            try
//            {
//                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
//                {
//                    transactionManager.BeginTransaction();
//                    messageCode = Tran_SaveTourMatch(transactionManager.TransactionObject, manager,mark,littleLevels,goals,theGap,toConcede,isGeneral,courage,isVictory,idx);

//                    if (messageCode == ShareUtil.SuccessCode)
//                    {
//                        transactionManager.Commit();
//                    }
//                    else
//                    {
//                        transactionManager.Rollback();
//                    }
//                }
//                if (messageCode == MessageCode.Success)
//                {
//                    RevelationHistoryofthegapEntity history = null;
//                    //获取最大分差信息

//                    if (_historyDic.ContainsKey(mark) && _historyDic[mark].ContainsKey(littleLevels))
//                        history = _historyDic[mark][littleLevels];

//                    //插入数据
//                    if (history == null)
//                    {
//                        RevelationHistoryofthegapEntity entity = new RevelationHistoryofthegapEntity();
//                        entity.CustomsPass = mark;
//                        entity.Goals = goals;
//                        entity.HistoryOfTheGap = theGap;
//                        entity.ManagerName = manager.Name;
//                        entity.Schedule = littleLevels;
//                        entity.States = 0;
//                        entity.ToConcede = toConcede;
//                        RevelationHistoryofthegapMgr.Insert(entity);
//                        if (_historyDic.ContainsKey(mark))
//                            _historyDic[mark].TryAdd(littleLevels, entity);
//                        else
//                        {
//                            if (_historyDic.ContainsKey(mark))
//                                _historyDic[mark].TryAdd(littleLevels, entity);
//                            else
//                            {
//                                var dic = new ConcurrentDictionary<int, RevelationHistoryofthegapEntity>();
//                                dic.TryAdd(littleLevels, entity);
//                                _historyDic.TryAdd(mark, dic);
//                            }
//                        }
//                    }
//                    else
//                    {
//                        //如果最大分差有更新
//                        if (theGap > history.HistoryOfTheGap)
//                        {
//                            history = RevelationHistoryofthegapMgr.GetById(mark, littleLevels);
//                            history.HistoryOfTheGap = theGap;
//                            history.Goals = goals;
//                            history.ToConcede = toConcede;
//                            history.ManagerName = manager.Name;
//                            RevelationHistoryofthegapMgr.Update(history);
//                            _historyDic[mark][littleLevels] = history;
//                        }
//                    }

//                    //获取自己最高记录
//                    RevelationMyhistoryofthegapEntity myHistory = null;
//                    if (_myHistoryDic.ContainsKey(manager.Idx.ToString() + mark) &&
//                        _myHistoryDic[manager.Idx.ToString() + mark].ContainsKey(littleLevels))
//                        myHistory = _myHistoryDic[manager.Idx.ToString() + mark][littleLevels];
//                    if (myHistory == null)
//                    {
//                        myHistory = new RevelationMyhistoryofthegapEntity();
//                        myHistory.Goals = goals;
//                        myHistory.HistoryOfTheGap = goals - toConcede;
//                        myHistory.ManagerId = manager.Idx;
//                        myHistory.Mark = mark;
//                        myHistory.Schedule = littleLevels;
//                        myHistory.ToConcede = toConcede;
//                        RevelationMyhistoryofthegapMgr.Insert(myHistory);
//                        if (_myHistoryDic.ContainsKey(manager.Idx.ToString() + mark))
//                        {
//                            _myHistoryDic[manager.Idx.ToString() + mark].TryAdd(littleLevels, myHistory);
//                        }
//                        else
//                        {
//                            if (_myHistoryDic.ContainsKey(manager.Idx.ToString() + mark))
//                                _myHistoryDic[manager.Idx.ToString() + mark].TryAdd(littleLevels, myHistory);
//                            else
//                            {
//                                var dic = new ConcurrentDictionary<int, RevelationMyhistoryofthegapEntity>();
//                                dic.TryAdd(littleLevels, myHistory);
//                                _myHistoryDic.TryAdd(manager.Idx.ToString() + mark, dic);
//                            }
//                        }
//                    }
//                    else
//                    {
//                        if (theGap > myHistory.HistoryOfTheGap)
//                        {
//                            myHistory = RevelationMyhistoryofthegapMgr.GetMyMarkHistoryOfTheGap(manager.Idx, mark,
//                                littleLevels);
//                            myHistory.Goals = goals;
//                            myHistory.HistoryOfTheGap = goals - toConcede;
//                            myHistory.ToConcede = toConcede;
//                            RevelationMyhistoryofthegapMgr.Update(myHistory);
//                            _myHistoryDic[manager.Idx.ToString() + mark][littleLevels] = myHistory;
//                        }
//                    }
//                }
//                return messageCode;
//            }
//            catch (Exception ex)
//            {
//                SystemlogMgr.Error("SaveTourMatch", ex);
//                return MessageCode.Exception;
//            }
//        }

//        /// <summary>
//        /// 更新数据库
//        /// </summary>
//        /// <param name="transaction">事务</param>
//        /// <param name="nbManager">经理</param>
//        /// <param name="mark">关卡</param>
//        /// <param name="littleLevels">进度</param>
//        /// <param name="goals">进球数</param>
//        /// <param name="theGap">分差</param>
//        /// <param name="toConcede">失球数</param>
//        /// <param name="isGeneral">是否通关</param>
//        /// <param name="courage">奖励勇气值</param>
//        /// <param name="isVictory">是否胜利</param>
//        /// <param name="idx">游戏ID</param>
//        /// <returns></returns>
//        private MessageCode Tran_SaveTourMatch(DbTransaction transaction, NbManagerEntity nbManager, int mark,
//            int littleLevels, int goals, int theGap, int toConcede, bool isGeneral, int courage, bool isVictory,
//            Guid idx)
//        {

//            if (!ManagerUtil.SaveManagerData(nbManager,null, transaction))
//            {
//                return MessageCode.NbUpdateFail;
//            }

//            //更改数据库
//            if (RevelationCheckpointMgr.C_RevelationTheGame(nbManager.Idx, mark, littleLevels, goals, toConcede,
//                isGeneral, courage, isVictory, transaction))
//            {
//                //TODO..
//                //if (NbManagerextraMgr.AddRevelateionChallenge(nbManager.Idx, transaction))
//                //{
//                    RevelationChallengerevordEntity entity = new RevelationChallengerevordEntity();
//                    entity.GameDate = DateTime.Now;
//                    entity.GameId = idx;
//                    entity.Goals = goals;
//                    entity.ManagerId = nbManager.Idx;
//                    entity.Mark = mark;
//                    entity.Schedule = littleLevels;
//                    entity.ToConcede = toConcede;
//                    RevelationChallengerevordMgr.Insert(entity);
//                    return MessageCode.Success;
//                //}
//            }
//            return MessageCode.NbParameterError;
//        }

//        #endregion

//        #region 增加挑战次数

//        /// <summary>
//        /// 增加挑战次数
//        /// </summary>
//        /// <param name="managerId"></param>
//        public RevelationAddChallengeResponse RevelationAddChallenge(Guid managerId)
//        {
//            RevelationAddChallengeResponse response = new RevelationAddChallengeResponse();
//            response.Data = new RevelationAddChallenge();
//            var manager = GetById(managerId);
//            //经理为空
//            if (manager == null)
//                return ResponseHelper.InvalidParameter<RevelationAddChallengeResponse>("manager is null");
//            //获取增加一次挑战次数需要点卷
//            int price = CacheFactory.RevelationCache.GetSunthesizeion((int)EnumRecelation.AddChallengePrice);
//            if (price == -1)
//                return ResponseHelper.InvalidParameter<RevelationAddChallengeResponse>("price is null");

//            //获取默认挑战次数
//            int challengeTime = CacheFactory.RevelationCache.GetSunthesizeion((int)EnumRecelation.ChallengeTime);
            
//            response.Data.Point = -1;
//            price = (manager.BuyTheNumber + 1) * price > 200 ? 200 : (manager.BuyTheNumber + 1) * price;
//            //获取点卷数量
//            int point = PayCore.Instance.GetPoint(managerId);
//            if (point < price)
//            {
//                response.Code = (int)MessageCode.NbPointShortage;
//                return response;
//            }

//            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
//            {
//                transactionManager.BeginTransaction();
//                response = AddChallenge(manager,point, price, challengeTime, transactionManager.TransactionObject);
//                if (response.Code == (int)MessageCode.Success)
//                    transactionManager.Commit();
//                else
//                    transactionManager.Rollback();
//            }
//            return response;
//        }

//        /// <summary>
//        /// 增加挑战次数
//        /// </summary>
//        /// <param name="manager"></param>
//        /// <param name="price"></param>
//        /// <param name="point"></param>
//        /// <param name="challengeTime"></param>
//        /// <returns></returns>
//        public RevelationAddChallengeResponse AddChallenge(RevelationMainEntity manager, int point, int price,
//            int challengeTime, DbTransaction tran)
//        {
//            RevelationAddChallengeResponse response = new RevelationAddChallengeResponse();
//            response.Data = new RevelationAddChallenge();
//            try
//            {
//                //扣除点卷
//                Guid idx = ShareUtil.GenerateComb();

//                var mall = new MallDirectFrame(manager.ManagerId, EnumConsumeSourceType.RevelationAddChallenge,
//                    manager.BuyTheNumber + 1);
//                MessageCode code = MessageCode.Success;
//                code = mall.Check();
//                if (code == MessageCode.Success)
//                {
//                    code = mall.Save(idx.ToString(), tran);
//                    if (code != MessageCode.Success)
//                    {
//                        response.Code = (int) code;
//                        return response;
//                    }
//                }
//                else
//                {
//                    response.Code = (int) code;
//                    return response;
//                }

//                //更新数据库
//                manager.BuyTheNumber = manager.BuyTheNumber + 1;
//                if (RevelationMainMgr.Update(manager, tran))
//                {
//                    //if (NbManagerextraMgr.BuyRevelationChallenge(manager.ManagerId,tran))
//                    //{
//                        response.Code = (int) MessageCode.Success;
//                        response.Data.ChallengesNums = manager.ChallengesNums;
//                        response.Data.Point = point - price;
//                        response.Data.BuyNumber = manager.BuyTheNumber;
//                        response.Data.SumChallengesNums = challengeTime + manager.BuyTheNumber;
//                        response.Code = (int) MessageCode.Success;
//                        return response;
//                    //}
//                }
//                response.Code = (int) MessageCode.NbParameterError;
//            }
//            catch (Exception ex)
//            {
//                response.Code = (int) MessageCode.NbParameterError;
//                SystemlogMgr.Error("球星启示录增加挑战次数", ex);
//            }
//            return response;
//        }

//        #endregion

//        #region 消除CD时间

//        /// <summary>
//        /// 消除CD时间
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="tran"></param>
//        /// <returns></returns>
//        public RevelationEliminateCDResponse RevelationEliminateCDTimes(Guid managerId)
//        {
//            //返回对象
//            RevelationEliminateCDResponse response = new RevelationEliminateCDResponse();
//            response.Data = new RevelationEliminateCD();
//            response.Data.Point = -1;
//            var manager = GetById(managerId);
//            if (manager == null)
//                return ResponseHelper.InvalidParameter<RevelationEliminateCDResponse>("manager is null");
//            //获取CD时间
//            int cdTime = CacheFactory.RevelationCache.GetSunthesizeion((int)EnumRecelation.HangUpCDTimes);
//            if (manager.OnHookCD.GetSpanTime(DateTime.Now) >= cdTime)
//            {
//                response.Code = (int)MessageCode.RevelationNotEliminateCD;
//                return response;
//            }
//            //获取消除CD时间价格
//            int price = CacheFactory.RevelationCache.GetSunthesizeion((int)EnumRecelation.HangUpCDPrice);
//            //获取点卷数量
//            int point = PayCore.Instance.GetPoint(managerId);
//            if (point < price)
//            {
//                response.Code = (int)MessageCode.NbPointShortage;
//                return response;
//            }

//            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
//            {
//                transactionManager.BeginTransaction();
//                response = EliminateCDTimes(manager,price, transactionManager.TransactionObject);
//                if (response.Code == (int) MessageCode.Success)
//                {
//                    transactionManager.Commit();
//                    response.Data.Point = point - price;
//                }
//                else
//                    transactionManager.Rollback();
//            }
//            return response;
//        }

//        /// <summary>
//        /// 消除CD时间
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="tran"></param>
//        /// <returns></returns>
//        public RevelationEliminateCDResponse EliminateCDTimes(RevelationMainEntity manager,int price, DbTransaction tran) 
//        {
//            //返回对象
//            RevelationEliminateCDResponse response = new RevelationEliminateCDResponse();
//            response.Data = new RevelationEliminateCD();
//            try
//            {
               
//                //扣除点卷
//                Guid idx = ShareUtil.GenerateComb();
//                MessageCode messCode = PayCore.Instance.GambleConsume(manager.ManagerId, price, idx,EnumConsumeSourceType.RevelationAddChallenge, tran);
//                if (messCode != MessageCode.Success)
//                {
//                    response.Code = (int)MessageCode.NbParameterError;
//                    SystemlogMgr.Error("球星启示录消除CD时间扣点卷出错", "消除CD时间");
//                    return response;
//                }
//                //更新数据库
//                manager.OnHookCD = DateTime.Now.AddMinutes(-60);
//                if (RevelationMainMgr.Update(manager, tran))
//                {
//                    response.Code = (int) MessageCode.Success;
//                }
//                else
//                    response.Code = (int) MessageCode.NbParameterError;
//            }
//            catch (Exception ex) 
//            {
//                response.Code = (int)MessageCode.NbParameterError;
//                SystemlogMgr.Error("球星启示录消除CD时间", ex);
//            }
//            return response;
//        }

//        #endregion

//        /// <summary>
//        /// 领取通关奖励
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="mark"></param>
//        /// <returns></returns>
//        public RevelationReceiveAwaryResponse RevelationReceiveGeneralAwary(Guid managerId, int mark) 
//        {
//            //返回对象
//            RevelationReceiveAwaryResponse response = new RevelationReceiveAwaryResponse();
//            response.Data = new RevelationReceiveAwary();
//            response.Data.GetTheSuccess = false;
//            try
//            {
//                var manager = GetById(managerId);
//                 var nbmanager = NbManagerMgr.GetById(managerId);
//                if (manager == null || nbmanager == null)
//                    return ResponseHelper.InvalidParameter<RevelationReceiveAwaryResponse>("manager is null");
               
//                //获取关卡信息
//                var checkPoint = RevelationCheckpointMgr.C_RevelationGetCheckoint(managerId, mark);
//                if (checkPoint == null)
//                {
//                    response.Code = (int)MessageCode.RevelationNotFountCheckoint;
//                    return response;
//                }
//                //未通关
//                if (!checkPoint.IsGeneral)
//                {
//                    response.Code = (int)MessageCode.RevelationNotGeneral;
//                    return response;
//                }
//                //已经领取
//                if (checkPoint.IsGeneralAwary)
//                {
//                    response.Code = (int) MessageCode.AlreadyPullDown;
//                    return response;
//                }
//                //获取通关奖励配置
//                var entity = CacheFactory.RevelationCache.GetGeneralAwary(mark);
//                if (entity == null)
//                {
//                    response.Code = (int)MessageCode.NbParameterError;
//                    SystemlogMgr.Error("球星启示录通关奖励配置错误", "球星启示录通关奖励");
//                    return response;
//                }
               
               
//                //更新数据库
//                checkPoint.IsGeneralAwary = true;
//                checkPoint.AwaryItem = entity.TheBadgeID.ToString();
//                checkPoint.GeneralAwaryTime = DateTime.Now;
//                 using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
//            {
//                transactionManager.BeginTransaction();
//                MessageCode messCode = ReceiveGeneralAwary(managerId,entity.TheBadgeID,1,true,true,checkPoint, transactionManager.TransactionObject);
//                if (messCode == (int) MessageCode.Success)
//                {
//                    transactionManager.Commit();
//                    response.Code = (int) MessageCode.Success;
//                    response.Data.AwaryItem = entity.TheBadgeID;
//                    response.Data.GetTheSuccess = true;
//                }
//                else
//                {
//                    transactionManager.Rollback();
//                    response.Code = (int)messCode;
//                }
//            }
                
              
//            }
//            catch (Exception ex)
//            {
//                response.Code = (int)MessageCode.NbParameterError;
//                SystemlogMgr.Error("球星启示录领取通关奖励",ex);
//            }
//            return response;
//        }

//        public MessageCode ReceiveGeneralAwary(Guid managerId, int itemCode, int itemNumber, bool isBind,bool isDeal,
//            RevelationCheckpointEntity entity,DbTransaction tran)
//        {
//             //向背包加物品
//            MessageCode code = AddItems(managerId, itemCode, itemNumber, 0, isBind, isDeal, tran);
//            if (code != MessageCode.Success)
//                return code;
//            if (!RevelationCheckpointMgr.Update(entity, tran))
//                return MessageCode.NbParameterError;
//            return MessageCode.Success;
//        }

//        /// <summary>
//        /// 根据经理ID获取所有通关的关卡信息
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <returns></returns>
//        public RevelationGetByGeneralResponse RevelationGetByGeneral(Guid managerId)
//        {
//            //返回对象
//            RevelationGetByGeneralResponse response = new RevelationGetByGeneralResponse();
//            response.Data = new RevelationGetByGeneral();
//            //获取经理信息
//            var manager = GetById(managerId);
//            if(manager == null)
//                return ResponseHelper.InvalidParameter<RevelationGetByGeneralResponse>("manager is null");
//            //获取关卡信息
//            List<RevelationCheckpointEntity> list = RevelationCheckpointMgr.C_RevelationCountGenerl(managerId);

//            response.Data.Dic = new Dictionary<int, int>();
//            foreach (var item in list)
//            {
//                response.Data.Dic.Add(item.CustomsPass, item.IsGeneral ? 1 : 0);
//            }
//            return response;
//        }

//        #region 勇气商城兑换物品

//        /// <summary>
//        /// 勇气商城兑换物品
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="itemCode"></param>
//        /// <param name="count"></param>
//        /// <returns></returns>
//        public RevelationPurchaseItemsResponse RevelationPurchaseItems(Guid managerId, int count, int itemCode)
//        {
//            RevelationPurchaseItemsResponse response = new RevelationPurchaseItemsResponse();
//            response.Data = new RevelationPurchaseItems();

//            var manager = GetById(managerId);
//            //获取主表经理信息
//            var nbManager = NbManagerMgr.GetById(managerId);
//            if (manager == null || nbManager == null)
//                return ResponseHelper.InvalidParameter<RevelationPurchaseItemsResponse>("manager is null");
//            var shop = CacheFactory.RevelationCache.GetShop(itemCode);
//            if (shop == null)
//                return ResponseHelper.InvalidParameter<RevelationPurchaseItemsResponse>("shop is null");
//            //有通关要求
//            if (shop.PassportControl != 0)
//            {
//                var checkPoint = RevelationCheckpointMgr.C_RevelationGetCheckoint(managerId, shop.PassportControl);
//                //未通关
//                if (checkPoint == null || checkPoint.IsGeneral == false)
//                {
//                    response.Code = (int) MessageCode.RevelationNotGeneral;
//                    return response;
//                }
//            }
//            if (shop.ManagerLevel != 0)
//            {
//                if(nbManager.Level < shop.ManagerLevel)
//                    response.Code = (int)MessageCode.LackofManagerLevel;
//            }
//            //价格
//            int price = shop.Price*count;
//            //勇气值不够
//            if (manager.Courage < price)
//            {
//                response.Code = (int)MessageCode.RevelationCourageNot;
//                return response;
//            }

//            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
//            {
//                transactionManager.BeginTransaction();
//                response = PurchaseItems(manager, itemCode, count, nbManager.VipLevel, price, transactionManager.TransactionObject);
//                if (response.Code == (int)MessageCode.Success)
//                    transactionManager.Commit();
//                else
//                    transactionManager.Rollback();
//            }
//            return response;
//        }

//        /// <summary>
//        /// 勇气商城兑换物品
//        /// </summary>
//        /// <param name="manager"></param>
//        /// <param name="itemCode"></param>
//        /// <param name="tran"></param>
//        /// <param name="count"></param>
//        /// <param name="vipLevel"></param>
//        /// <param name="price"></param>
//        /// <returns></returns>
//        public RevelationPurchaseItemsResponse PurchaseItems(RevelationMainEntity manager, int itemCode, int count, int vipLevel, int price, DbTransaction tran)
//        {
//            //返回对象
//            RevelationPurchaseItemsResponse response = new RevelationPurchaseItemsResponse();
//            response.Data = new RevelationPurchaseItems();
//            response.Data.Courage = -1;
//            try
//            {
//                //获取物品是否绑定配置
//                var vip = CacheFactory.RevelationCache.GetVipPrivilege(vipLevel);
//                //口勇气值
//                manager.Courage = manager.Courage - price;
//                if (RevelationMainMgr.Update(manager, tran))
//                {
//                    //TODO.. 加物品
//                    MessageCode code = AddItems(manager.ManagerId, itemCode, count, 0, vip.ItemIsBind, false, tran);
//                    response.Data.GetTheSuccess = true;
//                    response.Data.Courage = manager.Courage;
//                    response.Data.AwaryItem = itemCode;
//                    response.Data.ItemCount = count;
//                    response.Code = (int)code;
//                }
//                else
//                {
//                    response.Code = (int)MessageCode.NbParameterError;
//                    SystemlogMgr.Error("勇气商店兑换物品", "扣勇气值出错");
//                }
//            }
//            catch (Exception ex)
//            {
//                response.Code = (int)MessageCode.NbParameterError;
//                SystemlogMgr.Error("勇气商店兑换物品", ex);
//            }
//            return response;
//        }

//        #endregion

//        #region 挂机

//        /// <summary>
//        /// 挂机
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="mark"></param>
//        /// <returns></returns>
//        public RevelationOutboardMotorResponse RevelationOutboardMotor(Guid managerId,int mark)
//        {
//            RevelationOutboardMotorResponse response = new RevelationOutboardMotorResponse();
//            response.Data = new RevelationOutboardMotor();


//            return response;
//        }

//        /// <summary>
//        /// 计算挂机数据
//        /// </summary>
//        /// <param name="managerId"></param>
//        /// <param name="mark"></param>
//        /// <returns></returns>
//        public RevelationCalculateOutboardMotorResponse RevelationCalculateOutboardMotor(Guid managerId,int mark)
//        {
//            RevelationCalculateOutboardMotorResponse response = new RevelationCalculateOutboardMotorResponse();
//            response.Data = new RevelationCalculateOutboardMotor();


//            return response;
//        }

//        #endregion
//    }
//}
