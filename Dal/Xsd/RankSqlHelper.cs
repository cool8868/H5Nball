using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Rank;

namespace Games.NBall.Dal.Xsd
{
    public class RankSqlHelper
    {
        public static string ConnectionString
        {
            get { return ConnectionFactory.Instance.GetDefault(); }
        }

        public static void SaveRanking(int rankType, List<BaseRankEntity> list)
        {
            RankingYesterdayDataSet.Ranking_YesterdayDataTable dataTable = new RankingYesterdayDataSet.Ranking_YesterdayDataTable();

            foreach (var entity in list)
            {
                var row = dataTable.NewRow();
                row["ManagerId"] = entity.ManagerId;
                row["RankType"] = rankType;
                row["Rank"] = entity.Rank;
                dataTable.Rows.Add(row);
            }

            SqlBatchHelper.BulkInsert(ConnectionString, dataTable);
        }

        public static void SaveLadderRanking(int rankType, List<BaseRankEntity> list)
        {
            RankingLadderYesterdayDataSet.Ranking_LadderYesterdayDataTable dataTable = new RankingLadderYesterdayDataSet.Ranking_LadderYesterdayDataTable();

            foreach (var entity in list)
            {
                var row = dataTable.NewRow();
                row["ManagerId"] = entity.ManagerId;
                row["RankType"] = rankType;
                row["Rank"] = entity.Rank;
                dataTable.Rows.Add(row);
            }

            SqlBatchHelper.BulkInsert(ConnectionString, dataTable);
        }
    }
}
