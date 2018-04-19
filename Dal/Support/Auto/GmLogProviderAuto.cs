

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
    
    public partial class GmLogProvider
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
		/// 将IDataReader的当前记录读取到GmLogEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public GmLogEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new GmLogEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.AdminName = (System.String) reader["AdminName"];
            obj.Ip = (System.String) reader["Ip"];
            obj.OperationType = (System.Int32) reader["OperationType"];
            obj.TargetZoneId = (System.String) reader["TargetZoneId"];
            obj.TargetUser = (System.String) reader["TargetUser"];
            obj.TargetUserList = (System.String) reader["TargetUserList"];
            obj.ManagerName = (System.String) reader["ManagerName"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Memo = (System.String) reader["Memo"];
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
        public List<GmLogEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<GmLogEntity>();
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
        public GmLogProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public GmLogProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>GmLogEntity</returns>
        /// <remarks>2016/6/1 14:20:56</remarks>
        public GmLogEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GmLog_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            GmLogEntity obj=null;
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
        /// <returns>GmLogEntity列表</returns>
        /// <remarks>2016/6/1 14:20:56</remarks>
        public List<GmLogEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GmLog_GetAll");
            

            
            List<GmLogEntity> list = null;
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
        /// <remarks>2016/6/1 14:20:56</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_GmLog_Delete");
            
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
        /// <remarks>2016/6/1 14:20:56</remarks>
        public bool Insert(GmLogEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/1 14:20:56</remarks>
        public bool Insert(GmLogEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GmLog_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@AdminName", DbType.AnsiString, entity.AdminName);
			database.AddInParameter(commandWrapper, "@Ip", DbType.AnsiString, entity.Ip);
			database.AddInParameter(commandWrapper, "@OperationType", DbType.Int32, entity.OperationType);
			database.AddInParameter(commandWrapper, "@TargetZoneId", DbType.AnsiString, entity.TargetZoneId);
			database.AddInParameter(commandWrapper, "@TargetUser", DbType.AnsiString, entity.TargetUser);
			database.AddInParameter(commandWrapper, "@TargetUserList", DbType.AnsiString, entity.TargetUserList);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Memo", DbType.AnsiString, entity.Memo);
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
            
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/6/1 14:20:56</remarks>
        public bool Update(GmLogEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/1 14:20:56</remarks>
        public bool Update(GmLogEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_GmLog_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@AdminName", DbType.AnsiString, entity.AdminName);
			database.AddInParameter(commandWrapper, "@Ip", DbType.AnsiString, entity.Ip);
			database.AddInParameter(commandWrapper, "@OperationType", DbType.Int32, entity.OperationType);
			database.AddInParameter(commandWrapper, "@TargetZoneId", DbType.AnsiString, entity.TargetZoneId);
			database.AddInParameter(commandWrapper, "@TargetUser", DbType.AnsiString, entity.TargetUser);
			database.AddInParameter(commandWrapper, "@TargetUserList", DbType.AnsiString, entity.TargetUserList);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Memo", DbType.AnsiString, entity.Memo);
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

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
