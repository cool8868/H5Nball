

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
    
    public partial class PayUserProvider
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
		/// 将IDataReader的当前记录读取到PayUserEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public PayUserEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new PayUserEntity();
			
            obj.Account = (System.String) reader["Account"];
            obj.Point = (System.Int32) reader["Point"];
            obj.Bonus = (System.Int32) reader["Bonus"];
            obj.TotalCash = (System.Decimal) reader["TotalCash"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
            obj.ChargePoint = (System.Int32) reader["ChargePoint"];
            obj.BindPoint = (System.Int32) reader["BindPoint"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<PayUserEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<PayUserEntity>();
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
        public PayUserProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public PayUserProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="account">account</param>
        /// <returns>PayUserEntity</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public PayUserEntity GetById( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PayUser_GetById");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            PayUserEntity obj=null;
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
		
		#region  GetPointByManagerId
		
		/// <summary>
        /// GetPointByManagerId
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>PayUserEntity</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public PayUserEntity GetPointByManagerId( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_GetPointByManagerId");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            PayUserEntity obj=null;
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
        /// <returns>PayUserEntity列表</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public List<PayUserEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PayUser_GetAll");
            

            
            List<PayUserEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Charge
		
		/// <summary>
        /// Charge
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="sourceType">sourceType</param>
		/// <param name="billingId">billingId</param>
		/// <param name="gamePoint">gamePoint</param>
		/// <param name="chargePoint">chargePoint</param>
		/// <param name="cash">cash</param>
		/// <param name="bonus">bonus</param>
		/// <param name="result">result</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool Charge ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 gamePoint, System.Int32 chargePoint, System.Decimal cash, System.Int32 bonus,ref  System.Int32 result,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_Charge");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
			database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, billingId);
			database.AddInParameter(commandWrapper, "@GamePoint", DbType.Int32, gamePoint);
			database.AddInParameter(commandWrapper, "@ChargePoint", DbType.Int32, chargePoint);
			database.AddInParameter(commandWrapper, "@Cash", DbType.Decimal, cash);
			database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, bonus);
			database.AddParameter(commandWrapper, "@Result", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,result);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            result=(System.Int32)database.GetParameterValue(commandWrapper, "@Result");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ConsumePoint
		
		/// <summary>
        /// ConsumePoint
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="managerId">managerId</param>
		/// <param name="sourceType">sourceType</param>
		/// <param name="sourceId">sourceId</param>
		/// <param name="consumePoint">consumePoint</param>
		/// <param name="consumeTime">consumeTime</param>
		/// <param name="rowVersion">rowVersion</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool ConsumePoint ( System.String account, System.Guid managerId, System.Int32 sourceType, System.String sourceId, System.Int32 consumePoint, System.DateTime consumeTime, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ConsumePoint");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
			database.AddInParameter(commandWrapper, "@SourceId", DbType.AnsiString, sourceId);
			database.AddInParameter(commandWrapper, "@ConsumePoint", DbType.Int32, consumePoint);
			database.AddInParameter(commandWrapper, "@ConsumeTime", DbType.DateTime, consumeTime);
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
		
		#region  ConsumePointForGamble
		
		/// <summary>
        /// ConsumePointForGamble
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="managerId">managerId</param>
		/// <param name="sourceType">sourceType</param>
		/// <param name="sourceId">sourceId</param>
		/// <param name="consumePoint">consumePoint</param>
		/// <param name="consumeTime">consumeTime</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool ConsumePointForGamble ( System.String account, System.Guid managerId, System.Int32 sourceType, System.String sourceId, System.Int32 consumePoint, System.DateTime consumeTime,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ConsumePointForGamble");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
			database.AddInParameter(commandWrapper, "@SourceId", DbType.AnsiString, sourceId);
			database.AddInParameter(commandWrapper, "@ConsumePoint", DbType.Int32, consumePoint);
			database.AddInParameter(commandWrapper, "@ConsumeTime", DbType.DateTime, consumeTime);
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
		
		#region  Stat
		
		/// <summary>
        /// Stat
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="cash">cash</param>
		/// <param name="point">point</param>
		/// <param name="bonus">bonus</param>
		/// <param name="cPoint">cPoint</param>
		/// <param name="cBonus">cBonus</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool Stat ( System.String account,ref  System.Int32 cash,ref  System.Int32 point,ref  System.Int32 bonus,ref  System.Int32 cPoint,ref  System.Int32 cBonus,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_Stat");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddParameter(commandWrapper, "@Cash", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,cash);
			database.AddParameter(commandWrapper, "@Point", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,point);
			database.AddParameter(commandWrapper, "@Bonus", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,bonus);
			database.AddParameter(commandWrapper, "@CPoint", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,cPoint);
			database.AddParameter(commandWrapper, "@CBonus", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,cBonus);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            cash=(System.Int32)database.GetParameterValue(commandWrapper, "@Cash");
            point=(System.Int32)database.GetParameterValue(commandWrapper, "@Point");
            bonus=(System.Int32)database.GetParameterValue(commandWrapper, "@Bonus");
            cPoint=(System.Int32)database.GetParameterValue(commandWrapper, "@CPoint");
            cBonus=(System.Int32)database.GetParameterValue(commandWrapper, "@CBonus");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ChargeTest
		
		/// <summary>
        /// ChargeTest
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="sourceType">sourceType</param>
		/// <param name="billingId">billingId</param>
		/// <param name="gamePoint">gamePoint</param>
		/// <param name="cash">cash</param>
		/// <param name="bonus">bonus</param>
		/// <param name="curTime">curTime</param>
		/// <param name="result">result</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool ChargeTest ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 gamePoint, System.Int32 cash, System.Int32 bonus, System.DateTime curTime,ref  System.Int32 result,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ChargeTest");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
			database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, billingId);
			database.AddInParameter(commandWrapper, "@GamePoint", DbType.Int32, gamePoint);
			database.AddInParameter(commandWrapper, "@Cash", DbType.Int32, cash);
			database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, bonus);
			database.AddInParameter(commandWrapper, "@CurTime", DbType.DateTime, curTime);
			database.AddParameter(commandWrapper, "@Result", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,result);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            result=(System.Int32)database.GetParameterValue(commandWrapper, "@Result");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetGmChargePoint
		
		/// <summary>
        /// GetGmChargePoint
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="totalPoint">totalPoint</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool GetGmChargePoint ( System.String account,ref  System.Int32 totalPoint,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_GetGmChargePoint");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddParameter(commandWrapper, "@TotalPoint", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,totalPoint);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            totalPoint=(System.Int32)database.GetParameterValue(commandWrapper, "@TotalPoint");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ChargeForBonus
		
		/// <summary>
        /// ChargeForBonus
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="sourceType">sourceType</param>
		/// <param name="billingId">billingId</param>
		/// <param name="bonus">bonus</param>
		/// <param name="result">result</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool ChargeForBonus ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 bonus,ref  System.Int32 result,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ChargeForBonus");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
			database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, billingId);
			database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, bonus);
			database.AddParameter(commandWrapper, "@Result", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,result);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            result=(System.Int32)database.GetParameterValue(commandWrapper, "@Result");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetGmChargePointByTime
		
		/// <summary>
        /// GetGmChargePointByTime
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="totalPoint">totalPoint</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool GetGmChargePointByTime ( System.String account, System.DateTime startTime, System.DateTime endTime,ref  System.Int32 totalPoint,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_GetGmChargePointByTime");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddParameter(commandWrapper, "@TotalPoint", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,totalPoint);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            totalPoint=(System.Int32)database.GetParameterValue(commandWrapper, "@TotalPoint");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ChargeForBindPoint
		
		/// <summary>
        /// ChargeForBindPoint
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="sourceType">sourceType</param>
		/// <param name="billingId">billingId</param>
		/// <param name="bindPoint">bindPoint</param>
		/// <param name="result">result</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool ChargeForBindPoint ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 bindPoint,ref  System.Int32 result,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ChargeForBindPoint");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
			database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, billingId);
			database.AddInParameter(commandWrapper, "@BindPoint", DbType.Int32, bindPoint);
			database.AddParameter(commandWrapper, "@Result", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,result);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            result=(System.Int32)database.GetParameterValue(commandWrapper, "@Result");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ConsumeBindPoint
		
		/// <summary>
        /// ConsumeBindPoint
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="managerId">managerId</param>
		/// <param name="sourceType">sourceType</param>
		/// <param name="sourceId">sourceId</param>
		/// <param name="consumeBindPoint">consumeBindPoint</param>
		/// <param name="consumeTime">consumeTime</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool ConsumeBindPoint ( System.String account, System.Guid managerId, System.Int32 sourceType, System.String sourceId, System.Int32 consumeBindPoint, System.DateTime consumeTime,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ConsumeBindPoint");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
			database.AddInParameter(commandWrapper, "@SourceId", DbType.AnsiString, sourceId);
			database.AddInParameter(commandWrapper, "@ConsumeBindPoint", DbType.Int32, consumeBindPoint);
			database.AddInParameter(commandWrapper, "@ConsumeTime", DbType.DateTime, consumeTime);
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
		
		#region  ChargeForPoint
		
		/// <summary>
        /// ChargeForPoint
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="sourceType">sourceType</param>
		/// <param name="billingId">billingId</param>
		/// <param name="point">point</param>
		/// <param name="bonus">bonus</param>
		/// <param name="result">result</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool ChargeForPoint ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 point, System.Int32 bonus,ref  System.Int32 result,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ChargeForPoint");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
			database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, billingId);
			database.AddInParameter(commandWrapper, "@Point", DbType.Int32, point);
			database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, bonus);
			database.AddParameter(commandWrapper, "@Result", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,result);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            result=(System.Int32)database.GetParameterValue(commandWrapper, "@Result");
            
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
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool Insert(PayUserEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PayUser_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Point", DbType.Int32, entity.Point);
			database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, entity.Bonus);
			database.AddInParameter(commandWrapper, "@TotalCash", DbType.Decimal, entity.TotalCash);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);
			database.AddInParameter(commandWrapper, "@ChargePoint", DbType.Int32, entity.ChargePoint);
			database.AddInParameter(commandWrapper, "@BindPoint", DbType.Int32, entity.BindPoint);

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
        /// <remarks>2016/6/17 10:28:37</remarks>
        public bool Update(PayUserEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PayUser_Update");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Point", DbType.Int32, entity.Point);
			database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, entity.Bonus);
			database.AddInParameter(commandWrapper, "@TotalCash", DbType.Decimal, entity.TotalCash);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);
			database.AddInParameter(commandWrapper, "@ChargePoint", DbType.Int32, entity.ChargePoint);
			database.AddInParameter(commandWrapper, "@BindPoint", DbType.Int32, entity.BindPoint);

            
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
