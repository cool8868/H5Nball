using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.Rank;
using Games.NBall.Entity.Response.Rank;

namespace Games.NBall.Core.Rank
{
    public class CrossRankThread
    {
        /// <summary>
        /// domainId>>rankType>>handler
        /// </summary>
        private Dictionary<int, Dictionary<int, RankHandler>> _rankHandlers;
        /// <summary>
        /// siteId>>rankType>>handler
        /// </summary>
        private Dictionary<string, Dictionary<int, RankHandler>> _siterankHandlers;
        private DateTime _rankRecordDate;
        #region .ctor

        public CrossRankThread(int b)
        {
            _rankRecordDate = DateTime.Today;
            _rankHandlers = new Dictionary<int, Dictionary<int, RankHandler>>();
            _siterankHandlers = new Dictionary<string, Dictionary<int, RankHandler>>();
            var domainList = CrossSiteCache.Instance().GetDomainList();
            if (domainList != null && domainList.Count > 0)
            {
                foreach (var i in domainList)
                {
                    if (i > 0)
                    {
                        _rankHandlers.Add(i, new Dictionary<int, RankHandler>());
                        var rankHandler1 = new RankHandler(EnumRankType.CrossCrowdRank, i);
                        var rankHandler2 = new RankHandler(EnumRankType.CrossLadderRank, i);
                        var rankHandler3 = new RankHandler(EnumRankType.CrossLadderDailyRank, i);

                        _rankHandlers[i].Add((int)EnumRankType.CrossCrowdRank, rankHandler1);
                        _rankHandlers[i].Add((int)EnumRankType.CrossLadderRank, rankHandler2);
                        _rankHandlers[i].Add((int)EnumRankType.CrossLadderDailyRank, rankHandler3);

                        var siteList = CrossSiteCache.Instance().GetSiteListByDomain(i);
                        if (siteList != null && siteList.Count > 0)
                        {
                            foreach (var site in siteList)
                            {
                                _siterankHandlers.Add(site, new Dictionary<int, RankHandler>());
                                _siterankHandlers[site].Add((int)EnumRankType.CrossCrowdRank, rankHandler1);
                                _siterankHandlers[site].Add((int)EnumRankType.CrossLadderRank, rankHandler2);
                                _siterankHandlers[site].Add((int)EnumRankType.CrossLadderDailyRank, rankHandler3);
                            }
                        }
                    }
                }
            }

        }

        #endregion

        #region Facade
        public static CrossRankThread Instance
        {
            get { return SingletonFactory<CrossRankThread>.SInstance; }
        }

        public MessageCode RunJob(DateTime nextTime)
        {
            try
            {
                if (DateTime.Today != _rankRecordDate)
                {
                    RankRecord();
                    _rankRecordDate = DateTime.Today;
                }
                foreach (var domainRanks in _rankHandlers.Values)
                {
                    foreach (var rankHandler in domainRanks.Values)
                    {
                        rankHandler.RunJob(nextTime);
                    }
                }
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossRankThread-RunJob", ex);
                return MessageCode.Exception;
            }
        }

        public RankResponse GetRanking(string siteId, Guid managerId, int rankType, int pageIndex, int pageSize)
        {
            if (rankType == (int) EnumRankType.ArenaRank)
                return GetArenaRank(siteId, managerId, rankType, pageIndex, pageSize);
            RankHandler handler = GetHandler(siteId, rankType);
            if (handler != null)
            {
                var response = handler.GetResponse(managerId, pageIndex, pageSize);
                if (response != null && response.Data != null)
                {
                    response.Data.RankType = rankType;
                    var zoneInfo = CacheFactory.ZoneCache.GetZoneInfo(siteId);
                    if (zoneInfo != null)
                        response.Data.MyZoneName = zoneInfo.Name;
                }
                return response;
            }
            else
            {
                return ResponseHelper.InvalidParameter<RankResponse>();
            }
        }

        public BaseRankEntity GetMyRank(string siteId,Guid managerId,int rankType)
        {
            RankHandler handler = GetHandler(siteId,rankType);
            if (handler != null)
            {
                return handler.GetMyRankEntity(managerId);
            }
            else
            {
                return null;
            }
        }

        public RankResponse GetArenaRank(string siteId, Guid managerId, int rankType, int pageIndex, int pageSize)
        {
            RankResponse response = new RankResponse();
            response.Data = new RankDataEntity();
            try
            {
                var arenaList = ArenaCore.Instance.GetRankList(siteId);
                var resultList = new List<BaseRankEntity>();
                int index = 0;
                foreach (var item in arenaList)
                {
                    index ++;
                    if (index > pageSize)
                        break;
                    BaseRankEntity entity = new RankArenaEntity()
                    {
                        Integral = item.Integral,
                        Logo = item.Logo,
                        ManagerId = item.ManagerId,
                        ZoneName = item.ZoneName,
                        SiteId = item.SiteId,
                        Rank = item.Rank,
                        Name = item.ZoneName + "." + item.ManagerName,
                    };
                    resultList.Add(entity);
                }
                response.Data.Ranks = resultList;
                response.Data.RankType = rankType;
                var myData = ArenaCore.Instance.GetRankInfo(managerId, siteId);
                if (myData == null)
                {
                    response.Data.MyRank = -1;
                    response.Data.MyData = 0;
                    response.Data.MyZoneName = "";
                    return response;
                }
                response.Data.MyRank = myData.Rank;
                if (response.Data.MyRank == 0)
                    response.Data.MyRank = -1;
                response.Data.MyData = myData.Integral; //积分
                response.Data.MyZoneName = myData.ZoneName + "." + myData.ManagerName;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取竞技场排名", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        RankHandler GetHandler(string siteId, int rankType)
        {
            if (!_siterankHandlers.ContainsKey(siteId))
            {
                return null;
            }
            if (!_siterankHandlers[siteId].ContainsKey(rankType))
            {
                return null;
            }
            return _siterankHandlers[siteId][rankType];
        }
        #endregion

        void RankRecord()
        {
            
        }
    }
}

