using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.FriendShip;
using Games.NBall.Core.Manager;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceEngine;

namespace Games.NBall.Core
{
    public class OnlineThread
    {
        #region .ctor
        public OnlineThread(int p)
        {
        }
        #endregion

        #region Fields
        static readonly object lockWrite = new object();
        static readonly ReaderWriterLockSlim lockCache = new ReaderWriterLockSlim();
        static volatile bool syncTable = false;
        static volatile bool syncUpdating = false;
        static volatile bool syncCacheReset = false;
        static volatile bool syncDBReset = false;
        static volatile bool syncGuildActive = false;
        static volatile bool syncClose = false;


        static readonly Dictionary<Guid, OnlineInfoEntity> cacheActive = new Dictionary<Guid, OnlineInfoEntity>(OnlineConfig.INITCacheSize);
        static readonly OnlineinfoDataSet.Online_InfoDataTable cacheTable = new OnlineinfoDataSet.Online_InfoDataTable();
        #endregion

        #region Facade
        public static OnlineThread Instance
        {
            get { return SingletonFactory<OnlineThread>.SInstance; }
        }
        public void Start()
        {
            GetOnlineList();
        }


        public void Close()
        {
            try
            {
                this.UpdateActive();
                cacheTable.Clear();
                cacheActive.Clear();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Closing Online timer", ex);
            }
        }

