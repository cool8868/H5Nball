using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Friend;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response.Teammember;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Teammember
{
    /// <summary>
    /// 球员训练
    /// </summary>
    public class PlayerTrain
    {
        #region .ctor

        private ConcurrentDictionary<Guid, TeammemberTrainEntity> _trainDic;
        private ConcurrentDictionary<Guid, List<Guid>> _managerTrainDic;
        private ConcurrentDictionary<int, int> _limitPrizeCount;
        /// <summary>
        /// 碎片（合同页）专用类型
        /// </summary>
        private const int _contractItemType = 10;
        private DateTime _lastLimitUpdateTime;
        private readonly int _trainExpPerSecond;
        private readonly int _friendDayHelpMax;
        private readonly int _friendPerIntimacyAddCoin;
        private readonly int _speedUpItemCode;
        private readonly int _speedUpItemAddExp;
        private readonly int _speedUpFriendAddExp;


        public PlayerTrain(int b)
        {
            _trainExpPerSecond = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.TrainExpPerSecond);
            _friendDayHelpMax = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.FriendDayHelpMax);
            _friendPerIntimacyAddCoin =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.FriendPerIntimacyAddCoin);
            _speedUpItemCode = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.SpeedUpItemCode);
            _speedUpItemAddExp = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.SpeedUpItemAddExp);
            _speedUpFriendAddExp =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.SpeedUpFriendAddExp);
            var list = TeammemberTrainMgr.GetTrainList();
            _trainDic = new ConcurrentDictionary<Guid, TeammemberTrainEntity>();
            _managerTrainDic = new ConcurrentDictionary<Guid, List<Guid>>();
            foreach (var entity in list)
            {
                _trainDic.TryAdd(entity.Idx, entity);
                if (!_managerTrainDic.ContainsKey(entity.ManagerId))
                    _managerTrainDic.TryAdd(entity.ManagerId, new List<Guid>());
                _managerTrainDic[entity.ManagerId].Add(entity.Idx);
            }
            _limitPrizeCount = new ConcurrentDictionary<int, int>();
        }

        private void UpdateLimitPrizeCount()
        {
            _limitPrizeCount=new ConcurrentDictionary<int, int>();
            var startTime = DateTime.Now.Date;
            var endTime = DateTime.Now.Date.AddDays(1);
            var count = 0;
            //钻石 限量2000
            FriendOpenboxrecordMgr.GetCountByPrizeType(startTime, endTime, (int)EnumItemType.Point, ref count);
            _limitPrizeCount.TryAdd((int)EnumItemType.Point, count);

            //强化百搭卡 限量10个
            FriendOpenboxrecordMgr.GetCountByPrizeInfo(startTime, endTime, (int)EnumItemType.MallItem, 310101, ref count);
            _limitPrizeCount.TryAdd(310101, count);
            //能力值82-85的球星碎片 限量20
            FriendOpenboxrecordMgr.GetCountByPrizeType(startTime, endTime, _contractItemType, ref count);
            _limitPrizeCount.TryAdd(_contractItemType, count);

            _lastLimitUpdateTime = DateTime.Now.Date;
        }

        private void UpdateLimitPrizeCount(int prizeType,int count)
        {
            if (_limitPrizeCount.ContainsKey(prizeType))
                _limitPrizeCount[prizeType] += count;
        }

        #endregion

        #region Facade

        public static PlayerTrain Instance
        {
            get { return SingletonFactory<PlayerTrain>.SInstance; }
        }

        public MessageCode TrainJob()
        {
            try
            {
                foreach (var entity in _trainDic.Values)
                {
                    switch (entity.TrainState)
                    {
                        case (int)EnumTrainState.Train:
                            SettlementTrain(entity);
                            try
                            {
                                if (entity.StartTime.AddDays(3) < DateTime.Now)
                                    EndTrain(entity.ManagerId, entity.Idx);
                            }
                            catch (Exception ex)
                            {
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("TrainJob", ex);
                return MessageCode.Exception;
            }

            return MessageCode.Success;
        }
        /// <summary>
        /// 获取训练场球员信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="onlyTrain"></param>
        /// <returns></returns>
        public TeammemberTrainListResponse GetTeammemberListForTrain(Guid managerId, bool onlyTrain = true)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
              if (manager == null)
                return ResponseHelper.Create<TeammemberTrainListResponse>(MessageCode.NbParameterError);

            //只能看主力情况
            //var teammembers = MatchDataHelper.GetTeammembers(managerId);
            var teammembers = GetManagerTrainList(managerId);
            var response = ResponseHelper.CreateSuccess<TeammemberTrainListResponse>();
            response.Data = new TeammemberTrainListEntity();
            response.Data.Teammembers = new List<TeammemberTrainEntity>();
            response.Data.TrainSeatCount = manager.TrainSeatMax;
            bool isUpdatePackage = false;
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.PlayreTrain);
            foreach (var entity in teammembers)
            {
                var trainEntity = GetTrainEntityNew(entity.Idx);

                if (onlyTrain) //只显示在训练的
                {
                    if (trainEntity == null || trainEntity.TrainState != (int)EnumTrainState.Train)
                        continue;
                }

                if (trainEntity == null)
                {
                    trainEntity = new TeammemberTrainEntity();
                    trainEntity.Idx = entity.Idx;
                    trainEntity.Level = entity.Level;
                    trainEntity.EXP = 0;
                    trainEntity.TrainState = (int)EnumTrainState.None;
                    trainEntity.ManagerId = entity.ManagerId;
                    trainEntity.PlayerId = entity.PlayerId;
                }

                if (trainEntity.TrainState == (int)EnumTrainState.Train)
                {
                    trainEntity.TrainTick = ShareUtil.GetTimeTick(DateTime.Now) - ShareUtil.GetTimeTick(trainEntity.StartTime);
                    if (package != null)
                    {
                        var player = package.GetPlayer(trainEntity.Idx);
                        if (player != null)
                        {
                            var property = player.ItemProperty as PlayerCardProperty;
                            if (property != null)
                            {
                                if (!property.IsTrain)
                                {
                                    isUpdatePackage = true;
                                    property.IsTrain = true;
                                    player.ItemProperty = property;
                                    package.Update(player);
                                }
                            }
                        }
                    }
                }

                trainEntity.PropertyPoint = entity.PropertyPoint;
                trainEntity.LevelupExp = CacheFactory.TeammemberCache.GetExp(trainEntity.Level);
                trainEntity.Kpi = entity.Kpi;
                response.Data.Teammembers.Add(trainEntity);
            }
            if (isUpdatePackage)
            {
                package.Save();
            }
            return response;
        }

        /// <summary>
        /// 获取训练中的球员信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public TeammemberTrainResponse GetTrainInfo(Guid managerId, Guid teammemberId)
        {
            var trainEntity = GetTrainEntity(teammemberId);
            if (trainEntity == null || trainEntity.ManagerId != managerId)
                return ResponseHelper.InvalidParameter<TeammemberTrainResponse>();

            if (trainEntity.TrainState == (int)EnumTrainState.Train)
            {
                trainEntity.TrainTick = ShareUtil.GetTimeTick(DateTime.Now) - ShareUtil.GetTimeTick(trainEntity.StartTime);
            }
           
            trainEntity.PropertyPoint = trainEntity.PropertyPoint;
            trainEntity.LevelupExp = CacheFactory.TeammemberCache.GetExp(trainEntity.Level);
            trainEntity.Kpi = MatchDataHelper.GetTeammemberKpi(managerId, teammemberId);
            var response = ResponseHelper.CreateSuccess<TeammemberTrainResponse>();
            response.Data = trainEntity;
            return response;
        }

        public TeammemberTrainActionResponse HelpTrain(Guid managerId, int friendRecordId, Guid trainId, bool hasTask)
        {
            var record = FriendCore.Instance.GetFriendById(friendRecordId);
            if (record.ManagerId != managerId)
                return ResponseHelper.InvalidParameter<TeammemberTrainActionResponse>();
            if (record.DayHelpTrainCount >= _friendDayHelpMax)
                return ResponseHelper.Create<TeammemberTrainActionResponse>(MessageCode.FriendDayHelpOver);

            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            var manager = ManagerCore.Instance.GetManager(managerId);
            int maxHelpCount = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel,
                                                                       EnumVipEffect.TrainHelpFriendCount);
            if (managerExtra.HelpTrainCount >= maxHelpCount)
            {
                return ResponseHelper.Create<TeammemberTrainActionResponse>(MessageCode.FriendHelpTrainOver);
            }
            var trainEntity = GetTrainEntity(trainId);
            if (trainEntity == null || trainEntity.ManagerId != record.FriendId)
                return ResponseHelper.InvalidParameter<TeammemberTrainActionResponse>();
            if (trainEntity.TrainState != (int)EnumTrainState.Train)
                return ResponseHelper.Create<TeammemberTrainActionResponse>(MessageCode.TeammemberTrainFinish);
            //if(trainEntity)
            var byManagerExtra = ManagerCore.Instance.GetManagerExtra(record.FriendId);
            var byManager = ManagerCore.Instance.GetManager(record.FriendId);
            int byMaxHelpCount = CacheFactory.VipdicCache.GetEffectValue(byManager.VipLevel,
                                                                       EnumVipEffect.TrainHelpAcceptCount);
            if (byManagerExtra.ByHelpTrainCount >= byMaxHelpCount)
                return ResponseHelper.Create<TeammemberTrainActionResponse>(MessageCode.FriendByHelpTrainOver);
            DateTime curTime = DateTime.Now;
            int expValue = 500;
            var coin = 100 * Math.Sqrt(manager.Level);//100*level/2
            var addCoin = Convert.ToInt32(coin);

            var addFriendShipPoint = 20;
            var curFriendShipPoint = 0;
           
            var newTrainEntity = trainEntity.Clone();
            bool isExpMax = false;
            AddExp(newTrainEntity, expValue,ref isExpMax);
            if (isExpMax)
                return ResponseHelper.Create<TeammemberTrainActionResponse>(MessageCode.TeammemberTrainLevelNoAddExp);
            bool isLevelup = CalTrain(curTime, newTrainEntity);
            //更新背包中卡牌等级
            var package = ItemCore.Instance.GetPackage(trainEntity.ManagerId, EnumTransactionType.PlayreTrain);
            var player = package.GetPlayer(trainEntity.Idx);
            if (player == null)
            {
                return ResponseHelper.InvalidParameter<TeammemberTrainActionResponse>();
            }
            var playerCardProperty = player.ItemProperty as PlayerCardProperty;
            if (playerCardProperty != null)
            {
                playerCardProperty.Level = newTrainEntity.Level;
                playerCardProperty.Exp = newTrainEntity.EXP;
                //if (isStop)
                //    playerCardProperty.IsTrain = false;
            }
            var messCode = package.Update(player);
            if (messCode != MessageCode.Success)
                return ResponseHelper.Create<TeammemberTrainActionResponse>(messCode);
            int oldIntimacy = record.Intimacy;
            FriendCore.Instance.AddHelpTrainIntimacy(record);
            
            managerExtra.HelpTrainCount++;
            byManagerExtra.ByHelpTrainCount++;

            var code = SaveHelpTrain(package,record, newTrainEntity, managerExtra, byManagerExtra, manager, addCoin, addFriendShipPoint, ref curFriendShipPoint);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<TeammemberTrainActionResponse>(code);
            }
            CopyTrainProperty(trainEntity, newTrainEntity);
            if (addCoin > 0)
            {
                ManagerCore.Instance.UpdateCoinAfter(manager);
            }

            var response = ResponseHelper.Create<TeammemberTrainActionResponse>(code);
            response.Data = BuildTrainActionEntity(trainEntity, trainEntity.TrainState == (int)EnumTrainState.Train);
            response.Data.AddIntimacy = record.Intimacy - oldIntimacy;
            response.Data.CurIntimacy = record.Intimacy;
            response.Data.CurPoint = -1;
            response.Data.AddCoin = addCoin;
            response.Data.CurCoin = manager.Coin;
            response.Data.AddFriendShipPoint = addFriendShipPoint;
            response.Data.CurFriendShipPoint = curFriendShipPoint;

            List<PopMessageEntity> popList = null;
            popList = TaskHandler.Instance.TeammemberTrainHelp(managerId, byManager.Name);
            if (isLevelup)
            {
                MatchDataCacheHelper.DeleteTeamembersCache(managerId, true);
                var pop2 = TaskHandler.Instance.TeammemberTrain(trainEntity.ManagerId, trainEntity.Level, false);
                if (popList == null)
                    popList = pop2;
                else
                {
                    if (pop2 != null && pop2.Count > 0)
                    {
                        popList.AddRange(pop2);
                    }
                }
            }
            response.Data.PopMsg = popList;
            return response;
        }

        void CopyTrainProperty(TeammemberTrainEntity entity, TeammemberTrainEntity newTrainEntity)
        {
            if (entity.Level != newTrainEntity.Level)
            {
                MatchDataCacheHelper.DeleteTeamembersCache(entity.ManagerId, false);
            }
            entity.Level = newTrainEntity.Level;
            entity.EXP = newTrainEntity.EXP;
            entity.TrainStamina = newTrainEntity.TrainStamina;
            entity.TrainState = newTrainEntity.TrainState;
            entity.StartTime = newTrainEntity.StartTime;
            entity.SettleTime = newTrainEntity.SettleTime;
            entity.Status = newTrainEntity.Status;
        }

        TeammemberTrainActionEntity BuildTrainActionEntity(TeammemberTrainEntity trainEntity, bool isRest = false)
        {
            var entity = new TeammemberTrainActionEntity();
            entity.TrainState = trainEntity.TrainState;
            entity.EXP = trainEntity.EXP;
            entity.Level = trainEntity.Level;
            entity.LevelupExp = CacheFactory.TeammemberCache.GetExp(trainEntity.Level);
            entity.TrainStamina = trainEntity.TrainStamina;
            return entity;
        }

        #region SaveHelpTrain
        MessageCode SaveHelpTrain(ItemPackageFrame package,FriendManagerEntity entity, TeammemberTrainEntity trainEntity, NbManagerextraEntity managerextra, NbManagerextraEntity bymanagerextra, NbManagerEntity manager, int addCoin, int addFriendShipPoint, ref int curFriendShipPoint)
        {
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                var messageCode = Tran_SaveHelpTrain(package,transactionManager.TransactionObject, entity, trainEntity, managerextra, bymanagerextra, manager, addCoin, addFriendShipPoint, ref curFriendShipPoint);

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

        MessageCode Tran_SaveHelpTrain(ItemPackageFrame package,DbTransaction transaction, FriendManagerEntity entity, TeammemberTrainEntity trainEntity, NbManagerextraEntity managerextra, NbManagerextraEntity bymanagerextra, NbManagerEntity manager, int addCoin, int addFriendShipPoint, ref int curFriendShipPoint)
        {
            if (!package.Save(transaction))
                return MessageCode.NbUpdateFail;
            if (!FriendManagerMgr.Update(entity, transaction))
                return MessageCode.NbUpdateFail;
            if (!NbManagerextraMgr.SaveHelpCount(managerextra.ManagerId, managerextra.HelpTrainCount, managerextra.ByHelpTrainCount, managerextra.RecordDate, transaction))
                return MessageCode.NbUpdateFail;
            if (!NbManagerextraMgr.SaveHelpCount(bymanagerextra.ManagerId, bymanagerextra.HelpTrainCount, bymanagerextra.ByHelpTrainCount, bymanagerextra.RecordDate, transaction))
                return MessageCode.NbUpdateFail;
            if(!NbManagerMgr.AddFriendShipPoint(managerextra.ManagerId,addFriendShipPoint,ref curFriendShipPoint,transaction))
                return MessageCode.NbUpdateFail;

            int returnCode = -2;
            TeammemberTrainMgr.UpdateData(trainEntity.Idx, trainEntity.Level, trainEntity.EXP, trainEntity.TrainStamina, trainEntity.TrainState,
        trainEntity.StartTime, trainEntity.SettleTime, trainEntity.Status, ShareUtil.GetTableMod(trainEntity.ManagerId), ref returnCode, transaction);
            if (returnCode != 0)
            {
                return MessageCode.NbUpdateFail;
            }
            return ManagerCore.Instance.AddCoin(manager, addCoin, EnumCoinChargeSourceType.HelpTrain,
                                         trainEntity.Idx.ToString(), transaction);
        }
        #endregion

        public TeammemberTrainHelpResponse GetHelpTrainList(Guid managerId, int friendRecordId)
        {
            var record = FriendCore.Instance.GetFriendById(friendRecordId);
            if (record.ManagerId != managerId)
                return ResponseHelper.InvalidParameter<TeammemberTrainHelpResponse>();

            var trainListResponse = GetTeammemberListForTrain(record.FriendId, true);    

            var response = ResponseHelper.CreateSuccess<TeammemberTrainHelpResponse>();
            response.Data = new TeammemberTrainHelpEntity();
            if (trainListResponse.Code == (int) MessageCode.Success)
            {
                response.Data.HelpTrainInfo = trainListResponse.Data;
            }
            response.Data.HasBox = record.DayOpenBoxCount == 0;

            return response;
        }

        public TeammemberTrainOpenBoxResponse OpenBox(Guid managerId, int friendRecordId)
        {
            var record = FriendCore.Instance.GetFriendById(friendRecordId);
            if (record.ManagerId != managerId)
                return ResponseHelper.InvalidParameter<TeammemberTrainOpenBoxResponse>();
            var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            if (managerExtra != null && managerExtra.OpenBoxCount >= 10)
                return ResponseHelper.Create<TeammemberTrainOpenBoxResponse>(MessageCode.FriendBoxCountOver);
            if(record.DayOpenBoxCount>0)
                return ResponseHelper.Create<TeammemberTrainOpenBoxResponse>(MessageCode.FriendBoxHasOpen);

            if (_lastLimitUpdateTime.Date != DateTime.Now.Date)
                UpdateLimitPrizeCount();

            //宝箱奖励
           var prizeEntity = new TeammemberTrainOpenBoxEntity();
           var code = SendPrize(managerId, record,managerExtra, ref prizeEntity);
           if (code != MessageCode.Success)
               return ResponseHelper.Create<TeammemberTrainOpenBoxResponse>(MessageCode.FailUpdate);



           if (prizeEntity.PrizeItem==310101)
               UpdateLimitPrizeCount(310101, 1);
           else if(prizeEntity.PrizeType==(int)EnumItemType.Point)
               UpdateLimitPrizeCount(prizeEntity.PrizeType,prizeEntity.PrizeCount);
           else if (prizeEntity.PrizeType == _contractItemType)
               UpdateLimitPrizeCount(_contractItemType, prizeEntity.PrizeCount);

            var response = ResponseHelper.CreateSuccess<TeammemberTrainOpenBoxResponse>();
            response.Data = prizeEntity;
            return response;
        }

        public MessageCode SendPrize(Guid managerId,FriendManagerEntity friendRecord,NbManagerextraEntity managerExtra, ref TeammemberTrainOpenBoxEntity prizeEntity)
        {
            bool pointIsOver = false;
            bool contractIsOver = false;
            bool item310101IsOver = false;

            var prizedPointCount = 0;
            FriendOpenboxrecordMgr.GetCountByManagerAndPrizeType(managerId, DateTime.Now.Date,
                DateTime.Now.Date.AddDays(1), (int) EnumItemType.Point, ref prizedPointCount);

            if (_limitPrizeCount[(int)EnumItemType.Point] >= 2000 || prizedPointCount >= 40)
            {
                pointIsOver = true;
            }
            if (_limitPrizeCount[_contractItemType] > 20)
            {
                contractIsOver = true;
            }
            if (_limitPrizeCount[310101] > 20)
            {
                item310101IsOver = true;
            }


            prizeEntity = RandomBoxPrize(pointIsOver, contractIsOver, item310101IsOver);
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                var code = Tran_SendPrize(managerId, friendRecord, managerExtra, ref prizeEntity, transactionManager.TransactionObject);
                if (code == ShareUtil.SuccessCode)
                {
                    transactionManager.Commit();
                }
                else
                {
                    transactionManager.Rollback();
                }
                return code;
            }
        }

        private MessageCode Tran_SendPrize(Guid managerId, FriendManagerEntity friendRecord, NbManagerextraEntity managerExtra, ref TeammemberTrainOpenBoxEntity prizeEntity, DbTransaction transaction)
        {
            var code = MessageCode.Success;
            switch (prizeEntity.PrizeType)
            {
                case (int)EnumItemType.Point:
                    var itemDic = CacheFactory.ItemsdicCache.GetItem(prizeEntity.PrizeItem);
                    prizeEntity.PrizeCount = itemDic.FourthType;
                    code = PayCore.Instance.AddBonus(managerId, itemDic.FourthType, EnumChargeSourceType.OpenBoxPrize, ShareUtil.GenerateComb().ToString(), transaction);
                    if (code != MessageCode.Success)
                        return code;
                    break;
                case (int)EnumItemType.MallItem:
                    var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.OpenBoxPrize);
                    code = package.AddItem(prizeEntity.PrizeItem, prizeEntity.PrizeCount);
                    if (code != MessageCode.Success)
                        return code;
                    if (!package.Save(transaction))
                        return MessageCode.FailUpdate;
                    break;
            }
            var contractList = CacheFactory.ItemsdicCache.LotteryTheContractList(3, 82, 85);
            //以10标识合同页
            if (contractList.Contains(prizeEntity.PrizeItem))
                prizeEntity.PrizeType = _contractItemType;

            FriendOpenboxrecordEntity record = new FriendOpenboxrecordEntity()
            {
                ManagerId = managerId,
                FriendId = friendRecord.FriendId,
                PrizeType = prizeEntity.PrizeType,
                PrizeItem = prizeEntity.PrizeItem,
                PrizeCount = prizeEntity.PrizeCount,
                RowTime = DateTime.Now
                
            };
            if (!FriendOpenboxrecordMgr.Insert(record, transaction))
                return MessageCode.FailUpdate;

            friendRecord.DayOpenBoxCount++;
            friendRecord.OpenBoxTime = DateTime.Now;
            if (!FriendManagerMgr.Update(friendRecord, transaction))
                return MessageCode.FailUpdate;

            managerExtra.OpenBoxCount++;
            if (!NbManagerextraMgr.Update(managerExtra,transaction))
                return MessageCode.FailUpdate;

            return MessageCode.Success;
        }


        TeammemberTrainOpenBoxEntity RandomBoxPrize(bool pointIsOver, bool contractIsOver, bool item310101IsOver)
        {
            var limitList = new List<int>();
            if (!pointIsOver)
                limitList.Add(810002);
            if (!item310101IsOver)
                limitList.Add(310101);
            if (!contractIsOver)
                limitList.AddRange(CacheFactory.ItemsdicCache.LotteryTheContractList(3, 82, 85));

            var lotteryEntity = CacheFactory.LotteryCache.Lottery(EnumLotteryType.TrainHelpOpenBox, 1);
            while (limitList.Contains(lotteryEntity.PrizeItemCode))
            {
                lotteryEntity = CacheFactory.LotteryCache.Lottery(EnumLotteryType.TrainHelpOpenBox, 1);
            }
            var itemdic = CacheFactory.ItemsdicCache.GetItem(lotteryEntity.PrizeItemCode);
            var prize = new TeammemberTrainOpenBoxEntity()
            {
                PrizeType = itemdic.ItemType,
                PrizeItem = itemdic.ItemCode,
                PrizeCount = 1
            };
            return prize;
        }

        



        /// <summary>
        /// 开始训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public TeammemberTrainResponse StartTrain(Guid managerId, Guid teammemberId)
        {
            DateTime date = DateTime.Now;
            var trainEntity = GetTrainEntity(teammemberId);
            if (trainEntity != null && trainEntity.TrainState == (int)EnumTrainState.Train)
                return ResponseHelper.Create<TeammemberTrainResponse>((int)MessageCode.TeammemberTraining);
            //var teammember = TeammemberTrainMgr.GetById(teammemberId);
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.Create<TeammemberTrainResponse>((int)MessageCode.AdMissManager);
            var teammembers = GetManagerTrainList(managerId);
            var trainSeat = 0;
            foreach (var item in teammembers)
            {
                var entity = GetTrainEntityNew(item.Idx);
                if (trainEntity == null || trainEntity.TrainState != (int)EnumTrainState.Train)
                    continue;
                trainSeat++;
            }
            if(trainSeat >= manager.TrainSeatMax)
                return ResponseHelper.Create<TeammemberTrainResponse>((int)MessageCode.TeammemberTrainSeatOver);
            //所有球员中找到对应的球员卡
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.PlayreTrain);
            if (package == null)
                return ResponseHelper.Create<TeammemberTrainResponse>(MessageCode.NbParameterError);
            var teammember= package.GetPlayer(teammemberId);
            if (teammember == null)
                return ResponseHelper.Create<TeammemberTrainResponse>((int)MessageCode.TeammemberNotExists);
            var cardProperty = teammember.ItemProperty as PlayerCardProperty;
            if (cardProperty != null)
            {
                cardProperty.IsTrain = true;
                teammember.ItemProperty = cardProperty;
            }
            teammember.IsDeal = false;
            package.Update(teammember);
            var trainTeammember = TeammemberTrainMgr.GetById(teammemberId);
            if (trainTeammember == null)
            {
                
                trainTeammember = new TeammemberTrainEntity();
                trainTeammember.ManagerId = managerId;
                trainTeammember.StartTime = date;
                trainTeammember.SettleTime = date;
                trainTeammember.RowTime = date;

                trainTeammember.TrainState = (int) EnumTrainState.Train;
                trainTeammember.Idx = teammemberId;
                if (cardProperty != null)
                {
                    trainTeammember.Level = cardProperty.Level;
                    trainTeammember.EXP = cardProperty.Exp;
                }
                var player = ItemsdicCache.Instance.GetPlayerByItemCode(teammember.ItemCode);
                if (player != null)
                    trainTeammember.PlayerId = player.Idx;
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.Success;
                    do
                    {
                        if (!TeammemberTrainMgr.Insert(trainTeammember, transactionManager.TransactionObject))
                        {
                            messageCode = MessageCode.NbUpdateFail;
                            break;
                        }
                        if (!package.Save(transactionManager.TransactionObject))
                        {
                            messageCode = MessageCode.NbUpdateFail;
                            break;
                        }
                    } while (false); 
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit(); 
                        _trainDic.TryAdd(teammemberId, trainTeammember);
                        if (!_managerTrainDic.ContainsKey(managerId))
                            _managerTrainDic.TryAdd(managerId, new List<Guid>());
                        _managerTrainDic[managerId].Add(trainTeammember.Idx);
                    }
                    else
                    {
                        transactionManager.Rollback();
                         return ResponseHelper.Create<TeammemberTrainResponse>((int)MessageCode.NbUpdateFail);
                    }
                }
            }
            else
            {
                trainTeammember.StartTime = date;
                trainTeammember.SettleTime = date;
                trainTeammember.RowTime = date;
                trainTeammember.TrainState = (int)EnumTrainState.Train;
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.Success;
                    do
                    {
                        if (!TeammemberTrainMgr.Update(trainTeammember, transactionManager.TransactionObject))
                        {
                            messageCode = MessageCode.NbUpdateFail;
                            break;
                        }
                        if (!package.Save(transactionManager.TransactionObject))
                        {
                            messageCode = MessageCode.NbUpdateFail;
                            break;
                        }
                    } while (false);
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                        _trainDic.TryAdd(teammemberId, trainTeammember);
                        if (!_managerTrainDic.ContainsKey(managerId))
                            _managerTrainDic.TryAdd(managerId, new List<Guid>());
                        _managerTrainDic[managerId].Add(trainTeammember.Idx);
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<TeammemberTrainResponse>((int)MessageCode.NbUpdateFail);
                    }
                }
            }
            TaskHandler.Instance.TeammemberTrain(managerId, 0, false);
            return GetTrainInfo(managerId, teammemberId);
        }

        /// <summary>
        /// 结束训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public MessageCodeResponse EndTrain(Guid managerId, Guid teammemberId)
        {
            DateTime date = DateTime.Now;
            var trainEntity = GetTrainEntity(teammemberId);
            if (trainEntity == null || trainEntity.ManagerId != managerId)
                return ResponseHelper.InvalidParameter<MessageCodeResponse>();
            var teammember = TeammemberTrainMgr.GetById(teammemberId);
            if (teammember == null)
                return ResponseHelper.Create<MessageCodeResponse>((int)MessageCode.TeammemberNotExists);

            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.PlayreTrain);
            if (package == null)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
            var player = package.GetPlayer(teammemberId);
            if (player == null)
                return ResponseHelper.Create<MessageCodeResponse>((int)MessageCode.TeammemberNotExists);
            var cardProperty = player.ItemProperty as PlayerCardProperty;
            if (cardProperty != null)
            {
                cardProperty.IsTrain = false;
                player.ItemProperty = cardProperty;
            }
            package.Update(player);
            teammember.TrainState = (int)EnumTrainState.None;
            teammember.SettleTime = date;
            teammember.RowTime = date;

            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                var messageCode = MessageCode.Success;
                do
                {
                    if (!TeammemberTrainMgr.Update(teammember, transactionManager.TransactionObject))
                    {
                        messageCode = MessageCode.NbUpdateFail;
                        break;
                    }
                    if (!package.Save(transactionManager.TransactionObject))
                    {
                        messageCode = MessageCode.NbUpdateFail;
                        break;
                    }
                } while (false);
                if (messageCode == MessageCode.Success)
                {
                    transactionManager.Commit();
                    RemovetrainDic(teammemberId, managerId);
                }
                else
                {
                    transactionManager.Rollback(); 
                    return ResponseHelper.Create<MessageCodeResponse>((int)MessageCode.NbUpdateFail);
                }
            }
            return ResponseHelper.CreateSuccess<MessageCodeResponse>();
        }

        /// <summary>
        /// 使用经验药水
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="teammemberId"></param>
        /// <param name="expType">经验药水级别1初级 2中级 3高级 4超级</param>
        /// <returns></returns>
        public TeammemberTrainSpeedUpResponse SpeedUpTrain(Guid managerId, Guid teammemberId, int expType)
        {
            var expItemCode = 0;
            switch (expType)
            {
                case 1:
                    expItemCode = 310125;
                    break;
                case 2:
                    expItemCode = 310126;
                    break;
                case 3:
                    expItemCode = 310127;
                    break;
                case 4:
                    expItemCode = 310128;
                    break;
                default:
                    return ResponseHelper.Create<TeammemberTrainSpeedUpResponse>(MessageCode.NbParameterError);
            }
            TeammemberTrainEntity trainEntity = null;
            GetTrainEntity(teammemberId, ref trainEntity);
            if (trainEntity == null || trainEntity.ManagerId != managerId)
                return ResponseHelper.InvalidParameter<TeammemberTrainSpeedUpResponse>();
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.PlayreTrain);
            if (package == null)
                return ResponseHelper.Create<TeammemberTrainSpeedUpResponse>(MessageCode.NbParameterError);
            if (package.GetItemNumber(expItemCode) < 1)
                return ResponseHelper.Create<TeammemberTrainSpeedUpResponse>(MessageCode.SpeedUpItemCodeNumber);
            var messCode = package.Delete(expItemCode, 1);
            if(messCode != MessageCode.Success)
                return ResponseHelper.Create<TeammemberTrainSpeedUpResponse>(messCode);
           
            //经验值
            var expValue = ItemsdicCache.Instance.GetMallEntityWithoutPointByItemCode(expItemCode).EffectValue;
            bool isExpMax = false;
            bool isLevelUp = AddExp(trainEntity, expValue,ref isExpMax);
            if (isExpMax)
                return ResponseHelper.Create<TeammemberTrainSpeedUpResponse>(MessageCode.TeammemberTrainLevelOver);
            var player = package.GetPlayer(trainEntity.Idx);
            if (player != null)
            {
                var cardProperty = player.ItemProperty as PlayerCardProperty;
                if (cardProperty != null)
                {
                    cardProperty.IsTrain = true;
                    cardProperty.Level = trainEntity.Level;
                    player.ItemProperty = cardProperty;
                }
                package.Update(player);
            }
            //TeammemberTrainMgr.Update(trainEntity);
            int returnCode = 0;
            if (!package.Save())
                return ResponseHelper.Create<TeammemberTrainSpeedUpResponse>(MessageCode.NbUpdateFail);
            TeammemberTrainMgr.UpdateData(trainEntity.Idx, trainEntity.Level, trainEntity.EXP, trainEntity.TrainStamina, trainEntity.TrainState,
        trainEntity.StartTime, trainEntity.SettleTime, trainEntity.Status, ShareUtil.GetTableMod(trainEntity.ManagerId), ref returnCode);

            if (isLevelUp)
            {
                MemcachedFactory.TeammembersClient.Delete(managerId);
                KpiHandler.Instance.RebuildKpi(managerId, true);
            }
            var manager = MatchDataHelper.GetManager(managerId, true, true);
            var response = ResponseHelper.CreateSuccess<TeammemberTrainSpeedUpResponse>();
            response.Data=new TeammemberTrainSpeedUp
            {
                TrainEntity = trainEntity,
                Package = ItemCore.Instance.BuildPackageData(package),
                Kpi = manager.Kpi
            };
            return response;
        }

        TeammemberTrainEntity GetTrainEntity(Guid teammemberId, bool isStop = false)
        {
            TeammemberTrainEntity trainEntity = null;
            _trainDic.TryGetValue(teammemberId, out trainEntity);
            if (trainEntity == null)
            {
                trainEntity = TeammemberTrainMgr.GetById(teammemberId);
            }
            return trainEntity;
        }

        TeammemberTrainEntity GetTrainEntityNew(Guid teammemberId)
        {
            if (!_trainDic.ContainsKey(teammemberId))
                return null;
            TeammemberTrainEntity trainEntity = null;
            _trainDic.TryGetValue(teammemberId, out trainEntity);
            return trainEntity;
        }

        void GetTrainEntity(Guid teammemberId,ref TeammemberTrainEntity train, bool isStop = false)
        {
            TeammemberTrainEntity trainEntity = null;
            _trainDic.TryGetValue(teammemberId, out trainEntity);
            if (trainEntity == null)
            {
                trainEntity = TeammemberTrainMgr.GetById(teammemberId);
            }
            train = trainEntity;
        }

        public bool RemovetrainDic(Guid teammemberId,Guid managerId)
        {
            TeammemberTrainEntity trainEntity = null;
            _trainDic.TryRemove(teammemberId, out trainEntity);

            if (_managerTrainDic.ContainsKey(managerId))
            {
                if (_managerTrainDic[managerId].Contains(teammemberId))
                    _managerTrainDic[managerId].Remove(teammemberId);
            }
            return true;
        }

        /// <summary>
        /// 获取球员是否在训练
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public bool GetIsTrain(Guid teammemberId)
        {
            TeammemberTrainEntity trainEntity = null;
            _trainDic.TryGetValue(teammemberId, out trainEntity);
            if (trainEntity == null)
                return false;
            return trainEntity.TrainState == 1;
        }

        public List<TeammemberTrainEntity> GetManagerTrainList(Guid managerId)
        {
            return TeammemberTrainMgr.GetByManagerId(managerId);
        }


        /// <summary>
        /// 结算训练
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isStop">是否是结束训练</param>
        void SettlementTrain(TeammemberTrainEntity entity, bool isStop = false)
        {
            if (entity.TrainState != (int)EnumTrainState.Train)
                return;
            var curTime = DateTime.Now;
            var isLevelUp = CalTrain(curTime, entity);

            try
            {
                int returnCode = -2;
                TeammemberTrainMgr.UpdateData(entity.Idx, entity.Level, entity.EXP, entity.TrainStamina, entity.TrainState,
                    entity.StartTime, entity.SettleTime, entity.Status, ShareUtil.GetTableMod(entity.ManagerId), ref returnCode);
                if (!_trainDic.ContainsKey(entity.Idx))
                    _trainDic.TryAdd(entity.Idx, entity);
                else
                    _trainDic[entity.Idx] = entity;

                //更新背包中卡牌等级
                var package = ItemCore.Instance.GetPackage(entity.ManagerId, EnumTransactionType.PlayreTrain);

                var player = package.GetPlayer(entity.Idx);
                if (player == null)
                {
                    return;
                }
                var playerCardProperty = player.ItemProperty as PlayerCardProperty;
                if (playerCardProperty != null)
                {
                    playerCardProperty.Level = entity.Level;
                    playerCardProperty.Exp = entity.EXP;
                    //if (isStop)
                    //    playerCardProperty.IsTrain = false;
                }

                package.Update(player);
                package.Save();
                if (isLevelUp)
                {
                    MemcachedFactory.TeammembersClient.Delete(entity.ManagerId);
                    KpiHandler.Instance.RebuildKpi(entity.ManagerId, true);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SettlementTrain-Update", "SettlementTrain-Update"+ex);
            }
        }

        bool CalTrain(DateTime curTime, TeammemberTrainEntity entity)
        {
            if (entity == null)
                return false;
            try
            {
                var trainSecond = CalSecond(curTime, entity.SettleTime);
                 entity.SettleTime = curTime;
                var addExp = trainSecond * _trainExpPerSecond;
                var manager = ManagerCore.Instance.GetManager(entity.ManagerId);
                if (manager == null)
                    return false;
                if (manager.Level <= entity.Level)
                    return false;
                //球员训练经验Buff加成
                var buffExp = BuffPoolCore.Instance().GetBuffValue(entity.ManagerId, EnumBuffCode.TrainExpPlusRate, true, false);
                if (buffExp != null)
                {
                    if (buffExp.Percent > 0)
                        addExp = (int)(1 + buffExp.Percent) * addExp;
                }
                entity.EXP += addExp;
                bool isLevelup = false;
                //检查是否升级
                int exp = CacheFactory.TeammemberCache.GetExp(entity.Level);
                if (exp > 0 && entity.EXP >= exp)
                {
                    if (entity.Level < manager.Level)
                    {
                        entity.Level++;
                        entity.EXP -= exp;
                        isLevelup = true;
                        if (entity.Level == manager.Level) //球员等级不能超过经理等级
                        {
                            if (_managerTrainDic.ContainsKey(entity.ManagerId))
                            {
                                if (_managerTrainDic[entity.ManagerId].Exists(r => r == entity.Idx))
                                    _managerTrainDic[entity.ManagerId].Remove(entity.Idx);
                            }
                            entity.EXP = 0;
                            entity.TrainState = (int)EnumTrainState.Train;
                            //entity.IsMaxExp = true;
                        }
                        else
                        {
                            int loopCount = 0;
                            while (loopCount < 10 && CalLevelup(entity, ref exp, manager.Level))
                            {
                                loopCount = loopCount + 1;
                            }
                            if (!_managerTrainDic.ContainsKey(entity.ManagerId))
                                _managerTrainDic.TryAdd(entity.ManagerId, new List<Guid>());
                            if (!_managerTrainDic[entity.ManagerId].Exists(r => r == entity.Idx))
                                _managerTrainDic[entity.ManagerId].Add(entity.Idx);
                        }
                        MatchDataCacheHelper.DeleteTeamembersCache(entity.ManagerId, false);
                    }
                    else
                    {
                        if (_managerTrainDic.ContainsKey(entity.ManagerId))
                        {
                            if (_managerTrainDic[entity.ManagerId].Exists(r => r == entity.Idx))
                                _managerTrainDic[entity.ManagerId].Remove(entity.Idx);
                        }
                        entity.EXP = exp;
                    }
                }
                return isLevelup;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CalTrain", ex);
                return false;
            }
        }

        bool AddExp(TeammemberTrainEntity entity,int addExp,ref bool isExpMax)
        {
            if (entity == null)
                return false;
            var manager = ManagerCore.Instance.GetManager(entity.ManagerId);
            if (entity.Level >= manager.Level)
            {
                isExpMax = true;
                return false;
            }
            entity.EXP += addExp;
            bool isLevelup = false;
            //检查是否升级
            int exp = CacheFactory.TeammemberCache.GetExp(entity.Level);
            if (exp > 0 && entity.EXP >= exp)
            {
                if (entity.Level < manager.Level)
                {
                    entity.Level++;
                    entity.EXP -= exp;
                    isLevelup = true;
                    if (entity.Level == manager.Level) //球员等级不能超过经理等级
                    {
                        if (_managerTrainDic.ContainsKey(entity.ManagerId))
                        {
                            if (_managerTrainDic[entity.ManagerId].Exists(r => r == entity.Idx))
                                _managerTrainDic[entity.ManagerId].Remove(entity.Idx);
                        }
                        entity.EXP = 0;
                        entity.TrainState = (int)EnumTrainState.Train;
                    }
                    else
                    {
                        int loopCount = 0;
                        while (loopCount < 10 && CalLevelup(entity, ref exp, manager.Level))
                        {
                            loopCount = loopCount + 1;
                        }
                        if (!_managerTrainDic.ContainsKey(entity.ManagerId))
                            _managerTrainDic.TryAdd(entity.ManagerId, new List<Guid>());
                        if (!_managerTrainDic[entity.ManagerId].Exists(r => r == entity.Idx))
                            _managerTrainDic[entity.ManagerId].Add(entity.Idx);
                    }
                    entity.LevelupExp = CacheFactory.TeammemberCache.GetExp(entity.Level);
                    
                    MatchDataCacheHelper.DeleteTeamembersCache(entity.ManagerId, true);
                }
                else
                {
                    if (_managerTrainDic.ContainsKey(entity.ManagerId))
                    {
                        if (_managerTrainDic[entity.ManagerId].Exists(r => r == entity.Idx))
                            _managerTrainDic[entity.ManagerId].Remove(entity.Idx);
                    }
                    entity.EXP = exp;
                }
            }
            return isLevelup;
        }

        bool CalLevelup(TeammemberTrainEntity entity, ref int exp, int managerLevel)
        {
            exp = CacheFactory.TeammemberCache.GetExp(entity.Level);
            if (exp > 0 && entity.EXP >= exp)
            {
                if (entity.Level < managerLevel)
                {
                    entity.Level++;
                    entity.EXP -= exp;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        int CalSecond(DateTime curTime, DateTime settleTime)
        {
            return Convert.ToInt32(curTime.Subtract(settleTime).Minutes);
        }

        /// <summary>
        /// 获取是否有球员在训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public bool GetIsHaveTrain(Guid managerId)
        {
            if (!_managerTrainDic.ContainsKey(managerId))
                return false;
            if (_managerTrainDic[managerId].Count <= 0)
                return false;
            return true;
        }

        #endregion
    }
}
