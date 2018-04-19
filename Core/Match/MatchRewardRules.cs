using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using MsEntLibWrapper.Data;
using Games.NBall.Core.Manager;


namespace Games.NBall.Core.Match
{
    public class MatchRewardRules
    {
        #region Cache
        static int MATCHRewardMaxCoin = 30;
        static int MATCHRewardMaxPoint = 1;
        static int MATCHRewardMaxGetTimes = 2;
        static int MATCHRewardMaxSetTimes = 4;
        #endregion
        static MatchRewardRules()
        {
            try
            {
                MATCHRewardMaxCoin = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.MATCHRewardMaxCoin, MATCHRewardMaxCoin);
                MATCHRewardMaxPoint = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.MATCHRewardMaxPoint, MATCHRewardMaxPoint);
                MATCHRewardMaxGetTimes = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.MATCHRewardMaxGetTimes, MATCHRewardMaxGetTimes);
                MATCHRewardMaxSetTimes = AppsettingCache.Instance.GetAppSettingToInt(EnumAppsetting.MATCHRewardMaxSetTimes, MATCHRewardMaxSetTimes);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("MatchRewardRules:Init", ex);
            }
        }

        public static MessageCode CheackReward(Guid managerId, int matchType, Guid matchId)
        {
            if (!ShareUtil.IsAppRXYC)
                return MessageCode.MatchRewardLimitApp;
            if (!MatchThread.CheckRewardMatchType(matchType))
                return MessageCode.MatchRewardLimitType;
            string key = string.Concat(managerId, ".", matchType).ToLower();
            var state = MemcachedFactory.MatchRewardClient.Get<DTOMatchRewardState>(key);
            if (null == state || state.MatchId != matchId)
                return MessageCode.MatchRewardMissMatch;
            if (state.GetTimes == MATCHRewardMaxGetTimes - 1)
                MemcachedFactory.MatchRewardClient.Delete(key);
            else
            {
                state.GetTimes++;
                MemcachedFactory.MatchRewardClient.Set(key, state);
            }
            return MessageCode.Success;
        }

        public static RootResponse<DTOAssetInfo> CommitReward(Guid managerId, int matchType, Guid matchId, string mask, string sig)
        {
            if (!ShareUtil.IsAppRXYC)
                return ResponseHelper.CreateRoot<DTOAssetInfo>(MessageCode.MatchRewardLimitApp);
            if (!MatchThread.CheckRewardMatchType(matchType))
                return ResponseHelper.CreateRoot<DTOAssetInfo>(MessageCode.MatchRewardLimitType);
            int coin = 0;
            int point = 0;
            ParseMask(out coin, out point, mask);
            if (coin < 0 || coin > MATCHRewardMaxCoin)
                return ResponseHelper.CreateRoot<DTOAssetInfo>(MessageCode.MatchRewardOverCoin);
            if (point < 0 || point > MATCHRewardMaxPoint)
                return ResponseHelper.CreateRoot<DTOAssetInfo>(MessageCode.MatchRewardOverPoint);
            string key = string.Concat(managerId, ".", matchType).ToLower();
            var state = MemcachedFactory.MatchRewardClient.Get<DTOMatchRewardState>(key);
            if (null == state || state.MatchId != matchId)
                return ResponseHelper.CreateRoot<DTOAssetInfo>(MessageCode.MatchRewardMissMatch);
            if (state.SetTimes >= MATCHRewardMaxSetTimes)
            {
                MemcachedFactory.MatchRewardClient.Delete(key);
                return ResponseHelper.CreateRoot<DTOAssetInfo>(MessageCode.MatchRewardOverSet);
            }
            RootResponse<DTOAssetInfo> rst = null;
            if (coin > 0 || point > 0)
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (null == manager)
                    return ResponseHelper.CreateRoot<DTOAssetInfo>(MessageCode.MissManager);
                string orderId = string.Concat(matchType, ".", matchId, ".", state.SetTimes);
                string tranMap = string.Format("{0}:C{1}P{2}..", orderId, coin, point);
                int assetType = 0;
                int tranType = 0;
                rst = SaveReward(manager, orderId, coin, point, assetType, tranType, tranMap);
            }
            if (state.SetTimes == MATCHRewardMaxSetTimes - 1)
                MemcachedFactory.MatchRewardClient.Delete(key);
            else
            {
                state.SetTimes++;
                state.Coin = coin;
                state.Point = point;
                MemcachedFactory.MatchRewardClient.Set(key, state);
            }
            if (null == rst)
                rst = ResponseHelper.CreateRoot<DTOAssetInfo>(new DTOAssetInfo());
            return rst;
        }
        static RootResponse<DTOAssetInfo> SaveReward(NbManagerEntity manager, string orderId, int coin, int point, int assetType, int tranType, string tranMap)
        {
            var msgCode = MessageCode.Success;
            int totalCoin = -1;
            int totalPoint = -1;
            using (var tranMgr = new TransactionManager(Games.NBall.Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Main)))
            {
                tranMgr.BeginTransaction();
                do
                {
                    if (coin > 0)
                    {
                        msgCode = ManagerCore.Instance.AddCoinV2(out totalCoin, manager, coin, (int)EnumCoinChargeSourceType.MatchReward, orderId, tranMgr.TransactionObject);
                        if (msgCode != MessageCode.Success)
                            break;
                    }
                    if (point > 0)
                    {
                        msgCode = PayCore.Instance.AddBonusV2(out totalPoint, manager.Account, point, EnumChargeSourceType.MatchReward, orderId, tranMgr.TransactionObject);
                        if (msgCode != MessageCode.Success)
                            break;
                    }
                    int errorCode = 0;
                    NbManagerMgr.AssetRecord(false, manager.Idx, assetType, tranType, tranMap, ref errorCode, tranMgr.TransactionObject);
                    msgCode = (MessageCode)errorCode;
                }
                while (false);
                if (msgCode != MessageCode.Success)
                {
                    tranMgr.Rollback();
                    return ResponseHelper.CreateRoot<DTOAssetInfo>(msgCode);
                }
                tranMgr.Commit();
            }
            ManagerCore.Instance.DeleteCache(manager.Idx);
            var data = new DTOAssetInfo(totalCoin, totalPoint);
            return ResponseHelper.CreateRoot<DTOAssetInfo>(data);
        }
      
        static bool ParseMask(out int coin, out int point, string mask)
        {
            coin = 0;
            point = 0;
            mask = mask ?? string.Empty;
            mask = mask.Trim().ToUpper();
            foreach (char c in mask)
            {
                if (c == 'C')
                    coin++;
                else if (c == 'D')
                    point++;
            }
            return true;
        }
       
    }
}
