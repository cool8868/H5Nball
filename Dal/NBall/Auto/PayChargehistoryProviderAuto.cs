

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
    
    public partial class PayChargehistoryProvider
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
		/// 将IDataReader的当前记录读取到PayChargehistoryEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public PayChargehistoryEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new PayChargehistoryEntity();
			
            obj.Idx = (System.String) reader["Idx"];
            obj.Account = (System.String) reader["Account"];
            obj.SourceType = (System.Int32) reader["SourceType"];
            obj.BillingId = (System.String) reader["BillingId"];
            obj.Point = (System.Int32) reader["Point"];
            obj.Bonus = (System.Int32) reader["Bonus"];
            obj.Cash = (System.Decimal) reader["Cash"];
            obj.IsFirst = (System.Boolean) reader["IsFirst"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.MallCode = (System.Int32) reader["MallCode"];
            obj.States = (System.Int32) reader["States"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<PayChargehistoryEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<PayChargehistoryEntity>();
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
        public PayChargehistoryProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public PayChargehistoryProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>PayChargehistoryEntity</returns>
        /// <remarks>2016/6/17 10:29:12</remarks>
        public PayChargehistoryEntity GetById( System.String idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PayChargehistory_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.AnsiString, idx);

            
            PayChargehistoryEntity obj=null;
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
        /// <returns>PayChargehistoryEntity列表</returns>
        /// <remarks>2016/6/17 10:29:12</remarks>
        public List<PayChargehistoryEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PayChargehistory_GetAll");
            

            
            List<PayChargehistoryEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByAccount
		
		/// <summary>
        /// GetByAccount
        /// </summary>
        /// <returns>PayChargehistoryEntity列表</returns>
        /// <remarks>2016/6/17 10:29:12</remarks>
        public List<PayChargehistoryEntity> GetByAccount( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PayCharge_GetByAccount");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            List<PayChargehistoryEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetForActivity
		
		/// <summary>
        /// GetForActivity
        /// </summary>
        /// <returns>PayChargehistoryEntity列表</returns>
        /// <remarks>2016/6/17 10:29:12</remarks>
        public List<PayChargehistoryEntity> GetForActivity( System.String account, System.DateTime startTime, System.DateTime endTime)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PayCharge_GetForActivity");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);

            
            List<PayChargehistoryEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  SelectOrderNum
		
		/// <summary>
        /// SelectOrderNum
        /// </summary>
		/// <param name="orderId">orderId</param>
		/// <param name="sTime">sTime</param>
		/// <param name="eTime">eTime</param>
        /// <returns>Int32</returns>
        /// <remarks>2016/6/17 10:29:12</remarks>
        public Int32 SelectOrderNum ( System.Guid orderId, System.DateTime sTime, System.DateTime eTime)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_SelectOrderNum");
            
			database.AddInParameter(commandWrapper, "@OrderId", DbType.Guid, orderId);
			database.AddInParameter(commandWrapper, "@sTime", DbType.DateTime, sTime);
			database.AddInParameter(commandWrapper, "@eTime", DbType.DateTime, eTime);

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:12</remarks>
        public bool Delete ( System.String idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_PayChargehistory_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.AnsiString, idx);

            
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
		
		#region  GetPointForActivity
		
		/// <summary>
        /// GetPointForActivity
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="point">point</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:12</remarks>
        public bool GetPointForActivity ( System.String account, System.DateTime startTime, System.DateTime endTime,ref  System.Int32 point,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PayCharge_GetPointForActivity");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddParameter(commandWrapper, "@Point", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,point);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            point=(System.Int32)database.GetParameterValue(commandWrapper, "@Point");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetPointForActivityNoTime
		
		/// <summary>
        /// GetPointForActivityNoTime
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="point">point</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:12</remarks>
        public bool GetPointForActivityNoTime ( System.String account,ref  System.Int32 point,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_PayCharge_GetPointForActivityNoTime");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddParameter(commandWrapper, "@Point", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,point);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            point=(System.Int32)database.GetParameterValue(commandWrapper, "@Point");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ChargeCSDK
		
		/// <summary>
        /// ChargeCSDK
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="sourceType">sourceType</param>
		/// <param name="billingId">billingId</param>
		/// <param name="gamePoint">gamePoint</param>
		/// <param name="chargePoint">chargePoint</param>
		/// <param name="cash">cash</param>
		/// <param name="bonus">bonus</param>
		/// <param name="result">result</param>
		/// <param name="eqid">eqid</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:12</remarks>
        public bool ChargeCSDK ( System.String account, System.Int32 sourceType, System.String billingId, System.Int32 gamePoint, System.Int32 chargePoint, System.Decimal cash, System.Int32 bonus,ref  System.Int32 result, System.Int32 eqid,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Pay_ChargeCSDK");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
			database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, billingId);
			database.AddInParameter(commandWrapper, "@GamePoint", DbType.Int32, gamePoint);
			database.AddInParameter(commandWrapper, "@ChargePoint", DbType.Int32, chargePoint);
			database.AddInParameter(commandWrapper, "@Cash", DbType.Decimal, cash);
			database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, bonus);
			database.AddParameter(commandWrapper, "@Result", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,result);
			database.AddInParameter(commandWrapper, "@Eqid", DbType.Int32, eqid);

            
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
        /// <remarks>2016/6/17 10:29:12</remarks>
        public bool Insert(PayChargehistoryEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PayChargehistory_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.AnsiString, entity.Idx);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, entity.SourceType);
			database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, entity.BillingId);
			database.AddInParameter(commandWrapper, "@Point", DbType.Int32, entity.Point);
			database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, entity.Bonus);
			database.AddInParameter(commandWrapper, "@Cash", DbType.Decimal, entity.Cash);
			database.AddInParameter(commandWrapper, "@IsFirst", DbType.Boolean, entity.IsFirst);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@MallCode", DbType.Int32, entity.MallCode);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);

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
        /// <remarks>2016/6/17 10:29:12</remarks>
        public bool Update(PayChargehistoryEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PayChargehistory_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.AnsiString, entity.Idx);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, entity.SourceType);
			database.AddInParameter(commandWrapper, "@BillingId", DbType.AnsiString, entity.BillingId);
			database.AddInParameter(commandWrapper, "@Point", DbType.Int32, entity.Point);
			database.AddInParameter(commandWrapper, "@Bonus", DbType.Int32, entity.Bonus);
			database.AddInParameter(commandWrapper, "@Cash", DbType.Decimal, entity.Cash);
			database.AddInParameter(commandWrapper, "@IsFirst", DbType.Boolean, entity.IsFirst);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@MallCode", DbType.Int32, entity.MallCode);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);

            
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
