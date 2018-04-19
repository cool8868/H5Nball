

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
    
    public partial class CrossladderHookProvider
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
		/// 将IDataReader的当前记录读取到CrossladderHookEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public CrossladderHookEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new CrossladderHookEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.SiteId = (System.String) reader["SiteId"];
            obj.CurTimes = (System.Int32) reader["CurTimes"];
            obj.CurWiningTimes = (System.Int32) reader["CurWiningTimes"];
            obj.MaxTimes = (System.Int32) reader["MaxTimes"];
            obj.MinScore = (System.Int32) reader["MinScore"];
            obj.MaxScore = (System.Int32) reader["MaxScore"];
            obj.MaxWiningTimes = (System.Int32) reader["MaxWiningTimes"];
            obj.NextMatchTime = (System.DateTime) reader["NextMatchTime"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.Expired = (System.DateTime) reader["Expired"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<CrossladderHookEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<CrossladderHookEntity>();
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
        public CrossladderHookProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public CrossladderHookProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="siteId">siteId</param>
        /// <returns>CrossladderHookEntity</returns>
        /// <remarks>2016-09-14 15:52:48</remarks>
        public CrossladderHookEntity GetById( System.Guid managerId, System.String siteId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossLadderHook_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, siteId);

            
            CrossladderHookEntity obj=null;
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
        /// <returns>CrossladderHookEntity列表</returns>
        /// <remarks>2016-09-14 15:52:48</remarks>
        public List<CrossladderHookEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossladderHook_GetAll");
            

            
            List<CrossladderHookEntity> list = null;
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
        /// <remarks>2016-09-14 15:52:48</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_CrossladderHook_Delete");
            
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
		
		#region  End
		
		/// <summary>
        /// End
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="status">status</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-09-14 15:52:48</remarks>
        public bool End ( System.Guid managerId, System.Int32 status,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossLadderHook_End");
            
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
        /// <remarks>2016-09-14 15:52:48</remarks>
        public bool Insert(CrossladderHookEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-09-14 15:52:48</remarks>
        public bool Insert(CrossladderHookEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrossladderHook_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@CurTimes", DbType.Int32, entity.CurTimes);
			database.AddInParameter(commandWrapper, "@CurWiningTimes", DbType.Int32, entity.CurWiningTimes);
			database.AddInParameter(commandWrapper, "@MaxTimes", DbType.Int32, entity.MaxTimes);
			database.AddInParameter(commandWrapper, "@MinScore", DbType.Int32, entity.MinScore);
			database.AddInParameter(commandWrapper, "@MaxScore", DbType.Int32, entity.MaxScore);
			database.AddInParameter(commandWrapper, "@MaxWiningTimes", DbType.Int32, entity.MaxWiningTimes);
			database.AddInParameter(commandWrapper, "@NextMatchTime", DbType.DateTime, entity.NextMatchTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@Expired", DbType.DateTime, entity.Expired);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016-09-14 15:52:48</remarks>
        public bool Update(CrossladderHookEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016-09-14 15:52:48</remarks>
        public bool Update(CrossladderHookEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CrossladderHook_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@CurTimes", DbType.Int32, entity.CurTimes);
			database.AddInParameter(commandWrapper, "@CurWiningTimes", DbType.Int32, entity.CurWiningTimes);
			database.AddInParameter(commandWrapper, "@MaxTimes", DbType.Int32, entity.MaxTimes);
			database.AddInParameter(commandWrapper, "@MinScore", DbType.Int32, entity.MinScore);
			database.AddInParameter(commandWrapper, "@MaxScore", DbType.Int32, entity.MaxScore);
			database.AddInParameter(commandWrapper, "@MaxWiningTimes", DbType.Int32, entity.MaxWiningTimes);
			database.AddInParameter(commandWrapper, "@NextMatchTime", DbType.DateTime, entity.NextMatchTime);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@Expired", DbType.DateTime, entity.Expired);

            
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
