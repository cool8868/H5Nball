

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
    
    public partial class CrossrobotManagerProvider
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
		/// 将IDataReader的当前记录读取到CrossrobotManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public CrossrobotManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new CrossrobotManagerEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.SiteId = (System.String) reader["SiteId"];
            obj.CrossCrowd = (System.Boolean) reader["CrossCrowd"];
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
        public List<CrossrobotManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<CrossrobotManagerEntity>();
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
        public CrossrobotManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public CrossrobotManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>CrossrobotManagerEntity</returns>
        /// <remarks>2016-08-23 16:24:47</remarks>
        public CrossrobotManagerEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossrobotManager_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            CrossrobotManagerEntity obj=null;
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
        /// <returns>CrossrobotManagerEntity列表</returns>
        /// <remarks>2016-08-23 16:24:47</remarks>
        public List<CrossrobotManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossrobotManager_GetAll");
            

            
            List<CrossrobotManagerEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetCrossCrowd
		
		/// <summary>
        /// GetCrossCrowd
        /// </summary>
        /// <returns>CrossrobotManagerEntity列表</returns>
        /// <remarks>2016-08-23 16:24:47</remarks>
        public List<CrossrobotManagerEntity> GetCrossCrowd()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossRobotManager_GetCrossCrowd");
            

            
            List<CrossrobotManagerEntity> list = null;
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
        /// <remarks>2016-08-23 16:24:47</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossrobotManager_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            
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
		
		#region  UpdateStatus
		
		/// <summary>
        /// UpdateStatus
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="status">status</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-08-23 16:24:47</remarks>
        public bool UpdateStatus ( System.Guid managerId, System.Int32 status,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossRobotManager_UpdateStatus");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);

            
            
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
        /// <remarks>2016-08-23 16:24:47</remarks>
        public bool Insert(CrossrobotManagerEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-23 16:24:47</remarks>
        public bool Insert(CrossrobotManagerEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrossrobotManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@CrossCrowd", DbType.Boolean, entity.CrossCrowd);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016-08-23 16:24:47</remarks>
        public bool Update(CrossrobotManagerEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-08-23 16:24:47</remarks>
        public bool Update(CrossrobotManagerEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrossrobotManager_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@CrossCrowd", DbType.Boolean, entity.CrossCrowd);
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

            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
