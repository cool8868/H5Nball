

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
    
    public partial class PayContinuingProvider
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
		/// 将IDataReader的当前记录读取到PayContinuingEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public PayContinuingEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new PayContinuingEntity();
			
            obj.Account = (System.String) reader["Account"];
            obj.LastPayDate = (System.DateTime) reader["LastPayDate"];
            obj.ContinuingDay = (System.Int32) reader["ContinuingDay"];
            obj.StartDate = (System.DateTime) reader["StartDate"];
            obj.EndDate = (System.DateTime) reader["EndDate"];
            obj.TotalPoint = (System.Int32) reader["TotalPoint"];
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
        public List<PayContinuingEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<PayContinuingEntity>();
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
        public PayContinuingProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public PayContinuingProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="account">account</param>
        /// <returns>PayContinuingEntity</returns>
        /// <remarks>2016/6/17 10:29:03</remarks>
        public PayContinuingEntity GetById( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PayContinuing_GetById");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            PayContinuingEntity obj=null;
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
        /// <returns>PayContinuingEntity列表</returns>
        /// <remarks>2016/6/17 10:29:03</remarks>
        public List<PayContinuingEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PayContinuing_GetAll");
            

            
            List<PayContinuingEntity> list = null;
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
		/// <param name="account">account</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:03</remarks>
        public bool Delete ( System.String account,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PayContinuing_Delete");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
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
		
		#region  UpdateContinueday
		
		/// <summary>
        /// UpdateContinueday
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="curPoint">curPoint</param>
		/// <param name="needPoint">needPoint</param>
		/// <param name="yesterday">yesterday</param>
		/// <param name="today">today</param>
		/// <param name="curTime">curTime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:03</remarks>
        public bool UpdateContinueday ( System.String account, System.Int32 curPoint, System.Int32 needPoint, System.DateTime yesterday, System.DateTime today, System.DateTime curTime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_UpdateContinueday");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@CurPoint", DbType.Int32, curPoint);
			database.AddInParameter(commandWrapper, "@NeedPoint", DbType.Int32, needPoint);
			database.AddInParameter(commandWrapper, "@Yesterday", DbType.DateTime, yesterday);
			database.AddInParameter(commandWrapper, "@Today", DbType.DateTime, today);
			database.AddInParameter(commandWrapper, "@CurTime", DbType.DateTime, curTime);

            
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
		
		#region  ContinueReset
		
		/// <summary>
        /// ContinueReset
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="today">today</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:03</remarks>
        public bool ContinueReset ( System.String account, System.DateTime today,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ContinueReset");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@Today", DbType.DateTime, today);

            
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
        /// <remarks>2016/6/17 10:29:03</remarks>
        public bool Insert(PayContinuingEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PayContinuing_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@LastPayDate", DbType.DateTime, entity.LastPayDate);
			database.AddInParameter(commandWrapper, "@ContinuingDay", DbType.Int32, entity.ContinuingDay);
			database.AddInParameter(commandWrapper, "@StartDate", DbType.DateTime, entity.StartDate);
			database.AddInParameter(commandWrapper, "@EndDate", DbType.DateTime, entity.EndDate);
			database.AddInParameter(commandWrapper, "@TotalPoint", DbType.Int32, entity.TotalPoint);
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
        /// <remarks>2016/6/17 10:29:03</remarks>
        public bool Update(PayContinuingEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PayContinuing_Update");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@LastPayDate", DbType.DateTime, entity.LastPayDate);
			database.AddInParameter(commandWrapper, "@ContinuingDay", DbType.Int32, entity.ContinuingDay);
			database.AddInParameter(commandWrapper, "@StartDate", DbType.DateTime, entity.StartDate);
			database.AddInParameter(commandWrapper, "@EndDate", DbType.DateTime, entity.EndDate);
			database.AddInParameter(commandWrapper, "@TotalPoint", DbType.Int32, entity.TotalPoint);
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

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
