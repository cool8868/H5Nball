using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;
using Games.NBall.Core.FriendShip;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceContract.Client;
using Games.NBall.ServiceEngine;

namespace Games.NBall.Core
{
    public class OnlineCore
    {
        private static readonly int _expireTime = 600;//10分钟
        #region .ctor
        public OnlineCore(int p)
        {
            if (!ShareUtil.IsCross)
            {
                if (!CacheFactory.ServicetionSectionCache.HasOnlineService())
                {
                    _onlineClient = new OnlineClient();
                }
            }
        }
        #endregion

        #region Facade
        public static OnlineCore Instance
        {
            get { return SingletonFactory<OnlineCore>.SInstance; }
        }

        #region 在线统计
        /// <summary>
        /// 获取在线状态
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public static bool CheckOnline(Guid managerId)
        {
            string sessionId = string.Empty;
            return CheckOnlineState(managerId, out sessionId);
        }

        /// <summary>
        /// 获取当日在线时间：单位分钟
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public static int GetOnlineSecond(Guid managerId)
        {
            int cntonlineMinutes = 0;
            int seconds = 0;
            bool activityFlag = false;
            DateTime curTime = DateTime.Now;
            DateTime guildintime = curTime;
            OnlineInfoMgr.GetOnlineMinutes(managerId, ref activityFlag, ref guildintime, ref cntonlineMinutes);
            if (activityFlag)
            {
                seconds = (int)curTime.Subtract(guildintime).TotalSeconds;
            }
            seconds = seconds + cntonlineMinutes * 60;
            return seconds;
        }

        /// <summary>
        /// 获取在线数据
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="ifOnline">是否在线</param>
        /// <param name="loginTime">最近登录时间</param>
        /// <param name="activeTime">最近活跃时间</param>
        /// <param name="onlineMinutes">当日在线分钟数</param>
        public static void GetOnlineState(Guid managerId, ref bool ifOnline, ref DateTime loginTime, ref DateTime activeTime, ref int onlineMinutes)
        {
            try
            {
                OnlineInfoMgr.GetByManagerId(managerId, ref ifOnline, ref loginTime, ref activeTime, ref onlineMinutes);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("OnlineRules:GetOnlineState", ex);
            }
        }
        #endregion

