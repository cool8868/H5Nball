

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
    
    public partial class DailyattendanceManagerProvider
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
		/// 将IDataReader的当前记录读取到DailyattendanceManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DailyattendanceManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DailyattendanceManagerEntity();
			
            obj.Idx = (System.Int64) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Name = (System.String) reader["Name"];
            obj.AttendTimes = (System.Int32) reader["AttendTimes"];
            obj.Month = (System.Int32) reader["Month"];
            obj.IsAttend = (System.Boolean) reader["IsAttend"];
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
        public List<DailyattendanceManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DailyattendanceManagerEntity>();
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
        public DailyattendanceManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DailyattendanceManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetManager
		
		/// <summary>
        /// GetManager
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="name">name</param>
		/// <param name="month">month</param>
        /// <returns>DailyattendanceManagerEntity</returns>
        /// <remarks>2016/2/19 15:32:25</remarks>
        public DailyattendanceManagerEntity GetManager( System.Guid managerId, System.String name, System.Int32 month)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyAttendance_GetManager");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Name", DbType.AnsiString, name);
			database.AddInParameter(commandWrapper, "@Month", DbType.Int32, month);

            
            DailyattendanceManagerEntity obj=null;
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
		
		#region  UpdateStatus
		
		/// <summary>
        /// UpdateStatus
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>DailyattendanceManagerEntity</returns>
        /// <remarks>2016/2/19 15:32:25</remarks>
        public DailyattendanceManagerEntity UpdateStatus( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyAttendance_UpdateStatus");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            DailyattendanceManagerEntity obj=null;
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
		
		#region  UpdateMonth
		
		/// <summary>
        /// UpdateMonth
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="month">month</param>
        /// <returns>DailyattendanceManagerEntity</returns>
        /// <remarks>2016/2/19 15:32:25</remarks>
        public DailyattendanceManagerEntity UpdateMonth( System.Guid managerId, System.Int32 month)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DailyAttendance_UpdateMonth");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Month", DbType.Int32, month);

            
            DailyattendanceManagerEntity obj=null;
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
        /// <returns>DailyattendanceManagerEntity列表</returns>
        /// <remarks>2016/2/19 15:32:25</remarks>
        public List<DailyattendanceManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailyattendanceManager_GetAll");
            

            
            List<DailyattendanceManagerEntity> list = null;
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
        /// <remarks>2016/2/19 15:32:26</remarks>
        public bool Delete ( System.Int64 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DailyattendanceManager_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, idx);

            
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
        /// <remarks>2016/2/19 15:32:26</remarks>
        public bool Insert(DailyattendanceManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailyattendanceManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@AttendTimes", DbType.Int32, entity.AttendTimes);
			database.AddInParameter(commandWrapper, "@Month", DbType.Int32, entity.Month);
			database.AddInParameter(commandWrapper, "@IsAttend", DbType.Boolean, entity.IsAttend);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Int64)database.GetParameterValue(commandWrapper, "@Idx");
            
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
        /// <remarks>2016/2/19 15:32:26</remarks>
        public bool Update(DailyattendanceManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DailyattendanceManager_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@AttendTimes", DbType.Int32, entity.AttendTimes);
			database.AddInParameter(commandWrapper, "@Month", DbType.Int32, entity.Month);
			database.AddInParameter(commandWrapper, "@IsAttend", DbType.Boolean, entity.IsAttend);
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

            entity.Idx=(System.Int64)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

