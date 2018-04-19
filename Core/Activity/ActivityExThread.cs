using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.NBall;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Constellation;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response.Activity;
using Games.NBall.ServiceContract.Client;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Activity
{
    public partial class ActivityExThread
    {
        private List<TemplateActivityexgroupEntity> _activityGroupList;
        /// <summary>
        /// effectType->detail list
        /// </summary>
        private Dictionary<int, List<TemplateActivityexdetailEntity>> _effectDic;

        private List<TemplateActivityexEntity> _activityList;
        /// <summary>
        /// requireId->group list
        /// </summary>
        private Dictionary<int, List<TemplateActivityexgroupEntity>> _activityGroupRequireDic;
        /// <summary>
        /// activityId->group list
        /// </summary>
        private Dictionary<int, List<TemplateActivityexgroupEntity>> _activityGroupDic;
        /// <summary>
        /// activityId*10000+groupId*100+exstep->prizeList
        /// </summary>
        private Dictionary<int, List<TemplateActivityexprizeEntity>> _activityPrizeDic;

        private Dictionary<int, TemplateActivityexdetailEntity> _activityDetailDic;
        private ActivityClient _activityClient;
        /// <summary>
        /// 欧洲杯装备碎片
        /// </summary>
        private List<int> _europeEquipmentDebris;

        #region .ctor
        public ActivityExThread(int b)
        {
            try
            {
                InitCache();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExThread-InitCache", ex);
                throw ex;
            }

        }
        #endregion

        #region Facade

        #region Instance
        private bool _disableSchedule = false;
        private Timer _timer;
        private Timer _dailytimer;
        public static ActivityExThread Instance
        {
            get { return SingletonFactory<ActivityExThread>.SInstance; }
        }
        #endregion

        #region Start
        public void Start()
        {
            string disable = ConfigurationManager.AppSettings["DisableSchedule"];
            if (!string.IsNullOrEmpty(disable) && disable == "1")
            {
                _disableSchedule = true;
            }
            if (!_disableSchedule)
            {
                _timer = new Timer();
                _timer.Interval = 6000;
                _timer.Elapsed += Timer_Elapsed;
                _timer.Start();

                _dailytimer = new Timer();
                _dailytimer.Interval = 6000;
                _dailytimer.Elapsed += Timer_ElapsedDaily;
                _dailytimer.Start();
            }
        }
        #endregion

        #region GetActivityExList
        public ActivityExListResponse GetActivityExList()
        {
            var list = new List<TemplateActivityexEntity>();
            if (_activityList != null)
            {
                DateTime curTime = DateTime.Now;
                foreach (var activity in _activityList)
                {
                    if (activity.StartTime <= curTime && activity.EndTime >= curTime)
                    {
                        var newActivity = activity.Clone();
                        newActivity.ZoneActivityId = activity.ZoneActivityId;
                        newActivity.StartTimeTick = ShareUtil.GetTimeTick(activity.StartTime);
                        newActivity.EndTimeTick = ShareUtil.GetTimeTick(activity.EndTime);
                        list.Add(newActivity);
                    }
                }
            }
            var response = ResponseHelper.CreateSuccess<ActivityExListResponse>();
            response.Data = new ActivityExListEntity();
            response.Data.Activities = list;
            return response;
        }
        #endregion

        #region GetUserActivityEx
        public ActivityExRecordListResponse GetUserActivityEx(Guid managerId, int activityId)
        {
            if (!_activityGroupDic.ContainsKey(activityId))
            {
                return ResponseHelper.InvalidParameter<ActivityExRecordListResponse>();
            }
            var groupList = _activityGroupDic[activityId];
            if (groupList == null || groupList.Count <= 0)
                return ResponseHelper.InvalidParameter<ActivityExRecordListResponse>();
            if (groupList[0].ExRequireId == 33 || groupList[0].ExRequireId==(int)EnumActivityExRequire.EquipmentLockProperty)
            {
                SettlementConsume(activityId, managerId);
            }
            List<ActivityExRecordGroupEntity> returnGrpupList = null;
            if (groupList[0].ExcitingId == 7 || groupList[0].ExcitingId == 26 || groupList[0].ExcitingId == 32) //计数活动
                returnGrpupList = UserActivityExCount(managerId, groupList);
            else if (groupList[0].ExcitingId == 34)//活动结束统一发奖活动
                returnGrpupList = UserActivityExItem(managerId, groupList);
            else
                returnGrpupList = UserActivityEx(managerId, groupList);
            var response = ResponseHelper.CreateSuccess<ActivityExRecordListResponse>();
            response.Data = new ActivityExRecordListEntity();
            response.Data.Groups = returnGrpupList;
            return response;
        }


        List<ActivityExRecordGroupEntity> UserActivityExCount(Guid managerId, List<TemplateActivityexgroupEntity> groupList)
        {
            var returnGrpupList = new List<ActivityExRecordGroupEntity>(groupList.Count);

            DateTime curTime = DateTime.Now;
            int myData = 0;
            var userlist = ActivityexCountrecordMgr.GetByManagerList(managerId, groupList[0].ZoneActivityId);
            foreach (var group in groupList)
            {
                ActivityExRecordGroupEntity recordGroup = new ActivityExRecordGroupEntity();
                var entity = new ActivityexRecordEntity();
                recordGroup.GroupId = group.GroupId;
                var exRecord = userlist.Find(d => d.ZoneActivityId == group.ZoneActivityId && d.GroupId == group.GroupId);
                if (exRecord != null)
                {
                    exRecord.ExData = exRecord.ExData - exRecord.AlreadySendCount;
                    exRecord.ExData = exRecord.ExData < 0 ? 0 : exRecord.ExData;
                    myData = exRecord.ExData;
                    entity.CurData = exRecord.CurData;
                    entity.ExcitingId = exRecord.ExcitingId;
                    entity.ExData = exRecord.ExData;
                    entity.ExStep = exRecord.ExStep;
                    entity.GroupId = exRecord.GroupId;
                    entity.Idx = exRecord.Idx;
                    entity.ManagerId = exRecord.ManagerId;
                    entity.ZoneActivityId = exRecord.ZoneActivityId;
                }
                else
                {
                   entity.ExData = 0;
                   entity.ExStep = 0;
                   entity.ExcitingId = group.ExcitingId;
                   entity.GroupId = group.GroupId;
                   entity.ManagerId = managerId;
                   entity.RecordDate = curTime.Date;
                   entity.RowTime = curTime;
                   entity.ExcitingId = group.ExcitingId;
                   entity.Status = 0;
                   entity.CurData = 0;
                   entity.UpdateTime = curTime;
                   entity.ManagerId = managerId;
                   entity.ZoneActivityId = group.ZoneActivityId;
                }
                if (group.ExRequireId == (int) EnumActivityExRequire.ChargeCount) //充值计数活动  满RankCoun算一次
                {
                    entity.ExData = entity.ExData / group.RankCount;
                    if (group.RankCondition > 0)
                        entity.ExData = entity.ExData * group.RankCondition;
                    myData = entity.ExData;
                }
                entity.Status = myData > 0 ? 1 : 0;
                recordGroup.ExData = myData;
                recordGroup.ExRecord = entity;
                returnGrpupList.Add(recordGroup);
            }
            return returnGrpupList;
        }

        List<ActivityExRecordGroupEntity> UserActivityExItem(Guid managerId, List<TemplateActivityexgroupEntity> groupList)
        {
            var returnGrpupList = new List<ActivityExRecordGroupEntity>(groupList.Count);

            DateTime curTime = DateTime.Now;
            var userlist = ActivityexItemrecordMgr.GetByManagerList(managerId, groupList[0].ZoneActivityId);
            foreach (var group in groupList)
            {
                ActivityExRecordGroupEntity recordGroup = new ActivityExRecordGroupEntity();
                var entity = new ActivityexRecordEntity();
                recordGroup.GroupId = group.GroupId;
                var exRecord = userlist.Find(d => d.ZoneActivityId == group.ZoneActivityId && d.GroupId == group.GroupId);
                if (exRecord != null)
                {
                   
                    entity.ExcitingId = exRecord.ExcitingId;
                    entity.ItemString = exRecord.ItemString;
                    entity.GroupId = exRecord.GroupId;
                    entity.Idx = exRecord.Idx;
                    entity.ManagerId = exRecord.ManagerId;
                    entity.ZoneActivityId = exRecord.ZoneActivityId;
                }
                else
                {
                    entity.ExcitingId = group.ExcitingId;
                    entity.GroupId = group.GroupId;
                    entity.ManagerId = managerId;
                    entity.RecordDate = curTime.Date;
                    entity.RowTime = curTime;
                    entity.ExcitingId = group.ExcitingId;
                    entity.ItemString = "";
                    entity.UpdateTime = curTime;
                    entity.ManagerId = managerId;
                    entity.ZoneActivityId = group.ZoneActivityId;
                }
               
                entity.Status = 0;
                recordGroup.ExRecord = entity;
                returnGrpupList.Add(recordGroup);
            }
            return returnGrpupList;
        }

        List<ActivityExRecordGroupEntity> UserActivityEx(Guid managerId,List<TemplateActivityexgroupEntity> groupList)
        {
            var returnGrpupList = new List<ActivityExRecordGroupEntity>(groupList.Count);
           
            DateTime curTime = DateTime.Now;
            int myData = 0;
            var userlist = ActivityexRecordMgr.GetByActivityId(managerId, groupList[0].ZoneActivityId);
            foreach (var group in groupList)
            {
                ActivityExRecordGroupEntity recordGroup = new ActivityExRecordGroupEntity();
                recordGroup.GroupId = group.GroupId;

                if (group.IsRank)
                {
                    int num = group.RankCount;
                    if (num == 0)
                        num = 3;

                    if (group.ExRequireId == (int)EnumActivityExRequire.TopScoreCrossRank
                        || group.ExRequireId == (int)EnumActivityExRequire.ChargeCrossRank
                        || group.ExRequireId == (int)EnumActivityExRequire.ChargeDailyCrossRank)
                    {
                        var domainId = 0;
                      //  CrossSiteCache.Instance().TryGetDomainId(ShareUtil.ZoneName, out domainId);
                        var crossRankList = ActivityexCrossrankMgr.GetCrossRank(BuildExRankKey(group), group.RankCondition, num,
                            domainId, ShareUtil.ZoneName, managerId, ref myData);
                        //跨服排行转换为显示用的排行信息
                        recordGroup.Ranks = new List<ActivityexRankEntity>();
                        foreach (var crossRank in crossRankList)
                        {
                            ActivityexRankEntity rank = new ActivityexRankEntity()
                            {
                                Idx = crossRank.Idx,
                                ExData = crossRank.ExData,
                                ManagerId = crossRank.ManagerId,
                                Name = crossRank.Name,
                                RankKey = crossRank.RankKey,
                                RowTime = crossRank.RowTime,
                                Status = crossRank.Status,
                                UpdateTime = crossRank.UpdateTime
                            };
                            recordGroup.Ranks.Add(rank);
                        }
                    }
                    else
                        recordGroup.Ranks = ActivityexRankMgr.GetRank(BuildExRankKey(group), group.RankCondition, num, managerId, ref myData);
                    if (recordGroup.Ranks != null && recordGroup.Ranks.Count > 0)
                    {
                        for (int i = 0; i < recordGroup.Ranks.Count; i++)
                        {
                            recordGroup.Ranks[i].ExStep = i + 1;
                        }
                    }
                    recordGroup.ExData = myData;
                }
                else
                {
                    var exRecord = userlist.Find(d => d.ZoneActivityId == group.ZoneActivityId && d.GroupId == group.GroupId);
                    if (exRecord != null)
                    {
                        HandlerDailyExData(group, exRecord, curTime.Date, true);
                        if (group.ExRequireId == (int)EnumActivityExRequire.ChargeReturnPoint)
                        {
                            exRecord.ExData = exRecord.CurData;
                        }
                        if (group.ExcitingId == 80)
                        {
                            var manager = ManagerCore.Instance.GetManager(managerId);
                            PayConsumehistoryMgr.GetPointForActivity(manager.Account, group.StartTime, group.EndTime,
                                ref myData);
                            exRecord.ExData = myData;
                        }
                        if (group.ExcitingId == 91)
                        {
                            PayConsumehistoryMgr.GetEqLockPointForActivity(managerId, group.StartTime, group.EndTime,
                                ref myData);
                            exRecord.ExData = myData;
                        }
                    }
                    else
                    {
                        exRecord = new ActivityexRecordEntity();
                        exRecord.ExData = 0;
                        exRecord.ExStep = 0;
                        exRecord.ExcitingId = group.ExcitingId;
                        exRecord.GroupId = group.GroupId;
                        exRecord.ManagerId = managerId;
                        exRecord.ReceiveTimes = 0;
                        exRecord.RecordDate = curTime.Date;
                        exRecord.RowTime = curTime;
                        exRecord.Status = 0;
                        exRecord.UpdateTime = curTime;
                        exRecord.ZoneActivityId = group.ZoneActivityId;
                        //ActivityexRecordMgr.Insert(exRecord);
                    }
                    recordGroup.ExData = myData;
                    recordGroup.ExRecord = exRecord;
                }
                returnGrpupList.Add(recordGroup);
            }
            return returnGrpupList;
        }

        void HandlerDailyExData(TemplateActivityexgroupEntity group, ActivityexRecordEntity exRecord, DateTime recordDate, bool saveRecord)
        {
            if (group.StatisticCycle == (int)EnumActivityExStatisticCycle.Daily)
            {
                if (exRecord.RecordDate != recordDate && group.EndTime.Date >= recordDate)
                {
                    if (exRecord.Status < 2)
                    {
                        if (group.ExRequireId != (int)EnumActivityExRequire.LevelRank)
                        {
                            CalExStep(group, exRecord);
                            string exKey = BuildExKey(exRecord);
                            ActivityexPrizerecordEntity entity = new ActivityexPrizerecordEntity();
                            entity.CurData = exRecord.CurData;
                            entity.ExData = exRecord.ExData;
                            entity.ExKey = exKey;
                            entity.ExRecordId = exRecord.Idx;
                            entity.ExStep = exRecord.ExStep;
                            entity.ExcitingId = exRecord.ExcitingId;
                            entity.GroupId = exRecord.GroupId;
                            entity.ManagerId = exRecord.ManagerId;
                            entity.ReceiveTimes = exRecord.ReceiveTimes;
                            entity.RecordDate = exRecord.RecordDate;
                            entity.ReturnCode = 0;
                            entity.RowTime = DateTime.Now;
                            entity.SendTimes = 0;
                            entity.Status = exRecord.ExStep > 0 ? 1 : 0;
                            entity.UpdateTime = DateTime.Now;
                            entity.ZoneActivityId = exRecord.ZoneActivityId;
                            ActivityexPrizerecordMgr.Insert(entity);
                        }
                    }
                    exRecord.RecordDate = recordDate;
                    exRecord.CurData = 0;
                    exRecord.ExData = 0;
                    exRecord.Status = 0;
                    exRecord.ReceiveTimes = 0;
                    exRecord.ExStep = 0;
                    exRecord.UpdateTime = DateTime.Now;
                    if (saveRecord)
                    {
                        ActivityexRecordMgr.Update(exRecord);
                    }
                }
            }
        }

        #endregion

        #region PrizeReceive
        public ActivityExReceivePrizeResponse PrizeReceive(Guid managerId, int exRecordId,int itemCode = -1,int activityType=0)
        {
            List<ActivityPrizeEntity> receivePrizes = new List<ActivityPrizeEntity>();
            var messageCode = MessageCode.Success;
            if (activityType == 1)
                messageCode = PrizeReceiveCount(receivePrizes, exRecordId, managerId);
            else
                messageCode = PrizeReceive(receivePrizes, exRecordId, managerId, itemCode);
            if (messageCode != MessageCode.Success)
                return ResponseHelper.Create<ActivityExReceivePrizeResponse>(messageCode);
            var response = ResponseHelper.CreateSuccess<ActivityExReceivePrizeResponse>();
            response.Data = new ActivityExReceivePrizeEntity();
            response.Data.Prizes = receivePrizes;
            return response;
        }

        /// <summary>
        /// 计数活动发奖
        /// </summary>
        /// <param name="receivePrizes"></param>
        /// <param name="exRecordId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        private MessageCode PrizeReceiveCount(List<ActivityPrizeEntity> receivePrizes,int exRecordId,Guid managerId)
        {
            var exRecord = ActivityexCountrecordMgr.GetById(exRecordId);
            if (exRecord == null || exRecord.ManagerId != managerId)
            {
                return MessageCode.NbParameterError;
            }
            if (exRecord.AlreadySendCount >= exRecord.ExData)
                return MessageCode.ActivityHasReceive;

            var key = BuildActivityPrizeKey(exRecord.ZoneActivityId, exRecord.GroupId,
                exRecord.ExStep == 0 ? 1 : exRecord.ExStep);
            if (!_activityDetailDic.ContainsKey(key))
                return MessageCode.NbParameterError;
            var detail = _activityDetailDic[key];
            if (detail == null)
                return MessageCode.NbParameterError;
            int prizeItemcode = detail.EffectValue;
            int prizeItemCount = exRecord.ExData - exRecord.AlreadySendCount;
            int itemCount = 0;
            var groupList = _activityGroupDic[exRecord.ZoneActivityId];
            var group = groupList.Find(r => r.GroupId == exRecord.GroupId);
            if (group == null)
                return MessageCode.NbParameterError;
            if (group.ExRequireId == (int)EnumActivityExRequire.ChargeCount) //充值计数
            {
                itemCount = prizeItemCount / group.RankCount;
                exRecord.AlreadySendCount += (itemCount * group.RankCount);
                if (detail.EffectRate > 0)
                {
                    prizeItemCount = itemCount * detail.EffectRate;
                }
            }
            else
            {
                exRecord.AlreadySendCount += prizeItemCount;
            }
            MessageCode code = MessageCode.Success;
            ItemPackageFrame package = null;
            ScoutingGoldbarEntity goldBarManager = null;
            bool isInsert = false;
            if (prizeItemcode > 10000) //物品
            {
                package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ActivityExPrize);
                if (package == null || package.PackageSize < prizeItemCount)
                    return MessageCode.ItemPackageFull;
                code = package.AddItems(prizeItemcode, prizeItemCount);
                if (code != MessageCode.Success)
                    return code;
            }
            else
            {
                switch (prizeItemcode)
                {
                    case (int) EnumActivityExPrizeType.GoldBar: //金条
                        goldBarManager = ScoutingGoldbarMgr.GetById(managerId);
                        if (goldBarManager == null)
                        {
                            goldBarManager = new ScoutingGoldbarEntity(managerId, 0, 0, 0, 0, DateTime.Now, DateTime.Now);
                            isInsert = true;
                        }
                        goldBarManager.GoldBarNumber = goldBarManager.GoldBarNumber + prizeItemCount;
                        if (group.ExRequireId == (int) EnumActivityExRequire.ChargeCount)
                        {
                            package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ActivityExPrize);
                            if (package == null || package.PackageSize < itemCount)
                                return MessageCode.ItemPackageFull;
                            code = package.AddItems(310170, itemCount, false, true);
                            if (code != MessageCode.Success)
                                return code;
                        }
                        break;
                }
            }
            if (receivePrizes == null)
                receivePrizes = new List<ActivityPrizeEntity>();
            AddOutPrizes(receivePrizes, 2, prizeItemcode, prizeItemCount, 1, false);
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                code = MessageCode.FailUpdate;
                do
                {
                    if (!ActivityexCountrecordMgr.Update(exRecord, transactionManager.TransactionObject))
                        break;
                    if (package != null)
                    {
                        if (!package.Save(transactionManager.TransactionObject))
                            break;
                    }
                    if (goldBarManager != null)
                    {
                        if (isInsert)
                        {
                            if (!ScoutingGoldbarMgr.Insert(goldBarManager, transactionManager.TransactionObject))
                                break;
                        }
                        else
                        {
                            if (!ScoutingGoldbarMgr.Update(goldBarManager, transactionManager.TransactionObject))
                                break;
                        }

                    }
                    code = MessageCode.Success;
                } while (false);

                if (code != MessageCode.Success)
                    transactionManager.Rollback();
                else
                {
                    transactionManager.Commit();
                    if(package!=null)
                        package.Shadow.Save();
                }

            }
            return code;
        }

        /// <summary>
        /// 活动发奖
        /// </summary>
        /// <param name="receivePrizes"></param>
        /// <param name="exRecordId"></param>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private MessageCode PrizeReceive(List<ActivityPrizeEntity> receivePrizes, int exRecordId, Guid managerId,int itemCode)
        {
            var exRecord = ActivityexRecordMgr.GetById(exRecordId);
            if (exRecord == null || exRecord.ManagerId != managerId)
            {
                return MessageCode.NbParameterError;
            }
            if (exRecord.Status == 2)
                return MessageCode.ActivityHasReceive;
            if (exRecord.Status != 1)
                return MessageCode.ActivityNoPrize;

            var group =
                _activityGroupList.Find(
                    d => d.ZoneActivityId == exRecord.ZoneActivityId && d.GroupId == exRecord.GroupId);
            if (group == null)
                return MessageCode.NbParameterError;
            var code = BuildExRecordPrize(group, exRecord);
            if (code == MessageCode.Success)
            {
                int returnCode = -2;
                string exKey = BuildExKey(exRecord);
                ActivityexRecordMgr.SavePrize(exRecord.Idx, exKey, exRecord.CurData, exRecord.ExData,
                    exRecord.ExStep, exRecord.ReceiveTimes, exRecord.RecordDate, exRecord.Status,
                    exRecord.UpdateTime, exRecord.RowVersion, exRecord.NeedSaveHistory, ref returnCode);
                if (returnCode != 0)
                {
                    code = MessageCode.ActivityReceiveFail;
                }
                else
                {
                    var prize = ActivityexPrizerecordMgr.GetPrize(exKey);
                    if (prize == null)
                    {
                        code = MessageCode.ActivityReceiveFail;
                    }
                    else
                    {
                        code = SendPrize(prize, true, receivePrizes, "", null, itemCode);
                    }
                }
            }
            return code;
        }
        #endregion

        #region TeammemberReceive
        public ActivityExReceivePrizeResponse TeammemberReceive(Guid managerId, int exRecordId,int playerId)
        {
            var exRecord = ActivityexRecordMgr.GetById(exRecordId);
            if (exRecord == null || exRecord.ManagerId != managerId)
            {
                return ResponseHelper.InvalidParameter<ActivityExReceivePrizeResponse>();
            }
            if (exRecord.Status == 2)
                return ResponseHelper.Create<ActivityExReceivePrizeResponse>(MessageCode.ActivityHasReceive);
            if (exRecord.Status != 1)
                return ResponseHelper.Create<ActivityExReceivePrizeResponse>(MessageCode.ActivityNoPrize);

            var group =
                _activityGroupList.Find(
                    d => d.ZoneActivityId == exRecord.ZoneActivityId && d.GroupId == exRecord.GroupId);
            if (group == null)
                return ResponseHelper.InvalidParameter<ActivityExReceivePrizeResponse>("group");
            if (group.PrizeDic == null || group.PrizeDic.Count <= 0)
                return ResponseHelper.InvalidParameter<ActivityExReceivePrizeResponse>("PrizeDic");
            var groupPrize = group.PrizeDic[1][0];
            var playerCache = CacheFactory.PlayersdicCache.GetPlayer(playerId);
            if (playerCache == null)
                return ResponseHelper.InvalidParameter<ActivityExReceivePrizeResponse>("playerCache");
            if ((groupPrize.ThirdType != 0 && playerCache.CardLevel < groupPrize.ThirdType))
                return ResponseHelper.Create<ActivityExReceivePrizeResponse>(MessageCode.TeammemberCardLevelOver);
            if (playerCache.Kpi > groupPrize.MaxPower)
                return ResponseHelper.Create<ActivityExReceivePrizeResponse>(MessageCode.TeammemberPowerOver);
            var itemDic = CacheFactory.ItemsdicCache.GetItemByType(playerCache.Idx, EnumItemType.PlayerCard);
            if (itemDic == null)
                return ResponseHelper.InvalidParameter<ActivityExReceivePrizeResponse>("itemDic");
            List<ActivityPrizeEntity> receivePrizes = new List<ActivityPrizeEntity>();
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ActivityExPrize);
            if (package == null)
            {
                return ResponseHelper.InvalidParameter<ActivityExReceivePrizeResponse>("package");
            }
            var code = package.AddItem(itemDic.ItemCode, groupPrize.Strength1, groupPrize.IsBinding,false);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<ActivityExReceivePrizeResponse>(code);
            }
            AddOutPrizes(receivePrizes, 2, itemDic.ItemCode, 1, groupPrize.Strength1, groupPrize.IsBinding);
            exRecord.ReceiveTimes++;
            exRecord.Status = 2;
            code = SaveTeammemberPrize(package, exRecord);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<ActivityExReceivePrizeResponse>(code);
            }
            if (code == MessageCode.Success)
            {
                package.Shadow.Save();
            }
            var response = ResponseHelper.CreateSuccess<ActivityExReceivePrizeResponse>();
            response.Data = new ActivityExReceivePrizeEntity();
            response.Data.Prizes = receivePrizes;
            return response;
        }
        #endregion
        #endregion

        #region encapsulation
        ActivityexHistoryEntity BuildHistory(ActivityexRecordEntity entity)
        {
            ActivityexHistoryEntity history = new ActivityexHistoryEntity();
            history.ZoneActivityId = entity.ZoneActivityId;
            history.ExcitingId = entity.ExcitingId;
            history.GroupId = entity.GroupId;
            history.ExData = entity.ExData;
            history.ExStep = entity.ExStep;
            history.ReceiveTimes = entity.ReceiveTimes;
            history.ManagerId = entity.ManagerId;
            history.RecordDate = entity.RecordDate;
            history.Status = entity.Status;
            history.RowTime = entity.RowTime;
            history.UpdateTime = entity.UpdateTime;
            return history;
        }

        void InitCache()
        {
            _activityGroupDic = new Dictionary<int, List<TemplateActivityexgroupEntity>>();
            if (!CacheFactory.ServicetionSectionCache.HasActivityService())
            {
                _activityClient = new ActivityClient();
            }
            DateTime startTime = DateTime.MinValue;
            int zoneId = ShareUtil.ZoneId;
            _activityList = TemplateActivityexMgr.GetByZoneId(zoneId);
            var groups = TemplateActivityexgroupMgr.GetByZone(zoneId);
            if (groups == null)
            {groups=new List<TemplateActivityexgroupEntity>();}
            var details = TemplateActivityexdetailMgr.GetByZone(zoneId);
            if (details == null)
            {details = new List<TemplateActivityexdetailEntity>();}
            var prizes = TemplateActivityexprizeMgr.GetByZone(zoneId);
            if (prizes == null)
            {prizes = new List<TemplateActivityexprizeEntity>();}
            
            DateTime curTime = DateTime.Now;
            foreach (var activityexEntity in _activityList)
            {
                if (!(activityexEntity.Idx ==3 || activityexEntity.Idx == 5))
                {
                    if (activityexEntity.EndTime > curTime && activityexEntity.EndTime > startTime)
                        startTime = activityexEntity.EndTime;
                }
            }

            AddZeroActivity(startTime, groups, details, prizes);
            AddShareActivity(groups, details, prizes);

            var zoneActivityDic = _activityList.ToDictionary(d => d.ZoneActivityId, d => d);
            _effectDic = new Dictionary<int, List<TemplateActivityexdetailEntity>>();

            _activityGroupList = new List<TemplateActivityexgroupEntity>();
            _activityGroupRequireDic = new Dictionary<int, List<TemplateActivityexgroupEntity>>();
            if (groups.Count>0)
            {
                _activityDetailDic = details.ToDictionary(
                    d => BuildActivityPrizeKey(d.ZoneActivityId, d.GroupId, d.ExStep), d => d);
                _activityPrizeDic = new Dictionary<int, List<TemplateActivityexprizeEntity>>();
                foreach (var entity in groups)
                {
                    if (zoneActivityDic.ContainsKey(entity.ZoneActivityId))
                    {
                        var activity = zoneActivityDic[entity.ZoneActivityId];
                        entity.StartTime = activity.StartTime;
                        entity.EndTime = activity.EndTime;
                        entity.CloseTime = activity.CloseTime;

                        entity.Details = details.FindAll(d => d.ZoneActivityId == entity.ZoneActivityId && d.GroupId == entity.GroupId);
                        entity.PrizeDic = new Dictionary<int, List<TemplateActivityexprizeEntity>>();
                        var activityPrizes =
                            prizes.FindAll(d => d.ZoneActivityId == entity.ZoneActivityId && d.GroupId == entity.GroupId);
                        
                        foreach (var activityexprizeEntity in activityPrizes)
                        {
                            var prizeKey = BuildActivityPrizeKey(entity.ZoneActivityId, entity.GroupId,
                                                                 activityexprizeEntity.ExStep);
                            if (!entity.PrizeDic.ContainsKey(activityexprizeEntity.ExStep))
                            {
                                entity.PrizeDic.Add(activityexprizeEntity.ExStep,new List<TemplateActivityexprizeEntity>());
                                _activityPrizeDic.Add(prizeKey,new List<TemplateActivityexprizeEntity>());;
                            }
                            entity.PrizeDic[activityexprizeEntity.ExStep].Add(activityexprizeEntity);
                            _activityPrizeDic[prizeKey].Add(activityexprizeEntity);
                        }
                        
                        if (!_activityGroupRequireDic.ContainsKey(entity.ExRequireId))
                            _activityGroupRequireDic.Add(entity.ExRequireId, new List<TemplateActivityexgroupEntity>());
                        _activityGroupRequireDic[entity.ExRequireId].Add(entity);
                        if (!_activityGroupDic.ContainsKey(entity.ZoneActivityId))
                            _activityGroupDic.Add(entity.ZoneActivityId, new List<TemplateActivityexgroupEntity>());
                        _activityGroupDic[entity.ZoneActivityId].Add(entity);
                    }
                }
                _activityGroupList = groups.FindAll(d => d.ZoneActivityId > 0 && d.ActivityExType == 1);
                var effectList = groups.FindAll(d => d.ZoneActivityId > 0 && d.ActivityExType == 2);
                if (effectList != null)
                {
                    foreach (var entity in effectList)
                    {
                        foreach (var detail in entity.Details)
                        {
                            if (detail.EffectType > 0)
                            {
                                detail.StartTime = entity.StartTime;
                                detail.EndTime = entity.EndTime;
                                if (!_effectDic.ContainsKey(detail.EffectType))
                                    _effectDic.Add(detail.EffectType, new List<TemplateActivityexdetailEntity>());
                                _effectDic[detail.EffectType].Add(detail);
                            }
                        }
                    }
                }
            }

            _europeEquipmentDebris = new List<int>();
            var debrisString = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.EuropeEquipmentDebris);
            if (debrisString != null && debrisString.Length > 0)
            {
                var debrisList = debrisString.Split(',');
                foreach (var s in debrisList)
                {
                    _europeEquipmentDebris.Add(ConvertHelper.ConvertToInt(s));
                }
            }
        }

        void AddZeroActivity(DateTime startTime,List<TemplateActivityexgroupEntity> groups,List<TemplateActivityexdetailEntity> details,List<TemplateActivityexprizeEntity> prizes)
        {
            if (CacheFactory.SeasonCache.GetCurrentSeasonIndex() > 1)
            {
                var activities2 = TemplateActivityexMgr.GetByZoneId(0);
                if (activities2 != null)
                {
                    if (startTime != DateTime.MinValue)
                    {
                        startTime = startTime.Date.AddDays(1);
                        foreach (var entity in activities2)
                        {
                            if (entity.StartTime < startTime)
                            {
                                entity.StartTime = startTime;
                            }
                        }
                    }

                    _activityList.AddRange(activities2);
                    var groups2 = TemplateActivityexgroupMgr.GetByZone(0);
                    if (groups2 != null)
                    {
                        groups.AddRange(groups2);
                    }
                    var details2 = TemplateActivityexdetailMgr.GetByZone(0);
                    if (details2 != null)
                        details.AddRange(details2);
                    var prizes2 = TemplateActivityexprizeMgr.GetByZone(0);
                    if (prizes2 != null)
                        prizes.AddRange(prizes2);
                }
            }
        }

        void AddShareActivity(List<TemplateActivityexgroupEntity> groups, List<TemplateActivityexdetailEntity> details, List<TemplateActivityexprizeEntity> prizes)
        {
            var activities2 = TemplateActivityexMgr.GetByZoneId(1);
            if (activities2 != null)
            {
                _activityList.AddRange(activities2);
                var groups2 = TemplateActivityexgroupMgr.GetByZone(1);
                if (groups2 != null)
                {
                    groups.AddRange(groups2);
                }
                var details2 = TemplateActivityexdetailMgr.GetByZone(1);
                if (details2 != null)
                    details.AddRange(details2);
                var prizes2 = TemplateActivityexprizeMgr.GetByZone(1);
                if (prizes2 != null)
                    prizes.AddRange(prizes2);
            }
        }

        int BuildActivityPrizeKey(int zoneactivityId, int groupId, int exStep)
        {
            return zoneactivityId * 10000 + groupId * 100 + exStep;
        }

        string BuildExKey(ActivityexRecordEntity exRecord)
        {
            return string.Format("{0}_{1}_{2:yyyyMMdd}", exRecord.Idx, exRecord.ReceiveTimes, exRecord.RecordDate);
        }
        string BuildExKey(ActivityexItemrecordEntity exRecord)
        {
            return string.Format("{0}_{1:yyyyMMdd}", exRecord.Idx, exRecord.RecordDate);
        }

        public string BuildExRankKey(TemplateActivityexgroupEntity group)
        {
            DateTime recordDate = DateTime.Today;
            if (group.StatisticCycle != (int)EnumActivityExStatisticCycle.Daily)
                recordDate = group.EndTime.Date;
            return string.Format("{0}_{1}_{2:yyyyMMdd}", group.ZoneActivityId, group.GroupId, recordDate);
        }

        /// <summary>
        /// 排名活动发奖用Key，每日排名需生成前一天的Key
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public string BuildExRankSendKey(TemplateActivityexgroupEntity group)
        {
            DateTime recordDate = DateTime.Today.AddDays(-1);
            if (group.StatisticCycle != (int)EnumActivityExStatisticCycle.Daily)
                recordDate = group.EndTime.Date;
            return string.Format("{0}_{1}_{2:yyyyMMdd}", group.ZoneActivityId, group.GroupId, recordDate);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_disableSchedule)
                return;
            _timer.Stop();
            //SystemlogMgr.Info("ActivityExThread", "Timer_Elapsed");
            var nextTime = Run();

           // _timer.Interval = ShareUtil.CalTimerInterval(nextTime); //重新计算下次计算时间
            _timer.Start();
        }

        private void Timer_ElapsedDaily(object sender, ElapsedEventArgs e)
        {
            if (_disableSchedule)
                return;
            _dailytimer.Stop();
            //SystemlogMgr.Info("ActivityExThread", "Timer_ElapsedDaily");
            RunDaily();
            var nextTime = DateTime.Today.AddDays(1).AddMinutes(10);
            //_dailytimer.Interval = ShareUtil.CalTimerInterval(nextTime); //重新计算下次计算时间
            _dailytimer.Start();
        }

        public DateTime Run()
        {
            DateTime minTime = DateTime.Now.AddDays(1);
            DateTime curTime = DateTime.Now;
            if (_activityGroupList != null)
            {
                foreach (var group in _activityGroupList)
                {
                    if (!group.HasSend && group.StatisticCycle == (int)EnumActivityExStatisticCycle.During)
                    {
                        if (curTime >= group.EndTime)
                        {
                            if (curTime <= group.EndTime.AddDays(1))
                            {
                                int returnCode = 1;
                                ActivityexSendlogMgr.Check(group.ExcitingId, group.GroupId, group.EndTime.Date,
                                                           ref returnCode);
                                if (returnCode == 0)
                                {
                                    //活动结束统一发奖活动
                                    if (group.ExcitingId == 34)
                                    {
                                        SendGroupPrizeItem(group, group.EndTime.Date, false);
                                    }
                                    else
                                        SendGroupPrize(group, group.EndTime.Date, false);
                                    group.HasSend = true;
                                }
                            }
                        }
                        else
                        {
                            if (minTime > group.EndTime && curTime < group.EndTime)
                            {
                                minTime = group.EndTime;
                            }
                        }
                    }
                }
                RunSend();
            }
            return minTime;
        }

        public void RunDaily()
        {
            DateTime curDate = DateTime.Today;
            if (_activityGroupList != null)
            {
                foreach (var group in _activityGroupList)
                {
                    if (!group.HasSendDaily && group.StatisticCycle == (int)EnumActivityExStatisticCycle.Daily)
                    {
                        var maxDate = group.EndTime.Date.AddDays(1);
                        if (curDate > group.StartTime && curDate <= maxDate)
                        {
                            int returnCode = 1;
                            ActivityexSendlogMgr.Check(group.ExcitingId, group.GroupId, curDate,
                                                        ref returnCode);
                            if (returnCode == 0)
                            {
                                SendGroupPrize(group, curDate, true);
                                group.RecordDate = curDate;
                                group.HasSend = true;
                            }
                        }
                    }
                }
                RunSend();
            }
        }

        private static bool _isSending = false;

        public MessageCode RunSend()
        {
            if (_isSending)
            {
                return MessageCode.ActivityPrizeJobSending;
            }
            try
            {
                _isSending = true;
                var list = ActivityexPrizerecordMgr.GetForPrize();
                if (list != null && list.Count > 0)
                {
                    foreach (var entity in list)
                    {
                        MessageCode code = MessageCode.ActivityNoConfigPrize;

                        int oldStatus = entity.Status;
                        if (entity.Status != 1)
                        {
                            code = MessageCode.ActivityStatusNotSend;
                        }
                        else
                        {
                            code = SendPrize(entity, true, null);
                        }

                        if (code != MessageCode.Success)
                        {
                            entity.ReturnCode = (int) code;
                            entity.SendTimes++;
                            entity.Status = oldStatus;
                            ActivityexPrizerecordMgr.Update(entity);
                        }
                    }
                }
                return MessageCode.Success;
            }
            finally
            {
                _isSending = false;
            }
        }

        void SendGroupPrize(TemplateActivityexgroupEntity group, DateTime recordDate, bool isDaily)
        {
            if (group.IsRank)
            {
                if (group.ExRequireId == (int) EnumActivityExRequire.TopScoreCrossRank
                    || group.ExRequireId == (int)EnumActivityExRequire.ChargeCrossRank
                    || group.ExRequireId == (int)EnumActivityExRequire.ChargeDailyCrossRank)
                    SendCrossRankPrize(group, recordDate, isDaily);
                else 
                    SendRankPrize(group, recordDate, isDaily);
                return;
            }
            SettlementConsume(group, isDaily, recordDate);
            List<ActivityexRecordEntity> exRecordList = null;
            if (group.ExRequireId == (int)EnumActivityExRequire.LevelRank)
            {
                exRecordList = ActivityexRecordMgr.GetManagerRank(group.ZoneActivityId, group.ExcitingId, group.GroupId,
                                                                  recordDate);
            }
            else
            {
                exRecordList = ActivityexRecordMgr.GetForSend(group.ZoneActivityId, group.GroupId);
            }
            if (exRecordList != null)
            {
                foreach (var exRecord in exRecordList)
                {
                    if (isDaily && exRecord.RecordDate >= recordDate)
                        continue;

                    if (exRecord.Status != 2)
                    {
                        try
                        {
                            if (group.ExRequireId != (int)EnumActivityExRequire.LevelRank)
                            {
                                CalExStep(group, exRecord);
                            }
                            var code = BuildExRecordPrize(group, exRecord);
                            if (code == MessageCode.Success)
                            {
                                int returnCode = -2;
                                string exKey = BuildExKey(exRecord);
                                ActivityexRecordMgr.SavePrize(exRecord.Idx, exKey, exRecord.CurData, exRecord.ExData, exRecord.ExStep, exRecord.ReceiveTimes, exRecord.RecordDate, exRecord.Status, exRecord.UpdateTime, exRecord.RowVersion, exRecord.NeedSaveHistory, ref returnCode);
                            }
                        }
                        catch (Exception ex)
                        {
                            SystemlogMgr.Error("SendGroupPrize-Loop Record", ex);
                        }

                    }
                }
            }
            ActivityexSendlogMgr.Insert(new ActivityexSendlogEntity()
            {
                ExcitingId = group.ExcitingId,
                GroupId = group.GroupId,
                RecordDate = recordDate,
                RowTime = DateTime.Now
            });
        }

        void SendGroupPrizeItem(TemplateActivityexgroupEntity group, DateTime recordDate, bool isDaily)
        {
            var exRecordList = ActivityexItemrecordMgr.GetForSend(group.ZoneActivityId, group.GroupId);
            if (exRecordList != null)
            {
                foreach (var exRecord in exRecordList)
                {
                    if (isDaily && exRecord.RecordDate >= recordDate)
                        continue;

                    if (exRecord.Status != 2)
                    {
                        try
                        {
                            string title = "";
                            var activity = TemplateActivityexMgr.GetById(exRecord.ExcitingId);
                            if (activity != null)
                                title = activity.Name;
                            var mail = new MailBuilder(exRecord.ManagerId, title);
                            if (exRecord.ItemString.Length == 0)
                                continue;
                            var itemSring = exRecord.ItemString.Split('|');
                            foreach (var s in itemSring)
                            {
                                if (s.Length <= 0)
                                    continue;
                                var item = s.Split(',');
                                var itemType = ConvertHelper.ConvertToInt(item[0]);
                                var itemCode = ConvertHelper.ConvertToInt(item[1]);
                                var itemCount = ConvertHelper.ConvertToInt(item[2]);
                                switch (itemType)
                                {
                                    case 1:
                                        mail.AddAttachment(EnumCurrencyType.Point, itemCount);
                                        break;
                                    case 2:
                                        mail.AddAttachment(EnumCurrencyType.Coin, itemCount);
                                        break;
                                    case 3:
                                        mail.AddAttachment(itemCount, itemCode, false, 1);
                                        break;
                                }
                            }
                            exRecord.Status = 2;
                            exRecord.UpdateTime = DateTime.Now;
                            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                            {
                                transactionManager.BeginTransaction();
                                var messageCode = MessageCode.NbUpdateFail;
                                do
                                {
                                    if (!mail.Save(transactionManager.TransactionObject))
                                        break;
                                    if (!ActivityexItemrecordMgr.Update(exRecord, transactionManager.TransactionObject))
                                        break;
                                    messageCode = MessageCode.Success;
                                } while (false); 
                                if (messageCode == MessageCode.Success)
                                {
                                    transactionManager.Commit();
                                }
                                else
                                {
                                    transactionManager.Rollback();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            SystemlogMgr.Error("SendGroupPrize-Loop Record", ex);
                        }

                    }
                }
            }

            ActivityexSendlogMgr.Insert(new ActivityexSendlogEntity()
            {
                ExcitingId = group.ExcitingId,
                GroupId = group.GroupId,
                RecordDate = recordDate,
                RowTime = DateTime.Now
            });
        }

       public MessageCode CheckVip(ActivityexRecordEntity exRecord)
        {
            if (CheckVipId(exRecord))
            {
                var detailKey = BuildActivityPrizeKey(exRecord.ZoneActivityId, exRecord.GroupId, exRecord.ExStep);
                if (_activityDetailDic.ContainsKey(detailKey))
                {
                    var vipLevel = ManagerUtil.GetVipLevel(exRecord.ManagerId);
                    if (vipLevel < _activityDetailDic[detailKey].Condition)
                    {
                        return MessageCode.LackofVipLevel;
                    }
                }
                else
                {
                    return MessageCode.NbParameterError;
                }
            }
            return MessageCode.Success;
        }

        bool CheckVipId(ActivityexRecordEntity exRecord)
        {
            if (exRecord.ExcitingId > 100 && exRecord.ExcitingId < 200)
            {
                return true;
            }
            if (exRecord.ExcitingId == 73)
                return true;
            return false;
        }

        void SendRankPrize(TemplateActivityexgroupEntity group, DateTime recordDate, bool isDaily)
        {
            DateTime curTime = DateTime.Now;
            int num = group.RankCount;
            if(num == 0)
               num = 3;
            int myData = 0;
            var rankList = ActivityexRankMgr.GetRank(BuildExRankKey(group), group.RankCondition,num,Guid.Empty,ref myData);
            if (rankList != null)
            {
                for (int i = 0; i < rankList.Count; i++)
                {
                    try
                    {
                        int rank = i + 1;
                        var rankEntity = rankList[i];
                        ActivityexPrizerecordEntity entity = new ActivityexPrizerecordEntity();
                        entity.CurData = rankEntity.ExData;
                        entity.ExData = rankEntity.ExData;
                        entity.ExKey = "rank_" + rankEntity.RankKey;
                        entity.ExRecordId = rankEntity.Idx;
                        entity.ExStep = rank;
                        entity.ExcitingId = group.ExcitingId;
                        entity.GroupId = group.GroupId;
                        entity.ManagerId = rankEntity.ManagerId;
                        entity.ReceiveTimes = 1;
                        entity.RecordDate = recordDate;
                        entity.ReturnCode = 0;
                        entity.RowTime = curTime;
                        entity.SendTimes = 0;
                        entity.Status = 1;
                        entity.UpdateTime = curTime;
                        entity.ZoneActivityId = group.ZoneActivityId;
                        ActivityexPrizerecordMgr.Insert(entity);
                    }
                    catch (Exception ex)
                    {
                        SystemlogMgr.Error("SendGroupPrize-Loop Rank", ex);
                    }
                }
            }
            ActivityexSendlogMgr.Insert(new ActivityexSendlogEntity()
            {
                ExcitingId = group.ExcitingId,
                GroupId = group.GroupId,
                RecordDate = recordDate,
                RowTime = DateTime.Now
            });
        }


        void SendCrossRankPrize(TemplateActivityexgroupEntity group, DateTime recordDate, bool isDaily)
        {
            DateTime curTime = DateTime.Now;
            int num = group.RankCount;
            if (num == 0)
                num = 3;
            int myData = 0;
            var domainId = 0;
            //CrossSiteCache.Instance().TryGetDomainId(ShareUtil.ZoneName, out domainId);
            var rankList = ActivityexCrossrankMgr.GetCrossRank(BuildExRankSendKey(group), group.RankCondition, num, domainId, ShareUtil.ZoneName, Guid.Empty, ref myData);
            if (rankList != null)
            {
                for (int i = 0; i < rankList.Count; i++)
                {
                    try
                    {
                        int rank = i + 1;
                        var rankEntity = rankList[i];
                        //跨服排行活动，只发本服有排名的玩家奖励
                        if (rankEntity.SiteId != ShareUtil.ZoneName)
                            continue;

                        ActivityexPrizerecordEntity entity = new ActivityexPrizerecordEntity();
                        entity.CurData = rankEntity.ExData;
                        entity.ExData = rankEntity.ExData;
                        entity.ExKey = "rank_" + rankEntity.RankKey;
                        entity.ExRecordId = rankEntity.Idx;
                        entity.ExStep = rank;
                        entity.ExcitingId = group.ExcitingId;
                        entity.GroupId = group.GroupId;
                        entity.ManagerId = rankEntity.ManagerId;
                        entity.ReceiveTimes = 1;
                        entity.RecordDate = recordDate;
                        entity.ReturnCode = 0;
                        entity.RowTime = curTime;
                        entity.SendTimes = 0;
                        entity.Status = 1;
                        entity.UpdateTime = curTime;
                        entity.ZoneActivityId = group.ZoneActivityId;
                        ActivityexPrizerecordMgr.Insert(entity);
                    }
                    catch (Exception ex)
                    {
                        SystemlogMgr.Error("SendGroupPrize-Loop Rank", ex);
                    }
                }
            }
            ActivityexSendlogMgr.Insert(new ActivityexSendlogEntity()
            {
                ExcitingId = group.ExcitingId,
                GroupId = group.GroupId,
                RecordDate = recordDate,
                RowTime = DateTime.Now
            });
        }



        MessageCode BuildExRecordPrize(TemplateActivityexgroupEntity group, ActivityexRecordEntity exRecord)
        {
            if (exRecord.ExStep == 0)
            {
                return MessageCode.ActivityNoPrize;
            }
            if (exRecord.Status == 2)
            {
                return MessageCode.ActivityHasReceive;
            }
            try
            {
                var code = CheckVip(exRecord);
                if (code != MessageCode.Success)
                {
                    exRecord.ExStep = 0;
                    exRecord.Status = 0;
                    exRecord.UpdateTime = DateTime.Now;
                    return code;
                }
                if (group.ExRequireId == (int)EnumActivityExRequire.ChargeReturnPoint)
                {
                    exRecord.NeedSaveHistory = true;
                    exRecord.CurData = 0;
                    exRecord.Status = 0;
                    exRecord.UpdateTime = DateTime.Now;
                }
                else
                {
                    exRecord.Status = 2;
                    exRecord.UpdateTime = DateTime.Now;
                }
                if (group.StatisticCycle == (int)EnumActivityExStatisticCycle.Daily)
                {
                    exRecord.Status = 2;
                    exRecord.NeedSaveHistory = true;
                }
                exRecord.ReceiveTimes++;
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExThread SendPrize", ex);
                return MessageCode.Exception;
            }
        }

        private MessageCode SendPrize(ActivityexPrizerecordEntity exPrize, bool sendMail,
            List<ActivityPrizeEntity> receivePrizes, string zoneName = "",
            Dictionary<int, List<TemplateActivityexprizeEntity>> prizeDic = null, int userItemCode = -1)
        {
            if (exPrize.Status == 2)
            {
                return MessageCode.ActivityHasReceive;
            }
            try
            {
                var prizeKey = BuildActivityPrizeKey(exPrize.ZoneActivityId, exPrize.GroupId, exPrize.ExStep);
                if (_activityPrizeDic == null || !_activityPrizeDic.ContainsKey(prizeKey))
                {
                    return MessageCode.ActivityNoConfigPrize;
                }
                List<TemplateActivityexprizeEntity> prizeList = _activityPrizeDic[prizeKey];
                if (prizeList == null || prizeList.Count <= 0)
                {
                    return MessageCode.ActivityConfigPrizeIsNull;
                }
                if (receivePrizes == null)
                    receivePrizes = new List<ActivityPrizeEntity>(prizeList.Count);
                var manager = ManagerCore.Instance.GetManager(exPrize.ManagerId, zoneName);
                ItemPackageFrame package = null;
                PayUserEntity payUser = null;
                MailBuilder mail = null;
                ConstellationPackbager cpackage = null;
                bool isupdateCpackage = false;
                int addPrestige = 0;
                int bindPoint = 0;
                int addHonor = 0;
                int addGameCurrency = 0;
                int addGoldBar = 0;
                var code = BuildPrize(exPrize.ManagerId, manager.Account, exPrize.Idx, exPrize.CurData,
                    exPrize.ExData, prizeList, manager, ref payUser, receivePrizes, ref addPrestige, ref bindPoint,
                    ref addHonor,ref addGameCurrency,ref addGoldBar,
                    zoneName, userItemCode);
                if (code == MessageCode.Success)
                {
                    var items = receivePrizes.FindAll(d => d.Type == 2);
                    if (exPrize.ExcitingId == 4)
                    {
                        mail = new MailBuilder(exPrize.ManagerId, "等级大排名");
                        foreach (var item in items)
                        {
                            mail.AddAttachment(item.Count, item.ItemCode, item.IsBinding, item.Strength);
                        }
                    }
                    else
                        code = AddItems(exPrize.ManagerId, items, exPrize.ExcitingId, sendMail, ref package,
                            ref cpackage, ref isupdateCpackage, ref mail, zoneName);
                    if (code != MessageCode.Success)
                    {
                        LogHelper.Insert(
                            string.Format("ActivityExThread AddItems fail,exprize:{0},groupid:{1},exStep:{2}",
                                exPrize.Idx, exPrize.GroupId, exPrize.ExStep), LogType.Info);
                        return code;
                    }
                }
                else
                {
                    LogHelper.Insert(
                        string.Format("ActivityExThread SavePrize fail,exprize:{0},groupid:{1},exStep:{2}",
                            exPrize.Idx, exPrize.GroupId, exPrize.ExStep), LogType.Info);
                    return code;
                }
                exPrize.Status = 2;
                code = SavePrize(exPrize, package, manager, payUser, mail, addPrestige, cpackage,
                        isupdateCpackage,
                        bindPoint, addHonor, addGameCurrency, addGoldBar, zoneName);
                if (code != MessageCode.Success)
                {
                    LogHelper.Insert(
                        string.Format("ActivityExThread SavePrize fail,exprize:{0},groupid:{1},exStep:{2}",
                            exPrize.Idx, exPrize.GroupId, exPrize.ExStep), LogType.Info);
                }
                else
                {
                    if (package != null && package.Shadow != null)
                    {
                        package.Shadow.Save();
                    }
                    if (manager != null && manager.AddCoin > 0)
                    {
                        ManagerUtil.SaveManagerAfter(manager);
                    }
                }
                return code;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ActivityExThread SendPrize", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode AddItems(Guid managerId, List<ActivityPrizeEntity> prizeItems, int activityId, bool sendMail, ref ItemPackageFrame package, ref ConstellationPackbager cpackage, ref bool isupdateCpackager, ref MailBuilder mail, string zoneName = "")
        {
            if (prizeItems != null && prizeItems.Count > 0)
            {
                package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ActivityExPrize, zoneName);
                MessageCode code = MessageCode.NbUpdateFail;
                foreach (var item in prizeItems)
                {
                    code = package.AddItems(item.ItemCode, item.Count, item.Strength, item.IsBinding, item.IsBinding,
                            item.SlotColorCount);
                    if (code != MessageCode.Success)
                    {
                        break;
                    }
                }
                if (code != MessageCode.Success && sendMail)
                {
                    package = null;
                    var activity = _activityList.Find(d => d.Idx == activityId);
                    string activityname = activity == null ? "" : activity.Name;
                    mail = new MailBuilder(managerId, activityname);
                    foreach (var item in prizeItems)
                    {
                        var itemInfo = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                        if (itemInfo.ItemType == (int) EnumItemType.Equipment)
                        {
                            
                            var itemProperty = CacheFactory.EquipmentCache.RandomEquipmentProperty(itemInfo.LinkId,
                                item.Strength, item.SlotColorCount);
                            if (itemProperty == null)
                                return MessageCode.ItemEquipmentNotExists;
                            if (item.Strength > 1)
                            {
                                itemProperty.Level = item.Strength;
                            }
                            mail.AddAttachment(item.Count, item.ItemCode, item.IsBinding, itemProperty);
                        }
                        else
                        {
                            mail.AddAttachment(item.Count, item.ItemCode, item.IsBinding, item.Strength);
                        }

                    }
                }
                else
                {
                    return code;
                }
            }
            return MessageCode.Success;
        }

        MessageCode BuildPrize(Guid managerId, string account, int exRecordId, int curData, int exData, List<TemplateActivityexprizeEntity> prizeList, NbManagerEntity manager, ref PayUserEntity payUser, List<ActivityPrizeEntity> prizes, ref int addPrestige, ref int bindPoint, ref int addHonor, ref int addGameCurrency, ref int addGoldBar, string zoneName = "", int userItemCode = -1)
        {
            if (prizeList == null)
                return MessageCode.NbParameterError;
            MessageCode code = MessageCode.Success;
            foreach (var entity in prizeList)
            {
                switch (entity.PrizeType)
                {
                    case (int)EnumActivityExPrizeType.Coin:
                        if (manager == null)
                        {
                            manager = ManagerCore.Instance.GetManager(managerId, zoneName);
                        }
                        ManagerUtil.AddManagerDataCoin(manager, entity.Count, EnumCoinChargeSourceType.ActivityPrize, exRecordId.ToString());
                        AddOutPrizes(prizes, 1, entity.Count);
                        break;
                    case (int)EnumActivityExPrizeType.Point:
                        if (payUser == null)
                        {
                            payUser = PayCore.Instance.GetPayUser(account, zoneName);
                        }
                        payUser.Bonus += entity.Count;
                        payUser.AddPoint += entity.Count;
                        AddOutPrizes(prizes, 3, entity.Count);
                        break;
                    case (int)EnumActivityExPrizeType.ReturnPoint:
                        if (curData > 0)
                        {
                            if (payUser == null)
                            {
                                payUser = PayCore.Instance.GetPayUser(account, zoneName);
                            }
                            int re = entity.Count * curData / 100;
                            payUser.Bonus += re;
                            payUser.AddPoint += re;
                            AddOutPrizes(prizes, 3, re);
                        }
                        break;
                    case (int)EnumActivityExPrizeType.RandomItem:
                        int itemCode = 0;
                        if (entity.SubType == 1)
                        {
                            int maxPower = entity.MaxPower;
                            if (maxPower == 0)
                                maxPower = 2000;
                            DicItemEntity itemCache = null;
                            if (entity.ThirdType == 0)
                                itemCache = CacheFactory.ItemsdicCache.RandomPlayerCard(entity.MinPower, maxPower);
                            else
                                itemCache = CacheFactory.ItemsdicCache.RandomPlayerCard(entity.ThirdType,
                                    entity.MinPower, maxPower);
                            if (itemCache == null)
                            {
                                return MessageCode.NbUpdateFail;
                            }
                            itemCode = itemCache.ItemCode;
                            AddOutPrizes(prizes, 2, itemCode, 1, entity.Strength1, entity.IsBinding);
                            if (entity.Strength2 > 0)
                            {
                                AddOutPrizes(prizes, 2, itemCode, 1, entity.Strength2, true);
                            }
                        }
                        else if (entity.SubType == 2)
                        {
                            var equipCache = CacheFactory.ItemsdicCache.RandomEquipment(entity.ThirdType);
                            if (equipCache == null)
                            {
                                return MessageCode.NbUpdateFail;
                            }
                            itemCode = equipCache.ItemCode;
                            AddOutPrizes(prizes, 2, itemCode, entity.Count, entity.Strength1, entity.IsBinding, entity.Strength2);
                        }
                        else if (entity.SubType == 3)
                        {
                            //var club = CacheFactory.ClubClothesCache.RandomQuality(entity.ThirdType);
                            //itemCode = 700000 + club.Idx;
                            //AddOutPrizes(prizes, 2, itemCode, entity.Count, entity.Strength1, entity.IsBinding, entity.Strength2);

                        }
                        else if (entity.SubType > 10 && entity.SubType < 100)
                        {
                            var lottery = LotteryCache.Instance.Lottery(EnumLotteryType.ActivityEx, entity.SubType);
                            if (lottery != null)
                            {
                                itemCode = lottery.PrizeItemCode;
                                AddOutPrizes(prizes, 2, itemCode, entity.Count, lottery.Strength, entity.IsBinding);
                            }
                            else
                                return MessageCode.NbUpdateFail;
                        }
                        else
                        {
                            return MessageCode.NbUpdateFail;
                        }
                        if (code != MessageCode.Success)
                            return code;
                        break;
                    case (int)EnumActivityExPrizeType.Item:
                        AddOutPrizes(prizes, 2, entity.SubType, entity.Count, entity.Strength1, entity.IsBinding, entity.Strength2);
                        break;
                    case (int)EnumActivityExPrizeType.RandomSuit:
                        List<int> suitlist = null;
                        if (entity.SubType > 10)
                        {
                            suitlist = CacheFactory.LotteryCache.LotteryEquipmentSuitRange(entity.SubType,
                                entity.ThirdType);
                        }
                        else
                        {
                            suitlist = CacheFactory.LotteryCache.LotteryEquipmentSuitListByType(entity.SubType,
                                                                                            entity.ThirdType);
                        }
                        if (suitlist != null && suitlist.Count > 0)
                        {
                            if (entity.Count == 1)
                            {
                                var index = RandomHelper.GetInt32WithoutMax(0, suitlist.Count);
                                AddOutPrizes(prizes, 2, suitlist[index], 1, entity.Strength1, entity.IsBinding, entity.Strength2);
                            }
                            else
                            {
                                if (entity.ExcitingId == 67) //需要抽取2件以上三彩孔装备
                                {
                                    int colorCount = RandomHelper.GetInt32(2, 7);
                                    var colorSuitList = RandomSuitColorList(suitlist, colorCount);

                                    foreach (var suit in suitlist)
                                    {
                                        if (colorSuitList.Contains(suit))
                                            AddOutPrizes(prizes, 2, suit, entity.Count, entity.Strength1, entity.IsBinding, 3);
                                        else
                                            AddOutPrizes(prizes, 2, suit, entity.Count, entity.Strength1, entity.IsBinding, entity.Strength2);
                                    }
                                }
                                else
                                {
                                    foreach (var suit in suitlist)
                                    {
                                        AddOutPrizes(prizes, 2, suit, entity.Count, entity.Strength1, entity.IsBinding, entity.Strength2);
                                    }
                                }
                               
                            }
                        }
                        break;
                    case (int)EnumActivityExPrizeType.Prestige:
                        addPrestige += entity.Count;
                        break;
                    case (int)EnumActivityExPrizeType.Experience:
                        if (manager == null)
                        {
                            manager = ManagerCore.Instance.GetManager(managerId, zoneName);
                        }
                        manager.Sophisticate = manager.Sophisticate + entity.Count;
                        break;
                    case (int)EnumActivityExPrizeType.Teammember:
                        return MessageCode.NbParameterError;
                        break;
                    case (int)EnumActivityExPrizeType.BindPoint:
                        if (manager == null)
                        {
                            manager = ManagerCore.Instance.GetManager(managerId, zoneName);
                        }
                        bindPoint += entity.Count;
                        AddOutPrizes(prizes, 8, entity.Count);
                        break;
                    case (int)EnumActivityExPrizeType.ItemCode:
                        if(userItemCode == -1 || userItemCode == 0)
                            return MessageCode.UserNotItemCode;
                        var item = CacheFactory.ItemsdicCache.GetItem(userItemCode);
                        if (item == null)
                            return MessageCode.ItemNotExists;
                        AddOutPrizes(prizes, 2, userItemCode, entity.Count, entity.Strength1, entity.IsBinding,
                            entity.Strength2);
                        break;
                    case (int)EnumActivityExPrizeType.Honor:
                        addHonor = entity.Count;
                        AddOutPrizes(prizes, 11, entity.Count);
                        break;
                    case (int)EnumActivityExPrizeType.GameCurrency:
                        addGameCurrency = entity.Count;
                        AddOutPrizes(prizes, 99, entity.Count);
                        break;
                    case (int)EnumActivityExPrizeType.GoldBar:
                        addGoldBar = entity.Count;
                        AddOutPrizes(prizes, 12, entity.Count);
                        break;
                    default:
                        return MessageCode.NbParameterError;
                        break;
                }
            }
            return MessageCode.Success;
        }

        private List<int> RandomSuitColorList(List<int> suitList,int count )
        {
            if (suitList.Count <= count)
                return suitList;

            List<int> colorList = new List<int>();
            for (int i = 0; i < count; i++)
            {
                var suit = suitList[RandomHelper.GetInt32WithoutMax(0, suitList.Count)];
                while (colorList.Contains(suit))
                {
                    suit = suitList[RandomHelper.GetInt32WithoutMax(0, suitList.Count)];
                }
                colorList.Add(suit);
            }
            return colorList;
        }

        private void AddOutPrizes(List<ActivityPrizeEntity> prizes, int type, int count)
        {
            AddOutPrizes(prizes, type, 0, count, 0, false);
        }

        void AddOutPrizes(List<ActivityPrizeEntity> prizes, int type, int itemCode, int count, int strength, bool isBinding, int slotColorCount = 0)
        {
            if (prizes != null)
            {
                prizes.Add(new ActivityPrizeEntity() { Type = type, Count = count, IsBinding = isBinding, Strength = strength, ItemCode = itemCode,SlotColorCount = slotColorCount});
            }
        }
        #endregion

        #region SavePrize

        public MessageCode SavePrize(ActivityexPrizerecordEntity exPrize, ItemPackageFrame package, NbManagerEntity manager, PayUserEntity payUser, MailBuilder mail, int addPrestige, ConstellationPackbager cpackage, bool isupdateCpackage, int bindpoint, int addHonor, int addGameCurrency, int addGoldBar, string zoneName = "")
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SavePrize(transactionManager.TransactionObject, exPrize, package, manager, payUser, mail, addPrestige, cpackage, isupdateCpackage, bindpoint, addHonor, addGameCurrency, addGoldBar, zoneName);
                    if (messageCode == MessageCode.Success)
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
                SystemlogMgr.Error("SavePrize", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_SavePrize(DbTransaction transaction, ActivityexPrizerecordEntity exPrize, ItemPackageFrame package, NbManagerEntity manager, PayUserEntity payUser, MailBuilder mail, int addPrestige, ConstellationPackbager cpackage, bool isupdateCpackage, int bindpoint, int addHonor, int addGameCurrency,int addGoldBar, string zoneName = "")
        {
            if (!ActivityexPrizerecordMgr.Update(exPrize, transaction, zoneName))
            {
                return MessageCode.NbUpdateFail;
            }
            if (package != null)
            {
                if (!package.Save(transaction))
                    return MessageCode.NbUpdateFailPackage;
            }
            if (isupdateCpackage)
            {
                if(cpackage!= null)
                    if (!cpackage.Save(transaction, zoneName))
                        return MessageCode.NbUpdateFailPackage;
            }
            if (manager != null)
            {
                if (!ManagerUtil.SaveManagerData(manager, null, transaction, zoneName))
                {
                    return MessageCode.NbUpdateFailManager;
                }
            }
            if (payUser != null && payUser.AddPoint > 0)
            {
                var code = PayCore.Instance.AddBonus(payUser.Account, payUser.AddPoint, EnumChargeSourceType.ActivityExPrize,
                                                     "ActivityexPrizerecord_" + exPrize.Idx.ToString(), transaction, zoneName);
                if (code != MessageCode.Success)
                    return code;
            }
            if (mail != null)
            {
                if (!mail.Save(zoneName,transaction))
                    return MessageCode.NbUpdateFail;
            }
           
            if (addHonor > 0)
            {
                if (!LadderManagerMgr.AddHonor(manager.Idx, addHonor, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (addGameCurrency > 0)
            {
                if (!PenaltykickManagerMgr.AddGameCurrency(manager.Idx, addGameCurrency, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (addGoldBar > 0)
            {
                var goldBarManager = ScoutingGoldbarMgr.GetById(exPrize.ManagerId);
                if (goldBarManager == null)
                {
                    goldBarManager = new ScoutingGoldbarEntity(exPrize.ManagerId, addGoldBar, 0, 0, 0, DateTime.Now,
                        DateTime.Now);
                    if (!ScoutingGoldbarMgr.Insert(goldBarManager, transaction))
                        return MessageCode.NbUpdateFail;
                }
                else
                {
                    goldBarManager.GoldBarNumber = goldBarManager.GoldBarNumber + addGoldBar;
                    if (!ScoutingGoldbarMgr.Update(goldBarManager, transaction))
                        return MessageCode.NbUpdateFail;
                }
                GoldbarRecordMgr.Insert(new GoldbarRecordEntity(0, goldBarManager.ManagerId, true, addGoldBar,
                    (int) EnumTransactionType.ActivityExPrize, DateTime.Now));
            }
            return MessageCode.Success;
        }
        #endregion

        MessageCode SaveTeammemberPrize(ItemPackageFrame package, ActivityexRecordEntity activityex)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveTeammemberPrize(transactionManager.TransactionObject, package, activityex);
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
                SystemlogMgr.Error("SaveGuidePrize", ex);
                return MessageCode.Exception;
            }
        }
        MessageCode Tran_SaveTeammemberPrize(DbTransaction transaction, ItemPackageFrame package, ActivityexRecordEntity activityex)
        {
            if (!package.Save(transaction))
            {
                return MessageCode.NbUpdateFailPackage;
            }
            if (!ActivityexRecordMgr.Update(activityex))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }

        /// <summary>
        /// 生成物品基本信息
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <returns></returns>
        public ItemInfoEntity BuildItem(int itemType, int itemCode, int itemCount, bool isBinding)
        {
            Guid itemId = ShareUtil.GenerateComb();
           var LastAddItem = BuildItem(itemId, itemType, itemCode, itemCount, isBinding);
            return LastAddItem;
        }

        /// <summary>
        /// 生成物品基本信息
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemType"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <returns></returns>
        public ItemInfoEntity BuildItem(Guid itemId, int itemType, int itemCode, int itemCount, bool isBinding)
        {
            var item = new ItemInfoEntity(itemId, itemCode, itemType);
            item.IsBinding = isBinding;
            if (itemCount == 0)
                itemCount = 1;
            item.ItemCount = itemCount;
            return item;
        }

        /// <summary>
        /// 随机获取一个装备碎片
        /// </summary>
        /// <returns></returns>
        public int GetRandomDebris()
        {
            if (_europeEquipmentDebris != null && _europeEquipmentDebris.Count > 0)
            {
                return _europeEquipmentDebris[RandomHelper.GetInt32WithoutMax(0, _europeEquipmentDebris.Count)];
            }
            return 0;
        }
      
    }
}
