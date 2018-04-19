

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
    
    public partial class ActivityRecordProvider
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
		/// 将IDataReader的当前记录读取到ActivityRecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ActivityRecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ActivityRecordEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ActivityId = (System.Int32) reader["ActivityId"];
            obj.ActivityStep = (System.Int32) reader["ActivityStep"];
            obj.StepRecord = (System.String) reader["StepRecord"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.SettlementDate = (System.DateTime) reader["SettlementDate"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ActivityRecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ActivityRecordEntity>();
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
        public ActivityRecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ActivityRecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ActivityRecordEntity</returns>
        /// <remarks>2016/5/3 15:01:32</remarks>
        public ActivityRecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ActivityRecord_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ActivityRecordEntity obj=null;
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
		
		#region  GetByManagerActivityId
		
		/// <summary>
        /// GetByManagerActivityId
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="activityId">activityId</param>
        /// <returns>ActivityRecordEntity</returns>
        /// <remarks>2016/5/3 15:01:32</remarks>
        public ActivityRecordEntity GetByManagerActivityId( System.Guid managerId, System.Int32 activityId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityRecord_GetByManagerActivityId");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ActivityId", DbType.Int32, activityId);

            
            ActivityRecordEntity obj=null;
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
        /// <returns>ActivityRecordEntity列表</returns>
        /// <remarks>2016/5/3 15:01:32</remarks>
        public List<ActivityRecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ActivityRecord_GetAll");
            

            
            List<ActivityRecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetCompleteByManager
		
		/// <summary>
        /// GetCompleteByManager
        /// </summary>
        /// <returns>ActivityRecordEntity列表</returns>
        /// <remarks>2016/5/3 15:01:32</remarks>
        public List<ActivityRecordEntity> GetCompleteByManager( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityRecord_GetCompleteByManager");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<ActivityRecordEntity> list = null;
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
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/3 15:01:32</remarks>
        public bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ActivityRecord_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);

            
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
		
		#region  SavePrize
		
		/// <summary>
        /// SavePrize
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="managerId">managerId</param>
		/// <param name="activityId">activityId</param>
		/// <param name="activityStep">activityStep</param>
		/// <param name="stepRecord">stepRecord</param>
		/// <param name="recordDate">recordDate</param>
		/// <param name="settlementDate">settlementDate</param>
		/// <param name="status">status</param>
		/// <param name="updateTime">updateTime</param>
		/// <param name="rowVersion">rowVersion</param>
		/// <param name="activityKey">activityKey</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/5/3 15:01:32</remarks>
        public bool SavePrize ( System.Int32 idx, System.Guid managerId, System.Int32 activityId, System.Int32 activityStep, System.String stepRecord, System.DateTime recordDate, System.DateTime settlementDate, System.Int32 status, System.DateTime updateTime, System.Byte[] rowVersion, System.String activityKey,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ActivityRecord_SavePrize");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ActivityId", DbType.Int32, activityId);
			database.AddInParameter(commandWrapper, "@ActivityStep", DbType.Int32, activityStep);
			database.AddInParameter(commandWrapper, "@StepRecord", DbType.AnsiString, stepRecord);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);
			database.AddInParameter(commandWrapper, "@SettlementDate", DbType.DateTime, settlementDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, updateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);
			database.AddInParameter(commandWrapper, "@ActivityKey", DbType.AnsiString, activityKey);
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
        /// <remarks>2016/5/3 15:01:32</remarks>
        public bool Insert(ActivityRecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ActivityRecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ActivityId", DbType.Int32, entity.ActivityId);
			database.AddInParameter(commandWrapper, "@ActivityStep", DbType.Int32, entity.ActivityStep);
			database.AddInParameter(commandWrapper, "@StepRecord", DbType.AnsiString, entity.StepRecord);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@SettlementDate", DbType.DateTime, entity.SettlementDate);
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
        /// <remarks>2016/5/3 15:01:32</remarks>
        public bool Update(ActivityRecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ActivityRecord_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ActivityId", DbType.Int32, entity.ActivityId);
			database.AddInParameter(commandWrapper, "@ActivityStep", DbType.Int32, entity.ActivityStep);
			database.AddInParameter(commandWrapper, "@StepRecord", DbType.AnsiString, entity.StepRecord);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@SettlementDate", DbType.DateTime, entity.SettlementDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);

            
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

