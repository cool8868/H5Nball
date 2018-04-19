

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
    
    public partial class TaskRecordProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Main;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到TaskRecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TaskRecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TaskRecordEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.TaskId = (System.Int32) reader["TaskId"];
            obj.CurTimes = (System.Int32) reader["CurTimes"];
            obj.StepRecord = (System.String) reader["StepRecord"];
            obj.DoneParam = (System.Byte[]) reader["DoneParam"];
            obj.ManagerLevel = (System.Int32) reader["ManagerLevel"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<TaskRecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TaskRecordEntity>();
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
        public TaskRecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TaskRecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>TaskRecordEntity</returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public TaskRecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TaskRecord_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            TaskRecordEntity obj=null;
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
        /// <returns>TaskRecordEntity列表</returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public List<TaskRecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TaskRecord_GetAll");
            

            
            List<TaskRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByManager
		
		/// <summary>
        /// GetByManager
        /// </summary>
        /// <returns>TaskRecordEntity列表</returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public List<TaskRecordEntity> GetByManager( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Task_GetByManager");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<TaskRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetPending
		
		/// <summary>
        /// GetPending
        /// </summary>
        /// <returns>TaskRecordEntity列表</returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public List<TaskRecordEntity> GetPending( System.Guid managerId, System.Int32 level)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Task_GetPending");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);

            
            List<TaskRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetForHandler
		
		/// <summary>
        /// GetForHandler
        /// </summary>
        /// <returns>TaskRecordEntity列表</returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public List<TaskRecordEntity> GetForHandler( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Task_GetForHandler");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<TaskRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetManagerTaskList
		
		/// <summary>
        /// GetManagerTaskList
        /// </summary>
        /// <returns>TaskRecordEntity列表</returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public List<TaskRecordEntity> GetManagerTaskList( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TaskRecord_GetManagerTaskList");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<TaskRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Submit
		
		/// <summary>
        /// Submit
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="prizeExp">prizeExp</param>
		/// <param name="prizeCoin">prizeCoin</param>
		/// <param name="prizeItemCode">prizeItemCode</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public bool Submit ( System.Int32 idx, System.Int32 prizeExp, System.Int32 prizeCoin, System.Int32 prizeItemCode,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Task_Submit");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@PrizeExp", DbType.Int32, prizeExp);
			database.AddInParameter(commandWrapper, "@PrizeCoin", DbType.Int32, prizeCoin);
			database.AddInParameter(commandWrapper, "@PrizeItemCode", DbType.Int32, prizeItemCode);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Add
		
		/// <summary>
        /// Add
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="taskId">taskId</param>
		/// <param name="curTimes">curTimes</param>
		/// <param name="stepRecord">stepRecord</param>
		/// <param name="doneParam">doneParam</param>
		/// <param name="managerLevel">managerLevel</param>
		/// <param name="status">status</param>
		/// <param name="rowTime">rowTime</param>
		/// <param name="updateTime">updateTime</param>
		/// <param name="idx">idx</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public bool Add ( System.Guid managerId, System.Int32 taskId, System.Int32 curTimes, System.String stepRecord, System.Byte[] doneParam, System.Int32 managerLevel, System.Int32 status, System.DateTime rowTime, System.DateTime updateTime,ref  System.Int32 idx,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Task_Add");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@TaskId", DbType.Int32, taskId);
			database.AddInParameter(commandWrapper, "@CurTimes", DbType.Int32, curTimes);
			database.AddInParameter(commandWrapper, "@StepRecord", DbType.AnsiString, stepRecord);
			database.AddInParameter(commandWrapper, "@DoneParam", DbType.Binary, doneParam);
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, managerLevel);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, rowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, updateTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,idx);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public bool Insert(TaskRecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TaskRecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@TaskId", DbType.Int32, entity.TaskId);
			database.AddInParameter(commandWrapper, "@CurTimes", DbType.Int32, entity.CurTimes);
			database.AddInParameter(commandWrapper, "@StepRecord", DbType.AnsiString, entity.StepRecord);
			database.AddInParameter(commandWrapper, "@DoneParam", DbType.Binary, entity.DoneParam);
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, entity.ManagerLevel);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/18 12:02:33</remarks>
        public bool Update(TaskRecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TaskRecord_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@TaskId", DbType.Int32, entity.TaskId);
			database.AddInParameter(commandWrapper, "@CurTimes", DbType.Int32, entity.CurTimes);
			database.AddInParameter(commandWrapper, "@StepRecord", DbType.AnsiString, entity.StepRecord);
			database.AddInParameter(commandWrapper, "@DoneParam", DbType.Binary, entity.DoneParam);
			database.AddInParameter(commandWrapper, "@ManagerLevel", DbType.Int32, entity.ManagerLevel);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);

            
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

