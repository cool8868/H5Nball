using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Ladder;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Ladder;

namespace Games.NBall.Core.CrossLadder
{
    public class CrossLadderManager
    {
        private Dictionary<int, CrossLadderThread> _ladderThreadDic;
        Dictionary<string, CrossLadderThread> _siteLadderThreadDic;
        private int _ladderHookCD;
        private int _hookExpired;
        private ConcurrentDictionary<Guid, CrossladderHookEntity> _hookDic;
        private List<CrossladderHookEntity> _finishList;
        /// <summary>
        /// 挂机记录
        /// </summary>
        private ConcurrentDictionary<Guid, List<LadderHook>> _hookListDic;
        #region .ctor

        public CrossLadderManager(int p)
        {
            _ladderThreadDic = new Dictionary<int, CrossLadderThread>();
            _siteLadderThreadDic = new Dictionary<string, CrossLadderThread>();
            var domainList = CrossSiteCache.Instance().GetDomainList();
            if (domainList != null && domainList.Count > 0)
            {
                foreach (var i in domainList)
                {
                    if (i > 0)
                    {
                        var thread = new CrossLadderThread(i);
                        _ladderThreadDic.Add(i, thread);
                        var siteList = CrossSiteCache.Instance().GetSiteListByDomain(i);
                        if (siteList != null && siteList.Count > 0)
                        {
                            foreach (var site in siteList)
                            {
                                if (!_siteLadderThreadDic.ContainsKey(site))
                                    _siteLadderThreadDic.Add(site, thread);
                            }
                        }
                    }
                }
            }
            CreateHook();
        }

