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
    public class LadderSqlHelper
    {
        public static string ConnectionString
        {
            get { return ConnectionFactory.Instance.GetDefault(); }
        }

        //public static void SaveManagerPrize(List<LadderManagerhistoryEntity> list,MailinfoDataSet.Mail_InfoDataTable mailInfoDataTable,string zoneId="")
        //{
        //    var connectionString = ConnectionFactory.Instance.GetConnectionString(zoneId, EnumDbType.Main);
        //    var ladderTable = BuildManagerPrizeTable(list);
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlTransaction trans = conn.BeginTransaction();
        //        try
        //        {
        //            SaveManagerPrize(ladderTable, trans);
        //            MailSqlHelper.SaveMailBulk(mailInfoDataTable, trans);
        //            trans.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            trans.Rollback();
        //            throw ex;
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
        //    }
        //}

        //public static LadderManagerhistoryDataSet.Ladder_ManagerHistoryDataTable BuildManagerPrizeTable(List<LadderManagerhistoryEntity> list)
        //{
        //    LadderManagerhistoryDataSet.Ladder_ManagerHistoryDataTable dataTable = new LadderManagerhistoryDataSet.Ladder_ManagerHistoryDataTable();
        //    foreach (var entity in list)
        //    {
        //        var row = dataTable.NewRow();
        //        row["Idx"] = entity.Idx;
        //        row["Season"] = entity.Season;
        //        row["ManagerId"] = entity.ManagerId;
        //        row["Rank"] = entity.Rank;
        //        row["Score"] = entity.Score;
        //        row["PrizeItems"] = entity.PrizeItems;
        //        row["Status"] = entity.Status;
        //        row["RowTime"] = entity.RowTime;
        //        dataTable.Rows.Add(row);
        //    }
        //    return dataTable;
        //}

        public static void SaveManagerPrize(LadderManagerhistoryDataSet.Ladder_ManagerHistoryDataTable dataTable,SqlTransaction trans)
        {
            SqlCommand cmd = SqlBatchHelper.GetMyCommand(trans.Connection, CommandType.StoredProcedure, "C_Ladder_SavePrize");
            cmd.AddMyParameter("@Idx", SqlDbType.Int, dataTable.IdxColumn.ColumnName);
            cmd.AddMyParameter("@PrizeItems", SqlDbType.VarChar, dataTable.PrizeItemsColumn.ColumnName);
            SqlDataAdapter adap = SqlBatchHelper.GetMyBatchAdapter(null, cmd, cmd, cmd);
            cmd.Transaction = trans;
            adap.Update(dataTable);
            cmd.Parameters.Clear();
        }

        public static void SaveManagerPrize(List<LadderManagerhistoryEntity> list, MailinfoDataSet.Mail_InfoDataTable mailInfoDataTable, string zoneId = "")
        {
            var connectionString = ConnectionFactory.Instance.GetConnectionString(zoneId, EnumDbType.Main);
            var ladderTable = BuildManagerPrizeTable(list);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SaveManagerPrize(ladderTable, trans);
                    MailSqlHelper.SaveMailBulk(mailInfoDataTable, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static LadderManagerhistoryDataSet.Ladder_ManagerHistoryDataTable BuildManagerPrizeTable(List<LadderManagerhistoryEntity> list)
        {
            LadderManagerhistoryDataSet.Ladder_ManagerHistoryDataTable dataTable = new LadderManagerhistoryDataSet.Ladder_ManagerHistoryDataTable();
            foreach (var entity in list)
            {
                var row = dataTable.NewRow();
                row["Idx"] = entity.Idx;
                row["Season"] = entity.Season;
                row["ManagerId"] = entity.ManagerId;
                row["Rank"] = entity.Rank;
                row["Score"] = entity.Score;
                row["PrizeItems"] = entity.PrizeItems;
                row["Status"] = entity.Status;
                row["RowTime"] = entity.RowTime;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
