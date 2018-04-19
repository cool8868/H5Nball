using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;

namespace Games.NBall.Dal.Xsd
{
    public class OnlineSqlHelper
    {
        public static string ConnectionString
        {
            get { return ConnectionFactory.Instance.GetDefault(); }
        }

        public static List<OnlineInfoEntity> GetOnlineList(int offLineMinutes, int batchSize)
        {
            List<OnlineInfoEntity> list = new List<OnlineInfoEntity>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = SqlBatchHelper.GetMyCommand(con, CommandType.StoredProcedure, "C_Online_GetOnlineList");
                cmd.AddMyParameter("@OffLineMinutes", SqlDbType.Int, offLineMinutes);
                cmd.AddMyParameter("@BatchSize", SqlDbType.Int, batchSize);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        OnlineInfoEntity item = new OnlineInfoEntity();
                        item.ManagerId = (Guid)dr["ManagerId"];
                        item.LoginTime = (DateTime)dr["LoginTime"];
                        item.GuildInTime = (DateTime)dr["GuildInTime"];
                        item.ActiveTime = (DateTime)dr["ActiveTime"];
                        list.Add(item);
                    }
                }
                cmd.Parameters.Clear();
                return list;
            }
        }

        public static bool UpdateActive(OnlineinfoDataSet.Online_InfoDataTable inData)
        {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand cmd = SqlBatchHelper.GetMyCommand(con, CommandType.StoredProcedure, "C_Online_UpdateActive");
                    cmd.AddMyParameter("@ManagerId", SqlDbType.UniqueIdentifier, inData.ManagerIdColumn.ColumnName);
                    cmd.AddMyParameter("@ActiveFlag", SqlDbType.Bit, inData.ActiveFlagColumn.ColumnName);
                    cmd.AddMyParameter("@ResetFlag", SqlDbType.Bit, inData.ResetFlagColumn.ColumnName);
                    cmd.AddMyParameter("@LoginTime", SqlDbType.DateTime, inData.LoginTimeColumn.ColumnName);
                    cmd.AddMyParameter("@GuildInTime", SqlDbType.DateTime, inData.GuildInTimeColumn.ColumnName);
                    cmd.AddMyParameter("@ActiveTime", SqlDbType.DateTime, inData.ActiveTimeColumn.ColumnName);
                    cmd.AddMyParameter("@CntOnlineMinutes", SqlDbType.Int, inData.CntOnlineMinutesColumn.ColumnName);
                    cmd.AddMyParameter("@CurOnlineMinutes", SqlDbType.Int, inData.CurOnlineMinutesColumn.ColumnName);
                    SqlDataAdapter adap = SqlBatchHelper.GetMyBatchAdapter(null, cmd, cmd, cmd);
                    con.Open();
                    adap.Update(inData);
                    cmd.Parameters.Clear();
                    return true;
                }
        }

        public static bool ResetActive(int batchSize)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = SqlBatchHelper.GetMyCommand(con, CommandType.StoredProcedure, "C_Online_ResetActive");
                cmd.AddMyParameter("@BatchSize", SqlDbType.Int, batchSize);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}
