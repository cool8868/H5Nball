using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Ad;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core
{
    public class PenaltyKickCore
    {
        #region Instance

        public static PenaltyKickCore Instance
        {
            get { return SingletonFactory<PenaltyKickCore>.SInstance; }
        }

        /// <summary>
        /// 活动信息
        /// </summary>
        public PenaltykickSeasonEntity _seasonInfo;

        /// <summary>
        /// 是否有活动
        /// </summary>
        public bool IsActivity
        {
            get
            {
                DateTime date = DateTime.Now;
                if (_seasonInfo.StartTime <= date && _seasonInfo.EndTime >= date)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// 射飞概率13%
        /// </summary>
        public readonly int _aDRateShootFly = 13;
        /// <summary>
        /// 射中门框概率12%
        /// </summary>
        public readonly int _aDRateShootFrame = 25;
        /// <summary>
        /// 扑救一致概率65%
        /// </summary>
        public readonly int _aDRateDiveSame = 90;

        /// <summary>
        /// 排名列表
        /// </summary>
        public ConcurrentDictionary<Guid, PenaltyKickRankEntity> _rankDic;

        public int _refreshExChangePoint;

        public PenaltyKickCore(int p)
        {
            InitActivity();
            _refreshExChangePoint = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RefreshExChangePoint, 100);
            SetRank();
        }

        private void InitActivity()
        {
            _seasonInfo = PenaltykickSeasonMgr.GetSeason();
            if (_seasonInfo == null)
                _seasonInfo = new PenaltykickSeasonEntity(0, ConvertHelper.StringToDateTime("2000-01-01"),ConvertHelper.StringToDateTime("2000-01-01"), true, DateTime.Now);
            if (!IsActivity && !_seasonInfo.IsPrize)
            {
                //SetRank();
                //开始发奖
                SendPrize(_seasonInfo);
            }
        }

        #endregion

        #region 获取活动信息

        /// <summary>
        /// 活动提示接口
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PenaltyKickGetActivityInfoResponse GetActivityInfo(Guid managerId)
        {
            var response = new PenaltyKickGetActivityInfoResponse();
            response.Data = new PenaltyKickGetActivityInfo();
            try
            {
                DateTime date = DateTime.Now;
                //入口在活动结束2天后关闭
                if (_seasonInfo.StartTime <= date && _seasonInfo.EndTime >=DateTime.Now.AddDays(-2))
                    response.Data.IsActivity = true;
                var info = GetManager(managerId);
                if (info != null)
                {
                    if (info.FreeNumber > 0 || info.GameCurrency > 0)
                        response.Data.IsPrompt = true;
                }
                else if (IsActivity)
                    response.Data.IsPrompt = true;
                else
                    response.Data.IsPrompt = false;
                response.Data.StartTimeTick = ShareUtil.GetTimeTick(_seasonInfo.StartTime);
                response.Data.EndTimeTick = ShareUtil.GetTimeTick(_seasonInfo.EndTime);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("点球获取活动信息", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #region 获取排名
        /// <summary>
        /// 获取排名列表
        /// </summary>
        /// <returns></returns>
        public PenaltyKickRankResponse GetRank(Guid managerId)
        {
            var response = new PenaltyKickRankResponse();
            response.Data = new PenaltyKickRank();
            try
            {
                response.Data.RankList = GetRank();
                if (_rankDic.ContainsKey(managerId))
                    response.Data.MyData = _rankDic[managerId];

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("点球获取排名列表", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 设置排名信息
        /// </summary>
        /// <returns></returns>
        public MessageCode SetRank()
        {
            if (IsActivity)//有活动才更新排名
            {
                PenaltykickManagerMgr.SetRank();
                    //return MessageCode.NbUpdateFail;
            }
            var rankList = PenaltykickManagerMgr.GetRank();
            var dic = new ConcurrentDictionary<Guid, PenaltyKickRankEntity>();
            foreach (var item in rankList)
            {
                if (!dic.ContainsKey(item.ManagerId))
                    dic.TryAdd(item.ManagerId, item);

            }
            _rankDic = dic;
            return MessageCode.Success;
        }

        private List<PenaltyKickRankEntity> GetRank()
        {
            if (_rankDic == null || _rankDic.Count == 0)
                return new List<PenaltyKickRankEntity>();
            return _rankDic.Values.OrderBy(r => r.Rank).ToList();
        }

        /// <summary>
        /// 更新排名
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public MessageCode UpdateRank(PenaltykickManagerEntity info)
        {
            if (info == null)
                return MessageCode.Success;
            PenaltyKickRankEntity entity = null;
            if (_rankDic.ContainsKey(info.ManagerId))
            {
                entity = _rankDic[info.ManagerId];
                entity.ScoreChangeTime = info.ScoreChangeTime;
                entity.TotalScore = info.TotalScore;
            }
            else
            {
                var manager = ManagerCore.Instance.GetManager(info.ManagerId);
                if (manager == null)
                    return MessageCode.NbParameterError;
                entity = new PenaltyKickRankEntity();
                entity.ManagerId = info.ManagerId;
                entity.Name = manager.Name;
                entity.ScoreChangeTime = info.ScoreChangeTime;
                entity.TotalScore = info.TotalScore;
                _rankDic.TryAdd(info.ManagerId, entity);
            }
            var rankList = _rankDic.Values.OrderByDescending(r=>r.TotalScore).ToList();
            var dic = new ConcurrentDictionary<Guid, PenaltyKickRankEntity>();
            for (int i = 0; i < rankList.Count; i++)
            {
                if (i >= 100)
                    break;
                rankList[i].Rank = i + 1;
                if (!dic.ContainsKey(rankList[i].ManagerId))
                    dic.TryAdd(rankList[i].ManagerId, rankList[i]);
            }
            _rankDic = dic;
            return MessageCode.Success;
        }

        #endregion

        #region 发奖

        public MessageCode SendPrize()
        {
            InitActivity();
            return MessageCode.Success;
        }

        private MessageCode SendPrize(PenaltykickSeasonEntity seasonInfo)
        {
            if (seasonInfo.Idx == 0)
                return MessageCode.Success;
            PenaltykickManagerMgr.SetRank();
            var prizeList = PenaltykickManagerMgr.GetNotPrize(seasonInfo.Idx);
            seasonInfo.IsPrize = true;
            seasonInfo.PrizeTime = DateTime.Now;
            foreach (var item in prizeList)
            {
                if (item.IsPrize || item.TotalScore<500)
                    continue;
                item.IsPrize = true;
                //排名奖励
                var prize = CacheFactory.AdCache.GetPrize(3, item.Rank);
                if (prize == null || prize.Count == 0)
                    continue;
                //邮件
                var mail = new MailBuilder(item.Rank, item.ManagerId);
                foreach (var p in prize)
                {
                    switch (p.ItemType)
                    {
                        case 3://暂时只有物品奖励
                            mail.AddAttachment(p.ItemCount, p.ItemCode, false, 1);
                            break;
                        case 10://金条
                            mail.AddAttachment(EnumCurrencyType.GoldBar, p.ItemCount);
                            break;
                    }
                }
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (!PenaltykickManagerMgr.Update(item,transactionManager.TransactionObject))
                            break;
                        if (!mail.Save(transactionManager.TransactionObject))
                            break;
                        messageCode = MessageCode.Success;
                    } while (false);
                    if (messageCode != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                    }
                    transactionManager.Commit();
                }
            }
            PenaltykickSeasonMgr.Update(seasonInfo);
            return MessageCode.Success;
        }

        #endregion

        #region 获取点球信息

        /// <summary>
       /// 获取用户信息
       /// </summary>
       /// <param name="managerId"></param>
       /// <returns></returns>
        public GetPenaltyKickInfoResponse GetInfo(Guid managerId)
        {
            var response = new GetPenaltyKickInfoResponse();
            response.Data = new GetPenaltyKickInfo();
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (null == manager)
                    return ResponseHelper.Create<GetPenaltyKickInfoResponse>(MessageCode.MissManager);
                var info = GetManager(managerId);
                if (info == null)
                    return ResponseHelper.Create<GetPenaltyKickInfoResponse>(MessageCode.MissManager);
                //活动结束了
                if (!IsActivity)
                {
                    info.FreeNumber = 0;
                    info.GameCurrency = 0;
                    info.ShooterAttribute = 0;
                    info.ShootLog = "";
                    info.Status = 0;
                    info.CombGoals = 0;
                    info.MaxCombGoals = 0;
                }
                response.Data = GetManagerInfoResponse(info);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取点球用户信息", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        private GetPenaltyKickInfo GetManagerInfoResponse(PenaltykickManagerEntity info)
        {
            var entity = new GetPenaltyKickInfo();
            entity.AvailableScore = info.AvailableScore;
            entity.CombGoals = info.CombGoals;
            entity.FreeNumber = info.FreeNumber;
            entity.GameCurrency = info.GameCurrency;
            entity.MaxCombGoals = info.MaxCombGoals;
            entity.ShooterAttribute = info.ShooterAttribute;
            entity.ShootLog = info.ShootLog;
            entity.Status = info.Status;
            entity.TotalScore = info.TotalScore;
            if (info.ExChangeString.Length == 0)
            {
                info.ExChangeString = RefreshExChange();
            }
            var exList  = GetExChangeEntity(info.ExChangeString);
            //所有物品都兑换完了  自动刷新
            if (!exList.Exists(r => r.Status == 0))
                info.ExChangeString = RefreshExChange();
            exList  = GetExChangeEntity(info.ExChangeString);
            entity.ExChangeList = exList;
            return entity;
        }

        #endregion

        #region 开始活动
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetPenaltyKickInfoResponse Join(Guid managerId)
        {
            var response = new GetPenaltyKickInfoResponse();
            response.Data = new GetPenaltyKickInfo();
            try
            {
                //不在活动时间内
                if (!IsActivity)
                    return ResponseHelper.Create<GetPenaltyKickInfoResponse>(MessageCode.AdMissSeason);
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (null == manager)
                    return ResponseHelper.Create<GetPenaltyKickInfoResponse>(MessageCode.MissManager);
                var info = GetManager(managerId);
                if (info == null)
                    return ResponseHelper.Create<GetPenaltyKickInfoResponse>(MessageCode.MissManager);
                //游戏还未结束
                if (info.Status == 1)
                    return ResponseHelper.Create<GetPenaltyKickInfoResponse>(MessageCode.GameNotEnd);
                bool isfree = false;
                if (info.FreeNumber > 0)
                {
                    info.FreeNumber --;
                    isfree = true;
                }
                else
                {
                    if (info.GameCurrency <= 0)
                        return ResponseHelper.Create<GetPenaltyKickInfoResponse>(MessageCode.GameCurrencyNumberNot);
                    info.GameCurrency --;
                }
                info.Status = 1;
                //获取踢球球员属性
                var shooterAttribute = GetShooterId(managerId);
                if (shooterAttribute == 0)
                    return ResponseHelper.Create<GetPenaltyKickInfoResponse>(MessageCode.NbParameterError);
                info.ShooterAttribute = shooterAttribute;
                info.ShootNumber ++;
                info.ShootLog = "";
                info.CombGoals = 0;
                info.MaxCombGoals = 0;
                info.UpdateTime = DateTime.Now;
                if (!PenaltykickManagerMgr.Update(info))
                    return ResponseHelper.Create<GetPenaltyKickInfoResponse>(MessageCode.NbUpdateFail);
                //插入消费记录
                PenaltykickManagerMgr.InsertRecord(managerId, 1, isfree);
                response.Data = GetManagerInfoResponse(info);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("点球开始游戏", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }
        #endregion

        #region 开始踢球

        /// <summary>
        /// 开始射门
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PenaltyKickShootResponse Shoot(Guid managerId)
        {
            var response = new PenaltyKickShootResponse();
            response.Data = new PenaltyKickShoot();
            try
            {
                if (!IsActivity)
                    return ResponseHelper.Create<PenaltyKickShootResponse>(MessageCode.AdMissSeason);
                var info = GetManager(managerId);
                if (info == null)
                    return ResponseHelper.Create<PenaltyKickShootResponse>(MessageCode.MissManager);
                if (info.Status != 1)
                    return ResponseHelper.Create<PenaltyKickShootResponse>(MessageCode.GameEnd);
                if (info.ShootLog.Length > 0)
                {
                    var shootList = info.ShootLog.Split(',');
                    if (shootList.Length >= 5)
                        return ResponseHelper.Create<PenaltyKickShootResponse>(MessageCode.GameEnd);
                }
                //背包满了  不让踢球
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.AdTopScorerKeep);
                if (package == null)
                    return ResponseHelper.Create<PenaltyKickShootResponse>(MessageCode.NbParameterError);
                if (package.IsFull)
                    return ResponseHelper.Create<PenaltyKickShootResponse>(MessageCode.ItemPackageFull);
                //射门结果
                var shootResult = GetShootResult(info.ShooterAttribute);
                //射门结果处理
                Shoot(info, shootResult.IsGoals);
                int score = 0;
                //进球了才有奖励
                var prizeList = new List<PrizeEntity>();
                if (shootResult.IsGoals)
                {
                    score++; //每进一球加一点
                    prizeList = GetPrize(info.ShootLog, info.MaxCombGoals);
                    foreach (var item in prizeList)
                    {
                        switch (item.ItemType)
                        {
                            case 3: //物品
                                var code = package.AddItems(item.ItemCode, item.ItemCount);
                                if (code != MessageCode.Success)
                                    return ResponseHelper.Create<PenaltyKickShootResponse>(code);
                                break;
                            case 5: //积分
                                score += item.ItemCount;
                                break;
                        }
                    }
                    info.AvailableScore += score;
                    info.TotalScore += score;
                    info.ScoreChangeTime = DateTime.Now;
                }
                info.UpdateTime = DateTime.Now;
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (shootResult.IsGoals)
                        {
                            if (!package.Save(transactionManager.TransactionObject))
                                break;
                        }
                        if (!PenaltykickManagerMgr.Update(info, transactionManager.TransactionObject))
                            break;
                        messageCode = MessageCode.Success;
                    } while (false);
                    if (messageCode != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<PenaltyKickShootResponse>(messageCode);
                    }
                    transactionManager.Commit();
                    package.Shadow.Save();
                }

                response.Data.Info = GetManagerInfoResponse(info);
                response.Data.ShootResult = shootResult;
                response.Data.ItemList = prizeList;
                //更新排名信息
                UpdateRank(info);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("点球射门", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 踢球状态
        /// </summary>
        /// <param name="shootProp"></param>
        /// <returns></returns>
        ShootItem GetShootResult(int shootProp)
        {
            var rand = RandomPicker.GetRandom();
            var obj = new ShootItem();
            int val = RandomPicker.RandomInt(0, 100, rand);
            if (val <= _aDRateShootFly)
                obj.ShootPos = 2;//射飞
            else if (val <= _aDRateShootFrame)
                obj.ShootPos = 1;//射中门框
            else
                obj.ShootPos = 0;//射中
            bool diveSame = val <= _aDRateDiveSame;
            obj.DiveDir = diveSame;
           
            if (obj.ShootPos == 0)
            {
                if (!diveSame
                    || RandomPicker.RandomInt(1, 100) <= shootProp * 100 / (shootProp + 200))
                {
                    obj.IsGoals = true;
                    return obj;
                }
            }
            obj.IsGoals = false;
            return obj;
        }

        /// <summary>
        /// 射门结果处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="isGoals"></param>
        public void Shoot(PenaltykickManagerEntity info, bool isGoals)
        {

            if (isGoals)
            {
                if (info.ShootLog.Length == 0)
                    info.ShootLog += 1;
                else info.ShootLog += "," + 1;
            }
            else
            {
                if (info.ShootLog.Length == 0)
                    info.ShootLog += 0;
                else info.ShootLog += "," + 0;
            }
            if (!isGoals)
                info.CombGoals = 0;
            else
            {
                info.CombGoals++;
                info.TotalGoals ++;
                if (info.MaxCombGoals < info.CombGoals)
                    info.MaxCombGoals = info.CombGoals;
            }
            var shootList = info.ShootLog.Split(',');
            if (shootList.Length >= 5)
                info.Status = 2;//射门结束

        }

        #endregion

        #region 兑换

        /// <summary>
        /// 积分兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public PenaltyKickExChangeResponse ExChange(Guid managerId, int itemCode)
        {
            PenaltyKickExChangeResponse response = new PenaltyKickExChangeResponse();
            response.Data = new PenaltyKickExChange();
            try
            {
                var info = GetManager(managerId);
                if (info == null)
                    return ResponseHelper.Create<PenaltyKickExChangeResponse>(MessageCode.MissManager);
                var exList = GetExChangeEntity(info.ExChangeString);
                if (exList == null)
                    return ResponseHelper.Create<PenaltyKickExChangeResponse>(MessageCode.NbParameterError);
                //获取要兑换的物品
                var exItem = exList.Find(r => r.ItemCode == itemCode);
                if (exItem == null)
                    return ResponseHelper.Create<PenaltyKickExChangeResponse>(MessageCode.ExChangeItemNot);
                //已经兑换过
                if (exItem.Status == 1)
                    return ResponseHelper.Create<PenaltyKickExChangeResponse>(MessageCode.RepeatExChange);
                //积分不足
                if(info.AvailableScore< exItem.Price)
                    return ResponseHelper.Create<PenaltyKickExChangeResponse>(MessageCode.LadderExchangeScoreShortage);
                info.AvailableScore = info.AvailableScore - exItem.Price;
                exItem.Status = 1;
                //所有物品都兑换完了  自动刷新
                if (!exList.Exists(r => r.Status == 0))
                {
                    info.ExChangeString = RefreshExChange();
                    exList = GetExChangeEntity(info.ExChangeString);
                }
                else
                {
                    info.ExChangeString = SetExChangeString(exList);
                }

                info.UpdateTime = DateTime.Now;
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.AdTopScorerKeep);
                if (package == null)
                    return ResponseHelper.Create<PenaltyKickExChangeResponse>(MessageCode.NbParameterError);
                var messageCode = package.AddItem(exItem.ItemCode);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<PenaltyKickExChangeResponse>(messageCode);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    messageCode = MessageCode.NbUpdateFail;
                    do
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                        if (!PenaltykickManagerMgr.Update(info, transactionManager.TransactionObject))
                            break;
                        messageCode = MessageCode.Success;
                    } while (false);
                    if (messageCode != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<PenaltyKickExChangeResponse>(messageCode);
                    }
                    transactionManager.Commit();
                    package.Shadow.Save();
                }
                response.Data.AvailableScore = info.AvailableScore;
                response.Data.ExChangeList = exList;
                response.Data.ItemCode = exItem.ItemCode;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("点球积分兑换", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 刷新点球兑换
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public PenaltyKickExChangeResponse RefreshExChange(Guid managerId)
        {
             PenaltyKickExChangeResponse response = new PenaltyKickExChangeResponse();
            response.Data = new PenaltyKickExChange();
            try
            {
                var info = GetManager(managerId);
                if (info == null)
                    return ResponseHelper.Create<PenaltyKickExChangeResponse>(MessageCode.MissManager);
                var point = PayCore.Instance.GetPoint(managerId);
                if (point < _refreshExChangePoint)
                    return ResponseHelper.Create<PenaltyKickExChangeResponse>(MessageCode.NbPointShortage);
                info.ExChangeString = RefreshExChange();
                var exList = GetExChangeEntity(info.ExChangeString);
                info.ExChangeString = SetExChangeString(exList);
                info.UpdateTime = DateTime.Now;
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.NbUpdateFail;
                    do
                    {
                        messageCode = PayCore.Instance.GambleConsume(managerId, _refreshExChangePoint, ShareUtil.GenerateComb(),
                            EnumConsumeSourceType.AdTopScoreResetExchange, transactionManager.TransactionObject);
                        if (messageCode != MessageCode.Success)
                            break;
                        messageCode = MessageCode.NbUpdateFail;
                        if (!PenaltykickManagerMgr.Update(info, transactionManager.TransactionObject))
                            break;
                        messageCode = MessageCode.Success;
                    } while (false);
                    if (messageCode != MessageCode.Success)
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<PenaltyKickExChangeResponse>(messageCode);
                    }
                    transactionManager.Commit();
                }
                response.Data.AvailableScore = info.AvailableScore;
                response.Data.ExChangeList = exList;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("刷新点球兑换", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 获取兑换列表
        /// </summary>
        /// <param name="exChangeString"></param>
        /// <returns></returns>
        private List<PenaltyKickExChangeEntity> GetExChangeEntity(string exChangeString)
        {
            var resultList = new List<PenaltyKickExChangeEntity>();
            if (exChangeString.Length == 0)
                return null;
            var list = exChangeString.Split('|');
            foreach (var item in list)
            {
                var entity = new PenaltyKickExChangeEntity();
                var itemList = item.Split(',');
                entity.ItemCode = ConvertHelper.ConvertToInt(itemList[0]);
                entity.Price = ConvertHelper.ConvertToInt(itemList[1]);
                entity.Status = ConvertHelper.ConvertToInt(itemList[2]);
                resultList.Add(entity);
            }
            return resultList;
        }

        /// <summary>
        /// 设置兑换列表
        /// </summary>
        /// <param name="exChangeList"></param>
        /// <returns></returns>
        private string SetExChangeString(List<PenaltyKickExChangeEntity> exChangeList)
        {
            var resultString = "";
            foreach (var item in exChangeList)
            {
                resultString += item.ItemCode + "," + item.Price + "," + item.Status + "|";
            }
            if (resultString.Length > 0)
                resultString = resultString.Substring(0, resultString.Length - 1);
            return resultString;
        }

        #endregion

        #region 获取用户信息

        PenaltykickManagerEntity GetManager(Guid managerId)
        {
            var info = PenaltykickManagerMgr.GetById(managerId);
            DateTime date = DateTime.Now;
            if (info == null)
            {
                if (IsActivity)
                {
                    //默认1次免费次数,每天最多产出5个
                    info = new PenaltykickManagerEntity(managerId, 0, 1, 0, 5, 0, 0, 0, 0, "", 0, 0, RefreshExChange(),
                        0, 0,
                        false, date, date, date, date);
                    PenaltykickManagerMgr.Insert(info);
                }
                else 
                    info = new PenaltykickManagerEntity(managerId, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, RefreshExChange(),
                        0, 0,
                        false, date, date, date, date);
            }
            //每天补充免费次数
            if (info.RefreshDate.Date != date.Date)
            {
                if (IsActivity)
                {
                    info.FreeNumber = 1;
                    info.DayProduceLuckyCoin = 5;
                    info.RefreshDate = date;
                    PenaltykickManagerMgr.Update(info);
                }
            }
            return info;
        }

        #endregion

        #region 刷新兑换物品

        /// <summary>
        /// 刷新兑换物品
        /// </summary>
        /// <returns></returns>
        private string RefreshExChange()
        {
            return CacheFactory.AdCache.GetExChangeString();
        }

        #endregion

        #region 检测可获得的奖励

        /// <summary>
        /// 获取可获得的奖励
        /// </summary>
        /// <param name="goalsString">进球记录</param>
        /// <param name="maxCombGoals">最大连续进球数</param>
        /// <returns></returns>
        private List<PrizeEntity> GetPrize(string goalsString,int maxCombGoals)
        {
            var resultList = new List<PrizeEntity>();
            int goals = GetGoalsNumber(goalsString);
            //进球奖励
            var prizeList = CacheFactory.AdCache.GetPrize(1, goals);
            //连续进球奖励
            var combPrize =CacheFactory.AdCache.GetPrize(2, maxCombGoals);
            foreach (var item in prizeList)
            {
                var entity = new PrizeEntity();
                entity.ItemCode = item.ItemCode;
                entity.ItemCount = item.ItemCount;
                entity.ItemType = item.ItemType;
                resultList.Add(entity);
            }
            foreach (var item in combPrize)
            {
                var entity = new PrizeEntity();
                entity.ItemCode = item.ItemCode;
                entity.ItemCount = item.ItemCount;
                entity.ItemType = item.ItemType;
                resultList.Add(entity);
            }
            return resultList;
        }

        /// <summary>
        /// 获取进球数
        /// </summary>
        /// <param name="goalsString"></param>
        /// <returns></returns>
        private int GetGoalsNumber(string goalsString)
        {
            int number = 0;
            if (goalsString.Length > 0)
            {
                var list = goalsString.Split(',');
                foreach (var s in list)
                {
                    if (s == "1")
                        number++;
                }
            }
            return number;
        }

        #endregion

        #region 获取球队中射门属性最高的球员

        /// <summary>
        /// 获取射门属性最搞的球员属性
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        int GetShooterId(Guid managerId)
        {
            int shootProp = 0;
            var buffView = BuffDataCore.Instance().GetMembers(managerId);
            foreach (var item in buffView.BuffMembers.Values)
            {
                if (shootProp >= item.TotalShoot)
                    continue;
                shootProp = (int)item.TotalShoot;
            }
            return shootProp;
        }

        #endregion
    }
}
