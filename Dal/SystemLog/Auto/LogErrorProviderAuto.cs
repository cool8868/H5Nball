

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Dal
{
    
    public partial class LogErrorProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.SystemLog;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到LogErrorEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LogErrorEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LogErrorEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.AppId = (System.Int32) reader["AppId"];
            obj.TerminalIP = (System.String) reader["TerminalIP"];
            obj.ModuleId = (System.Int32) reader["ModuleId"];
            obj.FunctionId = (System.Int32) reader["FunctionId"];
            obj.Message = (System.String) reader["Message"];
            obj.StackTrace = (System.String) reader["StackTrace"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<LogErrorEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LogErrorEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow(reader));
            }
            return clt;
        }
        #endregion
        
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public LogErrorProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LogErrorProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>LogErrorEntity</returns>
        /// <remarks>2014/3/20 10:53:57</remarks>
        public LogErrorEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LogError_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            LogErrorEntity obj=null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                if(reader.Read())
                {
                    
            
                    obj = LoadSingleRow(reader);
                }
            }
            return obj;
        }
		
		#endregion		  
		
		#region  GetAll
		
		/// <summary>
        /// GetAll
        /// </summary>
        /// <returns>LogErrorEntity列表</returns>
        /// <remarks>2014/3/20 10:53:57</remarks>
        public List<LogErrorEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LogError_GetAll");
            

            
            List<LogErrorEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/3/20 10:53:57</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LogError_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }

            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region Insert
		
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/3/20 10:53:57</remarks>
        public bool Insert(LogErrorEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/3/20 10:53:57</remarks>
        public bool Insert(LogErrorEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LogError_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@AppId", DbType.Int32, entity.AppId);
			database.AddInParameter(commandWrapper, "@TerminalIP", DbType.AnsiString, entity.TerminalIP);
			database.AddInParameter(commandWrapper, "@ModuleId", DbType.Int32, entity.ModuleId);
			database.AddInParameter(commandWrapper, "@FunctionId", DbType.Int32, entity.FunctionId);
			database.AddInParameter(commandWrapper, "@Message", DbType.AnsiString, entity.Message);
			database.AddInParameter(commandWrapper, "@StackTrace", DbType.AnsiString, entity.StackTrace);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2014/3/20 10:53:57</remarks>
        public bool Update(LogErrorEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/3/20 10:53:57</remarks>
        public bool Update(LogErrorEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LogError_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@AppId", DbType.Int32, entity.AppId);
			database.AddInParameter(commandWrapper, "@TerminalIP", DbType.AnsiString, entity.TerminalIP);
			database.AddInParameter(commandWrapper, "@ModuleId", DbType.Int32, entity.ModuleId);
			database.AddInParameter(commandWrapper, "@FunctionId", DbType.Int32, entity.FunctionId);
			database.AddInParameter(commandWrapper, "@Message", DbType.AnsiString, entity.Message);
			database.AddInParameter(commandWrapper, "@StackTrace", DbType.AnsiString, entity.StackTrace);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

