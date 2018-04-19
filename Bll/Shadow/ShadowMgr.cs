using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Dal.Shadow;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Bll.Shadow
{
    public class ShadowMgr
    {

        public static bool SaveCoinConsume(Guid managerId, int coin, EnumCoinConsumeSourceType consumeSourceType,
                                           string orderId)
        {
            return SaveCoinConsume(managerId, coin, (int)consumeSourceType, orderId);
        }

        /// <summary>
        /// 保存金币消耗记录
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coin"></param>
        /// <param name="consumeSourceType"></param>
        /// <param name="orderId">用于关联源记录参考</param>
        /// <returns></returns>
        public static bool SaveCoinConsume(Guid managerId, int coin, int consumeSourceType, string orderId)
        {
            if (!CacheFactory.AppsettingCache.ShadowCoin)
                return true;
            try
            {
                var provider = new ShadowProvider();
                //统计金币
                StatisticKpiMgr.UpdateSame(ShareUtil.ZoneId, DateTime.Now.Date, 0, 0, coin, 0);
                return provider.SaveCoinConsume(managerId, coin, consumeSourceType, orderId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveCoinConsume", ex);
                return false;
            }
        }

        public static bool SaveCoinCharge(Guid managerId, int coin, int exp, bool isLevelup, int level, EnumCoinChargeSourceType chargeSourceType,
                                          string orderId)
        {
            return SaveCoinCharge(managerId, coin, exp, isLevelup, level, (int)chargeSourceType, orderId);
        }

        /// <summary>
        /// 保存金币增加记录
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coin"></param>
        /// <param name="chargeSourceType"></param>
        /// <param name="orderId">用于关联源记录参考</param>
        /// <returns></returns>
        public static bool SaveCoinCharge(Guid managerId, int coin, int exp, bool isLevelup, int level, int chargeSourceType, string orderId)
        {
            if (!CacheFactory.AppsettingCache.ShadowCoin)
                return true;
            try
            {
                //统计金币
                StatisticKpiMgr.UpdateSame(ShareUtil.ZoneId, DateTime.Now.Date, 0, 0,0,coin);
                var provider = new ShadowProvider();
                return provider.SaveCoinCharge(managerId, coin, exp, isLevelup, level, chargeSourceType, orderId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveCoinCharge", ex);
                return false;
            }
        }

        public static bool SaveOnlineHistory(OnlineInfoEntity entity)
        {
            try
            {
                var provider = new ShadowProvider();
                return provider.SaveOnlineHistory(entity);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveOnlineHistory", ex);
                return false;
            }
        }

        public static void CoinStat(Guid managerId, ref int chargeCoin, ref int consumeCoin, string zoneId)
        {
            try
            {
                var provider = new ShadowProvider(zoneId);
                provider.CoinStat(managerId, ref chargeCoin, ref consumeCoin);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CoinStat", ex);
            }
        }

        public static void CoinAllStat(DateTime startTime, DateTime endTime, ref int chargeCoin, ref int consumeCoin,string zoneId)
        {
            try
            {
                var provider = new ShadowProvider(zoneId);
                provider.CoinAllStat(startTime, endTime, ref chargeCoin, ref consumeCoin);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CoinAllStat", ex);
            }
        }
    }
}
