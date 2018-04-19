using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Common;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;

namespace Games.NBall.Core.League
{
    public class LeagueFightMapFrame
    {
        private LeagueFightmapEntity _leagueFightMapEntity;
        private LeagueFightMapsEntity _leagueFightMapsEntity;
        /// <summary>
        /// 对阵记录
        /// </summary>
        private Dictionary<int, List<LeagueFight>> FightMap { get; set; }
        /// <summary>
        /// 排名列表
        /// </summary>
        private Dictionary<int,LeagueRankRecord> RankList { get; set; }
        public LeagueFightMapFrame(Guid managerId)
        {
            var fightMap = LeagueFightmapMgr.GetById(managerId);
            if (fightMap == null)
            {
                fightMap = new LeagueFightmapEntity(managerId, Guid.Empty, new byte[0], DateTime.Now, DateTime.Now);
                LeagueFightmapMgr.Insert(fightMap);
            }
            _leagueFightMapEntity = fightMap;
            AnalyseFightMap();
        }

        /// <summary>
        /// 获取经理对阵记录
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public LeagueFightmapEntity GetLeagueManagerRecord(Guid managerId)
        {
            if (_leagueFightMapEntity == null)
            {
                var fightMap = LeagueFightmapMgr.GetById(managerId);
                if (fightMap == null)
                {
                    var manager = ManagerCore.Instance.GetManager(managerId);
                    if (manager != null)
                    {
                        fightMap = new LeagueFightmapEntity(managerId, Guid.Empty, new byte[0], DateTime.Now,
                            DateTime.Now);
                        LeagueFightmapMgr.Insert(fightMap);
                    }
                }
                _leagueFightMapEntity = fightMap;
            }
            return _leagueFightMapEntity;
        }
        
        /// <summary>
        /// 获取对阵
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<LeagueFight> GetFightMap(int round)
        {
            if (_leagueFightMapEntity != null)
            {
                if (FightMap == null || FightMap.Count == 0)
                    AnalyseFightMap();
                if (FightMap == null)
                    return new List<LeagueFight>();
                if (FightMap.ContainsKey(round))
                    return FightMap[round];
            }
            return new List<LeagueFight>();
        }

        #region 对阵
        /// <summary>
        /// 获取我的对阵
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public LeagueFight GetMyFightMap(int round)
        {
            if (_leagueFightMapEntity != null)
            {
                if (FightMap == null || FightMap.Count == 0)
                    AnalyseFightMap();
                if (FightMap == null)
                    return null;
                if (FightMap.ContainsKey(round))
                {
                    var allFightMap = FightMap[round];
                    if (allFightMap != null && allFightMap.Count > 0)
                        return allFightMap.Find(r => r.H == 0 || r.A == 0);
                }
            }
            return null;
        }

        /// <summary>
        /// 设置对阵信息
        /// </summary>
        /// <param name="round"></param>
        /// <param name="homeId"></param>
        /// <param name="awayId"></param>
        /// <param name="homeGoals"></param>
        /// <param name="awayGoals"></param>
        /// <returns></returns>
        public MessageCode SetFightMap(int round, int homeId, int awayId, int homeGoals, int awayGoals)
        {
            if (_leagueFightMapEntity == null)
                return MessageCode.NbParameterError;
            if (FightMap == null || FightMap.Count == 0)
                AnalyseFightMap();
            if (FightMap == null)
                FightMap = new Dictionary<int, List<LeagueFight>>();
            if (!FightMap.ContainsKey(round))
                FightMap.Add(round, new List<LeagueFight>());
            var entity = new LeagueFight();
            entity.H = homeId;
            entity.A = awayId;
            entity.AG = awayGoals;
            entity.HG = homeGoals;
            entity.R = round;
            if (!FightMap[round].Exists(r => r.H == homeId))
                FightMap[round].Add(entity);
            return MessageCode.Success;
        }

        #endregion

        #region 排名
        /// <summary>
        /// 获取排名
        /// </summary>
        /// <returns></returns>
        public List<LeagueRankRecord> GetRank(ref int myRank,ref int myScore)
        {
            if (_leagueFightMapEntity != null)
            {
                if (RankList == null || RankList.Count == 0)
                    AnalyseFightMap();
                if (RankList == null)
                    return new List<LeagueRankRecord>();
                if (RankList.ContainsKey(0))
                {
                    myRank = RankList[0].R;
                    myScore = RankList[0].J;
                }
                return RankList.Values.ToList();
            }
            return null;
        }

