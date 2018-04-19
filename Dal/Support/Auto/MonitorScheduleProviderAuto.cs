

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
    
    public partial class MonitorScheduleProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Support;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到MonitorScheduleEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public MonitorScheduleEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new MonitorScheduleEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ZoneId = (System.Int32) reader["ZoneId"];
            obj.ScheduleId = (System.Int32) reader["ScheduleId"];
            obj.AppId = (System.Int32) reader["AppId"];
            obj.TerminalIp = (System.String) reader["TerminalIp"];
            obj.StartTime = (System.DateTime) reader["StartTime"];
            obj.NextInvokeTime = (System.DateTime) reader["NextInvokeTime"];
            obj.EndTime = (System.DateTime) reader["EndTime"];
            obj.LastFailTime = (System.DateTime) reader["LastFailTime"];
            obj.Status = (System.Int32) reader["Status"];
            obj.SuccessTimes = (System.Int64) reader["SuccessTimes"];
            obj.FailTimes = (System.Int64) reader["FailTimes"];
            obj.RetryTimes = (System.Int32) reader["RetryTimes"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<MonitorScheduleEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<MonitorScheduleEntity>();
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
        public MonitorScheduleProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public MonitorScheduleProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>MonitorScheduleEntity</returns>
        /// <remarks>2016/1/7 15:48:28</remarks>
        public MonitorScheduleEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_MonitorSchedule_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            MonitorScheduleEntity obj=null;
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
		
		#region  GetByZone
		
		/// <summary>
        /// GetByZone
        /// </summary>
		/// <param name="zoneId">zoneId</param>
		/// <param name="scheduleId">scheduleId</param>
		/// <param name="appId">appId</param>
		/// <param name="terminalIp">terminalIp</param>
        /// <returns>MonitorScheduleEntity</returns>
        /// <remarks>2016/1/7 15:48:28</remarks>
        public MonitorScheduleEntity GetByZone( System.Int32 zoneId, System.Int32 scheduleId, System.Int32 appId, System.String terminalIp)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MonitorSchedule_GetByZone");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@ScheduleId", DbType.Int32, scheduleId);
			database.AddInParameter(commandWrapper, "@AppId", DbType.Int32, appId);
			database.AddInParameter(commandWrapper, "@TerminalIp", DbType.AnsiString, terminalIp);

            
            MonitorScheduleEntity obj=null;
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
        /// <returns>MonitorScheduleEntity列表</returns>
        /// <remarks>2016/1/7 15:48:28</remarks>
        public List<MonitorScheduleEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_MonitorSchedule_GetAll");
            

            
            List<MonitorScheduleEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  UpdateApp
		
		/// <summary>
        /// UpdateApp
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="appId">appId</param>
		/// <param name="terminalIp">terminalIp</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/7 15:48:28</remarks>
        public bool UpdateApp ( System.Int32 idx, System.Int32 appId, System.String terminalIp,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MonitorSchedule_UpdateApp");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@AppId", DbType.Int32, appId);
			database.AddInParameter(commandWrapper, "@TerminalIp", DbType.AnsiString, terminalIp);

            
            
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
		
		#region  Start
		
		/// <summary>
        /// Start
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="status">status</param>
		/// <param name="startTime">startTime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/7 15:48:28</remarks>
        public bool Start ( System.Int32 idx, System.Int32 status, System.DateTime startTime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MonitorSchedule_Start");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);

            
            
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
		
		#region  Finish
		
		/// <summary>
        /// Finish
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="status">status</param>
		/// <param name="invokeTime">invokeTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="lastFailTime">lastFailTime</param>
		/// <param name="successTimes">successTimes</param>
		/// <param name="failTimes">failTimes</param>
		/// <param name="retryTimes">retryTimes</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/7 15:48:28</remarks>
        public bool Finish ( System.Int32 idx, System.Int32 status, System.DateTime invokeTime, System.DateTime endTime, System.DateTime lastFailTime, System.Int64 successTimes, System.Int64 failTimes, System.Int32 retryTimes,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MonitorSchedule_Finish");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);
			database.AddInParameter(commandWrapper, "@InvokeTime", DbType.DateTime, invokeTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddInParameter(commandWrapper, "@LastFailTime", DbType.DateTime, lastFailTime);
			database.AddInParameter(commandWrapper, "@SuccessTimes", DbType.Int64, successTimes);
			database.AddInParameter(commandWrapper, "@FailTimes", DbType.Int64, failTimes);
			database.AddInParameter(commandWrapper, "@RetryTimes", DbType.Int32, retryTimes);

            
            
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
        /// <remarks>2016/1/7 15:48:28</remarks>
        public bool Insert(MonitorScheduleEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/7 15:48:28</remarks>
        public bool Insert(MonitorScheduleEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_MonitorSchedule_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@ScheduleId", DbType.Int32, entity.ScheduleId);
			database.AddInParameter(commandWrapper, "@AppId", DbType.Int32, entity.AppId);
			database.AddInParameter(commandWrapper, "@TerminalIp", DbType.AnsiString, entity.TerminalIp);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@NextInvokeTime", DbType.DateTime, entity.NextInvokeTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
			database.AddInParameter(commandWrapper, "@LastFailTime", DbType.DateTime, entity.LastFailTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@SuccessTimes", DbType.Int64, entity.SuccessTimes);
			database.AddInParameter(commandWrapper, "@FailTimes", DbType.Int64, entity.FailTimes);
			database.AddInParameter(commandWrapper, "@RetryTimes", DbType.Int32, entity.RetryTimes);
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
        /// <remarks>2016/1/7 15:48:28</remarks>
        public bool Update(MonitorScheduleEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/7 15:48:28</remarks>
        public bool Update(MonitorScheduleEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_MonitorSchedule_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@ScheduleId", DbType.Int32, entity.ScheduleId);
			database.AddInParameter(commandWrapper, "@AppId", DbType.Int32, entity.AppId);
			database.AddInParameter(commandWrapper, "@TerminalIp", DbType.AnsiString, entity.TerminalIp);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@NextInvokeTime", DbType.DateTime, entity.NextInvokeTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
			database.AddInParameter(commandWrapper, "@LastFailTime", DbType.DateTime, entity.LastFailTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@SuccessTimes", DbType.Int64, entity.SuccessTimes);
			database.AddInParameter(commandWrapper, "@FailTimes", DbType.Int64, entity.FailTimes);
			database.AddInParameter(commandWrapper, "@RetryTimes", DbType.Int32, entity.RetryTimes);

            
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