        #region 防止重复登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="sessionId">Session标识</param>
        /// <returns>抢线标记,True为抢线</returns>
        public static bool LoginSession(Guid managerId, string sessionId)
        {
            string curSession = GetSession(managerId.ToString(), true);
            bool bumpFlag = !string.IsNullOrEmpty(curSession) && !curSession.Equals(sessionId);//占线标记
            ForceSession(managerId, sessionId);
            DeleteSession(sessionId);
            if (bumpFlag)
                SetSession(curSession, bumpFlag.ToString(), false);

            return bumpFlag;
        }
        /// <summary>
        /// 保持Session
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="sessionId">Session标识</param>
        /// <returns></returns>
        public static bool ForceSession(Guid managerId, string sessionId)
        {
            return SetSession(managerId.ToString(), sessionId, true);
        }
        /// <summary>
        /// 验证重复登录
        /// </summary>
        /// <param name="sessionId">SessionId</param>
        /// <returns>踢线标记,True为被用户或系统踢线</returns>
        public static bool CheckKickState(string sessionId)
        {
            bool bumpFlag = false;
            return CheckKickNBumpState(sessionId, out bumpFlag);
        }
        /// <summary>
        /// 验证重复登录
        /// </summary>
        /// <param name="sessionId">SessionId</param>
        /// <param name="bumpFlag">抢线标记,True为被用户抢线,False为系统踢线</param>
        /// <returns>踢线标记,True为被用户或系统踢线</returns>
        public static bool CheckKickNBumpState(string sessionId, out bool bumpFlag)
        {
            bumpFlag = false;
            try
            {
                string kickState = GetSession(sessionId, false);
                bool kickFlag = !string.IsNullOrEmpty(kickState);
                if (kickFlag)
                    bool.TryParse(kickState, out bumpFlag);
                return kickFlag;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("OnlineRules:CheckKickNBumpState", ex);
                return false;
            }
        }
        /// <summary>
        /// 验证是否在线
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="sessionId">SessionId</param>
        /// <returns>在线标记,True为当前在线</returns>
        public static bool CheckOnlineState(Guid managerId, out string sessionId)
        {
            sessionId = GetSession(managerId.ToString(), true);
            return !string.IsNullOrEmpty(sessionId);
        }
        /// <summary>
        /// 踢线
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <returns>在线标记,True为当前在线</returns>
        public static bool KickSession(Guid managerId)
        {
            string sessionId = string.Empty;
            bool onlineFlag = CheckOnlineState(managerId, out sessionId);
            bool bumpFlag = false;
            if (onlineFlag)
            {
                DeleteSession(managerId.ToString());
                SetSession(sessionId, bumpFlag.ToString(), false);
            }
            return onlineFlag;
        }
        //a8接口放sessionId
        public static bool SetSessionId(string openId, string sessionId)
        {
            return true;
            //return SetSession(openId, sessionId, false);
        }
        //a8接口拿sessionId
        public static string GetSessionId(string openId)
        {
            return "";
            // return MemcachedFactory.OnlineSessionClient.Get<string>(openId);
        }
        static bool SetSession(string key, object value, bool withTime)
        {
            try
            {
                if (key == null || value == null) return false;
                string timeStr = string.Empty;
                if (withTime)
                    timeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
                return MemcachedFactory.OnlineSessionClient.Set(key, timeStr + value.ToString());
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("OnlineRules:SetSession", ex);
                return false;
            }
        }
        static string GetSession(string key, bool withTime)
        {
            DateTime setTime = DateTime.Now;
            return GetSession(key, withTime, out setTime);
        }
        static string GetSession(string key, bool withTime, out DateTime setTime)
        {
            setTime = DateTime.Now;
            string val = string.Empty;
            try
            {
                val = MemcachedFactory.OnlineSessionClient.Get<string>(key);
                if (!string.IsNullOrEmpty(val))
                {
                    if (withTime && val.Length >= 19)
                    {
                        DateTime.TryParseExact(val.Substring(0, 19), "yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out setTime);
                        val = val.Substring(19);
                        if (DateTime.Now.Subtract(setTime).TotalSeconds >= _expireTime / 2)
                            SetSession(key, val, withTime);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("OnlineRules:SetSession", ex);
            }
            return val;
        }
        static bool DeleteSession(string key)
        {
            return MemcachedFactory.OnlineSessionClient.Delete(key);
        }
        #endregion

        #region 封停用户
        /// <summary>
        /// 验证是否已封停
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <returns>封停标记</returns>
        public static bool CheckLockState(Guid managerId)
        {
            DateTime lockDate = Convert.ToDateTime("1900-01-01");
            DateTime breakDate = Convert.ToDateTime("2050-01-01");
            return CheckLockStateNDate(managerId, out lockDate, out breakDate);
        }
        /// <summary>
        /// 验证是否已封停
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="lockDate">封停时间</param>
        /// <param name="breakDate">预计解封时间</param>
        /// <returns>封停标记</returns>
        public static bool CheckLockStateNDate(Guid managerId, out DateTime lockDate, out DateTime breakDate)
        {
            lockDate = Convert.ToDateTime("1900-01-01");
            breakDate = Convert.ToDateTime("2050-01-01");
            try
            {
                bool lockFlag = false;
                OnlineLockmanagerMgr.CheckLock(managerId, ref lockFlag, ref lockDate, ref breakDate);
                return lockFlag;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("OnlineRules:CheckLock", ex);
                return false;
            }
        }
        /// <summary>
        /// 无限期封停用户
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="GMName">GM名字</param>
        /// <param name="memo">封停原因</param>
        /// <returns></returns>
        public static bool LockUserUnexpect(Guid managerId, string GMName, string memo)
        {
            DateTime expireTime = Convert.ToDateTime("2050-01-01");
            return LockUser(managerId, GMName, memo, expireTime);
        }
        /// <summary>
        /// 限期封停用户
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="GMName">GM名字</param>
        /// <param name="memo">封停原因</param>
        /// <param name="days">封停的天数</param>
        /// <param name="hours">封停的小时数</param>
        /// <param name="minutes">封停的分钟数</param>
        /// <returns></returns>
        public static bool LockUserExpect(Guid managerId, string GMName, string memo, int days, int hours, int minutes)
        {
            TimeSpan ts = new TimeSpan(days, hours, minutes, 0);
            DateTime expireTime = DateTime.Now.Add(ts);
            return LockUser(managerId, GMName, memo, expireTime);
        }
        static bool LockUser(Guid managerId, string GMName, string memo, DateTime expireTime)
        {
            try
            {
                KickSession(managerId);
                OnlineLockmanagerMgr.Lock(managerId, 0, expireTime, GMName, memo);
                return true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("OnlineRules:LockUser", ex);
                return false;
            }
        }

        /// <summary>
        /// 解封用户
        /// </summary>
        /// <param name="managerId">经理Id</param>
        /// <param name="GMName">GM名字</param>
        /// <param name="memo">解封说明</param>
        /// <returns></returns>
        public static bool BreakLock(Guid managerId, string GMName, string memo, string zoneId)
        {
            try
            {
                OnlineLockmanagerMgr.BreakLock(managerId, GMName, memo, null, zoneId);
                return true;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("OnlineRules:BreakLock", ex);
                return false;
            }
        }
        #endregion

        #region 在线列表
        static Dictionary<Guid, int> dicOnlineList = new Dictionary<Guid, int>();
        static readonly object lockOnline = new object();
        /// <summary>
        /// 登入在线列表
        /// </summary>
        /// <param name="managerId"></param>
        public static void LoginOnline(Guid managerId)
        {
            lock (lockOnline)
            {
                dicOnlineList[managerId] = 0;
            }
        }
        /// <summary>
        /// 登出在线列表
        /// </summary>
        /// <param name="managerIds"></param>
        public static void LogoutOnline(List<Guid> managerIds)
        {
            lock (lockOnline)
            {
                managerIds.ForEach(i => dicOnlineList.Remove(i));
            }
        }
        /// <summary>
        /// 获取在线列表
        /// </summary>
        /// <returns></returns>
        public static List<Guid> GetOnlineList()
        {
            lock (lockOnline)
            {
                return dicOnlineList.Keys.ToList();
            }
        }
        /// <summary>
        /// 获取在线人数
        /// </summary>
        /// <returns></returns>
        public static int GetOnlineCount()
        {
            lock (lockOnline)
            {
                return dicOnlineList.Count();
            }
        }

        #endregion

        #region 防沉迷
        public static int GetOnlineMinutes(Guid managerId)
        {
            if (!CheckOpenIndulge())
            {
                return -1;
            }
            int cntonlineMinutes = 0;
            int minutes = 0;
            bool activityFlag = false;
            DateTime curTime = DateTime.Now;
            DateTime guildintime = curTime;

            OnlineThread.Instance.RiseOnlineTime(managerId);
            OnlineInfoMgr.GetOnlineMinutes(managerId, ref activityFlag, ref guildintime, ref cntonlineMinutes);
            if (activityFlag)
            {
                minutes = (int)curTime.Subtract(guildintime).TotalMinutes;
            }
            minutes = minutes + cntonlineMinutes;
            return minutes;
        }

        public static int GetOnlineMinutes(Guid managerId, string siteId)
        {
            if (!CheckOpenIndulge(siteId))
            {
                return -1;
            }
            int cntonlineMinutes = 0;
            int minutes = 0;
            bool activityFlag = false;
            DateTime curTime = DateTime.Now;
            DateTime guildintime = curTime;

            OnlineInfoMgr.GetOnlineMinutes(managerId, ref activityFlag, ref guildintime, ref cntonlineMinutes, null, siteId);
            if (activityFlag)
            {
                minutes = (int)curTime.Subtract(guildintime).TotalMinutes;
            }
            minutes = minutes + cntonlineMinutes;
            return minutes;
        }

        public void CalIndulgePrize(Guid managerId, ref int exp)
        {
            int coin = 0;
            var manager = ManagerCore.Instance.GetManager(managerId);
            CalIndulgePrize(manager, ref exp, ref coin);
        }
        public void CalIndulgePrize(NbManagerEntity manager, ref int exp)
        {
            int coin = 0;
            CalIndulgePrize(manager, ref exp, ref coin);
        }
        public void CalIndulgePrize(Guid managerId, ref int exp, ref int coin, string siteId = "")
        {
            var manager = ManagerCore.Instance.GetManager(managerId, siteId);
            CalIndulgePrize(manager, ref exp, ref coin, siteId);
        }

        public void CalIndulgePrize(NbManagerEntity manager, ref int exp, ref int coin, string siteId = "")
        {
            int sophisticate = 0;
            CalIndulgePrize(manager, ref exp, ref coin, ref sophisticate, siteId);
        }

        public void CalIndulgePrize(Guid managerId, ref int exp, ref int coin, ref int sophisticate)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            CalIndulgePrize(manager, ref exp, ref coin, ref sophisticate);
        }

        public void CalIndulgePrize(NbManagerEntity manager, ref int exp, ref int coin, ref int sophisticate, string siteId = "")
        {
            if (!CheckOpenIndulge(siteId))
            {
                return;
            }
            int onlineMinutes = GetIndulgeMinutes(manager.Account, manager.Idx, siteId);
            if (onlineMinutes >= 60 && onlineMinutes < 180)
            {
                return;
            }
            else if (onlineMinutes >= 180 && onlineMinutes < 300)
            {
                exp = exp / 2;
                coin = coin / 2;
                sophisticate = sophisticate / 2;
            }
            else if (onlineMinutes >= 300)
            {
                exp = 0;
                coin = 0;
                sophisticate = 0;
            }
            else
            {
                return;
            }
        }

        public int CalIndulgeLadderScore(Guid managerId, int score, string siteId = "")
        {
            if (!CheckOpenIndulge(siteId))
            {
                return score;
            }
            if (score <= 0)
                return score;
            var manager = ManagerCore.Instance.GetManager(managerId, false, siteId);
            int onlineMinutes = GetIndulgeMinutes(manager.Account, manager.Idx, siteId);
            if (onlineMinutes >= 60 && onlineMinutes < 180)
            {
                return score;
            }
            else if (onlineMinutes >= 180 && onlineMinutes < 300)
            {
                return score / 2;
            }
            else if (onlineMinutes >= 300)
            {
                return 0;
            }
            else
            {
                return score;
            }
        }

        public bool CheckIndulgeNoPrize(Guid managerId)
        {
            if (!CheckOpenIndulge())
            {
                return false;
            }
            var manager = ManagerCore.Instance.GetManager(managerId);
            return CheckIndulgeNoPrize(manager);
        }

        public bool CheckIndulgeNoPrize(NbManagerEntity manager)
        {
            int onlineMinutes = GetIndulgeMinutes(manager.Account, manager.Idx);
            if (onlineMinutes >= 60 && onlineMinutes < 180)
            {
                return false;
            }
            else if (onlineMinutes >= 180 && onlineMinutes < 300)
            {
                return true;
            }
            else if (onlineMinutes >= 300)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private OnlineClient _onlineClient;
        public int GetIndulgeMinutes(string account, Guid managerId, string siteId = "")
        {
            try
            {
                if (!CheckOpenIndulge(siteId))
                {
                    return -1;
                }
                if (string.IsNullOrEmpty(siteId) && _onlineClient != null)
                {
                    return _onlineClient.GetIndulgeMinutes(account, managerId);
                }
                else
                {
                    var userreg = NbUserregMgr.GetById(account, siteId);
                    if (userreg != null)
                    {
                        var adultTime = DateTime.Now.AddYears(-18);
                        if (userreg.Birthday <= adultTime)
                            return -1;
                    }
                    var online = GetOnlineMinutes(managerId, siteId);
                    if (userreg != null && userreg.RecordDate == DateTime.Today && userreg.LastOnlineMinutes > 0)
                    {
                        online -= userreg.LastOnlineMinutes;
                    }
                    if (online < 0)
                        online = 0;
                    return online;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Onlinecore GetIndulgeMinutes", ex);
                return -1;
            }
        }

        public static bool CheckOpenIndulge(string siteId = "")
        {
            if (string.IsNullOrEmpty(siteId))
                siteId = ShareUtil.ZoneName;
            return CacheFactory.FunctionAppCache.CheckOpenIndulge(siteId);
        }
        #endregion

        #endregion

    }
}
