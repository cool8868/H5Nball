using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Games.MyControl
{
public sealed class MyControl_SqlHelper
{
    // Methods
    private MyControl_SqlHelper()
    {
    }

    private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
    {
        if ((commandParameters != null) && (parameterValues != null))
        {
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }
            int index = 0;
            int length = commandParameters.Length;
            while (index < length)
            {
                commandParameters[index].Value = parameterValues[index];
                index++;
            }
        }
    }

    private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
        foreach (SqlParameter parameter in commandParameters)
        {
            if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
            {
                parameter.Value = DBNull.Value;
            }
            command.Parameters.Add(parameter);
        }
    }

    public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        return ExecuteDataset(connection, null, commandType, commandText, commandParameters);
    }

    public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return ExecuteDataset(connection, commandType, commandText, commandParameters);
        }
    }

    public static DataSet ExecuteDataset(SqlConnection connection, SqlTransaction trans, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand command = new SqlCommand();
        PrepareCommand(command, connection, trans, commandType, commandText, commandParameters);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataSet dataSet = new DataSet();
        adapter.Fill(dataSet);
        command.Parameters.Clear();
        return dataSet;
    }

    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        return ExecuteNonQuery(connection, null, commandType, commandText, commandParameters);
    }

    public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
        }
    }

    public static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transcation, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand command = new SqlCommand();
        PrepareCommand(command, connection, transcation, commandType, commandText, commandParameters);
        int num = command.ExecuteNonQuery();
        command.Parameters.Clear();
        return num;
    }

    public static SqlDataReader ExecuteReader(SqlConnection conn, CommandType commandType, string comText, params SqlParameter[] commandParameters)
    {
        SqlDataReader reader2;
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandType = commandType;
            command.CommandText = comText;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            SqlDataReader reader = command.ExecuteReader();
            command.Dispose();
            reader2 = reader;
        }
        catch (Exception exception)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            throw exception;
        }
        return reader2;
    }

    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        return ExecuteScalar(connection, null, commandType, commandText, commandParameters);
    }

    public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            return ExecuteScalar(connection, commandType, commandText, commandParameters);
        }
    }

    public static object ExecuteScalar(SqlConnection connection, SqlTransaction trans, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand command = new SqlCommand();
        PrepareCommand(command, connection, trans, commandType, commandText, commandParameters);
        object obj2 = command.ExecuteScalar();
        command.Parameters.Clear();
        return obj2;
    }

    private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
    {
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        command.Connection = connection;
        command.CommandText = commandText;
        if (transaction != null)
        {
            command.Transaction = transaction;
        }
        command.CommandType = commandType;
        if (commandParameters != null)
        {
            AttachParameters(command, commandParameters);
        }
    }
}
}
