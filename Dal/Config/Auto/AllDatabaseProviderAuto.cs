

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
    
    public partial class AllDatabaseProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Config;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到AllDatabaseEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public AllDatabaseEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new AllDatabaseEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ZoneName = (System.String) reader["ZoneName"];
            obj.DBType = (System.String) reader["DBType"];
            obj.DBServerName = (System.String) reader["DBServerName"];
            obj.DBName = (System.String) reader["DBName"];
            obj.UserId = (System.String) reader["UserId"];
            obj.Password = (System.String) reader["Password"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<AllDatabaseEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<AllDatabaseEntity>();
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
        public AllDatabaseProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public AllDatabaseProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>AllDatabaseEntity</returns>
        /// <remarks>2015/10/18 16:58:57</remarks>
        public AllDatabaseEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllDatabase_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            AllDatabaseEntity obj=null;
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
        /// <returns>AllDatabaseEntity列表</returns>
        /// <remarks>2015/10/18 16:58:57</remarks>
        public List<AllDatabaseEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllDatabase_GetAll");
            

            
            List<AllDatabaseEntity> list = null;
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
        /// <remarks>2015/10/18 16:58:57</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllDatabase_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            
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
        /// <remarks>2015/10/18 16:58:57</remarks>
        public bool Insert(AllDatabaseEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 16:58:57</remarks>
        public bool Insert(AllDatabaseEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllDatabase_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.AnsiString, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@DBType", DbType.AnsiString, entity.DBType);
			database.AddInParameter(commandWrapper, "@DBServerName", DbType.AnsiString, entity.DBServerName);
			database.AddInParameter(commandWrapper, "@DBName", DbType.AnsiString, entity.DBName);
			database.AddInParameter(commandWrapper, "@UserId", DbType.AnsiString, entity.UserId);
			database.AddInParameter(commandWrapper, "@Password", DbType.AnsiString, entity.Password);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2015/10/18 16:58:57</remarks>
        public bool Update(AllDatabaseEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 16:58:57</remarks>
        public bool Update(AllDatabaseEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllDatabase_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.AnsiString, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@DBType", DbType.AnsiString, entity.DBType);
			database.AddInParameter(commandWrapper, "@DBServerName", DbType.AnsiString, entity.DBServerName);
			database.AddInParameter(commandWrapper, "@DBName", DbType.AnsiString, entity.DBName);
			database.AddInParameter(commandWrapper, "@UserId", DbType.AnsiString, entity.UserId);
			database.AddInParameter(commandWrapper, "@Password", DbType.AnsiString, entity.Password);

            
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

