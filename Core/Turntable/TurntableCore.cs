using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Information;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Task;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Friend;
using Games.NBall.Entity.Response.Match;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Turntable
{
    public class TurntableCore
    {
        #region .ctor
        /// <summary>
        /// 活动开始时间
        /// </summary>
        private DateTime startTime;
        /// <summary>
        /// 活动结束时间
        /// </summary>
        private DateTime endTime;

        public TurntableCore(int p)
        {
            startTime = DateTime.Now;
            endTime = DateTime.Now;
            var activityTime = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.TurntableActivityTime);
            if (activityTime.Length > 0)
            {
                var timeList = activityTime.Split('|');
                var start = timeList[0].Split(',');
                var startYear = ConvertHelper.ConvertToInt(start[0]);
                var startMonth = ConvertHelper.ConvertToInt(start[1]);
                var startDay = ConvertHelper.ConvertToInt(start[2]);
                startTime = new DateTime(startYear, startMonth, startDay, 0, 0, 0);

                var end = timeList[1].Split(',');
                var endYear = ConvertHelper.ConvertToInt(end[0]);
                var endMonth = ConvertHelper.ConvertToInt(end[1]);
                var endDay = ConvertHelper.ConvertToInt(end[2]);
                endTime = new DateTime(endYear, endMonth, endDay, 23, 59, 59);
            }
        }

        #endregion

        #region Instance
        public static TurntableCore Instance
        {
            get { return SingletonFactory<TurntableCore>.SInstance; }
        }
        #endregion

        #region 转盘

        /// <summary>
        /// 是否有活动
        /// </summary>
        public bool IsActivity {
            get
            {
                DateTime date = DateTime.Now;
                if (date >= startTime && date <= endTime)
                    return true;
                return false;
            }
        }

        #region 获取信息
        /// <summary>
        /// 获取用户转盘信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public GetTurntableInfoResponse GetManagerInfo(Guid managerId) 
        {
            GetTurntableInfoResponse response = new GetTurntableInfoResponse();
            response.Data = new TurntableInfo ();
            try 
            {
                var turntable = new TurntableFrame(managerId);
                if (turntable == null)
                    return ResponseHelper.Create<GetTurntableInfoResponse>((int)MessageCode.NbParameterError);
                var data = GetTurntableInfo(turntable);
                if(data==null)
                    return ResponseHelper.Create<GetTurntableInfoResponse>((int)MessageCode.NbParameterError);
                response.Data = data;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取经理转盘信息", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        TurntableInfo GetTurntableInfo(TurntableFrame turntable) 
        {
            TurntableInfo data = new TurntableInfo();
            var turntableManager = turntable.TurntableManagerEntity;
            data.DayProduceLuckyCoin = turntableManager.DayProduceLuckyCoin;
            data.FreeOfChargeNumber = turntableManager.GiveLuckyCoin;
            data.LuckyCoin = turntableManager.LuckyCoin;
            data.TurntableType = turntableManager.TurntableType;
            data.NextResetPoint = turntable.GetResetPoint();
            data.ItemList = turntable.GetTurntableList();
            data.StartTimeTick = ShareUtil.GetTimeTick(startTime);
            data.EndTimeTick = ShareUtil.GetTimeTick(endTime);
            return data;
        }

        #endregion


        #region 抽奖

        /// <summary>
        /// 转盘抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TurntableLuckDrawResponse TurntableLuckDraw(Guid managerId)
        {
            TurntableLuckDrawResponse response = new TurntableLuckDrawResponse();
            response.Data = new TurntableLuckDraw();
            try
            {
                var turntable = new TurntableFrame(managerId);
                if (turntable == null)
                    return ResponseHelper.Create<TurntableLuckDrawResponse>((int)MessageCode.NbParameterError);
                var turntableManager = turntable.TurntableManagerEntity;
                if (turntableManager == null)
                    return ResponseHelper.Create<TurntableLuckDrawResponse>((int)MessageCode.NbParameterError);
                if (turntableManager.GiveLuckyCoin <= 0 && turntableManager.LuckyCoin <= 0)
                    return ResponseHelper.Create<TurntableLuckDrawResponse>((int)MessageCode.LuckyCoinInsufficient);
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.Turntable);
                if (package == null)
                    return ResponseHelper.Create<TurntableLuckDrawResponse>((int)MessageCode.NbNoPackage);
                if (package.BlankCount < 1)
                    return ResponseHelper.Create<TurntableLuckDrawResponse>((int)MessageCode.ItemPackageFull);
                //抽奖
                int specialItem = 0;
                var resultPrize = turntable.LuckDraw(ref specialItem);
                if (resultPrize == null)
                    return ResponseHelper.Create<TurntableLuckDrawResponse>((int)MessageCode.NbParameterError);
                int addPoint = 0;
                int addCoin = 0;
                bool isAddPackage = false;
                NbManagerEntity manager = null;
                var record = new TurntableLuckyrecordEntity(0, managerId, false, 1, DateTime.Now, "");
                var messageCode = SendPrize(managerId, resultPrize, specialItem, ref addPoint, ref addCoin, ref isAddPackage, ref package, response, record);
                if (addCoin > 0)
                {
                    manager = ManagerCore.Instance.GetManager(managerId);
                    if (manager == null)
                        return ResponseHelper.Create<TurntableLuckDrawResponse>((int)MessageCode.NbParameterError);
                }
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<TurntableLuckDrawResponse>((int)messageCode);
                messageCode = SaveLuckDraw(managerId, addPoint, addCoin, isAddPackage, package, manager, turntable, record);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<TurntableLuckDrawResponse>((int)messageCode);
                response.Data.WinAlotteryId = resultPrize.TurntableId;
                response.Data.TurntableInfo = GetTurntableInfo(turntable);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("转盘抽奖", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 发奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="prizeEntity"></param>
        /// <param name="specialItem"></param>
        /// <param name="addpoint"></param>
        /// <param name="addcoin"></param>
        /// <param name="isAddPackage"></param>
        /// <param name="package"></param>
        /// <param name="response"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        MessageCode SendPrize(Guid managerId, ConfigTurntableprizeEntity prizeEntity, int specialItem, ref int addpoint, ref int addcoin, ref bool isAddPackage, ref ItemPackageFrame package, TurntableLuckDrawResponse response, TurntableLuckyrecordEntity record) 
        {
            var messCode = MessageCode.Success;
            response.Data.PrizeCount = prizeEntity.ItemCount;
            switch (prizeEntity.PrizeType)
            {
                case (int)EnumTurntablePrizeType.Point:
                    addpoint = prizeEntity.ItemCount;
                    response.Data.PrizeCode = 0;
                    response.Data.PrizeType = (int)EnumTurntablePrizeType.Point;
                    record.LuckDrawString = (int) prizeEntity.PrizeType + "," + prizeEntity.SubType + "," + prizeEntity.ItemCount;
                    break;
                case (int)EnumTurntablePrizeType.Coin:
                    addcoin = prizeEntity.ItemCount;
                    response.Data.PrizeCode = 0;
                    response.Data.PrizeType = (int)EnumTurntablePrizeType.Coin;
                    record.LuckDrawString = (int) prizeEntity.PrizeType + "," + prizeEntity.SubType + "," + prizeEntity.ItemCount;
                    break;
                case (int)EnumTurntablePrizeType.Item:
                    isAddPackage = true;
                    messCode = package.AddItems(prizeEntity.SubType, prizeEntity.ItemCount);
                    if (messCode != MessageCode.Success)
                        return messCode;
                    response.Data.PrizeCode = prizeEntity.SubType;
                    response.Data.PrizeType = (int)EnumTurntablePrizeType.Item;
                    record.LuckDrawString = (int) prizeEntity.PrizeType + "," + prizeEntity.SubType + "," + prizeEntity.ItemCount;
                    break;
                case (int)EnumTurntablePrizeType.Random:
                    isAddPackage = true;
                    var itemCode = CacheFactory.LotteryCache.LotteryByLib(prizeEntity.SubType);
                    var itemCache = CacheFactory.ItemsdicCache.GetItem(itemCode);
                    if (itemCache == null)
                    {
                        return MessageCode.ItemNotExists;
                    }
                    if (itemCache.ItemType == (int)EnumItemType.PlayerCard)
                    {
                        messCode = package.AddPlayerCard(itemCode, 1, false, 1,false);
                    }
                    else
                    {
                        messCode = package.AddItems(itemCode, prizeEntity.ItemCount);
                    }
                    if (messCode != MessageCode.Success)
                        return messCode;
                    response.Data.PrizeCode = itemCode;
                    response.Data.PrizeType = (int)EnumTurntablePrizeType.Item;
                    record.LuckDrawString = (int)prizeEntity.PrizeType + "," + itemCode + "," + prizeEntity.ItemCount;
                    break;
                case (int)EnumTurntablePrizeType.Turntable:
                    record.LuckDrawString = (int)prizeEntity.PrizeType + "," + prizeEntity.SubType + "," + prizeEntity.ItemCount;
                    break;
                case (int)EnumTurntablePrizeType.Special:
                    isAddPackage = true;
                    if (specialItem == 0)
                        return MessageCode.NbParameterError;
                    messCode = package.AddItems(specialItem, prizeEntity.ItemCount);
                    if (messCode != MessageCode.Success)
                        return messCode;
                    response.Data.PrizeCode = specialItem;
                    response.Data.PrizeType = (int)EnumTurntablePrizeType.Item;
                    record.LuckDrawString = (int)prizeEntity.PrizeType + "," + specialItem + "," + prizeEntity.ItemCount;
                    break;
                default:
                    break;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 保存抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="addPoint"></param>
        /// <param name="addCoin"></param>
        /// <param name="isAddPackage"></param>
        /// <param name="package"></param>
        /// <param name="manager"></param>
        /// <param name="turntable"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        MessageCode SaveLuckDraw(Guid managerId, int addPoint, int addCoin, bool isAddPackage, ItemPackageFrame package, NbManagerEntity manager, TurntableFrame turntable, TurntableLuckyrecordEntity record) 
        {
            MessageCode messageCode = MessageCode.Success;
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Main)))
            {
                transactionManager.BeginTransaction();
                messageCode = SaveLuckDraw(managerId, addPoint, addCoin, isAddPackage, package, manager, turntable, record, transactionManager.TransactionObject);
                if (messageCode == MessageCode.Success)
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

        MessageCode SaveLuckDraw(Guid managerId, int addPoint, int addCoin, bool isAddPackage, ItemPackageFrame package, NbManagerEntity manager, TurntableFrame turntable, TurntableLuckyrecordEntity record, DbTransaction trans) 
        {
            var messageCode = MessageCode.Success;
            if (addPoint > 0) 
            {
                messageCode = PayCore.Instance.AddBonus(managerId, addPoint, EnumChargeSourceType.Turntable, ShareUtil.GenerateComb().ToString(), trans);
                if (messageCode != MessageCode.Success)
                    return messageCode;
            }
            if (addCoin > 0) 
            {
                messageCode = ManagerCore.Instance.AddCoin(manager, addCoin, EnumCoinChargeSourceType.Turntable, ShareUtil.GenerateComb().ToString(), trans);
                if (messageCode != MessageCode.Success)
                    return messageCode;
            }
            if (!turntable.Save(trans))
                return MessageCode.NbUpdateFail;
            if (isAddPackage) 
            {
                if (!package.Save(trans))
                    return MessageCode.NbUpdateFail;
                package.Shadow.Save();
            }
            TurntableLuckyrecordMgr.Insert(record,trans);
            return MessageCode.Success;
        }

        #endregion

        #region 重置转盘

        /// <summary>
        /// 重置转盘
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public TurntableResetResponse ResetTurntable(Guid managerId) 
        {
            TurntableResetResponse response = new TurntableResetResponse();
            response.Data = new TurntableReset();
            try
            {
                var turntable = new TurntableFrame(managerId);
                if (turntable == null)
                    return ResponseHelper.Create<TurntableResetResponse>((int)MessageCode.NbParameterError);
                if (!turntable.TurnTableDic.ItemList.Exists(r => !r.IsEffective)) //不需要重置
                    return ResponseHelper.Create<TurntableResetResponse>((int)MessageCode.TurntableNotReset);
                int consumePoint = turntable.GetResetPoint(turntable.TurntableManagerEntity.TurntableType);
                if (consumePoint == 0)
                    return ResponseHelper.Create<TurntableResetResponse>((int)MessageCode.NbParameterError);
                var point = PayCore.Instance.GetPoint(managerId);
                if (point < consumePoint)//钻石不足
                    return ResponseHelper.Create<TurntableResetResponse>((int)MessageCode.NbPointShortage);
                //重置
                turntable.Reset(turntable.TurntableManagerEntity.TurntableType);
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Main)))
                {
                    transactionManager.BeginTransaction();
                    MessageCode messageCode = MessageCode.NbUpdateFail;
                    do 
                    {
                        messageCode = PayCore.Instance.GambleConsume(managerId, consumePoint, ShareUtil.GenerateComb(), EnumConsumeSourceType.Turntable, transactionManager.TransactionObject);
                        if (messageCode != MessageCode.Success)
                            break;
                        if (!turntable.Save(transactionManager.TransactionObject))
                        {
                            messageCode = MessageCode.NbUpdateFail;
                            break;
                        }
                        messageCode = MessageCode.Success;
                    } while (false);
                    if (messageCode == MessageCode.Success)
                    {
                        transactionManager.Commit();
                    }
                    else
                    {
                        transactionManager.Rollback();
                        return ResponseHelper.Create<TurntableResetResponse>((int)messageCode);
                    }
                }
                response.Data.TurntableInfo = GetTurntableInfo(turntable);
                response.Data.Point = point - consumePoint;
            }
            catch (Exception ex) 
            {
                SystemlogMgr.Error("重置转盘", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        #endregion

        #endregion

    }
}
