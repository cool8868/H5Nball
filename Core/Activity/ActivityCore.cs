using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Enum;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Constellation;
using Games.NBall.Core.Item;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Manager;
//using Games.NBall.Core.Online;
using Games.NBall.Core.Task;
using Games.NBall.Core.Turntable;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Activity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.Enums.Shadow;
//using Games.NBall.Entity.Response.Mall;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Activity;

using MsEntLibWrapper.Data;
using ManagervippackageEntity = Games.NBall.Entity.Response.Manager.ManagervippackageEntity;

namespace Games.NBall.Core.Activity
{
    public class ActivityCore
    {
        private Dictionary<int, int> _guideStepGiftDic1;
        private Dictionary<int, int> _guideStepGiftDic2;

        public int _legendCode;
        public int _legendDebrisCode;
        public ConfigEverydayactivitylegendEntity configEverydayactivitylegend = null;
        public int LegendCode{
            get
            {
                DateTime date = DateTime.Now.Date;
                if (configEverydayactivitylegend == null)
                    configEverydayactivitylegend = GetEvertyDayLegend(date.Date);
                if (configEverydayactivitylegend.RefreshDate != date.Date)
                    configEverydayactivitylegend = GetEvertyDayLegend(date.Date);
                if (configEverydayactivitylegend == null)
                    return 0;
                return configEverydayactivitylegend.LegendCode;
            }
        }

        public int LegendDebrisCode
        {
            get
            {
                DateTime date = DateTime.Now.Date;
                if (configEverydayactivitylegend == null)
                    configEverydayactivitylegend = GetEvertyDayLegend(date.Date);
                if (configEverydayactivitylegend.RefreshDate != date.Date)
                    configEverydayactivitylegend = GetEvertyDayLegend(date.Date);
                if (configEverydayactivitylegend == null)
                    return 0;
                return configEverydayactivitylegend.LegendDebrisCode;
            }
        }

        private ConfigEverydayactivitylegendEntity GetEvertyDayLegend(DateTime date)
        {
            configEverydayactivitylegend = ConfigEverydayactivitylegendMgr.GetById(date);
            if (configEverydayactivitylegend == null)
            {
                var legendLottery = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.RandomLegendDebrisCode, 199);
                _legendCode = CacheFactory.LotteryCache.LotteryByLib(legendLottery);
                if (_legendCode > 0)
                {
                    _legendDebrisCode = CacheFactory.ItemsdicCache.LotteryTheContractId(_legendCode % 100000);
                    if (_legendDebrisCode == 0)
                        return null;
                    configEverydayactivitylegend = new ConfigEverydayactivitylegendEntity(
                        date.Date,
                        _legendCode, _legendDebrisCode);
                    if (!ConfigEverydayactivitylegendMgr.Insert(configEverydayactivitylegend))
                    {
                        _legendCode = 0;
                        _legendDebrisCode = 0;
                        return null;
                    }
                }
                else
                    return null;
            }
            return configEverydayactivitylegend;
        }

