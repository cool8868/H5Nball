

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
    
    public partial class TransferDropoutProvider
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
		/// 将IDataReader的当前记录读取到TransferDropoutEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TransferDropoutEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TransferDropoutEntity();
			
            obj.DomaId = (System.Int32) reader["DomaId"];
            obj.DropOutType = (System.Int32) reader["DropOutType"];
            obj.DropOutNumber = (System.Int32) reader["DropOutNumber"];
            obj.RefreshTiem = (System.DateTime) reader["RefreshTiem"];
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
        public List<TransferDropoutEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TransferDropoutEntity>();
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
        public TransferDropoutProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TransferDropoutProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="domaId">domaId</param>
        /// <returns>TransferDropoutEntity</returns>
        /// <remarks>2016/10/26 10:33:12</remarks>
        public TransferDropoutEntity GetById( System.Int32 domaId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TransferDropout_GetById");
            
			database.AddInParameter(commandWrapper, "@DomaId", DbType.Int32, domaId);

            
            TransferDropoutEntity obj=null;
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
        /// <returns>TransferDropoutEntity列表</returns>
        /// <remarks>2016/10/26 10:33:12</remarks>
        public List<TransferDropoutEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TransferDropout_GetAll");
            

            
            List<TransferDropoutEntity> list = null;
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
		/// <param name="domaId">domaId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/10/26 10:33:12</remarks>
        public bool Delete ( System.Int32 domaId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_TransferDropout_Delete");
            
			database.AddInParameter(commandWrapper, "@DomaId", DbType.Int32, domaId);

            
            
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
        /// <remarks>2016/10/26 10:33:12</remarks>
        public bool Insert(TransferDropoutEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/10/26 10:33:12</remarks>
        public bool Insert(TransferDropoutEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TransferDropout_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@DomaId", DbType.Int32, entity.DomaId);
			database.AddInParameter(commandWrapper, "@DropOutType", DbType.Int32, entity.DropOutType);
			database.AddInParameter(commandWrapper, "@DropOutNumber", DbType.Int32, entity.DropOutNumber);
			database.AddInParameter(commandWrapper, "@RefreshTiem", DbType.Date, entity.RefreshTiem);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/10/26 10:33:12</remarks>
        public bool Update(TransferDropoutEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/10/26 10:33:12</remarks>
        public bool Update(TransferDropoutEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_TransferDropout_Update");
            
			database.AddInParameter(commandWrapper, "@DomaId", DbType.Int32, entity.DomaId);
			database.AddInParameter(commandWrapper, "@DropOutType", DbType.Int32, entity.DropOutType);
			database.AddInParameter(commandWrapper, "@DropOutNumber", DbType.Int32, entity.DropOutNumber);
			database.AddInParameter(commandWrapper, "@RefreshTiem", DbType.Date, entity.RefreshTiem);
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
