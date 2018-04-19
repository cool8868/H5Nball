using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Common
{
    public static class SqlBatchHelper
    {
        static readonly int COMMANDBatchSize = 3000;
        static readonly int COMMANDTimeout = 120;

        #region BulkInsert
        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="table">数据表</param>
        public static void BulkInsert(string connectionString, DataTable table)
        {
            if (string.IsNullOrEmpty(table.TableName)) throw new Exception("DataTable.TableName属性不能为空");
            using (SqlBulkCopy bulk = new SqlBulkCopy(connectionString))
            {
                bulk.BatchSize = COMMANDBatchSize;
                bulk.BulkCopyTimeout = COMMANDTimeout;
                bulk.DestinationTableName = table.TableName;
                foreach (DataColumn col in table.Columns)
                {
                    if (!col.AutoIncrement)
                        bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                }
                bulk.WriteToServer(table);
                bulk.Close();
            }
        }

        public static void BulkInsert(SqlTransaction trans, DataTable table)
        {
            if (string.IsNullOrEmpty(table.TableName)) throw new Exception("DataTable.TableName属性不能为空");
            using (SqlBulkCopy bulk = new SqlBulkCopy(trans.Connection,SqlBulkCopyOptions.Default,trans))
            {
                bulk.BatchSize = COMMANDBatchSize;
                bulk.BulkCopyTimeout = COMMANDTimeout;
                bulk.DestinationTableName = table.TableName;
                foreach (DataColumn col in table.Columns)
                {
                    if (!col.AutoIncrement)
                        bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                }
                bulk.WriteToServer(table);
                bulk.Close();
            }
        }
        #endregion

        #region Command
        public static SqlCommand GetMyCommand(SqlConnection con)
        {
            return GetMyCommand(con, string.Empty);
        }
        public static SqlCommand GetMyCommand(SqlConnection con, string cmdText)
        {
            return GetMyCommand(con, CommandType.Text, cmdText);
        }
        public static SqlCommand GetMyCommand(SqlConnection con, CommandType cmdType, string cmdText)
        {
            return GetMyCommand(con, cmdType, cmdText, null);
        }
        public static SqlCommand GetMyCommand(SqlConnection con, CommandType cmdType, string cmdText, params SqlParameter[] parms)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandTimeout = COMMANDTimeout;
            cmd.CommandType = cmdType;
            if (!string.IsNullOrEmpty(cmdText)) cmd.CommandText = cmdText;
            if (parms != null) cmd.Parameters.AddRange(parms);
            return cmd;
        }
        #endregion

        #region Adapter
        public static SqlDataAdapter GetMyAdapter(SqlCommand selectCmd)
        {
            return GetMyAdapter(selectCmd, null, null, null);
        }
        public static SqlDataAdapter GetMyAdapter(SqlCommand insertCmd, SqlCommand updateCmd, SqlCommand deleteCmd)
        {
            return GetMyAdapter(null, insertCmd, updateCmd, deleteCmd);
        }
        public static SqlDataAdapter GetMyAdapter(SqlCommand selectCmd, SqlCommand insertCmd, SqlCommand updateCmd, SqlCommand deleteCmd)
        {
            SqlDataAdapter adap = new SqlDataAdapter();
            adap.SelectCommand = selectCmd;
            adap.InsertCommand = insertCmd;
            adap.UpdateCommand = updateCmd;
            adap.DeleteCommand = deleteCmd;
            return adap;
        }
        public static SqlDataAdapter GetMyBatchAdapter(SqlCommand selectCmd, SqlCommand insertCmd, SqlCommand updateCmd, SqlCommand deleteCmd)
        {
            return GetMyBatchAdapter(selectCmd, insertCmd, updateCmd, deleteCmd, COMMANDBatchSize);
        }
        public static SqlDataAdapter GetMyBatchAdapter(SqlCommand selectCmd, SqlCommand insertCmd, SqlCommand updateCmd, SqlCommand deleteCmd, int batchSize)
        {
            SqlDataAdapter adap = GetMyAdapter(selectCmd, insertCmd, updateCmd, deleteCmd);
            if (batchSize >= 0) adap.UpdateBatchSize = batchSize;
            if (batchSize > 1)
            {
                if (null != selectCmd) selectCmd.UpdatedRowSource = UpdateRowSource.None;
                if (null != insertCmd) insertCmd.UpdatedRowSource = UpdateRowSource.None;
                if (null != updateCmd) updateCmd.UpdatedRowSource = UpdateRowSource.None;
                if (null != deleteCmd) deleteCmd.UpdatedRowSource = UpdateRowSource.None;
            }
            return adap;
        }
        #endregion

        #region Parameter
        public static SqlParameter AddMyParameter(this SqlCommand cmd, string parmName, SqlDbType dbType)
        {
            SqlParameter parm = new SqlParameter(parmName, dbType);
            if (cmd != null) cmd.Parameters.Add(parm);
            return parm;
        }
        public static SqlParameter AddMyParameter(this SqlCommand cmd, string parmName, SqlDbType dbType, object value)
        {
            SqlParameter parm = AddMyParameter(cmd, parmName, dbType);
            parm.Value = value;
            return parm;
        }
        public static SqlParameter AddMyParameter(this SqlCommand cmd, string parmName, SqlDbType dbType, ParameterDirection direction)
        {
            SqlParameter parm = new SqlParameter(parmName, dbType);
            parm.Direction = direction;
            if (cmd != null) cmd.Parameters.Add(parm);
            return parm;
        }
        public static SqlParameter AddMyParameter(this SqlCommand cmd, string parmName, SqlDbType dbType, ParameterDirection direction, object value)
        {
            SqlParameter parm = AddMyParameter(cmd, parmName, dbType, direction);
            parm.Value = value;
            return parm;
        }
        public static SqlParameter AddMyParameter(this SqlCommand cmd, string parmName, SqlDbType dbType, string sourceColumn)
        {
            SqlParameter parm = new SqlParameter(parmName, dbType);
            parm.SourceColumn = sourceColumn;
            if (cmd != null) cmd.Parameters.Add(parm);
            return parm;
        }
        public static SqlParameter AddMyParameter(this SqlCommand cmd, string parmName, SqlDbType dbType, string sourceColumn, object value)
        {
            SqlParameter parm = AddMyParameter(cmd, parmName, dbType, sourceColumn);
            parm.Value = value;
            return parm;
        }
        #endregion
    }
}
