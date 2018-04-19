using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Dal.Xsd
{
    public class DailycupSqlHelper
    {
        public static string ConnectionString
        {
            get { return ConnectionFactory.Instance.GetDefault(); }
        }

        public static void SaveCompetitorsFight(DailyCupCompetitorsDataSet.DailyCup_CompetitorsDataTable competitorsData)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = SqlBatchHelper.GetMyCommand(con, CommandType.StoredProcedure, "C_DailyCupCompetitors_SaveFight");
                cmd.AddMyParameter("@Idx", SqlDbType.Int, competitorsData.IdxColumn.ColumnName);
                cmd.AddMyParameter("@MaxRound", SqlDbType.Int, competitorsData.MaxRoundColumn.ColumnName);
                cmd.AddMyParameter("@WinCount", SqlDbType.Int, competitorsData.WinCountColumn.ColumnName);
                cmd.AddMyParameter("@Rank", SqlDbType.Int, competitorsData.RankColumn.ColumnName);
                SqlDataAdapter adap = SqlBatchHelper.GetMyBatchAdapter(null, cmd, cmd, cmd);
                con.Open();
                adap.Update(competitorsData);
                cmd.Parameters.Clear();
            }
        }

        public static void SaveCompetitorsPrize(List<DailycupCompetitorsEntity> list)
        {
            DailyCupCompetitorsDataSet.DailyCup_CompetitorsDataTable competitorsData = new DailyCupCompetitorsDataSet.DailyCup_CompetitorsDataTable();
            foreach (var entity in list)
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
                row["PrizeItems"] = entity.PrizeItems;
                row["Status"] = entity.Status;
                row["RowTime"] = entity.RowTime;
                competitorsData.Rows.Add(row);

            }
            SaveCompetitorsPrize(competitorsData);
        }

        public static void SaveCompetitorsPrize(DailyCupCompetitorsDataSet.DailyCup_CompetitorsDataTable competitorsData)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = SqlBatchHelper.GetMyCommand(con, CommandType.StoredProcedure, "C_DailyCupCompetitors_SavePrize");
                cmd.AddMyParameter("@Idx", SqlDbType.Int, competitorsData.IdxColumn.ColumnName);
                cmd.AddMyParameter("@ManagerId", SqlDbType.UniqueIdentifier, competitorsData.ManagerIdColumn.ColumnName);
                cmd.AddMyParameter("@PrizeScore", SqlDbType.Int, competitorsData.PrizeScoreColumn.ColumnName);
                cmd.AddMyParameter("@PrizeSophisticate", SqlDbType.Int, competitorsData.PrizeSophisticateColumn.ColumnName);
                cmd.AddMyParameter("@PrizeCoin", SqlDbType.Int, competitorsData.PrizeCoinColumn.ColumnName);
                cmd.AddMyParameter("@PrizeItems", SqlDbType.VarChar, competitorsData.PrizeItemsColumn.ColumnName);

                SqlDataAdapter adap = SqlBatchHelper.GetMyBatchAdapter(null, cmd, cmd, cmd);
                con.Open();
                adap.Update(competitorsData);
                cmd.Parameters.Clear();
            }
        }

        public static void SaveDailycupMatchBulk(DailycupMatchDataSet.DailyCup_MatchDataTable matchData)
        {
            SqlBatchHelper.BulkInsert(ConnectionString,matchData);
        }

        public static void SaveDailycupMatchBatch(DailycupMatchDataSet.DailyCup_MatchDataTable matchData)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = SqlBatchHelper.GetMyCommand(con, CommandType.StoredProcedure, "P_DailycupMatch_Insert2");
                cmd.AddMyParameter("@DailyCupId", SqlDbType.Int, matchData.DailyCupIdColumn.ColumnName);
                cmd.AddMyParameter( "@HomeManager", SqlDbType.UniqueIdentifier, matchData.HomeManagerColumn.ColumnName);
                cmd.AddMyParameter("@AwayManager", SqlDbType.UniqueIdentifier, matchData.AwayManagerColumn.ColumnName);
                cmd.AddMyParameter( "@HomeName", SqlDbType.VarChar, matchData.HomeNameColumn.ColumnName);
                cmd.AddMyParameter( "@AwayName", SqlDbType.VarChar, matchData.AwayNameColumn.ColumnName);
                cmd.AddMyParameter( "@HomeLogo", SqlDbType.VarChar, matchData.HomeLogoColumn.ColumnName);
                cmd.AddMyParameter("@AwayLogo", SqlDbType.VarChar, matchData.AwayLogoColumn.ColumnName);
                cmd.AddMyParameter( "@HomeLevel", SqlDbType.Int, matchData.HomeLevelColumn.ColumnName);
                cmd.AddMyParameter( "@AwayLevel", SqlDbType.Int, matchData.AwayLevelColumn.ColumnName);
                cmd.AddMyParameter( "@HomePower", SqlDbType.Int, matchData.HomePowerColumn.ColumnName);
                cmd.AddMyParameter( "@AwayPower", SqlDbType.Int, matchData.AwayPowerColumn.ColumnName);
                cmd.AddMyParameter( "@HomeWorldScore", SqlDbType.Int, matchData.HomeWorldScoreColumn.ColumnName);
                cmd.AddMyParameter( "@AwayWorldScore", SqlDbType.Int, matchData.AwayWorldScoreColumn.ColumnName);
                cmd.AddMyParameter( "@HomeScore", SqlDbType.Int, matchData.HomeScoreColumn.ColumnName);
                cmd.AddMyParameter( "@AwayScore", SqlDbType.Int, matchData.AwayScoreColumn.ColumnName);
                cmd.AddMyParameter( "@Round", SqlDbType.Int, matchData.RoundColumn.ColumnName);
                cmd.AddMyParameter( "@ChipInCount", SqlDbType.Int, matchData.ChipInCountColumn.ColumnName);
                cmd.AddMyParameter( "@Status", SqlDbType.Int, matchData.StatusColumn.ColumnName);
                cmd.AddMyParameter( "@RowTime", SqlDbType.DateTime, matchData.RowTimeColumn.ColumnName);
                cmd.AddMyParameter("@Idx", SqlDbType.UniqueIdentifier, matchData.IdxColumn.ColumnName);

                SqlDataAdapter adap = SqlBatchHelper.GetMyBatchAdapter(null, cmd, cmd, cmd);
                con.Open();
                adap.Update(matchData);
                cmd.Parameters.Clear();
            }
        }
    }
}
