

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
    
    public partial class InvestManagerProvider
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
		/// 将IDataReader的当前记录读取到InvestManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public InvestManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new InvestManagerEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Deposit = (System.Int32) reader["Deposit"];
            obj.StepStatus = (System.String) reader["StepStatus"];
            obj.TheMonthly = (System.Boolean) reader["TheMonthly"];
            obj.MonthlyTime = (System.DateTime) reader["MonthlyTime"];
            obj.ExpirationTime = (System.DateTime) reader["ExpirationTime"];
            obj.Once = (System.Boolean) reader["Once"];
            obj.ReceivedCount = (System.Int32) reader["ReceivedCount"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.DepositCount = (System.Int32) reader["DepositCount"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<InvestManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<InvestManagerEntity>();
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
        public InvestManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public InvestManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>InvestManagerEntity</returns>
        /// <remarks>2016/3/4 16:25:58</remarks>
        public InvestManagerEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_InvestManager_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            InvestManagerEntity obj=null;
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
        /// <returns>InvestManagerEntity列表</returns>
        /// <remarks>2016/3/4 16:25:58</remarks>
        public List<InvestManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_InvestManager_GetAll");
            

            
            List<InvestManagerEntity> list = null;
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
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/3/4 16:25:58</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_InvestManager_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
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
        /// <remarks>2016/3/4 16:25:58</remarks>
        public bool Insert(InvestManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_InvestManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Deposit", DbType.Int32, entity.Deposit);
			database.AddInParameter(commandWrapper, "@StepStatus", DbType.AnsiString, entity.StepStatus);
			database.AddInParameter(commandWrapper, "@TheMonthly", DbType.Boolean, entity.TheMonthly);
			database.AddInParameter(commandWrapper, "@MonthlyTime", DbType.DateTime, entity.MonthlyTime);
			database.AddInParameter(commandWrapper, "@ExpirationTime", DbType.DateTime, entity.ExpirationTime);
			database.AddInParameter(commandWrapper, "@Once", DbType.Boolean, entity.Once);
			database.AddInParameter(commandWrapper, "@ReceivedCount", DbType.Int32, entity.ReceivedCount);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@DepositCount", DbType.Int32, entity.DepositCount);
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
        /// <remarks>2016/3/4 16:25:58</remarks>
        public bool Update(InvestManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_InvestManager_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Deposit", DbType.Int32, entity.Deposit);
			database.AddInParameter(commandWrapper, "@StepStatus", DbType.AnsiString, entity.StepStatus);
			database.AddInParameter(commandWrapper, "@TheMonthly", DbType.Boolean, entity.TheMonthly);
			database.AddInParameter(commandWrapper, "@MonthlyTime", DbType.DateTime, entity.MonthlyTime);
			database.AddInParameter(commandWrapper, "@ExpirationTime", DbType.DateTime, entity.ExpirationTime);
			database.AddInParameter(commandWrapper, "@Once", DbType.Boolean, entity.Once);
			database.AddInParameter(commandWrapper, "@ReceivedCount", DbType.Int32, entity.ReceivedCount);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@DepositCount", DbType.Int32, entity.DepositCount);

            
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

