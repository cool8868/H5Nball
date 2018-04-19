

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
    
    public partial class TaskDailyrecordProvider
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
		/// 将IDataReader的当前记录读取到TaskDailyrecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TaskDailyrecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TaskDailyrecordEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.DailyCount = (System.Int32) reader["DailyCount"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.TaskIds = (System.String) reader["TaskIds"];
            obj.CurTimes = (System.String) reader["CurTimes"];
            obj.StepRecords = (System.String) reader["StepRecords"];
            obj.DoneParam = (System.Byte[]) reader["DoneParam"];
            obj.Status = (System.String) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
            obj.IsReceive = (System.Boolean) reader["IsReceive"];
            obj.FinishCount = (System.Int32) reader["FinishCount"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<TaskDailyrecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TaskDailyrecordEntity>();
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
        public TaskDailyrecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TaskDailyrecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>TaskDailyrecordEntity</returns>
        /// <remarks>2016/5/17 18:11:43</remarks>
        public TaskDailyrecordEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TaskDailyrecord_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            TaskDailyrecordEntity obj=null;
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
        /// <returns>TaskDailyrecordEntity列表</returns>
        /// <remarks>2016/5/17 18:11:43</remarks>
        public List<TaskDailyrecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TaskDailyrecord_GetAll");
            

            
            List<TaskDailyrecordEntity> list = null;
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
		/// <param name="managerId">managerId</param>
		/// <param name="newTaskId">newTaskId</param>
		/// <param name="stepRecord">stepRecord</param>
		/// <param name="curTimes">curTimes</param>
		/// <param name="allStepRecords">allStepRecords</param>
		/// <param name="allCurTimes">allCurTimes</param>
		/// <param name="allStatus">allStatus</param>
		/// <param name="prizeExp">prizeExp</param>
		/// <param name="prizeCoin">prizeCoin</param>
		/// <param name="prizeItemCode">prizeItemCode</param>
		/// <param name="finishCount">finishCount</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/17 18:11:43</remarks>
        public bool Submit ( System.Guid managerId, System.Int32 newTaskId, System.String stepRecord, System.Int32 curTimes, System.String allStepRecords, System.String allCurTimes, System.String allStatus, System.Int32 prizeExp, System.Int32 prizeCoin, System.Int32 prizeItemCode, System.Int32 finishCount,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TaskDaily_Submit");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@NewTaskId", DbType.Int32, newTaskId);
			database.AddInParameter(commandWrapper, "@StepRecord", DbType.AnsiString, stepRecord);
			database.AddInParameter(commandWrapper, "@CurTimes", DbType.Int32, curTimes);
			database.AddInParameter(commandWrapper, "@AllStepRecords", DbType.AnsiString, allStepRecords);
			database.AddInParameter(commandWrapper, "@AllCurTimes", DbType.AnsiString, allCurTimes);
			database.AddInParameter(commandWrapper, "@AllStatus", DbType.AnsiString, allStatus);
			database.AddInParameter(commandWrapper, "@PrizeExp", DbType.Int32, prizeExp);
			database.AddInParameter(commandWrapper, "@PrizeCoin", DbType.Int32, prizeCoin);
			database.AddInParameter(commandWrapper, "@PrizeItemCode", DbType.Int32, prizeItemCode);
			database.AddInParameter(commandWrapper, "@FinishCount", DbType.Int32, finishCount);
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
		
		#region  Giveup
		
		/// <summary>
        /// Giveup
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="newTaskId">newTaskId</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/17 18:11:43</remarks>
        public bool Giveup ( System.Guid managerId, System.Int32 newTaskId,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TaskDaily_Giveup");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@NewTaskId", DbType.Int32, newTaskId);
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
		
		#region  FinishPrize
		
		/// <summary>
        /// FinishPrize
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="prizeExp">prizeExp</param>
		/// <param name="prizeCoin">prizeCoin</param>
		/// <param name="prizeItemCode">prizeItemCode</param>
		/// <param name="prizePoint">prizePoint</param>
		/// <param name="recordDate">recordDate</param>
		/// <param name="rowVersion">rowVersion</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/17 18:11:43</remarks>
        public bool FinishPrize ( System.Guid managerId, System.Int32 prizeExp, System.Int32 prizeCoin, System.Int32 prizeItemCode, System.Int32 prizePoint, System.DateTime recordDate, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_TaskDailyrecord_FinishPrize");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@PrizeExp", DbType.Int32, prizeExp);
			database.AddInParameter(commandWrapper, "@PrizeCoin", DbType.Int32, prizeCoin);
			database.AddInParameter(commandWrapper, "@PrizeItemCode", DbType.Int32, prizeItemCode);
			database.AddInParameter(commandWrapper, "@PrizePoint", DbType.Int32, prizePoint);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);
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
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/17 18:11:43</remarks>
        public bool Insert(TaskDailyrecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TaskDailyrecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@DailyCount", DbType.Int32, entity.DailyCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@TaskIds", DbType.AnsiString, entity.TaskIds);
			database.AddInParameter(commandWrapper, "@CurTimes", DbType.AnsiString, entity.CurTimes);
			database.AddInParameter(commandWrapper, "@StepRecords", DbType.AnsiString, entity.StepRecords);
			database.AddInParameter(commandWrapper, "@DoneParam", DbType.Binary, entity.DoneParam);
			database.AddInParameter(commandWrapper, "@Status", DbType.AnsiString, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@IsReceive", DbType.Boolean, entity.IsReceive);
			database.AddInParameter(commandWrapper, "@FinishCount", DbType.Int32, entity.FinishCount);
			database.AddParameter(commandWrapper, "@ManagerId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.ManagerId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
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
        /// <remarks>2016/5/17 18:11:43</remarks>
        public bool Update(TaskDailyrecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TaskDailyrecord_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@DailyCount", DbType.Int32, entity.DailyCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@TaskIds", DbType.AnsiString, entity.TaskIds);
			database.AddInParameter(commandWrapper, "@CurTimes", DbType.AnsiString, entity.CurTimes);
			database.AddInParameter(commandWrapper, "@StepRecords", DbType.AnsiString, entity.StepRecords);
			database.AddInParameter(commandWrapper, "@DoneParam", DbType.Binary, entity.DoneParam);
			database.AddInParameter(commandWrapper, "@Status", DbType.AnsiString, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);
			database.AddInParameter(commandWrapper, "@IsReceive", DbType.Boolean, entity.IsReceive);
			database.AddInParameter(commandWrapper, "@FinishCount", DbType.Int32, entity.FinishCount);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