        /// <summary>
        /// 设置排名信息
        /// </summary>
        /// <param name="teamId">阵型ID</param>
        /// <param name="score">增加的积分</param>
        /// <param name="matchResult">胜利类型</param>
        /// <param name="goals">进球数</param>
        /// <param name="verlusts">丢球数</param>
        /// <returns></returns>
        public MessageCode SetRankScore(int teamId,int score,int matchResult,int goals,int verlusts)
        {
            if (_leagueFightMapEntity == null)
                AnalyseFightMap();
            if (RankList.ContainsKey(teamId))
            {
                var rankEntity = RankList[teamId];
                rankEntity.J += score;
                rankEntity.C ++;
                rankEntity.G += goals;
                if (matchResult == 1)
                    rankEntity.W++;
                else if (matchResult == 3)
                    rankEntity.L ++;
                rankEntity.S += verlusts;
                rankEntity.JS = rankEntity.G - rankEntity.S;
                if (rankEntity.JS < 0)
                    rankEntity.JS = 0;
                RankList[teamId] = rankEntity;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 更新排名
        /// </summary>
        /// <returns></returns>
        public MessageCode UpdateRankList()
        {
            if (RankList.Count <= 0)
                return MessageCode.NbParameterError;
            var rankList = RankList.Values.OrderByDescending(r => r.J).ThenBy(r=>r.T).ToList();
            for (int i = 1; i <= rankList.Count; i++)
            {
                rankList[i - 1].R = i;
            }
           
            RankList = new Dictionary<int, LeagueRankRecord>();
            RankList = rankList.ToDictionary(r => r.T);
            return MessageCode.Success;
        }

        #endregion
        /// <summary>
        /// 重置对阵
        /// </summary>
        /// <returns></returns>
        public MessageCode ClearFightMap(DbTransaction trans = null)
        {
            if (_leagueFightMapEntity == null)
                return MessageCode.NbParameterError;
            _leagueFightMapEntity.FightMapString = new byte[0];
            _leagueFightMapEntity.UpdateTime = DateTime.Now;
            if (!LeagueFightmapMgr.Update(_leagueFightMapEntity, trans))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }

        /// <summary>
        /// 重置对阵  这个联赛的总人数
        /// </summary>
        /// <returns></returns>
        public MessageCode ClearFightMapStartLeague(int count)
        {
            if (_leagueFightMapEntity == null)
                return MessageCode.NbParameterError;
            FightMap = new Dictionary<int, List<LeagueFight>>();
            RankList = InitRank(count);
            _leagueFightMapEntity.FightMapString = GenerateFightMapString();
            _leagueFightMapEntity.UpdateTime = DateTime.Now;
            if (!LeagueFightmapMgr.Update(_leagueFightMapEntity))
                return MessageCode.NbUpdateFail;
            return MessageCode.Success;
        }

        public Dictionary<int, LeagueRankRecord> InitRank(int count)
        {
            var result = new Dictionary<int, LeagueRankRecord>();
            for (int i = 0; i < count; i++)
            {
                result.Add(i, new LeagueRankRecord()
                {
                    C = 0,
                    G = 0,
                    J = 0,
                    JS = 0,
                    L = 0,
                    R = i+1,
                    S = 0,
                    T = i,
                    W = 0
                });
            }
            return result;
        }

        /// <summary>
        /// 保存对阵
        /// </summary>
        /// <returns></returns>
        public bool SaveFIghtMap(DbTransaction trans = null)
        {
            var fightMapString = GenerateFightMapString();
            _leagueFightMapEntity.FightMapString = fightMapString;
            _leagueFightMapEntity.UpdateTime = DateTime.Now;
            if (!LeagueFightmapMgr.Update(_leagueFightMapEntity, trans))
                return false;
            return true;
        }

        #region 解析对阵字符串
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>成功返回true</returns>
        private void AnalyseFightMap()
        {
            _leagueFightMapsEntity = SerializationHelper.FromByte<LeagueFightMapsEntity>(_leagueFightMapEntity.FightMapString);
            if (_leagueFightMapsEntity == null)
            {
                _leagueFightMapsEntity = new LeagueFightMapsEntity();
                FightMap = new Dictionary<int, List<LeagueFight>>();
                RankList = new Dictionary<int, LeagueRankRecord>();
            }
            else
            {
                if (_leagueFightMapsEntity.FM == null)
                    FightMap = new Dictionary<int, List<LeagueFight>>();
                else
                    FightMap = _leagueFightMapsEntity.FM;
                if (_leagueFightMapsEntity.RL == null)
                    RankList = new Dictionary<int, LeagueRankRecord>();
                else
                    RankList = _leagueFightMapsEntity.RL;
            }
        }
        #endregion
        /// <summary>
        /// 获取对阵字符串
        /// </summary>
        private byte[] GenerateFightMapString()
        {
            _leagueFightMapsEntity.FM = FightMap;
            _leagueFightMapsEntity.RL = RankList;
            return SerializationHelper.ToByte(_leagueFightMapsEntity);
        }
    }

}
