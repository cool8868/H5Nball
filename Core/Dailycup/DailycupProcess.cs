using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Manager;
//using Games.NBall.Core.Match;
using Games.NBall.Core.Match;
using Games.NBall.Core.Task;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Bll;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;

namespace Games.NBall.Core.Dailycup
{
    public class DailycupProcess
    {
        List<List<DailycupMatchEntity>> _matches;
        DailycupInfoEntity _dailycup;
        Dictionary<Guid, DailycupCompetitorsEntity> _CompetitorIndex;
        List<DailycupCompetitorsEntity> _Competitors;
        private Dictionary<Guid, NbMatchstatEntity> _matchstatDic; 

        /// <summary>
        /// constuctor
        /// </summary>
        /// <param name="competitors">报名人员</param>
        /// <param name="dailycup">The dailycup.</param>
        public DailycupProcess(List<DailycupCompetitorsEntity> competitors, DailycupInfoEntity dailycup)
        {
            _CompetitorIndex = CreateIndex(competitors);
            _dailycup = dailycup;
            _dailycup.Round = DailycupRound(competitors.Count);
            _matches = new List<List<DailycupMatchEntity>>();
            _Competitors = competitors;
            _matchstatDic = new Dictionary<Guid, NbMatchstatEntity>(competitors.Count);
            foreach (var entity in competitors)
            {
                if(!_matchstatDic.ContainsKey(entity.ManagerId))
                    _matchstatDic.Add(entity.ManagerId,new NbMatchstatEntity(){ManagerId = entity.ManagerId,Draw = 0,Lose = 0,Win = 0});
            }
        }

        /// <summary>
        /// Starts the dailycup.
        /// </summary>
        public MessageCode StartDailycup()
        {
            try
            {
                //开始比赛
                _matches.Add(CreateFirstRound(_Competitors));
                BeginDailycup();

                SaveDailycup();
                //结束
                _dailycup.Status = (int)EnumDailycupStatus.End;
                _dailycup.UpdateTime = DateTime.Now;
                DailycupInfoMgr.Update(_dailycup);
                SaveMatchStat();
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("StartDailycup", ex);
                return MessageCode.Exception;
            }
            
        }

