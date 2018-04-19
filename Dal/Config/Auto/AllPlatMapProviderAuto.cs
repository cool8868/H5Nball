

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
    
    public partial class AllPlatmapProvider
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
		/// 将IDataReader的当前记录读取到AllPlatmapEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public AllPlatmapEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new AllPlatmapEntity();
			
            obj.PlatCode = (System.String) reader["PlatCode"];
            obj.PlatName = (System.String) reader["PlatName"];
            obj.ShareDomain = (System.String) reader["ShareDomain"];
            obj.SessionMode = (System.Int32) reader["SessionMode"];
            obj.AppKey = (System.String) reader["AppKey"];
            obj.AppSecret = (System.String) reader["AppSecret"];
            obj.LoginKey = (System.String) reader["LoginKey"];
            obj.PayKey = (System.String) reader["PayKey"];
            obj.PayPointRate = (System.Int32) reader["PayPointRate"];
            obj.PlatMainUrl = (System.String) reader["PlatMainUrl"];
            obj.PlatApiUrl = (System.String) reader["PlatApiUrl"];
            obj.PayUrl = (System.String) reader["PayUrl"];
            obj.BbsUrl = (System.String) reader["BbsUrl"];
            obj.NavUrl = (System.String) reader["NavUrl"];
            obj.CdnUrl = (System.String) reader["CdnUrl"];
            obj.ChatUrl = (System.String) reader["ChatUrl"];
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
        public List<AllPlatmapEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<AllPlatmapEntity>();
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
        public AllPlatmapProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public AllPlatmapProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="platCode">platCode</param>
        /// <returns>AllPlatmapEntity</returns>
        /// <remarks>2015/10/18 18:56:38</remarks>
        public AllPlatmapEntity GetById( System.String platCode)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllPlatmap_GetById");
            
			database.AddInParameter(commandWrapper, "@PlatCode", DbType.AnsiString, platCode);

            
            AllPlatmapEntity obj=null;
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
        /// <returns>AllPlatmapEntity列表</returns>
        /// <remarks>2015/10/18 18:56:38</remarks>
        public List<AllPlatmapEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllPlatmap_GetAll");
            

            
            List<AllPlatmapEntity> list = null;
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
		/// <param name="platCode">platCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/18 18:56:38</remarks>
        public bool Delete ( System.String platCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllPlatmap_Delete");
            
			database.AddInParameter(commandWrapper, "@PlatCode", DbType.AnsiString, platCode);

            
            
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
        /// <remarks>2015/10/18 18:56:38</remarks>
        public bool Insert(AllPlatmapEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 18:56:38</remarks>
        public bool Insert(AllPlatmapEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllPlatmap_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@PlatCode", DbType.AnsiString, entity.PlatCode);
			database.AddInParameter(commandWrapper, "@PlatName", DbType.AnsiString, entity.PlatName);
			database.AddInParameter(commandWrapper, "@ShareDomain", DbType.AnsiString, entity.ShareDomain);
			database.AddInParameter(commandWrapper, "@SessionMode", DbType.Int32, entity.SessionMode);
			database.AddInParameter(commandWrapper, "@AppKey", DbType.AnsiString, entity.AppKey);
			database.AddInParameter(commandWrapper, "@AppSecret", DbType.AnsiString, entity.AppSecret);
			database.AddInParameter(commandWrapper, "@LoginKey", DbType.AnsiString, entity.LoginKey);
			database.AddInParameter(commandWrapper, "@PayKey", DbType.AnsiString, entity.PayKey);
			database.AddInParameter(commandWrapper, "@PayPointRate", DbType.Int32, entity.PayPointRate);
			database.AddInParameter(commandWrapper, "@PlatMainUrl", DbType.AnsiString, entity.PlatMainUrl);
			database.AddInParameter(commandWrapper, "@PlatApiUrl", DbType.AnsiString, entity.PlatApiUrl);
			database.AddInParameter(commandWrapper, "@PayUrl", DbType.AnsiString, entity.PayUrl);
			database.AddInParameter(commandWrapper, "@BbsUrl", DbType.AnsiString, entity.BbsUrl);
			database.AddInParameter(commandWrapper, "@NavUrl", DbType.AnsiString, entity.NavUrl);
			database.AddInParameter(commandWrapper, "@CdnUrl", DbType.AnsiString, entity.CdnUrl);
			database.AddInParameter(commandWrapper, "@ChatUrl", DbType.AnsiString, entity.ChatUrl);
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
        /// <remarks>2015/10/18 18:56:38</remarks>
        public bool Update(AllPlatmapEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/10/18 18:56:38</remarks>
        public bool Update(AllPlatmapEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_AllPlatmap_Update");
            
			database.AddInParameter(commandWrapper, "@PlatCode", DbType.AnsiString, entity.PlatCode);
			database.AddInParameter(commandWrapper, "@PlatName", DbType.AnsiString, entity.PlatName);
			database.AddInParameter(commandWrapper, "@ShareDomain", DbType.AnsiString, entity.ShareDomain);
			database.AddInParameter(commandWrapper, "@SessionMode", DbType.Int32, entity.SessionMode);
			database.AddInParameter(commandWrapper, "@AppKey", DbType.AnsiString, entity.AppKey);
			database.AddInParameter(commandWrapper, "@AppSecret", DbType.AnsiString, entity.AppSecret);
			database.AddInParameter(commandWrapper, "@LoginKey", DbType.AnsiString, entity.LoginKey);
			database.AddInParameter(commandWrapper, "@PayKey", DbType.AnsiString, entity.PayKey);
			database.AddInParameter(commandWrapper, "@PayPointRate", DbType.Int32, entity.PayPointRate);
			database.AddInParameter(commandWrapper, "@PlatMainUrl", DbType.AnsiString, entity.PlatMainUrl);
			database.AddInParameter(commandWrapper, "@PlatApiUrl", DbType.AnsiString, entity.PlatApiUrl);
			database.AddInParameter(commandWrapper, "@PayUrl", DbType.AnsiString, entity.PayUrl);
			database.AddInParameter(commandWrapper, "@BbsUrl", DbType.AnsiString, entity.BbsUrl);
			database.AddInParameter(commandWrapper, "@NavUrl", DbType.AnsiString, entity.NavUrl);
			database.AddInParameter(commandWrapper, "@CdnUrl", DbType.AnsiString, entity.CdnUrl);
			database.AddInParameter(commandWrapper, "@ChatUrl", DbType.AnsiString, entity.ChatUrl);
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

