

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
    
    public partial class RevelationInfoProvider
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
		/// 将IDataReader的当前记录读取到RevelationInfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public RevelationInfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new RevelationInfoEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Courage = (System.Int32) reader["Courage"];
            obj.LockMark = (System.Int32) reader["LockMark"];
            obj.PresentMark = (System.Int32) reader["PresentMark"];
            obj.Schedule = (System.Int32) reader["Schedule"];
            obj.IsGeneralMark = (System.Boolean) reader["IsGeneralMark"];
            obj.PassLog = (System.String) reader["PassLog"];
            obj.FirstPassLog = (System.String) reader["FirstPassLog"];
            obj.DayMatchLog = (System.String) reader["DayMatchLog"];
            obj.Morale = (System.Int32) reader["Morale"];
            obj.IsHaveDraw = (System.Boolean) reader["IsHaveDraw"];
            obj.DrawId = (System.Guid) reader["DrawId"];
            obj.IsHook = (System.Boolean) reader["IsHook"];
            obj.HookId = (System.Guid) reader["HookId"];
            obj.RefreshData = (System.DateTime) reader["RefreshData"];
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
        public List<RevelationInfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<RevelationInfoEntity>();
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
        public RevelationInfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public RevelationInfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>RevelationInfoEntity</returns>
        /// <remarks>2017/1/10 17:13:02</remarks>
        public RevelationInfoEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationInfo_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            RevelationInfoEntity obj=null;
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
        /// <returns>RevelationInfoEntity列表</returns>
        /// <remarks>2017/1/10 17:13:02</remarks>
        public List<RevelationInfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationInfo_GetAll");
            

            
            List<RevelationInfoEntity> list = null;
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
        /// <remarks>2017/1/10 17:13:02</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationInfo_Delete");
            
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
        /// <remarks>2017/1/10 17:13:02</remarks>
        public bool Insert(RevelationInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationInfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Courage", DbType.Int32, entity.Courage);
			database.AddInParameter(commandWrapper, "@LockMark", DbType.Int32, entity.LockMark);
			database.AddInParameter(commandWrapper, "@PresentMark", DbType.Int32, entity.PresentMark);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@IsGeneralMark", DbType.Boolean, entity.IsGeneralMark);
			database.AddInParameter(commandWrapper, "@PassLog", DbType.AnsiString, entity.PassLog);
			database.AddInParameter(commandWrapper, "@FirstPassLog", DbType.AnsiString, entity.FirstPassLog);
			database.AddInParameter(commandWrapper, "@DayMatchLog", DbType.AnsiString, entity.DayMatchLog);
			database.AddInParameter(commandWrapper, "@Morale", DbType.Int32, entity.Morale);
			database.AddInParameter(commandWrapper, "@IsHaveDraw", DbType.Boolean, entity.IsHaveDraw);
			database.AddInParameter(commandWrapper, "@DrawId", DbType.Guid, entity.DrawId);
			database.AddInParameter(commandWrapper, "@IsHook", DbType.Boolean, entity.IsHook);
			database.AddInParameter(commandWrapper, "@HookId", DbType.Guid, entity.HookId);
			database.AddInParameter(commandWrapper, "@RefreshData", DbType.Date, entity.RefreshData);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
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
        /// <remarks>2017/1/10 17:13:02</remarks>
        public bool Update(RevelationInfoEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationInfo_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Courage", DbType.Int32, entity.Courage);
			database.AddInParameter(commandWrapper, "@LockMark", DbType.Int32, entity.LockMark);
			database.AddInParameter(commandWrapper, "@PresentMark", DbType.Int32, entity.PresentMark);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@IsGeneralMark", DbType.Boolean, entity.IsGeneralMark);
			database.AddInParameter(commandWrapper, "@PassLog", DbType.AnsiString, entity.PassLog);
			database.AddInParameter(commandWrapper, "@FirstPassLog", DbType.AnsiString, entity.FirstPassLog);
			database.AddInParameter(commandWrapper, "@DayMatchLog", DbType.AnsiString, entity.DayMatchLog);
			database.AddInParameter(commandWrapper, "@Morale", DbType.Int32, entity.Morale);
			database.AddInParameter(commandWrapper, "@IsHaveDraw", DbType.Boolean, entity.IsHaveDraw);
			database.AddInParameter(commandWrapper, "@DrawId", DbType.Guid, entity.DrawId);
			database.AddInParameter(commandWrapper, "@IsHook", DbType.Boolean, entity.IsHook);
			database.AddInParameter(commandWrapper, "@HookId", DbType.Guid, entity.HookId);
			database.AddInParameter(commandWrapper, "@RefreshData", DbType.Date, entity.RefreshData);
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

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
