using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.NBall;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Ladder;
//using Games.NBall.Core.Manager;
using Games.NBall.Core.Manager;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.Rank;
using Games.NBall.Entity.Response.Rank;
//using Games.NBall.Core.Arena;

namespace Games.NBall.Core.Rank
{
    public class RankHandler
    {

        private List<BaseRankEntity> _rankList;
        private int _totalRecord;
        /// <summary>
        /// managerid->rank entity
        /// </summary>
        private Dictionary<Guid, BaseRankEntity> _rankLinkDic;

        private readonly EnumRankType _rankType;
        private DateTime _nextTime;
        private int _domainId;
        #region .ctor
        public RankHandler(EnumRankType rankType,int domainId=0)
        {
            _rankType = rankType;
            _rankList = new List<BaseRankEntity>(0);
            _rankLinkDic = new Dictionary<Guid, BaseRankEntity>();
            _domainId = domainId;
        }

        #endregion

        #region Facade
        public void RunJob(DateTime nextTime)
        {
            _nextTime = nextTime;
            try
            {
                var list = RankMgr.GetRankList(_rankType, _domainId);
                    if (list == null)
                        return;
                    if (_rankType == EnumRankType.LevelRank)
                    {
                        foreach (var entity in list)
                        {
                            var levelEntity = entity as RankLevelEntity;
                            if (levelEntity != null)
                            {
                                levelEntity.Exp = levelEntity.Exp +
                                                  CacheFactory.ManagerDataCache.GetTotalExp(levelEntity.Level);
                            }
                        }
                    }
                    _rankList = list;
                    _totalRecord = _rankList.Count;
                    _rankLinkDic = _rankList.ToDictionary(d => d.ManagerId, d => d);
                
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("RankHandler run job","["+_rankType+"],"+ex.Message, ex.StackTrace);
            }
        }

        public RankResponse GetResponse(Guid managerId, int pageIndex, int pageSize)
        {
            var response = ResponseHelper.CreateSuccess<RankResponse>();
            response.Data = new RankDataEntity();
            response.Data.MyRank = -1;
            int start = pageSize * (pageIndex - 1);
            int max = pageSize;
            if (_totalRecord > 0)
            {
                if ((start + pageSize) > _totalRecord)
                    max = _totalRecord - start;
                if (max > 0)
                {
                    response.Data.Ranks = _rankList.GetRange(start, max);
                }
            }
            response.Data.NextTimeTick = ShareUtil.GetTimeTick(_nextTime);
            response.Data.TotalCount = _totalRecord;
            response.Data.TotalPage = ShareUtil.CalPageCount(_totalRecord, pageSize);
            var entity = GetMyRankEntity(managerId);
            response.Data.MyRank = entity.Rank;
            if (entity.Rank > 0)
            {
                response.Data.MyData = entity.GetData();
                response.Data.MyExtra = entity.GetExtraData();
            }
            else
            {
                response.Data.MyData = GetMyData(managerId);
            }
            return response;
        }

        int GetMyData(Guid managerId)
        {
            int myData = 0;
            switch (_rankType)
            {
                case EnumRankType.KpiRank:
                    myData = ManagerCore.Instance.GetKpi(managerId);
                    break;
                case EnumRankType.ScoreRank:
                    var manager = ManagerCore.Instance.GetManager(managerId);
                    if(manager!=null)
                        myData = manager.Score;
                    break;
                case EnumRankType.LevelRank:
                    var manager2 = ManagerCore.Instance.GetManager(managerId);
                    if (manager2 != null)
                        myData = manager2.Level;
                    break;
                case EnumRankType.LadderRank:
                    var ladder= LadderCore.Instance.GetLadderManager(managerId);
                    if (ladder != null)
                        myData = ladder.Score;
                    break;
                case EnumRankType.ArenaRank:
                    //var arena = ArenaCore.Instance.GetArenaInfo(managerId);
                    //if (arena != null)
                    //    myData = arena.Integral;
                    break;
            }
            return myData;
        }

        public BaseRankEntity GetMyRankEntity(Guid managerId)
        {
            BaseRankEntity entity = null;
            _rankLinkDic.TryGetValue(managerId, out entity);
            if (entity == null)
            {
                entity = new BaseRankEntity();
                entity.Rank = -1;
                entity.YesterdayRank = -1;
            }
            return entity;
        }

        public void SaveRanking()
        {
            if (_rankList != null)
            {
                RankSqlHelper.SaveRanking((int)_rankType,_rankList);
            }
        }

        public void SaveLadderRanking()
        {
            if (_rankList != null)
            {
                RankSqlHelper.SaveLadderRanking((int)_rankType, _rankList);
            }
        }
        #endregion

        #region encapsulation
        int GetMyRank(Guid managerId)
        {
            int myRank = -1;
            var entity = GetMyRankEntity(managerId);
            if (entity != null)
                myRank = entity.Rank;
            return myRank;
        }
        #endregion
    }
}
