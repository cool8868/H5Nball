using System;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Core.CrossCrowd
{
    public class CrossCrowdCore
    {

        private readonly int _initMorale =100;
        #region .ctor
        public CrossCrowdCore(int p)
        {
            _initMorale = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.CrowdMoraleInit);
        }
        #endregion

        #region Facade

        public static CrossCrowdCore Instance
        {
            get { return SingletonFactory<CrossCrowdCore>.SInstance; }
        }
        
        public CrosscrowdInfoResponse GetCrowdInfo(string siteId)
        {
            var crowd = CrossCrowdManager.Instance.GetCurrent(siteId);
            if (crowd == null)
            {
                return ResponseHelper.Create<CrosscrowdInfoResponse>(MessageCode.CrowdNoData);
            }
            crowd.StartTick = ShareUtil.GetTimeTick(crowd.StartTime);
            crowd.EndTick = ShareUtil.GetTimeTick(crowd.EndTime);
            var response = ResponseHelper.CreateSuccess<CrosscrowdInfoResponse>();
            response.Data = crowd;
            return response;
        }

        public CrosscrowdMatchResponse GetMatch(Guid managerId, Guid matchId)
        {
            var match = MemcachedFactory.CrowdMatchClient.Get<CrosscrowdMatchEntity>(matchId);
            if (match == null)
            {
                match = CrosscrowdMatchMgr.GetById(matchId);
                if (match == null)
                {
                    return ResponseHelper.InvalidParameter<CrosscrowdMatchResponse>();
                }
            }
            var pop =MemcachedFactory.CrowdMessageClient.Get<string>(managerId);
            if (pop == null)
                pop = "";
            //LogHelper.Insert(pop,LogType.Info);
            match.Pop = pop;
            var response = ResponseHelper.CreateSuccess<CrosscrowdMatchResponse>();
            response.Data = match;
            return response;
        }

        public CrosscrowdManagerResponse GetManagerInfo(string siteId, Guid managerId)
        {
            var crowd = CrossCrowdManager.Instance.GetCurrent(siteId);
            int crowdId = 0;
            if (crowd != null && crowd.Status == 1)
                crowdId = crowd.Idx;
            DateTime curTime = DateTime.Now;
            return GetManagerInfo(siteId,managerId, crowdId, curTime,-1,false);
        }

        public CrosscrowdManagerResponse GetManagerInfo(string siteId, Guid managerId, int crowdId, DateTime curTime, int curPoint = -1, bool checkStatus = true)
        {
            var crowdManager = InnerGetManager(siteId, managerId, crowdId, curTime);
            if(crowdManager==null)
                return ResponseHelper.Create<CrosscrowdManagerResponse>(MessageCode.NbFunctionNotOpen);
            if (checkStatus)
            {
                if (crowdManager.Morale <= 0)
                {
                    return ResponseHelper.Create<CrosscrowdManagerResponse>(MessageCode.CrowdNoMorale);
                }
                var code = MatchCdHandler.CheckCd(managerId, EnumMatchType.CrossCrowd);
                if (code != MessageCode.Success)
                {
                    return ResponseHelper.Create<CrosscrowdManagerResponse>(MessageCode.NbMatchCd);
                }
                crowdManager.ShowName = ShareUtil.GetCrossManagerNameByZoneId(siteId, crowdManager.Name);
            }
            else
            {
                crowdManager.CdSeconds = MatchCdHandler.GetCdMilSecondsInt(managerId, EnumMatchType.CrossCrowd);
                crowdManager.CurPoint = curPoint;
                crowdManager.ClearCdPoint = CacheFactory.MallCache.GetDirectPoint(EnumConsumeSourceType.CrowdClearCd, 0);//crowdManager.ClearCdCount+1);
                crowdManager.ResurrectionPoint = CacheFactory.MallCache.GetDirectPoint(EnumConsumeSourceType.CrowdResurrection);
                if (crowdManager.Morale < 1)
                {
                    crowdManager.ResurrectionCdSeconds = ShareUtil.CalCountdown(crowdManager.ResurrectionTime, curTime);
                }
                else
                {
                    crowdManager.ResurrectionCdSeconds = -1;
                }
            }
            var response = ResponseHelper.Create<CrosscrowdManagerResponse>(MessageCode.Success);
            response.Data = crowdManager;
            return response;
        }

        public CrosscrowdManagerResponse ClearCd(string siteId, Guid managerId)
        {
            return doAction(siteId, managerId,EnumConsumeSourceType.CrowdClearCd, true);
        }

        public CrosscrowdManagerResponse Resurrection(string siteId, Guid managerId)
        {
            return doAction(siteId, managerId, EnumConsumeSourceType.CrowdResurrection, false);
        }

        public bool CheckSite(string siteId)
        {
            return true;
        }
        #endregion
        
        #region encapsulation

        CrosscrowdManagerResponse doAction(string siteId, Guid managerId,EnumConsumeSourceType mallDirectType,bool isClearCd)
        {
            var crowd = CrossCrowdManager.Instance.GetCurrent(siteId);
            if (crowd == null || crowd.Status != 1)
                return ResponseHelper.Create<CrosscrowdManagerResponse>(MessageCode.CrowdNoData);
            DateTime curTime = DateTime.Now;
            CrosscrowdManagerEntity crowdManager = InnerGetManager(siteId,managerId, crowd.Idx, curTime);
            if (isClearCd)
            {
                var cd = MatchCdHandler.GetCdSecondsInt(managerId, EnumMatchType.CrossCrowd);
                if (cd <= 0)
                {
                    return GetManagerInfo(siteId, managerId, crowd.Idx, DateTime.Now);
                }
                crowdManager.ClearCdCount++;
            }
            else
            {
                if (crowdManager.Morale <= 0)
                {
                    crowdManager.Morale = _initMorale;
                    crowdManager.ResurrectionCount++;
                    //crowdManager.NextMatchTime = ShareUtil.BaseTime;
                }
                else
                {
                    return ResponseHelper.Create<CrosscrowdManagerResponse>(MessageCode.CrowdHasMorale);
                }
            }
            var mallDirect = new CrossMallDirectFrame(siteId, managerId, mallDirectType,0);// crowdManager.ClearCdCount);
            var checkCode = mallDirect.Check();
            if (checkCode != MessageCode.Success)
                return ResponseHelper.Create<CrosscrowdManagerResponse>(checkCode);
            crowdManager.UpdateTime = curTime;
            if (!CrosscrowdManagerMgr.Update(crowdManager))
            {
                return ResponseHelper.Create<CrosscrowdManagerResponse>(MessageCode.NbUpdateFail);
            }

            MatchCdHandler.Delete(managerId, EnumMatchType.CrossCrowd);
            checkCode = mallDirect.Save(Guid.NewGuid().ToString());
            if (checkCode != MessageCode.Success)
            {
                SystemlogMgr.Error("CrossCrowd-doaction", string.Format("action:{0}", mallDirectType.ToString()));
            }
            return GetManagerInfo(siteId, managerId, crowd.Idx, DateTime.Now, mallDirect.RemainPoint, false);
        }

        CrosscrowdManagerEntity InnerGetManager(string siteId,Guid managerId, int crowdId, DateTime curTime)
        {
            var crowdManager = CrosscrowdManagerMgr.GetById(managerId);
            int domainId = 1;
            if (crowdManager == null)
            {
                var manager = NbManagerMgr.GetById(managerId, siteId);
                if (manager == null)
                    return null;
                //if (!ManagerUtil.CheckFunction(siteId, managerId, EnumOpenFunction.Crowd))
                //    return null;
                CrossSiteCache.Instance().TryGetDomainId(siteId, out domainId);
                crowdManager = new CrosscrowdManagerEntity();
                crowdManager.DomainId = domainId;
                crowdManager.SiteId = siteId;
                crowdManager.SiteName = CacheFactory.FunctionAppCache.GetCrossZoneName(siteId);
                crowdManager.Name = manager.Name;
                crowdManager.Logo = manager.Logo;
                crowdManager.CrossCrowdId = crowdId;
                crowdManager.Morale = _initMorale;
                crowdManager.ManagerId = managerId;
                crowdManager.RowTime = curTime;
                crowdManager.UpdateTime = curTime;
                crowdManager.ScoreUpdateTime = curTime;
                crowdManager.ResurrectionTime = ShareUtil.BaseTime;
                crowdManager.NextMatchTime = ShareUtil.BaseTime;
                crowdManager.Kpi = ManagerUtil.GetKpi(managerId, siteId);
                CrosscrowdManagerMgr.Insert(crowdManager);
            }
            else if (crowdId > 0)
            {
                if (crowdManager.CrossCrowdId != crowdId)
                {
                    CrossSiteCache.Instance().TryGetDomainId(siteId, out domainId);
                    crowdManager.DomainId = domainId;
                    crowdManager.CrossCrowdId = crowdId;
                    crowdManager.KillNumber = 0;
                    crowdManager.ByKillNumber = 0;
                    crowdManager.Morale = _initMorale;
                    crowdManager.Score = 0;
                    crowdManager.ScoreUpdateTime = curTime;
                    crowdManager.UpdateTime = curTime;
                    crowdManager.WinningCount = 0;
                    crowdManager.ResurrectionCount = 0;
                    crowdManager.ResurrectionAuto = 0;
                    crowdManager.ClearCdCount = 0;
                    crowdManager.Kpi = ManagerUtil.GetKpi(managerId,siteId);
                    
                    CrosscrowdManagerMgr.Update(crowdManager);
                }
                else if (crowdManager.Morale<=0)
                {
                    if (curTime >= crowdManager.ResurrectionTime)
                    {
                        crowdManager.Morale = _initMorale;
                        crowdManager.UpdateTime = curTime;
                        crowdManager.ResurrectionAuto++;
                        CrosscrowdManagerMgr.Update(crowdManager);
                    }
                    else
                    {
                        crowdManager.Morale = 0;
                    }
                }
            }
            return crowdManager;
        }

        
        #endregion
    }
}
