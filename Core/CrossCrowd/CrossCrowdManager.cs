using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Crowd;

namespace Games.NBall.Core.CrossCrowd
{
    public class CrossCrowdManager
    {
        private Dictionary<int, CrossCrowdThread> _crowdThreadDic;
        Dictionary<string, CrossCrowdThread> _siteCrowdThreadDic;
        #region .ctor

        public CrossCrowdManager(int p)
        {
            _crowdThreadDic = new Dictionary<int, CrossCrowdThread>();
            _siteCrowdThreadDic = new Dictionary<string, CrossCrowdThread>();
            var domainList = CrossSiteCache.Instance().GetDomainList();
            if (domainList != null && domainList.Count > 0)
            {
                foreach (var i in domainList)
                {
                    if (i > 0)
                    {
                        CrossCrowdMessage.InitBanner(i);
                        var thread = new CrossCrowdThread(i);
                        _crowdThreadDic.Add(i, thread);
                        var siteList = CrossSiteCache.Instance().GetSiteListByDomain(i);
                        if (siteList != null && siteList.Count > 0)
                        {
                            foreach (var site in siteList)
                            {
                                if (!_siteCrowdThreadDic.ContainsKey(site))
                                    _siteCrowdThreadDic.Add(site, thread);
                            }
                        }
                    }
                }
            }

        }
        #endregion

        #region Facade

        CrossCrowdThread GetThread(string siteId)
        {
            if (_siteCrowdThreadDic.ContainsKey(siteId))
                return _siteCrowdThreadDic[siteId];
            return null;
        }

        public static CrossCrowdManager Instance
        {
            get { return SingletonFactory<CrossCrowdManager>.SInstance; }
        }

        public string GetBanner(string siteId)
        {
            int domainId = 0;
            CrossSiteCache.Instance().TryGetDomainId(siteId, out domainId);
            return CrossCrowdMessage.GetBanner(domainId);
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

        public CrowdHeartResponse Heart(string siteId, Guid managerId)
        {
            var thread = GetThread(siteId);
            if (thread == null)
                return ResponseHelper.Create<CrowdHeartResponse>(MessageCode.NbDomainInvalid);
            return thread.Heart(managerId);
        }

        public CrosscrowdInfoEntity GetCurrent(string siteId)
        {
            var thread = GetThread(siteId);
            if (thread == null)
                return null;
            return thread.GetCurrent();
        }

        public MessageCode StartJob(DateTime startTime, DateTime endTime)
        {
            foreach (var thread in _crowdThreadDic.Values)
            {
                var code = thread.StartJob(startTime, endTime);
                if (code != MessageCode.Success)
                {
                    SystemlogMgr.Info("StartJob", code.ToString());
                }
            }
            return MessageCode.Success;
        }

        #endregion
    }
}