        public void RiseOnlineTime(Guid managerId)
        {
            try
            {
                UpdateActiveCache(managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("RiseOnlineTime", ex);
            }
        }

        public int GetCurOnlineSecond(Guid managerId)
        {
            if (syncClose) return -1;
            OnlineInfoEntity actObj = null;
            if (lockCache.TryEnterReadLock(OnlineConfig.TIMEWaitLock))
            {
                try
                {
                    cacheActive.TryGetValue(managerId, out actObj);
                }
                finally
                {
                    lockCache.ExitReadLock();
                }
            }
            if (actObj == null)
            {
                return -1;
            }
            int seconds = (int)DateTime.Now.Subtract(actObj.GuildInTime).TotalSeconds;
            return seconds;
        }
        #endregion

        #region Inner
        void UpdateActiveCache(Guid managerId)
        {
            if (syncClose || syncUpdating) return;
            OnlineInfoEntity actObj = null;
            if (lockCache.TryEnterReadLock(OnlineConfig.TIMEWaitLock))
            {
                try
                {
                    cacheActive.TryGetValue(managerId, out actObj);
                }
                finally
                {
                    lockCache.ExitReadLock();
                }
            }
            if (actObj == null)
            {
                if (lockCache.TryEnterWriteLock(OnlineConfig.TIMEWaitLock))
                {
                    try
                    {
                        OnlineInfoEntity newObj = new OnlineInfoEntity(managerId, DateTime.Now);
                        cacheActive[managerId] = newObj;

                        OnlineCore.LoginOnline(managerId);
                    }
                    finally
                    {
                        lockCache.ExitWriteLock();
                    }
                }
            }
            else
            {
                bool isCrossDay = false;
                DateTime curTime = DateTime.Now;
                DateTime today = curTime.Date;
                if (lockCache.TryEnterReadLock(OnlineConfig.TIMEWaitLock))
                {
                    try
                    {
                        lock (actObj)
                        {
                            if (!actObj.IsHandlerCrossDay && actObj.GuildInTime.Date.AddDays(1) == today)//跨天
                            {
                                actObj.IsHandlerCrossDay = true;
                                isCrossDay = true;
                            }
                            actObj.ActiveTime = curTime;
                        }
                    }
                    finally
                    {
                        lockCache.ExitReadLock();
                    }
                }
                if (isCrossDay)
                {
                    try
                    {
                        var manager = ManagerCore.Instance.GetManager(managerId);
                        if (manager == null)
                            return;
                        NbUserMgr.UpdateContinueday(manager.Idx, manager.Account, today.AddDays(-1), today);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Insert(ex);
                    }
                }
            }

        }
        bool UpdateActive()
        {
            if (!syncTable)
            {
                syncUpdating = true;
                lockCache.EnterWriteLock();
                syncUpdating = false;
                try
                {
                    lock (lockWrite)
                    {
                        cacheTable.Clear();
                        bool flagCacheReset = syncCacheReset;
                        List<Guid> removeList = new List<Guid>();

                        foreach (var kvp in cacheActive)
                        {

                            var actObj = kvp.Value;

                            //if (actObj.ManagerId.ToString() == "")
                            //{
                            //    int a = 0;
                            //    string s = a.ToString();
                            //}
                            DataRow row = cacheTable.NewRow();
                            int inactMinutes = (int)DateTime.Now.Subtract(actObj.ActiveTime).TotalMinutes;
                            int actMinutes = (int)actObj.ActiveTime.Subtract(actObj.GuildInTime).TotalMinutes;
                            if (actMinutes < 0)
                                actMinutes = 0;
                            bool isOnLine = (inactMinutes < OnlineConfig.TIMEOffLineMinutes);
                            row[cacheTable.ManagerIdColumn.ColumnName] = actObj.ManagerId;
                            row[cacheTable.LoginTimeColumn.ColumnName] = actObj.LoginTime;
                            row[cacheTable.GuildInTimeColumn.ColumnName] = actObj.GuildInTime;
                            row[cacheTable.ActiveTimeColumn.ColumnName] = actObj.ActiveTime;
                            row[cacheTable.ActiveFlagColumn.ColumnName] = isOnLine;
                            row[cacheTable.ResetFlagColumn.ColumnName] = 1;
                            row[cacheTable.CntOnlineMinutesColumn.ColumnName] = isOnLine ? 0 : actMinutes;
                            row[cacheTable.CurOnlineMinutesColumn.ColumnName] = isOnLine ? actMinutes : 0;
                            row[cacheTable.StatusColumn.ColumnName] = 0;
                            row[cacheTable.LoginIpColumn.ColumnName] = "";
                            row[cacheTable.RowTimeColumn.ColumnName] = DateTime.Now;
                            row[cacheTable.TotalOnlineMinutesColumn.ColumnName] = actObj.TotalOnlineMinutes;
                            cacheTable.Rows.Add(row);
                            if (isOnLine)
                            {
                                if (flagCacheReset)
                                {
                                    actObj.GuildInTime = DateTime.Now;
                                    actObj.ActiveTime = DateTime.Now;
                                    actObj.CntOnlineMinutes = 0;
                                    actObj.CurOnlineMinutes = 0;
                                }
                            }
                            else
                            {
                                removeList.Add(kvp.Key);
                            }
                        }
                        syncTable = true;
                        if (flagCacheReset) syncCacheReset = false;
                        for (int i = 0; i < removeList.Count; i++)
                        {
                            cacheActive.Remove(removeList[i]);
                        }

                        OnlineCore.LogoutOnline(removeList);
                    }

                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("GetCacheData", ex);
                }
                finally
                {
                    lockCache.ExitWriteLock();
                }
            }
            lock (lockWrite)
            {
                try
                {
                    OnlineSqlHelper.UpdateActive(cacheTable);
                    cacheTable.Clear();
                    syncTable = false;
                    if (syncDBReset)
                    {
                        OnlineSqlHelper.ResetActive(OnlineConfig.PROCBatchSize);
                        syncDBReset = false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("UpdateActive", ex);
                    return false;
                }
            }
        }
        void GetOnlineList()
        {
            List<OnlineInfoEntity> list = OnlineSqlHelper.GetOnlineList(OnlineConfig.TIMEOffLineMinutes, OnlineConfig.PROCBatchSize);
            if (list == null || list.Count == 0) return;
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                cacheActive[item.ManagerId] = item;
            }
        }
        #endregion

        #region Job

        public MessageCode JobUpdateActive()
        {
            if (this.UpdateActive())
            {
                return MessageCode.Success;
            }
            else
            {
                return MessageCode.NbUpdateFail;
            }
        }

        public MessageCode JobReset()
        {
            syncCacheReset = true;
            syncDBReset = true;
            return MessageCode.Success;
        }

        public MessageCode JobUpdateKpi()
        {
            var onlineList = OnlineCore.GetOnlineList();
            if (onlineList != null && onlineList.Count > 0)
            {
                foreach (var guid in onlineList)
                {
                    MatchDataHelper.GetManagerKpi(guid);
                }
            }
            return MessageCode.Success;
        }
        #endregion

    }
}
