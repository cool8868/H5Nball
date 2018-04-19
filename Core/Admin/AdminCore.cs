using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core
{
    public class AdminCore
    {
        #region .ctor

        public AdminCore(int p)
        {
        }
        #endregion

        #region Facade
        public static AdminCore Instance
        {
            get { return SingletonFactory<AdminCore>.SInstance; }
        }

        public MessageCode AddItems(Guid managerId, int itemCode, int itemCount, int strength, bool isBinding)
        {
            try
            {
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.AdminAddItem);
                if (package == null)
                    return MessageCode.NbParameterError;
                var item = CacheFactory.ItemsdicCache.GetItem(itemCode);
                if (item == null)
                    return MessageCode.ItemNotExists;
                var result = package.AddItems(itemCode, itemCount, strength, isBinding,false);
                if (result == MessageCode.Success)
                {
                    bool isSuccess = package.Save();
                    if (isSuccess)
                    {
                        package.Shadow.Save();
                    }
                    return MessageCode.Success;
                }
                else
                {
                    return result;
                }

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Admin AddItems", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode AddCoin(Guid managerId, int coin)
        {
            return AddCoin(managerId, coin, (int)EnumCoinChargeSourceType.AdminAdd);
        }

        public MessageCode AddCoin(Guid managerId, int coin, int sourceType)
        {
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return MessageCode.MissManager;
                var code = ManagerCore.Instance.AddCoin(manager, coin, sourceType, "");
                if (code == MessageCode.Success)
                {
                    ManagerCore.Instance.UpdateCoinAfter(manager);
                }
                return code;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Admin AddCoin", ex);
                return MessageCode.Exception;
            }

        }

        public MessageCode AddSophisticate(Guid managerId, int sophisticate)
        {
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return MessageCode.MissManager;
                return ManagerCore.Instance.UpdateSophisticate(manager, sophisticate, EnumActionType.Add);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Admin AddSophisticate", ex);
                return MessageCode.Exception;
            }

        }

        public MessageCode AddReiki(Guid managerId, int reiki)
        {
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return MessageCode.MissManager;
                return ManagerCore.Instance.UpdateReiki(manager, reiki, EnumActionType.Add);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Admin AddReiki", ex);
                return MessageCode.Exception;
            }

        }

        public MessageCode Charge(Guid managerId, string account, int sourceType, int bonus)
        {
            try
            {
                return PayCore.Instance.Charge(account, sourceType, 0, 0, bonus, Guid.NewGuid().ToString());
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Admin Charge", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode CheckActive(string account)
        {
            try
            {
                var manager = NbManagerMgr.GetByAccount(account);
                if (manager == null || manager.Count <= 0)
                    return MessageCode.MissManager;
                else
                {
                    return MessageCode.Success;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Admin CheckActive", ex);
                return MessageCode.Exception;
            }
        }
        

        public NBManagerInfoResponse Levelup(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            var levelupExp = CacheFactory.ManagerDataCache.GetExp(manager.Level);
            if (levelupExp > 0)
            {
                levelupExp = levelupExp - manager.EXP + 1;
                ManagerUtil.AddManagerData(manager, levelupExp, 0, 0, EnumCoinChargeSourceType.None, "");
                if (ManagerUtil.SaveManagerData(manager))
                {
                    ManagerUtil.SaveManagerAfter(manager);
                    return ManagerCore.Instance.GetManagerInfo(managerId, false);
                }
                else
                {
                    return ResponseHelper.Create<NBManagerInfoResponse>(MessageCode.NbUpdateFail);
                }
            }
            else
            {
                return ResponseHelper.Create<NBManagerInfoResponse>(MessageCode.NbManagerLevelOver);
            }
        }

        public MessageCode ClearPackage(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return MessageCode.MissManager;
            var code = ManagerCore.Instance.ClearPackage(managerId);
            return code;
        }

        public MessageCode AddManagerData(Guid managerId, int prizeExp, int prizeCoin, int prizeSophisticate)
        {
            try
            {
                NbManagerEntity homeManager = ManagerCore.Instance.GetManager(managerId);
                ManagerUtil.AddManagerData(homeManager, prizeExp, prizeCoin, prizeSophisticate, EnumCoinChargeSourceType.AdminAdd, "");
                ManagerUtil.SaveManagerData(homeManager);
                ManagerUtil.SaveManagerAfter(homeManager, true);
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("AdminCore:AddManagerData", ex);
                return MessageCode.Exception;
            }
        }

        #endregion
    }
}