        void SaveMatchStat()
        {
            try
            {
                int matchType = (int) EnumMatchType.Dailycup;
                foreach (var entity in _matchstatDic.Values)
                {
                    MatchCore.SaveMatchStat(entity.ManagerId, matchType, entity.Win, entity.Lose, entity.Draw, entity.Goals);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("DailycupProcess SaveMatchStat", ex);
            }
        }

        MessageCode SaveDailycup()
        {
            try
            {
                DailyCupCompetitorsDataSet.DailyCup_CompetitorsDataTable competitorsData = new DailyCupCompetitorsDataSet.DailyCup_CompetitorsDataTable();
                foreach (var entity in _Competitors)
                {
                    var row = competitorsData.NewRow();
                    row["Idx"] = entity.Idx;
                    row["DailyCupId"] = entity.DailyCupId;
                    row["ManagerId"] = entity.ManagerId;
                    row["ManagerName"] = entity.ManagerName;
                    row["Logo"] = entity.Logo;
                    row["MaxRound"] = entity.MaxRound;
                    row["WinCount"] = entity.WinCount;
                    row["Rank"] = entity.Rank;
                    row["PrizeScore"] = entity.PrizeScore;
                    row["PrizeSophisticate"] = entity.PrizeSophisticate;
                    row["PrizeCoin"] = entity.PrizeCoin;
                    row["Status"] = entity.Status;
                    row["RowTime"] = entity.RowTime;
                    competitorsData.Rows.Add(row);

                    
                }
                DailycupSqlHelper.SaveCompetitorsFight(competitorsData);

                DailycupMatchDataSet.DailyCup_MatchDataTable matchData = new DailycupMatchDataSet.DailyCup_MatchDataTable();
                foreach (var matchList in _matches)
                {
                    foreach (var entity in matchList)
                    {
                        var row = matchData.NewRow();
                        row["DailyCupId"] = entity.DailyCupId;
                        row["HomeManager"] = entity.HomeManager;
                        row["HomeName"] = entity.HomeName;
                        row["HomeLogo"] = entity.HomeLogo;
                        row["AwayLogo"] = entity.AwayLogo;
                        row["Idx"] = entity.Idx;
                        row["Round"] = entity.Round;
                        row["ChipInCount"] = entity.ChipInCount;
                        row["RowTime"] = entity.RowTime;
                        row["Status"] = entity.Status;
                        row["HomeLevel"] = entity.HomeLevel;
                        row["HomePower"] = entity.HomePower;
                        row["HomeWorldScore"] = entity.HomeWorldScore;
                        row["AwayManager"] = entity.AwayManager;
                        row["AwayName"] = entity.AwayName;
                        row["AwayLevel"] = entity.AwayLevel;
                        row["AwayPower"] = entity.AwayPower;
                        row["AwayWorldScore"] = entity.AwayWorldScore;
                        row["AwayScore"] = entity.AwayScore;
                        row["HomeScore"] = entity.HomeScore;
                        matchData.Rows.Add(row);
                    }
                }
                DailycupSqlHelper.SaveDailycupMatchBulk(matchData);
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveDailycup", ex);
                return MessageCode.Exception;
            }
        }

        void AddMatchStat(DailycupMatchEntity matchEntity)
        {
            AddMatchStat(matchEntity.HomeManager,matchEntity.HomeScore,matchEntity.AwayScore);
            AddMatchStat(matchEntity.AwayManager,matchEntity.AwayScore,matchEntity.HomeScore);
        }

        void AddMatchStat(Guid managerId, int homeScore, int awayScore)
        {
            if (homeScore > awayScore)
                _matchstatDic[managerId].Win++;
            else if (homeScore == awayScore)
                _matchstatDic[managerId].Draw++;
            else
            {
                _matchstatDic[managerId].Lose++;
            }
            _matchstatDic[managerId].Goals += homeScore;

        }

        /// <summary>
        /// Creates the index.
        /// </summary>
        /// <param name="competitors">The competitors.</param>
        /// <returns></returns>
        private Dictionary<Guid, DailycupCompetitorsEntity> CreateIndex(List<DailycupCompetitorsEntity> competitors)
        {
            Dictionary<Guid, DailycupCompetitorsEntity> dic = new Dictionary<Guid, DailycupCompetitorsEntity>();
            foreach (DailycupCompetitorsEntity entity in competitors)
            {
                    var manager = ManagerCore.Instance.GetManager(entity.ManagerId, true);
                    if (manager != null)
                    {
                        entity.Level = manager.Level;
                        entity.kpi = manager.Kpi;
                        entity.WorldScore = manager.Score;
                    }
                    if (!dic.ContainsKey(entity.ManagerId))
                        dic.Add(entity.ManagerId, entity);

                
            }
            return dic;
        }

        private int DailycupRound(int competitorCount)
        {
            double log = Math.Log(competitorCount, 2);
            if (log < 0)
            {
                return 0;
            }
            if (log == (int)log)
            {
                return (int)log;
            }
            return (int)log + 1;
        }

        /// <summary>
        /// Creates the first round.
        /// </summary>
        /// <param name="competitors">The competitors.</param>
        /// <returns></returns>
        private List<DailycupMatchEntity> CreateFirstRound(List<DailycupCompetitorsEntity> competitors)
        {
            //_MaxRound = 
            int count = competitors.Count;
            int matchCount = (int)Math.Pow(2, _dailycup.Round - 1);
            List<DailycupMatchEntity> firstRound = new List<DailycupMatchEntity>();
            for (int i = 0; i < matchCount; i++)
            {
                DailycupCompetitorsEntity away = null;
                if (matchCount * 2 - i <= count) //非轮空选手
                {
                    away = competitors[matchCount * 2 - i - 1];
                }
                DailycupMatchEntity match = Fight(competitors[i], away, 1);
                firstRound.Add(match);

            }
            return firstRound;
        }



        /// <summary>
        /// Begins the dailycup.
        /// </summary>
        private void BeginDailycup()
        {
            for (int i = 0; i < _dailycup.Round - 1; i++)
            {
                //取出上一轮，
                List<DailycupMatchEntity> preMatchs = _matches[i];
                _matches.Add(RunDailycupRound(preMatchs, i + 2));//从第2轮开始算
            }
        }

        /// <summary>
        /// Runs the dailycup round.
        /// </summary>
        /// <param name="matchs">The matchs.</param>
        /// <param name="round">The round.</param>
        /// <returns></returns>
        private List<DailycupMatchEntity> RunDailycupRound(List<DailycupMatchEntity> matchs, int round)
        {
            List<DailycupMatchEntity> list = new List<DailycupMatchEntity>();
            for (int i = 0; i < matchs.Count; i = i + 2) //roundcount必定是2的倍数！
            {
                //邻近用户比赛   
                DailycupMatchEntity matchResult = Fight(GetWinner(matchs[i]), GetWinner(matchs[i + 1]), round);
                list.Add(matchResult);
            }
            return list;
        }

        /// <summary>
        /// 根据比赛结果对象得出 出线者
        /// </summary>
        /// <param name="match">比赛对象</param>
        /// <returns>报名者对象</returns>
        private DailycupCompetitorsEntity GetWinner(DailycupMatchEntity match)
        {
            if (match.HomeScore >= match.AwayScore)
            {
                return _CompetitorIndex[match.HomeManager];
            }
            return _CompetitorIndex[match.AwayManager];
        }

        DailycupMatchEntity Fight(DailycupCompetitorsEntity home, DailycupCompetitorsEntity away, int round)
        {
            DailycupMatchEntity dailycupMatch = new DailycupMatchEntity();

            dailycupMatch.DailyCupId = _dailycup.Idx;
            dailycupMatch.HomeManager = home.ManagerId;
            dailycupMatch.HomeName = home.ManagerName;
            dailycupMatch.HomeLogo = home.Logo;
            
            dailycupMatch.Idx = ShareUtil.GenerateComb();
            dailycupMatch.Round = round;
            dailycupMatch.ChipInCount = 0;
            dailycupMatch.RowTime = DateTime.Now;
            dailycupMatch.Status = 0;
            dailycupMatch.HomeLevel = home.Level;
            dailycupMatch.HomePower = home.kpi;
            dailycupMatch.HomeWorldScore = home.WorldScore;
            if (away == null) //轮空
            {
                dailycupMatch.AwayManager = Guid.Empty;
                dailycupMatch.AwayName = "";
                dailycupMatch.AwayScore = 0;
                dailycupMatch.HomeScore = 0;
                dailycupMatch.AwayLogo = "";
            }
            else
            {
                try
                {
                    home.MaxRound = round;
                    away.MaxRound = round;
                    home.Rank = _dailycup.Round-round+1;
                    away.Rank = _dailycup.Round - round + 1;
                    var matchHome = new MatchManagerInfo(home.ManagerId, false, false);
                    var matchAway = new MatchManagerInfo(away.ManagerId, false, false);
                    var matchData = new BaseMatchData((int)EnumMatchType.Dailycup, dailycupMatch.Idx, matchHome, matchAway);
                    matchData.ErrorCode = (int)MessageCode.MatchWait;
                    matchData.NoDraw = true;
                    MemcachedFactory.MatchClient.Set(matchData.MatchId, matchData);

                    MatchCore.CreateMatch(matchData);
                    ////测试用 ------------
                    //matchData.ErrorCode = (int)MessageCode.Success;
                    //matchData.Home.Score = 5;
                    //matchData.Away.Score = 2;

                    if (matchData.ErrorCode == (int)MessageCode.Success)
                    {
                        dailycupMatch.AwayManager = away.ManagerId;
                        dailycupMatch.AwayName = away.ManagerName;
                        dailycupMatch.AwayLevel = away.Level;
                        dailycupMatch.AwayLogo = away.Logo;
                        dailycupMatch.AwayPower = away.kpi;
                        dailycupMatch.AwayWorldScore = away.WorldScore;
                        dailycupMatch.HomeScore = matchData.Home.Score;
                        dailycupMatch.AwayScore = matchData.Away.Score;

                        if (matchData.Away.Score > matchData.Home.Score)
                        {
                            away.WinCount++;
                            if (round == _dailycup.Round)
                                away.Rank = -1;
                        }
                        else
                        {
                            home.WinCount++;
                            if (round == _dailycup.Round)
                                home.Rank = -1;
                        }
                        AddMatchStat(dailycupMatch);
                    }
                    else
                    {
                        dailycupMatch.AwayManager = Guid.Empty;
                        dailycupMatch.AwayName = "";
                        dailycupMatch.AwayScore = 0;
                        dailycupMatch.HomeScore = 0;
                        dailycupMatch.AwayLogo = "";
                        SystemlogMgr.Error("Create Dailycup", string.Format("round:{0},homeId:{1},awayId:{2},code:{3}", round, home.Idx, away.Idx,matchData.ErrorCode));
                        return dailycupMatch;
                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("Create Dailycup",string.Format("round:{0},homeId:{1},awayId:{2}",round,home.Idx,away.Idx));
                    throw ex;
                }
            }
            return dailycupMatch;
        }
    }
}