        private void CreateHook()
        {
            _hookListDic = new ConcurrentDictionary<Guid, List<LadderHook>>();
            _ladderHookCD = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderHookCD);
            _hookExpired = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.LadderHookExpired);
            var list = CrossladderHookMgr.GetList();
            _hookDic = new ConcurrentDictionary<Guid, CrossladderHookEntity>();
            foreach (var entity in list)
            {
                if (HookEndCheck(entity))
                    HookEnd(entity.ManagerId, EnumHookStatus.Exception);
                else
                {
                    AddToDic(entity);
                }
            }
        }
        #endregion

        #region Facade

        public MessageCode ScoreToHonorJob()
        {
            foreach (var thread in _ladderThreadDic.Values)
            {
                var code = thread.ScoreToHonorJob();
                if (code != MessageCode.Success)
                {
                    SystemlogMgr.Info("ScoreToHonorJob", code.ToString());
                }
            }
            return MessageCode.Success;
        }

        public MessageCode GetMatchMarqueeJob()
        {
            foreach (var thread in _ladderThreadDic.Values)
            {
                var code = thread.GetMatchMarqueeJob();
                if (code != MessageCode.Success)
                {
                    SystemlogMgr.Info("GetMatchMarqueeJob", code.ToString());
                }
            }
            return MessageCode.Success;
        }
        /// <summary>
        /// 停止所有挂机
        /// </summary>
        /// <returns></returns>
        public MessageCode StopHookJob()
        {
            try
            {
                var hooklist = _hookDic.Values.ToList();
                foreach (var item in hooklist)
                {
                    try
                    {
                        if (HookEnd(item.ManagerId, EnumHookStatus.Stop))
                        {
                            var thread = GetThread(item.SiteId);
                            if (thread != null)
                            {
                                thread.Leave(item.ManagerId);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemlogMgr.Error("停止天梯赛挂机", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CheckStatusJob", ex);
                return MessageCode.Exception;
            }
            CreateHook();
            return MessageCode.Success;
        }
        public MessageCode HookJob()
        {
            try
            {
                _finishList = new List<CrossladderHookEntity>();

                foreach (var entity in _hookDic.Values)
                {
                    Hook(entity);
                }
                doFinish();
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("HookJob", ex);
                return MessageCode.Exception;
            }

            return MessageCode.Success;
        }

        CrossLadderThread GetThread(string siteId)
        {
            if (_siteLadderThreadDic.ContainsKey(siteId))
                return _siteLadderThreadDic[siteId];
            return null;
        }

        public static CrossLadderManager Instance
        {
            get { return SingletonFactory<CrossLadderManager>.SInstance; }
        }

        public MessageCodeResponse Attend(string siteId, Guid managerId)
        {
            var thread = GetThread(siteId);
            if (thread == null)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbDomainInvalid);
            return thread.Attend(siteId, managerId);
        }

        public MessageCodeResponse Leave(string siteId, Guid managerId)
        {
            var thread = GetThread(siteId);
            if (thread == null)
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbDomainInvalid);
            return thread.Leave(managerId);
        }

        public CrossLadderHeartResponse Heart(string siteId, Guid managerId)
        {
            var thread = GetThread(siteId);
            if (thread == null)
                return ResponseHelper.Create<CrossLadderHeartResponse>(MessageCode.NbDomainInvalid);
            return thread.Heart(managerId);
        }


        public LadderMatchMarqueeResponse GetMatchMarqueeResponse(string siteId)
        {
            var thread = GetThread(siteId);
            if (thread == null)
                return ResponseHelper.Create<LadderMatchMarqueeResponse>(MessageCode.NbDomainInvalid);
            return thread.GetMatchMarqueeResponse();
        }

        public void UpdateHookScore(string siteId, Guid managerId, int score, bool isWin, int homtScore, int awayScore, string homeName,
            string awayName, int myCoin)
        {
            UpdateHookScore(managerId,score,isWin,homtScore,awayScore,homeName,awayName,myCoin);
        }
        #endregion

        #region Hook
        public LadderHookInfoResponse GetHookInfoResponse(Guid managerId)
        {
            CrossladderHookEntity hook = null;
            _hookDic.TryGetValue(managerId, out hook);
            return GetHookInfoResponse(managerId, hook, true);
        }

        public LadderHookInfoResponse GetHookInfoResponse(Guid managerId, CrossladderHookEntity hook, bool isHook = false)
        {
            if (hook != null)
            {
                if (hook.Status >= (int)EnumHookStatus.Run)
                {
                    return BuildHookInfoResponse(hook, isHook);
                }
            }
            var response = ResponseHelper.CreateSuccess<LadderHookInfoResponse>();
            response.Data = new LadderHookInfoEntity();
            return response;

        }

        public LadderHookInfoResponse StartHook(Guid managerId, string siteId, int maxTimes, int minScore, int maxScore, int winTimes)
        {
            if (maxTimes < 1 && minScore < 1 && maxScore < 1 && winTimes < 1)
            {
                return ResponseHelper.Create<LadderHookInfoResponse>(MessageCode.NbParameterError);
            }
            if (IsExists(managerId))
            {
                return ResponseHelper.Create<LadderHookInfoResponse>(MessageCode.Success);
            }

            var hook = CrossladderHookMgr.GetById(managerId, siteId);
            var nextMatchTime = DateTime.Now.AddSeconds(_ladderHookCD);
            var curTime = DateTime.Now;
            hook.UpdateTime = curTime;
            hook.RowTime = curTime;
            hook.NextMatchTime = nextMatchTime;
            hook.Status = 0;
            hook.CurTimes = 0;
            hook.CurWiningTimes = 0;
            hook.MaxTimes = maxTimes;
            hook.MinScore = minScore;
            hook.MaxScore = maxScore;
            hook.MaxWiningTimes = winTimes;
            hook.Expired = curTime.AddMinutes(_hookExpired);
            if (CrossladderHookMgr.Update(hook))
            {
                AddToDic(hook);
                return BuildHookInfoResponse(hook, true);
            }
            else
            {
                return ResponseHelper.Create<LadderHookInfoResponse>(MessageCode.NbUpdateFail);
            }
        }

        public MessageCodeResponse StopHook(Guid managerId,string siteId)
        {
            if (!IsExists(managerId))
            {
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
            }
            if (HookEnd(managerId, EnumHookStatus.Stop))
            {
                var thread = GetThread(siteId);
                if (thread != null)
                {
                    thread.Leave(managerId);
                }
                return ResponseHelper.CreateSuccess<MessageCodeResponse>();
            }
            return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail);
        }

        public bool IsExists(Guid managerId)
        {
            return _hookDic.ContainsKey(managerId);
        }
        /// <summary>
        /// 更新挂机信息
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="score">获得积分</param>
        /// <param name="isWin">是否胜利</param>
        /// <param name="homtScore">主队进球数</param>
        /// <param name="awayScore">客队进球数</param>
        /// <param name="homeName">主队名</param>
        /// <param name="awayName">客队名</param>
        /// <param name="myCoin">获得金币</param>
        public void UpdateHookScore(Guid managerId, int score, bool isWin, int homtScore, int awayScore, string homeName, string awayName, int myCoin)
        {
            try
            {
                CrossladderHookEntity entity = null;
                _hookDic.TryGetValue(managerId, out entity);
                if (entity != null)
                {
                    if (entity.LadderManager != null)
                    {
                        entity.LadderManager.Score += score;
                        entity.Score = entity.LadderManager.Score;
                    }
                    else
                    {
                        entity.Score += score;
                    }
                    entity.CurTimes++;
                    if (isWin)
                        entity.CurWiningTimes++;
                    try
                    {

                        if (_hookListDic == null)
                            _hookListDic = new ConcurrentDictionary<Guid, List<LadderHook>>();
                        if (!_hookListDic.ContainsKey(managerId))
                            _hookListDic.TryAdd(managerId, new List<LadderHook>());
                        if (_hookListDic[managerId].Count >= 10)
                            _hookListDic[managerId].RemoveAt(0);
                        LadderHook hookrecord = new LadderHook();
                        hookrecord.HomeName = homeName;
                        hookrecord.AwayName = awayName;
                        hookrecord.HomeScore = homtScore;
                        hookrecord.AwayScore = awayScore;
                        hookrecord.MyCoin = myCoin;
                        hookrecord.MyIntegral = score;
                        _hookListDic[managerId].Add(hookrecord);
                    }
                    catch (Exception e)
                    {

                    }
                    if (HookEndCheck(entity))
                    {
                        HookEnd(managerId, entity.Status);
                    }
                    else
                    {
                        CrossladderHookMgr.Update(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("UpdateLadderHookInfo", ex);
            }
        }

        void Hook(CrossladderHookEntity entity)
        {
            if (HookEndCheck(entity))
            {
                _finishList.Add(entity);
                return;
            }
            DateTime compareTime = DateTime.Now;
            if (compareTime < entity.NextMatchTime)
                return;
            entity.NextMatchTime = compareTime.AddSeconds(_ladderHookCD);
            var thread = GetThread(entity.SiteId);
            if (thread != null)
            {
                thread.HookAttend(entity);
            }
        }

        void doFinish()
        {
            if (_finishList != null && _finishList.Count > 0)
            {
                foreach (var entity in _finishList)
                {
                    HookEnd(entity.ManagerId, entity.Status);
                }
            }
        }

        bool AddToDic(CrossladderHookEntity entity)
        {
            var response =CrossLadderCore.Instance.GetLadderManager(entity.SiteId, entity.ManagerId);
            if (response.Code != (int)MessageCode.Success)
            {
                return false;
            }
            var arenaManager =response.Data;
            if (arenaManager == null)
                return false;
            arenaManager.IsBot = false;
            arenaManager.UpdateTime = DateTime.Now;
            arenaManager.IsHook = true;
            entity.Score = arenaManager.Score;
            entity.Status = (int)EnumHookStatus.Run;
            entity.LadderManager = arenaManager;

            if (_hookDic.ContainsKey(entity.ManagerId))
                _hookDic[entity.ManagerId] = entity;
            else
            {
                _hookDic.TryAdd(entity.ManagerId, entity);
            }

            return true;
        }

        bool HookEnd(Guid managerId, EnumHookStatus hookStatus)
        {
            return HookEnd(managerId, (int)hookStatus);
        }

        bool HookEnd(Guid managerId, int hookStatus)
        {
            if (CrossladderHookMgr.End(managerId, hookStatus))
            {
                var e = new CrossladderHookEntity();
                _hookDic.TryRemove(managerId, out e);
                return true;
            }
            return false;
        }

        bool HookEndCheck(CrossladderHookEntity entity)
        {
            if (entity.Status != (int)EnumHookStatus.Run)
            {
                return true;
            }
            if (entity.Expired <= DateTime.Now)
                return true;
            int local = entity.Status;
            entity.Status = (int)EnumHookStatus.Finish;
            if (entity.MaxTimes > 0 && entity.CurTimes >= entity.MaxTimes)
                return true;
            if (entity.MaxWiningTimes > 0 && entity.CurWiningTimes >= entity.MaxWiningTimes)
                return true;
            if (entity.Score > 0)
            {
                if (entity.MaxScore > 0 && entity.Score >= entity.MaxScore)
                    return true;
                if (entity.MinScore > 0 && entity.Score <= entity.MinScore)
                    return true;
            }
            if (entity.MaxTimes < 1 && entity.MinScore < 1 && entity.MaxScore < 1 && entity.MaxWiningTimes < 1)
            {
                return true;
            }
            entity.Status = local;
            return false;
        }

        LadderHookInfoResponse BuildHookInfoResponse(CrossladderHookEntity entity, bool isHook)
        {
            var response = ResponseHelper.CreateSuccess<LadderHookInfoResponse>();
            response.Data = new LadderHookInfoEntity();
            response.Data.IsHook = isHook;
            response.Data.CurTimes = entity.CurTimes;
            response.Data.CurWiningTimes = entity.CurWiningTimes;
            response.Data.MaxScore = entity.MaxScore;
            response.Data.MaxTimes = entity.MaxTimes;
            response.Data.MinScore = entity.MinScore;
            response.Data.LadderHookList = new List<LadderHook>();
            if (_hookListDic.ContainsKey(entity.ManagerId))
                response.Data.LadderHookList = _hookListDic[entity.ManagerId];
            var curTime = DateTime.Now;
            response.Data.NextMatchWaitSeconds = isHook ? ShareUtil.CalWaitTime(entity.NextMatchTime, curTime) : 0;
            response.Data.ExpiredTick = ShareUtil.GetTimeTick(entity.Expired);
            return response;
        }
        #endregion
    }
}
