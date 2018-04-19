using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.Rank;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Games.NBall.Dal.NBall
{
    public class RankProvider
    {
        public List<BaseRankEntity> GetRankList(EnumRankType rankType, int domainId)
        {
            var procName = GetProcName(rankType);
            if (string.IsNullOrEmpty(procName))
                return null;
            SqlDatabase database = null;
            if (domainId>0)
                database = new SqlDatabase(ConnectionFactory.Instance.GetConnectionString(EnumDbType.Support));
            else
            {
                database = new SqlDatabase(ConnectionFactory.Instance.GetDefault());
            }
            DbCommand commandWrapper = database.GetStoredProcCommand(procName);
            database.AddInParameter(commandWrapper, "@RankType", DbType.Int32, (int) rankType);
            if (domainId > 0)
                database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                var list = new List<BaseRankEntity>();
                while (reader.Read())
                {
                    var entity = BuildRankEntity(reader, rankType);
                    if (entity != null)
                    {
                        entity.ManagerId = (Guid) reader["ManagerId"];
                        entity.Name = (System.String) reader["Name"];
                        entity.Rank = Convert.ToInt32(reader["Rank"]);
                        entity.YesterdayRank = Convert.ToInt32(reader["YesterdayRank"]);
                        if (domainId<=0)
                        {
                            entity.VipLevel = Convert.ToInt32(reader["VipLevel"]);
                        }
                        list.Add(entity);
                    }
                }
                return list;
            }
        }

        static BaseRankEntity BuildRankEntity(IDataReader reader, EnumRankType rankType)
        {
            switch (rankType)
            {
                case EnumRankType.KpiRank:
                    return new RankKpiEntity() { Kpi = (System.Int32)reader["Kpi"], Level = (System.Int32)reader["Level"], Logo = (System.String)reader["Logo"] };
                    break;
                case EnumRankType.LevelRank:
                    return new RankLevelEntity() { Exp = (System.Int32)reader["Exp"], Level = (System.Int32)reader["Level"], Logo = (System.String)reader["Logo"] };
                    break;
                case EnumRankType.ScoreRank:
                    return new RankScoreEntity() { Kpi = (System.Int32)reader["Kpi"], Score = (System.Int32)reader["Score"] };
                    break;
                case EnumRankType.LadderRank:
                    return new RankLadderEntity() { Kpi = (System.Int32)reader["Kpi"], Score = (System.Int32)reader["Score"], Logo = (System.String)reader["Logo"] };
                    break;
                case EnumRankType.ArenaRank:
                    return new RankArenaEntity()
                    {
                        Integral = (System.Int32)reader["Integral"],
                        SiteId = (System.String)reader["SiteId"],
                        ZoneName = (System.String)reader["ZoneName"],
                        Logo = (System.String)reader["Logo"]
                    };
                    break;
                case EnumRankType.CrowdRank:
                    return new RankCrowdEntity() { KillCount = (System.Int32)reader["KillNumber"], Score = (System.Int32)reader["Score"] };
                    break;
                case EnumRankType.CrossCrowdRank:
                    return new RankCrossCrowdEntity() { Kpi = (System.Int32)reader["Kpi"], KillCount = (System.Int32)reader["KillNumber"], Score = (System.Int32)reader["Score"], SiteName = (System.String)reader["SiteName"], SiteId = (System.String)reader["SiteId"] };
                    break;
                case EnumRankType.CrossLadderRank:
                    return new RankCrossLadderEntity() { Score = (System.Int32)reader["Score"], SiteName = (System.String)reader["SiteName"], SiteId = (System.String)reader["SiteId"], Kpi = (System.Int32)reader["Kpi"], Logo = (System.String)reader["Logo"] };
                    break;
                case EnumRankType.CrossLadderDailyRank:
                    return new RankCrossLadderEntity() { Score = (System.Int32)reader["NewlyScore"], SiteName = (System.String)reader["SiteName"], SiteId = (System.String)reader["SiteId"] };
                    break;
                case EnumRankType.CrossDialDailyRank:
                    return new RankDialEntity() { Score = (long)reader["Score"], SiteName = (System.String)reader["SiteName"], SiteId = (System.String)reader["SiteId"] };
                    break;
                case EnumRankType.CrossDialWeekRank:
                    return new RankDialEntity() { Score = (long)reader["Score"], SiteName = (System.String)reader["SiteName"], SiteId = (System.String)reader["SiteId"] };
                    break;
            }
            return null;
        }

        static string GetProcName(EnumRankType rankType)
        {
            string procName = "";
            switch (rankType)
            {
                case EnumRankType.KpiRank:
                    procName = "J_Rank_Kpi";
                    break;
                case EnumRankType.LevelRank:
                    procName = "J_Rank_Level";
                    break;
                case EnumRankType.ScoreRank:
                    procName = "J_Rank_Score";
                    break;
                case EnumRankType.LadderRank:
                    procName = "J_Rank_Ladder";
                    break;
                case EnumRankType.ArenaRank:
                    procName = "J_Arena_GetRankList";
                    break;
                case EnumRankType.CrowdRank:
                    procName = "J_Rank_Crowd";
                    break;
                case EnumRankType.CrossCrowdRank:
                    procName = "J_Rank_CrossCrowd";
                    break;
                case EnumRankType.CrossLadderRank:
                    procName = "J_Rank_CrossLadder";
                    break;
                case EnumRankType.CrossLadderDailyRank:
                    procName = "J_Rank_CrossLadderDaily";
                    break;
                case EnumRankType.CrossDialDailyRank:
                    procName = "J_Rank_CrossDialDaily";
                    break;
                case EnumRankType.CrossDialWeekRank:
                    procName = "J_Rank_CrossDialWeek";
                    break;
            }
            return procName;
        }
    }
}
