

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
    
    public partial class AllSitemapProvider
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
		/// 将IDataReader的当前记录读取到AllSitemapEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public AllSitemapEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new AllSitemapEntity();
			
            obj.Id = (System.Int32) reader["Id"];
            obj.PlatCode = (System.String) reader["PlatCode"];
            obj.PlatSiteId = (System.String) reader["PlatSiteId"];
            obj.SiteId = (System.String) reader["SiteId"];
            obj.SiteName = (System.String) reader["SiteName"];
            obj.PendStartTime = (System.DateTime) reader["PendStartTime"];
            obj.PendEndTime = (System.DateTime) reader["PendEndTime"];
            obj.SiteState = (System.String) reader["SiteState"];
            obj.PlatMainUrl = (System.String) reader["PlatMainUrl"];
            obj.PlatApiUrl = (System.String) reader["PlatApiUrl"];
            obj.PayUrl = (System.String) reader["PayUrl"];
            obj.BbsUrl = (System.String) reader["BbsUrl"];
            obj.NavUrl = (System.String) reader["NavUrl"];
            obj.CdnUrl = (System.String) reader["CdnUrl"];
            obj.ChatUrl = (System.String) reader["ChatUrl"];
            obj.SiteMainUrl = (System.String) reader["SiteMainUrl"];
            obj.SiteLoginUrl = (System.String) reader["SiteLoginUrl"];
            obj.SiteApiUrl = (System.String) reader["SiteApiUrl"];
            obj.SiteSvcUrl = (System.String) reader["SiteSvcUrl"];
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
        public List<AllSitemapEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<AllSitemapEntity>();
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
        public AllSitemapProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public AllSitemapProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="id">id</param>
        /// <returns>AllSitemapEntity</returns>
        /// <remarks>2015/10/18 18:56:48</remarks>
        public AllSitemapEntity GetById( System.Int32 id)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllSitemap_GetById");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, id);

            
            AllSitemapEntity obj=null;
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
        /// <returns>AllSitemapEntity列表</returns>
        /// <remarks>2015/10/18 18:56:48</remarks>
        public List<AllSitemapEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllSitemap_GetAll");
            

            
            List<AllSitemapEntity> list = null;
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
        /// <remarks>2015/10/18 18:56:48</remarks>
        public bool Delete ( System.Int32 id,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllSitemap_Delete");
            
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
        /// <remarks>2015/10/18 18:56:48</remarks>
        public bool Insert(AllSitemapEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 18:56:48</remarks>
        public bool Insert(AllSitemapEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllSitemap_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@PlatCode", DbType.AnsiString, entity.PlatCode);
			database.AddInParameter(commandWrapper, "@PlatSiteId", DbType.AnsiString, entity.PlatSiteId);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@SiteName", DbType.AnsiString, entity.SiteName);
			database.AddInParameter(commandWrapper, "@PendStartTime", DbType.DateTime, entity.PendStartTime);
			database.AddInParameter(commandWrapper, "@PendEndTime", DbType.DateTime, entity.PendEndTime);
			database.AddInParameter(commandWrapper, "@SiteState", DbType.AnsiString, entity.SiteState);
			database.AddInParameter(commandWrapper, "@PlatMainUrl", DbType.AnsiString, entity.PlatMainUrl);
			database.AddInParameter(commandWrapper, "@PlatApiUrl", DbType.AnsiString, entity.PlatApiUrl);
			database.AddInParameter(commandWrapper, "@PayUrl", DbType.AnsiString, entity.PayUrl);
			database.AddInParameter(commandWrapper, "@BbsUrl", DbType.AnsiString, entity.BbsUrl);
			database.AddInParameter(commandWrapper, "@NavUrl", DbType.AnsiString, entity.NavUrl);
			database.AddInParameter(commandWrapper, "@CdnUrl", DbType.AnsiString, entity.CdnUrl);
			database.AddInParameter(commandWrapper, "@ChatUrl", DbType.AnsiString, entity.ChatUrl);
			database.AddInParameter(commandWrapper, "@SiteMainUrl", DbType.AnsiString, entity.SiteMainUrl);
			database.AddInParameter(commandWrapper, "@SiteLoginUrl", DbType.AnsiString, entity.SiteLoginUrl);
			database.AddInParameter(commandWrapper, "@SiteApiUrl", DbType.AnsiString, entity.SiteApiUrl);
			database.AddInParameter(commandWrapper, "@SiteSvcUrl", DbType.AnsiString, entity.SiteSvcUrl);
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
        /// <remarks>2015/10/18 18:56:48</remarks>
        public bool Update(AllSitemapEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 18:56:48</remarks>
        public bool Update(AllSitemapEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllSitemap_Update");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int32, entity.Id);
			database.AddInParameter(commandWrapper, "@PlatCode", DbType.AnsiString, entity.PlatCode);
			database.AddInParameter(commandWrapper, "@PlatSiteId", DbType.AnsiString, entity.PlatSiteId);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@SiteName", DbType.AnsiString, entity.SiteName);
			database.AddInParameter(commandWrapper, "@PendStartTime", DbType.DateTime, entity.PendStartTime);
			database.AddInParameter(commandWrapper, "@PendEndTime", DbType.DateTime, entity.PendEndTime);
			database.AddInParameter(commandWrapper, "@SiteState", DbType.AnsiString, entity.SiteState);
			database.AddInParameter(commandWrapper, "@PlatMainUrl", DbType.AnsiString, entity.PlatMainUrl);
			database.AddInParameter(commandWrapper, "@PlatApiUrl", DbType.AnsiString, entity.PlatApiUrl);
			database.AddInParameter(commandWrapper, "@PayUrl", DbType.AnsiString, entity.PayUrl);
			database.AddInParameter(commandWrapper, "@BbsUrl", DbType.AnsiString, entity.BbsUrl);
			database.AddInParameter(commandWrapper, "@NavUrl", DbType.AnsiString, entity.NavUrl);
			database.AddInParameter(commandWrapper, "@CdnUrl", DbType.AnsiString, entity.CdnUrl);
			database.AddInParameter(commandWrapper, "@ChatUrl", DbType.AnsiString, entity.ChatUrl);
			database.AddInParameter(commandWrapper, "@SiteMainUrl", DbType.AnsiString, entity.SiteMainUrl);
			database.AddInParameter(commandWrapper, "@SiteLoginUrl", DbType.AnsiString, entity.SiteLoginUrl);
			database.AddInParameter(commandWrapper, "@SiteApiUrl", DbType.AnsiString, entity.SiteApiUrl);
			database.AddInParameter(commandWrapper, "@SiteSvcUrl", DbType.AnsiString, entity.SiteSvcUrl);
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

