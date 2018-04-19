

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
    
    public partial class RevelationHookProvider
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
		/// 将IDataReader的当前记录读取到RevelationHookEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public RevelationHookEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new RevelationHookEntity();
			
            obj.HookId = (System.Guid) reader["HookId"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.MarkId = (System.Int32) reader["MarkId"];
            obj.Schedule = (System.Int32) reader["Schedule"];
            obj.ScoreLog = (System.String) reader["ScoreLog"];
            obj.ItemString = (System.String) reader["ItemString"];
            obj.Status = (System.Int32) reader["Status"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
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
        public List<RevelationHookEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<RevelationHookEntity>();
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
        public RevelationHookProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public RevelationHookProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="hookId">hookId</param>
        /// <returns>RevelationHookEntity</returns>
        /// <remarks>2017/1/13 14:20:16</remarks>
        public RevelationHookEntity GetById( System.Guid hookId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationHook_GetById");
            
			database.AddInParameter(commandWrapper, "@HookId", DbType.Guid, hookId);

            
            RevelationHookEntity obj=null;
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
        /// <returns>RevelationHookEntity列表</returns>
        /// <remarks>2017/1/13 14:20:16</remarks>
        public List<RevelationHookEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationHook_GetAll");
            

            
            List<RevelationHookEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetHookList
		
		/// <summary>
        /// GetHookList
        /// </summary>
        /// <returns>RevelationHookEntity列表</returns>
        /// <remarks>2017/1/13 14:20:16</remarks>
        public List<RevelationHookEntity> GetHookList( System.DateTime dateTime)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_RevelationHook_GetHookList");
            
			database.AddInParameter(commandWrapper, "@dateTime", DbType.Date, dateTime);

            
            List<RevelationHookEntity> list = null;
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
		/// <param name="hookId">hookId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2017/1/13 14:20:16</remarks>
        public bool Delete ( System.Guid hookId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationHook_Delete");
            
			database.AddInParameter(commandWrapper, "@HookId", DbType.Guid, hookId);

            
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
        /// <remarks>2017/1/13 14:20:16</remarks>
        public bool Insert(RevelationHookEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationHook_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@ScoreLog", DbType.AnsiString, entity.ScoreLog);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.AnsiString, entity.ItemString);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@HookId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.HookId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.HookId=(System.Guid)database.GetParameterValue(commandWrapper, "@HookId");
            
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
        /// <remarks>2017/1/13 14:20:16</remarks>
        public bool Update(RevelationHookEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationHook_Update");
            
			database.AddInParameter(commandWrapper, "@HookId", DbType.Guid, entity.HookId);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@ScoreLog", DbType.AnsiString, entity.ScoreLog);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.AnsiString, entity.ItemString);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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

            entity.HookId=(System.Guid)database.GetParameterValue(commandWrapper, "@HookId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
