using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Coach;
using Games.NBall.Core.FriendShip;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Online;
using Games.NBall.Core.Turntable;
using Games.NBall.Entity;
using Games.NBall.Entity.Config.Custom;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Item;
using Games.NBall.Entity.Response.Scouting;
using Games.NBall.WebClient.Util;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Mall
{
    public class MallCore
    {
        private readonly int _lockSkillPoint;

        public Dictionary<int, int> _chargeDoubleConfig;
        public Dictionary<int, int> _chargeReturnConfig;
        /// <summary>
        /// 充值送幸运币配置
        /// </summary>
        public Dictionary<int, int> _turntableLuckyCoinConfig;
        /// <summary>
        /// 充值送游戏币币配置
        /// </summary>
        public Dictionary<int, int> _penaltyKickGameCurrencyConfig;

        public ConcurrentDictionary<Guid, bool> _buyPointPoss;
        public DateTime LimitedPackageStartTime;
        public DateTime LimitedPackageEndTime;
        public DateTime SevenPackageStartTime;
        public DateTime SevenPackageEndTime;
        public int LimitedPackageCode;
        public int SevenPackageCode;//节日礼包mallcode

        public string chagerKey = "";


        /// <summary>
        /// 金牌球探每天掉落可出售数量
        /// </summary>
        private int _dropOutNumber;

        #region .ctor

        private readonly int _tx_YellowvipDiscounts;
        public MallCore(int b)
        {
            _buyPointPoss = new ConcurrentDictionary<Guid, bool>();
            _mallBuyCountLimit = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.MallBuyCountLimit);
            _mallBuyPackageCountLimit =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.MallBuyPackageCountLimit);
            _mallExtraAddStaminaCount =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.MallExtraAddStaminaCount);
            _mallExtraExpandPackageSize =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.MallExtraExpandPackageSize);
            _scoutingTenPointMultiple = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ScoutingTenPointMultiple);

            var allCharegDouble = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.ChargeDoubleConfig);

            _dropOutNumber = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ScoutingDropOutNumber, 2);
            _chargeDoubleConfig = new Dictionary<int, int>();
            if (allCharegDouble.Length > 0)
            {
                var chargeDouble = allCharegDouble.Split('|');
                foreach (var s in chargeDouble)
                {
                    var c = s.Split(',');
                    if (c.Length > 1)
                    {

                        if (!_chargeDoubleConfig.ContainsKey(ConvertHelper.ConvertToInt(c[0])))
                            _chargeDoubleConfig.Add(ConvertHelper.ConvertToInt(c[0]), ConvertHelper.ConvertToInt(c[1]));
                    }
                }
            }
            var allChargeReturn = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.ChargeReturnConfig);
            _chargeReturnConfig = new Dictionary<int, int>();
            if (allChargeReturn.Length > 0)
            {
                var chargeReturn = allChargeReturn.Split('|');
                foreach (var s in chargeReturn)
                {
                    var c = s.Split(',');
                    if (c.Length > 1)
                    {

                        if (!_chargeReturnConfig.ContainsKey(ConvertHelper.ConvertToInt(c[0])))
                            _chargeReturnConfig.Add(ConvertHelper.ConvertToInt(c[0]), ConvertHelper.ConvertToInt(c[1]));
                    }
                }
            }
            var allturntableLuckyCoinConfig = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.TurntableLuckyCoinConfig);
            _turntableLuckyCoinConfig = new Dictionary<int, int>();
            if (allturntableLuckyCoinConfig.Length > 0)
            {
                var configList = allturntableLuckyCoinConfig.Split('|');
                foreach (var s in configList)
                {
                    var item = s.Split(',');
                    if (item.Length > 1)
                    {
                        int mallcode = ConvertHelper.ConvertToInt(item[0]);
                        int luckyCoin = ConvertHelper.ConvertToInt(item[1]);
                        if (mallcode == 0 || luckyCoin == 0)
                            continue;
                        if(!_turntableLuckyCoinConfig.ContainsKey(mallcode))
                            _turntableLuckyCoinConfig.Add(mallcode, luckyCoin);
                    }
                }
            }
            var allpenaltyKickGameCurrencyConfig = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.PenaltyKickGameCurrencyConfig);
            _penaltyKickGameCurrencyConfig = new Dictionary<int, int>();
            if (allpenaltyKickGameCurrencyConfig.Length > 0)
            {
                var configList = allpenaltyKickGameCurrencyConfig.Split('|');
                foreach (var s in configList)
                {
                    var item = s.Split(',');
                    if (item.Length > 1)
                    {
                        int mallcode = ConvertHelper.ConvertToInt(item[0]);
                        int gameCurrency = ConvertHelper.ConvertToInt(item[1]);
                        if (mallcode == 0 || gameCurrency == 0)
                            continue;
                        if (!_penaltyKickGameCurrencyConfig.ContainsKey(mallcode))
                            _penaltyKickGameCurrencyConfig.Add(mallcode, gameCurrency);
                    }
                }
            }

            //限时礼包时间戳
            var str = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.LimitedPackage);
            try
            {
                var strs = str.Split('|');
                var startTime = strs[0];
                var endTime = strs[1];
                var mallCode = strs[2];
                DateTime sTime = DateTime.Parse(startTime);
                DateTime eTime = DateTime.Parse(endTime);
                LimitedPackageStartTime = sTime;
                LimitedPackageEndTime = eTime;
                LimitedPackageCode = ConvertHelper.ConvertToInt(mallCode);
            }
            catch (Exception exception)
            {


            }
            //节日礼包时间戳
            var str2 = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.SevenPackage);
            try
            {
                var strs2 = str2.Split('|');
                var startTime2 = strs2[0];
                var endTime2 = strs2[1];
                var mallCode = strs2[2];
              
                DateTime sTime2 = DateTime.Parse(startTime2);
                DateTime eTime2 = DateTime.Parse(endTime2);
                SevenPackageStartTime= sTime2;
                SevenPackageEndTime = eTime2;
                SevenPackageCode = ConvertHelper.ConvertToInt(mallCode);
            }
            catch (Exception exception)
            {


            }

            if (ShareUtil.IsQunHei)
            {
                var platforms = AllUaplatformMgr.GetByFactory(ShareUtil.PlatformCode); ;
                if (platforms == null || platforms.Count <= 0)
                {
                    SystemlogMgr.Error("UaFactory create", "UaFactory config is null,factory:h5_qunhei");
                }
                else
                {
                    var qunheiPlatform = platforms.Find(d => d.PlatformCode == ShareUtil.PlatformCode);
                    if (qunheiPlatform != null)
                        chagerKey = qunheiPlatform.ChargeKey;
                }

            }

        }
        #endregion

        #region Facade

        public static MallCore Instance
        {
            get { return SingletonFactory<MallCore>.SInstance; }
        }

        #region GetShowList
        /// <summary>
        /// 获取显示列表
        /// </summary>
        /// <returns></returns>
        public MallShowlistResponse GetShowList(Guid managerId)
        {
            var response = ResponseHelper.CreateSuccess<MallShowlistResponse>();
            response.Data = new MallShowlistEntity();
            response.Data.ShowList = CacheFactory.MallCache.GetShowList();
            response.Data.Point = PayCore.Instance.GetPoint(managerId);
            return response;
        }
        #endregion
       
        #region BuyExtraItem
        /// <summary>
        /// 购买虚拟道具前提示用户
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="extraType"></param>
        public MallCheckExtraResponse CheckExtraItem(Guid managerId, int extraType)
        {
            var extraEntity = MallExtrarecordMgr.GetExtra(managerId, extraType);
            if (extraEntity == null)
                return ResponseHelper.InvalidParameter<MallCheckExtraResponse>();
            DateTime curTime = DateTime.Now;
            int mallCode = 0;
            NbManagerextraEntity managerextraEntity = null;
            int maxCount = 0;
            int usedCount = 0;
            var code = ExtraCheck(managerId,true, extraEntity, curTime, out mallCode, out managerextraEntity,ref maxCount,ref usedCount);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<MallCheckExtraResponse>(code);
            }
            if (mallCode <= 0)
                return ResponseHelper.InvalidParameter<MallCheckExtraResponse>();

            var mallEntity = CacheFactory.MallCache.GetMallEntity(mallCode, curTime);
            int payCurrency = mallEntity.CurrencyCount;
            int currencyType = mallEntity.CurrencyType;
            int count = 1;
            PayUserEntity payUserEntity = null;
            NbManagerEntity manager = null;
            code = MallCheck(managerId, count, currencyType, payCurrency, out payUserEntity, out manager, false, true);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<MallCheckExtraResponse>(code);
            int effectCount = 1;
            int messageCode = 0;
            switch (extraEntity.ExtraType)
            {
                case (int)EnumMallExtraType.AddStamina:
                    effectCount = _mallExtraAddStaminaCount;
                    messageCode = (int)MessageCode.MallBuyStaminaCheck;
                    break;
                case (int)EnumMallExtraType.AddTrainSeat:
                    effectCount = 1;
                    messageCode = (int)MessageCode.MallBuyTrainSeatCheck;
                    break;
                case (int)EnumMallExtraType.ExpandPackage:
                    effectCount = _mallExtraExpandPackageSize;
                    messageCode = (int)MessageCode.MallBuyPackageCheck;
                    break;
                case (int)EnumMallExtraType.ResetElite:
                    effectCount = 1;
                    messageCode = (int)MessageCode.MallResetEliteCheck;
                    break;
                case (int)EnumMallExtraType.AddSubstitute:
                    effectCount = 1;
                    messageCode = (int)MessageCode.MallBuySubstitute;
                    break;
                case (int)EnumMallExtraType.AddPkCount:
                    effectCount = 1;
                    messageCode = (int)MessageCode.MallAddPkCount;
                    break;
            }
            var response = ResponseHelper.CreateSuccess<MallCheckExtraResponse>();
            response.Data = new MallCheckExtraEntity();
            response.Data.MessageCode = messageCode;
            if(currencyType==(int)EnumCurrencyType.Point)
                response.Data.CostPoint = payCurrency;
            else if (currencyType == (int)EnumCurrencyType.Coin)
                response.Data.CostCoin = payCurrency;
            response.Data.MaxNumber = maxCount +1;
            response.Data.HaveNumber = usedCount;
            response.Data.BuyCount = effectCount;
            return response;
        }

        /// <summary>
        /// 购买道具--扩展，主要用于虚拟道具
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="extraType">1,扩展背包;2,重置精英巡回赛;3,加速训练;4,增加训练位;5,增加替补席;6,增加体力;</param>
        /// <returns></returns>
        public MallBuyItemResponse BuyExtraItem(Guid managerId, int extraType)
        {
            var extraEntity = MallExtrarecordMgr.GetExtra(managerId, extraType);
            if (extraEntity == null)
                return ResponseHelper.InvalidParameter<MallBuyItemResponse>();
            DateTime curTime = DateTime.Now;
            int mallCode = 0;
            NbManagerextraEntity managerextraEntity = null;

            int maxCount = 0;
            int usedCount = 0;
            var code = ExtraCheck(managerId, false, extraEntity, curTime, out mallCode, out managerextraEntity, ref maxCount, ref usedCount,true);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<MallBuyItemResponse>(code);
            }
            if (mallCode <= 0)
                return ResponseHelper.InvalidParameter<MallBuyItemResponse>();

            var mallEntity = CacheFactory.MallCache.GetMallEntity(mallCode, curTime);
            PayUserEntity payUserEntity = null;
            NbManagerEntity manager = null;
            int payCurrency = mallEntity.CurrencyCount;
            int rawCurrency = mallEntity.RawCurrencyCount;
            int currencyType = mallEntity.CurrencyType;
            int count = 1;

            code = MallCheck(managerId, count, currencyType, payCurrency, out payUserEntity, out manager);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<MallBuyItemResponse>(code);

            var saleRecord = BuildSalerecord(mallCode, managerId, currencyType, payCurrency, rawCurrency, count, curTime);
            int extraResultValue = 0;
            var messageCode = SaveBuyExtraItem(saleRecord, payUserEntity, manager, currencyType, payCurrency, null, extraEntity, managerextraEntity, ref extraResultValue);
            if (messageCode == ShareUtil.SuccessCode)
            {
                var response = ResponseHelper.CreateSuccess<MallBuyItemResponse>();
                response.Data = new MallBuyItemEntity();
                response.Data.CurrencyType = currencyType;
                response.Data.CurCurrency = CalCurCurrency(currencyType, payUserEntity, payCurrency, manager, EnumCoinConsumeSourceType.MallBuy, saleRecord);

                response.Data.ExtraType = extraType;
                response.Data.ExtraResultValue = extraResultValue;
                return response;
            }
            else
            {
                return ResponseHelper.Create<MallBuyItemResponse>(messageCode);
            }
        }
        #endregion

        #region BuyItem
        /// <summary>
        /// 购买道具
        /// 如果背包已满，提示背包满了，不让买
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public MallBuyItemResponse BuyItem(Guid managerId, int mallCode, int count)
        {
            return BuyItem(managerId, mallCode, count, DateTime.Now);
        }

        /// <summary>
        /// 购买道具
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <param name="count"></param>
        /// <param name="curTime"></param>
        /// <returns></returns>
        public MallBuyItemResponse BuyItem(Guid managerId, int mallCode, int count, DateTime curTime)
        {
            var mallEntity = CacheFactory.MallCache.GetMallEntity(mallCode, curTime);
            if (mallEntity == null)
                return ResponseHelper.InvalidParameter<MallBuyItemResponse>();
            PayUserEntity payUserEntity = null;
            ItemPackageFrame packageFrame = null;
            NbManagerEntity manager = null;
            MallExtrarecordEntity mallExtrarecord = null;
            int payCurrency = mallEntity.CurrencyCount * count;
            int rawCurrency = mallEntity.RawCurrencyCount * count;
            int currencyType = mallEntity.CurrencyType;
            var code = MallCheck(managerId, count, currencyType, payCurrency, out payUserEntity, out manager);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<MallBuyItemResponse>(code);

            MailBuilder mail = null;
            if (mallEntity.MallType != (int) EnumMallType.XN)
            {
                var dicitem = CacheFactory.ItemsdicCache.GetItemByType(mallEntity.MallCode, EnumItemType.MallItem);
                if (dicitem == null)
                {
                    return ResponseHelper.Create<MallBuyItemResponse>(MessageCode.ItemNotExists);
                }

                packageFrame = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.MallBuyItem);
                var result = packageFrame.AddMallItem(dicitem.ItemCode, count,false,false);
                if (result != MessageCode.Success)
                {
                    return ResponseHelper.Create<MallBuyItemResponse>(result);
                }
                ActivityExThread.Instance.MallBuyOneGetOne(managerId, dicitem.ItemCode, count, ref mail);
            }
            var saleRecord = BuildSalerecord(mallEntity, managerId, count, curTime);

            var messageCode = SaveBuyItem(saleRecord, payUserEntity, manager, currencyType, payCurrency, packageFrame, mallExtrarecord);
            if (messageCode == ShareUtil.SuccessCode)
            {
                if (mail != null)
                    mail.Save();
                var response = ResponseHelper.CreateSuccess<MallBuyItemResponse>();
                response.Data = new MallBuyItemEntity();
                response.Data.CurrencyType = currencyType;
                
                response.Data.CurCurrency = CalCurCurrency(currencyType, payUserEntity, payCurrency, manager, EnumCoinConsumeSourceType.MallBuy, saleRecord);

                return response;
            }
            else
            {
                return ResponseHelper.Create<MallBuyItemResponse>(messageCode);
            }
        }

        /// <summary>
        /// 购买绑定道具
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public MallBuyItemResponse BuyBindItem(Guid managerId, int mallCode, int count)
        {
            return BuyBindItem(managerId, mallCode, count, DateTime.Now);
        }

        public MallBuyItemResponse BuyBindItem(Guid managerId, int mallCode, int count, DateTime curTime)
        {
            var mallEntity = CacheFactory.MallCache.GetMallEntity(mallCode, curTime);
            if (mallEntity == null)
                return ResponseHelper.InvalidParameter<MallBuyItemResponse>();
            PayUserEntity payUserEntity = null;
            ItemPackageFrame packageFrame = null;
            NbManagerEntity manager = null;
            MallExtrarecordEntity mallExtrarecord = null;
            int payCurrency = mallEntity.CurrencyCount * count * 2;
            int currencyType = (int)EnumCurrencyType.BindPoint;
           
            var code = MallCheck(managerId, count, currencyType, payCurrency, out payUserEntity, out manager);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<MallBuyItemResponse>(code);

            if (mallEntity.MallType != (int) EnumMallType.XN)
            {
                var dicitem = CacheFactory.ItemsdicCache.GetItemByType(mallEntity.MallCode, EnumItemType.MallItem);
                if (dicitem == null)
                {
                    return ResponseHelper.Create<MallBuyItemResponse>(MessageCode.ItemNotExists);
                }

                packageFrame = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.MallBuyBindItem);
                var result = packageFrame.AddMallItem(dicitem.ItemCode, count, true,false);
                if (result != MessageCode.Success)
                {
                    return ResponseHelper.Create<MallBuyItemResponse>(result);
                }
            }
            var saleRecord = BuildSalerecord(mallEntity, managerId, count, curTime);

            var messageCode = SaveBuyItem(saleRecord, payUserEntity, manager, currencyType, payCurrency, packageFrame, mallExtrarecord);
            if (messageCode == ShareUtil.SuccessCode)
            {
                var response = ResponseHelper.CreateSuccess<MallBuyItemResponse>();
                response.Data = new MallBuyItemEntity();
                response.Data.CurrencyType = currencyType;

                response.Data.CurCurrency = CalCurCurrency(currencyType, payUserEntity, payCurrency, manager, EnumCoinConsumeSourceType.BindMallBuy, saleRecord);
                
                return response;
            }
            else
            {
                return ResponseHelper.Create<MallBuyItemResponse>(messageCode);
            }
        }
        #endregion

        #region BuyPoint

        /// <summary>
        /// 获取购买点卷信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MallGetBuyPointResponse GetBuyPointInfo(Guid managerId)
        {
            MallGetBuyPointResponse response = new MallGetBuyPointResponse();
            response.Data = new MallGetBuyPoint();
            try
            {
                DateTime date = DateTime.Now;
                List<MallBuyPointInfo> resultList = new List<MallBuyPointInfo>();
                var managerList = ManagerChargenumberMgr.GetManagerIdList(managerId);
                var configList = CacheFactory.MallCache.GetMallListByMallType((int) EnumMallType.QP);
                foreach (var item in configList)
                {
                    if (item.MallType == (int) EnumMallType.QP)
                    {
                        MallBuyPointInfo info = new MallBuyPointInfo();
                        info.MallCode = item.MallCode;
                        info.IsDouble = true;
                        if (managerList.Exists(r => r.MallCode == item.MallCode))
                            info.IsDouble = false;
                        if (item.EffectType == (int) EnumMallEffectType.MonthCard)
                            info.IsDouble = false;
                        resultList.Add(info);
                    }
                }
                configList = CacheFactory.MallCache.GetMallListByMallType((int) EnumMallType.Mystery);
                List<MallBuyGiftBagInfo> giftbagList = new List<MallBuyGiftBagInfo>();
                foreach (var item in configList)
                {
                    if (item.MallType == (int) EnumMallType.Mystery)
                    {
                        MallBuyGiftBagInfo entity = new MallBuyGiftBagInfo();
                        entity.MallCode = item.MallCode;
                        entity.IsHaveBuy = true;
                        entity.NextBuyTick = ShareUtil.GetTimeTick(DateTime.Now);
                        if (managerList.Exists(r => r.MallCode == item.MallCode))
                            entity.IsHaveBuy = false;
                        if (item.EffectType == (int) EnumMallEffectType.GiftBag)
                        {
                            var managerInfo = managerList.Find(r => r.MallCode == item.MallCode);
                            DateTime startBuyTime = ShareUtil.GetThisWeekDayDate(DayOfWeek.Friday);
                            DateTime endBuyTime = ShareUtil.GetThisWeekDayDate(DayOfWeek.Sunday);
                            if (managerInfo == null)
                            {
                                managerInfo = new ManagerChargenumberEntity();
                                managerInfo.UpdateTiem = startBuyTime.AddDays(-1);
                            }
                            if (managerInfo.UpdateTiem >= startBuyTime) //购买过了
                            {
                                entity.IsHaveBuy = false;
                                entity.NextBuyTick = ShareUtil.GetTimeTick(startBuyTime.AddDays(7));
                            }
                            else if (date < startBuyTime) //时间未到
                            {
                                entity.IsHaveBuy = false;
                                entity.NextBuyTick = ShareUtil.GetTimeTick(startBuyTime);
                            }
                            else
                            {
                                entity.IsHaveBuy = true;
                            }
                        }
                        else if (item.EffectType == (int) EnumMallEffectType.WeeklyGiftBag)
                        {
                            var managerInfo = managerList.Find(r => r.MallCode == item.MallCode);
                            //获取星期一的时间
                            DateTime buyTime = ShareUtil.GetThisWeekDayDate(DayOfWeek.Monday);
                            if (managerInfo == null)
                            {
                                managerInfo = new ManagerChargenumberEntity();
                                managerInfo.UpdateTiem = buyTime.AddDays(-1);
                            }
                            if (managerInfo.UpdateTiem >= buyTime) //购买过了
                            {
                                entity.IsHaveBuy = false;
                                entity.NextBuyTick = ShareUtil.GetTimeTick(buyTime.AddDays(7));
                            }
                            else
                            {
                                entity.IsHaveBuy = true;
                            }
                        }
                        giftbagList.Add(entity);
                    }
                }
                response.Data.SevenMallCode = SevenPackageCode;
                response.Data.LimitMallCode = LimitedPackageCode;
                response.Data.StartDateTime = ShareUtil.GetTimeTick(LimitedPackageStartTime);
                response.Data.EndDateTime = ShareUtil.GetTimeTick(LimitedPackageEndTime);
                response.Data.SevenStartDateTime = ShareUtil.GetTimeTick(SevenPackageStartTime);
                response.Data.SevenEndDateTime = ShareUtil.GetTimeTick(SevenPackageEndTime);
                response.Data.List = resultList;
                response.Data.GuftBagList = giftbagList;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取购买钻石信息", ex);
                return ResponseHelper.Create<MallGetBuyPointResponse>(MessageCode.NbParameterError);
            }
            return response;
        }

        /// <summary>
        /// 购买钻石下单
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <returns></returns>
        public MallBuyPointResponse BuyPoint(Guid managerId, int mallCode)
        {
            var response = new MallBuyPointResponse();
            response.Data = new MallBuyPoint();
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
            {
                response.Code = (int)MessageCode.LoginNoRegister;
                return response;
            }
            var opener = CSDKinterface.Instance.GetStartgameEntity(manager.Account);
            if (opener == null && (ShareUtil.IsH5A8 || ShareUtil.IsTx))
            {
                response.Code = (int)MessageCode.LoginNoUser;
                return response;
            }
            if (mallCode <= 0)
            {
                response.Code = (int)MessageCode.ItemNotExists;
                return response;
            }
            var mallEntity = CacheFactory.MallCache.GetMallEntityWithoutPoint(mallCode);
            if (mallEntity == null || (mallEntity.MallType != (int)EnumMallType.QP && mallEntity.MallType != (int)EnumMallType.Mystery))
            {
                response.Code =(int ) MessageCode.PayCreateItemFail;
                return response;
            }
            ManagerChargenumberEntity buyRecord = null;
            if (mallEntity.EffectType == (int) EnumMallEffectType.GiftBag)
            {
                buyRecord = ManagerChargenumberMgr.GetByManagerId(manager.Idx,mallCode);
                if (!IsHaveBuyWeekGiftBag(buyRecord, DateTime.Now))
                    return ResponseHelper.Create<MallBuyPointResponse > ((int) MessageCode.BuyPointFail);
            }
            else if (mallEntity.EffectType == (int)EnumMallEffectType.WeeklyGiftBag)
            {
                buyRecord = ManagerChargenumberMgr.GetByManagerId(manager.Idx, mallCode);
                if (!IsHaveBuyWeekGiftBag(buyRecord))
                    return ResponseHelper.Create<MallBuyPointResponse>((int)MessageCode.BuyPointFail);
            }
            else if (mallEntity.EffectType == (int) EnumMallEffectType.FirstCharge)
            {
                var list = ManagerChargenumberMgr.GetManagerIdList(manager.Idx);
                if (list.Count > 0)
                    return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.FirstChargeNot);
            }
            else if (mallEntity.MallType == (int) EnumMallType.Mystery)
            {
                buyRecord = ManagerChargenumberMgr.GetByManagerId(manager.Idx, mallCode);
                if (buyRecord != null)
                    return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.MallBuyCountLimit);
            }
            var orderiid = RandomHelper.GetInt32(10000,99999);
            string date=DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var gameOrderId =""+(int)EnumMallExtraType.A8csdkId+ date + orderiid;
            
            var cash = mallEntity.EffectValue;
            PayChargehistoryEntity payEntity = new PayChargehistoryEntity(gameOrderId, manager.Account,
                (int) EnumChargeSourceType.System, "",mallEntity.CurrencyCount, 0, cash, false
                , DateTime.Now, DateTime.Now, mallCode, 0);
            if (!PayChargehistoryMgr.Insert(payEntity))
            {
                return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.NbUpdateFail);
            }
            if (mallEntity.EffectType == (int) EnumMallEffectType.FirstCharge)
            {
                switch (mallCode)
                {
                    case 70108:
                        mallCode = 70101;
                        break;
                    case 70109:
                        mallCode = 70102;
                        break;
                    case 70110:
                        mallCode = 70103;
                        break;
                    case 70111:
                        mallCode = 70105;
                        break;
                }
            }
            var user = NbUserMgr.GetById(manager.Account);
            var ip = "";
            if (user == null)
                ip = "0";
            ip = user.LastLoginIp;
            response.Data.ItemName = mallEntity.Name;
            response.Data.DateEntity = manager;

            //ss=sessionId+","+state
            //var ss = OnlineCore.GetSessionId(manager.Account);
            if (ShareUtil.IsH5A8||ShareUtil.IsTx)
            {
            var channelAlias = opener.Pf;
            var ext = opener.State;
            var sessionId = opener.SessionId;
            response.Data.StrCommon = opener.Common;
            response.Data.Ext = ext;
            response.Data.ChannelAlias = channelAlias;
            response.Data.SessionId = sessionId;

            }else if (ShareUtil.IsEgret)
            {
                response.Data.Ext = gameOrderId;
            }

            response.Data.ServerId = ShareUtil.PlatformZoneName;
            response.Data.itemCount = 1;
            response.Data.Cash = cash * 100;//a8价格单位是  分
            if (ShareUtil.IsQunHei)
            {
                response.Data.Cash = cash;
                response.Data.Ext = mallCode.ToString()+"_"+ShareUtil.PlatformZoneName+"_"+gameOrderId;
                var sign = cash + manager.Account + response.Data.Ext + chagerKey;
                response.Data.StrCommon = ShareUtil.GetMD5(sign).ToLower();
            }
            response.Data.IP = ip;
            response.Data.ItemCode = mallCode;
            response.Data.OrderId = payEntity.Idx;
            return response;
        }

        /// <summary>
        /// 腾讯购买参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <returns></returns>
        public MallTxBuyPointResponse TxBuyPointPara(Guid managerId, int mallCode)
        {
            var response = new MallTxBuyPointResponse();
            response.Data = new MallTxBuyPoint();
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                {
                    response.Code = (int) MessageCode.LoginNoRegister;
                    return response;
                }
                var opener = TxLogininfoMgr.GetById(manager.Account);
                if (opener == null)
                {
                    response.Code = (int) MessageCode.LoginNoUser;
                    return response;
                }
                if (mallCode <= 0)
                {
                    response.Code = (int) MessageCode.ItemNotExists;
                    return response;
                }
                var mallEntity = CacheFactory.MallCache.GetMallEntityWithoutPoint(mallCode);
                if (mallEntity == null ||
                    (mallEntity.MallType != (int) EnumMallType.QP && mallEntity.MallType != (int) EnumMallType.Mystery))
                {
                    response.Code = (int) MessageCode.PayCreateItemFail;
                    return response;
                }
                ManagerChargenumberEntity buyRecord = null;
                if (mallEntity.EffectType == (int) EnumMallEffectType.GiftBag)
                {
                    buyRecord = ManagerChargenumberMgr.GetByManagerId(manager.Idx, mallCode);
                    if (!IsHaveBuyWeekGiftBag(buyRecord, DateTime.Now))
                        return ResponseHelper.Create<MallTxBuyPointResponse>((int) MessageCode.BuyPointFail);
                }
                else if (mallEntity.EffectType == (int) EnumMallEffectType.WeeklyGiftBag)
                {
                    buyRecord = ManagerChargenumberMgr.GetByManagerId(manager.Idx, mallCode);
                    if (!IsHaveBuyWeekGiftBag(buyRecord))
                        return ResponseHelper.Create<MallTxBuyPointResponse>((int) MessageCode.BuyPointFail);
                }
                else if (mallEntity.EffectType == (int) EnumMallEffectType.FirstCharge)
                {
                    var list = ManagerChargenumberMgr.GetManagerIdList(manager.Idx);
                    if (list.Count > 0)
                        return ResponseHelper.Create<MallTxBuyPointResponse>((int) MessageCode.FirstChargeNot);
                }
                else if (mallEntity.MallType == (int) EnumMallType.Mystery)
                {
                    buyRecord = ManagerChargenumberMgr.GetByManagerId(manager.Idx, mallCode);
                    if (buyRecord != null)
                        return ResponseHelper.Create<MallTxBuyPointResponse>((int) MessageCode.MallBuyCountLimit);
                }
                var cash = mallEntity.EffectValue *10;

                response.Data.MallCode = mallCode;
                if (mallEntity.EffectType == (int) EnumMallEffectType.FirstCharge)
                {
                    switch (mallCode)
                    {
                        case 70108:
                            mallCode = 70101;
                            break;
                        case 70109:
                            mallCode = 70102;
                            break;
                        case 70110:
                            mallCode = 70103;
                            break;
                        case 70111:
                            mallCode = 70105;
                            break;
                    }
                }

                var txItemId = CacheFactory.MallCache.GetTxChargeId(mallCode, ConvertHelper.ConvertToInt(opener.Format));
                if (txItemId == 0)
                    return ResponseHelper.Create<MallTxBuyPointResponse>((int) MessageCode.MallItemBuyFail);
                response.Data.Count = 1;
                response.Data.ItemId = txItemId.ToString();
                response.Data.OpenId = manager.Account;
                response.Data.Openkey = opener.OpenKey;
                response.Data.Pf = opener.Pf;
                response.Data.ZoneId = opener.Format;
                response.Data.ItemName = mallEntity.Name;
                response.Data.Cash = cash;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("腾讯验证充值", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        public MallTxBuyPointResultResponse TxBuyPointShipments(Guid managerId, string orderId, decimal cash, int mallCode)
        {
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null || string.IsNullOrEmpty(manager.Account))
                {
                    return ResponseHelper.Create<MallTxBuyPointResultResponse>((int)MessageCode.MissManagers);
                }

                var mallConfig = CacheFactory.MallCache.GetMallEntityWithoutPoint(mallCode);
                if (mallConfig == null)
                {
                    mallConfig = CacheFactory.MallCache.GetMallEntityWithoutPoint(mallCode);
                }
                if (mallConfig == null ||
                    (mallConfig.MallType != (int)EnumMallType.QP && mallConfig.MallType != (int)EnumMallType.Mystery))
                {
                    return ResponseHelper.Create<MallTxBuyPointResultResponse>((int)MessageCode.ItemNotExists);
                }
                if (mallConfig.EffectValue * 10 != cash)
                {
                    return ResponseHelper.Create<MallTxBuyPointResultResponse>((int)MessageCode.PayCashFail);
                }
                ManagerMonthcardEntity monthcardEntity = null;
                DateTime date = DateTime.Now;
                int bonus = 0;
                var recordList = ManagerChargenumberMgr.GetManagerIdList(manager.Idx);
                ManagerChargenumberEntity buyRecord = null;
                bool isFirst = false;
                if (recordList.Count > 0)
                {
                    buyRecord = recordList.Find(r => r.MallCode == mallCode);
                }
                else
                    isFirst = true;
                if (buyRecord == null)
                {
                    buyRecord = new ManagerChargenumberEntity(0, manager.Idx, mallCode, 1, date, date);
                }
                else
                {
                    buyRecord.BuyNumber++;
                }
                MailBuilder mail = null;
                List<ConfigMallgiftbagEntity> prizeList = null;
                EnumMailType mailType = EnumMailType.ChargeSuccess;
                int point = mallConfig.CurrencyCount;
                if (mallConfig.EffectType == (int)EnumMallEffectType.MonthCard) //月卡
                {
                    MonthcardShipments(manager.Idx, ref monthcardEntity);
                }
                else if (mallConfig.MallType == (int)EnumMallType.Mystery)
                {
                    if (mallConfig.EffectType == (int)EnumMallEffectType.GiftBag)
                    {
                        if (!IsHaveBuyWeekGiftBag(buyRecord, DateTime.Now))
                        {
                            mailType = EnumMailType.ChargeSuccess;
                            point = mallConfig.EffectValue * 10;
                        }
                        else
                        {
                            prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                            if (prizeList.Count <= 0)
                                return ResponseHelper.Create<MallTxBuyPointResultResponse>((int)MessageCode.ItemNotExists);
                            mailType = EnumMailType.GiftBagSuccess;
                        }
                    }
                    else if (mallConfig.EffectType == (int)EnumMallEffectType.WeeklyGiftBag)
                    {
                        if (!IsHaveBuyWeekGiftBag(buyRecord))
                        {
                            mailType = EnumMailType.ChargeSuccess;
                            point = mallConfig.EffectValue * 10;
                        }
                        else
                        {
                            prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                            if (prizeList.Count <= 0)
                                return ResponseHelper.Create<MallTxBuyPointResultResponse>((int)MessageCode.ItemNotExists);
                            mailType = EnumMailType.GiftBagSuccess;
                        }
                    }
                    else if (buyRecord.Idx > 0)
                    {
                        mailType = EnumMailType.ChargeSuccess;
                        point = mallConfig.EffectValue * 10;
                    }
                    else
                    {
                        prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                        if (prizeList.Count <= 0)
                            return ResponseHelper.Create<MallTxBuyPointResultResponse>((int)MessageCode.ItemNotExists);
                        mailType = EnumMailType.GiftBagSuccess;
                    }
                }
                else if (mallConfig.EffectType == (int)EnumMallEffectType.FirstCharge) //首冲4倍
                {
                    prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                    FirstChargeShipments(manager.Idx, recordList, ref bonus, ref mallConfig, ref buyRecord);
                }
                else
                {
                    prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                    BuyPointShipments(buyRecord, mallConfig, ref bonus);
                }
                
                //欧洲杯狂欢
                if (ActivityExThread.Instance.IsActivity(Entity.Enums.Activity.EnumActivityExEffectType.EuropeCarnival,
                    0, 0))
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                    {
                        int addpoint = point + bonus;
                        ActivityExThread.Instance.EuropeCarnival(6, ref addpoint);
                        bonus += addpoint;
                    }
                }
                int addLuckyCoin = 0;
                int addgameCurrency = 0;
                if (TurntableCore.Instance.IsActivity)
                {
                    if (_turntableLuckyCoinConfig.ContainsKey(mallConfig.MallCode))
                        addLuckyCoin = _turntableLuckyCoinConfig[mallConfig.MallCode];
                }
                if (PenaltyKickCore.Instance.IsActivity)
                {
                    if (_penaltyKickGameCurrencyConfig.ContainsKey(mallConfig.MallCode))
                        addgameCurrency = _penaltyKickGameCurrencyConfig[mallConfig.MallCode];
                }
                var payEntity = new PayChargehistoryEntity();
                payEntity.Idx = orderId;
                payEntity.Account = manager.Account;
                payEntity.BillingId = orderId;
                payEntity.Bonus = bonus;
                payEntity.Cash = cash;
                payEntity.IsFirst = isFirst;
                payEntity.MallCode = mallCode;
                payEntity.Point = point;
                payEntity.RowTime = DateTime.Now;
                payEntity.SourceType = (int) EnumChargeSourceType.System;
                payEntity.States = 1;
                payEntity.UpdateTime = DateTime.Now;
                //邮件发货
                mail = new MailBuilder(manager.Idx, mallConfig.Name, point + bonus, prizeList, mailType, addLuckyCoin,
                    addgameCurrency);
                buyRecord.UpdateTiem = DateTime.Now;
                payEntity.Bonus = bonus;
                var code = SaveBuyPointShipmentsTx(manager.Idx, manager.Account, cash, point, bonus, payEntity,
                    monthcardEntity, mail,
                    buyRecord, mallConfig);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<MallTxBuyPointResultResponse>(code);

                var response = new MallTxBuyPointResultResponse();
                response.Data = new MallTxBuyPointResult();
                response.Data.AddPoint = point + bonus;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("充值发货", ex);
                return ResponseHelper.Create<MallTxBuyPointResultResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 购买钻石发货（判断区域）
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="orderId"></param>
        /// <param name="billingId"></param>
        /// <param name="cash"></param>
        /// <param name="mallCode"></param>
        /// <returns></returns>
         public MallBuyPointResponse BuyPointShipments(string mid, string orderId, string billingId, decimal cash,
            int mallCode)
        {
            var manager = new NbManagerEntity();
            string str = "";
            if (ShareUtil.IsEgret || ShareUtil.IsQunHei)//白鹭接口传参为account
            {
                manager = ManagerCore.Instance.GetManager(mid);// NbManagerMgr.GetByAccount(mid);
                if (manager == null)
                {
                    return ResponseHelper.Create<MallBuyPointResponse>((int)MessageCode.MissManagers);
                }
                return BuyPointShipments(manager, orderId, billingId, cash, mallCode);
            }

            Guid managerId = Guid.Empty;
            //腾讯itemcode转换为mallcode
            if (ShareUtil.IsTx)
            {
                var itemId = AppsettingCache.Instance.FindValueByTX(mallCode);
                if (itemId != 0)
                {
                    mallCode = itemId;
                }
            }

            try
            {
                managerId = new Guid(mid);
            }
            catch
            {
                return ResponseHelper.Create<MallBuyPointResponse>((int)MessageCode.MissManagers);
            }
            manager = ManagerCore.Instance.GetManager(managerId);
             return BuyPointShipments(manager, orderId, billingId, cash, mallCode);

        }

        /// <summary>
         /// 购买钻石发货
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="orderId"></param>
        /// <param name="billingId"></param>
        /// <param name="cash"></param>
        /// <param name="mallCode"></param>
        /// <returns></returns>
         public MallBuyPointResponse BuyPointShipments(NbManagerEntity manager, string orderId, string billingId, decimal cash,
            int mallCode)
        {
            try
            {
                if (manager == null || string.IsNullOrEmpty(manager.Account))
                {
                    return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.MissManagers);
                }
                var payEntity = PayChargehistoryMgr.GetById(orderId);
                if (payEntity == null || payEntity.Account != manager.Account)
                {
                    return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.PayNoOrderId);
                }
                if (payEntity.States == 1)
                {
                    return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.Success);
                }
                var mallConfig = CacheFactory.MallCache.GetMallEntityWithoutPoint(payEntity.MallCode);
                if (mallConfig == null)
                {
                    mallConfig = CacheFactory.MallCache.GetMallEntityWithoutPoint(mallCode);
                }
                if (mallConfig == null ||
                    (mallConfig.MallType != (int) EnumMallType.QP && mallConfig.MallType != (int) EnumMallType.Mystery))
                {
                    return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.ItemNotExists);
                }
                if ((decimal) mallConfig.EffectValue != cash && ((decimal) mallConfig.EffectValue*80)/100 != cash)
                {
                    return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.PayCashFail);
                }
                ManagerMonthcardEntity monthcardEntity = null;
                DateTime date = DateTime.Now;
                int bonus = 0;
                var recordList = ManagerChargenumberMgr.GetManagerIdList(manager.Idx);
                ManagerChargenumberEntity buyRecord = null;
                bool isFirst = false;
                if (recordList.Count > 0)
                {
                    buyRecord = recordList.Find(r => r.MallCode == mallCode);
                }
                else
                    isFirst = true;
                if (buyRecord == null)
                {
                    buyRecord = new ManagerChargenumberEntity(0, manager.Idx, mallCode, 1, date, date);
                }
                else
                {
                    buyRecord.BuyNumber ++;
                }
                MailBuilder mail = null;
                List<ConfigMallgiftbagEntity> prizeList = null;
                EnumMailType mailType = EnumMailType.ChargeSuccess;
                int point = mallConfig.CurrencyCount;
                if (mallConfig.EffectType == (int) EnumMallEffectType.MonthCard) //月卡
                {
                    MonthcardShipments(manager.Idx, ref monthcardEntity);
                }
                else if (mallConfig.MallType == (int) EnumMallType.Mystery)
                {
                    if (mallConfig.EffectType == (int) EnumMallEffectType.GiftBag)
                    {
                        if (!IsHaveBuyWeekGiftBag(buyRecord, DateTime.Now))
                        {
                            mailType = EnumMailType.ChargeSuccess;
                            point = mallConfig.EffectValue*10;
                        }
                        else
                        {
                            prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                            if (prizeList.Count <= 0)
                                return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.ItemNotExists);
                            mailType = EnumMailType.GiftBagSuccess;
                        }
                    }
                    else if (mallConfig.EffectType == (int) EnumMallEffectType.WeeklyGiftBag)
                    {
                        if (!IsHaveBuyWeekGiftBag(buyRecord))
                        {
                            mailType = EnumMailType.ChargeSuccess;
                            point = mallConfig.EffectValue*10;
                        }
                        else
                        {
                            prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                            if (prizeList.Count <= 0)
                                return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.ItemNotExists);
                            mailType = EnumMailType.GiftBagSuccess;
                        }
                    }
                    else if (buyRecord.Idx > 0)
                    {
                        mailType = EnumMailType.ChargeSuccess;
                        point = mallConfig.EffectValue*10;
                    }
                    else
                    {
                        prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                        if (prizeList.Count <= 0)
                            return ResponseHelper.Create<MallBuyPointResponse>((int) MessageCode.ItemNotExists);
                        mailType = EnumMailType.GiftBagSuccess;
                    }
                }
                else if (mallConfig.EffectType == (int) EnumMallEffectType.FirstCharge) //首冲4倍
                {
                    prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                    FirstChargeShipments(manager.Idx, recordList, ref bonus, ref mallConfig, ref buyRecord);
                }
                else
                {
                    prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(mallCode);
                    BuyPointShipments(buyRecord, mallConfig, ref bonus);
                }
                payEntity.BillingId = billingId;
                payEntity.States = 1;
                payEntity.UpdateTime = DateTime.Now;
                payEntity.Cash = cash;
                payEntity.IsFirst = isFirst;
                //欧洲杯狂欢
                if (ActivityExThread.Instance.IsActivity(Entity.Enums.Activity.EnumActivityExEffectType.EuropeCarnival,
                    0, 0))
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                    {
                        int addpoint = point + bonus;
                        ActivityExThread.Instance.EuropeCarnival(6, ref addpoint);
                        bonus += addpoint;
                    }
                }
                int addLuckyCoin = 0;
                int addgameCurrency = 0;
                if (TurntableCore.Instance.IsActivity)
                {
                    if (_turntableLuckyCoinConfig.ContainsKey(mallConfig.MallCode))
                        addLuckyCoin = _turntableLuckyCoinConfig[mallConfig.MallCode];
                }
                if (PenaltyKickCore.Instance.IsActivity)
                {
                    if (_penaltyKickGameCurrencyConfig.ContainsKey(mallConfig.MallCode))
                        addgameCurrency = _penaltyKickGameCurrencyConfig[mallConfig.MallCode];
                }
                //邮件发货
                mail = new MailBuilder(manager.Idx, mallConfig.Name, point + bonus, prizeList, mailType, addLuckyCoin,
                    addgameCurrency);
                buyRecord.UpdateTiem = DateTime.Now;
                payEntity.Bonus = bonus;
                var code = SaveBuyPointShipments(manager.Idx, manager.Account, cash, point, bonus, payEntity,
                    monthcardEntity, mail,
                    buyRecord, mallConfig);
                return ResponseHelper.Create<MallBuyPointResponse>(code);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("充值发货", ex);
                return ResponseHelper.Create<MallBuyPointResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 购买月卡发货
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="monthcardEntity"></param>
        /// <returns></returns>
        private MessageCode MonthcardShipments(Guid managerId,ref ManagerMonthcardEntity monthcardEntity)
        {
            DateTime date = DateTime.Now;
            monthcardEntity = ManagerMonthcardMgr.GetById(managerId);
            if (monthcardEntity == null)
            {
                monthcardEntity = new ManagerMonthcardEntity(managerId, 1, date, date.AddDays(30),
                    date.AddDays(-1),
                    date, date);
            }
            else
            {
                monthcardEntity.BuyNumber++;
                if (monthcardEntity.DueToTime.Date < date.Date) //已过期
                {
                    monthcardEntity.BuyTime = date;
                    monthcardEntity.DueToTime = date.AddDays(30);
                    monthcardEntity.PrizeDate = date.AddDays(-1);
                    monthcardEntity.UpdateTime = date;
                    monthcardEntity.RowTime = date;
                }
                else //未过期
                {
                    monthcardEntity.DueToTime = monthcardEntity.DueToTime.AddDays(30);
                    monthcardEntity.UpdateTime = date;
                }
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 首次购买发货
        /// </summary>
        /// <param name="maangerId"></param>
        /// <param name="recordList"></param>
        /// <param name="bonus"></param>
        /// <param name="mallConfig"></param>
        /// <param name="buyRecord"></param>
        /// <returns></returns>
        private MessageCode FirstChargeShipments(Guid maangerId, List<ManagerChargenumberEntity> recordList, ref int bonus, ref DicMallItemDataEntity mallConfig, ref ManagerChargenumberEntity buyRecord)
        {
            DateTime date = DateTime.Now;
            if (recordList.Count == 0)
            {
                bonus = mallConfig.CurrencyCount*3;
                return MessageCode.Success;
            }
            int mallCode = 0;
            switch (mallConfig.MallCode)
            {
                case 70108:
                    mallCode = 70101;
                    break;
                case 70109:
                    mallCode = 70102;
                    break;
                case 70110:
                    mallCode = 70103;
                    break;
                case 70111:
                    mallCode = 70105;
                    break;
                default:
                    return MessageCode.Success;
                    break;
            }

            mallConfig = CacheFactory.MallCache.GetMallEntityWithoutPoint(mallCode);
            buyRecord = recordList.Find(r => r.MallCode == mallCode);
            if (buyRecord == null)
            {
                buyRecord = new ManagerChargenumberEntity(0, maangerId, mallCode, 1, date, date);
            }
            else
            {
                buyRecord.BuyNumber++;
            }
            if (buyRecord.Idx == 0) //首次充值
            {
                int doubles = 2;
                if (_chargeDoubleConfig.ContainsKey(mallCode))
                    doubles = _chargeDoubleConfig[mallCode];
                if (doubles < 2)
                    doubles = 2;
                bonus = mallConfig.CurrencyCount*(doubles - 1);
            }
            else
            {
                int returns = 0;
                if (_chargeReturnConfig.ContainsKey(mallCode))
                    returns = _chargeReturnConfig[mallCode];
                if (returns < 0)
                    returns = 0;
                bonus = (mallConfig.CurrencyCount*returns)/100;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 购买钻石发货
        /// </summary>
        /// <param name="buyRecord"></param>
        /// <param name="mallConfig"></param>
        /// <param name="bonus"></param>
        /// <returns></returns>
        private MessageCode BuyPointShipments(ManagerChargenumberEntity buyRecord,DicMallItemDataEntity mallConfig,ref int bonus)
        {
            if (buyRecord.Idx == 0) //首次充值
            {
                int doubles = 2;
                if (_chargeDoubleConfig.ContainsKey(mallConfig.MallCode))
                    doubles = _chargeDoubleConfig[mallConfig.MallCode];
                if (doubles < 2)
                    doubles = 2;
                bonus = mallConfig.CurrencyCount * (doubles - 1);
            }
            else
            {
                int returns = 0;
                if (_chargeReturnConfig.ContainsKey(mallConfig.MallCode))
                    returns = _chargeReturnConfig[mallConfig.MallCode];
                if (returns < 0)
                    returns = 0;
                bonus = (mallConfig.CurrencyCount * returns) / 100;
            }
            return MessageCode.Success;
        }

        private MessageCode SaveBuyPointShipments(Guid managerId, string account, decimal cash, int point, int bonus, PayChargehistoryEntity payEntity, ManagerMonthcardEntity monthcardEntity, MailBuilder mail, ManagerChargenumberEntity buyRecord, DicMallItemDataEntity mallConfig)
        {
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                MessageCode messCode = MessageCode.NbUpdateFail;
                do
                {
                    if (point > 0 || bonus > 0)
                    {
                        var messCode1 = PayCore.Instance.AddPoint(payEntity.Account, point, bonus, (int)EnumChargeSourceType.System,
                            payEntity.BillingId, transactionManager.TransactionObject);
                        if (messCode1 != MessageCode.Success)
                        {
                            messCode = messCode1;
                            break;
                        }
                    }
                    if (!PayChargehistoryMgr.Update(payEntity, transactionManager.TransactionObject))
                        break;
                    if (monthcardEntity != null)
                    {
                        if (monthcardEntity.BuyNumber == 1)
                        {
                            if (!ManagerMonthcardMgr.Insert(monthcardEntity, transactionManager.TransactionObject))
                                break;
                        }
                        else
                        {
                            if (!ManagerMonthcardMgr.Update(monthcardEntity, transactionManager.TransactionObject))
                                break;
                        }
                    }
                    if (!mail.Save(transactionManager.TransactionObject))
                        break;
                    if (buyRecord.Idx == 0)
                    {
                        if (!ManagerChargenumberMgr.Insert(buyRecord, transactionManager.TransactionObject))
                            break;
                    }
                    else
                    {
                        if (!ManagerChargenumberMgr.Update(buyRecord, transactionManager.TransactionObject))
                            break;
                    }
                    //if (addLuckyCoin > 0)
                    //{
                    //    if (
                    //        !TurntableManagerMgr.AddLuckyCoin(managerId, addLuckyCoin,
                    //            transactionManager.TransactionObject))
                    //        break;
                    //}
                    //if (addgameCurrency > 0)
                    //{
                    //    if (
                    //        !PenaltykickManagerMgr.AddGameCurrency(managerId, addgameCurrency,
                    //            transactionManager.TransactionObject))
                    //        break;
                    //}
                    messCode = MessageCode.Success;
                } while (false);
                if (messCode == MessageCode.Success)
                {
                    transactionManager.Commit();
                    if (mallConfig.MallType != (int)EnumMallType.Mystery)
                    {
                        if (!_buyPointPoss.ContainsKey(managerId))
                            _buyPointPoss.TryAdd(managerId, true);
                    }
                    PayCore.ChargeAfter(account, point, 0, DateTime.Now, cash, mallConfig.EffectValue * 10);
                    ManagerCore.Instance.DeleteCache(managerId);
                }
                else
                {
                    transactionManager.Rollback();
                    return messCode;
                }
            }
            return MessageCode.Success;
        }

        private MessageCode SaveBuyPointShipmentsTx(Guid managerId, string account, decimal cash, int point, int bonus, PayChargehistoryEntity payEntity, ManagerMonthcardEntity monthcardEntity, MailBuilder mail, ManagerChargenumberEntity buyRecord, DicMallItemDataEntity mallConfig)
        {
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                MessageCode messCode = MessageCode.NbUpdateFail;
                do
                {
                    if (point > 0 || bonus > 0)
                    {
                        var messCode1 = PayCore.Instance.AddPoint(payEntity.Account, point, bonus, (int)EnumChargeSourceType.System,
                            payEntity.BillingId, transactionManager.TransactionObject);
                        if (messCode1 != MessageCode.Success)
                        {
                            messCode = messCode1;
                            break;
                        }
                    }
                    if (!PayChargehistoryMgr.Insert(payEntity, transactionManager.TransactionObject))
                        break;
                    if (monthcardEntity != null)
                    {
                        if (monthcardEntity.BuyNumber == 1)
                        {
                            if (!ManagerMonthcardMgr.Insert(monthcardEntity, transactionManager.TransactionObject))
                                break;
                        }
                        else
                        {
                            if (!ManagerMonthcardMgr.Update(monthcardEntity, transactionManager.TransactionObject))
                                break;
                        }
                    }
                    if (!mail.Save(transactionManager.TransactionObject))
                        break;
                    if (buyRecord.Idx == 0)
                    {
                        if (!ManagerChargenumberMgr.Insert(buyRecord, transactionManager.TransactionObject))
                            break;
                    }
                    else
                    {
                        if (!ManagerChargenumberMgr.Update(buyRecord, transactionManager.TransactionObject))
                            break;
                    }
                    messCode = MessageCode.Success;
                } while (false);
                if (messCode == MessageCode.Success)
                {
                    transactionManager.Commit();
                    if (mallConfig.MallType != (int)EnumMallType.Mystery)
                    {
                        if (!_buyPointPoss.ContainsKey(managerId))
                            _buyPointPoss.TryAdd(managerId, true);
                    }
                    PayCore.ChargeAfter(account, point, 0, DateTime.Now, cash, mallConfig.EffectValue * 10);
                    ManagerCore.Instance.DeleteCache(managerId);
                }
                else
                {
                    transactionManager.Rollback();
                    return messCode;
                }
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 是否可以购买每周礼包
        /// </summary>
        /// <param name="managerInfo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsHaveBuyWeekGiftBag(ManagerChargenumberEntity managerInfo, DateTime date)
        {
            DateTime startBuyTime = ShareUtil.GetThisWeekDayDate(DayOfWeek.Friday);
            DateTime endBuyTime = ShareUtil.GetThisWeekDayDate(DayOfWeek.Sunday);
            if (managerInfo != null && managerInfo.Idx > 0)
            {
                if (managerInfo.UpdateTiem > startBuyTime) //购买过了
                    return false;                
            }
            if (date < startBuyTime) //时间未到
                return false;
            return true;
        } 
        /// <summary>
        /// 是否可以购买每周礼包
        /// </summary>
        /// <param name="managerInfo"></param>
        /// <returns></returns>
        private bool IsHaveBuyWeekGiftBag(ManagerChargenumberEntity managerInfo)
        {
            DateTime buyTime = ShareUtil.GetThisWeekDayDate(DayOfWeek.Monday);
            if (managerInfo != null && managerInfo.Idx > 0)
            {
                if (managerInfo.UpdateTiem >= buyTime) //购买过了
                    return false;
            }
            return true;
        }
        #endregion

        #region QuickenTrain
        /// <summary>
        /// 加速训练
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <param name="curTime"></param>
        /// <param name="trainEntity"></param>
        /// <returns></returns>
        public MessageCode QuickenTrain(Guid managerId, int mallCode, DateTime curTime, TeammemberTrainEntity trainEntity, NbManagerextraEntity extra, out int curPoint)
        {
            PayUserEntity payUserEntity = null;
            NbManagerEntity manager = null;
            MallSalerecordEntity saleRecord = null;
            int currencyCount = 0;
            int currencyType = 0;
            curPoint = -1;
            if (mallCode > 0)
            {
                var mallEntity = CacheFactory.MallCache.GetMallEntity(mallCode, curTime);

                currencyCount = mallEntity.CurrencyCount;
                int rawCurrencyCount = mallEntity.RawCurrencyCount;
                currencyType = mallEntity.CurrencyType;
                int count = 1;


                var code = MallCheck(managerId, count, currencyType, currencyCount, out payUserEntity, out manager);
                if (code != MessageCode.Success)
                    return code;

                saleRecord = BuildSalerecord(mallCode, managerId, currencyType, currencyCount, rawCurrencyCount,
                                                 count, curTime, false);
            }
            var code2 = SaveQuickenTrain(saleRecord, payUserEntity, manager, currencyType, currencyCount, trainEntity, extra);
            if (code2 == MessageCode.Success && mallCode > 0)
            {
                curPoint = CalCurCurrency(currencyType, payUserEntity, currencyCount, manager,
                                          EnumCoinConsumeSourceType.MallBuy, saleRecord);
            }
            return code2;
        }
        #endregion

        #region Scouting

        public ScoutingLotteryResponse Scouting(Guid managerId, int mallCode, DateTime curTime,
            ScoutingRecordEntity scoutingRecord, bool isTen, List<int> cardList, bool isAutoDec, bool isFree = false)
        {
            var mallEntity = CacheFactory.MallCache.GetMallEntity(mallCode, curTime);
            PayUserEntity payUserEntity = null;
            NbManagerEntity manager = null;
            MailBuilder mail = null;
            int currencyCount = 0;
            if (!isFree)
                currencyCount = mallEntity.CurrencyCount;
            else
                scoutingRecord.Status = 2; //免费
            int rawCurrencyCount = mallEntity.RawCurrencyCount;
            int currencyType = mallEntity.CurrencyType;
            int count = 1;
            if (isTen)
            {
                currencyCount = currencyCount * _scoutingTenPointMultiple; //9折
                rawCurrencyCount = rawCurrencyCount * _scoutingTenPointMultiple;
            }

            ActivityExThread.Instance.ScoutingHalfPrice(ref currencyCount);
            var code = MallCheck(managerId, count, currencyType, currencyCount, out payUserEntity, out manager, isFree);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<ScoutingLotteryResponse>(code);
            if (manager == null)
            {
                manager = ManagerCore.Instance.GetManager(managerId);
            }

            var packageFrame = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ScoutingLottery);
            //可出售的物品串
            string dealItemString = "";
            if (isTen)
            {
                if (packageFrame.BlankCount < 10)
                    return ResponseHelper.Create<ScoutingLotteryResponse>(MessageCode.ItemPackageFull);
                foreach (var itemCode in cardList)
                {
                    bool isDeal = false;
                    //金牌球探
                    if (currencyType == (int)EnumCurrencyType.Point)
                        isDeal = IsHaveDeal(itemCode);
                    if (isDeal)
                        dealItemString += itemCode + ",";
                    var pCode = packageFrame.AddItem(itemCode, scoutingRecord.Strength, scoutingRecord.IsBinding, isDeal);
                    if (pCode != MessageCode.Success)
                    {
                        return ResponseHelper.Create<ScoutingLotteryResponse>(pCode);
                    }
                }
                if (dealItemString.Length > 0)
                    dealItemString = dealItemString.Substring(0, dealItemString.Length - 1);
            }
            else
            {
                bool isDeal = false;
                //金牌球探
                if (currencyType == (int) EnumCurrencyType.Point)
                    isDeal = IsHaveDeal(scoutingRecord.ItemCode);
                if (isDeal)
                    dealItemString += scoutingRecord.ItemCode;
                var result = packageFrame.AddItem(scoutingRecord.ItemCode, scoutingRecord.Strength,
                    scoutingRecord.IsBinding, isDeal);
                if (result != MessageCode.Success)
                {
                    return ResponseHelper.Create<ScoutingLotteryResponse>(result);
                }
            }

            var saleRecord = BuildSalerecord(mallCode, managerId, currencyType, currencyCount, rawCurrencyCount, count,
                curTime, false);

            code = SaveScouting(saleRecord, payUserEntity, manager, 0, currencyType, currencyCount, scoutingRecord,
                packageFrame, mail, isFree);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<ScoutingLotteryResponse>(code);
            }
            else
            {
                var response = ResponseHelper.CreateSuccess<ScoutingLotteryResponse>();
                response.Data = new ScoutingLotteryEntity();
                var extra = ManagerCore.Instance.GetManagerExtra(managerId);
                response.Data = new ScoutingLotteryEntity();
                response.Data.PointScouting = extra.Scouting;
                response.Data.CoinScouting = extra.CoinScouting;
                response.Data.FriendScouting = extra.FriendScouting;
                response.Data.NextPointScoutingFreeTick = ShareUtil.GetTimeTick(extra.ScoutingUpdate);
                response.Data.NextCoinScoutingFreeTick = ShareUtil.GetTimeTick(extra.CoinScoutingUpdate);
                response.Data.NextFriendScoutingFreeTick = ShareUtil.GetTimeTick(extra.FriendScoutingUpdate);
                response.Data.CurrencyType = currencyType;
                response.Data.CurCurrency = CalCurCurrency(currencyType, payUserEntity, currencyCount, manager,
                    EnumCoinConsumeSourceType.MallBuy, saleRecord);
                response.Data.IsBinding = scoutingRecord.IsBinding;
                response.Data.ItemCode = scoutingRecord.ItemCode;
                response.Data.ItemString = scoutingRecord.ItemString;
                response.Data.Strength = scoutingRecord.Strength;
                response.Data.AddReiki = 0;
                response.Data.PackageIsFull = false;
                response.Data.DealItemString = dealItemString;
                if (packageFrame.Save())
                {
                    packageFrame.Shadow.Save();
                }
                return response;
            }
        }

        public ScoutingLotteryResponse Scouting(Guid managerId, DateTime curTime,ScoutingRecordEntity scoutingRecord,ScoutingGoldbarEntity scoutingManager, bool isTen, List<int> cardList)
        {
            int count = 1;
           
            var packageFrame = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ScoutingLottery);
            //可出售的物品串
            string dealItemString = "";
            if (isTen)
            {
                if (packageFrame.BlankCount < 10)
                    return ResponseHelper.Create<ScoutingLotteryResponse>(MessageCode.ItemPackageFull);
                foreach (var itemCode in cardList)
                {
                    dealItemString += itemCode + ",";
                    //var item = CacheFactory.ItemsdicCache.GetItem(itemCode);
                    //bool isDeal = true;
                    //if (item != null && item.ItemType == (int) EnumItemType.MallItem &&
                    //    item.MallEffectType == (int) EnumMallEffectType.CoachDebris)
                    //    isDeal = false;
                    var pCode = packageFrame.AddItem(itemCode, scoutingRecord.Strength, scoutingRecord.IsBinding, true);
                    if (pCode != MessageCode.Success)
                    {
                        return ResponseHelper.Create<ScoutingLotteryResponse>(pCode);
                    }
                }
                if (dealItemString.Length > 0)
                    dealItemString = dealItemString.Substring(0, dealItemString.Length - 1);
            }
            else
            {
                dealItemString += scoutingRecord.ItemCode;
                var result = packageFrame.AddItem(scoutingRecord.ItemCode, scoutingRecord.Strength,
                    scoutingRecord.IsBinding,true);
                if (result != MessageCode.Success)
                {
                    return ResponseHelper.Create<ScoutingLotteryResponse>(result);
                }
            }

            var code = SaveScouting(scoutingManager,scoutingRecord, packageFrame);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<ScoutingLotteryResponse>(code);
            }
            else
            {
                var response = ResponseHelper.CreateSuccess<ScoutingLotteryResponse>();
                response.Data = new ScoutingLotteryEntity();
                var extra = ManagerCore.Instance.GetManagerExtra(managerId);
                response.Data = new ScoutingLotteryEntity();
                response.Data.PointScouting = extra.Scouting;
                response.Data.CoinScouting = extra.CoinScouting;
                response.Data.FriendScouting = extra.FriendScouting;
                response.Data.NextPointScoutingFreeTick = ShareUtil.GetTimeTick(extra.ScoutingUpdate);
                response.Data.NextCoinScoutingFreeTick = ShareUtil.GetTimeTick(extra.CoinScoutingUpdate);
                response.Data.NextFriendScoutingFreeTick = ShareUtil.GetTimeTick(extra.FriendScoutingUpdate);
                response.Data.CurrencyType = (int)EnumCurrencyType.GoldBar;
                response.Data.CurCurrency =scoutingManager.GoldBarNumber;
                response.Data.IsBinding = scoutingRecord.IsBinding;
                response.Data.ItemCode = scoutingRecord.ItemCode;
                response.Data.ItemString = scoutingRecord.ItemString;
                response.Data.Strength = scoutingRecord.Strength;
                response.Data.AddReiki = 0;
                response.Data.PackageIsFull = false;
                response.Data.DealItemString = dealItemString;
                if (packageFrame.Save())
                {
                    packageFrame.Shadow.Save();
                }
                return response;
            }
        }


        #endregion

        #region Pandora
        public MessageCode Pandora(Guid managerId, ItemPackageFrame package, NbManagerEntity manager, PayUserEntity payUser, NbManagerextraEntity extra, int costCoin, int costPoint, bool packageFlag = false, TeammemberEntity teammember = null)
        {
            return SavePandora(managerId, EnumConsumeSourceType.StrengthProtect, package, manager, payUser, extra, null, costCoin, costPoint, teammember);
        }

        public MessageCode Pandora(ItemPackageFrame package, NbManagerEntity manager, PayUserEntity payUser, NbManagerextraEntity extra, int costCoin, int costPoint, int mallCode, bool packageFlag = false)
        {
            MallSalerecordEntity salerecord = null;
            if (costPoint > 0)
            {
                var rawPoint = CacheFactory.MallCache.GetCostPoint(mallCode, DateTime.Now);
                salerecord = BuildSalerecord(mallCode, package.ManagerId, (int)EnumCurrencyType.Point, costPoint, rawPoint, 1, DateTime.Now, packageFlag);
            }
            return SavePandora(package.ManagerId, EnumConsumeSourceType.Mall, package, manager, payUser, extra, salerecord, costCoin, costPoint);
        }
        #endregion

        #region DailytaskAction
        ///// <summary>
        ///// 每日任务
        ///// </summary>
        ///// <param name="managerId"></param>
        ///// <param name="mallCode"></param>
        ///// <param name="curTime"></param>
        ///// <param name="taskEntity"></param>
        ///// <returns></returns>
        //public MessageCode DailytaskAction(Guid managerId, int mallCode, DateTime curTime, TaskDailyrecordEntity taskEntity, out int point)
        //{
        //    var mallEntity = CacheFactory.MallCache.GetMallEntity(mallCode, curTime);
        //    PayUserEntity payUserEntity = null;
        //    NbManagerEntity manager = null;
        //    int currencyCount = mallEntity.CurrencyCount;
        //    int rawCurrencyCount = mallEntity.RawCurrencyCount;
        //    int currencyType = mallEntity.CurrencyType;
        //    int count = 1;
        //    point = -1;
        //    var code = MallCheck(managerId, count, currencyType, currencyCount, out payUserEntity, out manager);
        //    if (code != MessageCode.Success)
        //        return code;

        //    var saleRecord = BuildSalerecord(mallCode, managerId, currencyType, currencyCount, rawCurrencyCount, count, curTime);

        //    code = SaveDailytaskAction(saleRecord, payUserEntity, manager, currencyType, currencyCount, taskEntity);
        //    if (code != MessageCode.Success)
        //        return code;
        //    point = payUserEntity.TotalPoint - currencyCount;
        //    return code;
        //}
        #endregion

        #region EquipmentWash
        public MessageCode EquipmentWash(Guid managerId, bool buyStone, bool buyFusogen, bool buyLockProperty, bool buyLockSkill, int quality, ItemPackageFrame package, ref int currentPoint, ref int costPoint)
        {
            PayUserEntity payUserEntity = null;
            MallSalerecordEntity salerecordStone = null;
            MallSalerecordEntity salerecordFusogen = null;
            MallSalerecordEntity salerecordLockProperty = null;
            currentPoint = -1;
            int currencyCount = 0;
            DateTime curTime = DateTime.Now;
            var plusCache = CacheFactory.EquipmentCache.GetEquipmentPlus(quality);
            if (plusCache == null)
                return MessageCode.NbParameterError;
            if (buyStone)
            {
                var mallEntity = CacheFactory.MallCache.GetMallEntity(plusCache.WashMallCode, curTime);
                currencyCount += mallEntity.CurrencyCount;
                salerecordStone = BuildSalerecord(mallEntity, managerId, curTime, false);
            }
            if (buyFusogen)
            {
                var mallEntity = CacheFactory.MallCache.GetMallEntity(PandoraCore.Instance.FusogenMallcode, curTime);
                currencyCount += mallEntity.CurrencyCount;
                salerecordFusogen = BuildSalerecord(mallEntity, managerId, curTime, false);
            }
            if (buyLockProperty)
            {
                var mallEntity = CacheFactory.MallCache.GetMallEntity(plusCache.LockMallCode, curTime);
                currencyCount += mallEntity.CurrencyCount;
                salerecordLockProperty = BuildSalerecord(mallEntity, managerId, curTime, false);
            }
            if (buyLockSkill)
            {
                currencyCount += _lockSkillPoint;
            }
            if (currencyCount > 0)
            {
                payUserEntity = PayCore.Instance.GetPayUser(managerId);
                if (payUserEntity.TotalPoint < currencyCount)
                    return MessageCode.NbPointShortage;
                currentPoint = payUserEntity.TotalPoint - currencyCount;
            }
            costPoint = currencyCount;
            return SaveEquipmentWash(salerecordStone, salerecordFusogen, salerecordLockProperty, payUserEntity,
                                     currencyCount, package, buyLockSkill);
        }
        #endregion

        #region UseItem
        /// <summary>
        /// 7	增加体力
        /// 8	增加灵气
        /// 9	增加金币
        /// 35	卡库
        /// 36	技能
        /// 37	新手礼包
        /// 38	恢复训练体力
        /// 39	Buff
        /// 65  荣誉
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="usedCount"></param>
        /// <returns></returns>
        public MallUseItemResponse UseItem(Guid managerId, Guid itemId, int usedCount)
        {
            #region Check

            if (usedCount < 1)
                usedCount = 1;
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ItemUsed);
            if (package == null)
                return ResponseHelper.InvalidParameter<MallUseItemResponse>();
            var item = package.GetItem(itemId);
            if (item == null)
                return ResponseHelper.InvalidParameter<MallUseItemResponse>();
            if (item.ItemCount < usedCount)
                return ResponseHelper.Create<MallUseItemResponse>(MessageCode.ItemCountInvalid);//TODO:
            var mallCache = CacheFactory.ItemsdicCache.GetMallEntityWithoutPointByItemCode(item.ItemCode);
            if (mallCache == null)
                return ResponseHelper.InvalidParameter<MallUseItemResponse>();
            if (usedCount > 1 && mallCache.ShowBatch == false)
            {
                return ResponseHelper.InvalidParameter<MallUseItemResponse>();//TODO 该物品不能批量使用
            }
            #endregion

            int outEffectType = 0;
            int outEffectValue = mallCache.EffectValue * usedCount;
            int curValue = 0;
            int point = 0;
            int bindPoint = 0;
            NbManagerEntity manager = ManagerCore.Instance.GetManager(managerId);
            NbManagerextraEntity managerExtra = null;
            MallExtrarecordEntity mallExtra = null;

            if (mallCache.UseLevel > manager.Level)
            {
                return ResponseHelper.Create<MallUseItemResponse>(MessageCode.MallUsedLevelShortage);
            }

            MessageCode code = package.Delete(item, usedCount);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<MallUseItemResponse>(code);
            }
            TransactionManager tranMgr = null;
            string itemCode = "";
            LadderManagerEntity ladderManager = null;
            OlympicManagerEntity olympicInfo = null;
            int addLuckyCoin = 0;
            int addCoachExp = 0;
            switch (mallCache.EffectType)
            {
                case (int)EnumMallEffectType.AddStamina:
                    outEffectType = 1;
                    code = UseAddStamina(managerId,manager.VipLevel, mallCache.EffectValue, manager.Level, out managerExtra, out curValue);
                    break;
                case (int)EnumMallEffectType.AddReiki:
                    outEffectType = 2;
                    code = UseAddReiki(managerId, mallCache.EffectValue, usedCount, ref manager, out curValue);
                    break;
                case (int)EnumMallEffectType.AddCoin:
                    outEffectType = 3;
                    code = UseAddCoin(managerId, mallCache.EffectValue, usedCount, ref manager, out curValue);
                    break;
                case (int)EnumMallEffectType.AddHonor:
                    outEffectType = 65;//荣誉
                    code = UseAddHonor(managerId, mallCache.EffectValue, usedCount, out ladderManager, out curValue);
                    break;
                case (int)EnumMallEffectType.Cardlib:
                    var _H5_CardCount5 = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_CardCount5);
                    int count = 1;
                    var malcode = ""+mallCache.MallCode;
                    if (_H5_CardCount5.Contains(malcode) )
                        count = 5;
                    if (package.BlankCount < count)
                        return ResponseHelper.Create<MallUseItemResponse>(MessageCode.ItemPackageFull);

                    outEffectType = 6;
                    code = UseCardlib(package, mallCache.EffectValue, item.IsBinding,item.IsDeal, usedCount,count , mallCache.CurrencyCount, out itemCode);
                    
                    break;
                case (int)EnumMallEffectType.SkillCard:
                    outEffectType = 7;
                    code = UseSkillcard(ref tranMgr, managerId, mallCache.EffectValue, out itemCode);
                    break;
                case (int)EnumMallEffectType.NewPlayerPack:
                    mallExtra = MallExtrarecordMgr.GetExtra(managerId, item.ItemCode);
                    if (mallExtra.UsedCount > 0)
                    {
                        code = MessageCode.MallUsedNewPlayerPack;
                    }
                    else
                    {
                        mallExtra.UsedCount = 1;
                        code = UseNewPlayerPack(managerId, mallCache.EffectValue, package, EnumCoinChargeSourceType.NewPlayerPack, ref manager, ref outEffectValue, ref curValue, ref point, ref bindPoint);
                    }
                    if (outEffectValue > 0)
                        outEffectType = 3;
                    break;
                case (int)EnumMallEffectType.AddBuff:
                    code = UseAddBuff(ref tranMgr, managerId, mallCache.EffectValue, mallCache.MallCode);
                    outEffectType = 5;
                    break;
                case (int)EnumMallEffectType.HolidayGifts:
                    if (package.BlankCount < 1)
                        return ResponseHelper.Create<MallUseItemResponse>(MessageCode.ItemPackageFull);
                    var lottery = LotteryCache.Instance.Lottery(EnumLotteryType.HolidayGifts, mallCache.EffectValue);
                    if (lottery == null)
                        code = MessageCode.NbParameterError;
                    else
                    {
                        if (lottery.PrizeItemCode < -100000)//幸运币
                        {
                            addLuckyCoin = lottery.PrizeItemCode%100000;
                        }
                        //点卷
                        else if (lottery.PrizeItemCode < 0)
                        {
                            point += Math.Abs(lottery.PrizeItemCode);
                            outEffectType = 8;
                        }
                        else
                        {
                            itemCode = lottery.PrizeItemCode.ToString();

                            code = package.AddItem(lottery.PrizeItemCode, lottery.Strength, item.IsBinding, false);
                            outEffectType = 6;
                        }
                    }

                    break;
                case (int)EnumMallEffectType.MyStery:
                    code = UseNewPlayerPack(managerId, mallCache.EffectValue, package, EnumCoinChargeSourceType.NewPlayerPack, ref manager, ref outEffectValue, ref curValue, ref point, ref bindPoint);
                    if (outEffectValue > 0)
                        outEffectType = 3;
                    break;
                case (int)EnumMallEffectType.AddExp:
                    outEffectType = 9;
                    int exp = mallCache.EffectValue*usedCount;
                    ManagerUtil.AddManagerData(manager, exp, 0, 0, EnumCoinChargeSourceType.None,
                        ShareUtil.GenerateComb().ToString());
                    break;
                case (int)EnumMallEffectType.OlympicGoldMeda:
                    olympicInfo = OlympicCore.Instance.GetInfo(managerId);
                    outEffectValue = olympicInfo.RandomAddTheGoldMedal();
                    outEffectType = 100;
                    break;
                case (int)EnumMallEffectType.CoachDebrisGiftBag://教练碎片宝箱
                    if (package.BlankCount < 1)
                        return ResponseHelper.Create<MallUseItemResponse>(MessageCode.ItemPackageFull);
                    var coachFrame = CoachCore.Instance.GetFrame(managerId);
                    var coachCode = coachFrame.GetCoachItemCode();
                    code = package.AddItem(coachCode,1, item.IsBinding, false);
                    outEffectType = 6;
                    itemCode = coachCode.ToString();
                    break;
                case (int)EnumMallEffectType.CoachCertificate://教练证书
                    outEffectType = 75;
                    addCoachExp = mallCache.EffectValue * usedCount;
                    break;
                default:
                    code = MessageCode.NbParameterError;
                    break;
            }
            if (code != MessageCode.Success)
            {
                if (null != tranMgr && tranMgr.IsOpen)
                    tranMgr.Rollback();
                return ResponseHelper.Create<MallUseItemResponse>(code);
            }
            code = SaveUseItem(tranMgr, package, manager, managerExtra, mallExtra, point, addLuckyCoin, bindPoint, null, ladderManager, olympicInfo, addCoachExp);
            if (code != MessageCode.Success)
                return ResponseHelper.Create<MallUseItemResponse>(code);
            package.Shadow.Save();

            var response = ResponseHelper.CreateSuccess<MallUseItemResponse>();
            response.Data = new MallUseItemEntity();
            response.Data.EffectType = outEffectType;
            if (point > 0)
            {
                response.Data.EffectValue = point.ToString();
            }
            else if (bindPoint > 0)
            {
                response.Data.EffectValue = bindPoint.ToString();
                response.Data.EffectType = 9;
            }
            else if (!string.IsNullOrEmpty(itemCode))
            {
                response.Data.EffectValue = itemCode;
            }
            else
            {
                response.Data.EffectValue = outEffectValue.ToString();
            }
            response.Data.CurValue = curValue;
            response.Data.Package = ItemCore.Instance.BuildPackageData(package);
            return response;
        }

        MessageCode UseAddStamina(Guid managerId,int vipLevel, int effectValue, int level, out NbManagerextraEntity managerExtra, out int curValue)
        {
            managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
            ManagerUtil.CalCurrentStamina(managerExtra, level,vipLevel);

            managerExtra.Stamina += effectValue;
            curValue = managerExtra.Stamina;
            return MessageCode.Success;
        }

        MessageCode UseAddHonor(Guid managerId, int effectValue, int usedCount, out LadderManagerEntity ladderManager, out int curValue)
        {
            ladderManager = LadderManagerMgr.GetById(managerId, 1000);
            ladderManager.Honor += effectValue * usedCount;
            curValue = ladderManager.Honor;
            return MessageCode.Success;
        }


        MessageCode UseAddReiki(Guid managerId, int effectValue, int usedCount, ref NbManagerEntity manager, out int curValue)
        {
            manager.Reiki += effectValue * usedCount;
            curValue = manager.Reiki;
            return MessageCode.Success;
        }

        MessageCode UseAddCoin(Guid managerId, int effectValue, int usedCount, ref NbManagerEntity manager, out int curValue)
        {
            manager.Coin += effectValue * usedCount;
            curValue = manager.Coin;
            return MessageCode.Success;
        }

        MessageCode UseCardlib(ItemPackageFrame package, int effectValue, bool isBinding,bool isDeal, int usedCount, int itemCount,int currencyCount, out string itemCodeStr)
        {
            itemCodeStr = "";
            for (int i = 0; i < usedCount; i++)
            {
                var itemCode = CacheFactory.LotteryCache.LotteryByLib(effectValue);
                itemCodeStr = itemCode.ToString() + ",";
                if (itemCode <= 0)
                    return MessageCode.NbUpdateFail;
                MessageCode code = MessageCode.Success;
                if (currencyCount <= 0)
                    currencyCount = 1;
                code = package.AddItems(itemCode, itemCount, currencyCount, isBinding, isDeal);
              
                if (code != MessageCode.Success)
                    return code;
            }
            itemCodeStr = itemCodeStr.TrimEnd(',');
            return MessageCode.Success;
        }

        MessageCode UseSkillcard(ref TransactionManager tranMgr, Guid managerId, int effectValue, out string outskillId)
        {
            //outskillId = "";
            //var bag = SkillCardConvert.GetSkillBagWrap(managerId);
            //string skillCode = string.Empty;
            //DicSkillcardEntity skillCfg = null;
            //if (!SkillCardCache.Instance().TryRandSkillCode(effectValue, 1, out skillCode)
            //    || !SkillCardCache.Instance().TryGetSkillCard(skillCode, out skillCfg))
            //    return MessageCode.SkillMissConfig;
            //DTOSkillSetItem onItem = null;
            //DTOSkillCardItem newItem = null;
            //if (!bag.SetList.ContainsKey(skillCfg.SkillRoot))
            //{
            //    onItem = SkillCardConvert.GetNewSkillCardOn(skillCfg);
            //    outskillId = onItem.ItemCode;
            //}
            //else
            //{
            //    newItem = SkillCardConvert.GetNewSkillCard(skillCfg);
            //    outskillId = newItem.ItemCode;
            //}
            //string onItemMap = string.Empty;
            //string itemMap = string.Empty;
            //if (null != onItem)
            //    onItemMap = Games.NBall.Bll.Frame.FlatTextFormatter.ItemToText(onItem, null, true, SkillBagWrap.SPLITSect, SkillBagWrap.SPLITUnit);
            //if (null != newItem)
            //    itemMap = Games.NBall.Bll.Frame.FlatTextFormatter.ItemToText(newItem, null, true, SkillBagWrap.SPLITSect, SkillBagWrap.SPLITUnit);
            //string recItemMap = string.Concat(onItemMap, itemMap, "@mall");
            //int errorCode = 0;
            //PrepareTranMgr(ref tranMgr);
            //NbManagerskillbagMgr.Add(managerId, onItemMap, itemMap, recItemMap, bag.RawBag.RowVersion, ref errorCode, tranMgr.TransactionObject);
            //if (errorCode != (int)MessageCode.Success)
            //{
            //    tranMgr.Rollback();
            //    return (MessageCode)errorCode;
            //}
            outskillId = "";
            return MessageCode.Success;
        }

        MessageCode UseAddBuff(ref TransactionManager tranMgr, Guid managerId, int effectValue, int mallCode)
        {
            string skillCode = string.Concat("M", effectValue / 1000);
            int skillLevel = effectValue % 1000;
            string srcId = string.Concat("M", mallCode);
            return Bll.Frame.BuffPoolCore.Instance().AddPools(ref tranMgr, managerId, srcId, skillCode, skillLevel);
        }

        public MessageCode BuildPackMail(int packId, ref MailBuilder mail)
        {
            var packlist = CacheFactory.MallCache.GetNewplayerpacklist(packId);
            if (packlist == null)
                return MessageCode.NbParameterError;
            foreach (var entity in packlist)
            {
                switch (entity.Type)
                {
                    case (int)EnumPrizeItemType.Coin:
                        mail.AddAttachment(EnumCurrencyType.Coin, entity.Count);
                        break;
                    case (int)EnumPrizeItemType.Cardlib:
                        var itemCode = CacheFactory.LotteryCache.LotteryByLib(entity.SubType);
                        var itemCache = CacheFactory.ItemsdicCache.GetItem(itemCode);
                        if (itemCache == null)
                        {
                            return MessageCode.ItemNotExists;
                        }
                        mail.AddAttachment(entity.Count, itemCode, entity.IsBinding, entity.Strength);
                        break;
                    case (int)EnumPrizeItemType.Item:
                        mail.AddAttachment(entity.Count, entity.SubType, entity.IsBinding, entity.Strength);
                        break;
                    case (int)EnumPrizeItemType.Point:
                        mail.AddAttachment(EnumCurrencyType.Point, entity.Count);
                        break;
                    case (int)EnumPrizeItemType.Prestige:
                        mail.AddAttachment(EnumCurrencyType.Prestige, entity.Count);
                        break;
                    default:
                        return MessageCode.NbUpdateFail;
                        break;
                }
            }
            return MessageCode.Success;
        }

        public MessageCode UseNewPlayerPack(Guid managerId, int packId, ItemPackageFrame package, EnumCoinChargeSourceType coinChargeSourceType, ref NbManagerEntity manager, ref int effectCoin, ref int curValue, ref int point, ref int bindPoint, ExchangeEntity exchangeEntity = null)
        {
            curValue = -1;
            effectCoin = 0;
            var packlist = CacheFactory.MallCache.GetNewplayerpacklist(packId);
            if (packlist == null)
                return MessageCode.NbParameterError;
            MessageCode code = MessageCode.Success;
            foreach (var entity in packlist)
            {
                switch (entity.Type)
                {
                    case (int)EnumPrizeItemType.Coin:
                        if (manager == null)
                        {
                            manager = ManagerCore.Instance.GetManager(managerId);
                        }
                        effectCoin += entity.Count;
                        ManagerUtil.AddManagerDataCoin(manager, entity.Count, coinChargeSourceType, packId.ToString());
                        if (exchangeEntity != null)
                        {
                            exchangeEntity.PrizeList.Add(new ExchangePrizeEntity() { Type = 2, Count = effectCoin });
                        }
                        break;
                    case (int)EnumPrizeItemType.Cardlib:
                        var itemCode = CacheFactory.LotteryCache.LotteryByLib(entity.SubType);
                        var itemCache = CacheFactory.ItemsdicCache.GetItem(itemCode);
                        if (itemCache == null)
                        {
                            return MessageCode.ItemNotExists;
                        }
                        if (itemCache.ItemType == (int)EnumItemType.PlayerCard)
                        {
                            code = package.AddPlayerCard(itemCode, 1, entity.IsBinding, entity.Strength,false);
                        }
                        else
                        {
                            code = package.AddItem(itemCode, entity.IsBinding,false);
                        }
                        if (code != MessageCode.Success)
                            return code;
                        if (exchangeEntity != null)
                        {
                            exchangeEntity.PrizeList.Add(new ExchangePrizeEntity() { Type = 3, Count = 1, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = itemCode });
                        }
                        break;
                    case (int)EnumPrizeItemType.Item:
                        code = package.AddItems(entity.SubType, entity.Count, entity.Strength, entity.IsBinding,false);
                        if (code != MessageCode.Success)
                            return code;
                        if (exchangeEntity != null)
                        {
                            exchangeEntity.PrizeList.Add(new ExchangePrizeEntity() { Type = 3, Count = entity.Count, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = entity.SubType });
                        }
                        break;
                    case (int)EnumPrizeItemType.Point:
                        code = MessageCode.Success;
                        point = entity.Count;
                        if (exchangeEntity != null)
                        {
                            exchangeEntity.PrizeList.Add(new ExchangePrizeEntity() { Type = 4, Count = entity.Count, IsBinding = entity.IsBinding, Strength = entity.Strength, ItemCode = entity.SubType });
                        }
                        break;
                    case (int)EnumPrizeItemType.BindPoint:
                        code = MessageCode.Success;
                        bindPoint = entity.Count;
                        if (exchangeEntity != null)
                        {
                            exchangeEntity.PrizeList.Add(new ExchangePrizeEntity()
                            {
                                Type = 9,
                                Count = entity.Count,
                                IsBinding = entity.IsBinding,
                                Strength = entity.Strength,
                                ItemCode = entity.SubType
                            });
                        }
                        break;
                    default:
                        return MessageCode.NbUpdateFail;
                        break;
                }
            }
            return MessageCode.Success;
        }
        #endregion

        #endregion

        #region encapsulation

        private readonly int _mallBuyCountLimit;
        private readonly int _mallBuyPackageCountLimit;
        private readonly int _mallExtraAddStaminaCount;
        private readonly int _mallExtraExpandPackageSize;
        private readonly int _scoutingTenPointMultiple;

        #region Check
        public MessageCode MallCheck(Guid managerId, int count, int currencyType, int payCurrency, out PayUserEntity payUserEntity, out NbManagerEntity manager, bool isFree = false, bool ignoreFlag = false)
        {
            payUserEntity = null;
            manager = null;
            if (count <= 0)
            {
                return MessageCode.NbParameterError;
            }
            if (count > _mallBuyCountLimit)
            {
                return MessageCode.MallBuyCountLimit;
            }
            if (payCurrency <= 0 && !isFree)
            {
                return MessageCode.MallItemInvalidBuy;
            }
            if (ignoreFlag)
                return MessageCode.Success;
            switch (currencyType)
            {
                case (int)EnumCurrencyType.Point:
                    payUserEntity = PayCore.Instance.GetPayUser(managerId);
                    if (payUserEntity.TotalPoint < payCurrency)
                        return MessageCode.NbPointShortage;
                    break;
                case (int)EnumCurrencyType.Coin:
                    manager = ManagerCore.Instance.GetManager(managerId);
                    if (manager.Coin < payCurrency)
                        return MessageCode.NbCoinShortage;
                    break;
                case (int)EnumCurrencyType.BindPoint:
                    payUserEntity = PayCore.Instance.GetPayUser(managerId);
                    if (payUserEntity.BindPoint < payCurrency)
                        return MessageCode.NbBindPointShortage;
                    break;
                case (int)EnumCurrencyType.FriendshipPoint:
                    manager = ManagerCore.Instance.GetManager(managerId);
                    if (manager.FriendShipPoint < payCurrency)
                        return MessageCode.FriendshipPointShortage;
                    break;
                default:
                    return MessageCode.MallItemInvalidBuy;
            }
            return MessageCode.Success;
        }

        MessageCode ExtraCheck(Guid managerId, bool isHint, MallExtrarecordEntity extraEntity, DateTime curTime, out int mallCode, out NbManagerextraEntity managerextraEntity,ref int maxCount,ref int usedCount,bool isBuy=false)
        {
            managerextraEntity = null;
            NbManagerEntity manager = ManagerCore.Instance.GetManager(managerId);
            mallCode = 0;
            switch (extraEntity.ExtraType)
            {
                case (int)EnumMallExtraType.AddStamina:
                    managerextraEntity = ManagerCore.Instance.GetManagerExtra(extraEntity.ManagerId);
                    ManagerUtil.CalCurrentStamina(managerextraEntity, manager.Level,manager.VipLevel);
                    if (!isHint)
                    {
                        if (managerextraEntity.Stamina >= managerextraEntity.StaminaMax)
                        {
                            return MessageCode.MallStaminaOver;
                        }
                    }
                    if (!ExtraCountCheck(extraEntity, manager, true, curTime, (int)EnumVipEffect.AddStaminaCount,ref maxCount,ref usedCount))
                    {
                        if (isBuy)
                            return MessageCode.MallAddStaminaLimit;
                    }
                    break;
                case (int)EnumMallExtraType.AddTrainSeat:
                    if (!ExtraCountCheck(extraEntity, manager, false, curTime, (int)EnumVipEffect.TrainSeatMax, ref maxCount, ref usedCount))
                    {
                        return MessageCode.MallAddTrainSeatLimit;
                    }
                    break;
                case (int)EnumMallExtraType.ExpandPackage:
                    if (extraEntity.UsedCount >= _mallBuyPackageCountLimit)
                        return MessageCode.MallBuyPackageCountLimit;
                    break;
                case (int)EnumMallExtraType.QuickenTrain:
                    //不用验证次数
                    break;
                case (int)EnumMallExtraType.ResetElite:
                    //if (!ExtraCountCheck(extraEntity, manager, true, curTime, (int)EnumVipEffect.EliteResetCount,ref maxCount,ref usedCount))
                    //{
                    //    return MessageCode.MallResetEliteLimit;
                    //}
                    break;
                case (int)EnumMallExtraType.AddSubstitute:
                    //不用验证次数
                    break;
                case (int)EnumMallExtraType.AddPkCount:
                    if (extraEntity.RecordDate != curTime.Date)
                    {
                        extraEntity.RecordDate = curTime.Date;
                        extraEntity.UsedCount = 0;
                    }
                    else
                    {
                        //验证Vip登记对应的次数
                        //var maxTime = PlayerKillCache.Instance.GetVipLevelBuyTimes(manager.VipLevel);
                        //if (maxTime <= extraEntity.UsedCount)
                        //    return MessageCode.PlayerKillBuyTimesOver;
                        if (!ExtraCountCheck(extraEntity, manager, true, curTime, (int)EnumVipEffect.PlayerKillCount, ref maxCount, ref usedCount))
                        {
                            return MessageCode.PlayerKillBuyTimesOver;
                        }
                    }
                    break;
                default:
                    return MessageCode.NbParameterError;
            }
            extraEntity.UsedCount++;
            mallCode = CacheFactory.MallCache.GetExtraMallCode(extraEntity.ExtraType, extraEntity.UsedCount);
            return MessageCode.Success;
        }

        bool ExtraCountCheck(MallExtrarecordEntity extraEntity, NbManagerEntity manager, bool isByDate, DateTime curTime, int vipEffect,ref int maxBuyCount,ref int usedCount)
        {
            if (isByDate && extraEntity.RecordDate != curTime.Date)
            {
                extraEntity.RecordDate = curTime.Date;
                extraEntity.UsedCount = 0;
                return true;
            }
            var maxCount = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, vipEffect);
            //return (manager.TrainSeatMax - 3) < maxCount;
            maxBuyCount = maxCount;
            usedCount = extraEntity.UsedCount;
            if(vipEffect ==(int)EnumVipEffect.AddStaminaCount)
                return extraEntity.UsedCount <= maxCount;
            return extraEntity.UsedCount < maxCount;
        }
        #endregion

        #region BuildSalerecord
        MallSalerecordEntity BuildSalerecord(DicMallItemDataEntity mallEntity, Guid managerId, int count, DateTime curTime)
        {
            return BuildSalerecord(mallEntity, managerId, count, curTime, mallEntity.PackageFlag);
        }

        MallSalerecordEntity BuildSalerecord(DicMallItemDataEntity mallEntity, Guid managerId, DateTime curTime)
        {
            return BuildSalerecord(mallEntity, managerId, 1, curTime, mallEntity.PackageFlag);
        }

        MallSalerecordEntity BuildSalerecord(DicMallItemDataEntity mallEntity, Guid managerId, DateTime curTime, bool packageFlag)
        {
            return BuildSalerecord(mallEntity, managerId, 1, curTime, packageFlag);
        }

        MallSalerecordEntity BuildSalerecord(DicMallItemDataEntity mallEntity, Guid managerId, int count, DateTime curTime, bool packageFlag)
        {
            return BuildSalerecord(mallEntity.MallCode, managerId, mallEntity.CurrencyType, mallEntity.CurrencyCount * count,
                                   mallEntity.RawCurrencyCount * count, count, curTime, packageFlag);
        }

        MallSalerecordEntity BuildSalerecord(int mallCode, Guid managerId, int currencyType, int payCurrency, int rawCurrency, int count, DateTime curTime, bool packageFlag = false)
        {
            var saleRecord = new MallSalerecordEntity();
            saleRecord.Idx = ShareUtil.GenerateComb();
            saleRecord.MallCode = mallCode;
            saleRecord.ManagerId = managerId;
            saleRecord.CurrencyType = currencyType;
            saleRecord.PayCurrency = payCurrency;
            saleRecord.RawCurrency = rawCurrency;
            saleRecord.Qty = count;
            saleRecord.RowTime = curTime;
            saleRecord.PackageFlag = packageFlag;
            saleRecord.Status = 0;
            return saleRecord;
        }
        #endregion

        #region TxSaveBuyItem
        MessageCode TxSaveBuyItem(MallSalerecordEntity salerecord, PayUserEntity payUserEntity, PayChargehistoryEntity chargehistory, PayConsumehistoryEntity consumehistory, ItemPackageFrame package, string zoneId)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(zoneId, EnumDbType.Main)))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = TxTran_SaveBuyItem(transactionManager.TransactionObject, salerecord, payUserEntity, chargehistory, consumehistory, package, zoneId);
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
                SystemlogMgr.Error("TxSaveBuyItem", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode TxTran_SaveBuyItem(DbTransaction transaction, MallSalerecordEntity salerecord, PayUserEntity payUserEntity, PayChargehistoryEntity chargehistory, PayConsumehistoryEntity consumehistory, ItemPackageFrame package, string zoneId)
        {
            if (salerecord == null || payUserEntity == null || chargehistory == null || consumehistory == null)
                return MessageCode.NbUpdateFail;
            if (payUserEntity.IsNew)
            {
                if (!PayUserMgr.Insert(payUserEntity, transaction, zoneId))
                    return MessageCode.NbUpdateFail;
            }
            else
            {
                if (!(PayUserMgr.Update(payUserEntity, transaction, zoneId)))
                    return MessageCode.NbUpdateFail;
            }
            if (!PayChargehistoryMgr.Insert(chargehistory, transaction, zoneId))
                return MessageCode.NbUpdateFail;
            if (!PayConsumehistoryMgr.Insert(consumehistory, transaction, zoneId))
                return MessageCode.NbUpdateFail;
            if (!MallSalerecordMgr.Insert(salerecord, transaction, zoneId))
                return MessageCode.NbUpdateFail;
            if (package != null)
            {
                if (!package.Save(transaction))
                {
                    return MessageCode.NbUpdateFail;
                }
            }
            return MessageCode.Success;
        }
        #endregion

        #region SaveBuyItem

        MessageCode SaveBuyItem(MallSalerecordEntity salerecord, PayUserEntity payUserEntity, NbManagerEntity manager, int currencyType, int payCurrency, ItemPackageFrame package, MallExtrarecordEntity mallExtrarecord)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveBuyItem(transactionManager.TransactionObject, salerecord, payUserEntity, manager, currencyType, payCurrency, package, mallExtrarecord);
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
                SystemlogMgr.Error("SaveBuyItem", ex);
                return MessageCode.Exception;
            }
        }
        #endregion

        #region SaveQuickenTrain
        MessageCode SaveQuickenTrain(MallSalerecordEntity salerecord, PayUserEntity payUserEntity, NbManagerEntity manager, int currencyType, int payCurrency, TeammemberTrainEntity trainEntity, NbManagerextraEntity extra)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.Success;
                    if (salerecord != null)
                    {
                        messageCode = Tran_SaveBuyItem(transactionManager.TransactionObject, salerecord, payUserEntity,
                                                       manager, currencyType, payCurrency, null);
                    }
                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        int returnCode = -2;
                        TeammemberTrainMgr.SaveTrainData(trainEntity, ref returnCode, transactionManager.TransactionObject);
                        if (returnCode == 0)
                        {
                            if (extra != null)
                            {
                                if (NbManagerextraMgr.Update(extra))
                                {
                                    transactionManager.Commit();
                                }
                                else
                                {
                                    transactionManager.Rollback();
                                    messageCode = MessageCode.ExecuteFail;
                                }
                            }
                            else
                            {
                                transactionManager.Commit();
                            }

                        }
                        else
                        {
                            transactionManager.Rollback();
                            messageCode = MessageCode.ExecuteFail;
                        }
                    }
                    return messageCode;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveQuickenTrain", ex);
                return MessageCode.Exception;
            }
        }
        #endregion

        #region SaveScouting
        MessageCode SaveScouting(MallSalerecordEntity salerecord, PayUserEntity payUserEntity, NbManagerEntity manager, int addReiki, int currencyType, int payCurrency, ScoutingRecordEntity scoutingRecord, ItemPackageFrame package, MailBuilder mail, bool isFree = false)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveBuyItem(transactionManager.TransactionObject, salerecord, payUserEntity, manager, currencyType, payCurrency, package, null, isFree);
                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        if (!ScoutingRecordMgr.Insert(scoutingRecord, transactionManager.TransactionObject))
                        {
                            messageCode = MessageCode.NbUpdateFail;
                        }
                        else if (mail != null && !mail.Save(transactionManager.TransactionObject))
                        {
                            messageCode = MessageCode.NbUpdateFail;
                        }
                        if (messageCode == ShareUtil.SuccessCode && addReiki > 0)
                        {
                            int curSophisticate = 0;
                            if (NbManagerMgr.AddReiki(salerecord.ManagerId, addReiki,
                                ref curSophisticate,
                                transactionManager.TransactionObject))
                            {
                                messageCode = MessageCode.Success;
                            }
                            else
                            {
                                messageCode = MessageCode.NbUpdateFail;
                            }
                        }

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
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveScouting", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode SaveScouting(ScoutingGoldbarEntity scoutingManager,ScoutingRecordEntity scoutingRecord, ItemPackageFrame package)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode =  MessageCode.NbUpdateFail;
                    do
                    {
                        if (!ScoutingRecordMgr.Insert(scoutingRecord, transactionManager.TransactionObject))
                            break;
                        if (!ScoutingGoldbarMgr.Update(scoutingManager, transactionManager.TransactionObject))
                            break;
                        if (package != null)
                        {
                            if (!package.Save(transactionManager.TransactionObject))
                                break;
                            package.Shadow.Save();
                        }
                        messageCode = MessageCode.Success;
                    } while (false);
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
                SystemlogMgr.Error("SaveScouting", ex);
                return MessageCode.Exception;
            }
        }
        #endregion

        #region SaveEquipmentWash
        MessageCode SaveEquipmentWash(MallSalerecordEntity salerecordStone, MallSalerecordEntity salerecordFusogen, MallSalerecordEntity salerecordLockProperty, PayUserEntity payUserEntity, int payCurrency, ItemPackageFrame package, bool lockSkill)
        {
            if (package == null)
                return MessageCode.NbParameterError;
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveEquipmentWash(transactionManager.TransactionObject, salerecordStone, salerecordFusogen, salerecordLockProperty, payUserEntity, payCurrency, package, lockSkill);
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
                SystemlogMgr.Error("SaveEquipmentWash", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_SaveEquipmentWash(DbTransaction transaction, MallSalerecordEntity salerecordStone, MallSalerecordEntity salerecordFusogen, MallSalerecordEntity salerecordLockProperty, PayUserEntity payUserEntity, int payCurrency, ItemPackageFrame package, bool lockSkill)
        {
            string orderId = "";
            if (lockSkill)
                orderId = Guid.NewGuid().ToString();

            if (salerecordStone != null)
            {
                orderId = salerecordStone.Idx.ToString();
                if (!MallSalerecordMgr.Insert(salerecordStone, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (salerecordFusogen != null)
            {
                orderId = salerecordFusogen.Idx.ToString();
                if (!MallSalerecordMgr.Insert(salerecordFusogen, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (salerecordLockProperty != null)
            {
                orderId = salerecordLockProperty.Idx.ToString();
                if (!MallSalerecordMgr.Insert(salerecordLockProperty, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (payCurrency > 0)
            {
                if (string.IsNullOrEmpty(orderId))
                    return MessageCode.NbUpdateFail;
                int returnCode = 0;
                PayUserMgr.ConsumePoint(payUserEntity.Account, package.ManagerId, (int)EnumConsumeSourceType.Mall, orderId, payCurrency, DateTime.Now,
                                        payUserEntity.RowVersion, ref returnCode, transaction);
                if (returnCode != (int)MessageCode.PaySuccess)
                    return MessageCode.NbPointShortage;
            }
            if (!package.Save(transaction))
                return MessageCode.NbUpdateFail;
            package.Shadow.Save();
            return MessageCode.Success;
        }
        #endregion

        #region SaveBuyExtraItem
        MessageCode SaveBuyExtraItem(MallSalerecordEntity salerecord, PayUserEntity payUserEntity, NbManagerEntity manager, int currencyType, int payCurrency, ItemPackageFrame package, MallExtrarecordEntity extraEntity, NbManagerextraEntity managerextraEntity, ref int extraResultValue)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveBuyItem(transactionManager.TransactionObject, salerecord, payUserEntity, manager, currencyType, payCurrency, package);
                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        messageCode = Tran_SaveExtra(transactionManager.TransactionObject, extraEntity, managerextraEntity,
                                                     ref extraResultValue);
                        if (messageCode == ShareUtil.SuccessCode)
                        {
                            transactionManager.Commit();
                        }
                        else
                        {
                            transactionManager.Rollback();
                        }
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
                SystemlogMgr.Error("SaveBuyExtraItem", ex);
                return MessageCode.Exception;
            }
        }
        #endregion

        #region Tran_SaveBuyItem

        private MessageCode Tran_SaveBuyItem(DbTransaction transaction, MallSalerecordEntity salerecord,
            PayUserEntity payUserEntity, NbManagerEntity manager, int currencyType, int payCurrency,
            ItemPackageFrame package, MallExtrarecordEntity mallExtrarecord = null, bool isFree = false)
        {
            return Tran_SaveBuyItem(transaction, salerecord.ManagerId, EnumConsumeSourceType.Mall, salerecord, payUserEntity, manager, currencyType,
                payCurrency, package, mallExtrarecord, isFree);
        }

        MessageCode Tran_SaveBuyItem(DbTransaction transaction, Guid managerId, EnumConsumeSourceType sourceType, MallSalerecordEntity salerecord, PayUserEntity payUserEntity, NbManagerEntity manager, int currencyType, int payCurrency, ItemPackageFrame package, MallExtrarecordEntity mallExtrarecord = null, bool isFree = false)
        {
            int returnCode = -2;
            if (currencyType == (int)EnumCurrencyType.Point)
            {
                if (!isFree)
                {
                    var orderId = salerecord == null ? Guid.NewGuid().ToString() : salerecord.Idx.ToString();
                    PayUserMgr.ConsumePoint(payUserEntity.Account, managerId,
                        (int)sourceType, orderId, payCurrency, DateTime.Now,
                        payUserEntity.RowVersion, ref returnCode, transaction);
                    if (returnCode != (int)MessageCode.PaySuccess)
                        return MessageCode.NbUpdateFail;
                }
            }
            else if (currencyType == (int)EnumCurrencyType.BindPoint)
            {
                if (payUserEntity.BindPoint < payCurrency)
                    return MessageCode.NbBindPointShortage;
                payUserEntity.BindPoint -= payCurrency;
                if (!PayUserMgr.Update(payUserEntity, transaction))
                    return MessageCode.NbUpdateFail;
            }
            else if (currencyType == (int)EnumCurrencyType.FriendshipPoint)
            {
                if (!isFree)
                {
                    var code = ManagerCore.Instance.UpdateFriendShipPoint(manager, payCurrency, EnumActionType.Minus,
                        transaction);
                    if (code != MessageCode.Success)
                    {
                        return code;
                    }
                }
            }
            else
            {
                if (!isFree)
                {
                    var code = ManagerCore.Instance.CostCoin(manager, payCurrency, EnumCoinConsumeSourceType.MallBuy,
                        salerecord.Idx.ToString(), transaction);
                    if (code != MessageCode.Success)
                    {
                        return code;
                    }
                }
            }

            if (salerecord != null)
            {
                if (!MallSalerecordMgr.Insert(salerecord, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (package != null)
            {
                if (!package.Save(transaction))
                {
                    return MessageCode.NbUpdateFailPackage;
                }
                package.Shadow.Save();
            }
            if (mallExtrarecord != null)
            {
                if (!MallExtrarecordMgr.Update(mallExtrarecord, transaction))
                    return MessageCode.NbUpdateFail;
            }
            //免费的
            if (isFree)
            {
                //更新下次免费时间
                if (currencyType == (int)EnumCurrencyType.Coin)
                {
                    if (!NbManagerextraMgr.UpdateCoinScouting(managerId, DateTime.Now.AddDays(1), transaction))
                        return MessageCode.NbUpdateFail;
                }
                else if (currencyType == (int)EnumCurrencyType.FriendshipPoint)
                {
                    if (!NbManagerextraMgr.UpdateFriendScouting(managerId, DateTime.Now.AddDays(1), transaction))
                        return MessageCode.NbUpdateFail;
                }
                else if (currencyType == (int)EnumCurrencyType.Point)
                {
                    if (!NbManagerextraMgr.UpdateScouting(managerId, DateTime.Now.AddDays(1), transaction))
                        return MessageCode.NbUpdateFail;
                }
            }
            return MessageCode.Success;
        }
        #endregion

        #region Tran_SaveExtra
        MessageCode Tran_SaveExtra(DbTransaction transaction, MallExtrarecordEntity extraEntity, NbManagerextraEntity managerExtra, ref int extraResultValue)
        {
            switch (extraEntity.ExtraType)
            {
                case (int)EnumMallExtraType.AddStamina:
                    managerExtra.Stamina += _mallExtraAddStaminaCount;
                    MallExtrarecordMgr.ExtraAddStamina(extraEntity.ManagerId, managerExtra.ResumeStaminaTime, managerExtra.Stamina, transaction);
                    extraResultValue = managerExtra.Stamina;
                    break;
                case (int)EnumMallExtraType.AddTrainSeat:
                    MallExtrarecordMgr.ExtraAddTrainseat(extraEntity.ManagerId, ref extraResultValue, transaction);
                    ManagerCore.Instance.DeleteCache(extraEntity.ManagerId);
                    break;
                case (int)EnumMallExtraType.ExpandPackage:
                    MallExtrarecordMgr.ExtraExpandPackage(extraEntity.ManagerId, _mallExtraExpandPackageSize, ref extraResultValue, transaction);
                    break;
                case (int)EnumMallExtraType.ResetElite:
                    MallExtrarecordMgr.ExtraResetElite(extraEntity.ManagerId, extraEntity.RecordDate, transaction);
                    break;
                case (int)EnumMallExtraType.AddSubstitute:
                    MallExtrarecordMgr.ExtraAddSubstitute(extraEntity.ManagerId, ref extraResultValue, transaction);
                    ManagerCore.Instance.DeleteCache(extraEntity.ManagerId);
                    break;
                case (int)EnumMallExtraType.AddPkCount:
                    MallExtrarecordMgr.ExtraAddPkCount(extraEntity.ManagerId, ref extraResultValue, transaction);
                    var manager = ManagerCore.Instance.GetManager(extraEntity.ManagerId);
                    if (manager == null)
                        return MessageCode.NbParameterError;
                    var info= PlayerKillCore.Instance.InnerGetInfo(extraEntity.ManagerId);
                    extraResultValue = CacheFactory.PlayerKillCache.GetChallengeTimes(manager.Level) - info.RemainTimes + info.BuyTimes;
                    break;
                default:
                    return MessageCode.NbParameterError;
            }
            if (MallExtrarecordMgr.Update(extraEntity, transaction))
            {
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbUpdateFail;
            }
        }
        #endregion

        #region SavePandora
        MessageCode SavePandora(Guid managerId, EnumConsumeSourceType sourceType, ItemPackageFrame package, NbManagerEntity manager, PayUserEntity payUserEntity, NbManagerextraEntity extra, MallSalerecordEntity salerecord, int costCoin, int costPoint, TeammemberEntity teammember = null)
        {
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = MessageCode.Success;
                    if (costPoint > 0)
                    {
                        messageCode = Tran_SaveBuyItem(transactionManager.TransactionObject, managerId, sourceType, salerecord, payUserEntity, manager, (int)EnumCurrencyType.Point, costPoint, null);
                        if (messageCode != MessageCode.Success)
                        {
                            transactionManager.Rollback();
                            return messageCode;
                        }
                    }

                    messageCode = Tran_SavePandora(transactionManager.TransactionObject, package, manager, extra, costCoin, teammember);
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
                SystemlogMgr.Error("SavePandora", ex);
                return MessageCode.Exception;
            }
        }
        #endregion

        #region Tran_SavePandora
        MessageCode Tran_SavePandora(DbTransaction transaction, ItemPackageFrame package, NbManagerEntity manager, NbManagerextraEntity extra, int costCoin, TeammemberEntity teammember = null)
        {
            if (package == null)
                return MessageCode.NbUpdateFail;
            if (costCoin > 0)
            {
                var code = ManagerCore.Instance.CostCoin(manager, costCoin, EnumCoinConsumeSourceType.MallBuy, "", transaction);
                if (code != MessageCode.Success)
                {
                    return code;
                }
            }
            if (teammember != null)
            {
                int returnCode = 0;
                string errorMessage = "";
                TeammemberMgr.SetPlayerCard(teammember.Idx, teammember.ManagerId, manager.Mod,
                    teammember.UsedPlayerCard, package.NewItemString,
                    package.RowVersion, ref returnCode, ref errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    SystemlogMgr.Error("SaveSetPlayerCard", errorMessage);
                }
                if (returnCode != ShareUtil.SuccessCode)
                    return (MessageCode)returnCode;
                package.Shadow.Save();
                MatchDataCacheHelper.DeleteTeamembersCache(teammember.ManagerId, true);
            }
            else
            {
                if (!package.Save(transaction))
                {
                    return MessageCode.NbUpdateFail;
                }
                package.Shadow.Save();
            }
            if (extra != null)
            {
                if (!NbManagerextraMgr.Update(extra, transaction))
                    return MessageCode.NbUpdateFail;
            }
            return MessageCode.Success;
        }
        #endregion

        #region SaveUseItem
        public MessageCode SaveUseItem(ItemPackageFrame package, NbManagerEntity manager, NbManagerextraEntity managerextra,   List<TeammemberTrainEntity> trainList = null)
        {
            return SaveUseItem(null, package, manager, managerextra, null, 0,0, 0, trainList);
        }
        public MessageCode SaveUseItem(TransactionManager tranMgr, ItemPackageFrame package, NbManagerEntity manager, NbManagerextraEntity managerextra, MallExtrarecordEntity mallExtra, int point, int addLuckyCoin, int bindPoint = 0, List<TeammemberTrainEntity> trainList = null, LadderManagerEntity ladderManager = null, OlympicManagerEntity olympicManager = null, int addCoachExp = 0)
        {
            try
            {
                PrepareTranMgr(ref tranMgr);
                using (tranMgr)
                {
                    //tranMgr.BeginTransaction();
                    var messageCode = Tran_SaveUseItem(tranMgr.TransactionObject, package, manager, managerextra, mallExtra, point, addLuckyCoin, trainList, bindPoint, ladderManager, olympicManager, addCoachExp);
                    if (messageCode == MessageCode.Success)
                    {
                        tranMgr.Commit();
                    }
                    else
                    {
                        tranMgr.Rollback();
                    }
                    return messageCode;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveUseItem", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_SaveUseItem(DbTransaction transaction, ItemPackageFrame package, NbManagerEntity manager, NbManagerextraEntity managerextra, MallExtrarecordEntity mallExtra, int point, int addLuckyCoin, List<TeammemberTrainEntity> trainList, int bindPoint = 0, LadderManagerEntity ladderManager = null, OlympicManagerEntity olympicManager = null, int addCoachExp = 0)
        {
            if (!package.Save(transaction))
                return MessageCode.NbUpdateFail;
            if (manager != null)
            {
                if (!ManagerUtil.SaveManagerData(manager, managerextra, transaction))
                {
                    return MessageCode.NbUpdateFail;
                }
            }
            if (managerextra != null)
            {
                if (!NbManagerextraMgr.Update(managerextra, transaction))
                {
                    return MessageCode.NbUpdateFail;
                }
            }
            
            if (trainList != null && trainList.Count > 0)
            {
                foreach (var trainEntity in trainList)
                {
                    int returnCode = -2;
                   // TeammemberTrainMgr.SaveTrainData(trainEntity, ref returnCode, transaction);
                    if (returnCode != 0)
                    {
                        return MessageCode.NbUpdateFail;
                    }
                }
            }
            if (mallExtra != null)
            {
                if (!MallExtrarecordMgr.Update(mallExtra, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (point > 0)
            {
                if (manager == null)
                    manager = ManagerCore.Instance.GetManager(package.ManagerId);
                var code = PayCore.Instance.AddBonus(manager.Account, point, EnumChargeSourceType.ActivityPrize,
                                          package.Shadow.TransactionId.ToString(), transaction);
                if (code != MessageCode.Success)
                    return code;
            }
            if (bindPoint > 0)
            {
                if (manager == null)
                    manager = ManagerCore.Instance.GetManager(package.ManagerId);
                var code = PayCore.Instance.AddBindPoint(manager.Account, bindPoint, EnumChargeSourceType.ActivityPrize,
                    package.Shadow.TransactionId.ToString(), transaction);
                if (code != MessageCode.Success)
                    return code;
            }
            if (ladderManager != null)
            {
                if (!LadderManagerMgr.Update(ladderManager))
                    return MessageCode.FailUpdate;
            }
            if (olympicManager != null)
            {
                if (!OlympicManagerMgr.Update(olympicManager, transaction))
                    return MessageCode.FailUpdate;
            }
            if (addLuckyCoin > 0)
            {
                if (!TurntableManagerMgr.AddLuckyCoin(package.ManagerId, addLuckyCoin, transaction))
                    return MessageCode.FailUpdate;
            }
            if (addCoachExp > 0)
            {
                if (!CoachManagerMgr.AddExp(package.ManagerId, addCoachExp, transaction))
                    return MessageCode.FailUpdate;
            }
            return MessageCode.Success;
        }
        #endregion

        #region CalCurCurrency
        static int CalCurCurrency(int currencyType, PayUserEntity payUserEntity, int payCurrency, NbManagerEntity manager, EnumCoinConsumeSourceType consumeSourceType, MallSalerecordEntity salerecord)
        {
            if (currencyType == (int)EnumCurrencyType.Point)
                return payUserEntity.TotalPoint - payCurrency;
            else if (currencyType == (int)EnumCurrencyType.BindPoint)
            {
                return payUserEntity.BindPoint;
            }
            else if (currencyType == (int)EnumCurrencyType.Coin)
            {
                if (payCurrency > 0)
                {
                    ShadowMgr.SaveCoinConsume(manager.Idx, payCurrency, consumeSourceType, salerecord.OrderId);
                }
                return manager.Coin;
            }
            else
            {
                return manager.FriendShipPoint;
            }
        }
        #endregion

        #region PrepareTranMgr
        void PrepareTranMgr(ref TransactionManager tranMgr)
        {
            if (null == tranMgr)
                tranMgr = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault());
            if (!tranMgr.IsOpen)
                tranMgr.BeginTransaction();
        }
        #endregion
        #endregion
        /// <summary>
        /// 金牌球探抽卡是否可以交易
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public bool IsHaveDeal(int itemCode)
        {
            bool isDeal = false;
            var item = CacheFactory.ItemsdicCache.GetItem(itemCode);
            if (item != null && item.ItemType == (int) EnumItemType.MallItem)
            {
                var player = CacheFactory.PlayersdicCache.GetPlayer(item.ImageId);
                if (player == null)
                    return false;
                if (player.KpiLevel == "S" || player.KpiLevel == "S+")
                {
                    if (RandomHelper.CheckPercentage(20))
                    {
                        if (IsHaveDeal())
                            isDeal = true;
                    }
                }
            }
            return isDeal;
        }

        /// <summary>
        /// 金牌球探抽卡是否可以交易
        /// </summary>
        public bool IsHaveDeal()
        {
            var domainId = CacheFactory.ArenaCache.GetDomainIdToInt(ShareUtil.ZoneName);
            var info = TransferDropoutMgr.GetById(domainId);
            DateTime date = DateTime.Now;
            if (info == null)
                return false;
            if (info.RefreshTiem.Date != date.Date)
            {
                info.DropOutNumber = _dropOutNumber;
                info.RefreshTiem = date;
            }
            if (info.DropOutNumber < 1)
                return false;
            info.DropOutNumber--;
            info.UpdateTime = date;
            if (!TransferDropoutMgr.Update(info))
                return false;
            return true;
        }


    }
}