        #region .ctor
        public ActivityCore(int p)
        {
            _guideStepGiftDic1 =
                FrameUtil.CastIntDic(CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.GuideStepGift1), ',');
            _guideStepGiftDic2 =
                FrameUtil.CastIntDic(CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.GuideStepGift2), ',');

            DateTime date = DateTime.Now.Date;
           configEverydayactivitylegend = ConfigEverydayactivitylegendMgr.GetById(date);
        }
        #endregion

        #region Facade
        public static ActivityCore Instance
        {
            get { return SingletonFactory<ActivityCore>.SInstance; }
        }

        public ActivityRecordResponse GetActivityInfo(Guid managerId, int activityId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.InvalidParameter<ActivityRecordResponse>();

            var response = ResponseHelper.CreateSuccess<ActivityRecordResponse>();
            response.Data = GetActivityInfo(manager, activityId);
            return response;
        }

        public ActivityRecordEntity GetActivityInfo(NbManagerEntity manager, int activityId)
        {
            if (manager == null)
                return null;
            var activityConfig = CacheFactory.ActivityCache.GetActivity(activityId);
            if (activityConfig == null)
                return null;
            var entity = ActivityRecordMgr.GetByManagerActivityId(manager.Idx, activityId);
            if (entity == null)
            {
                entity = new ActivityRecordEntity();
                entity.ManagerId = manager.Idx;
                entity.ActivityId = activityId;
                entity.ActivityStep = 1;
                //if (activityId == (int)EnumActivityType.PayContinue)
                //{
                //    entity.RecordDate = ShareUtil.BaseTime;
                //}
                //else
                //{
                    entity.RecordDate = DateTime.Today;
                //}
                if (activityId == (int)EnumActivityType.LevelGift || activityId==(int)EnumActivityType.OnlinePrize)
                    entity.ActivityStep = 0;
                
                entity.StepRecord = "";
                entity.RowTime = DateTime.Now;
                entity.Status = 0;
                entity.UpdateTime = DateTime.Now;
                entity.SettlementDate = DateTime.Now;
                entity.NeedSync = true;
            }
            if (activityId == (int)EnumActivityType.PayDaily)
            {
                entity.LegendCode = LegendCode;
                entity.LegendDebrisCode = LegendDebrisCode;
            }
            ActivityHistoryEntity history = null;
            Settlement(entity, manager, activityConfig, out history);
            if (entity.NeedSync)
            {
                if (entity.Idx == 0)
                {
                    if (!ActivityRecordMgr.Insert(entity))
                        return entity;
                }
                else if (ActivityRecordMgr.Update(entity))
                {
                    if (history != null)
                        ActivityHistoryMgr.Insert(history);
                }
                else
                {
                    return null;
                }
            }
            if (activityId == (int) EnumActivityType.PayDaily)
            {
                var managerList = EverydayactivityprizeMgr.GetManagerInfo(manager.Idx);
                bool isInsert = false;
                if (managerList.Count > 0)
                {
                    if (managerList[0].RowDate.Date != DateTime.Now.Date)
                    {
                        EverydayactivityprizeMgr.Delete(manager.Idx);
                        isInsert = true;
                        managerList.Clear();
                    }
                }
                else
                    isInsert = true;
                List<NBSolutionTeammember> teammembers = null;
                List<ActivityPrizeEntity> list = new List<ActivityPrizeEntity>();
                var prizeList = CacheFactory.ActivityCache.GetPrize(activityId);
                int plaryerId = 0;
                if (isInsert)
                {
                    teammembers = MatchDataHelper.GetSolutionTeammembers(manager.Idx, false, false);
                    plaryerId = GetPlayerId(teammembers);
                }
                //获取竞技场类型
                var season = ArenaSeasonMgr.GetSeason(DateTime.Now);
                if (season != null)
                    entity.ArenaType = season.ArenaType;
                foreach (var prize in prizeList)
                {
                    if (prize.PrizeType == 2)
                        continue;
                    ActivityPrizeEntity info = new ActivityPrizeEntity();
                    if (prize.PrizeType == 6 || prize.PrizeType == 7)
                    {
                        if (isInsert)
                        {
                            var insertInfo = InsertEveryDayPrize(manager.Idx, activityId,
                                prize.ActivityStep, prize.SubType,
                                prize.PrizeType, ref plaryerId);
                            if (insertInfo != null)
                                managerList.Add(insertInfo);
                        }
                        var prizeInfo =
                            managerList.FindAll(
                                r =>
                                    r.ActivityId == activityId && r.ActivityStep == prize.ActivityStep &&
                                    r.SubType == prize.SubType);
                        if (prizeInfo.Count > 0)
                            info.ItemCode = prizeInfo[0].ItemCode;
                        info.Type = 3;
                    }
                    else
                    {
                        info.ItemCode = prize.SubType;
                        info.Type = prize.PrizeType;
                    }
                    info.Count = prize.Count;
                    info.IsBinding = prize.IsBinding;
                    info.Strength = prize.Strength;
                    info.ActivityStep = prize.ActivityStep;
                    info.ActivityId = prize.ActivityId;
                    list.Add(info);
                }
                entity.PrizeList = list;
            }


            return entity;
        }

        /// <summary>
        /// 获取playerId
        /// </summary>
        /// <param name="teamm"></param>
        /// <returns></returns>
        int GetPlayerId(List<NBSolutionTeammember> teamm)
        {
            teamm = teamm.FindAll(r => r.ArousalLv != 5);
            var teammembers = new List<NBSolutionTeammember>();
            foreach (var item in teamm)
            {
                var player = CacheFactory.PlayersdicCache.GetPlayer(item.PlayerId);
                if (player.Kpi <= 108 && player.Kpi >= 88)
                    teammembers.Add(item);
            }
            if (teammembers.Count != 0)
            {
                teammembers = teammembers.OrderByDescending(r => CacheFactory.PlayersdicCache.GetPlayer(r.PlayerId).Kpi).ToList();
                DicItemEntity item = null;
                do
                {
                    if (teammembers.Count == 0)
                        break;
                    int index = 2;
                    if (teammembers.Count <= 2)
                        index = teammembers.Count - 1;
                    var teammember = teammembers[RandomHelper.GetInt32(0, index)];
                    item = CacheFactory.ItemsdicCache.GetItem(teammember.PlayerId + 100000);
                    if (item != null && item.ItemType == (int)EnumItemType.PlayerCard &&
                        item.PlayerCardLevel != (int)EnumPlayerCardLevel.BlackGold)
                    {
                        item = CacheFactory.ItemsdicCache.LotteryTheContract(teammember.PlayerId);
                        if (item != null)
                            return teammember.PlayerId;
                    }
                    teammembers.Remove(teammember);
                    if (teammembers.Count == 0)
                        break;
                } while (true);
            }
            return 0;
        }

        EverydayactivityprizeEntity InsertEveryDayPrize(Guid managerId,int activityId,int activitySetpId,int subType,int prizeType,ref int playerId)
        {
            if (playerId != 0)
            {
                DicItemEntity item = null;
                if (prizeType == 6)
                    item = CacheFactory.ItemsdicCache.LotteryTheContract(playerId);
                else if (prizeType == 7)
                    item = CacheFactory.ItemsdicCache.GetItem(playerId + 100000);
                if (item != null)
                {
                    var everyday = new EverydayactivityprizeEntity(0, managerId, activityId,
                        activitySetpId, subType, item.ItemCode, DateTime.Now);
                    EverydayactivityprizeMgr.Insert(everyday);
                    return everyday;
                }
            }
            else
            {
                DicItemEntity item = null;
                if (prizeType == 6)
                {
                    item = CacheFactory.ItemsdicCache.LotteryTheContract((int) EnumPlayerCardLevel.Orange, 88, 89);
                    playerId =  CacheFactory.MallCache.GetTheContractRewardCode(item.ItemCode%100000) %100000;
                }
                else if (prizeType == 7)
                {
                    item = CacheFactory.ItemsdicCache.RandomPlayerCard((int) EnumPlayerCardLevel.Orange, 88, 89);
                    playerId = item.ItemCode % 100000;
                }
                if (item != null)
                {
                    var everyday = new EverydayactivityprizeEntity(0, managerId, activityId,
                        activitySetpId, subType, item.ItemCode, DateTime.Now);
                    EverydayactivityprizeMgr.Insert(everyday);
                    return everyday;
                }
            }
            return null;
        }


        public MessageCodeResponse TxTaskStep(string account, int activityId, int activityStep, string cmd, string billno,string zoneId)
        {
            if (activityStep <= 0)
                activityStep = 1;
            var manager = ManagerCore.Instance.GetManager(account,zoneId);
            if (manager == null)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.MissManager);
            }
            var managerId = manager.Idx;
            var activityConfig = CacheFactory.ActivityCache.GetActivity(activityId);
            if (activityConfig == null)
                return ResponseHelper.InvalidParameter<MessageCodeResponse>("activityConfig is null.");
            var entity = ActivityRecordMgr.GetByManagerActivityId(managerId, activityId,zoneId);
            if (entity == null)
            {
                entity = new ActivityRecordEntity();
                entity.ManagerId = managerId;
                entity.ActivityId = activityId;
                entity.ActivityStep = activityStep;
                entity.RecordDate = DateTime.Today;
                entity.StepRecord = "";
                entity.RowTime = DateTime.Now;
                entity.Status = 0;
                entity.UpdateTime = DateTime.Now;
                entity.SettlementDate = DateTime.Now;
                entity.NeedSync = true;
            }
            MessageCodeResponse response = TxTaskCheck(entity, activityConfig, manager, billno, zoneId);
            if (cmd.Contains("award") && response.Code == (int)MessageCode.Success)
            {
                response = TxTaskPrize(entity.Idx, activityStep, manager, billno, zoneId);
                return response;
            }

            return response;
        }

        MessageCodeResponse TxTaskCheck(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager, string billno, string zoneId)
        {
            SettlementTxTask(entity, activityConfig, manager,zoneId);
            if (entity.NeedSync)
            {
                entity.StepRecord = billno;
                if (entity.Idx == 0)
                {
                    if (!ActivityRecordMgr.Insert(entity,null,zoneId))
                        return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail, "ActivityRecordMgr.Insert");
                }
                else
                {
                    if (!ActivityRecordMgr.Update(entity, null, zoneId))
                    {
                        return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail, "ActivityRecordMgr.Update");
                    }
                }
            }
            if (entity.Status == 1)
            {
                return ResponseHelper.CreateSuccess<MessageCodeResponse>();
            }
            else if (entity.Status == 2)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.ActivityHasReceive);
            }
            else
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.ActivityNoPrize);
            }
        }

        MessageCodeResponse TxTaskPrize(int activityRecordId, int activityStep, NbManagerEntity manager, string billno, string zoneId)
        {

            MessageCode code = MessageCode.NbUpdateFail;
            var entity = ActivityRecordMgr.GetById(activityRecordId,zoneId);
            var package = ItemCore.Instance.GetPackage(manager.Idx, EnumTransactionType.ActivityPrize,zoneId);
            if (package == null)
                return ResponseHelper.InvalidParameter<MessageCodeResponse>("package is null");
            var cpackage = new ConstellationPackbager(manager.Idx,zoneId);
            bool isupdateCpackage = false;
            int point = 0;
            int bindPoint = 0;
            int goldBar = 0;
            string activityKey = "";
            if (entity.Status == 0)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.ActivityNoPrize);
            }
            else if (entity.Status == 2)
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.ActivityHasReceive);
            }
            List<ActivityPrizeEntity> prizes = new List<ActivityPrizeEntity>();
            code = BuildPrize(entity.ManagerId, entity.ActivityId, activityStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage, zoneId);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<MessageCodeResponse>(code, "BuildPrize");
            entity.Status = 2;
            activityKey = BuildActivityKey(entity, activityStep);
            entity.PrizeList = prizes;
            entity.StepRecord = billno;
            PayUserEntity payUserEntity = PayUserMgr.GetById(manager.Account);
            code = SavePrize(activityKey, entity, package, manager, null, point, bindPoint,goldBar, 0,ref  payUserEntity, isupdateCpackage, cpackage, zoneId);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<MessageCodeResponse>(code, "SavePrize");

            if (manager.AddCoin > 0)
            {
                //ShadowMgr.SaveCoinCharge(manager.Idx, manager.AddCoin, manager.AddExp, manager.IsLevelup, manager.Level, manager.CoinSourceType, manager.CoinOrderId);
                entity.ManagerCoin = manager.Coin;
            }
             package.Shadow.Save();
            entity.NeedSync = false;
            var response = ResponseHelper.CreateSuccess<MessageCodeResponse>();
            return response;
        }

        public ActivityRecordResponse PrizeReceive(Guid managerId, int activityId, int activityStep)
        {
            var entity = ActivityRecordMgr.GetByManagerActivityId(managerId, activityId);
            if (entity == null)
            {
                return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
            }
            var activityConfig = CacheFactory.ActivityCache.GetActivity(activityId);
            if (activityConfig == null)
                return ResponseHelper.InvalidParameter<ActivityRecordResponse>("activityConfig is null");
            var manager = ManagerCore.Instance.GetManager(managerId);
            ActivityHistoryEntity history = null;
            Settlement(entity, manager, activityConfig, out history);

            MessageCode code = MessageCode.NbUpdateFail;
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ActivityPrize);
            if (package == null)
                return ResponseHelper.InvalidParameter<ActivityRecordResponse>();
            var cpackage = new ConstellationPackbager(managerId);
            bool isupdateCpackage = false;
            NbManagerextraEntity managerextra = null;
            List<ActivityPrizeEntity> prizes = new List<ActivityPrizeEntity>();
            int point = 0;
            int bindPoint = 0;
            int realStep = 0;
            int goldBar = 0;
            string activityKey = "";
            switch (activityId)
            {
                case (int)EnumActivityType.LoginPrize:
                    var stepList = FrameUtil.CastIntList(entity.StepRecord, ',');
                    if (stepList == null)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else
                    {
                        if (activityStep > stepList.Count)
                        {
                            return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                        }
                        else
                        {
                            if (stepList[activityStep - 1] != 0)
                            {
                                return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                            }
                            else
                            {
                                stepList[activityStep - 1] = 1;
                                entity.StepRecord = string.Join(",", stepList);
                            }
                        }
                    }
                    code = BuildPrize(managerId, activityId, activityStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage);
                   
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<ActivityRecordResponse>(code);
                    activityKey = BuildActivityKey(entity, activityStep);
                    
                    break;
                case (int)EnumActivityType.OnlinePrize:
                    var stepList5 = FrameUtil.CastIntList(entity.StepRecord, ',');
                    if (stepList5 == null)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else
                    {
                        if (activityStep > stepList5.Count)
                        {
                            return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                        }
                        else
                        {
                            if (stepList5[activityStep - 1] != 0)
                            {
                                return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                            }
                            else
                            {
                                stepList5[activityStep - 1] = 1;
                                entity.StepRecord = string.Join(",", stepList5);
                                if (activityStep>=activityConfig.StepDic.Count && !stepList5.Exists(d => d == 0))
                                    entity.Status = 2;
                            }
                        }
                    }
                    code = BuildPrize(managerId, activityId, activityStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<ActivityRecordResponse>(code);
                    activityKey = BuildActivityKey(entity, activityStep);
                    break;
                case (int)EnumActivityType.PayFirst:
                    if (entity.Status != 1)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else if (entity.Status == 2)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                    }
                    code = BuildPrize(managerId, activityId, 1, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<ActivityRecordResponse>(code);
                    entity.Status = 2;
                    activityKey = BuildActivityKey(entity, activityStep);
                    managerextra = NbManagerextraMgr.GetById(managerId);
                    managerextra.PayFirstFlag = true;
                    break;
                case (int)EnumActivityType.PayDaily:
                    var stepList1 = FrameUtil.CastIntList(entity.StepRecord, ',');
                    if (stepList1 == null)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else
                    {
                        if (activityStep > stepList1.Count)
                        {
                            return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                        }
                        else
                        {
                            if (stepList1[activityStep - 1] != 0)
                            {
                                return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                            }
                            else
                            {
                                stepList1[activityStep - 1] = 1;
                                entity.StepRecord = string.Join(",", stepList1);
                            }
                        }
                    }
                    int arentType = 0;
                    // var season = ArenaSeasonMgr.GetSeason(DateTime.Now);
                    //int arentType = 0;
                    //if (season != null)
                    //{
                    //    if (season.ArenaType == 5)
                    //        activityId = 99;
                    //    arentType = season.ArenaType;
                    //}

                    code = BuildPrize(managerId, activityId, activityStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage, "", arentType);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<ActivityRecordResponse>(code);

                    #region 竞技场配套活动
                    //var season = ArenaSeasonMgr.GetSeason(DateTime.Now);
                    //if (season != null)
                    //{
                    //    int itemCode = 0;
                    //    switch (season.ArenaType)
                    //    {
                    //        case 1:
                    //            itemCode = 310163;
                    //            break;
                    //        case 2:
                    //            itemCode = 310164;
                    //            break;
                    //        case 3:
                    //            itemCode = 310165;
                    //            break;
                    //        case 4:
                    //            itemCode = 310166;
                    //            break;
                    //        case 5:
                    //            itemCode = 310167;
                    //            break;
                    //    }
                    //    int itemCount = 0;
                    //    if (itemCode > 0)
                    //    {
                    //        switch (activityStep)
                    //        {
                    //            case 2:
                    //                if (season.ArenaType ==5)
                    //                {
                    //                    itemCode = 310101; //高级强化卡
                    //                    itemCount = 1;
                    //                }
                    //                else
                    //                    itemCount = 2;
                    //                break;
                    //            case 3:
                    //                if (season.ArenaType ==5)
                    //                {
                    //                    point += 1588;
                    //                    prizes.Add(new ActivityPrizeEntity(){Type = 3, Count = 1588, IsBinding = false, Strength = 0,ItemCode = 0 });
                    //                }
                    //                else
                    //                    itemCount = 4;
                    //                break;
                    //            case 4:
                    //                if (season.ArenaType == 5)
                    //                    itemCount = 1;
                    //                else
                    //                    itemCount = 8;
                    //                break;
                    //            case 5:
                    //                if (season.ArenaType == 5)
                    //                    itemCount = 2;
                    //                else
                    //                    itemCount = 30;
                    //                break;
                    //            case 6:
                    //                if (season.ArenaType == 5)
                    //                    itemCount = 5;
                    //                else
                    //                    itemCount = 70;
                    //                break;
                    //        }
                    //        if (itemCount > 0)
                    //        {
                    //            code = package.AddItems(itemCode, itemCount);
                    //            prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = itemCount, IsBinding = false, Strength = 1, ItemCode = itemCode });
                    //        }
                    //    }
                    //}

                    #endregion

                    #region 奥运活动
                    //if(OlympicCore.Instance.IsActivity){
                    //        switch (activityStep)
                    //        {
                    //            case 1:
                    //                code = package.AddItems(310151, 1);
                    //                prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = false, Strength = 1, ItemCode = 310151 });
                    //                break;
                    //            case 2:
                    //                code = package.AddItems(310151, 4);
                    //                prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 4, IsBinding = false, Strength = 1, ItemCode = 310151 });
                    //                break;
                    //            case 3:
                    //                code = package.AddItems(310151, 10);
                    //                prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 10, IsBinding = false, Strength = 1, ItemCode = 310151 });
                    //                break;
                    //            case 4:
                    //                code = package.AddItems(310151, 20);
                    //                prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 20, IsBinding = false, Strength = 1, ItemCode = 310151 });
                    //                break;
                    //            case 5:
                    //                code = package.AddItems(310151, 50);
                    //                prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 50, IsBinding = false, Strength = 1, ItemCode = 310151 });
                    //                break;
                    //            case 6:
                    //                code = package.AddItems(310151, 180);
                    //                prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 180, IsBinding = false, Strength = 1, ItemCode = 310151 });
                    //                break;
                    //        }
                    //}
                    #endregion

                    activityKey = BuildActivityKey(entity, activityStep);
                    break;

                case (int)EnumActivityType.LadderScore:
                case (int)EnumActivityType.DailyTaskCount:
                case (int)EnumActivityType.BuyStamina:
                case (int)EnumActivityType.LeagueWin:
                case (int)EnumActivityType.PayFirstDaily:
                    if (entity.Status == 0)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else if (entity.Status == 2)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                    }
                    code = BuildPrize(managerId, activityId, entity.ActivityStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<ActivityRecordResponse>(code);
                    entity.Status = 2;
                    activityKey = BuildActivityKey(entity, activityStep);
                    break;

                case (int)EnumActivityType.VipGift:
                    var stepList2 = FrameUtil.CastIntList(entity.StepRecord, ',');
                    if (stepList2 == null)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else
                    {
                        if (activityStep > stepList2.Count)
                        {
                            return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                        }
                        else
                        {
                            if (stepList2[activityStep - 1] != 0)
                            {
                                return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                            }
                            else
                            {
                                stepList2[activityStep - 1] = 1;
                                entity.StepRecord = string.Join(",", stepList2);
                                if (!stepList2.Exists(d => d == 0))
                                    entity.Status = 2;
                            }
                        }
                    }
                    code = BuildPrize(managerId, activityId, activityStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<ActivityRecordResponse>(code);
                    activityKey = BuildActivityKey(entity, activityStep);
                    break;
                case (int)EnumActivityType.VipDaily:
                    if (entity.Status == 0)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else if (entity.Status == 2)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                    }
                    code = BuildPrize(managerId, activityId, entity.ActivityStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<ActivityRecordResponse>(code);
                    entity.Status = 2;
                    activityKey = BuildActivityKey(entity, activityStep);
                    break;
                case (int)EnumActivityType.LevelGift:
                    var stepList4 = FrameUtil.CastIntList(entity.StepRecord, ',');
                    if (stepList4 == null)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else
                    {
                        realStep = (activityStep + 4) / 5;
                        if (realStep > stepList4.Count)
                        {
                            return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                        }
                        else
                        {
                            if (stepList4[realStep - 1] != 1)
                            {
                                return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                            }
                            else if (stepList4[realStep - 1] == 2)
                            {
                                return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                            }
                            else
                            {
                                stepList4[realStep - 1] = 2;
                                entity.StepRecord = string.Join(",", stepList4);
                                code = BuildPrize(managerId, activityId, activityStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage);
                                if (code != MessageCode.Success)
                                    return ResponseHelper.Create<ActivityRecordResponse>(code);
                                activityKey = BuildActivityKey(entity, activityStep,realStep);
                                if (entity.StepRecord == "2,2,2,2,2,2")
                                    entity.Status = 2;
                                else
                                    entity.Status = 0;
                            }
                        }
                    }
                    break;
                case (int)EnumActivityType.GuidePrize:
                    realStep = activityStep;
                    if(realStep>10)
                        realStep =1;
                    var stepList3 = FrameUtil.CastIntList(entity.StepRecord, ',');
                    if (stepList3 == null)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else
                    {
                        if (realStep > stepList3.Count)
                        {
                            return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                        }
                        else
                        {
                            if (stepList3[realStep - 1] != 0)
                            {
                                return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                            }
                            else
                            {
                                stepList3[realStep - 1] = 1;
                                entity.StepRecord = string.Join(",", stepList3);
                                if (entity.Status == 1 && !entity.StepRecord.Contains("0"))
                                {
                                    entity.Status = 2;
                                }
                            }
                        }
                    }
                    int guideItemCode = 0;
                    
                    if (activityStep > 20)
                    {
                        if (manager.VipLevel == 0)
                        {
                            return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.LackofVipLevel);
                        }
                        _guideStepGiftDic2.TryGetValue(activityStep - 20, out guideItemCode);
                    }
                    else if (activityStep > 10)
                    {
                        _guideStepGiftDic1.TryGetValue(activityStep - 10, out guideItemCode);
                    }
                    if (guideItemCode > 0)
                    {
                        code = package.AddPlayerCard(guideItemCode, 1, true, 1,false);
                        prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = true, Strength = 1, ItemCode = guideItemCode });
                        if (code != MessageCode.Success)
                            return ResponseHelper.Create<ActivityRecordResponse>(code);
                        if (managerextra == null)
                        {
                            managerextra = NbManagerextraMgr.GetById(managerId);
                        }
                        managerextra.GuideItemCode = guideItemCode;
                    }
                    else if (activityStep == 2)
                    {
                        if (managerextra == null)
                        {
                            managerextra = NbManagerextraMgr.GetById(managerId);
                        }
                        if (managerextra.GuideItemCode < 1)
                        {
                            return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                        }
                        code = package.AddItems(managerextra.GuideItemCode,1,3,true,false);
                        prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = true, Strength = 3, ItemCode = managerextra.GuideItemCode });
                        if (code != MessageCode.Success)
                            return ResponseHelper.Create<ActivityRecordResponse>(code);
                    }
                    else
                    {
                        code = BuildPrize(managerId, activityId, realStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage);
                        if (code != MessageCode.Success)
                            return ResponseHelper.Create<ActivityRecordResponse>(code);
                    }
                    activityKey = BuildActivityKey(entity, realStep);
                    break;
                case (int)EnumActivityType.NewPlayerGift:
                    var stepList8 = FrameUtil.CastIntList(entity.StepRecord, ',');
                    if (stepList8 == null)
                    {
                        return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                    }
                    else
                    {
                        if (activityStep > stepList8.Count)
                        {
                            return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityNoPrize);
                        }
                        else
                        {
                            if (stepList8[activityStep - 1] != 0)
                            {
                                return ResponseHelper.Create<ActivityRecordResponse>(MessageCode.ActivityHasReceive);
                            }
                            else
                            {
                                stepList8[activityStep - 1] = 1;
                                entity.StepRecord = string.Join(",", stepList8);
                                if (entity.ActivityStep == activityConfig.StepDic.Count && !entity.StepRecord.Contains("0"))
                                {
                                    entity.Status = 2;
                                }
                            }
                        }
                    }
                    code = BuildPrize(managerId, activityId, activityStep, package, manager, prizes, ref point, ref isupdateCpackage, ref bindPoint, ref goldBar, cpackage);
                    if (code != MessageCode.Success)
                        return ResponseHelper.Create<ActivityRecordResponse>(code);
                    activityKey = BuildActivityKey(entity, activityStep);
                    break;
            }
            entity.PrizeList = prizes;
            PayUserEntity payUserEntity = PayUserMgr.GetById(manager.Account);
            if (activityId == (int)EnumActivityType.LoginPrize)
            {
               //code = ActiveCore.Instance.AddActive(manager.Idx, EnumActiveType.LoginPrize, 1);
            }
            if (code != MessageCode.Success)
                return ResponseHelper.Create<ActivityRecordResponse>(code);
            code = SavePrize(activityKey, entity, package, manager, managerextra, point, bindPoint, goldBar, realStep, ref payUserEntity, isupdateCpackage, cpackage);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<ActivityRecordResponse>(code);

            if (manager.AddCoin > 0)
            {
                ShadowMgr.SaveCoinCharge(manager.Idx, manager.AddCoin, manager.AddExp, manager.IsLevelup, manager.Level, manager.CoinSourceType, manager.CoinOrderId);
                entity.ManagerCoin = manager.Coin;
            }
            if (package.Shadow != null)
            {
                package.Shadow.Save();
            }
            if (activityId == 111)
            {
                if (prizes != null && prizes.Count > 0)
                {
                    //ChatHelper.SendRemindGuidePrize(managerId, manager.Name, realStep, prizes[0].ItemCode);
                }
            }
            else if (activityId == 110)
            {
                if (prizes != null && prizes.Count > 0)
                {
                    //ChatHelper.SendRemindLevelGift(managerId, manager.Name, prizes[0].ItemCode,prizes[0].Strength,prizes[1].Strength);
                }
            }
            else if (activityId == 108)
            {
                if (prizes != null && prizes.Count > 0)
                {
                    //ChatHelper.SendRemindVipGift(managerId, manager.Name, prizes[0].ItemCode, activityStep);
                }
            }
          
            if (bindPoint > 0)
            {
                //ChatHelper.SendBindPoint(manager.Idx, payUserEntity.BindPoint);
            }
            var response = ResponseHelper.CreateSuccess<ActivityRecordResponse>();
            response.Data = entity;
            return response;
        }
        //vip礼包
        public NBManagerVipPackageResponse HasVipPackage(Guid managerId)
        {
            var response = new NBManagerVipPackageResponse();
            response.Data = new ManagerVippackageEntity();
            try
            {
                var manger = ManagerCore.Instance.GetManager(managerId);
                if (manger == null)
                    return ResponseHelper.Create<NBManagerVipPackageResponse>(MessageCode.NbParameterError);
                //managerId参数错误
                //判断manager.VipLevel能买多少礼包
                var lv = manger.VipLevel;
                var isHave = new List<int> {0, 0, 0, 0, 0};
                response.Data.IsHave = isHave;
                if (lv <= 0)
                {
                    return response;
                }
                //礼包列表格式如下：
                //   礼包等级 $ 礼包价格 $ 礼包类型1，装备1code，数量，是否绑定|礼包类型2，装备2code，数量，是否绑定 |礼包类型3，装备3code，数量，是否绑定|

                var managerVipPackage = NbManagervippackageMgr.GetByManagerId(manger.Idx);

                //数据库没有数据且经理等级大于0
                if (managerVipPackage == null || managerVipPackage.Count == 0)
                {
                    if(!NbManagervippackageMgr.InsertRecord((managerId)))
                        return ResponseHelper.Create<NBManagerVipPackageResponse>(MessageCode.NbUpdateFail);
                    managerVipPackage = NbManagervippackageMgr.GetByManagerId(manger.Idx);
                }
                DateTime date = DateTime.Now.Date;
                //if (date.DayOfWeek == DayOfWeek.Monday && managerVipPackage[0].RowTime.Date < date)
                //{
                //    if (!NbManagervippackageMgr.DayUpdate(managerId))
                //        return ResponseHelper.Create<NBManagerVipPackageResponse>(MessageCode.NbUpdateFail);
                //    foreach (var item in managerVipPackage)
                //    {
                //        item.IsGet = 0;
                //    }
                //}
            

                //for (int i = 0; i < managerVipPackage.Count; i++)
                //{
                //   if (managerVipPackage[i].RowTime.Date < date)

                //}
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        date = date.AddDays(1);
                        break;
                    case DayOfWeek.Monday:
                        date = date.AddDays(7);
                        break;
                    case DayOfWeek.Tuesday:
                        date = date.AddDays(6);
                        break;
                    case DayOfWeek.Wednesday:
                        date = date.AddDays(5);
                        break;
                    case DayOfWeek.Thursday:
                        date = date.AddDays(4);
                        break;
                    case DayOfWeek.Friday:
                        date = date.AddDays(3);
                        break;
                    case DayOfWeek.Saturday:
                        date = date.AddDays(2);
                        break;
                }
                response.Data.NextRefreshTick = ShareUtil.GetTimeTick(date);
                var cofinglist = CacheFactory.ActivityCache.GetVipPackagePrize(lv);
                if (cofinglist.Count > 0)
                {
                    int index = cofinglist[0].PackageId;
                    if (managerVipPackage.Exists(r => r.PackageLevel == index))
                    {
                        var mangerlist = managerVipPackage.FindAll((r => r.PackageLevel <= index));
                        for (int i = 0; i < mangerlist.Count; i++)
                        {
                            if (mangerlist[i].IsGet == 1)
                            {
                                isHave[i] = 2;
                                continue;
                            }
                            isHave[i] = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取经理VIP礼包", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 购买VIP礼包
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public BuyVipPackageResponse BuyVipPackage(Guid managerId, int packageId)
        {
            var response = new BuyVipPackageResponse();
            response.Data = new BuyVipPackage();
            try
            {
                if (packageId == 0)
                {
                    response.Code = (int)MessageCode.NbParameterError;
                    return response;
                }
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                {
                    response.Code = (int) MessageCode.AdMissManager;
                    return response;
                }
                var config = CacheFactory.ActivityCache.GetVipPackagePrizeByPackageId(packageId);
                if (config == null || config.Count <= 0 || config[0].VipLevel> manager.VipLevel)
                {
                    response.Code = (int) MessageCode.LackofVipLevel;
                    return response;
                }
                var point = PayCore.Instance.GetPoint(managerId);
                var price = config[0].Price;
                if (point <= 0 || point <= price)
                {
                    response.Code = (int) MessageCode.NbPointShortage;
                    return response;
                }
                var managerRecord = NbManagervippackageMgr.GetRecord(managerId,packageId);
                if (managerRecord == null)
                {
                    response.Code = (int) MessageCode.NbParameterError;
                    return response;
                }
                if (managerRecord.IsGet > 0)
                {
                    response.Code = (int) MessageCode.MallBuyCountLimit;
                    return response;
                }
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.BuyVipPackage);
                if (package == null)
                {
                    response.Code = (int) MessageCode.NbNoPackage;
                    return response;
                }
                bool isAddCoin = false;
                managerRecord.IsGet ++;
                foreach (var item in config)
                {
                    switch (item.PrizeType)
                    {
                        case 1://金币
                            isAddCoin = true;
                            Games.NBall.Core.Manager.ManagerUtil.AddManagerData(manager, 0, item.Counts, 0,
                                EnumCoinChargeSourceType.BuyVipPackage, ShareUtil.GenerateComb().ToString());
                            break;
                        case 3://物品
                            var code = package.AddItems(item.PrizeItemCode, item.Counts);
                            if(code != MessageCode.Success)
                            {
                                response.Code = (int)code;
                                return response;
                            }
                            break;
                    }
                }
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = SaveBuyVipPackage(manager, price, package, managerRecord, isAddCoin, transactionManager.TransactionObject);
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        response.Data.Point = point - price;
                        response.Data.Coin = manager.Coin;
                        response.Data.Package = ItemCore.Instance.BuildPackageData(package);
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("购买VIP礼包", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        

        private MessageCode SaveBuyVipPackage(NbManagerEntity manager,int price,ItemPackageFrame package,NbManagervippackageEntity record,bool isAddCoin,DbTransaction trans)
        {
            var messCode = MessageCode.Success;
            if (price > 0)
            {
                messCode = PayCore.Instance.GambleConsume(manager.Idx, price, ShareUtil.GenerateComb(),
                    EnumConsumeSourceType.BuyVipPackage, trans);
                if (messCode != MessageCode.Success)
                    return messCode;
            }
            if (isAddCoin)
            {
                if (!Games.NBall.Core.Manager.ManagerUtil.SaveManagerData(manager, null, trans))
                {
                    messCode = MessageCode.NbUpdateFail;
                    return messCode;
                }
            }
            if (!package.Save(trans))
            {
                messCode = MessageCode.NbUpdateFail;
                return messCode;
            }
            if (!NbManagervippackageMgr.Update(record, trans))
            {
                messCode = MessageCode.NbUpdateFail;
                return messCode;
            }
            return messCode;
        }

        //NB_ManagerVipPackage添加数据
        private void AddInner(String inner, Guid managerId)
        {
            String[] strs = inner.Split('$');
            var nbme = new NbManagervippackageEntity();
            nbme.IsGet = 1;
            nbme.ManagerId = managerId;
            nbme.PackageLevel = int.Parse(strs[0]);
            nbme.RowTime = DateTime.Now;
            NbManagervippackageMgr.Insert(nbme);
        }

        #endregion

        #region encapsulation

        #region BuildActivityKey
        string BuildActivityKey(ActivityRecordEntity entity, int activityStep,int realStep=0)
        {
            string key = "";
            switch (entity.ActivityId)
            {
                case (int) EnumActivityType.LoginPrize:
                    var date = entity.RecordDate.AddDays(activityStep - 1);
                    key = string.Format("{0}@{1:yyyyMMdd}", entity.ActivityId, date);
                    break;
                case (int) EnumActivityType.OnlinePrize:
                    key = string.Format("{0}@{1}@{2:yyyyMMdd}", entity.ActivityId, activityStep, entity.RecordDate);
                    break;
                case (int) EnumActivityType.PayFirst:
                    key = string.Format("{0}", entity.ActivityId);
                    break;
                case (int) EnumActivityType.PayDaily:
                    key = string.Format("{0}@{1}@{2:yyyyMMdd}", entity.ActivityId, activityStep, entity.RecordDate);
                    break;
                case (int) EnumActivityType.LadderScore:
                case (int) EnumActivityType.DailyTaskCount:
                case (int) EnumActivityType.BuyStamina:
                case (int) EnumActivityType.LeagueWin:
                case (int) EnumActivityType.PayFirstDaily:
                    key = string.Format("{0}@{1}@{2:yyyyMMdd}", entity.ActivityId, activityStep, entity.RecordDate);
                    break;
                case (int) EnumActivityType.VipGift:
                    key = "vipgift" + entity.ActivityId + "_" + activityStep;
                    break;
                case (int) EnumActivityType.VipDaily:
                    key = string.Format("vipdaily{0}_{1}@{2:yyyyMMdd}", entity.ActivityId, activityStep,
                        entity.RecordDate);
                    break;
                case (int) EnumActivityType.LevelGift:
                    key = "levelgift" + entity.ActivityId + "_" + realStep;
                    break;
                case (int) EnumActivityType.GuidePrize:
                    key = "guideprize" + entity.ActivityId + "_" + activityStep;
                    break;
                case (int) EnumActivityType.NewPlayerGift:
                    key = "newplayergift" + entity.ActivityId + "_" + activityStep;
                    break;
                default:
                    break;
            }
            return key;
        }
        #endregion

        #region Settlement
        void Settlement(ActivityRecordEntity entity, NbManagerEntity manager, DicActivityEntity activityConfig, out ActivityHistoryEntity history)
        {
            history = null;
            switch (entity.ActivityId)
            {
                case (int)EnumActivityType.LoginPrize:
                    if (entity.SettlementDate != DateTime.Today)
                    {
                        SettlementLoginPrize(entity, manager, out history);
                    }
                    break;
                case (int)EnumActivityType.OnlinePrize:
                    SettlementOnlinePrize(entity, activityConfig, out history);
                    break;
                case (int)EnumActivityType.PayFirst:
                    if (entity.Status == 0)
                    {
                        SettlementPayFirst(entity, manager);
                    }
                    break;
                case (int)EnumActivityType.PayDaily:
                    SettlementPayDaily(entity, activityConfig, manager, out history);
                    break;

                case (int)EnumActivityType.LadderScore:
                    SettlementLadderScore(entity, activityConfig, manager, out history);
                    break;
                case (int)EnumActivityType.DailyTaskCount:
                    SettlementDailyTaskCount(entity, activityConfig, manager, out history);
                    break;
                case (int)EnumActivityType.BuyStamina:
                    SettlementDailyBuyStaminaCount(entity, activityConfig, manager, out history);
                    break;
                case (int)EnumActivityType.LeagueWin:
                    SettlementDailyLeagueMatchWinCount(entity, activityConfig, manager, out history);
                    break;


                case (int)EnumActivityType.VipGift:
                    SettlementVip(entity, manager);
                    break;
                case (int)EnumActivityType.VipDaily:
                    SettlementVipDaily(entity, manager);
                    break;
                case (int)EnumActivityType.LevelGift:
                    SettlementLevelGift(entity, activityConfig, manager);
                    break;
                case (int)EnumActivityType.GuidePrize:
                    SettlementGuidePrize(entity, manager);
                    break;
                case (int)EnumActivityType.NewPlayerGift:
                    SettlementNewPlayerGift(entity, activityConfig, manager);
                    break;
                case (int)EnumActivityType.PayFirstDaily:
                    SettlementPayFirstDaily(entity, manager.Account, out history);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region SettlementLoginPrize
        void SettlementLoginPrize(ActivityRecordEntity entity, NbManagerEntity manager, out ActivityHistoryEntity history)
        {
            history = null;
            var user = NbUserMgr.GetById(manager.Account);
            var continuingDay = user.ContinuingLoginDay;
            if (user.LastLoginDate.AddDays(1) == DateTime.Today)//跨天
            {
                continuingDay = continuingDay + 1;
            }

            continuingDay = continuingDay % 7;
            if (continuingDay == 0)
                continuingDay = 7;
            if (continuingDay == 1)
            {
                history = RebuildActivityRecord(entity);
            }
            else
            {
                if (entity.RecordDate.AddDays(continuingDay - 1) != DateTime.Today)
                {
                    history = RebuildActivityRecord(entity);
                    entity.RecordDate = DateTime.Today.AddDays(1 - continuingDay);
                }

                if (entity.ActivityStep < continuingDay) //有没有激活的
                {
                    var stepList = FrameUtil.CastIntList(entity.StepRecord, ',');
                    if (stepList == null)
                    {
                        stepList = new List<int>(continuingDay);
                    }

                    for (int i = 1; i <= continuingDay; i++)
                    {
                        if (stepList.Count < i)
                        {
                            stepList.Add(0);
                        }
                    }
                    entity.ActivityStep = continuingDay;
                    entity.StepRecord = string.Join(",", stepList);
                    entity.NeedSync = true;
                }
                else
                {
                    return;
                }
            }
            entity.SettlementDate = DateTime.Today;

        }
        #endregion

        #region SettlementOnlinePrize
        void SettlementOnlinePrize(ActivityRecordEntity entity, DicActivityEntity activityConfig, out ActivityHistoryEntity history)
        {
            history = null;
            if (entity.RecordDate != DateTime.Today)
            {
                history = RebuildOnlineRecord(entity);
            }
            if (entity.Status == 2)
                return;
            var onlineSeconds = OnlineCore.GetOnlineSecond(entity.ManagerId);
            var activityStep = entity.ActivityStep;
            foreach (var activitystepEntity in activityConfig.StepDic.Values)
            {
                if (activitystepEntity.ActivityStep > activityStep)
                {
                    if (onlineSeconds >= activitystepEntity.OnlineSeconds)
                    {
                        activityStep = activitystepEntity.ActivityStep;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (entity.ActivityStep < activityStep) //有没有激活的
            {
                var stepList = FrameUtil.CastIntList(entity.StepRecord, ',');
                if (stepList == null)
                {
                    stepList = new List<int>(activityStep);
                }

                for (int i = 1; i <= activityStep; i++)
                {
                    if (stepList.Count < i)
                    {
                        stepList.Add(0);
                    }
                }
                entity.ActivityStep = activityStep;
                entity.StepRecord = string.Join(",", stepList);
                entity.NeedSync = true;
                entity.Countdown = onlineSeconds;
            }
            else
            {
                entity.Countdown = onlineSeconds;
                return;
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementPayFirst
        void SettlementPayFirst(ActivityRecordEntity entity, NbManagerEntity manager)
        {
            if (entity.Status == 2)
                return;
            if (manager.VipLevel > 0)
            {
                entity.ActivityStep = 1;
                entity.Status = 1;
                entity.NeedSync = true;
            }
            else
            {
                var payAccount = PayUserMgr.GetById(manager.Account);
                if (payAccount != null && payAccount.TotalCash > 0)
                {
                    entity.ActivityStep = 1;
                    entity.Status = 1;
                    entity.NeedSync = true;
                }
            }
            entity.SettlementDate = DateTime.Today;

        }
        #endregion

        #region SettlementPayDaily
        void SettlementPayDaily(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager, out ActivityHistoryEntity history)
        {
            history = null;
            DateTime curTime = DateTime.Now;
            if (entity.Idx == 0)
                entity.ActivityStep = 0;
            if (entity.RecordDate != curTime.Date)
            {
                history = RebuildPayRecord(entity);
            }
            if (entity.Status == 2)
                return;
            var starTime = curTime.Date;
            var endTime = curTime.Date.AddDays(1).AddSeconds(-1);
            var point = 0;
            PayChargehistoryMgr.GetPointForActivity(manager.Account, starTime, endTime, ref point);
            var activityStep = entity.ActivityStep;
            foreach (var activitystepEntity in activityConfig.StepDic.Values)
            {
                if (activitystepEntity.ActivityStep > activityStep)
                {
                    if (point >= activitystepEntity.ConditionInt)
                    {
                        activityStep = activitystepEntity.ActivityStep;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (entity.ActivityStep < activityStep) //有没有激活的
            {
                var stepList = FrameUtil.CastIntList(entity.StepRecord, ',');
                if (stepList == null)
                {
                    stepList = new List<int>(activityStep);
                }

                for (int i = 1; i <= activityStep; i++)
                {
                    if (stepList.Count < i)
                    {
                        stepList.Add(0);
                    }
                }
                entity.ActivityStep = activityStep;
                entity.StepRecord = string.Join(",", stepList);
                entity.NeedSync = true;
                entity.Countdown = point;
            }
            else
            {
                entity.Countdown = point;
                return;
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion


        void SettlementPayFirstDaily(ActivityRecordEntity entity,string account, out ActivityHistoryEntity history)
        {
            history = null;
            DateTime curTime = DateTime.Now;
            if (entity.Idx == 0)
                entity.ActivityStep = 0;
            if (entity.RecordDate != curTime.Date)
            {
                history = RebuildPayRecord(entity);
            }
            if (entity.Status == 2)
                return;
            var starTime = curTime.Date;
            var endTime = curTime.Date.AddDays(1).AddSeconds(-1);
            var point = 0;
            PayChargehistoryMgr.GetPointForActivity(account, starTime, endTime, ref point);
            entity.ActivityStep = 1;
            if (point > 0)
            {
                entity.StepRecord = "1";
                entity.NeedSync = true;
                entity.Status = 1;
                entity.Countdown = point;
            }
            else
            {
                entity.StepRecord = "";
                return;
            }
            entity.SettlementDate = DateTime.Today;
        }

        #region SettlementLadderScore
        void SettlementLadderScore(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager, out ActivityHistoryEntity history)
        {
            history = null;
            DateTime curTime = DateTime.Now;
            entity.ActivityStep = 0;
            if (entity.RecordDate != curTime.Date)
            {
                history = RebuildPayRecord(entity);
            }
            if (entity.Status == 2)
                return;
            var starTime = curTime.Date;
            var endTime = curTime.Date.AddDays(1).AddSeconds(-1);
            var score = 0;
            LadderMatchMgr.GetPrizeScoreByTime(manager.Idx, starTime, endTime, ref score);

            entity.Countdown = score;
            var activitystepEntity = activityConfig.StepDic[1];

            if (score >= activitystepEntity.ConditionInt)
            {
                entity.ActivityStep = activitystepEntity.ActivityStep;
                entity.Status = 1;
                entity.NeedSync = true;
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementDailyTaskCount
        void SettlementDailyTaskCount(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager, out ActivityHistoryEntity history)
        {
            history = null;
            DateTime curTime = DateTime.Now;
            entity.ActivityStep = 0;
            if (entity.RecordDate != curTime.Date)
            {
                history = RebuildPayRecord(entity);
            }
            if (entity.Status == 2)
                return;
            
            var taskInfo = TaskCore.Instance.GetTaskListShow(manager.Idx);
            var count = taskInfo.FinishCount;
            entity.Countdown = count;
            var activitystepEntity = activityConfig.StepDic[1];
            if (count >= activitystepEntity.ConditionInt)
            {
                entity.ActivityStep = activitystepEntity.ActivityStep;
                entity.Status = 1;
                entity.NeedSync = true;
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementDailyBuyStaminaCount
        void SettlementDailyBuyStaminaCount(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager, out ActivityHistoryEntity history)
        {
            history = null;
            DateTime curTime = DateTime.Now;
            entity.ActivityStep = 0;
            if (entity.RecordDate != curTime.Date)
            {
                history = RebuildPayRecord(entity);
            }
            if (entity.Status == 2)
                return;
            var extraEntity = MallExtrarecordMgr.GetExtra(entity.ManagerId, 6);
            if (extraEntity.RecordDate != curTime.Date)
            {
                extraEntity.UsedCount = 0;
            }

            var count = extraEntity.UsedCount;

            entity.Countdown = count;
            var activitystepEntity = activityConfig.StepDic[1];

            if (count >= activitystepEntity.ConditionInt)
            {
                entity.ActivityStep = activitystepEntity.ActivityStep;
                entity.Status = 1;
                entity.NeedSync = true;
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementDailyLeagueMatchWinCount
        void SettlementDailyLeagueMatchWinCount(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager, out ActivityHistoryEntity history)
        {
            history = null;
            DateTime curTime = DateTime.Now;
            entity.ActivityStep = 0;
            if (entity.RecordDate != curTime.Date)
            {
                history = RebuildPayRecord(entity);
            }
            if (entity.Status == 2)
                return;
            var leagueInfo = LaegueManagerinfoMgr.GetById(entity.ManagerId);
            if (leagueInfo == null)
                return;
            var count = leagueInfo.DailyWinCount;
            entity.Countdown = count;
            var activitystepEntity = activityConfig.StepDic[1];

            if (count >= activitystepEntity.ConditionInt)
            {
                entity.ActivityStep = activitystepEntity.ActivityStep;
                entity.Status = 1;
                entity.NeedSync = true;
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion



        #region SettlementPayContinue
        void SettlementPayContinue(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager, out ActivityHistoryEntity history)
        {
            history = null;
            DateTime curTime = DateTime.Now;
            if (entity.Idx == 0)
                entity.ActivityStep = 0;
            if (entity.RecordDate != curTime.Date)
            {
                history = RebuildPayRecord(entity);
            }
            var payContinue = PayContinuingMgr.GetById(manager.Account);

            if (payContinue == null)
            {
                return;
            }
            var continueDay = payContinue.ContinuingDay;
            var totalPoint = payContinue.TotalPoint;
            if (continueDay < 7)
            {
                if (payContinue.LastPayDate == curTime.Date || payContinue.LastPayDate == curTime.Date.AddDays(-1))
                {
                    entity.Countdown = totalPoint;
                    entity.ActivityStep = BuildPayContinueStep(continueDay, 0); ;
                    return;
                }
                else
                {
                    entity.Countdown = 0;
                    entity.ActivityStep = 0;
                    return;
                }
            }
            else if (totalPoint >= PayCore.Instance.PayContinuePoint)
            {
                int maxStep = 0;
                foreach (var activitystepEntity in activityConfig.StepDic.Values)
                {
                    if (totalPoint >= activitystepEntity.ConditionInt && maxStep < activitystepEntity.ActivityStep)
                    {
                        maxStep = activitystepEntity.ActivityStep;
                    }
                }

                entity.ActivityStep = BuildPayContinueStep(7, maxStep);
                entity.NeedSync = true;
                entity.Countdown = totalPoint;
                entity.Status = 1;
            }
            else
            {
                if (payContinue.LastPayDate == curTime.Date)//今天
                {
                    entity.ActivityStep = BuildPayContinueStep(7, 0); ;
                    entity.NeedSync = true;
                    entity.Countdown = totalPoint;
                }
                else if (payContinue.LastPayDate == curTime.Date.AddDays(-1))
                {
                    var starTime = curTime.Date.AddDays(-6);
                    var endTime = curTime;
                    var point = 0;
                    PayChargehistoryMgr.GetPointForActivity(manager.Account, starTime, endTime, ref point);
                    entity.ActivityStep = BuildPayContinueStep(6, 0);
                    entity.NeedSync = true;
                    entity.Countdown = point;
                }
                else
                {
                    entity.ActivityStep = 0;
                    entity.NeedSync = true;
                    entity.Countdown = 0;
                }
            }
            entity.SettlementDate = curTime.Date;
        }

        int BuildPayContinueStep(int day, int step)
        {
            return day * 1000 + step;
        }

        int ParsePayContinueStep(int extraStep)
        {
            return extraStep % 1000;
        }
        #endregion

        #region SettlementYellowVip
        void SettlementYellowVip(ActivityRecordEntity entity, NbManagerEntity manager)
        {
            //if (entity.Status != 0)
            //    return;
            //var yellowinfo = TxYellowvipMgr.GetById(manager.Account);
            //if (yellowinfo != null && yellowinfo.IsYellowVip)
            //{
            //    entity.ActivityStep = 1;
            //    entity.Status = 1;
            //    entity.NeedSync = true;
            //}
            //entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementYellowVipOpening
        void SettlementYellowVipOpening(ActivityRecordEntity entity, NbManagerEntity manager)
        {

            //var yellowinfo = TxYellowvipMgr.GetById(manager.Account);
            //if (yellowinfo != null)
            //{
            //    int step = ConvertHelper.ConvertToInt(entity.StepRecord);
            //    if (yellowinfo.OpeningTimes <= 0 || yellowinfo.OpeningTimes <= step)
            //    {
            //        return;
            //    }
            //    entity.StepRecord = yellowinfo.OpeningTimes.ToString();
            //    entity.ActivityStep = 1;
            //    entity.Status = 1;
            //    entity.NeedSync = true;
            //}
            //entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementYellowVipDaily
        void SettlementYellowVipDaily(ActivityRecordEntity entity, NbManagerEntity manager)
        {
            DateTime curTime = DateTime.Now;
            //if (entity.RecordDate != curTime.Date)
            //{
            //    entity.RecordDate = curTime.Date;
            //    entity.ActivityStep = 0;
            //    entity.Status = 0;
            //}

            //if (entity.Status != 2)
            //{
            //    var yellowinfo = TxYellowvipMgr.GetById(manager.Account);
            //    if (yellowinfo != null && yellowinfo.IsYellowVip)
            //    {
            //        entity.ActivityStep = yellowinfo.YellowVipLevel;
            //        entity.Status = 1;
            //        entity.NeedSync = true;
            //    }
            //    entity.SettlementDate = DateTime.Today;
            //}
        }
        #endregion

        #region SettlementBlueVip
        void SettlementBlueVip(ActivityRecordEntity entity, NbManagerEntity manager)
        {
            //if (entity.Status != 0)
            //    return;
            //var yellowinfo = TxYellowvipMgr.GetById(ShareUtil.GetBlueVipAccount(manager.Account));
            //if (yellowinfo != null && yellowinfo.IsYellowVip)
            //{
            //    entity.ActivityStep = 1;
            //    entity.Status = 1;
            //    entity.NeedSync = true;
            //}
            //entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementBlueVipOpening
        void SettlementBlueVipOpening(ActivityRecordEntity entity, NbManagerEntity manager)
        {

            //var yellowinfo = TxYellowvipMgr.GetById(ShareUtil.GetBlueVipAccount(manager.Account));
            //if (yellowinfo != null)
            //{
            //    int step = ConvertHelper.ConvertToInt(entity.StepRecord);
            //    if (yellowinfo.OpeningTimes <= 0 || yellowinfo.OpeningTimes <= step)
            //    {
            //        return;
            //    }
            //    entity.StepRecord = yellowinfo.OpeningTimes.ToString();
            //    entity.ActivityStep = 1;
            //    entity.Status = 1;
            //    entity.NeedSync = true;
            //}
            //entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementBlueVipDaily
        void SettlementBlueVipDaily(ActivityRecordEntity entity, NbManagerEntity manager)
        {
            //DateTime curTime = DateTime.Now;
            //if (entity.RecordDate != curTime.Date)
            //{
            //    entity.RecordDate = curTime.Date;
            //    entity.ActivityStep = 0;
            //    entity.Status = 0;
            //}

            //if (entity.Status != 2)
            //{
            //    var yellowinfo = TxYellowvipMgr.GetById(ShareUtil.GetBlueVipAccount(manager.Account));
            //    if (yellowinfo != null && yellowinfo.IsYellowVip)
            //    {
            //        entity.ActivityStep = yellowinfo.YellowVipLevel;
            //        entity.Status = 1;
            //        entity.NeedSync = true;
            //    }
            //    entity.SettlementDate = DateTime.Today;
            //}
        }
        #endregion

        #region SettlementTxTask
        void SettlementTxTask(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager,string zoneId)
        {
            DateTime curTime = DateTime.Now;
            if (entity.Status != 0)
                return;
            switch (entity.ActivityId)
            {
                case 104:
                    entity.Status = 1;
                    entity.NeedSync = true;
                    break;
                case 105:
                    if (SettlementTxTaskCheck(manager.Idx, manager.Level, activityConfig.StepDic[entity.ActivityStep].ConditionInt, zoneId))
                    {
                        entity.Status = 1;
                        entity.NeedSync = true;
                    }
                    break;
                case 106:
                    if (SettlementTxTaskCheck(manager.Idx, manager.Level, activityConfig.StepDic[entity.ActivityStep].ConditionInt, zoneId))
                    {
                        entity.Status = 1;
                        entity.NeedSync = true;
                    }
                    break;
                case 107:
                    entity.Status = 1;
                    entity.NeedSync = true;
                    break;
            }
            entity.SettlementDate = DateTime.Today;
        }

        bool SettlementTxTaskCheck(Guid managerId, int level, int condition, string zoneId)
        {
            if (condition < 100)
            {
                return level >= condition;
            }
            else if (condition < 1000)
            {
                var count = condition - 100;
                var solution = NbSolutionMgr.GetById(managerId, zoneId);
                if (solution == null)
                    return false;
                return solution.OrangeCount >= count;
            }
            else
            {
                var extra = NbManagerextraMgr.GetById(managerId,zoneId);
                if (extra == null)
                    return false;
                return extra.Kpi >= condition;
            }
        }
        #endregion

        #region SettlementVip
        void SettlementVip(ActivityRecordEntity entity, NbManagerEntity manager)
        {
            var vip = manager.VipLevel;
            if (vip == 0)
                return;
            if (string.IsNullOrEmpty(entity.StepRecord) || entity.ActivityStep < vip) //有没有激活的
            {
                var stepList = FrameUtil.CastIntList(entity.StepRecord, ',');
                if (stepList == null)
                {
                    stepList = new List<int>(vip);
                }

                for (int i = 1; i <= vip; i++)
                {
                    if (stepList.Count < i)
                    {
                        stepList.Add(0);
                    }
                }
                entity.ActivityStep = vip;
                entity.StepRecord = string.Join(",", stepList);
                entity.NeedSync = true;
            }
            else
            {
                return;
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementLevelGift
        void SettlementLevelGift(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager)
        {
            if (entity.Status != 0)
                return;
            var extra = NbManagerextraMgr.GetById(manager.Idx);
            if (extra.LevelGiftStep <= 0 || entity.ActivityStep >= 6)
                return;

            var point = 0;
            var stepList = FrameUtil.CastIntList(entity.StepRecord, ',');
            if (string.IsNullOrEmpty(entity.StepRecord) || stepList == null)
            {
                stepList = new List<int>(6);
                stepList.Add(-1);
                stepList.Add(-1);
                stepList.Add(-1);
                stepList.Add(-1);
                stepList.Add(-1);
                stepList.Add(-1);
            }
            else if (stepList.Count == 3)
            {
                stepList.Add(-1);
                stepList.Add(-1);
                stepList.Add(-1);
            }
            else if (stepList.Count == 4)
            {
                stepList.Add(-1);
                stepList.Add(-1);
            }
            else if (stepList.Count == 5)
            {
                stepList.Add(-1);
            }
            for (int step = entity.ActivityStep + 1; step <= 6; step++)
            {
                if (stepList[step - 1] > 0)
                {
                    continue;
                }
                point = 0;
                PayChargehistoryMgr.GetPointForActivityNoTime(manager.Account, ref point);
                var key = step * 5 - 4;
                if (point >= activityConfig.StepDic[key].ConditionInt)
                {
                    entity.ActivityStep = step;
                    entity.NeedSync = true;
                    stepList[step - 1] = 1;
                }
                else
                {
                    if (stepList[step - 1] == -1)
                    {
                        stepList[step - 1] = 0;
                        entity.NeedSync = true;
                    }
                    entity.Countdown = point;
                }
            }
            if (entity.NeedSync)
            {
                entity.StepRecord = string.Join(",", stepList);
            }
            entity.SettlementDate = DateTime.Today;
        }

        DateTime GetLevelGiftTime(NbManagerextraEntity extraEntity, int step)
        {
            switch (step)
            {
                case 1:
                    return extraEntity.LevelGiftExpired;
                case 2:
                    return extraEntity.LevelGiftExpired2;
                case 3:
                    return extraEntity.LevelGiftExpired3;

            }
            return ShareUtil.BaseTime;
        }

        void SettlementLevelGift2(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager)
        {
            if (entity.Status != 0)
                return;
            var extra = NbManagerextraMgr.GetById(manager.Idx);
            if (extra.LevelGiftExpired == ShareUtil.BaseTime)
                return;

            var point = 0;
            PayChargehistoryMgr.GetPointForActivity(manager.Account, manager.RowTime, extra.LevelGiftExpired, ref point);
            if (extra.LevelGiftStep == 0)
                extra.LevelGiftStep = 1;
            var key = extra.LevelGiftStep * 5 - 4;
            if (point >= activityConfig.StepDic[key].ConditionInt)
            {
                entity.Status = 1;
                entity.NeedSync = true;
            }
            else if (extra.LevelGiftExpired <= DateTime.Now)
            {
                entity.Status = -1;
                entity.NeedSync = true;
            }
            else
            {
                entity.Countdown = activityConfig.StepDic[key].ConditionInt - point;
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementVipDaily
        void SettlementVipDaily(ActivityRecordEntity entity, NbManagerEntity manager)
        {
            DateTime curTime = DateTime.Now;
            if (entity.RecordDate != curTime.Date)
            {
                entity.RecordDate = curTime.Date;
                entity.ActivityStep = 0;
                entity.Status = 0;
            }
            if (entity.Status == 2 && entity.ActivityStep < manager.VipLevel)
                entity.Status = 0;
            if (entity.Status != 2)
            {
                if (manager.VipLevel > 0)
                {
                    entity.ActivityStep = manager.VipLevel;
                    entity.Status = 1;
                    entity.NeedSync = true;
                }
                entity.SettlementDate = DateTime.Today;
            }
        }
        #endregion

        #region SettlementGuidePrize
        void SettlementGuidePrize(ActivityRecordEntity entity, NbManagerEntity manager)
        {
            if (entity.Status != 0)
                return;
            var extra = NbManagerextraMgr.GetById(manager.Idx);
            if (extra.GuidePrizeExpired == ShareUtil.BaseTime)
                return;

            var prizeCount = extra.GuidePrizeCount;
            if (prizeCount == 1 || entity.ActivityStep < prizeCount) //有没有激活的
            {
                List<int> stepList = null;
                if (string.IsNullOrEmpty(entity.StepRecord))
                {
                    stepList = new List<int>(prizeCount);
                }
                else
                {
                    stepList = FrameUtil.CastIntList(entity.StepRecord, ',');
                }

                for (int i = 1; i <= prizeCount; i++)
                {
                    if (stepList.Count < i)
                    {
                        stepList.Add(0);
                    }
                }
                entity.ActivityStep = prizeCount;
                entity.StepRecord = string.Join(",", stepList);
                entity.NeedSync = true;
            }
            if (entity.ActivityStep >= 7 || DateTime.Today > extra.GuidePrizeExpired)
            {
                if (entity.StepRecord.Contains("0"))
                {
                    entity.Status = 1;
                }
                else
                {
                    entity.Status = 2;
                    entity.NeedSync = true;
                }
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region SettlementNewPlayerGift
        void SettlementNewPlayerGift(ActivityRecordEntity entity, DicActivityEntity activityConfig, NbManagerEntity manager)
        {
            if (entity.Idx == 0)
                entity.ActivityStep = 0;
            if (entity.Status == 2)
                return;
            var level = manager.Level;
            var activityStep = entity.ActivityStep;
            foreach (var activitystepEntity in activityConfig.StepDic.Values)
            {
                if (activitystepEntity.ActivityStep > activityStep)
                {
                    if (level >= activitystepEntity.ConditionInt)
                    {
                        activityStep = activitystepEntity.ActivityStep;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (entity.ActivityStep < activityStep) //有没有激活的
            {
                var stepList = FrameUtil.CastIntList(entity.StepRecord, ',');
                if (stepList == null)
                {
                    stepList = new List<int>(activityStep);
                }

                for (int i = 1; i <= activityStep; i++)
                {
                    if (stepList.Count < i)
                    {
                        stepList.Add(0);
                    }
                }
                entity.ActivityStep = activityStep;
                entity.StepRecord = string.Join(",", stepList);
                entity.NeedSync = true;
                entity.Countdown = level;
            }
            else
            {
                entity.Countdown = level;
                return;
            }
            entity.SettlementDate = DateTime.Today;
        }
        #endregion

        #region BuildPrize
        MessageCode BuildPrize(Guid managerId, int activityId, int activityStep, ItemPackageFrame package, NbManagerEntity manager, List<ActivityPrizeEntity> prizes, ref int point,ref bool isupdateCpackage, ref int bindPoint,ref int goldBar, ConstellationPackbager cpackbager = null,string zoneId="",int arenaType=0)
        {
            var prizeList = CacheFactory.ActivityCache.GetPrize(activityId, activityStep);
            if (prizeList == null)
                return MessageCode.NbParameterError;
            MessageCode code = MessageCode.Success;
            foreach (var entity in prizeList)
            {
                switch (entity.PrizeType)
                {
                    case (int)EnumPrizeItemType.Coin:
                        if (manager == null)
                        {
                            manager = ManagerCore.Instance.GetManager(managerId,zoneId);
                        }
                        Manager.ManagerUtil.AddManagerDataCoin(manager, entity.Count, EnumCoinChargeSourceType.ActivityPrize, activityId.ToString());
                        prizes.Add(new ActivityPrizeEntity() { Type = 1, Count = entity.Count });
                        break;
                    case (int)EnumPrizeItemType.Cardlib:
                        var itemCode = CacheFactory.LotteryCache.LotteryByLib(entity.SubType);
                        var itemCache = CacheFactory.ItemsdicCache.GetItem(itemCode);
                        if (itemCache == null)
                        {
                            return MessageCode.NbUpdateFail;
                        }
                        if (itemCache.ItemType == (int)EnumItemType.PlayerCard)
                        {
                            if (activityId == (int)EnumActivityType.PayFirst || entity.Count == 2)
                            {
                                code = package.AddPlayerCard(itemCode, 1, entity.IsBinding, 1,false);
                                prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = entity.IsBinding, Strength = 1, ItemCode = itemCode });
                                if (code != MessageCode.Success)
                                    return code;
                            }
                            code = package.AddPlayerCard(itemCode, 1, entity.IsBinding, entity.Strength,false);
                            prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = itemCode });
                        }
                        else
                        {
                            if (itemCache.ItemType == (int)EnumItemType.MallItem &&
                            (itemCache.MallEffectType == (int)EnumMallEffectType.TheStars ||
                             itemCache.MallEffectType == (int)EnumMallEffectType.TheUniverse))
                            {
                                code = cpackbager.AddItems(entity.SubType, 1, entity.IsBinding);
                                isupdateCpackage = true;
                            }
                            else
                            code = package.AddItem(itemCode, entity.Strength, entity.IsBinding,false);
                            prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = itemCode });
                        }
                        if (code != MessageCode.Success)
                            return code;
                        break;
                    case (int)EnumPrizeItemType.Item:
                        var itemCache2 = CacheFactory.ItemsdicCache.GetItem(entity.SubType);
                        if (itemCache2 == null)
                        {
                            return MessageCode.NbUpdateFail;
                        }
                        var itemCount = entity.Count;
                        if (itemCache2.ItemType == (int)EnumItemType.PlayerCard && itemCount == 2)
                        {
                            code = package.AddPlayerCard(entity.SubType, 1, entity.IsBinding, 1,false);
                            prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = entity.IsBinding, Strength = 1, ItemCode = entity.SubType });
                            if (code != MessageCode.Success)
                                return code;
                            itemCount = 1;
                        }
                        if (itemCache2.ItemType == (int) EnumItemType.MallItem &&
                            (itemCache2.MallEffectType == (int) EnumMallEffectType.TheStars ||
                             itemCache2.MallEffectType == (int) EnumMallEffectType.TheUniverse))
                        {
                            code = cpackbager.AddItems(entity.SubType, itemCount, entity.IsBinding);
                            isupdateCpackage = true;
                        }
                        else
                            code = package.AddItems(entity.SubType, itemCount, entity.Strength, entity.IsBinding,false);
                        if (code != MessageCode.Success)
                            return code;
                        prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = itemCount, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = entity.SubType });
                        break;
                    case (int)EnumPrizeItemType.Point:
                        code = MessageCode.Success;
                        point = entity.Count;
                        prizes.Add(new ActivityPrizeEntity() { Type = 3, Count = entity.Count, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = entity.SubType });
                        break;
                    case (int)EnumPrizeItemType.BindPoint:
                        code = MessageCode.Success;
                        bindPoint = entity.Count;
                        prizes.Add(new ActivityPrizeEntity()
                        {
                            Type = 8,
                            Count = entity.Count,
                            IsBinding = entity.IsBinding,
                            Strength = entity.Strength,
                            ItemCode = entity.SubType
                        });
                        break;
                    case (int)EnumPrizeItemType.RandomSuit:
                        var suitlist = CacheFactory.LotteryCache.LotteryEquipmentSuitListByType(entity.SubType,
                                                                                            entity.Strength);
                        if (suitlist != null && suitlist.Count > 0)
                        {
                            if (entity.Count == 1)
                            {
                                var index = RandomHelper.GetInt32WithoutMax(0, suitlist.Count);
                                code = package.AddItems(suitlist[index], 1, 0, entity.IsBinding,false);
                                if (code != MessageCode.Success)
                                    return code;
                                prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = suitlist[index] });
                            }
                            else
                            {
                                foreach (var suit in suitlist)
                                {
                                    code = package.AddItems(suit, 1, 0, entity.IsBinding,false);
                                    if (code != MessageCode.Success)
                                        return code;
                                    prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = suit });
                                }
                            }
                        }
                        break;
                    case (int)EnumPrizeItemType.RandomTheContract:
                    case (int)EnumPrizeItemType.RandomPlary:
                        var managerList = EverydayactivityprizeMgr.GetManagerInfo(manager.Idx);
                        var prize =
                            managerList.FindAll(
                                r =>
                                    r.ActivityId == entity.ActivityId && r.ActivityStep == entity.ActivityStep &&
                                    r.SubType == entity.SubType);
                        if (prize.Count > 0)
                        {
                            var itemCache3 = CacheFactory.ItemsdicCache.GetItem(prize[0].ItemCode);
                            if (itemCache3 == null)
                            {
                                return MessageCode.NbUpdateFail;
                            }
                            var itemCount1 = entity.Count;
                            if (itemCache3.ItemType == (int)EnumItemType.PlayerCard && itemCount1 == 2)
                            {
                                code = package.AddPlayerCard(itemCache3.ItemCode, 1, entity.IsBinding, 1,false);
                                prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = 1, IsBinding = entity.IsBinding, Strength = 1, ItemCode = entity.SubType });
                                if (code != MessageCode.Success)
                                    return code;
                                itemCount1 = 1;
                            }
                            code = package.AddItems(itemCache3.ItemCode, itemCount1, entity.Strength, entity.IsBinding,false);
                            if (code != MessageCode.Success)
                                return code;
                            prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = itemCount1, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = entity.SubType });
                            break;
                        }
                        return MessageCode.NbUpdateFail;
                    case (int)EnumPrizeItemType.ArenaCardPackage://竞技场卡包
                        var aItemCode = 0;
                        switch (arenaType)
                        {
                            case 1:
                                aItemCode = 310163;
                                break;
                            case 2:
                                aItemCode = 310164;
                                break;
                            case 3:
                                aItemCode = 310165;
                                break;
                            case 4:
                                aItemCode = 310166;
                                break;
                            case 5:
                                aItemCode = 310167;
                                break;
                        }

                        code = package.AddItems(aItemCode, entity.Count, entity.Strength, entity.IsBinding,
                            entity.IsBinding);
                        if (code != MessageCode.Success)
                            return code;
                        prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = entity.Count, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = aItemCode });
                        code = MessageCode.Success;
                        break;
                    case (int)EnumPrizeItemType.GoldBar://金条
                         code = MessageCode.Success;
                        goldBar = entity.Count;
                        prizes.Add(new ActivityPrizeEntity() { Type = 10, Count = entity.Count, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = entity.SubType });
                        break;
                    case (int)EnumPrizeItemType.LegendPlayerDebris://传奇球员卡碎片
                        code = package.AddItems(LegendDebrisCode, entity.Count, entity.Strength, entity.IsBinding,
                            entity.IsBinding);
                        if (code != MessageCode.Success)
                            return code;
                        prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = entity.Count, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = LegendDebrisCode });
                        code = MessageCode.Success;
                        break;
                    case (int)EnumPrizeItemType.LegendPlayer://传奇球员卡
                         code = package.AddItems(LegendCode, entity.Count, entity.Strength, entity.IsBinding,
                            entity.IsBinding);
                        if (code != MessageCode.Success)
                            return code;
                        prizes.Add(new ActivityPrizeEntity() { Type = 2, Count = entity.Count, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = LegendDebrisCode });
                        code = MessageCode.Success;
                        break;
                    default:
                        return MessageCode.NbParameterError;
                        break;
                }
            }
            return MessageCode.Success;
        }
        #endregion

        #region RebuildRecord
        ActivityHistoryEntity RebuildOnlineRecord(ActivityRecordEntity entity)
        {
            var history = BuildHistory(entity);
            entity.ActivityStep = 0;
            entity.StepRecord = "";
            entity.OnlinePrevSeconds = 0;
            entity.RecordDate = DateTime.Today;
            entity.UpdateTime = DateTime.Now;
            entity.NeedSync = true;
            entity.Status = 0;
            return history;
        }

        ActivityHistoryEntity RebuildActivityRecord(ActivityRecordEntity entity)
        {
            var history = BuildHistory(entity);
            entity.ActivityStep = 1;
            entity.StepRecord = "0";
            entity.RecordDate = DateTime.Today;
            entity.UpdateTime = DateTime.Now;
            entity.NeedSync = true;
            entity.Status = 0;
            return history;
        }

        ActivityHistoryEntity RebuildPayRecord(ActivityRecordEntity entity)
        {
            var history = BuildHistory(entity);
            entity.ActivityStep = 0;
            entity.StepRecord = "";
            entity.RecordDate = DateTime.Today;
            entity.UpdateTime = DateTime.Now;
            entity.NeedSync = true;
            entity.Status = 0;
            return history;
        }

        ActivityHistoryEntity BuildHistory(ActivityRecordEntity entity)
        {
            ActivityHistoryEntity history = new ActivityHistoryEntity();
            history.ActivityId = entity.ActivityId;
            history.ActivityStep = entity.ActivityStep;
            history.ManagerId = entity.ManagerId;
            history.RecordDate = entity.RecordDate;
            history.Status = entity.Status;
            history.RowTime = entity.RowTime;
            history.StepRecord = entity.StepRecord;
            history.UpdateTime = entity.UpdateTime;
            return history;
        }
        #endregion

        #endregion

        #region SavePrize

        public MessageCode SavePrize(string activityKey, ActivityRecordEntity activityRecord, ItemPackageFrame package, NbManagerEntity manager, NbManagerextraEntity managerextra, int point, int bindPoint,int goldBar, int realStep, ref PayUserEntity payUserEntity, bool isupdateCpackage = false,ConstellationPackbager cpackage = null,string zoneId="")
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(zoneId,EnumDbType.Main)))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SavePrize(transactionManager.TransactionObject, activityKey, activityRecord, package, manager, managerextra, point, bindPoint, goldBar, realStep, ref payUserEntity, isupdateCpackage, cpackage, zoneId);
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
                SystemlogMgr.ErrorByZone("SavePrize", ex, zoneId);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_SavePrize(DbTransaction transaction, string activityKey, ActivityRecordEntity activityRecord, ItemPackageFrame package, NbManagerEntity manager, NbManagerextraEntity managerextra, int point, int bindPoint, int goldBar, int realStep, ref PayUserEntity payUserEntity, bool isupdateCpackage, ConstellationPackbager cpackage, string zoneId)
        {
            int returnCode = -2;
            ActivityRecordMgr.SavePrize(activityRecord.Idx, activityRecord.ManagerId, activityRecord.ActivityId,
                                        activityRecord.ActivityStep,
                                        activityRecord.StepRecord, activityRecord.RecordDate,
                                        activityRecord.SettlementDate, activityRecord.Status, activityRecord.UpdateTime,
                                        activityRecord.RowVersion, activityKey, ref returnCode, transaction,zoneId);
            if (returnCode != 0)
            {
                if (returnCode == 901)
                    return MessageCode.ActivityHasReceive;
                else
                {
                    SystemlogMgr.Info("SavePrize", "ActivityRecordMgr.SavePrize fail");
                    return MessageCode.NbUpdateFail;
                }
            }
            if (!package.Save(transaction))
                return MessageCode.NbUpdateFailPackage;
            if (manager != null)
            {
                if (!Manager.ManagerUtil.SaveManagerData(manager, managerextra, transaction,zoneId))
                {
                    return MessageCode.NbUpdateFailManager;
                }
            }
            if (isupdateCpackage)
            {
                //if (!cpackage.Save(transaction))
                //    return MessageCode.NbUpdateFailPackage;
            }
            if (managerextra != null)
            {
                if (activityRecord.ActivityId == (int) EnumActivityType.PayFirst)
                {
                    if (!NbManagerextraMgr.UpdatePayFirst(managerextra.ManagerId, managerextra.PayFirstFlag, transaction, zoneId))
                        return MessageCode.NbUpdateFail;
                }
                else if (activityRecord.ActivityId == (int)EnumActivityType.GuidePrize)
                {
                    if (!NbManagerextraMgr.Update(managerextra, transaction, zoneId))
                        return MessageCode.NbUpdateFail;
                }
            }
            if (activityRecord.ActivityId == (int)EnumActivityType.LevelGift)
            {
                if (!NbManagerextraMgr.UpdateLevelGift(activityRecord.ManagerId, DateTime.Now.AddHours(-1), realStep, transaction, zoneId))
                    return MessageCode.NbUpdateFail;
            }

            if (point > 0)
            {
                if (manager == null)
                    manager = ManagerCore.Instance.GetManager(activityRecord.ManagerId, zoneId);
                var code = PayCore.Instance.AddBonus(manager.Account, point, EnumChargeSourceType.ActivityPrize,
                    string.Format("{0}_{1}", activityRecord.Idx,
                        ShareUtil.GenerateComb().ToString()),transaction, zoneId);
                if (code != MessageCode.Success)
                    return code;
            }

            if (bindPoint > 0)
            {
                if (payUserEntity == null)
                {
                    payUserEntity = new PayUserEntity();
                    payUserEntity.Account = manager.Account;
                    payUserEntity.IsNew = true;
                    payUserEntity.BindPoint += bindPoint;
                    payUserEntity.RowTime = DateTime.Now;
                }
                else
                {
                    payUserEntity.BindPoint += bindPoint;
                }
                if (payUserEntity.IsNew)
                {
                    if (!PayUserMgr.Insert(payUserEntity, transaction))
                        return MessageCode.NbUpdateFail;
                }
                else
                {
                    if (!PayUserMgr.Update(payUserEntity, transaction))
                        return MessageCode.NbUpdateFail;
                }
                
            }
            if (goldBar > 0)
            {
                var goldBarManager = ScoutingGoldbarMgr.GetById(activityRecord.ManagerId);
                if (goldBarManager == null)
                {
                    goldBarManager = new ScoutingGoldbarEntity(activityRecord.ManagerId, goldBar, 0, 0, 0, DateTime.Now,
                        DateTime.Now);
                    if (!ScoutingGoldbarMgr.Insert(goldBarManager, transaction))
                        return MessageCode.NbUpdateFail;
                }
                else
                {
                    goldBarManager.GoldBarNumber = goldBarManager.GoldBarNumber + goldBar;
                    if (!ScoutingGoldbarMgr.Update(goldBarManager, transaction))
                        return MessageCode.NbUpdateFail;
                }
            }
            return MessageCode.Success;
        }
        #endregion
    }
}
