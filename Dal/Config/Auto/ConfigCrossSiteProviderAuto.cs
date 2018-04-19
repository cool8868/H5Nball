

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
    
    public partial class ConfigCrosssiteProvider
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
		/// 将IDataReader的当前记录读取到ConfigCrosssiteEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigCrosssiteEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigCrosssiteEntity();
			
            obj.Id = (System.Int32) reader["Id"];
            obj.DomainId = (System.Int32) reader["DomainId"];
            obj.DomainName = (System.String) reader["DomainName"];
            obj.SiteId = (System.String) reader["SiteId"];
            obj.SiteName = (System.String) reader["SiteName"];
            obj.ShowSiteId = (System.String) reader["ShowSiteId"];
            obj.ShowSiteName = (System.String) reader["ShowSiteName"];
            obj.InvalidFlag = (System.Int32) reader["InvalidFlag"];
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
        public List<ConfigCrosssiteEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigCrosssiteEntity>();
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
        public ConfigCrosssiteProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigCrosssiteProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="id">id</param>
        /// <returns>ConfigCrosssiteEntity</returns>
        /// <remarks>2016/3/11 14:44:58</remarks>
        public ConfigCrosssiteEntity GetById( System.Int32 id)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCrosssite_GetById");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, id);

            
            ConfigCrosssiteEntity obj=null;
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
        /// <returns>ConfigCrosssiteEntity列表</returns>
        /// <remarks>2016/3/11 14:44:58</remarks>
        public List<ConfigCrosssiteEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCrosssite_GetAll");
            

            
            List<ConfigCrosssiteEntity> list = null;
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
		/// <param name="id">id</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/3/11 14:44:58</remarks>
        public bool Delete ( System.Int32 id,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigCrosssite_Delete");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, id);

            
            
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
        /// <remarks>2016/3/11 14:44:58</remarks>
        public bool Insert(ConfigCrosssiteEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/3/11 14:44:58</remarks>
        public bool Insert(ConfigCrosssiteEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCrosssite_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@DomainName", DbType.String, entity.DomainName);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@SiteName", DbType.String, entity.SiteName);
			database.AddInParameter(commandWrapper, "@ShowSiteId", DbType.String, entity.ShowSiteId);
			database.AddInParameter(commandWrapper, "@ShowSiteName", DbType.String, entity.ShowSiteName);
			database.AddInParameter(commandWrapper, "@InvalidFlag", DbType.Int32, entity.InvalidFlag);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Id", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Id);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Id=(System.Int32)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/3/11 14:44:58</remarks>
        public bool Update(ConfigCrosssiteEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/3/11 14:44:58</remarks>
        public bool Update(ConfigCrosssiteEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigCrosssite_Update");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, entity.Id);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
			database.AddInParameter(commandWrapper, "@DomainName", DbType.String, entity.DomainName);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@SiteName", DbType.String, entity.SiteName);
			database.AddInParameter(commandWrapper, "@ShowSiteId", DbType.String, entity.ShowSiteId);
			database.AddInParameter(commandWrapper, "@ShowSiteName", DbType.String, entity.ShowSiteName);
			database.AddInParameter(commandWrapper, "@InvalidFlag", DbType.Int32, entity.InvalidFlag);
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

            entity.Id=(System.Int32)database.GetParameterValue(commandWrapper, "@Id");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

