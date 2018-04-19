

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
    
    public partial class OnlineLockmanagerProvider
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
		/// 将IDataReader的当前记录读取到OnlineLockmanagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public OnlineLockmanagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new OnlineLockmanagerEntity();
			
            obj.Id = (System.Int64) reader["Id"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ManagerName = (System.String) reader["ManagerName"];
            obj.LockType = (System.Int32) reader["LockType"];
            obj.LockOperator = (System.String) reader["LockOperator"];
            obj.LockDate = (System.DateTime) reader["LockDate"];
            obj.LockMemo = (System.String) reader["LockMemo"];
            obj.BreakFlag = (System.Boolean) reader["BreakFlag"];
            obj.PreBreakDate = (System.DateTime) reader["PreBreakDate"];
            obj.BreakOperator = (System.String) reader["BreakOperator"];
            obj.BreakDate = (System.DateTime) reader["BreakDate"];
            obj.BreakMemo = (System.String) reader["BreakMemo"];
            obj.Status = (System.Int32) reader["Status"];
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
        public List<OnlineLockmanagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<OnlineLockmanagerEntity>();
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
        public OnlineLockmanagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public OnlineLockmanagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="id">id</param>
        /// <returns>OnlineLockmanagerEntity</returns>
        /// <remarks>2015/10/18 16:52:56</remarks>
        public OnlineLockmanagerEntity GetById( System.Int64 id)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_OnlineLockmanager_GetById");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, id);

            
            OnlineLockmanagerEntity obj=null;
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
        /// <returns>OnlineLockmanagerEntity列表</returns>
        /// <remarks>2015/10/18 16:52:56</remarks>
        public List<OnlineLockmanagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_OnlineLockmanager_GetAll");
            

            
            List<OnlineLockmanagerEntity> list = null;
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
		/// <param name="id">id</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 16:52:56</remarks>
        public bool Delete ( System.Int64 id,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_OnlineLockmanager_Delete");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, id);

            
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
		
		#region  CheckLock
		
		/// <summary>
        /// CheckLock
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="lockFlag">lockFlag</param>
		/// <param name="lockDate">lockDate</param>
		/// <param name="breakDate">breakDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 16:52:56</remarks>
        public bool CheckLock ( System.Guid managerId,ref  System.Boolean lockFlag,ref  System.DateTime lockDate,ref  System.DateTime breakDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_OnlineLockManager_CheckLock");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@LockFlag", DbType.Boolean, ParameterDirection.InputOutput,"",DataRowVersion.Current,lockFlag);
			database.AddParameter(commandWrapper, "@LockDate", DbType.DateTime, ParameterDirection.InputOutput,"",DataRowVersion.Current,lockDate);
			database.AddParameter(commandWrapper, "@BreakDate", DbType.DateTime, ParameterDirection.InputOutput,"",DataRowVersion.Current,breakDate);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            lockFlag=(System.Boolean)database.GetParameterValue(commandWrapper, "@LockFlag");
            lockDate=(System.DateTime)database.GetParameterValue(commandWrapper, "@LockDate");
            breakDate=(System.DateTime)database.GetParameterValue(commandWrapper, "@BreakDate");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Lock
		
		/// <summary>
        /// Lock
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="lockType">lockType</param>
		/// <param name="preBreakDate">preBreakDate</param>
		/// <param name="lockOperator">lockOperator</param>
		/// <param name="memo">memo</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 16:52:56</remarks>
        public bool Lock ( System.Guid managerId, System.Int32 lockType, System.DateTime preBreakDate, System.String lockOperator, System.String memo,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_OnlineLockManager_Lock");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@LockType", DbType.Int32, lockType);
			database.AddInParameter(commandWrapper, "@PreBreakDate", DbType.DateTime, preBreakDate);
			database.AddInParameter(commandWrapper, "@LockOperator", DbType.AnsiString, lockOperator);
			database.AddInParameter(commandWrapper, "@Memo", DbType.AnsiString, memo);

            
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
		
		#region  BreakLock
		
		/// <summary>
        /// BreakLock
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="breakOperator">breakOperator</param>
		/// <param name="memo">memo</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 16:52:56</remarks>
        public bool BreakLock ( System.Guid managerId, System.String breakOperator, System.String memo,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_OnlineLockManager_BreakLock");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@BreakOperator", DbType.AnsiString, breakOperator);
			database.AddInParameter(commandWrapper, "@Memo", DbType.AnsiString, memo);

            
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
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 16:52:56</remarks>
        public bool Insert(OnlineLockmanagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_OnlineLockmanager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.AnsiString, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@LockType", DbType.Int32, entity.LockType);
			database.AddInParameter(commandWrapper, "@LockOperator", DbType.AnsiString, entity.LockOperator);
			database.AddInParameter(commandWrapper, "@LockDate", DbType.DateTime, entity.LockDate);
			database.AddInParameter(commandWrapper, "@LockMemo", DbType.AnsiString, entity.LockMemo);
			database.AddInParameter(commandWrapper, "@BreakFlag", DbType.Boolean, entity.BreakFlag);
			database.AddInParameter(commandWrapper, "@PreBreakDate", DbType.DateTime, entity.PreBreakDate);
			database.AddInParameter(commandWrapper, "@BreakOperator", DbType.AnsiString, entity.BreakOperator);
			database.AddInParameter(commandWrapper, "@BreakDate", DbType.DateTime, entity.BreakDate);
			database.AddInParameter(commandWrapper, "@BreakMemo", DbType.AnsiString, entity.BreakMemo);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Id", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Id);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Id=(System.Int64)database.GetParameterValue(commandWrapper, "@Id");
            
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
        /// <remarks>2015/10/18 16:52:56</remarks>
        public bool Update(OnlineLockmanagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_OnlineLockmanager_Update");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, entity.Id);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.AnsiString, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@LockType", DbType.Int32, entity.LockType);
			database.AddInParameter(commandWrapper, "@LockOperator", DbType.AnsiString, entity.LockOperator);
			database.AddInParameter(commandWrapper, "@LockDate", DbType.DateTime, entity.LockDate);
			database.AddInParameter(commandWrapper, "@LockMemo", DbType.AnsiString, entity.LockMemo);
			database.AddInParameter(commandWrapper, "@BreakFlag", DbType.Boolean, entity.BreakFlag);
			database.AddInParameter(commandWrapper, "@PreBreakDate", DbType.DateTime, entity.PreBreakDate);
			database.AddInParameter(commandWrapper, "@BreakOperator", DbType.AnsiString, entity.BreakOperator);
			database.AddInParameter(commandWrapper, "@BreakDate", DbType.DateTime, entity.BreakDate);
			database.AddInParameter(commandWrapper, "@BreakMemo", DbType.AnsiString, entity.BreakMemo);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
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

            entity.Id=(System.Int64)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

