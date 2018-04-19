using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.League;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Online;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Revelation;
using MsEntLibWrapper.Data;
using Games.NBall.Core.Item;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Core.Match;


namespace Games.NBall.Core.Revelation
{
    /// <summary>
    /// 球星启示录主表 处理客户端的请求
    /// </summary>
    public class RevelationNewCore
    {
        private static RevelationNewCore instance;
        private static object __LOCK__ = new object();

        /// <summary>
        /// 关卡霸主
        /// </summary>
        private static ConcurrentDictionary<int, ConcurrentDictionary<int, RevelationHistoryofthegapEntity>> _historyDic;


        private RevelationNewCore()
        {
            _historyDic = new ConcurrentDictionary<int, ConcurrentDictionary<int, RevelationHistoryofthegapEntity>>();
        }

        /// <summary>
        /// 静态构造方法 返回本类的实例
        /// </summary>
        public static RevelationNewCore Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (__LOCK__)
                    {
                        if (instance == null)
                            instance = new RevelationNewCore();
                    }
                }
                return instance;
            }
        }
        #region 关卡霸主

        /// <summary>
        /// 获取关卡霸主
        /// </summary>
        /// <param name="markId"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        RevelationHistoryofthegapEntity GetHistoryOfTheGap(int markId, int schedule)
        {
            if (_historyDic.ContainsKey(markId))
            {
                var markList = _historyDic[markId];
                if (markList.Count > 0)
                {
                    if (markList.ContainsKey(schedule))
                        return markList[schedule];
                }
                return null;
            }
            ConcurrentDictionary<int, RevelationHistoryofthegapEntity> dic = new ConcurrentDictionary<int, RevelationHistoryofthegapEntity>();

            var list = RevelationHistoryofthegapMgr.C_RevelationHistoryOfTheGapGetId(markId);
            foreach (var item in list)
            {
                dic.TryAdd(item.Schedule, item);
            }
            _historyDic.TryAdd(markId, dic);
            if (dic.ContainsKey(schedule))
                return dic[schedule];
            return null;
        }

        /// <summary>
        /// 设置关卡霸主
        /// </summary>
        /// <param name="markId"></param>
        /// <param name="schedule"></param>
        /// <param name="goals"></param>
        /// <param name="toConcede"></param>
        /// <param name="managerName"></param>
        private void SetHistoryOfTheGap(int markId, int schedule, int goals, int toConcede,string managerName)
        {
            if (goals - toConcede <= 0)
                return;
            RevelationHistoryofthegapEntity historyInfo = null;
            if (_historyDic.ContainsKey(markId))
            {
                var markList = _historyDic[markId];
                if (markList.Count > 0)
                {
                    if (markList.ContainsKey(schedule))
                        historyInfo = markList[schedule];
                }
                if (historyInfo == null)
                {
                    var entity = InsertHistoryOfTheGap(markId, schedule, managerName, goals, toConcede);
                    markList.TryAdd(schedule, entity);
                }
                else
                {
                    if ((historyInfo.Goals - historyInfo.ToConcede) < (goals - toConcede))
                    {
                        var entity = UpdateHistoryOfTheGap(historyInfo, managerName, goals, toConcede);
                        markList[schedule] = entity;
                    }
                }
                //_historyDic[markId] = markList;
            }
            else
            {
                ConcurrentDictionary<int, RevelationHistoryofthegapEntity> dic =
                    new ConcurrentDictionary<int, RevelationHistoryofthegapEntity>();

                var list = RevelationHistoryofthegapMgr.C_RevelationHistoryOfTheGapGetId(markId);

                foreach (var item in list)
                {
                    dic.TryAdd(item.Schedule, item);
                }
                if (dic.Count == 0)
                {
                    var entity = InsertHistoryOfTheGap(markId, schedule, managerName, goals, toConcede);
                    dic.TryAdd(schedule, entity);
                    _historyDic.TryAdd(markId, dic);
                    return;
                }
                if (dic.ContainsKey(schedule))
                    historyInfo = dic[schedule];
                if (historyInfo == null)
                {
                    var entity = InsertHistoryOfTheGap(markId, schedule, managerName, goals, toConcede);
                    dic.TryAdd(schedule, entity);
                }
                else
                {
                    if ((historyInfo.Goals - historyInfo.ToConcede) < (goals - toConcede))
                    {
                        var entity = UpdateHistoryOfTheGap(historyInfo, managerName, goals, toConcede);
                        dic[schedule] = entity;
                    }
                }

                _historyDic.TryAdd(markId, dic);
            }
        }

        private RevelationHistoryofthegapEntity InsertHistoryOfTheGap(int markId, int schedule, string managerName,
            int goals, int toConcede)
        {
            var entity = new RevelationHistoryofthegapEntity(markId, schedule, managerName, goals, toConcede,
                        goals - toConcede, 0, DateTime.Now);
            RevelationHistoryofthegapMgr.Insert(entity);
            return entity;
        }

        private RevelationHistoryofthegapEntity UpdateHistoryOfTheGap(RevelationHistoryofthegapEntity entity, string managerName,
           int goals, int toConcede)
        {
            entity.Goals = goals;
            entity.ToConcede = toConcede;
            entity.ManagerName = managerName;
            RevelationHistoryofthegapMgr.Update(entity);
            return entity;
        }

        #endregion

        /// <summary>
        /// 获取球星启示录经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationInfoEntity GetById(Guid managerId)
        {
            var entity = RevelationInfoMgr.GetById(managerId);
            DateTime date = DateTime.Now;
            if (entity == null)
            {
                entity = new RevelationInfoEntity(managerId,0, 1, 0, 0,false, "", "", "", 0, false, Guid.Empty, false,
                    Guid.Empty, date, 0, date, date);
                RevelationInfoMgr.Insert(entity);
            }
            else
            {
                if (entity.RefreshData != date.Date)
                {
                    entity.DayMatchLog = "";
                    entity.RefreshData = date;
                    entity.UpdateTime = date;
                    entity.DrawId = Guid.Empty;
                    entity.HookId = Guid.Empty;
                    entity.IsHaveDraw = false;
                    entity.IsHook = false;
                    entity.PassLog = "";
                    entity.PresentMark = 0;
                    entity.Schedule = 0;
                    entity.IsGeneralMark = false;
                    RevelationInfoMgr.Update(entity);
                }
            }
            return entity;
        }

        #region 获取是否有正在比赛的关卡和关卡解锁情况

        /// <summary>
        /// 根据经理ID获取经理信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetManagerResponse RevelationGetManagerId(Guid managerId)
        {
            //返回对象
            RevelationGetManagerResponse response = new RevelationGetManagerResponse();
            response.Data = new RevelationGetManager();
            try
            {
                //获取经理信息
                var info = GetById(managerId);
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (info == null || manager == null)
                    return ResponseHelper.Create<RevelationGetManagerResponse>(MessageCode.AdMissManager);
                if (info.PresentMark != 0)
                {
                    response.Data.IsMatchMark = true;
                    response.Data.MarchMarkId = info.PresentMark;
                }
                if (info.IsHaveDraw)
                {
                    response.Data.IsDraw = true;
                    response.Data.DrawId = info.DrawId;
                }
                if (info.IsHook)
                {
                    response.Data.IsHook = true;
                    response.Data.HookId = info.HookId;
                }
                response.Data.LockMark = info.LockMark;
                response.Data.MarkInfo = GetPassMarkInfo(info.DayMatchLog, manager.VipLevel,info.LockMark);
            }
            catch (Exception ex)
            {
                response.Code = (int) MessageCode.NbParameterError;
                SystemlogMgr.Error("球星启示录获取经理信息", ex);
            }
            return response;
        }

        /// <summary>
        /// 通关记录
        /// </summary>
        /// <param name="markLog"></param>
        /// <param name="vipLevel"></param>
        /// <param name="lockMark"></param>
        /// <returns></returns>
        private List<RevelationMarkNumberEntity> GetPassMarkInfo(string markLog, int vipLevel, int lockMark)
        {
            var result = new List<RevelationMarkNumberEntity>();
            var maxCount = CacheFactory.VipdicCache.GetEffectValue(vipLevel,
                (int) EnumVipEffect.RevelationPassMarkNumber);
            int markId = 0;
            if (markLog.Length > 0)
            {
                var marks = markLog.Split(',');
                foreach (var item in marks)
                {
                    markId++;
                    var entity = new RevelationMarkNumberEntity();
                    entity.MarkId = markId;
                    entity.SumNumber = maxCount;
                    entity.UseNumber = ConvertHelper.ConvertToInt(item);
                    result.Add(entity);
                }
            }
            while (markId < lockMark)
            {
                markId++;
                var entity = new RevelationMarkNumberEntity();
                entity.MarkId = markId;
                entity.SumNumber = maxCount;
                entity.UseNumber = 0;
                result.Add(entity);
            }
            return result;
        }

        #endregion

        #region 开始一个大关卡

        /// <summary>
        /// 开始一个大关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="markId"></param>
        /// <returns></returns>
        public RevelationStartMarkResponse StartMark(Guid managerId, int markId)
        {
            var response = new RevelationStartMarkResponse();
            response.Data = new RevelationStartMark();
            try
            {
                var info = GetById(managerId);
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return ResponseHelper.Create<RevelationStartMarkResponse>(MessageCode.AdMissManager);
                if (info.LockMark < markId) //未解锁
                    return ResponseHelper.Create<RevelationStartMarkResponse>(MessageCode.RevelationNotLock);
                if (info.PresentMark > 0) //还有未结束的关卡
                    return ResponseHelper.Create<RevelationStartMarkResponse>(MessageCode.RevelationNotEndMark);
                var passLogList = GetPassMarkInfo(info.DayMatchLog, manager.VipLevel, info.LockMark); //当天的通关记录
                if (passLogList.Count == 0 && markId != 1)
                    return ResponseHelper.Create<RevelationStartMarkResponse>(MessageCode.RevelationChallengeUseUP);
                var passLogInfo = passLogList.Find(r => r.MarkId == markId);
                if (passLogInfo == null || passLogInfo.UseNumber >= passLogInfo.SumNumber)
                    return ResponseHelper.Create<RevelationStartMarkResponse>(MessageCode.RevelationChallengeUseUP);
                passLogInfo.UseNumber ++;
                info.DayMatchLog = ListToString(passLogList);
                info.Morale = CacheFactory.RevelationCache.Morale;
                info.PresentMark = markId;
                info.Schedule = 1;
                info.UpdateTime = DateTime.Now;
                if (!RevelationInfoMgr.Update(info))
                    return ResponseHelper.Create<RevelationStartMarkResponse>(MessageCode.NbUpdateFail);
                response.Data.MarkInfo = GetMarkInfo(info);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("球星启示录开始一个关卡", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        public string ListToString(List<RevelationMarkNumberEntity> list)
        {
            var str = new StringBuilder();
            foreach (var item in list)
            {
                str.Append(item.UseNumber);
                str.Append(",");
            }
            if (str.Length > 0)
                return str.ToString().Substring(0, str.Length - 1);
            return str.ToString();
        }

        public bool IsHaveNumber(string markLog, int vipLevel,int lockMark, int markId)
        {
            var list = GetPassMarkInfo(markLog, vipLevel, lockMark);
            if (list.Count == 0 && markId == 1)
                return true;
            var info = list.Find(r => r.MarkId == markId);
            if (info == null)
                return false;
            return info.UseNumber < info.SumNumber;
        }

        #endregion

        #region 获取关卡信息

        /// <summary>
        /// 根据关卡获取关卡信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetIsGeneralResponse GetMarkInfo(Guid managerId)
        {
            //返回对象
            RevelationGetIsGeneralResponse response = new RevelationGetIsGeneralResponse();
            response.Data = new RevelationGetIsGeneral();
            try
            {
                var info = GetById(managerId);
                //未找到经理
                if (info == null)
                    return ResponseHelper.Create<RevelationGetIsGeneralResponse>(MessageCode.AdMissManager);
                if (info.PresentMark == 0)
                    return ResponseHelper.Create<RevelationGetIsGeneralResponse>(MessageCode.RevelationMarkNotStart);
                response.Data = GetMarkInfo(info);
            }
            catch (Exception ex) 
            {
                response.Code = (int)MessageCode.NbParameterError;
                SystemlogMgr.Error("球星启示录获取关卡信息", ex);
            }
            return response;
        }

        public RevelationGetIsGeneral GetMarkInfo(RevelationInfoEntity info)
        {
            var result = new RevelationGetIsGeneral();
            result.MarkId = info.PresentMark;
            result.Schedule = info.Schedule;
            result.Morale = info.Morale;
            //有翻牌机会
            if (info.IsHaveDraw)
            {
                result.IsDraw = true;
                result.DrawId = info.DrawId;
            }
            if (info.IsHook)
            {
                result.IsHook = true;
                result.HookId = info.HookId;
            }
            result.IsGeneral = info.IsGeneralMark;
            result.IsCanChallenge = !info.IsGeneralMark;

            var history = GetHistoryOfTheGap(info.PresentMark, info.Schedule);
            if (history != null)
            {
                result.MarkDominateName = history.ManagerName;
                result.MarkDominateGoals = history.Goals + ":" + history.ToConcede;
            }
            var myhistoryFrame = new RevelationHistoryFrame(info.ManagerId);
            var myHistory = myhistoryFrame.GetGoals(info.PresentMark, info.Schedule);
            if (myHistory != null)
            {
                result.MyGoalsLog = myHistory.Goals + ":" + myHistory.ToConcede;
            }
            return result;
        }

        #endregion

        #region 比赛

        /// <summary>
        /// 打比赛
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <returns></returns>
        public RevelationTheGameResponse RevelationTheGame(Guid managerId)
        {
            //返回对象
            RevelationTheGameResponse response = new RevelationTheGameResponse();
            response.Data = new RevelationTheGameEntity();
            try
            {
                #region 获取数据

                //获取主表经理信息
                var manager = NbManagerMgr.GetById(managerId);
                //获取启示录经理信息
                var info = GetById(managerId);
                if (manager == null || info == null)
                    return ResponseHelper.Create<RevelationTheGameResponse>(MessageCode.AdMissManager);
                //在挂机
                if (info.IsHook)
                    return ResponseHelper.Create<RevelationTheGameResponse>(MessageCode.RevelationIsHook);
                if(info.IsGeneralMark)//通关了
                    return ResponseHelper.Create<RevelationTheGameResponse>(MessageCode.RevelationGeneralMark);
                if (info.PresentMark == 0 || info.Schedule == 0)//还未开始关卡
                    return ResponseHelper.Create<RevelationTheGameResponse>(MessageCode.NbParameterError);
                if(info.Morale<=0)//士气不足
                    return ResponseHelper.Create<RevelationTheGameResponse>(MessageCode.RevelationMorale);
                //获取关卡配置
                var markConfig = CacheFactory.RevelationCache.GetMarkInfo(info.PresentMark, info.Schedule);
                if (markConfig == null)
                    return ResponseHelper.Create<RevelationTheGameResponse>(MessageCode.NbParameterError);
                #endregion

                int goals = 0; //进球数
                int toConcede = 0; //失球数

                #region 比赛

                //构建主队
                var matchHome = new MatchManagerInfo(managerId, false, false);
                //构建客队
                var matchAway = new MatchManagerInfo(markConfig.NpcId, true, false);
                //创建一场比赛
                Guid idx = ShareUtil.GenerateComb();
                var matchData = new BaseMatchData((int)EnumMatchType.Revelation, idx, matchHome, matchAway);
                matchData.ErrorCode = (int)MessageCode.MatchWait;

                MemcachedFactory.MatchClient.Set(matchData.MatchId, matchData);
                MatchCore.CreateMatch(matchData);
                response.Code = matchData.ErrorCode;
                if (matchData.ErrorCode != (int)MessageCode.Success)
                {
                    response.Code = (int)matchData.ErrorCode;
                    return response;
                }
                goals = matchData.Home.Score;
                toConcede = matchData.Away.Score;
                #endregion

                bool isGeneral = false;
                bool isVictory = false;
                int addCourage = 0;
                RevelationDrawEntity drawEntity=null;
                RevelationHistoryFrame frame = null;
                int markId = info.PresentMark;
                int schedule = info.Schedule;
                bool isFirstPass = false;//首次通关
                //比赛结果处理
                var messageCode = MatchDispose(manager, info, markConfig, goals, toConcede, ref isGeneral, ref isVictory, ref addCourage, ref isFirstPass,
                    ref drawEntity, ref frame);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<RevelationTheGameResponse>(messageCode);
                MailBuilder mail = null;
                //首次通关奖励
                if (isFirstPass)
                {
                    var items = markConfig.FirstPassItem.Split(',');
                    if (items.Length <= 1)
                        return ResponseHelper.Create<RevelationTheGameResponse>(MessageCode.NbParameterError);
                    mail = new MailBuilder(managerId, ConvertHelper.ConvertToInt(items[0]),
                        ConvertHelper.ConvertToInt(items[1]), false, 1,markConfig.MarkPlayer);
                }
                messageCode = SaveTourMatch(info, drawEntity, frame,null,mail);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<RevelationTheGameResponse>(messageCode);
                //更新霸主
                if (goals - toConcede > 0)
                {
                    SetHistoryOfTheGap(markId, schedule, goals, toConcede, manager.Name);
                }
                response.Data.Courage = addCourage;
                if (drawEntity != null)
                {
                    response.Data.IsDraw = true;
                    response.Data.DrawId = drawEntity.DrawId;
                }
                response.Data.Goals = goals;
                response.Data.IsGeneral = isGeneral;
                response.Data.MatchId = idx;
                response.Data.Moare = info.Morale;
                response.Data.Schedule = info.Schedule;
                response.Data.ToConcede = toConcede;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("球星启示录过关卡失败", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 比赛处理
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="info"></param>
        /// <param name="markConfig"></param>
        /// <param name="goals"></param>
        /// <param name="toConcede"></param>
        /// <param name="isGeneral"></param>
        /// <param name="isVictory"></param>
        /// <param name="addCourage"></param>
        /// <param name="isFirstPass"></param>
        /// <param name="drawEntity"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        public MessageCode MatchDispose(NbManagerEntity manager,RevelationInfoEntity info, ConfigRevelationEntity markConfig, int goals, int toConcede,
            ref bool isGeneral, ref bool isVictory, ref int addCourage, ref bool isFirstPass, ref RevelationDrawEntity drawEntity, ref RevelationHistoryFrame frame)
        {
            int theGap = goals - toConcede; //分差
            if (theGap > 0) //赢了比赛
            {
                isVictory = true;
                addCourage = markConfig.CourageNumber;
                //获取配置  是否通关
                isGeneral = CacheFactory.RevelationCache.GetIsGeneral(info.PresentMark, info.Schedule);

                #region 生成抽卡信息

                var drawString = CacheFactory.RevelationCache.GetDrawString();
                if (drawString.Length == 0)
                    return MessageCode.NbParameterError;
                drawEntity = new RevelationDrawEntity(ShareUtil.GenerateComb(), info.ManagerId, info.PresentMark, info.Schedule, drawString, "", 0,
                    0,
                    DateTime.Now, DateTime.Now);

                #endregion

                #region 防沉迷

                //防沉迷
                int exp = 0;
                int gold = 0;
                OnlineCore.Instance.CalIndulgePrize(manager, ref exp, ref gold, ref addCourage);

                #endregion

                info.Courage += addCourage;
                info.DrawId = drawEntity.DrawId;
                info.IsHaveDraw = true;
                info.UpdateTime = DateTime.Now;
                #region 更新记录

                frame = new RevelationHistoryFrame(info.ManagerId);
                frame.SetGoals(info.PresentMark, info.Schedule, goals, toConcede);

                #endregion

                //通关了
                if (isGeneral)
                {
                    //每日通关
                   // info.DayMatchLog = SetMatchLog(info.DayMatchLog, info.PresentMark);
                    //首次通关
                    info.FirstPassLog = SetFirstPassLog(info.FirstPassLog, info.PresentMark, ref isFirstPass);
                    var lockMark = CacheFactory.RevelationCache.NextLockMark(info.PresentMark);
                    if (lockMark > info.LockMark)
                        info.LockMark = lockMark;
                    #region 重置

                    info.Morale = 0;
                    info.PresentMark = 0;
                    info.Schedule = 0;

                    #endregion
                }
                else
                {
                    info.Schedule++;
                }

            }
            else//打输了 
            {
                info.Morale = info.Morale - CacheFactory.RevelationCache.SubtractMorale(theGap);
                if (info.Morale < 0)
                    info.Morale = 0;
                info.UpdateTime = DateTime.Now;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 设置通关记录
        /// </summary>
        /// <param name="dayMatchLog"></param>
        /// <param name="markId"></param>
        /// <returns></returns>
        public string SetMatchLog(string dayMatchLog, int markId)
        {
            var marks = dayMatchLog.Split(',');
            var result = "";
            for (int i = 0; i < marks.Length; i++)
            {
                if (markId == i + 1)
                {
                    var number = ConvertHelper.ConvertToInt(marks[i]) + 1;
                    marks[i] = number.ToString();
                }
                result += marks[i] + ",";
            }
            if (marks.Length < markId)
                result += "1,";
            if (result.Length > 0)
                result = result.Substring(0, result.Length - 1);
            return result;
        }

        /// <summary>
        /// 设置首次通关记录
        /// </summary>
        /// <param name="firstPassLog"></param>
        /// <param name="markId"></param>
        /// <param name="isFirstPass"></param>
        /// <returns></returns>
        public string SetFirstPassLog(string firstPassLog, int markId, ref bool isFirstPass)
        {
            if (firstPassLog.Length < markId)
            {
                isFirstPass = true;
                return firstPassLog + "1";
            }
            isFirstPass = false;
            return firstPassLog;
        }

        /// <summary>
        /// 更新数据库   --事务
        /// </summary>
        /// <param name="info"></param>
        /// <param name="draw"></param>
        /// <param name="frame"></param>
        /// <param name="package"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        MessageCode SaveTourMatch(RevelationInfoEntity info,RevelationDrawEntity draw,RevelationHistoryFrame frame,ItemPackageFrame package,MailBuilder mail)
        {
            MessageCode messageCode = MessageCode.NbParameterError;
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    messageCode = Tran_SaveTourMatch(transactionManager.TransactionObject, info, draw, frame, package, mail);

                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                }
                return messageCode;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveTourMatch", ex);
                return MessageCode.Exception;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="info"></param>
        /// <param name="draw"></param>
        /// <param name="frame"></param>
        /// <param name="package"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        private MessageCode Tran_SaveTourMatch(DbTransaction transaction, RevelationInfoEntity info, RevelationDrawEntity draw, RevelationHistoryFrame frame, ItemPackageFrame package, MailBuilder mail)
        {
            if (!RevelationInfoMgr.Update(info, transaction))
                return MessageCode.NbUpdateFail;
            if (draw != null)
            {
                if (!RevelationDrawMgr.Insert(draw, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (frame != null)
            {
                if (!frame.Save(transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (package != null)
            {
                if (!package.Save(transaction))
                    return MessageCode.NbUpdateFail;
                package.Shadow.Save();
            }
            if (mail != null)
            {
                if (!mail.Save(transaction))
                    return MessageCode.NbUpdateFail;
            }
            return MessageCode.Success;
        }

        #endregion

        #region 翻牌

        /// <summary>
        /// 获取翻牌信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        public RevelationDrawResponse GetDrawInfo(Guid managerId, Guid drawId)
        {
            var response = new RevelationDrawResponse();
            response.Data = new RevelationDrawEntity();
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                var drawInfo = RevelationDrawMgr.GetById(drawId);
                if (drawInfo == null || manager ==null)
                    return ResponseHelper.Create<RevelationDrawResponse>(MessageCode.NbParameterError);

                drawInfo.Price = CacheFactory.RevelationCache.GetDrawGoldBar(drawInfo.OpenNumber, manager.VipLevel);
                response.Data = drawInfo;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("球星启示录获取翻牌信息", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 翻牌结果
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="drawId"></param>
        /// <returns></returns>
        public RevelationDrawResultResponse DrawResult(Guid managerId, Guid drawId)
        {
            var response = new RevelationDrawResultResponse();
            response.Data = new RevelationDrawResult();
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                var drawInfo = RevelationDrawMgr.GetById(drawId);
                if (drawInfo == null || manager == null || drawInfo.OpenNumber >= CacheFactory.RevelationCache.DrawNumber)
                    return ResponseHelper.Create<RevelationDrawResultResponse>(MessageCode.NbParameterError);
                drawInfo.Price = CacheFactory.RevelationCache.GetDrawGoldBar(drawInfo.OpenNumber, manager.VipLevel);
                ScoutingGoldbarEntity goldbar = null;
                if (drawInfo.Price > 0)
                {
                    goldbar = ScoutingGoldbarMgr.GetById(managerId);
                    if (goldbar == null || goldbar.GoldBarNumber < drawInfo.Price)
                        return ResponseHelper.Create<RevelationDrawResultResponse>(MessageCode.ScoutingGoldBarNot);
                }
                string newItemstring = "";
                string prizeItemString = drawInfo.PrizeItemString;
                //抽到的奖励ID
                var drawIdx = GetDrawId(drawInfo.AllItemString, ref newItemstring, ref prizeItemString);
                if (drawIdx == 0)
                    return ResponseHelper.Create<RevelationDrawResultResponse>(MessageCode.NbParameterError);
                drawInfo.AllItemString = newItemstring;
                drawInfo.PrizeItemString = prizeItemString;
                drawInfo.OpenNumber++;
                drawInfo.UpdateTime = DateTime.Now;
                if (goldbar != null)
                    goldbar.GoldBarNumber = goldbar.GoldBarNumber - drawInfo.Price;


                ItemPackageFrame package = null;
                MailBuilder mail = null;
                var info = GetById(drawInfo.ManagerId);
                int itemCode = 0;
                var messageCode = ParsingPrize(drawInfo, drawIdx, ref package, ref mail, ref info,ref itemCode);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<RevelationDrawResultResponse>(messageCode);
                drawInfo.Price = CacheFactory.RevelationCache.GetDrawGoldBar(drawInfo.OpenNumber, manager.VipLevel);
                if (drawInfo.Price > 0)
                {
                    info.IsHaveDraw = false;
                    info.DrawId = Guid.Empty;
                }
                messageCode = SaveDraw(info, drawInfo, package, mail,goldbar);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<RevelationDrawResultResponse>(messageCode);

                response.Data.AllItemString = newItemstring;
                response.Data.DrawId = drawIdx;
                response.Data.NextDrawGoldBar = CacheFactory.RevelationCache.GetDrawGoldBar(drawInfo.OpenNumber,
                    manager.VipLevel);
                response.Data.PrizeItemString = prizeItemString;
                response.Data.ItemCode = itemCode;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("球星启示录翻牌", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 更新数据库   --事务
        /// </summary>
        /// <param name="info"></param>
        /// <param name="draw"></param>
        /// <param name="package"></param>
        /// <param name="mail"></param>
        /// <param name="goldbar"></param>
        /// <returns></returns>
        MessageCode SaveDraw(RevelationInfoEntity info, RevelationDrawEntity draw, ItemPackageFrame package, MailBuilder mail, ScoutingGoldbarEntity goldbar)
        {
            MessageCode messageCode = MessageCode.NbParameterError;
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    messageCode = Tran_SaveDraw(transactionManager.TransactionObject, info, draw, package, mail,goldbar);

                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                }
                return messageCode;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveDraw", ex);
                return MessageCode.Exception;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="info"></param>
        /// <param name="draw"></param>
        /// <param name="package"></param>
        /// <param name="mail"></param>
        /// <param name="goldbar"></param>
        /// <returns></returns>
        private MessageCode Tran_SaveDraw(DbTransaction transaction, RevelationInfoEntity info, RevelationDrawEntity draw, ItemPackageFrame package, MailBuilder mail, ScoutingGoldbarEntity goldbar)
        {
            if (!RevelationInfoMgr.Update(info, transaction))
                return MessageCode.NbUpdateFail;
            if (draw != null)
            {
                if (!RevelationDrawMgr.Update(draw, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (package != null)
            {
                if (!package.Save(transaction))
                    return MessageCode.NbUpdateFail;
                package.Shadow.Save();
            }
            if (mail != null)
            {
                if (!mail.Save(transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (goldbar != null)
            {
                if (!ScoutingGoldbarMgr.Update(goldbar, transaction))
                    return MessageCode.NbUpdateFail;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 得到抽取到的卡
        /// </summary>
        /// <param name="drawString"></param>
        /// <param name="newDrawString"></param>
        /// <param name="prizeItemString"></param>
        /// <returns></returns>
        public int GetDrawId(string drawString,ref string newDrawString,ref string prizeItemString)
        {
            if (drawString.Length <= 0)
                return 0;
            var items = drawString.Split('|');
            var drawList = new List<int>();
            foreach (var item in items)
            {
                var drawInfo = item.Split(',');
                if (ConvertHelper.ConvertToInt(drawInfo[1]) == 0)
                    drawList.Add(ConvertHelper.ConvertToInt(drawInfo[0]));
            }
            if (drawList.Count > 0)
            {
                var result = "";
                var idx = drawList[RandomHelper.GetInt32WithoutMax(0, drawList.Count)];
                if (prizeItemString.Length == 0)
                    prizeItemString = idx.ToString();
                else
                    prizeItemString += "," + idx;
                bool isReplace = false;
                foreach (var item in items)
                {
                    if (!isReplace)
                    {
                        var drawInfo = item.Split(',');
                        if (ConvertHelper.ConvertToInt(drawInfo[0]) == idx &&
                            ConvertHelper.ConvertToInt(drawInfo[1]) == 0)
                        {
                            isReplace = true;
                            result += drawInfo[0] + ",1|";
                            continue;
                        }
                    }
                    result += item + "|";
                }
                if (result.Length > 0)
                    result = result.Substring(0, result.Length - 1);
                newDrawString = result;
                return idx;
            }
            return 0;
        }

        /// <summary>
        /// 解析奖励
        /// </summary>
        /// <param name="drawEntity"></param>
        /// <param name="drawIdx"></param>
        /// <param name="package"></param>
        /// <param name="mail"></param>
        /// <param name="info"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public MessageCode ParsingPrize(RevelationDrawEntity drawEntity,int drawIdx,ref ItemPackageFrame package,ref MailBuilder mail,ref RevelationInfoEntity info,ref int itemCode)
        {
            var prizeInfo = CacheFactory.RevelationCache.GetDrawPrizeInfo(drawIdx);
            switch (prizeInfo.PrizeType)
            {
                case (int)RevelationDrawPrizeType.CoachDebris:
                    var markConfig = CacheFactory.RevelationCache.GetMarkInfo(drawEntity.MarkId, drawEntity.Schedule);
                    if (markConfig == null || markConfig.PassPrizeItem.Length == 0)
                        return MessageCode.NbParameterError;
                    var items = markConfig.PassPrizeItem.Split('|'); 
                    var iteminfo = items[RandomHelper.GetInt32WithoutMax(0, items.Length)];
                    var additem = iteminfo.Split(',');
                    itemCode = ConvertHelper.ConvertToInt(additem[0]);
                    RevelationAddItem(drawEntity.ManagerId, itemCode, ConvertHelper.ConvertToInt(additem[1]), ref package, ref mail);
                    break;
                case (int)RevelationDrawPrizeType.Morale:
                    info.Morale += prizeInfo.ItemCount;
                    break;
                case (int)RevelationDrawPrizeType.Item:
                    RevelationAddItem(drawEntity.ManagerId, prizeInfo.SubType, prizeInfo.ItemCount, ref package, ref mail);
                    break;
                case (int)RevelationDrawPrizeType.Courage:
                    info.Courage += prizeInfo.ItemCount;
                    break;
                default:
                    return MessageCode.NbParameterError;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 翻牌加物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="package"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        public MessageCode RevelationAddItem(Guid managerId, int itemCode,int itemCount, ref ItemPackageFrame package, ref MailBuilder mail)
        {
            package = ItemCore.Instance.GetPackage(managerId,
                        EnumTransactionType.RevelationClearanceAward);
            var messageCode = package.AddItems(ConvertHelper.ConvertToInt(itemCode),
                ConvertHelper.ConvertToInt(itemCount));
            if (messageCode != MessageCode.Success)
            {
                package = null;
                mail = new MailBuilder(managerId, ConvertHelper.ConvertToInt(itemCode),
                    ConvertHelper.ConvertToInt(itemCount), false, 1);
            }
            return MessageCode.Success;
        }


        #endregion

        #region 增加士气

        /// <summary>
        /// 增加挑战次数
        /// </summary>
        /// <param name="managerId"></param>
        public RevelationAddChallengeResponse RevelationAddMorale(Guid managerId)
        {
            RevelationAddChallengeResponse response = new RevelationAddChallengeResponse();
            response.Data = new RevelationAddChallenge();
            var info = GetById(managerId);
            //经理为空
            if (info == null)
                return ResponseHelper.Create<RevelationAddChallengeResponse>(MessageCode.NbParameterError);
            if (info.PresentMark == 0)
                return ResponseHelper.Create<RevelationAddChallengeResponse>(MessageCode.NbParameterError);
            if (info.Morale >= CacheFactory.RevelationCache.Morale)
                return ResponseHelper.Create<RevelationAddChallengeResponse>(MessageCode.RevelationMoraleFull);
            int price = CacheFactory.RevelationCache.AddMoralePrice;
            //获取点卷数量
            int point = PayCore.Instance.GetPoint(managerId);
            if (point < price)
                return ResponseHelper.Create<RevelationAddChallengeResponse>(MessageCode.NbPointShortage);
            info.Morale = CacheFactory.RevelationCache.Morale;
            info.UpdateTime = DateTime.Now;
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                var messageCode = AddMorale(info,  price, transactionManager.TransactionObject);
                if (messageCode != (int) MessageCode.Success)
                {
                    transactionManager.Rollback();
                    return ResponseHelper.Create<RevelationAddChallengeResponse>(messageCode);
                }
                transactionManager.Commit();
            }
            response.Data.Point = point - price;
            response.Data.Morale = CacheFactory.RevelationCache.Morale;
            return response;
        }

        /// <summary>
        /// 增加士气
        /// </summary>
        /// <param name="info"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public MessageCode AddMorale(RevelationInfoEntity info, int price, DbTransaction tran)
        {
            var messageCode = PayCore.Instance.GambleConsume(info.ManagerId, price, ShareUtil.GenerateComb(),
                EnumConsumeSourceType.RevelationAddChallenge, tran);
            if (messageCode != MessageCode.Success)
                return messageCode;
            if (!RevelationInfoMgr.Update(info, tran))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }

        #endregion

        #region 重置关卡

        /// <summary>
        /// 重置关卡
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse RevelationResetMark(Guid managerId)
        {
            var response = new MessageCodeResponse();
            try
            {
                var info = GetById(managerId);
                info.DrawId = Guid.Empty;
                info.HookId = Guid.Empty;
                info.IsHaveDraw = false;
                info.IsHook = false;
                info.Morale = 0;
                info.PresentMark = 0;
                info.Schedule = 0;
                info.UpdateTime = DateTime.Now;
                if (!RevelationInfoMgr.Update(info))
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("球星启示录重置关卡", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #region 勇气商城兑换物品

        /// <summary>
        /// 获取勇气商城信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationShopEntity GetShopInfo(Guid managerId)
        {
            DateTime date = DateTime.Now;
            var shopInfo = RevelationShopMgr.GetById(managerId);
            if (shopInfo == null)
            {
                shopInfo = new RevelationShopEntity(managerId, CacheFactory.RevelationCache.GetShopString(), "", 0,
                    GetNextrefreshTime(), date, date);
                RevelationShopMgr.Insert(shopInfo);
                return shopInfo;
            }
            if (IsRefresh(shopInfo))
            {
                shopInfo.ItemString = CacheFactory.RevelationCache.GetShopString();
                shopInfo.RefreshData = GetNextrefreshTime();
                shopInfo.UpdateTime = date;
                shopInfo.ExChangeString = "";
                shopInfo.Status = 0;
                RevelationShopMgr.Update(shopInfo);
            }
            return shopInfo;
        }

        /// <summary>
        /// 验证是否刷新
        /// </summary>
        /// <param name="shopInfo"></param>
        /// <returns></returns>
        public bool IsRefresh(RevelationShopEntity shopInfo)
        {
            if (string.IsNullOrEmpty(shopInfo.ItemString))
                return true;
            if (DateTime.Now >= shopInfo.RefreshData)
                return true;
            return false;
        }

        /// <summary>
        /// 下次刷新时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetNextrefreshTime()
        {
            DateTime date = DateTime.Now;
            //下次刷新时间
            DateTime nextRefreshTime = date.Date.AddHours(21);
            if (date.Hour >= 21)
                nextRefreshTime = nextRefreshTime.Date.AddDays(1);
            return nextRefreshTime;
        }

        /// <summary>
        /// 获取勇气商城信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetShopResponse RevelationGetShopInfo(Guid managerId)
        {
            var response = new RevelationGetShopResponse();
            response.Data = new RevelationGetShop();
            try
            {
                var shopInfo = GetShopInfo(managerId);
                var info = GetById(managerId);
                response.Data.MyCourage = info.Courage;
                var itemStrings = shopInfo.ItemString.Split('|');
                var resultList = new List<RevelationShopItem>();
                foreach (var itemString in itemStrings)
                {
                    var entity = new RevelationShopItem();
                    var items = itemString.Split(',');
                    entity.Idx = ConvertHelper.ConvertToInt(items[0]);
                    //已经购买过
                    if (shopInfo.ExChangeString.Length > 0 && shopInfo.ExChangeString.Contains(items[0]))
                        entity.IsMayBuy = true;
                    entity.ItemCode = ConvertHelper.ConvertToInt(items[1]);
                    entity.ItemCount = ConvertHelper.ConvertToInt(items[2]);
                    entity.Price = ConvertHelper.ConvertToInt(items[3]);
                    resultList.Add(entity);
                }
                response.Data.ItemList = resultList;
                response.Data.RefreshPoint = CacheFactory.RevelationCache.GetShopRefreshPrice(shopInfo.Status); ;
                response.Data.Point = -1;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取勇气商城信息", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 勇气商城兑换物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public RevelationPurchaseItemsResponse RevelationPurchaseItems(Guid managerId, int idx)
        {
            RevelationPurchaseItemsResponse response = new RevelationPurchaseItemsResponse();
            response.Data = new RevelationPurchaseItems();
            try
            {
                var info = GetById(managerId);
                var shopInfo = GetShopInfo(managerId);
                if (info == null || shopInfo == null)
                    return ResponseHelper.InvalidParameter<RevelationPurchaseItemsResponse>("manager is null");

                //验证是否兑换过
                if (shopInfo.ExChangeString.Length > 0 && shopInfo.ExChangeString.Contains(idx.ToString()))
                    return ResponseHelper.Create<RevelationPurchaseItemsResponse>(MessageCode.LadderExchangeTimesOver);
                int price = CacheFactory.RevelationCache.GetShopPrice(idx);
                if (price == 0)
                    return ResponseHelper.Create<RevelationPurchaseItemsResponse>(MessageCode.NbParameterError);
                //勇气值不够
                if (info.Courage < price)
                    return ResponseHelper.Create<RevelationPurchaseItemsResponse>(MessageCode.RevelationCourageNot);
                if (shopInfo.ExChangeString.Length == 0)
                    shopInfo.ExChangeString = idx.ToString();
                else
                    shopInfo.ExChangeString += "," + idx;
                shopInfo.UpdateTime = DateTime.Now;
                info.Courage = info.Courage - price;
                info.UpdateTime = DateTime.Now;

                #region 解析物品

                var shopItems = shopInfo.ItemString.Split('|');
                var itemCode = 0;
                var itemCount = 0;
                foreach (var items in shopItems)
                {
                    var item = items.Split(',');
                    var itemid = ConvertHelper.ConvertToInt(item[0]);
                    if (itemid == idx)
                    {
                        itemCode = ConvertHelper.ConvertToInt(item[1]);
                        itemCount = ConvertHelper.ConvertToInt(item[2]);
                        break;
                    }
                }

                #endregion

                if (itemCount == 0 || itemCode == 0)
                    return ResponseHelper.Create<RevelationPurchaseItemsResponse>(MessageCode.NbParameterError);

                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.RevelationExChange);

                var messageCode = package.AddItems(itemCode, itemCount);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<RevelationPurchaseItemsResponse>(messageCode);

                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    messageCode = PurchaseItems(info, shopInfo, package, transactionManager.TransactionObject);
                    if (messageCode != (int) MessageCode.Success)
                    {
                        transactionManager.Rollback();
                    return ResponseHelper.Create<RevelationPurchaseItemsResponse>(messageCode);
                    }
                    transactionManager.Commit();
                }
                response.Data.ItemCode = itemCode;
                response.Data.ItemCount = itemCount;
                response.Data.Courage = info.Courage;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("球星启示录兑换物品", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 勇气商城兑换物品
        /// </summary>
        /// <param name="info"></param>
        /// <param name="shopInfo"></param>
        /// <param name="package"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public MessageCode PurchaseItems(RevelationInfoEntity info, RevelationShopEntity shopInfo,ItemPackageFrame package, DbTransaction tran)
        {
            if (!RevelationInfoMgr.Update(info, tran))
                return MessageCode.NbUpdateFail;
            if (!RevelationShopMgr.Update(shopInfo, tran))
                return MessageCode.NbUpdateFail;
            if (!package.Save(tran))
                return MessageCode.NbUpdateFail;
            package.Shadow.Save();
            return MessageCode.Success;
        }

        /// <summary>
        /// 勇气商城刷新
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationGetShopResponse RevelationShopRefresh(Guid managerId)
        {
            try
            {
                var info = GetById(managerId);
                var shopInfo = GetShopInfo(managerId);
                if (info == null || shopInfo == null)
                    return ResponseHelper.InvalidParameter<RevelationGetShopResponse>("manager is null");
                var price = CacheFactory.RevelationCache.GetShopRefreshPrice(shopInfo.Status);
                if (price == 0)
                    return ResponseHelper.Create<RevelationGetShopResponse>(MessageCode.NbParameterError);
                var point = PayCore.Instance.GetPoint(managerId);
                if (point < price)
                    return ResponseHelper.Create<RevelationGetShopResponse>(MessageCode.NbPointShortage);

                shopInfo.ItemString = CacheFactory.RevelationCache.GetShopString();
                shopInfo.ExChangeString = "";
                shopInfo.Status ++;
                shopInfo.UpdateTime = DateTime.Now;

                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();

                    var messageCode = MessageCode.NbUpdateFail;
                    do
                    {
                        messageCode = PayCore.Instance.GambleConsume(managerId, price, ShareUtil.GenerateComb(),
                            EnumConsumeSourceType.RevelateionShopRefresh, transactionManager.TransactionObject);
                        if (messageCode != MessageCode.Success)
                            break;
                        if (!RevelationShopMgr.Update(shopInfo, transactionManager.TransactionObject))
                            break;
                        messageCode = MessageCode.Success;
                    } while (false); 
                   
                    if (messageCode != (int)MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<RevelationGetShopResponse>(messageCode);
                    }
                    transactionManager.Commit();
                }

                var response = RevelationGetShopInfo(managerId);
                response.Data.Point = point - price;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("勇气商城刷新", ex);
                return ResponseHelper.Create<RevelationGetShopResponse>(MessageCode.NbParameterError);
            }
        }

        #endregion

        #region 挂机


        /// <summary>
        /// 挂机
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RevelationOutboardMotorResponse RevelationOutboardMotor(Guid managerId)
        {
            RevelationOutboardMotorResponse response = new RevelationOutboardMotorResponse();
            response.Data = new RevelationOutboardMotor();
            try
            {
                var info = GetById(managerId);
                if (info.PresentMark == 0)
                    return ResponseHelper.Create<RevelationOutboardMotorResponse>(MessageCode.RevelationMarkNotStart);
                if (info.Morale <= 0)
                    return ResponseHelper.Create<RevelationOutboardMotorResponse>(MessageCode.RevelationMorale);
                if (info.IsHook)
                    return ResponseHelper.Create<RevelationOutboardMotorResponse>(MessageCode.RevelationHaveHook);
                //是否通过了首次

                var hookInfo = new RevelationHookEntity(ShareUtil.GenerateComb(), managerId, info.PresentMark,
                    info.Schedule, "", "", 0, DateTime.Now, DateTime.Now);
                info.IsHook = true;
                info.HookId = hookInfo.HookId;
                info.UpdateTime = DateTime.Now;


            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("球星启示录挂机", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 计算挂机数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public RevelationCalculateOutboardMotorResponse RevelationCalculateOutboardMotor(Guid managerId,int mark)
        {
            RevelationCalculateOutboardMotorResponse response = new RevelationCalculateOutboardMotorResponse();
            response.Data = new RevelationCalculateOutboardMotor();


            return response;
        }

        #endregion
    }
}
