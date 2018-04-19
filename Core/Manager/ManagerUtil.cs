using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.SkillCard;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Core.Manager
{
    public class ManagerUtil
    {
        private static readonly int ManagerMaxLevel =
            CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ManagerMaxLevel);
        private static readonly int ManagerLevelupAddStamina =
            CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.ManagerLevelupAddStamina);

        #region CheckUserName
        public static MessageCode CheckUserName(string userName, string logo)
        {
            ////检查是否有屏蔽字
            //if (FilterHelper.Instance.ScanContent(userName))
            //{
            //    return MessageCode.RegisterNameContainBadWord;
            //}
            if (string.IsNullOrEmpty(userName))
            {
                return MessageCode.RegisterNameIsEmpty;
            }
            if (string.IsNullOrEmpty(logo))
                logo = "1";
            logo = logo.ToLower();
            if (!logo.Contains("http"))
            {
                string chinese = @"[\u4E00-\u9FA5]";
                string letter = @"[a-zA-Z]";
                string number = @"^\d+$";
                //bool result = false;
                int count = 0;
                char[] ch = userName.ToCharArray();
                foreach (char c in ch)
                {
                    if (Regex.IsMatch(c.ToString(), chinese))
                    {
                        count = count + 2;
                    }
                    else if (Regex.IsMatch(c.ToString(), letter) || Regex.IsMatch(c.ToString(), number))
                    {
                        count++;
                    }
                    else
                    {
                        return MessageCode.RegisterNameHasValidWord;
                    }
                }
                if (count < 4 || count > 12)
                {
                    return MessageCode.RegisterNameLengthRange;
                }
            }
            int exists = -1;
            NbManagerMgr.NameExists(userName, ref exists);
            if (exists == 0)
                return MessageCode.Success;
            else
                return MessageCode.RegisterNameRepeat;
        }
        #endregion

        #region CalCurrentStamina
        /// <summary>
        /// Cals the current stamina.
        /// </summary>
        /// <param name="managerExtra">The managerExtra.</param>
        public static void CalCurrentStamina(NbManagerextraEntity managerExtra, int level,int vipLevel)
        {
            if (managerExtra == null)
                return;
            DateTime newResumeTime = DateTime.Now;
            managerExtra.StaminaMax = CacheFactory.ManagerDataCache.GetMaxStamina(level);
            int newStamina = CalCurrentStamina(managerExtra.ResumeStaminaTime, out newResumeTime, managerExtra.Stamina,
                                       managerExtra.StaminaMax, vipLevel);
            managerExtra.Stamina = newStamina;
            managerExtra.ResumeStaminaTime = newResumeTime;
        }

        /// <summary>
        /// Cals the current stamina.
        /// </summary>
        /// <param name="resumeTime">恢复时间.</param>
        /// <param name="newResumeTime">下一次恢复时间.</param>
        /// <param name="stamina">The stamina.</param>
        /// <param name="maxStamina">The max stamina.</param>
        /// <returns></returns>
        public static int CalCurrentStamina(DateTime resumeTime, out DateTime newResumeTime, int stamina, int maxStamina,int vipLevel)
        {
            int timeConfig = ManagerCore.Instance.GetResumeStaminaTimeConfig(vipLevel);
            DateTime now = DateTime.Now;
            newResumeTime = now;
            if (timeConfig <= 0) //恢复时间配置没获取到，为防错误，返回0
                return 0;

            if (stamina >= maxStamina)
            {
                newResumeTime = now;
                return stamina;
            }

            try
            {
                //计算离上次恢复的时间
                TimeSpan span = now.Subtract(resumeTime);
                double minutes = span.TotalSeconds;
                if (minutes < timeConfig)
                {
                    newResumeTime = resumeTime;
                    return stamina;
                }
                else
                {
                    var addStamina = Convert.ToInt32(Math.Floor(minutes / timeConfig));
                    var totalStamina = stamina + addStamina * ManagerCore.Instance.ResumeStaminaCount;
                    if (totalStamina >= maxStamina)
                    {
                        newResumeTime = now;
                        totalStamina = maxStamina;
                    }
                    else
                    {
                        newResumeTime = resumeTime.AddSeconds(addStamina * timeConfig);
                    }

                    return totalStamina;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CalCurrentStamina", ex.Message, ex.StackTrace);
                return stamina;
            }
        }
        #endregion

        #region AddManagerData

        public static void AddManagerDataCoin(NbManagerEntity manager, int prizeCoin, EnumCoinChargeSourceType coinSourceType,
                                              string coinOrderId)
        {
            AddManagerData(manager, 0, prizeCoin, 0, coinSourceType, coinOrderId);
        }

        /// <summary>
        /// 加经理数据，不保存
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="prizeExp"></param>
        /// <param name="prizeCoin"></param>
        /// <param name="prizeSophisticate"></param>
        public static void AddManagerData(NbManagerEntity manager, int prizeExp, int prizeCoin, int prizeSophisticate, EnumCoinChargeSourceType coinSourceType, string coinOrderId)
        {
            if (manager == null)
                return;

            if (prizeCoin > 0)
            {
                manager.Coin += prizeCoin;
                manager.AddCoin += prizeCoin;
                manager.CoinSourceType = (int)coinSourceType;
                manager.CoinOrderId = coinOrderId;
                if (manager.CoinSourceType == 0)
                {
                    throw new Exception("add coin must enter coin source type.");
                }
            }
            manager.Sophisticate += prizeSophisticate;
            if (prizeExp > 0)
            {
                manager.EXP += prizeExp;
                manager.AddExp += prizeExp;
                if (manager.Level < ManagerMaxLevel)
                {
                    int exp = CacheFactory.ManagerDataCache.GetExp(manager.Level);
                    if (exp > 0 && manager.EXP >= exp)
                    {
                        manager.Level++;
                        manager.EXP -= exp;
                        manager.IsLevelup = true;
                    }
                }
                else
                {
                    manager.EXP = 0;
                }
            }
        }
        #endregion

        #region SaveManagerData
        public static bool SaveManagerData(NbManagerEntity manager)
        {
            return SaveManagerData(manager, null);
        }

        public static bool SaveManagerData(NbManagerEntity manager, NbManagerextraEntity extra, DbTransaction transaction, string zoneId = "")
        {
            return SaveManagerData(manager, extra, true, transaction, zoneId);
        }

        public static bool SaveManagerData(NbManagerEntity manager, NbManagerextraEntity extra, bool openTask = true,
                                           DbTransaction transaction = null, string zoneId = "")
        {
            try
            {
                var functionList = manager.FunctionList;
                if (manager.IsLevelup)
                {
                    var function = CacheFactory.ManagerDataCache.GetFunctionList(manager.Level);
                    if (function != null)
                    {
                        if (function.FunctionId > 0)
                        {
                            manager.AddOpenFunc(function.FunctionId);
                        }
                        functionList = function.FunctionList;
                        if (extra != null)
                            extra.FunctionList = functionList;
                    }
                }
                int returnCode = -2;
                var levelGiftExpired = ShareUtil.BaseTime;
                var levelGiftExpired2 = ShareUtil.BaseTime;
                var levelGiftExpired3 = ShareUtil.BaseTime;
                int step = 0;
                if (manager.IsLevelup)
                {
                    step = CacheFactory.ManagerDataCache.GetLevelgiftStep(manager.Level);
                    if (step > 0)
                    {
                        manager.OpenLevelGift = true;
                        switch (step)
                        {
                            case 1:
                                levelGiftExpired = DateTime.Now.AddHours(72);
                                break;
                            case 2:
                                levelGiftExpired2 = DateTime.Now.AddHours(72);
                                break;
                            case 3:
                                levelGiftExpired3 = DateTime.Now.AddHours(72);
                                break;
                        }
                        if (extra != null)
                        {
                            extra.LevelGiftExpired = levelGiftExpired;
                            extra.LevelGiftExpired2 = levelGiftExpired2;
                            extra.LevelGiftExpired3 = levelGiftExpired3;
                            extra.LevelGiftStep = step;
                        }
                    }
                }
                NbManagerMgr.Save(manager.Idx, manager.Level, manager.EXP, manager.Sophisticate, manager.Score, manager.Coin,
                                  manager.Reiki, manager.TeammemberMax, manager.TrainSeatMax, manager.VipLevel,
                                   functionList, levelGiftExpired, levelGiftExpired2, levelGiftExpired3, step, manager.RowVersion, ref returnCode, transaction, zoneId);
                if (returnCode != 0)
                    return false;
                manager.FunctionList = functionList;

                try
                {
                    if (manager.IsLevelup)
                    {
                        var code = SkillCardRules.SetSkillMapByManagerLevel(manager, transaction);
                        if (code != MessageCode.Success)
                            return false;

                         var addSkillPoint = CacheFactory.ManagerDataCache.GetAddSkillPointByLevel(manager.Level);
                        if (extra != null)
                        {
                            extra.SkillPoint += addSkillPoint;
                        }
                        else
                        {
                            if (!NbManagerextraMgr.AddSkillPoint(manager.Idx, addSkillPoint, transaction))
                                return false;
                        }

                        CalCurrentStamina(extra, manager.Level, manager.VipLevel);
                        if (extra == null)
                        {
                            extra = ManagerCore.Instance.GetManagerExtra(manager.Idx);
                            if (extra.Stamina < extra.StaminaMax)
                            {
                                extra.Stamina = extra.StaminaMax;
                                // extra.Stamina += ManagerLevelupAddStamina;
                                //if (extra.Stamina > extra.StaminaMax)
                                //    extra.Stamina = extra.StaminaMax;
                                extra.ResumeStaminaTime = DateTime.Now;
                                NbManagerextraMgr.Update(extra, transaction);
                            }
                        }
                        else
                        {
                            if (extra.Stamina < extra.StaminaMax)
                            {
                                extra.Stamina = extra.StaminaMax;
                                extra.ResumeStaminaTime = DateTime.Now;
                            }
                        }

                        //if (TaskCore.Instance.CheckOpenDailyTask(manager.Level))
                        //{
                        //    var daily = TaskDailyrecordMgr.GetById(manager.Idx, zoneId);
                        //    if (daily == null)
                        //    {
                        //        var entity = TaskCore.Instance.CreateDailyTask(manager.Idx);
                        //        if (!TaskDailyrecordMgr.Insert(entity, transaction, zoneId))
                        //            return false;
                        //    }
                        //}
                        if (openTask)
                        {
                            List<TaskRecordEntity> newTasks = new List<TaskRecordEntity>();
                            TaskCore.Instance.GetLevelOpenTasks(manager.Idx, manager.Level, ref newTasks, zoneId);
                            if (newTasks.Count > 0)
                            {
                                foreach (var entity in newTasks)
                                {
                                    TaskCore.Instance.HandleOpenTaskStatus(entity, zoneId);
                                    if (!TaskRecordMgr.Add(entity, transaction, zoneId))
                                        return false;
                                }
                                manager.HasOpenTask = true;
                            }
                        }
                        //如果等级为15级 向竞技场注册用户
                        //获取竞技场等级限制
                        //if (manager.Level == level.Values)
                        //{

                        //}


                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.ErrorByZone("SaveManagerData Task pending", ex, zoneId);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.ErrorByZone("SaveManagerData", ex, zoneId);
                return false;
            }
            finally
            {
                ManagerCore.Instance.DeleteCache(manager.Idx);
            }

        }

        public static List<PopMessageEntity> SaveManagerAfter(NbManagerEntity manager, NbManagerextraEntity extra, DbTransaction transaction)
        {
            if (manager == null)
                return null;
            List<PopMessageEntity> popList = new List<PopMessageEntity>();
            if (manager.AddCoin > 0 || manager.AddExp > 0)
            {
                //ShadowMgr.SaveCoinCharge(manager.Idx, manager.AddCoin, manager.AddExp, manager.IsLevelup, manager.Level, manager.CoinSourceType, manager.CoinOrderId);
            }
            if (manager.IsLevelup)
            {
                // ChatHelper.SendOpenLevelGiftPop(manager);
                if (extra == null)
                {
                    extra = ManagerCore.Instance.GetManagerExtra(manager.Idx);
                }
                CalCurrentStamina(extra, manager.Level,manager.VipLevel);
                if (extra.Stamina < extra.StaminaMax)
                {
                    extra.Stamina = extra.StaminaMax;
                    // extra.Stamina += ManagerLevelupAddStamina;
                    //if (extra.Stamina > extra.StaminaMax)
                    //    extra.Stamina = extra.StaminaMax;
                    extra.ResumeStaminaTime = DateTime.Now;
                    NbManagerextraMgr.Update(extra, transaction);
                }
            }
            DeleteOpenFunctionCache(manager.Idx);
            manager.IsLevelup = false;
            return popList;
        }


        public static List<PopMessageEntity> SaveManagerAfter(NbManagerEntity manager, NbManagerextraEntity extra = null,
                                                              bool sendByChat = false)
        {
            return SaveManagerAfter(manager, sendByChat, extra);
        }

        public static List<PopMessageEntity> SaveManagerAfter(NbManagerEntity manager, bool sendByChat, NbManagerextraEntity extra = null, int currentPoint = -1, int tourLeagueId = -1)
        {
            if (manager == null)
                return null;
            List<PopMessageEntity> popList = new List<PopMessageEntity>();
            if (manager.AddCoin > 0 || manager.AddExp > 0)
            {
                //ShadowMgr.SaveCoinCharge(manager.Idx, manager.AddCoin, manager.AddExp, manager.IsLevelup, manager.Level, manager.CoinSourceType, manager.CoinOrderId);
            }
            if (manager.IsLevelup)
            {
               // ChatHelper.SendOpenLevelGiftPop(manager);
                if (extra == null)
                {
                    extra = ManagerCore.Instance.GetManagerExtra(manager.Idx);
                }
                CalCurrentStamina(extra, manager.Level,manager.VipLevel);
                if (extra.Stamina < extra.StaminaMax)
                {
                    extra.Stamina = extra.StaminaMax;
                    //extra.Stamina += ManagerLevelupAddStamina;
                    //if (extra.Stamina > extra.StaminaMax)
                    //    extra.Stamina = extra.StaminaMax;
                    NbManagerextraMgr.Update(extra);
                }
            }

            //var pop1 = ChatHelper.SendUpdateManagerInfoPop(manager, extra, sendByChat, currentPoint, tourLeagueId);
            //if (pop1 != null)
            //    popList.Add(pop1);
            //var pop2 = ChatHelper.SendOpenFuncPop(manager, sendByChat);
            //if (pop2 != null)
            //    popList.Add(pop2);
            //var pop3 = ChatHelper.SendOpenTaskPop(manager, sendByChat);
            //if (pop3 != null)
            //    popList.Add(pop3);

            DeleteOpenFunctionCache(manager.Idx);
            manager.IsLevelup = false;
            return popList;
        }
        #endregion

        #region GetVipLevel
        public static int GetVipLevel(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager != null)
                return manager.VipLevel;
            return 0;
        }
        #endregion

        #region CheckHook
        public static MessageCode CheckHook(Guid managerId)
        {
           

            return MessageCode.Success;
        }

        #endregion

        #region OpenFunction
        public static void DeleteOpenFunctionCache(Guid managerId)
        {
            MemcachedFactory.OpenFunctionClient.Delete(managerId);
        }

        public static void SetOpenFunction(NbManagerEntity manager, int functionId)
        {
            var func = GetOpenFunction(manager.Idx);
            var ss = func.Split(',');
            if (ss.Length >= functionId)
            {
                manager.AddOpenFunc(functionId);
                ss[functionId - 1] = "1";
                DeleteOpenFunctionCache(manager.Idx);
                manager.FunctionList = string.Join(",", ss);
            }
        }

        public static bool CheckFunction(Guid managerId, EnumOpenFunction enumOpenFunction)
        {
            return CheckFunction(managerId, (int)enumOpenFunction);
        }

        public static bool CheckFunction(Guid managerId, int enumOpenFunction)
        {
            var function = GetOpenFunction(managerId);
            return CheckFunction(function, enumOpenFunction);
        }

        public static bool CheckFunction(string siteId, Guid managerId, EnumOpenFunction enumOpenFunction)
        {
            var managerExtra = NbManagerextraMgr.GetById(managerId, siteId);
            var func = managerExtra.FunctionList;
            return CheckFunction(func, (int)enumOpenFunction);
        }

        static bool CheckFunction(string function, int enumOpenFunction)
        {
           
            var ss = function.Split(',');
            if (ss.Length >= enumOpenFunction)
            {
                return ss[enumOpenFunction - 1] == "1";
            }
            else
            {
                return false;
            }
        }

        public static string GetOpenFunction(Guid managerId)
        {
            string func = MemcachedFactory.OpenFunctionClient.Get<string>(managerId);
            if (string.IsNullOrEmpty(func))
            {
                var managerExtra = ManagerCore.Instance.GetManagerExtra(managerId);
                func = managerExtra.FunctionList;
                MemcachedFactory.OpenFunctionClient.Set(managerId, func);
            }
            return func;
        }


        #endregion

        #region GetWinRate
        public static double GetWinRate(Guid managerId, EnumMatchType matchType, string siteId = "")
        {
            var matchState = NbMatchstatMgr.GetByManagerAndType(managerId, (int)matchType, siteId);
            if (matchState != null)
            {
                return matchState.WinRate;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        public static int GetKpi(Guid managerId, string siteId = "")
        {
            var extra = NbManagerextraMgr.GetById(managerId, siteId);
            if (extra != null)
                return extra.Kpi;
            return 0;
        }

        #region encapsulation

        #endregion
    }
}
