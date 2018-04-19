using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Dpm.Core.Activity;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Friend;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.SkillCard;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Information;
using Games.NBall.Entity.Response.SkillCard;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core
{
    public class CrossActivityThread
    {
      
        /// <summary>
        /// 当前域
        /// </summary>
        private int _domainId;

        private CrossactivityMainEntity _ActivityInfo;

        /// <summary>
        /// 是否有活动
        /// </summary>
        public bool IsActivity
        {
            get
            {
                if (_ActivityInfo == null)
                    return false;
                DateTime date = DateTime.Now;
                return date >= _ActivityInfo.StartTime && date <= _ActivityInfo.EndTime;
            }
        }
        /// <summary>
        /// 欧洲杯装备碎片
        /// </summary>
        private List<int> _europeEquipmentDebris;
        /// <summary>
        /// 恢复间隔   分
        /// </summary>
        private int _RecoverInterval = 60;
        /// <summary>
        /// 恢复多少
        /// </summary>
        private int _RecoverValue = 150;
        public CrossActivityThread(int domainId)
        {
            _domainId = domainId;
            _ActivityInfo = CrossactivityMainMgr.GetActivityInfo(_domainId);
            _RecoverInterval = CacheFactory.AppsettingCache.GetAppSettingToInt(
                EnumAppsetting.CrossActivityRecoverInterval, 60);

            _RecoverValue = CacheFactory.AppsettingCache.GetAppSettingToInt(
                EnumAppsetting.CrossActivityRecoverValue, 150);

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

        public MessageCode Refresh()
        {
            if (_ActivityInfo == null)
                return MessageCode.Success;
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts2 = new TimeSpan(_ActivityInfo.GoldBarRefresh.Ticks);
            int m = ts1.Subtract(ts2).Minutes;
            if (m > _RecoverInterval)
            {
                int number = m/_RecoverInterval;
                _ActivityInfo.GoldBarRefresh = _ActivityInfo.GoldBarRefresh.AddHours(number);
                _ActivityInfo.GoldBarNumber = _ActivityInfo.GoldBarNumber + (_RecoverValue*number);
            }
            _ActivityInfo.UpdateTime = DateTime.Now;
            if (!CrossactivityMainMgr.Update(_ActivityInfo))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }

        /// <summary>
        /// 获取活动信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public CrossActivityInfoResponse GetActivityInfo(Guid managerId, string zoneName)
        {
            CrossActivityInfoResponse response = new CrossActivityInfoResponse();
            response.Data = new CrossActivityInfo();
            try
            {
                if (!IsActivity)
                {
                    response.Data.IsOpen = false;
                    return response;
                }
                var managerInfo = NbManagerMgr.GetById(managerId, zoneName);
                if (managerInfo == null)
                    return ResponseHelper.Create<CrossActivityInfoResponse>(MessageCode.NbParameterError);
                var maxCount = CacheFactory.VipdicCache.GetEffectValue(managerInfo.VipLevel,
                    (int) EnumVipEffect.BeThankfulActivity);
                var usedNumber = CrossactivityRecordMgr.GetActivityNumber(managerId, DateTime.Now.Date);
                response.Data.MaxNumber = maxCount;
                response.Data.UsedNumber = usedNumber;
                response.Data.GoldBar = _ActivityInfo.GoldBarNumber;
                //response.Data.PrizeList = CacheFactory.TurntableCache.GetAllPrize();
                response.Data.IsOpen = true;
                response.Data.ActivityEnd = ShareUtil.GetTimeTick(_ActivityInfo.EndTime);
                response.Data.ActivityStart = ShareUtil.GetTimeTick(_ActivityInfo.StartTime);
                response.Data.IsPrompt = maxCount > usedNumber;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取跨服活动信息", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public CrossActivityPrizeResponse Prize(Guid managerId, string zoneName)
        {
            CrossActivityPrizeResponse response = new CrossActivityPrizeResponse();
            response.Data = new CrossActivityPrize();
            try
            {
                if (!IsActivity)
                    return ResponseHelper.Create<CrossActivityPrizeResponse>(MessageCode.CrowdNoData);
                var managerInfo = NbManagerMgr.GetById(managerId, zoneName);
                if (managerInfo == null)
                    return ResponseHelper.Create<CrossActivityPrizeResponse>(MessageCode.NbParameterError);
                var maxCount = CacheFactory.VipdicCache.GetEffectValue(managerInfo.VipLevel,
                    (int)EnumVipEffect.BeThankfulActivity);
                var usedNumber = CrossactivityRecordMgr.GetActivityNumber(managerId, DateTime.Now.Date);
                if (maxCount <= usedNumber)
                    return ResponseHelper.Create<CrossActivityPrizeResponse>(MessageCode.DayNumberNot);
                var result = Prize(managerInfo.VipLevel);
                CrossactivityRecordEntity entity = new CrossactivityRecordEntity(0, managerId, zoneName, result.PrizeId,
                    DateTime.Now);
                ItemPackageFrame package = null;
                int addPoint = 0;
                int addGoldBar = 0;
                int itemCode = 0;
                int itemCount = 0;
                var messageCode = AddPrize(result, managerId, zoneName, ref package, ref addPoint, ref addGoldBar, ref itemCode, ref itemCount);
                if(messageCode!= MessageCode.Success)
                    return ResponseHelper.Create<CrossActivityPrizeResponse>(messageCode);
                if (addPoint > 0)
                {
                    messageCode = PayCore.Instance.AddBonus(managerInfo.Account, addPoint, EnumChargeSourceType.CrossActivity,
                        ShareUtil.GenerateComb().ToString(),null,zoneName);
                    if (messageCode != MessageCode.Success)
                        return ResponseHelper.Create<CrossActivityPrizeResponse>(messageCode);
                }
                else if (package != null)
                {
                    if (!package.Save())
                        return ResponseHelper.Create<CrossActivityPrizeResponse>(MessageCode.NbUpdateFail);
                    package.Shadow.Save();
                }
                else if (addGoldBar > 0)
                {
                    var goldBarManager = ScoutingGoldbarMgr.GetById(managerId, zoneName);
                    if (goldBarManager == null)
                    {
                        goldBarManager = new ScoutingGoldbarEntity(managerId, addGoldBar, 0, 0, 0, DateTime.Now,
                            DateTime.Now);
                        if (!ScoutingGoldbarMgr.Insert(goldBarManager, null, zoneName))
                            return ResponseHelper.Create<CrossActivityPrizeResponse>(MessageCode.NbUpdateFail);
                    }
                    else
                    {
                        goldBarManager.GoldBarNumber = goldBarManager.GoldBarNumber + addGoldBar;
                        if (!ScoutingGoldbarMgr.Update(goldBarManager, null, zoneName))
                            return ResponseHelper.Create<CrossActivityPrizeResponse>(MessageCode.NbUpdateFail);
                    }
                }
                try
                {
                    CrossactivityRecordMgr.Insert(entity);
                }
                catch
                {
                }

                response.Data.MaxNumber = maxCount;
                response.Data.PrizeId = result.PrizeId;
                response.Data.UsedNumber = usedNumber + 1;
                response.Data.Number = itemCount;
                response.Data.ItemCode =itemCode;

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("抽奖", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        public MessageCode AddPrize(ConfigCrossactivityprizeEntity prize,Guid managerId,string zoneName,ref ItemPackageFrame package,ref int addPoint,ref int addGoldBar,ref int itemCode,ref int itemCount)
        {
            switch (prize.PrizeType)
            {
                case (int)EnumCrossActivityType.Point:
                    itemCount = RandomHelper.GetInt32(prize.PrizeCount, prize.PrizeCount2);
                    addPoint = itemCount;
                    break;
                case (int)EnumCrossActivityType.Coin:
                    break;
                case (int)EnumCrossActivityType.Item:
                    if (package == null)
                        package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CrossActivity, zoneName);
                    if (package == null)
                        return MessageCode.NbParameterError;
                    itemCode = prize.PrizeCode;
                    itemCount = prize.PrizeCount;
                    return package.AddItems(prize.PrizeCode, prize.PrizeCount);
                case (int)EnumCrossActivityType.Random:
                    itemCode = CacheFactory.LotteryCache.LotteryByLib(prize.PrizeCode);
                    if (itemCode == 0)
                        return MessageCode.NbParameterError;
                    if (package == null)
                        package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CrossActivity, zoneName);
                    if (package == null)
                        return MessageCode.NbParameterError;
                    itemCount = prize.PrizeCount;
                    return package.AddItems(itemCode, prize.PrizeCount);
                case (int)EnumCrossActivityType.Equipment:
                    itemCode =  GetRandomDebris();;
                    if (itemCode == 0)
                        return MessageCode.NbParameterError;
                    if (package == null)
                        package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.CrossActivity, zoneName);
                    if (package == null)
                        return MessageCode.NbParameterError;
                    itemCount = prize.PrizeCount;
                    return package.AddItems(itemCode, prize.PrizeCount);
                case (int)EnumCrossActivityType.GoleBar:
                    itemCount = RandomHelper.GetInt32(prize.PrizeCount, prize.PrizeCount2);
                    addGoldBar = itemCount;
                    _ActivityInfo.GoldBarNumber = _ActivityInfo.GoldBarNumber - addGoldBar;
                    if (_ActivityInfo.GoldBarNumber < 0)
                        _ActivityInfo.GoldBarNumber = 0;
                    break;
            }
            return MessageCode.Success;
        }

        public ConfigCrossactivityprizeEntity Prize(int vipLevel)
        {
            ConfigCrossactivityprizeEntity result = null;
            int rate = 0;
            if (vipLevel < 2)
                rate = RandomHelper.GetInt32(801, 10000);
            else
                rate = RandomHelper.GetInt32(0, 10000);
            int index = 20;
            do
            {
                var prizeId = CacheFactory.TurntableCache.Prize(rate);
                result = CacheFactory.TurntableCache.GetPrize(prizeId);
                if (result != null)
                {
                    if (result.PrizeType == (int)EnumCrossActivityType.GoleBar)
                    {
                        if (_ActivityInfo.GoldBarNumber < result.PrizeCount)
                        {
                            rate = RandomHelper.GetInt32(801, 10000);
                        }
                        else
                            break;
                    }
                    else
                        break;
                }
                index --;
            } while (index >0);
            return result;
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

    internal enum EnumCrossActivityType
    {
        /// <summary>
        /// 钻石
        /// </summary>
        Point = 1,

        /// <summary>
        /// 金币
        /// </summary>
        Coin = 2,

        /// <summary>
        /// 指定物品
        /// </summary>
        Item = 3,

        /// <summary>
        /// 随机物品
        /// </summary>
        Random = 4,
        /// <summary>
        /// 装备碎片
        /// </summary>
        Equipment=5,

        /// <summary>
        /// 金条
        /// </summary>
        GoleBar = 12,
    }
}
